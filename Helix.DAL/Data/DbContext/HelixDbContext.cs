

using Microsoft.EntityFrameworkCore;

namespace HelixAssignment.DAL
{
    public class HelixDbContext : DbContext
    {
        public HelixDbContext(DbContextOptions<HelixDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)   
        {                                                 
            modelBuilder.Entity<HelixEventProduct>()             
                .HasKey(x => new { x.EventId, x.ProductId });


            /*******************************************************
            /**** Model query filter *************************************
            #A This adds a filter to all accesses to the Book entities. You can bypass this filter by using the IgnoreQueryFilters() operator
            modelBuilder.Entity<Book>()             //#A
                .HasQueryFilter(p => !p.SoftDeleted);//#A
            */
        }


        public DbSet<HelixEvent> HelixEvents { get; set; }

        public DbSet<HelixEventProduct> HelixEventProducts { get; set; }

        public DbSet<Product> Products { get; set; }

       
    }
}