using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Models
{
    public class RoomModel
    {
        public int Id { get; set; } 
        public string RoomEmail { get; set; }
        public string Name { get; set; }    
        public string Description { get; set; } 
        public int Space {  get; set; }
        public bool HasKitchen { get; set; } = false;
        public bool HasCoffeeMachine { get; set; } = false;
        public int CategoryId { get; set; } = 0;
    }
}
