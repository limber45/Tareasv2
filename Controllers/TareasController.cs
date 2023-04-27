using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tareasv2;

using Tareasv2.Observer;

namespace Tareasv2.Controllers
{
    public class TareasController : Controller
    {
        private readonly TareasDBv3Context _context;

        public TareasController(TareasDBv3Context context)
        {
            _context = context;
        }

        // GET: Tareas
        public async Task<IActionResult> Index()
        {
            var tareasDBv3Context = _context.Tareas.Include(t => t.IdProyectoNavigation).Include(t => t.PrioridadNavigation);
            return View(await tareasDBv3Context.ToListAsync());
        }

        // GET: Tareas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tareas == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tareas
                .Include(t => t.IdProyectoNavigation)
                .Include(t => t.PrioridadNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }

        // GET: Tareas/Create
        public IActionResult Create()
        {
            ViewData["IdProyecto"] = new SelectList(_context.Proyectos, "Id", "NombreProyecto");
            ViewData["Prioridad"] = new SelectList(_context.Prioridads, "Id", "Nombre");
            return View();
        }

        // POST: Tareas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FechaC,FechaI,FechaF,IdProyecto,Descripcion,Solucion,Comentario,TiempoIdeal,Prioridad")] Tarea tarea)
        {
            if (tarea != null)
            {
                _context.Add(tarea);
                await _context.SaveChangesAsync();
                //IMPLEMENTACION DE LA NOTIFICACION

                await UsuariosSg(tarea);
                await NotificarGruposAsync(tarea);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProyecto"] = new SelectList(_context.Proyectos, "Id", "Id", tarea.IdProyecto);
            ViewData["Prioridad"] = new SelectList(_context.Prioridads, "Id", "Id", tarea.Prioridad);
            return View(tarea);
        }

        // GET: Tareas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tareas == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null)
            {
                return NotFound();
            }


            ViewData["IdProyecto"] = new SelectList(_context.Proyectos, "Id", "Id", tarea.IdProyecto);
            ViewData["Prioridad"] = new SelectList(_context.Prioridads, "Id", "Id", tarea.Prioridad);
            return View(tarea);
        }

