using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tareasv2
{
    [Table("Usuario")]
    [Index("Usuario1", Name = "usuario_usuario_unique", IsUnique = true)]
    public partial class Usuario
    {
        public Usuario()
        {
            Notificacions = new HashSet<Notificacion>();
            Proyectos = new HashSet<Proyecto>();
            TareaUsuarios = new HashSet<TareaUsuario>();
            UsuarioGrupos = new HashSet<UsuarioGrupo>();
            UsuarioRols = new HashSet<UsuarioRol>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string Nombres { get; set; } = null!;
        [StringLength(255)]
        [Unicode(false)]
        public string Apellidos { get; set; } = null!;
        [Column("Usuario")]
        [StringLength(255)]
        [Unicode(false)]
        public string Usuario1 { get; set; } = null!;
        [Column("correo")]
        [StringLength(255)]
        [Unicode(false)]
        public string Correo { get; set; } = null!;
        [StringLength(255)]
        [Unicode(false)]
        public string Clave { get; set; } = null!;

        [InverseProperty("Usuario")]
        public virtual ICollection<Notificacion> Notificacions { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Proyecto> Proyectos { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<TareaUsuario> TareaUsuarios { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<UsuarioGrupo> UsuarioGrupos { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<UsuarioRol> UsuarioRols { get; set; }
    }
}
