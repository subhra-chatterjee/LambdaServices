using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using SimpleApiLambda.DTO;
using SimpleApiLambda.Services;

namespace SimpleApiLambda;

public static class Endpoint
{
    public static void MapEndpoints(WebApplication app)
    {
        IProductService productService = app.Services.GetRequiredService<IProductService>();
        app.MapGet("/", () => "Welcome to Lambda");

        // For product Endpoint

        // For Get product 
        app.MapGet("/product/{id}", async (int id, IDynamoDBContext dynamoDBContext) =>
        {
            var product = await dynamoDBContext.LoadAsync<Product>(id);
            return product is not null ? Results.Ok(product) : Results.NotFound();
        });

        //get all list of product
        app.MapGet("/product", async (IDynamoDBContext dynamoDBContext) =>
        {
            var allProduct = await productService.GetProductsAll(new Product());
            return Results.Ok(allProduct);
        });

        // For create product Endpoint
        app.MapPost("/product", async (CreateProductDto productDto, IDynamoDBContext dynamoDBContext) =>
        {
            var product = new Product
            {
                Id = new Random().Next(1, int.MaxValue),
                Name = productDto.Name,
                Category = productDto.Category,
                Price = productDto.Price,
                InStock = productDto.InStock,
                CreatedDate = productDto.CreatedDate
            };

            await dynamoDBContext.SaveAsync(product);
            return Results.Created($"/product/{product.Id}", product);
        });

        //Delete the product 
        app.MapDelete("/product/{id}", async (int id, IDynamoDBContext db) =>
        {
            var data = await db.LoadAsync<Product>(id);
            if (data is null) return Results.NotFound();

            await db.DeleteAsync(data);
            return Results.NoContent();
        });
    }
}