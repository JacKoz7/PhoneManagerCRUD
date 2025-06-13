using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    /// Represents a mobile phone in the system
    public class Phone
    {
        /// Unique identifier for the phone
        public int Id { get; set; }
        
        /// Name/model of the phone (max 15 characters)
        [Required]
        [StringLength(15, ErrorMessage = "Name cannot be longer than 15 characters")]
        public string Name { get; set; } = string.Empty;
        
        /// Manufacturer of the phone
        [Required]
        public string Manufacturer { get; set; } = string.Empty;
        
        /// Phone number (exactly 9 digits)
        [Required]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Phone number must be exactly 9 digits")]
        public string PhoneNumber { get; set; } = string.Empty;
        
        /// Price of the phone
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value")]
        public decimal Price { get; set; }
        
        /// Whether the phone is in stock
        public bool InStock { get; set; } = true;
    }
}