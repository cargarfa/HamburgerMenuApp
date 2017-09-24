using HamburgerMenuApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerMenuApp.Services.PersonaService
{
   public interface IPersonaService
    {
        /*
         * Para realizar las operaciones básicas con las tablas lo mejor va a ser crear servicios que separen esa lógica.
         * Creo los servicio ICustomerService y IProjectService. 
         * Las operaciones son mas que sencillas e intuitivas.
         */

        List<b_Persona> GetPersona();
        b_Persona GetPersona(int Id);
        void InsertOrUpdatePersona(b_Persona persona);
        void DeletePersona(b_Persona persona);

    }
}
