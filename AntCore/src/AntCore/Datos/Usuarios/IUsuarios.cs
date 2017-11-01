using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntCore.Models;
namespace AntCore.Datos.Usuarios
{
    public interface IUsuarios<T> where T:BaseEntity
    {
        void AddUsuario(T item);
        void EliminarUsuario(int id);
        void UpdateUsuario(T item);
        T IdUsuario(int id);
        IEnumerable<T> ListaUsuarios();
    }
}
