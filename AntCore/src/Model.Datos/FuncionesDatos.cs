using Modelo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Npgsql;
using Dapper;
using System.Data;


namespace Model.Datos
{
    public class FuncionesDatos : Obigatorio<Funciones>
    {
        private string connectionString;
        public FuncionesDatos(IConfiguration configuration)
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

        public void Add(Funciones item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO \"PFAC\" (\"PF000\",\"PF001\") VALUES(@PF000,@PF001)", item);
            }

        }


        public IEnumerable<Funciones> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();

                return dbConnection.Query<Funciones>("SELECT * FROM \"PFAC\" Where \"PF001\" = 1");

            }
        }

        public Funciones FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Funciones>("SELECT * FROM \"PFAC\" WHERE \"PF001\" = 1 and \"PFCOD\" = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                // dbConnection.Execute("DELETE FROM customer WHERE Id=@Id", new { Id = id });
                dbConnection.Execute("UPDATE \"PFAC\" SET \"PF001\"  = 0 WHERE \"PFCOD\" = @Id", new { Id = id });
            }
        }

        public void Update(Funciones item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE \"PFAC\" SET \"PF000\"= @PF000,  \"PF001\" = @PF001 WHERE \"PFCOD\" = @PFCOD", item);
            }
        }
    }
}
