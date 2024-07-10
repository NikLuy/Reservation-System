using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Models
{
    public class AppUser: IdentityUser
    {
        public string Name { get; set; }
        public string Vorname { get; set; }

        //public virtual ICollection<AppUserRole> UserRoles { get; set; }
    }
}
