using HamburgerMenuApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerMenuApp.Services.TallaService
{
  public interface ITallaService
    {
        List<bx_Talla> GetTalla();
        List<TallaJoinPersona> GetTallaJoinPersona();
        List<bx_Talla> GetTallaPorPersonaId(int personaId);
       // List<TallaJoinPersona> GetTallaJoinPersona(int IdPersona);
        bx_Talla GetTalla(int Id);
        void InsertOrUpdateTalla(bx_Talla talla);
        void DeleteTalla(bx_Talla talla);

    }
}
