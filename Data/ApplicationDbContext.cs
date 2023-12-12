using BE_WiseWallet.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BE_WiseWallet.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        //public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Split> Splits { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Member> Members { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Member>()
                .HasKey(m => new { m.TeamId, m.UserId });

            builder.Entity<Team>()
                .HasMany(t => t.Members)
                .WithOne(m => m.Team);

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Teams)
                .WithOne(m => m.User);

        }
    }
}
