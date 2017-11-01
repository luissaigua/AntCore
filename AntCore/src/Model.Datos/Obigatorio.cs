using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Modelo.Entity;
namespace Model.Datos
{
    public interface Obigatorio<T> where T : BaseEntity
    {
        //void create(cualquierclase obj);
        //void delete(cualquierclase obj);
        //void update(cualquierclase obj);
        //bool find(cualquierclase obj);
        //List<cualquierclase> findAll();

        void Add(T item);
        void Remove(int id);
        void Update(T item);
        T FindByID(int id);
        IEnumerable<T> FindAll();
    }
}
