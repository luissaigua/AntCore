using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Modelo.Entity;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;


using Model.Datos;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AntCore.Controllers.Login
{

    public class LoginController : Controller
    {

        const string SessionKeyUsuario = "_Usuario";
        const string SessionKeyTipo = "_Tipo";
        const string SessionKeyCodUsuario = "_CodUsuario";
        const string SessionKeyNombreUsuario = "_NombreUsuario";
        const string SessionKeyCodFuncion = "_CodFuncion";
        const string SessionKeyPermisoGeneral = "_PermisoGeneral";
        const string SessionKeyPermisoAgente = "_PermisoAgente";
        const string SessionKeyPermisoCargaMasiva = "_PermisoCargaMasiva";

        private readonly UsuariosDatos UsuariosRepositorio;

        public LoginController(IConfiguration configuration)
        {
            //objDepartamentosNeg = new DepartamentoNegocios();
            UsuariosRepositorio = new UsuariosDatos(configuration);
        }


        // GET: /<controller>/
        public IActionResult Index()
        {

            var name = HttpContext.Session.GetString(SessionKeyUsuario);
            var tipo = HttpContext.Session.GetString(SessionKeyTipo);
            var codUsuario = HttpContext.Session.GetString(SessionKeyCodUsuario);
            
            if (name != null && tipo != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }


        }

        // POST: Autoridad/Create


        [HttpPost]




        public IActionResult GetSessionData(Modelo.Entity.Usuarios cust)
        {
            string nombre = cust.PU000 + cust.PU001;

           


            var datos = UsuariosRepositorio.Login(cust);
            if (datos.Any())
            {
                var dato = datos.FirstOrDefault();
                // Requires using Microsoft.AspNetCore.Http;
                HttpContext.Session.SetString(SessionKeyUsuario, dato.PU000);
                HttpContext.Session.SetString(SessionKeyTipo, Convert.ToString(dato.PFCOD));
                HttpContext.Session.SetString(SessionKeyCodUsuario, dato.PUCOD.ToString());
                HttpContext.Session.SetString(SessionKeyNombreUsuario, dato.PU002.ToString() + " " + dato.PU003.ToString());
                HttpContext.Session.SetString(SessionKeyCodFuncion, dato.PFCOD.ToString());
                HttpContext.Session.SetString(SessionKeyPermisoCargaMasiva, dato.caraut.ToString());
                HttpContext.Session.SetString("PermisoCargaMasiva", dato.caraut.ToString());
                Modelo.Entity.Usuarios us = new Modelo.Entity.Usuarios();
                us.PUCOD = Convert.ToInt32(dato.PUCOD.ToString());
                HttpContext.Session.SetString("codAutoridad", dato.CODAUT.ToString());
                HttpContext.Session.SetString("codusuarioingreso", dato.PUCOD.ToString());
                HttpContext.Session.SetString("nombrePerfil", dato.funcion.ToString());
                var name = HttpContext.Session.GetString(SessionKeyUsuario);
                var tipo = HttpContext.Session.GetString(SessionKeyTipo);
                var codUsuario = HttpContext.Session.GetString(SessionKeyCodUsuario);

                if (name != null && tipo != null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        [HttpGet]
        public IActionResult GetLoginSmart(string Usuario, string Password)
        {
            // string nombre = cust.PU000 + cust.PU001;
            Modelo.Entity.Usuarios us = new Modelo.Entity.Usuarios();
            us.PU000 = Usuario;
            us.PU001 = Password;

            var datos = UsuariosRepositorio.Login(us).FirstOrDefault();
           // var jsonResult = listaDatosU(us);
            return Json(datos);

        }

        public List<Modelo.Entity.Usuarios> listaDatosU(Modelo.Entity.Usuarios us)
        {
            return UsuariosRepositorio.Login(us).ToList();
        }


    }


}

