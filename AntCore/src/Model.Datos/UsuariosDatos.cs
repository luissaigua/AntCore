using Modelo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Npgsql;
using Dapper;
using System.Data;
using System.Security.Cryptography;

namespace Model.Datos
{
    public class UsuariosDatos : Obigatorio<Usuarios>
    {
        private string connectionString;
        private Encriptacion enc = new Encriptacion();
        public UsuariosDatos(IConfiguration configuration)
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

        public void Add(Usuarios item)
        {



            MD5 md5Hash = MD5.Create();

            string Password = enc.GetMd5Hash(md5Hash, item.PU001);
            item.PU001 = Password;
            item.PU005 = 1;
            item.PU004 = "ND";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();

                var datos = dbConnection.Query<Usuarios>("SELECT * FROM \"PUAC\" WHERE \"PU000\" = @PU000", item).Any();
                if (datos)
                {

                }
                else
                {
                    dbConnection.Execute("INSERT INTO \"PUAC\" (\"PU000\",\"PU001\",\"PU002\",\"PU003\",\"PU004\",\"PU005\",\"PU006\",\"PDCOD\",\"PFCOD\",codaut) VALUES(@PU000,@PU001,@PU002,@PU003,@PU004,@PU005,@PU006,@PDCOD,@PFCOD,@CODAUT)", item);
                }

            }

        }


        public IEnumerable<Usuarios> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Usuarios>("select a.desaut AS autoridad,d.\"PD000\" AS departamento,f.\"PF000\" as FUNCION, u.* from \"PUAC\" u  INNER JOIN autoridades a on a.codaut = u.codaut inner join  \"PDAC\" d on d.\"PDCOD\"  = u.\"PDCOD\"  INNER JOIN \"PFAC\" f on f.\"PFCOD\" = u.\"PFCOD\"  WHERE \"PU005\"=1 ");
                //  return dbConnection.Query<Usuarios>("SELECT * FROM \"PUAC\" WHERE \"PU005\"=1");
            }
        }



        public Usuarios FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Usuarios>("SELECT * FROM \"PUAC\" WHERE \"PUCOD\" = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("UPDATE \"PUAC\" SET   \"PU005\" = 0 WHERE \"PUCOD\"=@Id", new { Id = id });
            }
        }

        public void Update(Usuarios item)
        {

            MD5 md5Hash = MD5.Create();

            string Password = enc.GetMd5Hash(md5Hash, item.PU001);
            item.PU001 = Password;
            item.PU005 = 1;
            item.PU004 = "ND";
            
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE \"PUAC\" SET \"PU000\"=@PU000,\"PU001\"=@PU001,\"PU002\"=@PU002,\"PU003\"=@PU003,\"PU004\"=@PU004,\"PU005\"=@PU005,\"PU006\"=@PU006,\"PDCOD\"=@PDCOD,\"PFCOD\"=@PFCOD,codaut=@CODAUT WHERE \"PUCOD\" = @PUCOD", item);
            }
        }



        public IEnumerable<Usuarios> Login(Usuarios item)
        {

            MD5 md5Hash = MD5.Create();
            string Password = enc.GetMd5Hash(md5Hash, item.PU001);
            item.PU001 = Password;


            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Usuarios>("select a.caraut,f.\"PF000\" as funcion, u.* from  \"PUAC\" u inner join  autoridades a on a.codaut = u.codaut inner join public.\"PFAC\" f on f.\"PFCOD\"  = u.\"PFCOD\" WHERE  \"PU000\"= @PU000 and \"PU001\"= @PU001 and \"PU005\"= 1", item);

            }
        }

        public Boolean Validacion(string cedula)
        {
            using (IDbConnection dbConnection = Connection)
            {
                var datos = dbConnection.Query<Usuarios>("SELECT * FROM \"PUAC\" WHERE \"PU000\" = @Id", new { Id = cedula }).Any();
                return datos;
            }
        }

    }
}
