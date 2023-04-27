using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace Tareasv2.Controllers
{
    public class TareaGruposController : Controller
    {
        private readonly TareasDBv3Context _context;

        public TareaGruposController(TareasDBv3Context context)
        {
            _context = context;
        }

        // GET: TareaGrupos
        public async Task<IActionResult> Index()
        {
            var tareasDBv3Context = _context.TareaGrupos.Include(t => t.IdGrupoNavigation).Include(t => t.IdTareaNavigation);
            return View(await tareasDBv3Context.ToListAsync());
        }

        // GET: TareaGrupos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TareaGrupos == null)
            {
                return NotFound();
            }

            var tareaGrupo = await _context.TareaGrupos
                .Include(t => t.IdGrupoNavigation)
                .Include(t => t.IdTareaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tareaGrupo == null)
            {
                return NotFound();
            }

            return View(tareaGrupo);
        }

        // GET: TareaGrupos/Create
        public IActionResult Create()
        {
            ViewData["IdGrupo"] = new SelectList(_context.Grupos, "Id", "Nombre");
            ViewData["IdTarea"] = new SelectList(_context.Tareas, "Id", "Descripcion");
            return View();
        }

        // POST: TareaGrupos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdGrupo,IdTarea")] TareaGrupo tareaGrupo)
        {
            if (tareaGrupo!=null)
            {
                _context.Add(tareaGrupo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdGrupo"] = new SelectList(_context.Grupos, "Id", "Nombre", tareaGrupo.IdGrupo);
            ViewData["IdTarea"] = new SelectList(_context.Tareas, "Id", "Descripcion", tareaGrupo.IdTarea);
            return View(tareaGrupo);
        }

        // GET: TareaGrupos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TareaGrupos == null)
            {
                return NotFound();
            }

            var tareaGrupo = await _context.TareaGrupos.FindAsync(id);
            if (tareaGrupo == null)
            {
                return NotFound();
            }
            ViewData["IdGrupo"] = new SelectList(_context.Grupos, "Id", "Id", tareaGrupo.IdGrupo);
            ViewData["IdTarea"] = new SelectList(_context.Tareas, "Id", "Id", tareaGrupo.IdTarea);
            return View(tareaGrupo);
        }

        // POST: TareaGrupos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdGrupo,IdTarea")] TareaGrupo tareaGrupo)
        {
            if (id != tareaGrupo.Id)
            {
                return NotFound();
            }

            if (tareaGrupo != null)
            {
                try
                {
                    _context.Update(tareaGrupo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TareaGrupoExists(tareaGrupo.Id))
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
            ViewData["IdGrupo"] = new SelectList(_context.Grupos, "Id", "Id", tareaGrupo.IdGrupo);
            ViewData["IdTarea"] = new SelectList(_context.Tareas, "Id", "Id", tareaGrupo.IdTarea);
            return View(tareaGrupo);
        }

        // GET: TareaGrupos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TareaGrupos == null)
            {
                return NotFound();
            }

            var tareaGrupo = await _context.TareaGrupos
                .Include(t => t.IdGrupoNavigation)
                .Include(t => t.IdTareaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tareaGrupo == null)
            {
                return NotFound();
            }

            return View(tareaGrupo);
        }

        // POST: TareaGrupos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TareaGrupos == null)
            {
                return Problem("Entity set 'TareasDBv3Context.TareaGrupos'  is null.");
            }
            var tareaGrupo = await _context.TareaGrupos.FindAsync(id);
            if (tareaGrupo != null)
            {
                _context.TareaGrupos.Remove(tareaGrupo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TareaGrupoExists(int id)
        {
          return (_context.TareaGrupos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
