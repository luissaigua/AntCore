using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model.Datos;
using Modelo.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.Net.Http.Headers;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AntCore.Controllers.Geolocalizacion
{
    public class GeolocalizacionController : Controller
    {

        private readonly GeoDatos GeoRepositorio;

        private readonly IHostingEnvironment _hostingEnvironment;
        public GeolocalizacionController(IConfiguration configuration, IHostingEnvironment environment)
        {
            _hostingEnvironment = environment;
            //objAutoridadsNeg = new AutoridadNegocios();
            GeoRepositorio = new GeoDatos(configuration);
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }


        public ActionResult Geolocalizacion(Georeferencia geo, string fecha )
        {
            geo.VALGEO = 0;
            geo.FECGEO = Convert.ToDateTime(fecha);
            var data = GeoRepositorio.Add(geo);
            
         
            
            return Json(new { CodGeo=data});
        }
        [HttpPost]
        public async Task<IActionResult> UploadFilesSiniestro()
        {


            long size = 0;
            var files = Request.Form.Files;
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "Siniestros");
            foreach (var file in files)
            {
                var filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');
                filename = uploads + $@"/{filename}"; // para linux el path es con "/"
                //filename = uploads + $@"\{filename}"; // para wind el path es con "\"
                size += file.Length;
                using (FileStream fs = System.IO.File.Create(filename))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
            }
            return Ok(new { count = files.Count, size });
        }
        [HttpPost]
        public async Task<IActionResult> UploadFilesCroquis()
        {


            long size = 0;
            var files = Request.Form.Files;
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "Croquis");
            foreach (var file in files)
            {
                var filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');
                filename = uploads + $@"/{filename}"; // para linux el path es con "/"
                //filename = uploads + $@"\{filename}"; // para wind el path es con "\"
                size += file.Length;
                using (FileStream fs = System.IO.File.Create(filename))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
            }
            return Ok(new { count = files.Count, size });
        }



        //public ActionResult Imagen()
        //{
        //    ContentRootPath
        //    var path = env.ContentRootPath + "Views\\" + s;
        //}


    }
}
