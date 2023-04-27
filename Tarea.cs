using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Tareasv2.Observer;

namespace Tareasv2
{
    [Table("Tarea")]
    public partial class Tarea : IObservable<Tarea>
    {
        public Tarea()
        {
            EstadoTareas = new HashSet<EstadoTarea>();
            TareaGrupos = new HashSet<TareaGrupo>();
            TareaUsuarios = new HashSet<TareaUsuario>();
        }

        [Key]
        public int Id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime FechaC { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime FechaI { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime FechaF { get; set; }
        public int IdProyecto { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string Descripcion { get; set; } = null!;
        [StringLength(255)]
        [Unicode(false)]
        public string Solucion { get; set; } = null!;
        [StringLength(255)]
        [Unicode(false)]
        public string Comentario { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime TiempoIdeal { get; set; }
        public int Prioridad { get; set; }

        [ForeignKey("IdProyecto")]
        [InverseProperty("Tareas")]
        public virtual Proyecto IdProyectoNavigation { get; set; } = null!;
        [ForeignKey("Prioridad")]
        [InverseProperty("Tareas")]
        public virtual Prioridad PrioridadNavigation { get; set; } = null!;
        [InverseProperty("IdTareaNavigation")]
        public virtual ICollection<EstadoTarea> EstadoTareas { get; set; }
        [InverseProperty("IdTareaNavigation")]
        public virtual ICollection<TareaGrupo> TareaGrupos { get; set; }
        [InverseProperty("IdTareaNavigation")]
        public virtual ICollection<TareaUsuario> TareaUsuarios { get; set; }

        //IMPLEMENTACION DEL PATON OBSERVER

        private List<IObserver> _observers = new List<IObserver>();
        public IDisposable Subscribe(IObserver observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }

            return new Unsubscriber(_observers, observer);
        }
        public IDisposable Subscribe(IObserver<Tarea> observer)
        {
            if (!_observers.Contains((IObserver)observer))
            {
                _observers.Add((IObserver)observer);
            }
            return new Unsubscriber(_observers, (IObserver)observer);
        }

        // Clase interna para permitir la desuscripción de observadores
        private class Unsubscriber : IDisposable
        {
            private List<IObserver> _observers;
            private IObserver _observer;

            public Unsubscriber(List<IObserver> observers, IObserver observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observers.Contains(_observer))
                {
                    _observers.Remove(_observer);
                }
            }
        }

        // Método para notificar a los observadores cuando se realice un cambio en la tarea
        private void Notify(Tarea tarea)
        {

            foreach (var observer in _observers)
            {
                observer.Update(tarea);
            }
        }
        public void Modificar(Tarea tarea)
        {
            // Aquí implementa la lógica para modificar la tarea en la base de datos

            // Notifica a los observadores sobre el cambio en la tarea
            Notify(tarea);
        }
        public void Crear(Tarea tarea)
        {
            // Aquí implementa la lógica para modificar la tarea en la base de datos

            // Notifica a los observadores sobre el cambio en la tarea
            foreach (var observer in _observers)
            {
                observer.Create(tarea);
            }
        }
    }
}
