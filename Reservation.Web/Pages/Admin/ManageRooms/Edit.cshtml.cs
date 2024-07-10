using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Reservation.Models;
using Reservation.Web.Data;

namespace Reservation.Web.Pages.Admin.ManageRooms
{
    public class EditModel : PageModel
    {
        private readonly Reservation.Web.Data.ApplicationDbContext _context;

        public EditModel(Reservation.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RoomModel RoomModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Rooms == null)
            {
                return NotFound();
            }

            var roommodel =  await _context.Rooms.FirstOrDefaultAsync(m => m.Id == id);
            if (roommodel == null)
            {
                return NotFound();
            }
            RoomModel = roommodel;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(RoomModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomModelExists(RoomModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RoomModelExists(int id)
        {
          return (_context.Rooms?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
