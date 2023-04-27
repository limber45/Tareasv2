using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tareasv2
{
    [Table("Tarea_Grupo")]
    public partial class TareaGrupo
    {
        [Key]
        public int Id { get; set; }
        public int IdGrupo { get; set; }
        public int IdTarea { get; set; }

        [ForeignKey("IdGrupo")]
        [InverseProperty("TareaGrupos")]
        public virtual Grupo IdGrupoNavigation { get; set; } = null!;
        [ForeignKey("IdTarea")]
        [InverseProperty("TareaGrupos")]
        public virtual Tarea IdTareaNavigation { get; set; } = null!;
    }
}
