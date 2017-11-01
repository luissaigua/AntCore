using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Modelo.Entity;
using Modelo.Negocios;
using Model.Datos;
using Newtonsoft.Json; // Use for JsonConvert
using Microsoft.AspNetCore.Mvc.Rendering;

using System.Text;

namespace AntCore.Controllers.Siniestro
{
    public class FindSiniestro : Controller
    {
        SiniestroNegocio objSiniestrosNeg;
        public FindSiniestro()
        {
            objSiniestrosNeg = new SiniestroNegocio();
            
        }
        public ActionResult Find(int codsin)
        {
            Vehiculo v = new Vehiculo();
            v.codsin = codsin;
            return View(ListaVehiculos(v.codsin));
        }

        public List<Vehiculo> ListaVehiculos(int codsin)
        {
            return objSiniestrosNeg.ListaVehiculos(codsin);
        }
    }
}
