namespace SimpleApiLambda.Services
{
    public interface IProductService
    {
        public Task<IEnumerable<Product>> GetProductsAll(Product product);
    }
}
