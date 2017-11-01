using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Modelo.Entity;
using Model.Datos;
using Modelo.Negocios;
using Microsoft.AspNetCore.Http;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AntCore.Controllers.Usuarios
{
    public class AutoridadesController : Controller
    {
        const string SessionKeyUsuario = "_Usuario";
        const string SessionKeyTipo = "_Tipo";
        private readonly AutoridadDatos AutoridadRepositorio;
        SiniestroNegocio objSiniestrosNeg;

        public AutoridadesController(IConfiguration configuration)
        {
            //objAutoridadsNeg = new AutoridadNegocios();
            AutoridadRepositorio = new AutoridadDatos(configuration);
            objSiniestrosNeg = new SiniestroNegocio();
        }
        // GET: Autoridads


        public IActionResult Index()
        {


            var name = HttpContext.Session.GetString(SessionKeyUsuario);
            var tipo = HttpContext.Session.GetString(SessionKeyTipo);
            if (name != null && tipo != null)
            {

                return View(AutoridadRepositorio.FindAll());
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: Autoridad/Create
        [HttpPost]
        public IActionResult Create(Modelo.Entity.Autoridad cust)
        {
            if (ModelState.IsValid)
            {
                AutoridadRepositorio.Add(cust);
                return RedirectToAction("Index");
            }
            return View(cust);

        }

        // GET: /Autoridad/Edit/1
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Modelo.Entity.Autoridad obj = AutoridadRepositorio.FindByCOD(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }

        // POST: /Autoridad/Edit   
        [HttpPost]
        public IActionResult Edit(Modelo.Entity.Autoridad obj)
        {

            if (ModelState.IsValid)
            {
                AutoridadRepositorio.Update(obj);
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET:/Autoridad/Delete/1
        public IActionResult Delete(string id)
        {
           // string id = CODAUT;
            if (id == null)
            {
                return NotFound();
            }
            objSiniestrosNeg.ModificarAutoridad(id);
            //AutoridadRepositorio.Remove(CODAUT);
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
