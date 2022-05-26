using AdamStone.AzureFunctionsCrudDemo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdamStone.AzureFunctionsCrudDemo.Domain.DataPersistence
{
    /// <summary>
    /// Represents a class that defines configuration for the <see cref="Pet" /> entity.
    /// </summary>
    public class PetEntityConfiguration : IEntityTypeConfiguration<Pet>
    {
        /// <summary>
        /// Configures the entity.
        /// </summary>
        /// <param name="builder">
        /// A builder that facilitates configuration of the entity.
        /// </param>
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.HasKey(nameof(Pet.Id));
        }
    }
}
