using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model.Datos;
using Modelo.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AntCore.Controllers.Usuarios
{
    public class UsuariosController : Controller
    {
        private readonly UsuariosDatos UsuariosRepositorio;
        private readonly DepartamentoDatos DepartamentoRepositorio;
        private readonly FuncionesDatos FuncionRepositorio;
        private readonly AutoridadDatos AutoridadRepositorio;


        const string SessionKeyUsuario = "_Usuario";
        const string SessionKeyTipo = "_Tipo";
        public UsuariosController(IConfiguration configuration)
        {
            //objUsuariossNeg = new UsuariosNegocios();
            UsuariosRepositorio = new UsuariosDatos(configuration);
            DepartamentoRepositorio = new DepartamentoDatos(configuration);
            FuncionRepositorio = new FuncionesDatos(configuration);
            AutoridadRepositorio = new AutoridadDatos(configuration);

        }
        // GET: Usuarioss


        public IActionResult Index()
        {



            var name = HttpContext.Session.GetString(SessionKeyUsuario);
            var tipo = HttpContext.Session.GetString(SessionKeyTipo);
            if (name != null && tipo != null)
            {

                return View(UsuariosRepositorio.FindAll());
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult GetDepartamentos()
        {
            var datos = DepartamentoRepositorio.FindAll();
            return Json(datos);
        }

        public IActionResult Create()
        {
            ViewBag.DepList = DepartamentoRepositorio.FindAll();
            ViewBag.FunList = FuncionRepositorio.FindAll();
            ViewBag.AutList = AutoridadRepositorio.FindAll();
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        public IActionResult Create(Modelo.Entity.Usuarios cust)
        {
            if (ModelState.IsValid)
            {

                UsuariosRepositorio.Add(cust);
                return RedirectToAction("Index");
            }

            ViewBag.DepList = DepartamentoRepositorio.FindAll();
            ViewBag.FunList = FuncionRepositorio.FindAll();
            ViewBag.AutList = AutoridadRepositorio.FindAll();
            return View();

        }

        // GET: /Usuarios/Edit/1
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.DepList = DepartamentoRepositorio.FindAll();
            ViewBag.FunList = FuncionRepositorio.FindAll();
            ViewBag.AutList = AutoridadRepositorio.FindAll();
            Modelo.Entity.Usuarios obj = UsuariosRepositorio.FindByID(id.Value);
            Model.Datos.Encriptacion desc = new Model.Datos.Encriptacion();
          //  obj.PU001 = desc.DesEncriptar(obj.PU001);
            if (obj == null)
            {
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        // POST: /Usuarios/Edit   
        [HttpPost]
        public IActionResult Edit(Modelo.Entity.Usuarios obj)
        {

            if (ModelState.IsValid)
            {
                UsuariosRepositorio.Update(obj);
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET:/Usuarios/Delete/1
        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            UsuariosRepositorio.Remove(id.Value);
            return RedirectToAction("Index");
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
