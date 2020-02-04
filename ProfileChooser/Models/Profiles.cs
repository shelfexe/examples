namespace Profile_directory
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Windows;

    public partial class Profiles 
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Profiles()
        {
            ChosenProfiles = new HashSet<ChosenProfiles>();
        }

        [Key]
        public int ProfileId { get; set; }

        [Required]
        [StringLength(60)]
        public string Name { get; set; }

        public int DirectionId { get; set; }

        [Required]
        [StringLength(10)]
        public string Faculty { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChosenProfiles> ChosenProfiles { get; set; }

        public virtual Directions Directions { get; set; }

        public Profiles Clone()
        {
            return new Profiles
            {
                Name = this.Name,
                ProfileId = this.ProfileId,
                DirectionId = this.DirectionId,
                Faculty = this.Faculty,
                Directions = this.Directions
            };
        }


    }
}
