namespace ProductApi.Domain.Entities
{
    public class Category : BaseEntity
    {
        public Category()
        {
            categoryProducts = new List<CategoryProduct>();
        }

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<CategoryProduct> categoryProducts { get; set; }
    }
}
