using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace AdamStone.AzureFunctionsCrudDemo.Domain.Models
{
    [DataContract]
    [Table("Pet")]
    public class Pet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Pet" /> class.
        /// </summary>
        public Pet()
        {
            return;
        }

        /// <summary>
        /// Gets or sets the age, in months, of the current <see cref="Pet" />.
        /// </summary>
        [DataMember]
        public Int32 Age
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the breed for the current <see cref="Pet" />.
        /// </summary>
        [DataMember]
        public String Breed
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a unique identifier for the current <see cref="Pet" />.
        /// </summary>
        [DataMember]
        [Key]
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the current <see cref="Pet" />.
        /// </summary>
        [DataMember]
        public String Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the sex of the current <see cref="Pet" />.
        /// </summary>
        [DataMember]
        public PetSex Sex
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the species of the current <see cref="Pet" />.
        /// </summary>
        [DataMember]
        public PetSpecies Species
        {
            get;
            set;
        }
    }
}