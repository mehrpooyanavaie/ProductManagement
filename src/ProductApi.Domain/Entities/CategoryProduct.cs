namespace ProductApi.Domain.Entities
{
    public class CategoryProduct
{
    public int ProductId { get; set; }
    public  Product product { get; set; }

    public int CategoryId { get; set; }
    public Category category { get; set; }
}
}
