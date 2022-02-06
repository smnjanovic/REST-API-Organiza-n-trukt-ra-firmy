using kolo2.Entities;
using kolo2.Enums;
using System;

namespace kolo2.DTOs
{
    public static class DTOFormatter
    {
        private static string GetFullname(string fn, string ln, string tbn, string tan)
        {
            string result = fn + " " + ln;
            if (tbn != null && tbn != "") result = tbn + " " + result;
            if (tan != null && tan != "") result += ", " + tan;
            Console.WriteLine("titles: " + tan + " " + tbn);
            return result;
        }

        public static EmployeeDTO Convert(Employee emp) => emp is null ? null : new()
        {
            Id = emp.id,
            Fullname = GetFullname(emp.first_name, emp.last_name, emp.title_before_name, emp.title_after_name),
            Email = emp.email,
            Phone = emp.phone
        };

        public static NodeDTO Convert(Node node) => node is null ? null : new()
        {
            Id = node.id,
            Type = ((NodeType)node.node_type).ToString(),
            Name = node.node_name,
            Boss = "",
            Super = ""
        };
    }
}
