using Core.Security.Entities;
using Domain.Entities;
using Domain.Entities.CrossTableEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Persistence.Contexts
{
    public class RestaurantDbContext : DbContext
    {
        public IConfiguration _configuration { get; set; }
        public DbSet<Owner> Owners{ get; set; }
        public DbSet<Restaurant> Restaurants{ get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<CampaignDrinks> CampaignDrinks { get; set; }
        public DbSet<CampaignFoods> CampaignFoods { get; set; }
        public DbSet<RefreshToken> RefreshTokens{ get; set; }


        public RestaurantDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RestaurantDbContext).Assembly);

            modelBuilder.Entity<Restaurant>(r =>
            {
                r.HasOne(r => r.Owner)
                .WithMany()
                .HasForeignKey(r =>r.OwnerId);

                r.HasOne(r => r.Menu)
                .WithOne(m => m.Resturant);
            });

            modelBuilder.Entity<Menu>(m =>
            {
                m.HasMany(m => m.Campaigns)
                .WithOne(c => c.Menu)
                .HasForeignKey(x => x.MenuId);

                m.HasMany(m => m.Categories)
                .WithOne(c => c.Menu)
                .HasForeignKey(c => c.MenuId);
            });

            modelBuilder.Entity<Category>(c =>
            {
                c.HasMany(c => c.Foods)
                .WithOne(f => f.Category);

                c.HasMany(c => c.Drinks)
                .WithOne(d => d.Category);
            });

            modelBuilder.Entity<Campaign>(c =>
            {
                c.HasOne(c => c.Menu);

            });

            modelBuilder.Entity<CampaignFoods>(c =>
            {
                c.HasKey(c => new { c.CampaignId, c.FoodId });

                c.HasOne(c => c.Food)
                .WithMany(f => f.Campaigns)
                .HasForeignKey(key => key.FoodId)
                .OnDelete(DeleteBehavior.NoAction);


                c.HasOne(c => c.Campaign)
                .WithMany(c => c.Foods)
                .HasForeignKey(k => k.CampaignId)
                .OnDelete(DeleteBehavior.NoAction);



            });

            modelBuilder.Entity<CampaignDrinks>(c =>
            {
                c.HasKey(c => new { c.CampaignId, c.DrinkId });

                c.HasOne(c => c.Drink)
                .WithMany(d => d.Campaigns)
                .HasForeignKey(key => key.DrinkId)
                .OnDelete(DeleteBehavior.NoAction);

                c.HasOne(c => c.Campaign)
                .WithMany(c => c.Drinks)
                .HasForeignKey(key => key.CampaignId)
                .OnDelete(DeleteBehavior.NoAction);



            });

            modelBuilder.Entity<Food>(f =>
            {

                f.HasOne(f => f.Category)
                .WithMany(c => c.Foods);
            });

            modelBuilder.Entity<Drink>(d =>
            {

                d.HasOne(d => d.Category)
                .WithMany(c => c.Drinks);

            });

            modelBuilder.Entity<Owner>(o =>
            {
                o.HasMany(o => o.Restaurants)
                .WithOne(r => r.Owner);
            });

            



        }
    }
}
