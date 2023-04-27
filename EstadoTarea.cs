using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tareasv2
{
    [Table("Estado_Tarea")]
    public partial class EstadoTarea
    {
        [Key]
        public int Id { get; set; }
        public int IdTarea { get; set; }
        public int IdEstado { get; set; }

        [ForeignKey("IdEstado")]
        [InverseProperty("EstadoTareas")]
        public virtual Estado IdEstadoNavigation { get; set; } = null!;
        [ForeignKey("IdTarea")]
        [InverseProperty("EstadoTareas")]
        public virtual Tarea IdTareaNavigation { get; set; } = null!;
    }
}
