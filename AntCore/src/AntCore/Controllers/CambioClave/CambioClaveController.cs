using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Modelo.Negocios;
using Modelo.Entity;
using Microsoft.AspNetCore.Http;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AntCore.Controllers.Usuarios
{
    public class CambioClaveController : Controller
    {
        // GET: /<controller>/
        SiniestroNegocio objSiniestrosNeg;
        const string SessionKeyCodUsuario = "_CodUsuario";
        public CambioClaveController()
        {
            
            objSiniestrosNeg = new SiniestroNegocio();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CambioClave()
        {
            return View();
        }

        public JsonResult ModificarClave(string clave_nueva)
        {
            Modelo.Entity.Usuarios u = new Modelo.Entity.Usuarios();
            var jsonResult = objSiniestrosNeg.CambioContrasenia(u,0);
            var codUsuario = HttpContext.Session.GetString(SessionKeyCodUsuario);
            u.PDCOD = Convert.ToInt32(codUsuario);
            u.PU001 = clave_nueva.ToString();
            try
            {
                 jsonResult = objSiniestrosNeg.CambioContrasenia(u,Convert.ToInt32(codUsuario));
               
            }
            catch
            {
                jsonResult = "0";
            }
            return Json(jsonResult);

        }

        public IActionResult GetHola()
        {
            HttpContext.Session.Clear();
            //var name = HttpContext.Session.GetString(SessionKeyUsuario);
            return RedirectToAction("Index", "Login");
        }

    }
}
