using kolo2.Enums;

namespace kolo2.Entities
{
    public record NodeDTO
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Super { get; set; }
        public string Boss { get; set; }
    }
}