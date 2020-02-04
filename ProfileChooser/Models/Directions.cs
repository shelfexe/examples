namespace Profile_directory
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Collections.ObjectModel;

  
    public partial class Directions : IEquatable<Directions>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Directions()
        {
            ChosenDirections = new HashSet<ChosenDirections>();
            Profiles = new ObservableCollection<Profiles>();
        }

        [Key]
        public int DirectionId { get; set; }

        [Required]
        [StringLength(10)]
        public string Number { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChosenDirections> ChosenDirections { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ObservableCollection<Profiles> Profiles { get; set; }

        
        public Directions FullCopy()
        {
            var dir = new Directions();
            dir.Name = this.Name;
            dir.Number = this.Number;
            dir.DirectionId = this.DirectionId;
            foreach (Profiles p in this.Profiles)
            {
                dir.Profiles.Add(p);
            }
            return dir;
        }

        public Directions Copy()
        {
            return new Directions
            {
                Name = this.Name,
                Number = this.Number,
                DirectionId = this.DirectionId,
                Profiles = new ObservableCollection<Profiles>()
            };
        }

        public bool Equals(Directions other)
        {
            if (this.Name == other.Name) return true;
            else return false;
        }
    }

}
