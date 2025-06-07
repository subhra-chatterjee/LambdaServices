namespace SimpleApiLambda.DTO
{
    public class CreateProductDto
    {

        public string? Name { get; set; }
        public string? Category { get; set; }
        public decimal? Price { get; set; }
        public bool? InStock { get; set; }
        public string? CreatedDate { get; set; }
    }
}
