using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerMenuApp.Models
{
    public class bx_Talla
    {
        [PrimaryKey, AutoIncrement]
        public int TallaId { get; set; }

        [MaxLength(50)]
        public string NombreTalla { get; set; }

        [MaxLength(4)]
        public int Talla { get; set; }

        [ForeignKey("b_PersonaBD")]
        public int TallaPersonaId { get; set; }

    }
}
