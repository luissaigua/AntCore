using AntCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AntCore.Datos.Usuarios
{
    interface IAutoridades<T> where T : BaseEntity
    {
        void AddAutoridad(T item);
        void EliminarAutoridad(int id);
        void UpdateAutoridad(T item);
        T IdAutoridad(int id);
        IEnumerable<T> ListaAutoridades();
    }
}
