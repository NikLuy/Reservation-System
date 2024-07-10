using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Models
{
    public class UserReservation
    {
        public int Id { get; set; } 
        public virtual AppUser User {  get; set; }
        public virtual RoomModel Room  {  get; set; }
        public string Comment { get; set; }
        public ReservationState State { get; set; }
        public DateTimeOffset From { get; set; }
        public DateTimeOffset To { get; set; }

    }
}
