using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceBay.Data;
using ServiceBay.Dto;

namespace ServiceBay.Controllers
{
    public class ApiBidController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApiBidController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ApiBid
        public async Task<IActionResult> Index()
        {
            return View(await _context.BidForCreationDto.ToListAsync());
        }

        // GET: ApiBid/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bidForCreationDto = await _context.BidForCreationDto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bidForCreationDto == null)
            {
                return NotFound();
            }

            return View(bidForCreationDto);
        }

        // GET: ApiBid/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApiBid/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Price")] BidForCreationDto bidForCreationDto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bidForCreationDto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bidForCreationDto);
        }

        // GET: ApiBid/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bidForCreationDto = await _context.BidForCreationDto.FindAsync(id);
            if (bidForCreationDto == null)
            {
                return NotFound();
            }
            return View(bidForCreationDto);
        }

        // POST: ApiBid/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Price")] BidForCreationDto bidForCreationDto)
        {
            if (id != bidForCreationDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bidForCreationDto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BidForCreationDtoExists(bidForCreationDto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bidForCreationDto);
        }

        // GET: ApiBid/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bidForCreationDto = await _context.BidForCreationDto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bidForCreationDto == null)
            {
                return NotFound();
            }

            return View(bidForCreationDto);
        }

        // POST: ApiBid/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bidForCreationDto = await _context.BidForCreationDto.FindAsync(id);
            _context.BidForCreationDto.Remove(bidForCreationDto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BidForCreationDtoExists(int id)
        {
            return _context.BidForCreationDto.Any(e => e.Id == id);
        }
    }
}
