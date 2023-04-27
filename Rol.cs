using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tareasv2
{
    [Table("Rol")]
    public partial class Rol
    {
        public Rol()
        {
            UsuarioRols = new HashSet<UsuarioRol>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string Nombre { get; set; } = null!;

        [InverseProperty("IdRolNavigation")]
        public virtual ICollection<UsuarioRol> UsuarioRols { get; set; }
    }
}
