using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tareasv2
{
    [Table("Estado")]
    public partial class Estado
    {
        public Estado()
        {
            EstadoTareas = new HashSet<EstadoTarea>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string Nombre { get; set; } = null!;

        [InverseProperty("IdEstadoNavigation")]
        public virtual ICollection<EstadoTarea> EstadoTareas { get; set; }
    }
}
