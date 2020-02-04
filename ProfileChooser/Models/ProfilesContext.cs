using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections.ObjectModel;

namespace Profile_directory.Models
{
    class ProfilesContext : DbContext
    {
        public ProfilesContext() : base("Profiles") { }

        public DbSet<Users> users { get; set; }
        public DbSet<Profiles> profiles { get; set; }
        public DbSet<Directions> directions { get; set; }
        public DbSet<ChosenDirections> chosenDirections { get; set; }
        public DbSet<ChosenProfiles> chosenProfiles { get; set; }

    }
}
