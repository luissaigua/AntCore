using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Modelo.Negocios;
using Model.Datos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using excel = OfficeOpenXml;
using Microsoft.AspNetCore.Http;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AntCore.Controllers.DescargaSiniestros
{
    public class DescargaSiniestrosController : Controller
    {

        SiniestroNegocio objSiniestrosNeg;
        
        private readonly IHostingEnvironment _hostingEnvironment;
        public  DescargaSiniestrosController(IHostingEnvironment environment)
        {
            _hostingEnvironment = environment;
            objSiniestrosNeg = new SiniestroNegocio();
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            string codAutoridad = HttpContext.Session.GetString("codAutoridad");
            string cargamasiva = HttpContext.Session.GetString("PermisoCargaMasiva");
            ViewBag.listaProvincias = objSiniestrosNeg.listaProvinciasEditVista();
            ViewBag.listaAutoridades = objSiniestrosNeg.listadAutoridades(codAutoridad, cargamasiva);
            @ViewBag.mensajeError = "";
            return View();
        }
        public JsonResult ProcesarVistaSiniestros(string fecini, string fecFin, string codprov,string codauto)
        {
            string mensajeError = "";
            string mensajeOk = "";
            var jsonResult = objSiniestrosNeg.listaSiniestrosPorFechas(fecini, fecFin, codauto, codprov);

            return Json(jsonResult);
        }

        public FileResult DownloadSin(string tbFechaini, string tbFechafin, int tbcodprov, string tbcodautoridad)
        {
            var retorno = "0";//
            string nombreArchivo = "";
            nombreArchivo = "Siniestros" +"_" + tbFechaini + "_" + tbFechafin;
            retorno = DescargaExcel(nombreArchivo, tbFechaini, tbFechafin, tbcodprov, tbcodautoridad);
                var fileName = nombreArchivo + ".xlsx";
                string sWebRootFolder = _hostingEnvironment.WebRootPath + "/uploadsSin";
                //var filepath = $"C:/uploadSin/{fileName}";
                var filepath = sWebRootFolder + "/" + fileName;
                byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
                return File(fileBytes, "application/x-msdownload", fileName);
            
                

        }

        [HttpGet]
        [Route("Export")]
        public string DescargaExcel(string sFileName, string tbFechaini, string tbFechafin, int tbcodprov, string tbcodautoridad)
        {
            string retorno = "1";

            string sWebRootFolder = _hostingEnvironment.WebRootPath + "/uploadsSin/";//"C:\\uploadSin";//
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
                    excel.ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Siniestro");
                    //coloco las cabeceras del primer sheet
                    worksheet.Cells[1, 1].Value = "#Siniestro";
                    worksheet.Cells[1, 2].Value = "Fecha";
                    worksheet.Cells[1, 3].Value = "Hora";
                    worksheet.Cells[1, 4].Value = "Latitud";
                    worksheet.Cells[1, 5].Value = "Longitud";
                    worksheet.Cells[1, 6].Value = "Provincia";
                    worksheet.Cells[1, 7].Value = "Cantón";
                    worksheet.Cells[1, 8].Value = "Parroquia";
                    worksheet.Cells[1, 9].Value = "Zona";
                    worksheet.Cells[1, 10].Value = "Direccion";
                    worksheet.Cells[1, 11].Value = "#Fallecidos";
                    worksheet.Cells[1, 12].Value = "#Lesionados";
                    worksheet.Cells[1, 13].Value = "CondicionAtmosférica";
                    worksheet.Cells[1, 14].Value = "CondicionVía";
                    worksheet.Cells[1, 15].Value = "TipoDeVía";
                    worksheet.Cells[1, 16].Value = "LímiteDeVelocidad";
                    worksheet.Cells[1, 17].Value = "TrabajosEnLaVía";
                    worksheet.Cells[1, 18].Value = "#Carriles";
                    worksheet.Cells[1, 19].Value = "MaterialDeLaSuperfice";
                    worksheet.Cells[1, 20].Value = "ControlDeIntersección";
                    worksheet.Cells[1, 21].Value = "ObstáculosEnLaVía";
                    worksheet.Cells[1, 22].Value = "LugarEnLaVíaVia";
                    worksheet.Cells[1, 23].Value = "CurvaExistente";
                    worksheet.Cells[1, 24].Value = "SeñalizacionExistente";
                    worksheet.Cells[1, 25].Value = "TipoDeSiniestro";
                    worksheet.Cells[1, 26].Value = "CausaDelProbable";
                    worksheet.Cells[1, 27].Value = "LuzArtificial";
                    worksheet.Cells[1, 28].Value = "RegistroValidado";
                    worksheet.Cells[1, 29].Value = "Autoridad";
                    

                    // busco y recorrro el siniestro a buscar
                    string codisniestros ="";
                    var datos = objSiniestrosNeg.listaVistaSiniestrosPorFechas(tbFechaini, tbFechafin, tbcodprov, tbcodautoridad);
                    int filaS = 1;
                    foreach (var d in datos)
                    {
                        filaS++;
                        // coloco los valores en los campos correspondientes
                        worksheet.Cells["A" + "" + filaS].Value = d.codsin.ToString();
                        worksheet.Cells["B" + "" + filaS].Value = d.fecsin;
                        worksheet.Cells["C" + "" + filaS].Value = d.horsin.ToString();
                        worksheet.Cells["D" + "" + filaS].Value = d.latsin;
                        worksheet.Cells["E" + "" + filaS].Value = d.lonsin;
                        worksheet.Cells["F" + "" + filaS].Value = d.provincia;
                        worksheet.Cells["G" + "" + filaS].Value = d.canton;
                        worksheet.Cells["H" + "" + filaS].Value = d.parroquia;
                        worksheet.Cells["I" + "" + filaS].Value = d.zonsin;
                        worksheet.Cells["J" + "" + filaS].Value = d.dirsin;
                        worksheet.Cells["K" + "" + filaS].Value = d.numfalsin;
                        worksheet.Cells["L" + "" + filaS].Value = d.numlessin;
                        worksheet.Cells["M" + "" + filaS].Value = d.conatmsin;
                        worksheet.Cells["N" + "" + filaS].Value = d.conviasin;
                        worksheet.Cells["O" + "" + filaS].Value = d.desviasin;
                        worksheet.Cells["P" + "" + filaS].Value = d.limvelsin;
                        worksheet.Cells["Q" + "" + filaS].Value = d.traviasin == true ? "SI" : "NO";
                        worksheet.Cells["R" + "" + filaS].Value = d.numcarsin;
                        worksheet.Cells["S" + "" + filaS].Value = d.matsupviasin;
                        worksheet.Cells["T" + "" + filaS].Value = d.intsin;
                        worksheet.Cells["U" + "" + filaS].Value = d.obsviasin;
                        worksheet.Cells["V" + "" + filaS].Value = d.lugviasin;
                        worksheet.Cells["W" + "" + filaS].Value = d.cursin;
                        worksheet.Cells["X" + "" + filaS].Value = d.sensin;
                        worksheet.Cells["Y" + "" + filaS].Value = d.tiposiniestro;
                        worksheet.Cells["Z" + "" + filaS].Value = d.causa_probable;
                        worksheet.Cells["AA" + "" + filaS].Value = d.luzartsin == "SELECCIONAR" ? "" : d.luzartsin;
                        worksheet.Cells["AB" + "" + filaS].Value = d.regvalsin == true ? "OK" : "PENDIENTE";
                        worksheet.Cells["AC" + "" + filaS].Value = d.codaut ;
                        codisniestros += d.codsin.ToString()+",";
                    }
                    codisniestros = codisniestros;
                    // añado el  2do sheet de vehiculos
                    excel.ExcelWorksheet worksheet1 = package.Workbook.Worksheets.Add("Vehiculos");
                    //coloco las cabeceras del primer sheet
                    worksheet1.Cells[1, 1].Value = "#Siniestro";
                    worksheet1.Cells[1, 2].Value = "#Placa";
                    worksheet1.Cells[1, 3].Value = "DanioMaterialAlVehículo";
                    worksheet1.Cells[1, 4].Value = "MatriculaVigente";
                    worksheet1.Cells[1, 5].Value = "Chasis";
                    worksheet1.Cells[1, 6].Value = "Marca";
                    worksheet1.Cells[1, 7].Value = "Modelo";
                    worksheet1.Cells[1, 8].Value = "Año";
                    worksheet1.Cells[1, 9].Value = "Cilindraje";
                    worksheet1.Cells[1, 10].Value = "SeguroPrivado";
                    worksheet1.Cells[1, 11].Value = "TransportaMaterialPeligroso";
                    worksheet1.Cells[1, 12].Value = "TipoDeServicio";
                    worksheet1.Cells[1, 13].Value = "TipoDEVehÍculo";
                    worksheet1.Cells[1, 14].Value = "SubTipoVehiculo";

                    int fila = 1;
                    // busco y recorrro el vehiculo a buscar
                    foreach (var d in objSiniestrosNeg.ListaVehiculosPorRangFechas(codisniestros))
                    {
                        fila++;
                        // coloco los valores en los campos correspondientes
                        worksheet1.Cells["A" + "" + fila].Value = d.codsin.ToString();
                        worksheet1.Cells["B" + "" + fila].Value = d.placvehinv.ToString();
                        worksheet1.Cells["C" + "" + fila].Value = d.danmatvehinv == true ? "SI" : "NO";
                        worksheet1.Cells["D" + "" + fila].Value = d.matvigvehinv == true ? "SI" : "NO"; ;
                        worksheet1.Cells["E" + "" + fila].Value = d.chavehinv;
                        worksheet1.Cells["F" + "" + fila].Value = d.marvehinv;
                        worksheet1.Cells["G" + "" + fila].Value = d.modvehinv;
                        worksheet1.Cells["H" + "" + fila].Value = d.anivehinv.ToString();
                        worksheet1.Cells["I" + "" + fila].Value = d.cilvehinv.ToString();
                        worksheet1.Cells["J" + "" + fila].Value = d.segprivehinv == true ? "SI" : "NO";
                        worksheet1.Cells["K" + "" + fila].Value = d.matpelvehinv;
                        worksheet1.Cells["L" + "" + fila].Value = d.desser;
                        worksheet1.Cells["M" + "" + fila].Value = d.destipveh;
                        worksheet1.Cells["N" + "" + fila].Value = d.dessubveh;

                    }

                    // añado el  3er sheet de victimas
                    excel.ExcelWorksheet worksheet2 = package.Workbook.Worksheets.Add("Victimas");
                    //coloco las cabeceras del primer sheet
                    worksheet2.Cells[1, 1].Value = "#Siniestro";
                    worksheet2.Cells[1, 2].Value = "#Placa";
                    worksheet2.Cells[1, 3].Value = "TipoDeIdentificación";
                    worksheet2.Cells[1, 4].Value = "#Indentificacion";
                    worksheet2.Cells[1, 5].Value = "Edad";
                    worksheet2.Cells[1, 6].Value = "Sexo";
                    worksheet2.Cells[1, 7].Value = "CondicionVictima24h";
                    worksheet2.Cells[1, 8].Value = "CondicionVictima30d";
                    worksheet2.Cells[1, 9].Value = "TipoDeParticipante";
                    worksheet2.Cells[1, 10].Value = "UsoCasco";
                    worksheet2.Cells[1, 11].Value = "UsoCinturon";
                    worksheet2.Cells[1, 12].Value = "PosiciondeLaPlaza";
                    worksheet2.Cells[1, 13].Value = "SospechaDeConsumoAlcohol";

                    int fila1 = 1;
                    // busco y recorrro la victima a buscar
                    foreach (var d in objSiniestrosNeg.listaVictimasInvolucradosPorRangoFechas(codisniestros))
                    {
                        fila1++;
                        // coloco los valores en los campos correspondientes
                        worksheet2.Cells["A" + "" + fila1].Value = d.codsin.ToString();
                        worksheet2.Cells["B" + "" + fila1].Value = d.PLACAVHL.ToString();
                        worksheet2.Cells["C" + "" + fila1].Value = d.tipidenvicinv.ToString();
                        worksheet2.Cells["D" + "" + fila1].Value = d.numidenvicinv.ToString();
                        worksheet2.Cells["E" + "" + fila1].Value = d.edavicinv.ToString();
                        worksheet2.Cells["F" + "" + fila1].Value = d.sexo;
                        worksheet2.Cells["G" + "" + fila1].Value = d.convicinv24;
                        worksheet2.Cells["H" + "" + fila1].Value = d.convicinv30;
                        worksheet2.Cells["I" + "" + fila1].Value = d.tipparvicinv.ToString();
                        worksheet2.Cells["J" + "" + fila1].Value = d.USO_CASO.ToString();
                        worksheet2.Cells["K" + "" + fila1].Value = d.USO_CINTU;
                        worksheet2.Cells["L" + "" + fila1].Value = d.posvicinv;
                        worksheet2.Cells["M" + "" + fila1].Value = d.CONS_ALCOHOL;
                        

                    }
                    // añado el  4er sheet de acciones peaton
                    excel.ExcelWorksheet worksheet3 = package.Workbook.Worksheets.Add("AccionesPeaton");
                    //coloco las cabeceras del primer sheet
                    worksheet3.Cells[1, 1].Value = "#Siniestro";
                    worksheet3.Cells[1, 2].Value = "#PlacaVehiculo";
                    worksheet3.Cells[1, 3].Value = "#Indetificacion";
                    worksheet3.Cells[1, 4].Value = "AccionPeaton";
                    
                    

                    int fila2 = 1;
                    // busco y recorrro la victima a buscar
                    foreach (var d in objSiniestrosNeg.listaVistaAccionesPeatonesPorRangoFechas(codisniestros))
                    {
                        fila2++;
                        // coloco los valores en los campos correspondientes
                        worksheet3.Cells["A" + "" + fila2].Value = d.codsin.ToString();
                        worksheet3.Cells["B" + "" + fila2].Value = d.placvehinv.ToString();
                        worksheet3.Cells["C" + "" + fila2].Value = d.numidenvicinv.ToString();
                        worksheet3.Cells["D" + "" + fila2].Value = d.desaccpea.ToString();
                        


                    }

                    // añado el  5to sheet de danios a terceros
                    excel.ExcelWorksheet worksheet4 = package.Workbook.Worksheets.Add("DaniosTerceros");
                    //coloco las cabeceras del primer sheet
                    worksheet4.Cells[1, 1].Value = "#Siniestro";
                    worksheet4.Cells[1, 2].Value = "TipoDaño";
                    worksheet4.Cells[1, 3].Value = "Observaciones";
                    


                    int fila3 = 1;
                    // busco y recorrro la victima a buscar
                    foreach (var d in objSiniestrosNeg.listaVistaDaniosTercerosPorRangoFehcas(codisniestros))
                    {
                        fila3++;
                        worksheet4.Cells["A" + "" + fila3].Value = d.codsin.ToString();
                        worksheet4.Cells["B" + "" + fila3].Value = d.destipdater.ToString();
                        worksheet4.Cells["C" + "" + fila3].Value = d.obsdater.ToString();
                        



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

        public IActionResult GetHola()
        {
            HttpContext.Session.Clear();
            //var name = HttpContext.Session.GetString(SessionKeyUsuario);
            return RedirectToAction("Index", "Login");
        }

    }
}