        // POST: Tareas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FechaC,FechaI,FechaF,IdProyecto,Descripcion,Solucion,Comentario,TiempoIdeal,Prioridad")] Tarea tarea)
        {
            if (id != tarea.Id)
            {
                return NotFound();
            }

            if (tarea != null)
            {
                try
                {
                    _context.Update(tarea);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TareaExists(tarea.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

               // await NotificarUsuarioAsync(tarea);
                await UsuariosSg(tarea);
                await NotificarGruposAsync(tarea);


                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProyecto"] = new SelectList(_context.Proyectos, "Id", "Id", tarea.IdProyecto);
            ViewData["Prioridad"] = new SelectList(_context.Prioridads, "Id", "Id", tarea.Prioridad);
            return View(tarea);
        }

        // GET: Tareas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tareas == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tareas
                .Include(t => t.IdProyectoNavigation)
                .Include(t => t.PrioridadNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }

        // POST: Tareas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tareas == null)
            {
                return Problem("Entity set 'TareasDBv3Context.Tareas'  is null.");
            }
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea != null)
            {
                _context.Tareas.Remove(tarea);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TareaExists(int id)
        {
            return (_context.Tareas?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        public async Task NotificarUsuarioAsync(Tarea tarea)
        {
            //IMPLEMENTACION DE LA NOTIFICACION

            //Proyecto proyecto = await _context.Proyectos.FirstOrDefaultAsync(p => p.Id == tarea.IdProyecto);
            //TareaUsuario usertarea =  _context.TareaUsuarios.Where(p => p.Id == tarea.Id);
            //TareaUsuario usuar = await _context.TareaUsuarios.Where(p => p.IdTarea == tarea.Id).ToList();
            var tareaUsuarios = await _context.TareaUsuarios.Where(p => p.IdTarea == tarea.Id).ToListAsync();


            if (tareaUsuarios != null)
            {
                // Obtener la lista de usuarios que pertenecen al proyecto
                //List<Usuario> usuarios = _context.Usuarios.Where(u => u.Id == proyecto.IdUsuario).ToList();
                //List<Usuario> usuarios = _context.Usuarios.Where(u => u.Id == proyecto.IdUsuario).ToList();
                // Suscribir a los usuarios como observadores de la tarea modificada
                var idsUsuarios = tareaUsuarios.Select(tu => tu.IdUsuario).ToList();
                var usuarios = await _context.Usuarios.Where(u => idsUsuarios.Contains(u.Id)).ToListAsync();

                foreach (var usuario in usuarios)
                {
                    tarea.Subscribe(new UsuarioObserver(usuario));
                }
            }



            // Notifica a los observadores sobre el cambio en la tarea
            tarea.Modificar(tarea);
        }
        public async Task NotificarGruposAsync(Tarea tarea)
        {
            // Obtener el grupo de la tarea
            var tareaGrupo = await _context.TareaGrupos.FirstOrDefaultAsync(tg => tg.IdTarea == tarea.Id);
            if (tareaGrupo != null)
            {
                var grupo = await _context.Grupos.FirstOrDefaultAsync(g => g.Id == tareaGrupo.IdGrupo);
                if (grupo != null)
                {
                    var usuariosGrupo = await _context.UsuarioGrupos
                        .Where(ug => ug.IdGrupo == grupo.Id)
                        .Select(ug => ug.IdUsuario)
                        .ToListAsync();
                    var usuarios = await _context.Usuarios.Where(u => usuariosGrupo.Contains(u.Id)).ToListAsync();

                    // Realizar la notificación a los usuarios del grupo
                    foreach (var usuario in usuarios)
                    {
                        // Realizar la notificación a cada usuario, por ejemplo, enviar un correo, una notificación push, etc.
                        // Puedes implementar la lógica de notificación aquí, por ejemplo:
                        tarea.Subscribe(new UsuarioObserver(usuario));
                    }
                }
                tarea.Modificar(tarea);
            }

            // Notificar a los observadores sobre el cambio en la tarea
           // tarea.Modificar(tarea);
        }
        public async Task UsuariosSing(Tarea tarea)
        {

            var tareaUsuarios = await _context.TareaUsuarios
         .Where(tu => tu.IdTarea == tarea.Id)
         .Select(tu => tu.IdUsuario)
         .ToListAsync();
            var usuariosSinGrupo = await _context.Usuarios
                .Where(u => !_context.UsuarioGrupos.Any(ug => ug.IdUsuario == u.Id) && tareaUsuarios.Contains(u.Id))
                .ToListAsync();

            // Realizar la notificación a los usuarios que no pertenecen a ningún grupo pero sí a la tarea
            foreach (var usuario in usuariosSinGrupo)
            {
                // Realizar la notificación a cada usuario, por ejemplo, enviar un correo, una notificación push, etc.
                // Puedes implementar la lógica de notificación aquí, por ejemplo:
                tarea.Subscribe(new UsuarioObserver(usuario));
            }

            // Notificar a los observadores sobre el cambio en la tarea
            tarea.Modificar(tarea);
        }
        public async Task UsuariosSg(Tarea tarea)
        {
            var tareaUsuario = await _context.TareaGrupos
                .Where(tu => tu.IdTarea == tarea.Id)
                
                .ToListAsync();
            if (!tareaUsuario.Any())
            {
                var tareaUsuarios = await _context.TareaUsuarios.Where(p => p.IdTarea == tarea.Id).ToListAsync();


                if (tareaUsuarios != null)
                {
                    // Obtener la lista de usuarios que pertenecen al proyecto
                    //List<Usuario> usuarios = _context.Usuarios.Where(u => u.Id == proyecto.IdUsuario).ToList();
                    //List<Usuario> usuarios = _context.Usuarios.Where(u => u.Id == proyecto.IdUsuario).ToList();
                    // Suscribir a los usuarios como observadores de la tarea modificada
                    var idsUsuarios = tareaUsuarios.Select(tu => tu.IdUsuario).ToList();
                    var usuarios = await _context.Usuarios.Where(u => idsUsuarios.Contains(u.Id)).ToListAsync();

                    foreach (var usuario in usuarios)
                    {
                        tarea.Subscribe(new UsuarioObserver(usuario));
                    }
                }
                tarea.Modificar(tarea);
            }
            else
            {
                var tareaUsuarios = await _context.TareaUsuarios
          .Where(tu => tu.IdTarea == tarea.Id)
          .Select(tu => tu.IdUsuario)
          .ToListAsync();
                var usuariosSinGrupo = await _context.Usuarios
                    .Where(u => !_context.UsuarioGrupos.Any(ug => ug.IdUsuario == u.Id) && tareaUsuarios.Contains(u.Id))
                    .ToListAsync();

                // Realizar la notificación a los usuarios que no pertenecen a ningún grupo pero sí a la tarea
                foreach (var usuario in usuariosSinGrupo)
                {
                    // Realizar la notificación a cada usuario, por ejemplo, enviar un correo, una notificación push, etc.
                    // Puedes implementar la lógica de notificación aquí, por ejemplo:
                    tarea.Subscribe(new UsuarioObserver(usuario));
                }

                // Notificar a los observadores sobre el cambio en la tarea
                tarea.Modificar(tarea);
            }
            // Notificar a los observadores sobre el cambio en la tarea
            //tarea.Modificar(tarea);

          
        }


    }
}


