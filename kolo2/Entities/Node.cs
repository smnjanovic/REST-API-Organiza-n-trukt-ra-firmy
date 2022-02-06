using System.ComponentModel.DataAnnotations;

namespace kolo2.Entities
{
    public record Node
    {
        [Key]
        public int id { get; set; }
        public string node_name { get; set; }
        public int node_type { get; set; }
        public int? node_super { get; set; }
        public int? node_boss { get; set; }
    }
}