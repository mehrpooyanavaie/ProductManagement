
namespace ProductApi.Application.VM
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ProduceDate { get; set; }
        public string ManufacturePhone { get; set; }
        public string ManufactureEmail { get; set; }
        public bool IsAvailable { get; set; }
        public string UserId { get; set; }
        public List<string> CategoryNames { get; set; }
    }
}