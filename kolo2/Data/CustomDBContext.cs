using kolo2.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace kolo2.Data
{
    public class CustomDBContext : DbContext
    {
        public CustomDBContext(DbContextOptions<CustomDBContext> options) : base(options)
        {

        }

        public DbSet<Employee> employees { get; set; }
        public DbSet<Node> nodes { get; set; }

    }
}
