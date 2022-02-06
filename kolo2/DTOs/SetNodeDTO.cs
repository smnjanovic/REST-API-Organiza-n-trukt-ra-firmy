using System.ComponentModel.DataAnnotations;

namespace kolo2.Entities
{
    public record SetNodeDTO
    {
        [Required]
        public string node_name { get; set; }
        [Required]
        [Range(1,4)]
        public int node_type { get; set; }
        public int? node_super { get; set; }
        public int? node_boss { get; set; }
    }
}