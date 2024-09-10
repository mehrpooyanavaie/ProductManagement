namespace ProductApi.Application.DTOs
{
    public class RedisProductDTO
    {
        public int Id { get; set;}
        
        public string Name { get; set; }

        public DateTime ProduceDate { get; set; }

        public string ManufacturePhone { get; set; }

        public string ManufactureEmail { get; set; }
        public bool IsAvailable { get; set; }
        public string UserId { get; set; }

        public List<int> CategoryIds { get; set; }
    }
}