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
    public class GeoDatos
    {


        private string connectionString;
        public GeoDatos(IConfiguration configuration)
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



        public int Add(Georeferencia  item)
        {
         
               
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                //try
                //{
                string r = "INSERT INTO georeferencias (latgeo,longeo,fecgeo,fotprigeo,fotsegeo,obsgeo,valgeo,codaut,\"PUCOD\") VALUES(@LATGEO,@LONGEO,@FECGEO,@FOTPRIGEO,@FOTSEGEO,@OBSGEO,@VALGEO,@CODAUT,@PUCOD)" + item;
                    dbConnection.Execute("INSERT INTO georeferencias (latgeo,longeo,fecgeo,fotprigeo,fotsegeo,obsgeo,valgeo,codaut,\"PUCOD\") VALUES(@LATGEO,@LONGEO,@FECGEO,@FOTPRIGEO,@FOTSEGEO,@OBSGEO,@VALGEO,@CODAUT,@PUCOD)", item);
                   // return true;

                    var Id = dbConnection.Query<Georeferencia>("SELECT max(codgeo) as codgeo FROM georeferencias").FirstOrDefault();
                    return Id.CODGEO;
                //}catch(Exception e)
                //{
                //    return false;
                //}

            }

        }
    }
}
