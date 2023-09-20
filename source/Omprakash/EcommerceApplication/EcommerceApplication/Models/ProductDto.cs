using System.ComponentModel.DataAnnotations;

namespace EcommerceApplication.Models
{
    public class ProductDto
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string Images { get; set; }       
        public float Price { get; set; }
        public int CategoryId { get; set; }        
        public Category Category { get; set; }
    }
}
