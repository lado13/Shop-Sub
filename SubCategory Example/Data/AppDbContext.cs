using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.Model;

namespace Shop.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> Subcategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserOrder> UserOrders { get; set; }    


        public static async Task EnsureRolesCreated(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Admin", "User" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasKey(c => c.CategoryId);

            modelBuilder.Entity<SubCategory>()
                .HasKey(s => s.SubcategoryId);

            modelBuilder.Entity<Product>()
                .HasKey(p => p.ProductId);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Subcategories)
                .WithOne(s => s.Category)
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete from Category to SubCategory

            modelBuilder.Entity<SubCategory>()
                .HasMany(s => s.Products)
                .WithOne(p => p.Subcategory)
                .HasForeignKey(p => p.SubcategoryId)
                .OnDelete(DeleteBehavior.Restrict); // Restrict delete from SubCategory to Product



            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserLogin<string>>(b =>
            {
                b.HasKey(l => new { l.LoginProvider, l.ProviderKey });
            });



        }




    }
}
