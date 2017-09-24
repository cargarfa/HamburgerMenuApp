using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerMenuApp.Models
{
   public class b_Persona
    {
        [PrimaryKey, AutoIncrement]
        public int PersonaId { get; set; }

        [Unique, MaxLength(50)]
        public String Nif { get; set; }

        [ MaxLength(50)]
        public String Nombre { get; set; }

        [MaxLength(50)]
        public String Apellido1 { get; set; }

        [MaxLength(50)]
        public String Apellido2 { get; set; }

    }
}
