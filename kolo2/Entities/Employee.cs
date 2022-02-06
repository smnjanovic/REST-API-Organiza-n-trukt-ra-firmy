using System;
using System.ComponentModel.DataAnnotations;

namespace kolo2.Entities
{
    public record Employee
    {
        [Key]
        public int id { get; init; }

        public string title_before_name { get; set; }

        public string title_after_name { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }

        public string phone { get; set; }

        public string email { get; set; }
    }
}