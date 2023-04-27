using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace Tareasv2.Controllers
{
    public class UsuarioGruposController : Controller
    {
        private readonly TareasDBv3Context _context;

        public UsuarioGruposController(TareasDBv3Context context)
        {
            _context = context;
        }

        // GET: UsuarioGrupos
        public async Task<IActionResult> Index()
        {
            var tareasDBv3Context = _context.UsuarioGrupos.Include(u => u.IdGrupoNavigation).Include(u => u.IdUsuarioNavigation);
            return View(await tareasDBv3Context.ToListAsync());
        }

        // GET: UsuarioGrupos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UsuarioGrupos == null)
            {
                return NotFound();
            }

            var usuarioGrupo = await _context.UsuarioGrupos
                .Include(u => u.IdGrupoNavigation)
                .Include(u => u.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuarioGrupo == null)
            {
                return NotFound();
            }

            return View(usuarioGrupo);
        }

        // GET: UsuarioGrupos/Create
        public IActionResult Create()
        {
            ViewData["IdGrupo"] = new SelectList(_context.Grupos, "Id", "Nombre");
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Nombres");
            return View();
        }

        // POST: UsuarioGrupos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdUsuario,IdGrupo")] UsuarioGrupo usuarioGrupo)
        {
            if (usuarioGrupo!=null)
            {
                _context.Add(usuarioGrupo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                
            }
            ViewData["IdGrupo"] = new SelectList(_context.Grupos, "Id", "Nombre", usuarioGrupo.IdGrupo);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Nombres", usuarioGrupo.IdUsuario);
            return View(usuarioGrupo);
        }

        // GET: UsuarioGrupos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UsuarioGrupos == null)
            {
                return NotFound();
            }

            var usuarioGrupo = await _context.UsuarioGrupos.FindAsync(id);
            if (usuarioGrupo == null)
            {
                return NotFound();
            }
            ViewData["IdGrupo"] = new SelectList(_context.Grupos, "Id", "Id", usuarioGrupo.IdGrupo);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Id", usuarioGrupo.IdUsuario);
            return View(usuarioGrupo);
        }

        // POST: UsuarioGrupos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdUsuario,IdGrupo")] UsuarioGrupo usuarioGrupo)
        {
            if (id != usuarioGrupo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuarioGrupo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioGrupoExists(usuarioGrupo.Id))
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
            ViewData["IdGrupo"] = new SelectList(_context.Grupos, "Id", "Id", usuarioGrupo.IdGrupo);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Id", usuarioGrupo.IdUsuario);
            return View(usuarioGrupo);
        }

        // GET: UsuarioGrupos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UsuarioGrupos == null)
            {
                return NotFound();
            }

            var usuarioGrupo = await _context.UsuarioGrupos
                .Include(u => u.IdGrupoNavigation)
                .Include(u => u.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuarioGrupo == null)
            {
                return NotFound();
            }

            return View(usuarioGrupo);
        }

        // POST: UsuarioGrupos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UsuarioGrupos == null)
            {
                return Problem("Entity set 'TareasDBv3Context.UsuarioGrupos'  is null.");
            }
            var usuarioGrupo = await _context.UsuarioGrupos.FindAsync(id);
            if (usuarioGrupo != null)
            {
                _context.UsuarioGrupos.Remove(usuarioGrupo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioGrupoExists(int id)
        {
          return (_context.UsuarioGrupos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
