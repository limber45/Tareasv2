using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tareasv2
{
    [Table("Notificacion")]
    public partial class Notificacion
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string Contenido { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime Fecha { get; set; }
        public int UsuarioId { get; set; }
        public int ProyectoId { get; set; }
        public int Leida { get; set; }

        [ForeignKey("ProyectoId")]
        [InverseProperty("Notificacions")]
        public virtual Proyecto Proyecto { get; set; } = null!;
        [ForeignKey("UsuarioId")]
        [InverseProperty("Notificacions")]
        public virtual Usuario Usuario { get; set; } = null!;
    }
}
