namespace Profile_directory
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ChosenDirections 
    {
        [Key]
        public int ChosenDirectionId { get; set; }

        public int UserId { get; set; }

        public int? Priority { get; set; }

        public int DirectionId { get; set; }

        public virtual Directions Directions { get; set; }

        public virtual Users User { get; set; }

       
    }
}
