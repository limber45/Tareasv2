using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tareasv2
{
    [Table("Tarea_Usuario")]
    public partial class TareaUsuario
    {
        [Key]
        public int Id { get; set; }
        public int IdTarea { get; set; }
        public int IdUsuario { get; set; }

        [ForeignKey("IdTarea")]
        [InverseProperty("TareaUsuarios")]
        public virtual Tarea IdTareaNavigation { get; set; } = null!;
        [ForeignKey("IdUsuario")]
        [InverseProperty("TareaUsuarios")]
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}
