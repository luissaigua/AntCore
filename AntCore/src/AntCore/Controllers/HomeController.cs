using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model.Datos;
using Modelo.Negocios;
using Microsoft.AspNetCore.Http;
using Modelo.Entity;
using Microsoft.Extensions.Configuration;


namespace AntCore.Controllers
{
    public class HomeController : Controller
    {
        SiniestroNegocio objSiniestrosNeg;
        const string SessionKeyUsuario = "_Usuario";
        const string SessionKeyTipo = "_Tipo";
        const string SessionKeyCodUsuario = "_CodUsuario";
        const string SessionNombreUsuario = "_NombreUsuario";
        const string SessionKeyPermisoGeneral = "_PermisoGeneral";
        const string SessionKeyPermisoAgente = "_PermisoAgente"; // GESTOR
        const string SessionKeyPermisoValidador = "_PermisoValidador";//VALIDADOR
        const string SessionKeyPermisoGestVali = "_PermisoGestorValidador";//"GESTOR DE DATOS -  VALIDADOR"
        const string SessionKeyPermisoSupANT = "_PermisoSupervisor";//"SUPERVISOR ANT" 
        const string SessionKeyPermisoCargaCal = "_PermisoCargaCal";//"carga calificacion ANT" 

        public HomeController()
        {
            objSiniestrosNeg = new SiniestroNegocio();
            
        }
        public IActionResult Index()
        {

            Modelo.Entity.Usuarios us = new Modelo.Entity.Usuarios();
            var name = HttpContext.Session.GetString(SessionKeyUsuario);
            var tipo = HttpContext.Session.GetString(SessionKeyTipo);
            var codUsuario = HttpContext.Session.GetString(SessionKeyCodUsuario);
            var nombreUsuario = HttpContext.Session.GetString(SessionNombreUsuario);
            string valor = "";
            valor = objSiniestrosNeg.verificaPermiso(Convert.ToInt32(codUsuario), 1);
            ViewBag.valorPermisoGeneral = valor;
            us.permisoGeneral = valor;
            HttpContext.Session.SetString("PermisoGeneral", valor);
            valor = objSiniestrosNeg.verificaPermiso(Convert.ToInt32(codUsuario), 2);
            ViewBag.valorPermisoAgente = valor;
            us.permisoAgente = valor;
            HttpContext.Session.SetString("PermisoAgente", valor);//agente

            valor = objSiniestrosNeg.verificaPermiso(Convert.ToInt32(codUsuario), 3);//validador
            us.permisoValidador = valor;
            HttpContext.Session.SetString("PermisoValidador", valor);//validador

            valor = objSiniestrosNeg.verificaPermiso(Convert.ToInt32(codUsuario), 4);//gest - validador
            us.permisoValidador = valor;
            HttpContext.Session.SetString("PermisoGestorValidador", valor);//

            valor = objSiniestrosNeg.verificaPermiso(Convert.ToInt32(codUsuario), 6);//sup ant
            us.permisoValidador = valor;
            HttpContext.Session.SetString("PermisoSupervisor", valor);//

            valor = objSiniestrosNeg.verificaPermiso(Convert.ToInt32(codUsuario), 7);//carga cal
            us.permisoValidador = valor;
            HttpContext.Session.SetString("PermisoCargaCal", valor);//caga
            //
            HttpContext.Session.SetString("nombreloginUsuario", nombreUsuario);


            var codautoridad = HttpContext.Session.GetString("codAutoridad");
            int codusuarioingreso = Convert.ToInt32( HttpContext.Session.GetString("codusuarioingreso"));


            ViewBag.listaVistaSiniestrosPorAutoridad = ListaVistaSiniestrosPorEnte(codautoridad, codusuarioingreso);

            if (name != null && tipo != null)
            {

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }


        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
        [HttpPost]
        public IActionResult GetHola()
        {
            HttpContext.Session.Clear();
            //var name = HttpContext.Session.GetString(SessionKeyUsuario);
            return RedirectToAction("Index", "Login");
        }
        public IActionResult PaginaPrincipal()
        {
            
            //var name = HttpContext.Session.GetString(SessionKeyUsuario);
            return RedirectToAction("Index", "Home");
        }

        public JsonResult cerrarSession()
        {
            string result = "";
            HttpContext.Session.Clear();
            var codUsuario = HttpContext.Session.GetString(SessionKeyCodUsuario);
            if (codUsuario != "" && codUsuario != null)
                result = "1";
            else
                result = "0";

            return Json(result);
        }

        public List<Modelo.Entity.Siniestro> ListaVistaSiniestrosPorEnte(string codAutoridad, int cod_usuario)
        {
            return objSiniestrosNeg.VistaSiniestrosPorAutoridad( codAutoridad,  cod_usuario);
        }

    }
}
