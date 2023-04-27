using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tareasv2
{
    [Table("Prioridad")]
    public partial class Prioridad
    {
        public Prioridad()
        {
            Tareas = new HashSet<Tarea>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string Nombre { get; set; } = null!;
        [StringLength(200)]
        [Unicode(false)]
        public string Descripcion { get; set; } = null!;
        [StringLength(200)]
        [Unicode(false)]
        public string Color { get; set; } = null!;
        public int Orden { get; set; }

        [InverseProperty("PrioridadNavigation")]
        public virtual ICollection<Tarea> Tareas { get; set; }
    }
}
