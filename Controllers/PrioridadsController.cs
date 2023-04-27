using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace Tareasv2.Controllers
{
    public class PrioridadsController : Controller
    {
        private readonly TareasDBv3Context _context;

        public PrioridadsController(TareasDBv3Context context)
        {
            _context = context;
        }

        // GET: Prioridads
        public async Task<IActionResult> Index()
        {
              return _context.Prioridads != null ? 
                          View(await _context.Prioridads.ToListAsync()) :
                          Problem("Entity set 'TareasDBv3Context.Prioridads'  is null.");
        }

        // GET: Prioridads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Prioridads == null)
            {
                return NotFound();
            }

            var prioridad = await _context.Prioridads
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prioridad == null)
            {
                return NotFound();
            }

            return View(prioridad);
        }

        // GET: Prioridads/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Prioridads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,Color,Orden")] Prioridad prioridad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prioridad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prioridad);
        }

        // GET: Prioridads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Prioridads == null)
            {
                return NotFound();
            }

            var prioridad = await _context.Prioridads.FindAsync(id);
            if (prioridad == null)
            {
                return NotFound();
            }
            return View(prioridad);
        }

        // POST: Prioridads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,Color,Orden")] Prioridad prioridad)
        {
            if (id != prioridad.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prioridad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrioridadExists(prioridad.Id))
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
            return View(prioridad);
        }

        // GET: Prioridads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Prioridads == null)
            {
                return NotFound();
            }

            var prioridad = await _context.Prioridads
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prioridad == null)
            {
                return NotFound();
            }

            return View(prioridad);
        }

        // POST: Prioridads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Prioridads == null)
            {
                return Problem("Entity set 'TareasDBv3Context.Prioridads'  is null.");
            }
            var prioridad = await _context.Prioridads.FindAsync(id);
            if (prioridad != null)
            {
                _context.Prioridads.Remove(prioridad);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrioridadExists(int id)
        {
          return (_context.Prioridads?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
