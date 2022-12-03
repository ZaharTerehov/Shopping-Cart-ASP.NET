using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Models
{
	public class Product
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "Please enter a value"), 
		MinLength(2, ErrorMessage = "Minimum lendth is 2")]
		public string Name { get; set; }

		public string Slug { get; set; }

		[Required, MinLength(4, ErrorMessage = "Minimum length is 4")]
		public string Description { get; set; }

		[Required]
		[Range(0.01, double.MaxValue, ErrorMessage = "Please enter a value")]
		[Column(TypeName = "decimal(8,2)")]
		public decimal Price { get; set; }

		public Guid CategoryId { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "You must choose a category")]
        public Category Category { get; set; }

		public string Image { get; set; }

		[NotMapped]
		[FileExtensions]
		public IFormFile ImageUpload { get; set; }
	}
}
