using Microsoft.EntityFrameworkCore;

namespace AngularProject.Context
{
    public class MyDBContext : DbContext
    {
        public MyDBContext()
        { }

        public MyDBContext(DbContextOptions<MyDBContext> options)
            : base(options)
        {
        }
        public DbSet<dept2> dept2 { get; set; }
        public DbSet<items2> items2 { get; set; }
    }

}
