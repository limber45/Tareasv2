

namespace Tareasv2.Observer
{
    public class UsuarioObserver : IObserver

    {
        private readonly Usuario _usuario;

        public UsuarioObserver(Usuario usuario)
        {
            _usuario = usuario;
        }
        public void Create(Tarea tarea)
        {

            // Aquí puedes implementar la lógica para notificar al usuario sobre el cambio en la tarea
            // Puedes acceder a los datos de la tarea a través del parámetro tarea
            // Por ejemplo, puedes guardar una notificación en la base de datos para el usuario y la tarea específica
            Notificacion notificacion = new Notificacion
            {

                UsuarioId = _usuario.Id,
                ProyectoId = tarea.IdProyecto,
                Contenido = "Se ha creado la " + tarea.Descripcion ,
                Fecha = DateTime.UtcNow,
                Leida = 0
            };

            // Aquí puedes guardar la notificación en la base de datos utilizando el contexto de base de datos correspondiente
            // Por ejemplo, si estás utilizando Entity Framework Core, puedes usar el contexto TareasDbContext para guardar la notificación
            using (var context = new TareasDBv3Context())
            {
                context.Notificacions.Add(notificacion);
                context.SaveChanges();
            }
        }
        public void Update(Tarea tarea)
        {

            // Aquí puedes implementar la lógica para notificar al usuario sobre el cambio en la tarea
            // Puedes acceder a los datos de la tarea a través del parámetro tarea
            // Por ejemplo, puedes guardar una notificación en la base de datos para el usuario y la tarea específica
            Notificacion notificacion = new Notificacion
            {

                UsuarioId = _usuario.Id,
                ProyectoId = tarea.IdProyecto,
                Contenido = "La tarea " + tarea.Descripcion + " ha sido modificada.",
                Fecha = DateTime.UtcNow,
                Leida = 0
            };

            // Aquí puedes guardar la notificación en la base de datos utilizando el contexto de base de datos correspondiente
            // Por ejemplo, si estás utilizando Entity Framework Core, puedes usar el contexto TareasDbContext para guardar la notificación
            using (var context = new TareasDBv3Context())
            {
                context.Add(notificacion);
               
                context.SaveChanges();
            }
        }
    }
}
