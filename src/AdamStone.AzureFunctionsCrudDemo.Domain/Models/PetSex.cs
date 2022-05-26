using System.Runtime.Serialization;

namespace AdamStone.AzureFunctionsCrudDemo.Domain.Models
{
    /// <summary>
    /// Represents a value specifying the sex of a <see cref="Pet" />.
    /// </summary>
    [DataContract]
    public enum PetSex
    {
        /// <summary>
        /// The <see cref="Pet" /> is a female.
        /// </summary>
        [EnumMember]
        Female = 1,

        /// <summary>
        /// The <see cref="Pet" /> is a male.
        /// </summary>
        [EnumMember]
        Male = 2,
    }
}
