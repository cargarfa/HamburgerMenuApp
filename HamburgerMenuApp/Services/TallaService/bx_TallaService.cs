using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamburgerMenuApp.Models;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;

namespace HamburgerMenuApp.Services.TallaService
{
   public class bx_TallaService : ITallaService
    {
        String path = App.DbConnectionString;

  

        public void DeleteTalla(bx_Talla talla)
        {
            using (var conn = new SQLiteConnection(new SQLitePlatformWinRT(), path))
            {
                conn.RunInTransaction(() =>
                {
                    var t = conn.Execute("DELETE FROM bx_Talla WHERE TallaId = ?", talla.TallaId);
                });
            }
        }


        public bx_Talla GetTalla(int Id)
        {
            bx_Talla result;
            using (var conn = new SQLiteConnection(new SQLitePlatformWinRT(), path))
            {
                result = conn.Query<bx_Talla>(String.Format("SELECT * FROM bx_Talla Where TallaId = '{0}'", Id)).FirstOrDefault();
            }
            return result;
        }
        

        public List<bx_Talla> GetTallaPorPersonaId(int personaId)
        {
            List<bx_Talla> result;
            using (var conn = new SQLiteConnection(new SQLitePlatformWinRT(), path))
            {
                result = conn.Query<bx_Talla>("SELECT * FROM bx_Talla where TallaPersonaId=personaId");
            }
            return result;
        }

        public List<bx_Talla> GetTalla()
        {
            List<bx_Talla> result;
            using (var conn = new SQLiteConnection(new SQLitePlatformWinRT(), path))
            {
                result = conn.Query<bx_Talla>("SELECT * FROM bx_Talla");
            }
            return result;
        }
        ////INNER JOIN : Una combinación que muestra sólo las filas que tienen una coincidencia en ambas tablas se unieron. Valido para relaciones varios a varios, ejemplo
        //en favoritos, aqui no vale.
        public List<TallaJoinPersona> GetTallaJoinPersona()
        {
            List<TallaJoinPersona> result;
            using (var conn = new SQLiteConnection(new SQLitePlatformWinRT(), path))
            {
                //        result=conn.Query<TallaJoinPersona>("SELECT * FROM bx_Talla p INNER JOIN b_Persona c ON p.TallaPersonaId= c.PersonaId ");
               result = conn.Query<TallaJoinPersona>("SELECT * FROM bx_Talla p INNER JOIN b_Persona c ON p.TallaPersonaId= c.PersonaId ");
            }
            return result;
        }
        //public List<TallaJoinPersona> GetTallaJoinPersona(int idPersona)
        //{
        //    List<TallaJoinPersona> result;
        //    using (var conn = new SQLiteConnection(new SQLitePlatformWinRT(), path))
        //    {
        //        result = conn.Query<TallaJoinPersona>("SELECT * FROM bx_Talla where TallaId == idPersona");
        //        //result = conn.Query<TallaJoinPersona>("SELECT * FROM bx_Talla p INNER JOIN b_Persona c ON p.TallaPersonaId= c.PersonaId ");
        //    }
        //    return result;
        //}


        public void InsertOrUpdateTalla(bx_Talla talla)
        {
            using (var conn = new SQLiteConnection(new SQLitePlatformWinRT(), path))
            {
                if (talla.TallaId.Equals(0))
                {
                    conn.RunInTransaction(() =>
                    {
                        conn.Execute("INSERT INTO bx_Talla (NombreTalla, Talla, TallaPersonaId) VALUES(?, ?)", talla.NombreTalla, talla.Talla, talla.TallaPersonaId);
                    });
                }
                /*
                 * [PrimaryKey, AutoIncrement]
        public int TallaId { get; set; }

        [MaxLength(50)]
        public int NombreTalla { get; set; }

        [MaxLength(4)]
        public int Talla { get; set; }

        [ForeignKey("b_PersonaBD")]
        public int TallaPersonaId { get; set; }
 
                 * 
                 */
                else
                {
                    conn.RunInTransaction(() =>
                    {
                        conn.Execute("UPDATE bx_Talla SET NombreTalla = ?, TallaPersonaId = ? WHERE TallaId = ?", talla.TallaId, talla.NombreTalla, talla.Talla, talla.TallaPersonaId);
                    });
                }
            }
        }
    }
}
