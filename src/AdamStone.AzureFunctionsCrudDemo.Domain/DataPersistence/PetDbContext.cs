using AdamStone.AzureFunctionsCrudDemo.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AdamStone.AzureFunctionsCrudDemo.Domain.DataPersistence
{
    /// <summary>
    /// Represents a session with the database for the <see cref="Pet" /> entity.
    /// </summary>
    public class PetDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PetDbContext" /> class.
        /// </summary>
        /// <param name="dbContextOptions"></param>
        public PetDbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
            return;
        }

        /// <summary>
        /// Configures the entity model.
        /// </summary>
        /// <param name="modelBuilder">
        /// An object that is used to configure the entity model.
        /// </param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Pet>()
                .HasNoDiscriminator()
                .ToContainer(nameof(Pet))
                .HasPartitionKey(property => property.Id)
                .HasKey(property => new { property.Id });
            modelBuilder.ApplyConfiguration(new PetEntityConfiguration());

        }

        /// <summary>
        /// Gets or sets a representation of the persisted <see cref="Pet" /> entities.
        /// </summary>
        public DbSet<Pet> Pets
        {
            get;
            set;
        }
    }
}
