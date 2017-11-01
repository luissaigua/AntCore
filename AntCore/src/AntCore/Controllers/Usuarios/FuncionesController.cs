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
    public class FuncionesController : Controller
    {

        const string SessionKeyUsuario = "_Usuario";
        const string SessionKeyTipo = "_Tipo";
        private readonly FuncionesDatos FuncionesRepositorio;

        public FuncionesController(IConfiguration configuration)
        {
            //objFuncionessNeg = new FuncionesNegocios();
            FuncionesRepositorio = new FuncionesDatos(configuration);
        }
        // GET: Funcioness


        public IActionResult Index()
        {
            
            

            var name = HttpContext.Session.GetString(SessionKeyUsuario);
            var tipo = HttpContext.Session.GetString(SessionKeyTipo);
            if (name != null && tipo != null)
            {

                return View(FuncionesRepositorio.FindAll());
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

        // POST: Funciones/Create
        [HttpPost]
        public IActionResult Create(Funciones cust)
        {
            if (ModelState.IsValid)
            {
                cust.PF001 = 1;
                FuncionesRepositorio.Add(cust);
                return RedirectToAction("Index");
            }
            return View(cust);

        }

        // GET: /Funciones/Edit/1
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Funciones obj = FuncionesRepositorio.FindByID(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }

        // POST: /Funciones/Edit   
        [HttpPost]
        public IActionResult Edit(Funciones obj)
        {

            if (ModelState.IsValid)
            {
                obj.PF001 = 1;
                FuncionesRepositorio.Update(obj);
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET:/Funciones/Delete/1
        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            FuncionesRepositorio.Remove(id.Value);
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
