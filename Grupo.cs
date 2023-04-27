using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tareasv2
{
    [Table("Grupo")]
    public partial class Grupo
    {
        public Grupo()
        {
            TareaGrupos = new HashSet<TareaGrupo>();
            UsuarioGrupos = new HashSet<UsuarioGrupo>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string Nombre { get; set; } = null!;

        [InverseProperty("IdGrupoNavigation")]
        public virtual ICollection<TareaGrupo> TareaGrupos { get; set; }
        [InverseProperty("IdGrupoNavigation")]
        public virtual ICollection<UsuarioGrupo> UsuarioGrupos { get; set; }
    }
}
