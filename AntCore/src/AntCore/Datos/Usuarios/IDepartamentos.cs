using AntCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AntCore.Datos.Usuarios
{
    public interface IDepartamentos<T> where T : BaseEntity
    {
        void AddDepartamentos(T item);
        void EliminarDepartamento(int id);
        void UpdateDepartamento(T item);
        T IdDepartamento(int id);
        IEnumerable<T> ListaDetaptamentos();
    }
}
