using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace Tareasv2.Controllers
{
    public class NotificacionsController : Controller
    {
        private readonly TareasDBv3Context _context;

        public NotificacionsController(TareasDBv3Context context)
        {
            _context = context;
        }

        // GET: Notificacions
        public async Task<IActionResult> Index()
        {
            var tareasDBv3Context = _context.Notificacions.Include(n => n.Proyecto).Include(n => n.Usuario);
            return View(await tareasDBv3Context.ToListAsync());
        }

        // GET: Notificacions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Notificacions == null)
            {
                return NotFound();
            }

            var notificacion = await _context.Notificacions
                .Include(n => n.Proyecto)
                .Include(n => n.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notificacion == null)
            {
                return NotFound();
            }

            return View(notificacion);
        }

        // GET: Notificacions/Create
        public IActionResult Create()
        {
            ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "Id", "Id");
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id");
            return View();
        }

        // POST: Notificacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Contenido,Fecha,UsuarioId,ProyectoId,Leida")] Notificacion notificacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(notificacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "Id", "Id", notificacion.ProyectoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", notificacion.UsuarioId);
            return View(notificacion);
        }

        // GET: Notificacions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Notificacions == null)
            {
                return NotFound();
            }

            var notificacion = await _context.Notificacions.FindAsync(id);
            if (notificacion == null)
            {
                return NotFound();
            }
            ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "Id", "Id", notificacion.ProyectoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", notificacion.UsuarioId);
            return View(notificacion);
        }

        // POST: Notificacions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Contenido,Fecha,UsuarioId,ProyectoId,Leida")] Notificacion notificacion)
        {
            if (id != notificacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notificacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotificacionExists(notificacion.Id))
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
            ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "Id", "Id", notificacion.ProyectoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", notificacion.UsuarioId);
            return View(notificacion);
        }

        // GET: Notificacions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Notificacions == null)
            {
                return NotFound();
            }

            var notificacion = await _context.Notificacions
                .Include(n => n.Proyecto)
                .Include(n => n.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notificacion == null)
            {
                return NotFound();
            }

            return View(notificacion);
        }

        // POST: Notificacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Notificacions == null)
            {
                return Problem("Entity set 'TareasDBv3Context.Notificacions'  is null.");
            }
            var notificacion = await _context.Notificacions.FindAsync(id);
            if (notificacion != null)
            {
                _context.Notificacions.Remove(notificacion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotificacionExists(int id)
        {
          return (_context.Notificacions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
