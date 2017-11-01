using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using OfficeOpenXml;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Modelo.Entity;
using Modelo.Negocios;
using Model.Datos;
using excel = OfficeOpenXml;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AntCore.Controllers.CargaCalificaciones
{
    public class CargaCalificacionesController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        SiniestroNegocio objSiniestrosNeg;

        List<SelectListItem> listaCodificaciones = new List<SelectListItem>();
        List<SelectListItem> listaCodificacionesSin = new List<SelectListItem>();
        public CargaCalificacionesController(IHostingEnvironment environment)
        {
            _hostingEnvironment = environment;
            objSiniestrosNeg = new SiniestroNegocio();
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.ListaCodificaciones = listaCodificaciones;
            ViewBag.listaCargaCalificacionesPorFecha = objSiniestrosNeg.ListaverificarCalificacionesPorFecha("", "");
            return View();
           // ViewBag.listaCargaCalificacionesPorFecha = objSiniestrosNeg.ListaverificarCalificacionesPorFecha("", "");
        }
        public IActionResult Vista()
        {
            ViewBag.listaCargaCalificacionesPorFechaVista = objSiniestrosNeg.ListaverificarCalificacionesPorRangoFecha("", "",0);
            ViewBag.listaProvincias = objSiniestrosNeg.listaProvinciasEditVista();
            return View();
        }
        public JsonResult ProcesarVista(string fecinical, string fecFin, int codprov)
        {
            string mensajeError = "";
            string mensajeOk = "";
            var jsonResult = objSiniestrosNeg.ListaverificarCalificacionesPorRangoFecha(fecinical, fecFin, codprov);
           
            return Json(jsonResult);
        }

        public async Task<ActionResult> Validar(ICollection<IFormFile> file,string feccal,string reemplazar)
        {
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploadsCal");
            string mensajeError = "", mensajeOk="";
            if (reemplazar == null || reemplazar == "")
                reemplazar = "NO";
            int count = 0;
            int codprov = 0;
            string anio = "", mes = "";
            var _fecha = feccal.Split('-');
            anio = _fecha[0].ToString();
            mes = _fecha[1].ToString();
            if (feccal == null || feccal == "")
            {
                mensajeError = "Ingrese la fecha";
            }
            else if (file.Count == 0)
            {
                mensajeError = "Seleccione un archvio";

            }
            else if (objSiniestrosNeg.ListaverificarCalificacionesPorFecha(anio, mes).Count() > 0 && reemplazar == "NO")
            {
                mensajeError = "Para la fecha seleccionada ya existen calificaciones cargadas, si desea reemplazar favor habilitar la opción Reemplazar calificaciones.";
            }
            else
            {
                try
                {
                    List<Modelo.Entity.Calificaciones> listaCal = new List<Modelo.Entity.Calificaciones>();
                    foreach (var f in file)
                    {
                        if (f.Length > 0)
                        {

                            var fileStream = new FileStream(Path.Combine(uploads, f.FileName), FileMode.Create);
                            await f.CopyToAsync(fileStream);
                            fileStream.Dispose();

                            //string fileName = $@"/{f.FileName}";//esta line para linux
                            string fileName = f.FileName;// ;//esta line para win
                          //  fileName = fileName.Replace(".xlsx","_" + DateTime.Now.ToString("yyyy-mm-dd")+".xlsx");
                            string fileContentType = f.ContentType;
                            byte[] fileBytes = new byte[f.Length];
                            //string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\uploadsCal";  //esta line para win
                            
                            string sWebRootFolder = _hostingEnvironment.WebRootPath + "/uploadsCal/";  //esta line para linux
                            FileInfo files = new FileInfo(Path.Combine(sWebRootFolder, fileName));
                            using (var package = new ExcelPackage(files))
                            {
                                var currentSheet = package.Workbook.Worksheets;
                                var workSheet = currentSheet.First();
                                var noOfCol = workSheet.Dimension.End.Column;
                                var noOfRow = workSheet.Dimension.End.Row;
                                string hoja1 = workSheet.Name;
                               
                                for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                                {
                                    Calificaciones obj = new Calificaciones();
                                    Model.Datos.CargaDropDownList objs = new Model.Datos.CargaDropDownList();
                                    obj.feccal = feccal.ToString();
                                    obj.nombreProvincia = workSheet.Cells[rowIterator, 1].Value.ToString();
                                    obj.cumcal = workSheet.Cells[rowIterator, 2].Value.ToString();
                                    obj.puncal = workSheet.Cells[rowIterator, 3].Value.ToString();
                                    obj.calcal = workSheet.Cells[rowIterator, 4].Value.ToString();
                                    obj.comcal = workSheet.Cells[rowIterator, 5].Value.ToString();
                                    obj.totcal = workSheet.Cells[rowIterator, 6].Value.ToString();
                                    listaCal.Add(obj);

                                    count++;

                                    string nombre_provincia = workSheet.Cells[rowIterator, 1].Value == null ? "" : workSheet.Cells[rowIterator, 1].Value.ToString();
                                    string valor_cumplimiento = workSheet.Cells[rowIterator, 2].Value == null ? "" : workSheet.Cells[rowIterator, 2].Value.ToString();
                                    string ValorPuntualidad = workSheet.Cells[rowIterator, 3].Value == null ? "" : workSheet.Cells[rowIterator, 3].Value.ToString();
                                    string ValorCalidad = workSheet.Cells[rowIterator, 4].Value == null ? "" : workSheet.Cells[rowIterator, 4].Value.ToString();
                                    string ValorCompromiso = workSheet.Cells[rowIterator, 5].Value == null ? "" : workSheet.Cells[rowIterator, 5].Value.ToString();
                                    string TotalCalificacion = workSheet.Cells[rowIterator, 5].Value == null ? "" : workSheet.Cells[rowIterator, 5].Value.ToString();
                                    int cont_prov = 0;

                                    if (nombre_provincia == "") { objs.codigo = count.ToString() + " - " + hoja1; objs.nombre = "El nombre de la provincia esta vacío" + " - " + " Celda {A -" + count + "} "; objs.codigo = count.ToString(); listaCodificaciones.Add(new SelectListItem() { Value = objs.codigo.ToString(), Text = objs.nombre.ToString() }); }
                                    if (nombre_provincia != "")
                                    {
                                        var provincias = objSiniestrosNeg.listaProvincias();
                                        foreach (var p in provincias)
                                        {
                                           
                                            if (p.nomprov == nombre_provincia)
                                            {
                                                cont_prov++;
                                                codprov = p.codprov;
                                            }
                                           

                                        }
                                        if (cont_prov == 0) { objs.codigo = count.ToString() + " - " + hoja1; objs.nombre = "El nombre de la provincia es incorrecto, favor revisar que no tenga tildes" + " - " + " Celda {A -" + count + "} "; objs.codigo = count.ToString(); listaCodificaciones.Add(new SelectListItem() { Value = objs.codigo.ToString(), Text = objs.nombre.ToString() }); }
                                    }
                                    try
                                    {
                                        decimal _valor_cumplimiento = Convert.ToDecimal(valor_cumplimiento);
                                    }
                                    catch (Exception ex)
                                    {
                                        objs.codigo = count.ToString() + " - " + hoja1; objs.nombre = "El valor de cumplimiento tiene un formato incorrecto, el valor debe ser un decimal con el separdor de decimales (.) ejemplo 0.30" + " - " + " Celda {B -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = objs.codigo.ToString(), Text = objs.nombre.ToString() });
                                    }
                                    try
                                    {
                                        decimal _ValorPuntualidad = Convert.ToDecimal(ValorPuntualidad);
                                    }
                                    catch (Exception ex)
                                    {
                                        objs.codigo = count.ToString() + " - " + hoja1; objs.nombre = "El valor de puntualidad tiene un formato incorrecto, el valor debe ser un decimal con el separdor de decimales (.) ejemplo 0.30" + " - " + " Celda {C -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = objs.codigo.ToString(), Text = objs.nombre.ToString() });
                                    }
                                    try
                                    {
                                        decimal _ValorCalidad = Convert.ToDecimal(ValorCalidad);
                                    }
                                    catch (Exception ex)
                                    {
                                        objs.codigo = count.ToString() + " - " + hoja1; objs.nombre = "El valor de calidad tiene un formato incorrecto, el valor debe ser un decimal con el separdor de decimales (.) ejemplo 0.30" + " - " + " Celda {C -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = objs.codigo.ToString(), Text = objs.nombre.ToString() });
                                    }
                                    try
                                    {
                                        decimal _ValorCompromiso = Convert.ToDecimal(ValorCompromiso);
                                    }
                                    catch (Exception ex)
                                    {
                                        objs.codigo = count.ToString() + " - " + hoja1; objs.nombre = "El valor de compromiso tiene un formato incorrecto, el valor debe ser un decimal con el separdor de decimales (.) ejemplo 0.30" + " - " + " Celda {C -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = objs.codigo.ToString(), Text = objs.nombre.ToString() });
                                    }

                                    try
                                    {
                                        decimal _TotalCalificacion = Convert.ToDecimal(TotalCalificacion);
                                    }
                                    catch (Exception ex)
                                    {
                                        objs.codigo = count.ToString() + " - " + hoja1; objs.nombre = "El valor de total calificación tiene un formato incorrecto, el valor debe ser un decimal con el separdor de decimales (.) ejemplo 0.30" + " - " + " Celda {C -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = objs.codigo.ToString(), Text = objs.nombre.ToString() });
                                    }

                                }
                                if (listaCodificaciones.Count() == 0)
                                {
                                    _fecha = feccal.Split('-');
                                    anio = _fecha[0].ToString();
                                    mes = _fecha[1].ToString();
                                    mensajeOk = objSiniestrosNeg.EnviaListaCalificaciones(listaCal, anio, mes);
                                    mensajeOk = "Registros a ser cargados:  " + " " + count.ToString() + "     Registros Total Cargados: " + " " + mensajeOk;
                                    files.Delete();
                                }
                                else if (listaCodificaciones.Count() > 0)
                                {
                                    mensajeError = "Existen errores en el archvio a cargar, favor verificar y corregir.";
                                }
                                

                            }
                            // }
                        }
                    }
                }
                catch  (Exception ex)
                {
                    mensajeError = ex.ToString();
                }
               
           
            }
            ViewBag.listaCargaCalificacionesPorFecha = objSiniestrosNeg.ListaverificarCalificacionesPorFecha(anio, mes);
            ViewBag.mensajeValidacion = mensajeOk;
            ViewBag.mensajeError = mensajeError;
            ViewBag.ListaCodificaciones = listaCodificaciones;
            return View("Index");
        }


        [HttpGet]
        [Route("Import")]
        public string Import(string sFileName)
        {
            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            //string sFileName = @"demo.xlsx";
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            try
            {
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    StringBuilder sb = new StringBuilder();
                    ExcelWorksheet worksheet = package.Workbook.Worksheets["Hoja1"];
                    int rowCount = worksheet.Dimension.Rows;
                    int ColCount = worksheet.Dimension.Columns;
                    bool bHeaderRow = true;
                    for (int row = 1; row <= rowCount; row++)
                    {
                        for (int col = 1; col <= ColCount; col++)
                        {
                            if (bHeaderRow)
                            {
                                sb.Append(worksheet.Cells[row, col].Value.ToString() + "\t");
                            }
                            else
                            {
                                sb.Append(worksheet.Cells[row, col].Value.ToString() + "\t");
                            }
                        }
                        sb.Append(Environment.NewLine);
                    }
                    return sb.ToString();
                }
                
            }
            catch (Exception ex)
            {
                return "Some error occured while importing." + ex.Message;
            }
        }

        public FileResult DownloadCal(string tbFechaini, string tbFechafin, int tbcodprov)
        {
            var retorno = "0";//
            string nombreArchivo = "";
            nombreArchivo = "Calificacion" + "_" + tbFechaini + "_" + tbFechafin;
            retorno = DescargaExcel(nombreArchivo, tbFechaini, tbFechafin, tbcodprov);
            var fileName = nombreArchivo + ".xlsx";
            string sWebRootFolder = _hostingEnvironment.WebRootPath + "/uploadsCal";
            //var filepath = $"C:/uploadSin/{fileName}";
            var filepath = sWebRootFolder + "/" + fileName;
            byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
            return File(fileBytes, "application/x-msdownload", fileName);
            
        }
        [HttpGet]
        [Route("Export")]
        public string DescargaExcel(string sFileName, string fecini, string fecFin, int codprov)
        {
            string retorno = "1";

            string sWebRootFolder = _hostingEnvironment.WebRootPath + "/uploadsCal/";//"C:\\uploadSin";//
            sFileName = sFileName + "." + "xlsx";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            }
            try
            {
                using (excel.ExcelPackage package = new excel.ExcelPackage(file))
                {
                    // añado el  primer sheet de siniestros
                    excel.ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Calificacion");
                    //coloco las cabeceras del primer sheet
                    worksheet.Cells[1, 1].Value = "Fecha";
                    worksheet.Cells[1, 2].Value = "Provincia";
                    worksheet.Cells[1, 3].Value = "V. Cumplimiento";
                    worksheet.Cells[1, 4].Value = "V. Puntualidad";
                    worksheet.Cells[1, 5].Value = "V. Calidad";
                    worksheet.Cells[1, 6].Value = "V. Compromiso";
                    worksheet.Cells[1, 7].Value = "T. Calificacion";

                    int fila = 1;
                    foreach (var d in objSiniestrosNeg.ListaverificarCalificacionesPorRangoFecha(fecini,fecFin,codprov))
                    {
                        fila++;
                        // coloco los valores en los campos correspondientes
                        worksheet.Cells["A" + "" + fila].Value = d.feccal.ToString();
                        worksheet.Cells["B" + "" + fila].Value = d.nombreProvincia;
                        worksheet.Cells["C" + "" + fila].Value = d.cumcal.ToString();
                        worksheet.Cells["D" + "" + fila].Value = d.puncal;
                        worksheet.Cells["E" + "" + fila].Value = d.calcal;
                        worksheet.Cells["F" + "" + fila].Value = d.comcal;
                        worksheet.Cells["G" + "" + fila].Value = d.totcal;

                    }

                   

                    try
                    {
                        if (file.Exists)
                        {
                            file.Delete();
                            file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
                        }
                    }
                    catch
                    {
                    }
                    package.Save(); //guardo el excel.
                                    //string _sPathFilePDF = String.Empty;
                 





                }
            }
            catch (Exception ex)
            {
                retorno = "0";
            }

            return retorno;
        }
        public FileResult DownloadFormatoCargaCalificaciones()
        {
            var retorno = "0";//
            string nombreArchivo = "";
            nombreArchivo = "FormatoCargaCalificaciones.xlsx";
            //retorno = DescargaExcel(nombreArchivo, id);
            var fileName = nombreArchivo;
            string sWebRootFolder = _hostingEnvironment.WebRootPath + "/uploads";
            //var filepath = $"C:/uploadSin/{fileName}";
            var filepath = sWebRootFolder + "/" + fileName;
            byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
            return File(fileBytes, "application/x-msdownload", fileName);
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
