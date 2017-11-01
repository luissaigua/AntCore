using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Modelo.Entity;
using Model.Datos;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Modelo.Negocios
{
    public class ProvinciasNegocio
    {
        private ProvinciasDatos objProvinciaDao;
        public SelectList Provincias { get; set; }
        public ProvinciasNegocio()
        {
            objProvinciaDao = new ProvinciasDatos();
        }
        //public List<Provincias> findAll()
        //{
        //    return objProvinciaDao.listaProvincias();
        //}
    }
}
