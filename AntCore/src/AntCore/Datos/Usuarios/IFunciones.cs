using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntCore.Models;
namespace AntCore.Datos.Usuarios
{
    public interface IFunciones<T> where T : BaseEntity
    {

        void AddFuncion(T item);
        void EliminarFuncion(int id);
        void UpdateFuncion(T item);
        T IdFuncion(int id);
        IEnumerable<T> ListaFuncion();
    }
}
