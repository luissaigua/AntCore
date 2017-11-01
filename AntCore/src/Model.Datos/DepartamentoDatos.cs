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
    public class DepartamentoDatos:Obigatorio<Departamento>
    {
        private string connectionString;
        public DepartamentoDatos(IConfiguration configuration)
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

        public void Add(Departamento item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO \"PDAC\" (\"PD000\",\"PD001\") VALUES(@PD000,@PD001)", item);
            }

        }


        public IEnumerable<Departamento> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Departamento>("SELECT * FROM \"PDAC\" Where \"PD001\" = 1");
                
            }

            
        }

        public List<Departamento> ListaDepartamentos()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Departamento>("SELECT * FROM \"PDAC\" Where \"PD001\" = 1").ToList();

            }


        }

        public Departamento FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Departamento>("SELECT * FROM \"PDAC\" WHERE \"PD001\" = 1 and \"PDCOD\" = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                //dbConnection.Execute("DELETE FROM \"PDAC\" WHERE Id=@Id", new { Id = id });
                dbConnection.Execute("UPDATE \"PDAC\" SET \"PD001\"  = 0 WHERE \"PDCOD\" = @Id", new { Id = id });
            }
        }

        public void Update(Departamento item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                
                dbConnection.Query("UPDATE \"PDAC\" SET \"PD000\"= @PD000,  \"PD001\" = @PD001 WHERE \"PDCOD\" = @PDCOD", item);
            }
        }
    }
}
