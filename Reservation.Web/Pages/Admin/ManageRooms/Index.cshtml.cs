using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Reservation.Models;
using Reservation.Web.Data;

namespace Reservation.Web.Pages.Admin.ManageRooms
{
    public class IndexModel : PageModel
    {
        private readonly Reservation.Web.Data.ApplicationDbContext _context;

        public IndexModel(Reservation.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<RoomModel> RoomModel { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Rooms != null)
            {
                RoomModel = await _context.Rooms.ToListAsync();
            }
        }
    }
}
