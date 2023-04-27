using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tareasv2
{
    [Table("Usuario_rol")]
    public partial class UsuarioRol
    {
        [Key]
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }

        [ForeignKey("IdRol")]
        [InverseProperty("UsuarioRols")]
        public virtual Rol IdRolNavigation { get; set; } = null!;
        [ForeignKey("IdUsuario")]
        [InverseProperty("UsuarioRols")]
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}
