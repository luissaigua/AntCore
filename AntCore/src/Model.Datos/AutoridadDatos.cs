using Modelo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Npgsql;
using Dapper;
using System.Data;

using System.Collections;

namespace Model.Datos
{
    public class AutoridadDatos:Obigatorio<Autoridad>
    {

        private string connectionString;
        public AutoridadDatos(IConfiguration configuration)
        {
            connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
        }
        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }

        public void Add(Autoridad item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO autoridades (codaut,desaut,emaaut,caraut,estaut) VALUES(@CODAUT,@DESAUT,@EMAAUT,@CARAUT,@ESTAUT)", item);
            }

        }


        public IEnumerable<Autoridad> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Autoridad>("SELECT * FROM autoridades Where estaut = 1");

            }


        }
        public IEnumerable<Autoridad> FindAllAutoriad()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Autoridad>(" SELECT CODAUT,DESAUT FROM autoridades Where estaut = 1 UNION select '-1' ,'SELECCIONAR' ORDER BY 1");

            }


        }

        public Autoridad FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Autoridad>("SELECT * FROM  autoridades WHERE estaut = 1 and codaut = @Id", new { Id = id }).FirstOrDefault();
            }
        }


        public Autoridad FindByCOD(string id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Autoridad>("SELECT * FROM  autoridades WHERE estaut = 1 and codaut = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                //dbConnection.Execute("DELETE FROM \"PDAC\" WHERE Id=@Id", new { Id = id });
                dbConnection.Execute("UPDATE autoridades SET estaut = 0 WHERE codaut = @Id", new { Id = id });
            }
        }

        public void Update(Autoridad item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();

                dbConnection.Query("UPDATE autoridades SET desaut=@DESAUT,emaaut=@EMAAUT,caraut=@CARAUT WHERE CODAUT = '"+item.CODAUT+"'", item);
            }
        }
    }
}
