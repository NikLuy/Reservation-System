using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Reservation.Models;
using Reservation.Web.Data;

namespace Reservation.Web.Pages.Admin.ManageRooms
{
    public class CreateModel : PageModel
    {
        private readonly Reservation.Web.Data.ApplicationDbContext _context;

        public CreateModel(Reservation.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public RoomModel RoomModel { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Rooms == null || RoomModel == null)
            {
                return Page();
            }

            _context.Rooms.Add(RoomModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
