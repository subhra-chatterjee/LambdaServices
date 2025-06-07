using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.Extensions.Logging;

namespace SimpleApiLambda.Services
{
    public class ProductService : IProductService
    {
        private readonly IDynamoDBContext _dynamoDBContext;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IDynamoDBContext dynamoDBContext, ILogger<ProductService> logger)
        {
            _dynamoDBContext = dynamoDBContext;
            _logger = logger;
        }

        public async Task<IEnumerable<Product>> GetProductsAll(Product product)
        {
            _logger.LogInformation("Starting retrieval of all products.");
            try
            {
                var condition = new List<ScanCondition>();
                var allProduct = await _dynamoDBContext.ScanAsync<Product>(condition).GetRemainingAsync();
                _logger.LogInformation("Successfully retrieved {Count} products.", allProduct.Count);
                return allProduct;
            }
            catch (AmazonDynamoDBException ex)
            {
                _logger.LogError(ex, "DynamoDB-specific error occurred while retrieving products.");
                throw new Exception("An error occurred while accessing DynamoDB.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while retrieving products.");
                throw new Exception("An unexpected error occurred while retrieving products.", ex);
            }
        }
    }
}
