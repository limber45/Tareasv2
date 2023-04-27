using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tareasv2
{
    [Table("Proyecto")]
    public partial class Proyecto
    {
        public Proyecto()
        {
            Notificacions = new HashSet<Notificacion>();
            Tareas = new HashSet<Tarea>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string NombreProyecto { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime FechaC { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime FechaI { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime FechaF { get; set; }
        public int IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        [InverseProperty("Proyectos")]
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
        [InverseProperty("Proyecto")]
        public virtual ICollection<Notificacion> Notificacions { get; set; }
        [InverseProperty("IdProyectoNavigation")]
        public virtual ICollection<Tarea> Tareas { get; set; }
    }
}
