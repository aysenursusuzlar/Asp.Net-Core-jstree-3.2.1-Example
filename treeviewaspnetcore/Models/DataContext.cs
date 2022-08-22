using Microsoft.EntityFrameworkCore;

namespace treeviewaspnetcore.Models
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<City> City { get; set; }
        public DbSet<State> State { get; set; }
     

    }

}
