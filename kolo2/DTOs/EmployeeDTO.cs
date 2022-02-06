using System;

namespace kolo2.Entities
{
    public record EmployeeDTO
    {
        public long Id { get; init; }

        public string Fullname { get; init; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}