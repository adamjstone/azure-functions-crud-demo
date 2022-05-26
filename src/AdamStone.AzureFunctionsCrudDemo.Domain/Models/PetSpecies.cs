using System.Runtime.Serialization;

namespace AdamStone.AzureFunctionsCrudDemo.Domain.Models
{
    /// <summary>
    /// Represents a value specifying the species of a <see cref="Pet" />.
    /// </summary>
    [DataContract]
    public enum PetSpecies
    {
        /// <summary>
        /// The <see cref="Pet" /> is a cat.
        /// </summary>
        [EnumMember]
        Cat = 1,

        /// <summary>
        /// The <see cref="Pet" /> is a dog.
        /// </summary>
        [EnumMember]
        Dog = 2,
    }
}
