using System.ComponentModel;

namespace EcommerceApplication.Models
{
	public class Category
	{
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
       
        public DateTime? CreatedAt { get; set; } 
       
        public DateTime? UpdatedAt { get; set; } 

        public DateTime? DeletedAt { get; set; } 
        public string colour { get; set; }
		public string description { get; set; }

	}
}
