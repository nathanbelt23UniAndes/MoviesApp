using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dominio
{
    public class EstadoAlquiler
    {
 


        [Key]
        public int EstadoAquilerID { get; set; }

        public string Nombre { get; set; }

        public ICollection<PeliculaAlquiler> PeliculaAlquiler { get; set; }

    }
}
