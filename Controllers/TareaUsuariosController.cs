using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace Tareasv2.Controllers
{
    public class TareaUsuariosController : Controller
    {
        private readonly TareasDBv3Context _context;

        public TareaUsuariosController(TareasDBv3Context context)
        {
            _context = context;
        }

        // GET: TareaUsuarios
        public async Task<IActionResult> Index()
        {
            var tareasDBv3Context = _context.TareaUsuarios.Include(t => t.IdTareaNavigation).Include(t => t.IdUsuarioNavigation);
            return View(await tareasDBv3Context.ToListAsync());
        }

        // GET: TareaUsuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TareaUsuarios == null)
            {
                return NotFound();
            }

            var tareaUsuario = await _context.TareaUsuarios
                .Include(t => t.IdTareaNavigation)
                .Include(t => t.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tareaUsuario == null)
            {
                return NotFound();
            }

            return View(tareaUsuario);
        }

        // GET: TareaUsuarios/Create
        public IActionResult Create()
        {
            ViewData["IdTarea"] = new SelectList(_context.Tareas, "Id", "Descripcion");
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Nombres");
            return View();
        }

        // POST: TareaUsuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdTarea,IdUsuario")] TareaUsuario tareaUsuario)
        {
            if (tareaUsuario!=null)
            {
                _context.Add(tareaUsuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTarea"] = new SelectList(_context.Tareas, "Id", "Id", tareaUsuario.IdTarea);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Id", tareaUsuario.IdUsuario);
            return View(tareaUsuario);
        }

        // GET: TareaUsuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TareaUsuarios == null)
            {
                return NotFound();
            }

            var tareaUsuario = await _context.TareaUsuarios.FindAsync(id);
            if (tareaUsuario == null)
            {
                return NotFound();
            }
            ViewData["IdTarea"] = new SelectList(_context.Tareas, "Id", "Id", tareaUsuario.IdTarea);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Id", tareaUsuario.IdUsuario);
            return View(tareaUsuario);
        }

        // POST: TareaUsuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdTarea,IdUsuario")] TareaUsuario tareaUsuario)
        {
            if (id != tareaUsuario.Id)
            {
                return NotFound();
            }

            if (tareaUsuario != null)
            {
                try
                {
                    _context.Update(tareaUsuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TareaUsuarioExists(tareaUsuario.Id))
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
            ViewData["IdTarea"] = new SelectList(_context.Tareas, "Id", "Id", tareaUsuario.IdTarea);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Id", tareaUsuario.IdUsuario);
            return View(tareaUsuario);
        }

        // GET: TareaUsuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TareaUsuarios == null)
            {
                return NotFound();
            }

            var tareaUsuario = await _context.TareaUsuarios
                .Include(t => t.IdTareaNavigation)
                .Include(t => t.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tareaUsuario == null)
            {
                return NotFound();
            }

            return View(tareaUsuario);
        }

        // POST: TareaUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TareaUsuarios == null)
            {
                return Problem("Entity set 'TareasDBv3Context.TareaUsuarios'  is null.");
            }
            var tareaUsuario = await _context.TareaUsuarios.FindAsync(id);
            if (tareaUsuario != null)
            {
                _context.TareaUsuarios.Remove(tareaUsuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TareaUsuarioExists(int id)
        {
          return (_context.TareaUsuarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
