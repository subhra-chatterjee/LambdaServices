using Amazon.DynamoDBv2.DataModel;

[DynamoDBTable("product")]
public class Product
{
    [DynamoDBHashKey("id")] // Partition Key
    public int? Id { get; set; }

    [DynamoDBProperty("name")]
    public string? Name { get; set; }

    [DynamoDBProperty("category")]
    public string? Category { get; set; }

    [DynamoDBProperty("price")]
    public decimal? Price { get; set; }

    [DynamoDBProperty("inStock")]
    public bool? InStock { get; set; }

    [DynamoDBProperty("createdDate")]
    public string? CreatedDate { get; set; }
}
