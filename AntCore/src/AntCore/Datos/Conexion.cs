using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using Npgsql;
using AntCore.Models.Departamentos;
using AntCore.Datos.Usuarios;
using AntCore.Models.Funciones;
using AntCore.Models.Users;

namespace AntCore.Datos
{
    public class Conexion : IDepartamentos<DepartamentoViewModel>, IFunciones<FuncionesViewModel>,
        IAutoridades<AutoridadesViewModel>, IUsuarios<UsuariosViewModel>
    {
        private string connectionString;
        public Conexion(IConfiguration configuration)
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

        


        #region Departamentos


        public void AddDepartamentos(DepartamentoViewModel item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO customer (name,phone,email,address) VALUES(@Name,@Phone,@Email,@Address)", item);
            }

        }

        public IEnumerable<DepartamentoViewModel> ListaDetaptamentos()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<DepartamentoViewModel>("SELECT * FROM customer where eliminado!=true");
            }
        }

        public DepartamentoViewModel IdDepartamento(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<DepartamentoViewModel>("SELECT * FROM customer WHERE id = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public void EliminarDepartamento(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("update customer set eliminado=true WHERE Id=@Id", new { Id = id });
            }
        }

        public void UpdateDepartamento(DepartamentoViewModel item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE customer SET name = @Name,  phone  = @Phone, email= @Email, address= @Address WHERE id = @Id", item);
            }
        }

        #endregion

        #region Funciones


        public void AddFuncion(FuncionesViewModel item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO customer (name,phone,email,address) VALUES(@Name,@Phone,@Email,@Address)", item);
            }

        }

        public IEnumerable<FuncionesViewModel> ListaFuncion()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<FuncionesViewModel>("SELECT * FROM customer where eliminado!=true");
            }
        }

        public FuncionesViewModel IdFuncion(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<FuncionesViewModel>("SELECT * FROM customer WHERE id = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public void EliminarFuncion(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("update customer set eliminado=true WHERE Id=@Id", new { Id = id });
            }
        }

        public void UpdateFuncion(FuncionesViewModel item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE customer SET name = @Name,  phone  = @Phone, email= @Email, address= @Address WHERE id = @Id", item);
            }
        }

        #endregion

        #region Autoridades


        public void AddAutoridad(AutoridadesViewModel item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO customer (name,phone,email,address) VALUES(@Name,@Phone,@Email,@Address)", item);
            }

        }

        public IEnumerable<AutoridadesViewModel> ListaAutoridades()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<AutoridadesViewModel>("SELECT * FROM customer where eliminado!=true");
            }
        }

        public AutoridadesViewModel IdAutoridad(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<AutoridadesViewModel>("SELECT * FROM customer WHERE id = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public void EliminarAutoridad(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("update customer set eliminado=true WHERE Id=@Id", new { Id = id });
            }
        }

        public void UpdateAutoridad(AutoridadesViewModel item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE customer SET name = @Name,  phone  = @Phone, email= @Email, address= @Address WHERE id = @Id", item);
            }
        }

        #endregion

        #region Usuarios


        public void AddUsuario(UsuariosViewModel item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO customer (name,phone,email,address) VALUES(@Name,@Phone,@Email,@Address)", item);
            }

        }

        public IEnumerable<UsuariosViewModel> ListaUsuarios()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<UsuariosViewModel>("SELECT * FROM customer where eliminado!=true");
            }
        }

        public UsuariosViewModel IdUsuario(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<UsuariosViewModel>("SELECT * FROM customer WHERE id = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public void EliminarUsuario(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("update customer set eliminado=true WHERE Id=@Id", new { Id = id });
            }
        }

        public void UpdateUsuario(UsuariosViewModel item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE customer SET name = @Name,  phone  = @Phone, email= @Email, address= @Address WHERE id = @Id", item);
            }
        }

        #endregion
    }
}
