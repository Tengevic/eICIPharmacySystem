using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coderush.Data;
using coderush.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace coderush.Controllers
{
    
    public class GoodsReceivedNoteController : Controller
    {
        private readonly ApplicationDbContext _context;
        public GoodsReceivedNoteController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Detail(int id)
        {
            GoodsReceivedNote goodsReceivedNote = await _context
                .GoodsReceivedNote
                .Include(x =>x.Bill)
                .FirstOrDefaultAsync(x => x.GoodsReceivedNoteId.Equals(id));

            if (goodsReceivedNote == null)
            {
                return NotFound();
            }

            return View(goodsReceivedNote);
        }
    }
}