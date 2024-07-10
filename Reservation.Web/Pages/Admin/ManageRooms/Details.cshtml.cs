﻿using System;
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
    public class DetailsModel : PageModel
    {
        private readonly Reservation.Web.Data.ApplicationDbContext _context;

        public DetailsModel(Reservation.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public RoomModel RoomModel { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Rooms == null)
            {
                return NotFound();
            }

            var roommodel = await _context.Rooms.FirstOrDefaultAsync(m => m.Id == id);
            if (roommodel == null)
            {
                return NotFound();
            }
            else 
            {
                RoomModel = roommodel;
            }
            return Page();
        }
    }
}
