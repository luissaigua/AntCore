using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Modelo.Negocios;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AntCore.Controllers.Consultas
{
    public class ConsultasController : Controller
    {
        SiniestroNegocio objSiniestrosNeg;

        public ConsultasController()
        {
            objSiniestrosNeg = new SiniestroNegocio();

        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.mensajeValidacion = "";
            ViewBag.mensajeError = "";
            ViewBag.VistaBusquedaVictimas = objSiniestrosNeg.VistaBusquedaVictimas("");
            return View();
        }
        
        public ActionResult FindVistaVictimas(string id)
        {
            var resultado = objSiniestrosNeg.VistaBusquedaVictimas(id);
            ViewBag.VistaBusquedaVictimas = resultado;
            if (resultado.Count() == 0 || resultado == null)
            {
                ViewBag.mensajeError = "No existe resultados en la búsqueda.";
            }
            return View ("Index");
        }


        [HttpPost]
        public IActionResult GetHola()
        {
            HttpContext.Session.Clear();
            //var name = HttpContext.Session.GetString(SessionKeyUsuario);
            return RedirectToAction("Index", "Login");
        }

    }
}
