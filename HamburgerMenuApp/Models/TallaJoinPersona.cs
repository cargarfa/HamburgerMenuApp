using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerMenuApp.Models
{
   public class TallaJoinPersona : bx_Talla
    {
        /*
         * hereda todos los campos de la tabla Talla y que se le agreguen los de Persona para cuando hagamos INNER JOIN entre las dos tablas.
         */
        public String Nif { get; set; } 
        public String Nombre { get; set; }
        public String Apellido1 { get; set; }
        public String Apellido2 { get; set; }
    }
}
