using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
namespace Model.Datos
{
    public class ConexionDB
    {
        private static ConexionDB objConexionDB = null;
        private NpgsqlConnection con;
        private ConexionDB()
        {
          //  con = new NpgsqlConnection("User ID=sindb_user;Password=SIhb_ant8;Host=192.168.1.115;Port=5432;Database=siniestros;Pooling=true;");
            con = new NpgsqlConnection("User ID=sindb_user;Password=SIhb_ant8;Host=172.17.0.83;Port=5432;Database=siniestros_depu;Pooling=true;");
        }

        public static ConexionDB saberEstado()
        {
            if (objConexionDB == null)
            {
                objConexionDB = new ConexionDB();

            }
            return objConexionDB;
        }


        public NpgsqlConnection getCon()
        {
            return con;
        }

        public void closeDB()
        {
            objConexionDB = null;
           // con.Close();
           // con.Dispose();

        }
    }
}
