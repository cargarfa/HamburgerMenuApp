using HamburgerMenuApp.Models;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerMenuApp.Services.PersonaService
{
  public class b_PersonaService : IPersonaService
    {
        String path = App.DbConnectionString;

        public b_PersonaService()
        {

        }
        public void DeletePersona(b_Persona persona)
        {
            using (var conn = new SQLiteConnection(new SQLitePlatformWinRT(), path))
            {
                conn.RunInTransaction(() =>
                {
                    conn.Delete(persona);
                });
            }
        }

        public b_Persona GetPersona(int Id)
        {
            b_Persona result;
            using (var conn = new SQLiteConnection(new SQLitePlatformWinRT(), path))
            {
                result = conn.Get<b_Persona>(Id);
            }
            return result;
        }

        public List<b_Persona> GetPersona()
        {
            List<b_Persona> result;
            using (var conn = new SQLiteConnection(new SQLitePlatformWinRT(), path))
            {
                result = conn.Table<b_Persona>().ToList();
                //result = conn.Query<b_Persona>("SELECT * FROM b_Persona");
            }
            return result;
        
        }
       

        public void InsertOrUpdatePersona(b_Persona persona)
        {
            using (var conn = new SQLiteConnection(new SQLitePlatformWinRT(), path))
            {
                if (persona.PersonaId.Equals(0))
                {
                    conn.RunInTransaction(() =>
                    {
                        conn.Insert(persona);
                    });
                }
                else
                {
                    conn.RunInTransaction(() =>
                    {
                        conn.Update(persona);
                    });
                }
            }
        }

       }
}
