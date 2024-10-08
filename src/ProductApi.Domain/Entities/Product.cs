﻿// using Microsoft.AspNetCore.Identity.
// using Microsoft.AspNetCore.Identity;
namespace ProductApi.Domain.Entities
{
    public class Product : BaseEntity
    {
        public Product()
        {
            categoryProducts = new List<CategoryProduct>();
        }

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MaxLength(100)]
        public string Name { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public DateTime ProduceDate { get; set; }
        [System.ComponentModel.DataAnnotations.Phone]
        public string ManufacturePhone { get; set; }
        [System.ComponentModel.DataAnnotations.EmailAddress]
        [System.ComponentModel.DataAnnotations.Required]
        public string ManufactureEmail { get; set; }
        public bool IsAvailable { get; set; }
        /*with out quantity*/
        public string UserId { get; set; }
        // public IdentityUser User { get; set; }
        public ICollection<CategoryProduct> categoryProducts { get; set; }
    }
}
