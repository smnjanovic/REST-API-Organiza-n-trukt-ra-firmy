using System.ComponentModel.DataAnnotations;

namespace kolo2.DTOs
{
    public record SetEmployeeDTO
    {
        public string title_before_name { get; init; }
        public string title_after_name { get; init; }
        [Required]
        public string first_name { get; init; }
        [Required]
        public string last_name { get; init; }
        [Required]
        public string email { get; init; }
        [Required]
        public string phone { get; init; } 
    }
}
