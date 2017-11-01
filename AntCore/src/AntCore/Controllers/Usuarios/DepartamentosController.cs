using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Modelo.Entity;
using Model.Datos;
using Microsoft.AspNetCore.Http;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AntCore.Controllers.Departamentos
{
    public class DepartamentosController : Controller
    {
        const string SessionKeyUsuario = "_Usuario";
        const string SessionKeyTipo = "_Tipo";
        private readonly DepartamentoDatos DepartamentoRepositorio;

        public DepartamentosController(IConfiguration configuration)
        {
            //objDepartamentosNeg = new DepartamentoNegocios();
            DepartamentoRepositorio = new DepartamentoDatos(configuration);
        }
        // GET: Departamentos


        public IActionResult Index()
        {


            var name = HttpContext.Session.GetString(SessionKeyUsuario);
            var tipo = HttpContext.Session.GetString(SessionKeyTipo);
            if (name != null && tipo != null)
            {

                return View(DepartamentoRepositorio.FindAll());
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

        // POST: Departamento/Create
        [HttpPost]
        public IActionResult Create(Departamento cust)
        {
            if (ModelState.IsValid)
            {
                cust.PD001 = 1;
                DepartamentoRepositorio.Add(cust);
                return RedirectToAction("Index");
            }
            return View(cust);

        }

        // GET: /Departamento/Edit/1
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Departamento obj = DepartamentoRepositorio.FindByID(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }

        // POST: /Departamento/Edit   
        [HttpPost]
        public IActionResult Edit(Departamento obj)
        {

            if (ModelState.IsValid)
            {
                obj.PD001 = 1;
                DepartamentoRepositorio.Update(obj);
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET:/Departamento/Delete/1
        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            DepartamentoRepositorio.Remove(id.Value);
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
