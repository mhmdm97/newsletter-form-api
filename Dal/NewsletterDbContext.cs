using Microsoft.EntityFrameworkCore;
using newsletter_form_api.Dal.Entities;

namespace newsletter_form_api.Dal
{
    public class NewsletterDbContext(DbContextOptions<NewsletterDbContext> options) : DbContext(options)
    {
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<CommunicationPreference> CommunicationPreferences { get; set; }
        public DbSet<Interest> Interests { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Set UpdatedAt for modified entities
            foreach (var entry in ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified && e.Entity is BaseEntity))
            {
                ((BaseEntity)entry.Entity).UpdatedAt = DateTime.UtcNow;
            }
            
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply global query filter for soft delete
            modelBuilder.Entity<Subscriber>().HasQueryFilter(s => !s.IsDeleted);
            modelBuilder.Entity<CommunicationPreference>().HasQueryFilter(cp => !cp.IsDeleted);
            modelBuilder.Entity<Interest>().HasQueryFilter(i => !i.IsDeleted);

            // Configure many-to-many relationship between Subscriber and CommunicationPreference
            modelBuilder.Entity<Subscriber>()
                .HasMany(s => s.CommunicationPreferences)
                .WithMany(c => c.Subscribers)
                .UsingEntity(j => j.ToTable("SubscriberCommunicationPreferences"));

            // Configure many-to-many relationship between Subscriber and Interest
            modelBuilder.Entity<Subscriber>()
                .HasMany(s => s.Interests)
                .WithMany(i => i.Subscribers)
                .UsingEntity(j => j.ToTable("SubscriberInterests"));
                
            // Add seed data for interests
            modelBuilder.Entity<Interest>().HasData(
                new Interest { Id = 1, Name = "Houses" },
                new Interest { Id = 2, Name = "Apartments" },
                new Interest { Id = 3, Name = "Shared ownership" },
                new Interest { Id = 4, Name = "Rental" },
                new Interest { Id = 5, Name = "Land sourcing" }
            );
            
            // Add seed data for communication preferences
            modelBuilder.Entity<CommunicationPreference>().HasData(
                new CommunicationPreference { Id = 1, Tag = "Email" },
                new CommunicationPreference { Id = 2, Tag = "SMS" }
            );
        }
    }
}