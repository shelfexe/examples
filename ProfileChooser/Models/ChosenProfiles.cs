namespace Profile_directory
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ChosenProfiles
    {
        [Key]
        public int ChosenProfileId { get; set; }

        public int UserId { get; set; }

        public int? Priority { get; set; }

        public int ProfileId { get; set; }

        public virtual Profiles Profiles { get; set; }

        public virtual Users Users { get; set; }
    }
}
