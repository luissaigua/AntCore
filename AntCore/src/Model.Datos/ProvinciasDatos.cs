using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Modelo.Entity;
using Npgsql;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Model.Datos
{
    public class ProvinciasDatos
    {
        private ConexionDB objConexinDB;
        private NpgsqlCommand comando;
        public ProvinciasDatos()
        {
            objConexinDB = ConexionDB.saberEstado();

        }

        
    }
}
