using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tareasv2
{
    [Table("Usuario_Grupo")]
    public partial class UsuarioGrupo
    {
        [Key]
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdGrupo { get; set; }

        [ForeignKey("IdGrupo")]
        [InverseProperty("UsuarioGrupos")]
        public virtual Grupo IdGrupoNavigation { get; set; } = null!;
        [ForeignKey("IdUsuario")]
        [InverseProperty("UsuarioGrupos")]
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}
