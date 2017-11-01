using System;
using System.Collections.Generic;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Modelo.Entity;
using Modelo.Negocios;
using Model.Datos;
using Newtonsoft.Json; // Use for JsonConvert
using Microsoft.AspNetCore.Mvc.Rendering;
using excel = OfficeOpenXml;
using System.Text;
using Microsoft.AspNetCore.Http;
using Npgsql;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Xml.Linq;
using System.Linq;
using System.Xml.Serialization;
//using System.Web.Mvc.FilePathResult;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AntCore.Controllers.Siniestro
{
    public class SiniestroController : Controller
    {

       // DownloadFiles obj;
        private readonly IHostingEnvironment _hostingEnvironment;
        SiniestroNegocio objSiniestrosNeg;
       ProvinciasNegocio objProvinciasNeg;
       ProvinciasRepositorio provinciasRepository = null;
       const string SessionKeyCodUsuario = "_CodUsuario";
        private ConexionDB objConexinDB;
        private NpgsqlCommand comando;
        public SiniestroController(IHostingEnvironment environment)
        {
            objSiniestrosNeg = new SiniestroNegocio();
            provinciasRepository = new ProvinciasRepositorio();
            objProvinciasNeg = new ProvinciasNegocio();
            objConexinDB = ConexionDB.saberEstado();
            _hostingEnvironment = environment;
        }

        
        // GET: /<controller>/
        public IActionResult Index()
        {
            var codUsuario = HttpContext.Session.GetString(SessionKeyCodUsuario);
            string codAutoridad = HttpContext.Session.GetString("codAutoridad");
            List<Modelo.Entity.Siniestro> lista = objSiniestrosNeg.findAll(codAutoridad);
            if(codUsuario != "" && codUsuario != null)
                return View(lista);
            else
                return RedirectToAction("Index", "Login");


        }
        public  IActionResult Busqueda()
        {
            //objSiniestrosNeg.enviaMail();
            objSiniestrosNeg.ActualizaNombresVictimas();
            ViewBag.resultado = "";
            return View();
        }


        public IActionResult FindModificar()
        {
            var codUsuario = HttpContext.Session.GetString(SessionKeyCodUsuario);
            var codpermisoGeneral = HttpContext.Session.GetString("PermisoGeneral");
            var codpermisoSupervisor = HttpContext.Session.GetString("PermisoGeneral");
            string codAutoridad = HttpContext.Session.GetString("codAutoridad");
            List<Modelo.Entity.Siniestro> lista = objSiniestrosNeg.findAll(codAutoridad);
            if (codUsuario != "" && codUsuario != null)
                return View(lista);
            else
                return RedirectToAction("Index", "Login");
            //return View(lista);

        }
        
        public ActionResult FindSiniestro(int id)
        {
            var codUsuario = HttpContext.Session.GetString(SessionKeyCodUsuario);
            string codAutoridad = HttpContext.Session.GetString("codAutoridad");
            Vehiculo v = new Vehiculo();
            v.codsin = id;
            ViewBag.CodigoSiniestroFind = v.codsin.ToString();
            ViewBag.codAutoridad = codAutoridad.ToString();
            if (codUsuario != "" && codUsuario != null)
                return View(ListaVehiculos(v.codsin));
            else
                return RedirectToAction("Index", "Login");
        }

       
        public IActionResult CrearSiniestro()
        {
            var codUsuario = HttpContext.Session.GetString(SessionKeyCodUsuario);
            
            Modelo.Entity.Siniestro model = new Modelo.Entity.Siniestro()
            {
                provinciasLista = new SelectList(objSiniestrosNeg.listaProvincias(), "codprov", "nomprov"),
            };

            ViewBag.tipoZonaLista = ObtenerListadoTipoZona();
            ViewBag.ObtenerListadoCondicionAtmosferica = ObtenerListadoCondicionAtmosferica();
            ViewBag.ObtenerlistadatosCondicionVia = ObtenerlistadatosCondicionVia();
            ViewBag.ObtenerlistadatosLuzArtificial = ObtenerlistadatosLuzArtificial();
            ViewBag.ObtenerlistadatosTipoVia = ObtenerlistadatosTipoVia();
            ViewBag.ObtenerlistadatosLimiteVelocidad = ObtenerlistadatosLimiteVelocidad();
            ViewBag.ObtenerlistadatosControlInterseccion = ObtenerlistadatosControlInterseccion();
            ViewBag.ObtenerlistadatosMaterialSuperficieVia = ObtenerlistadatosMaterialSuperficieVia();
            ViewBag.ObtenerlistadatosObstaculoVia = ObtenerlistadatosObstaculoVia();
            ViewBag.ObtenerlistadatosLugarVia = ObtenerlistadatosLugarVia();
            ViewBag.ObtenerlistadatosNumeroCarriles = ObtenerlistadatosNumeroCarriles();
            ViewBag.ObtenerlistadatosSenialitica = ObtenerlistadatosSenialitica();
            ViewBag.ObtenerlistadatosTransporteMaterialPeligroso = ObtenerlistadatosTransporteMaterialPeligroso();
            ViewBag.ObtenerListaTipoVehiculo = ObtenerListaTipoVehiculo();
            ViewBag.ObtenerlistaServiciosVehiculos = ObtenerlistaServiciosVehiculos();
            ViewBag.ObtenerlistaTipoDaniosTerceros = ObtenerlistaTipoDaniosTerceros();
            ViewBag.ObtenerlistadatosTipoIdentificacion = ObtenerlistadatosTipoIdentificacion();
            ViewBag.ObtenerlistadatosSexo = ObtenerlistadatosSexo();
            ViewBag.ObtenerlistadatosCondicionVictimas24 = ObtenerlistadatosCondicionVictimas24();
            ViewBag.ObtenerlistadatosCondicionVictimas30 = ObtenerlistadatosCondicionVictimas30();
            ViewBag.ObtenerlistadatosTipoParticipante = ObtenerlistadatosTipoParticipante();
            ViewBag.ObtenerlistadatosPosicionPlaza = ObtenerlistadatosPosicionPlaza();
            ViewBag.ObtenerlistadatosAccionesPeaton = ObtenerlistadatosAccionesPeaton();
            ViewBag.ObtenerlistadatosCurvaExistente = ObtenerlistadatosCurvaExistente();
            ViewBag.ObtenerlistatipoSiniestros = ObtenerlistatipoSiniestros();
            ViewBag.ObtenerlistaCausaProbableSiniestros = ObtenerlistaCausaProbableSiniestros();
            ViewBag.ObtenerlistaCausaRealSiniestros = ObtenerlistaCausaRealSiniestros();
            ViewBag.ObtenerListaVheiculos = ListaVehiculos(399);
            string codAutoridad = HttpContext.Session.GetString("codAutoridad");
            ViewBag.ObtenerlistaGeoreferencias = listaGeoreferencias(codAutoridad);
            ViewBag.ObtenerlistaParroquias = ObtenerlistaParroquias();
            ViewBag.ObtenerlistaTodosCiruitos = ObtenerlistaTodosCiruitos();
            ViewBag.ObtenerlistaTodosDistritos = ObtenerlistaTodosDistritos();

            ViewBag.ObtenerlistadatosValoresSiNo = ObtenerlistadatosValoresSiNo();
            string mes = Convert.ToString( DateTime.Now.Month.ToString()).Length == 1 ? "0"+""+ Convert.ToString(DateTime.Now.Month.ToString()) : Convert.ToString(DateTime.Now.Month.ToString());
            string dia = Convert.ToString(DateTime.Now.Day.ToString()).Length == 1 ? "0" + "" + Convert.ToString(DateTime.Now.Day.ToString()) : Convert.ToString(DateTime.Now.Day.ToString());
            string hora = Convert.ToString(DateTime.Now.Hour.ToString()).Length == 1 ? "0" + "" + Convert.ToString(DateTime.Now.Hour.ToString()) : Convert.ToString(DateTime.Now.Hour.ToString());
            string minuto = Convert.ToString(DateTime.Now.Minute.ToString()).Length == 1 ? "0" + "" + Convert.ToString(DateTime.Now.Minute.ToString()) : Convert.ToString(DateTime.Now.Minute.ToString());
            model.fecsin = DateTime.Now.Year.ToString() + '-' + mes +'-'+ dia;
            model.horsin = hora+':'+minuto;
            if (codUsuario != "" && codUsuario != null)
                return View(model);
            else
                return RedirectToAction("Index", "Login");

          
        }

        public JsonResult listaGeoreferenciasAppMovil(int coudusuario)
        {
            var jsonResult = listaGeoreferenciasMovil(coudusuario);
            return Json(jsonResult);
        }

        public ActionResult CargaDatosView(int id)
        {
            ViewBag.ObtenerlistaParroquias = ObtenerlistaParroquias();
            ViewBag.ObtenerlistaTodosCiruitos = ObtenerlistaTodosCiruitos();
            ViewBag.ObtenerlistaTodosDistritos = ObtenerlistaTodosDistritos();
            return View();
        }
        public IActionResult CargaDistritosPorCodigo(int codcant)
        {
            ViewBag.ObtenerlistaCiruitos = ObtenerlistaCiruitos(Convert.ToInt32(1701), Convert.ToInt32(0));
            return View("CrearSiniestro");
        }
        public JsonResult ObtenerPlacaVehiculo(string parametro, string opcion)
        {
            var jsonResult = ObtenerPlacaVhl(parametro, opcion);
            return Json(jsonResult);
        }
        public JsonResult ObtenerlistaGeoreferencias(string codaut)
        {
            var jsonResult = listaGeoreferencias(codaut);
            return Json(jsonResult);
        }
        public JsonResult ObtenerlistaVistaSiniestros(int codsin)
        {
            var jsonResult = listaVistaSiniestros(codsin);
            return Json(jsonResult);
        }
        public JsonResult ObtenerlistaVistaAccionesPeatones(int codsin)
        {
            var jsonResult = listaVistaAccionesPeatones(codsin);
            return Json(jsonResult);
        }
        public JsonResult ObtenerlistaVistaDaniosTerceros(int codsin)
        {
            var jsonResult = listaVistaDaniosTerceros(codsin);
            return Json(jsonResult);
        }
        public JsonResult ObtenerlistaPeaton(int codsin)
        {
            var jsonResult = ObtenerlistaPeatones(codsin);
            return Json(jsonResult);
        }
        public JsonResult ObtenerlistaVictimasInvolucrados(int codsin)
        {
            var jsonResult = listaVictimasInvolucrados(codsin);
            return Json(jsonResult);
        }
        public JsonResult ObtenertarerInformacionSiniestroProcersosSin(int codsin)
        {
            var jsonResult = ObtenertarerInformacionSiniestroProcerso(codsin);
            return Json(jsonResult);
        }


        
        public JsonResult ObtenerTraerNumLesionadosFallecidos(int codsin)
        {
            var jsonResult = TraerNumLesionadosFallecidos(codsin);
            return Json(jsonResult);
        }

        public JsonResult traerDatosVehiculosInvolucradosVictimas(int codsin)
        {
            var   jsonResult = ObtenerlistadaVehiculosInvolucrados(codsin);
            return Json(jsonResult);
        }

        public JsonResult ListaVehiculosFind(int codsin)
        {
            var jsonResult = ListaVehiculos(codsin);
            return Json(jsonResult);
        }
        public JsonResult ObtenerlistaDistritos(int codProv, int codcant, int codpar)
        {
           
            var jsonResult = ObtenerlistaDistrito( codProv,  codcant,codpar);
            return Json(jsonResult);
        }
        public JsonResult ObtenerlistaCircuitos( int codCant, int codPar)
        {
            var jsonResult = ObtenerlistaCiruitos(codCant, codPar);
            return Json(jsonResult);
        }
        public JsonResult JsoninsertarDatosVehiculosInvolucrados(int codsin, string placa , string chavehinv ,string marvehinv,string modvehinv ,string cilvehinv,string matpelvehinv,string codser, string codtipve, string seguroPrivado,string danioMaterial, string matriculaVigente,int anivehinv, int codsubtipoVHL)
        {

            bool _seguroPrivado = seguroPrivado == "1" ? true : false;
            bool _danioMaterial = danioMaterial == "1" ? true : false;
            bool _matriculaVigente = matriculaVigente == "1" ? true : false;
            var jsonmensaje = "";
            var jsonResult = ListaVehiculos(0);
            Vehiculo v = new Vehiculo();
            v.placvehinv = placa;
            v.danmatvehinv = _danioMaterial;
            v.matvigvehinv = _matriculaVigente;
            v.chavehinv = chavehinv;
            v.marvehinv = marvehinv;
            v.modvehinv = modvehinv;
            v.cilvehinv = Convert.ToString(cilvehinv);
            v.segprivehinv = _seguroPrivado;
            v.matpelvehinv = matpelvehinv;
            v.anivehinv = anivehinv;
            v.codser = Convert.ToInt32(codser);
            v.codtipve = Convert.ToInt32(codtipve);
            v.codsin = codsin;
            v.codsubtipoVHL = codsubtipoVHL;
            jsonmensaje = objSiniestrosNeg.insertaVehiculosInvolucrados(v);
            if (jsonmensaje != "" && jsonmensaje != "0" && Convert.ToInt32(jsonmensaje) > 0)
            {
                objSiniestrosNeg.ModificarSiniestrosFinProceso(codsin, 2);
                ViewBag.ObtenerListaVheiculos = ListaVehiculos(codsin);
                jsonResult = ListaVehiculos(codsin);
            }
            return Json(jsonResult);
        }
        public JsonResult JsonGuardaSiniestro(string fecsin, string horsin, string latsin, string lonsin, string dirsin, int numfalsin, int numlessin,  string zonsin, string traviasin, string conatmsin, string conviasin, string luzartsin, string desviasin, int limvelsin, string intsin, string matsupviasin, string obsviasin, string lugviasin, string cursin, int numcarsin, string sensin,  int codtipsin, int codpar, string codsubcir, int codcant, int codprov, string codcaupro, string codcaurea, string codcir, string coddis, int codgeo, string latsinGeo, string lonsinGeo)
        {
            var jsonmensaje = "";
            bool _traviasin = false;
            try
            {
                if (codgeo != 0 && latsin.Replace(',','.') == latsinGeo && lonsin.Replace(',', '.') == lonsinGeo)// comparo si  la georeferencia son identicas (ingreso por buesqueda o ingreso manual)
                {
                    latsin = latsinGeo;
                    lonsin = lonsinGeo;
                }

                foreach (var datos in objSiniestrosNeg.buscaCircuitoZona(codprov, codcant, codpar))
                {

                    zonsin = datos.zona.ToString();
                    if (datos.nombre.ToString() == null || datos.nombre.ToString() == "")
                        codcir = "-1";
                    else
                        codcir = datos.nombre.ToString();

                    if (datos.codigo.ToString() == null || datos.codigo.ToString() == "")
                        coddis = "-1";
                    else
                        coddis = datos.codigo.ToString();
                }
                var codUsuario = HttpContext.Session.GetString(SessionKeyCodUsuario);
                string codAutoridad = HttpContext.Session.GetString("codAutoridad");
                if (luzartsin == "SELECCIONAR")
                    luzartsin = "";
                if (intsin == "SELECCIONAR")
                    intsin = "";
                if (traviasin == "SI")
                    _traviasin = true;
                else
                    _traviasin = false;

                jsonmensaje =  objSiniestrosNeg.GuardaSiniestros(Convert.ToDateTime(fecsin), horsin, latsin.Replace(',', '.'), lonsin.Replace(',', '.'), dirsin.ToUpper(), numfalsin,numlessin,1,false,Convert.ToInt32( codUsuario),Convert.ToInt32( codUsuario), zonsin, _traviasin, conatmsin, conviasin, luzartsin, desviasin, limvelsin,intsin,matsupviasin,obsviasin,lugviasin,cursin,numcarsin,sensin,Convert.ToInt32(codUsuario), codAutoridad.ToUpper(), codtipsin,codpar,codsubcir,codcant,codprov,codcaupro,codcaurea,codcir,coddis);
                if (jsonmensaje != "0" && codgeo != 0 && latsin == latsinGeo && lonsin == lonsinGeo)
                    objSiniestrosNeg.ModificarGeoreferenciaSiniestro(Convert.ToInt32(jsonmensaje), codgeo);
            }
            catch (Exception ex)
            {
                jsonmensaje = "0";
            }
           
            return Json(jsonmensaje.ToString());
        }


        public JsonResult JsonModificarSiniestroFinProceso(int codsin)
        {
            var jsonmensaje = "";

            try
            {

                jsonmensaje = objSiniestrosNeg.ModificarSiniestrosFinProceso(codsin,5);
            }
            catch (Exception ex)
            {
                jsonmensaje = "0";
            }

            return Json(jsonmensaje.ToString());
        }

        public JsonResult GuardarDanioTercero(int codsin, string obsdater, int codtipdater)
        {
            // var retorno ="";
            var jsonResult = listaVistaDaniosTerceros(0);
            int _codsin = Convert.ToInt32(codsin);
            string _obsdater = obsdater == null ? "" : obsdater.ToString();
            int _codtipdater = Convert.ToInt32(codtipdater);
            DanioMaterial v = new DanioMaterial();
            v.codsin = _codsin;
            v.codtipdater = _codtipdater;
            v.obsdater = _obsdater.ToString().ToUpper();
             var retorno = objSiniestrosNeg.insertarDaniosTerceros(v);
            if (retorno != "" && retorno != "0" && Convert.ToInt32(retorno) > 0)
            {
                objSiniestrosNeg.ModificarSiniestrosFinProceso(codsin, 4);
                jsonResult = listaVistaDaniosTerceros(codsin);
              //  return Json(jsonResult);
            }
            //else
            //{
            //    jsonResult = "0";
            //}
            return Json(jsonResult);
        }

        public JsonResult GuardarVictimasInvolucradas( string tipidenvicinv,string numidenvicinv,int edavicinv,string genvicinv,string convicinv24, string convicinv30, string tipparvicinv, string casvicinv, string cinvicinv, string posvicinv, string conalcvicinv, int codsin, int codveh, string[] desaccpea,string ccc,string nombre_victima)
        {
            var retorno = "";
            var ccc1 = "";
            ccc1 = ccc;
            bool _casvicinv = casvicinv == "SI" ? true:false;
            bool _cinvicinv = cinvicinv == "SI" ? true : false;
            bool _conalcvicinv = conalcvicinv == "SI" ? true : false;

            var jsonResult = listaVictimasInvolucrados(0);
            Victimas v = new Victimas();
            v.tipidenvicinv = tipidenvicinv;
            v.numidenvicinv = numidenvicinv;
            v.edavicinv = edavicinv;
            v.genvicinv = Convert.ToChar( genvicinv);
            v.convicinv24 = convicinv24;
            v.convicinv30 = "";
            v.tipparvicinv = tipparvicinv;
            v.casvicinv  = _casvicinv;
            v.cinvicinv = _cinvicinv;
            v.posvicinv = posvicinv;
            v.conalcvicinv = _conalcvicinv;
            v.codsin = codsin;
            v.codveh = codveh;
            v.desaccpea = ccc1; //desaccpea == null ? "": desaccpea.ToString();
            v.nombreVictima = nombre_victima == null ? "" : nombre_victima;
            retorno = objSiniestrosNeg.insertarVictimasInvolucradas(v);
            if (retorno != "" && retorno != "0" && Convert.ToInt32(retorno) > 0)
            {
                objSiniestrosNeg.ModificarSiniestrosFinProceso(codsin, 3);
                jsonResult = listaVictimasInvolucrados(codsin);
                //  return Json(jsonResult);
            }
            return Json(jsonResult);
        }
        public JsonResult GuardarAccionesPeaton(string desaccpea, int codvicinv)
        {
            var retorno = "";
            retorno = objSiniestrosNeg.insertarAccionesPeaton(desaccpea, codvicinv);
            return Json(retorno);
        }
        public JsonResult ObtenerTipoVehiculoVictimas(int codVehiculo)
        {
            var retorno = "";
            retorno = objSiniestrosNeg.obtenerTipoVehiculo(codVehiculo);
            return Json(retorno);
        }
        //
        public JsonResult modificaRegistroValidadoParaEstadisticas(int codsin)
        {
            //int supervIn  
            var retorno = "";
            var codUsuario = HttpContext.Session.GetString(SessionKeyCodUsuario);
            retorno = objSiniestrosNeg.modificaRegistroValidadoParaEstadistica(codsin, Convert.ToInt32(codUsuario));
            return Json(retorno);
        }
        //
        public JsonResult CargarCantonesPorProvincia(int codprov)
        {
            var jsonResult = ObtenerlistaCantonesPorProvincia(codprov);

            return Json(jsonResult);
        }

        public JsonResult CargarParroquiasPorCantones(int codcant , int codprov)
        {
            var jsonResult = ObtenerlistaParroquiasPorCantones(codcant, codprov);
            
            return Json(jsonResult);
        }
        public JsonResult ObtenerInformacionVictima(string cedula, string tipoI)
        {
            var jsonResult = ObtenerInformacionVictimas(cedula,Convert.ToInt32( tipoI));

            return Json(jsonResult);
        }

        public JsonResult ObtenerlistaSubTipoVehiculo(int codTipoVhl)
        {
            var jsonResult = ObtenerlistaSubTipoVehiculos(codTipoVhl);

            return Json(jsonResult);
        }

        
        public JsonResult JsonDescargaExcel(int codsin)
        {
            var retorno = "0";
            string nombreArchivo = "";
            string sWebRootFolder = _hostingEnvironment.WebRootPath + "/uploads/";
            try
            {

                nombreArchivo = "Siniestro" + "_" + codsin.ToString() + "_" + DateTime.Now.Year.ToString();
                retorno = DescargaExcel(nombreArchivo, codsin);
                //return File("d","application/xlsx", "nombreArchivo");
            }
            catch (Exception ex)
            {
                sWebRootFolder = _hostingEnvironment.WebRootPath + "/uploads/";
                retorno = sWebRootFolder;//"0";
            }
            retorno = sWebRootFolder;
            return Json(retorno);

        }

       

        public FileResult Download(int id)
        {
            var retorno = "0";//
            string nombreArchivo = "";
            nombreArchivo = "Siniestro" + "_" + id.ToString() + "_" + DateTime.Now.Year.ToString();
            retorno = DescargaExcel(nombreArchivo, id);
            var fileName = nombreArchivo+".xlsx";
            string sWebRootFolder = _hostingEnvironment.WebRootPath + "/uploads";
            //var filepath = $"C:/uploadSin/{fileName}";
            var filepath = sWebRootFolder+"/"+ fileName;
            byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
            return File(fileBytes, "application/x-msdownload", fileName);
        }
        public FileResult DownloadFoto1(string fotprigeo)
        {
            var retorno = "0";//
            string nombreArchivo = "";
            nombreArchivo = fotprigeo.Trim().ToString();
           // retorno = DescargaExcel(nombreArchivo, id);
            var fileName = nombreArchivo;
            string sWebRootFolder = _hostingEnvironment.WebRootPath + "/Siniestros";
            //var filepath = $"C:/uploadSin/{fileName}";
            var filepath = sWebRootFolder + "/" + fileName;
            byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
            return File(fileBytes, "application/x-msdownload", fileName);
        }

        public FileResult DownloadFoto2(string fotsegeo)
        {
            var retorno = "0";//
            string nombreArchivo = "";
            nombreArchivo = fotsegeo.Trim().ToString();
            //retorno = DescargaExcel(nombreArchivo, id);
            var fileName = nombreArchivo;
            string sWebRootFolder = _hostingEnvironment.WebRootPath + "/Croquis";
            //var filepath = $"C:/uploadSin/{fileName}";
            var filepath = sWebRootFolder + "/" + fileName;
            byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
            return File(fileBytes, "application/x-msdownload", fileName);
        }
        public FileResult DownloadFoto1C(string id)
        {
            var retorno = "0";//
            string nombreArchivo = "";
            nombreArchivo = id.Trim().ToString();
            // retorno = DescargaExcel(nombreArchivo, id);
            var fileName = nombreArchivo;
            string sWebRootFolder = _hostingEnvironment.WebRootPath + "/Siniestros";
            //var filepath = $"C:/uploadSin/{fileName}";
            var filepath = sWebRootFolder + "/" + fileName;
            byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
            return File(fileBytes, "application/x-msdownload", fileName);
        }
        public FileResult DownloadFoto2C(string id)
        {
            var retorno = "0";//
            string nombreArchivo = "";
            nombreArchivo = id.Trim().ToString();
            //retorno = DescargaExcel(nombreArchivo, id);
            var fileName = nombreArchivo;
            string sWebRootFolder = _hostingEnvironment.WebRootPath + "/Croquis";
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


        public JsonResult ObtenerDatosWSRC(string numeroIdentificacion)
        {

            DatosWSRegistroCivil datos = new DatosWSRegistroCivil();
            List<DatosWSRegistroCivil> listDatos = new List<DatosWSRegistroCivil>();
            var jsonResult = objSiniestrosNeg.ConsultaDatisWsRegistroCivil(numeroIdentificacion);
            var res = jsonResult.Result.ToArray();
            if (res[0].codigoPaquete != "0")
            {
                foreach (var d in res)
                {
                    datos.codigoPaquete = d.codigoPaquete;
                    datos.edad =d.edad;
                    datos.sexo = d.sexo;
                    datos.nombre = d.nombre;
                    datos.fechaNacimiento =d.fechaNacimiento;
                    listDatos.Add(datos);
                }
            }

            return Json(listDatos);
        }



        public class Ciudad
        {
            public int cod { get; set; }
            public string opcion { get; set; }
        }
        
        public List<SelectListItem> ObtenerlistaCiruitos(int codCant, int codPar)
        {
            return objSiniestrosNeg.listaCiruitos( codCant,  codPar);
        }
        public List<SelectListItem> ObtenerlistaTodosCiruitos()
        {
            return objSiniestrosNeg.listaTodosCiruitos();
        }
        public List<CargaDropDownList> ObtenerlistaDistrito(int codProv, int codCant,int codpar)
        {
            return objSiniestrosNeg.listaDistritos( codProv,  codCant,codpar);
        }
        public List<SelectListItem> ObtenerlistaTodosDistritos()
        {
            return objSiniestrosNeg.listaTodosDistritos();
        }

        public List<SelectListItem> ObtenerlistatipoSiniestros()
        {
            return objSiniestrosNeg.listatipoSiniestros();
        }
        public List<SelectListItem> ObtenerlistaCausaProbableSiniestros()
        {
            return objSiniestrosNeg.listaCausaProbableSiniestros();
        }
        public List<SelectListItem> ObtenerlistaCausaRealSiniestros()
        {
            return objSiniestrosNeg.listaCausaRealSiniestros();
        }

        public List<SelectListItem> ObtenerlistaTipoDaniosTerceros()
        {
            return objSiniestrosNeg.listaTipoDaniosTerceros();
        }
        public List<SelectListItem> ObtenerlistaServiciosVehiculos()
        {
            return objSiniestrosNeg.listaServiciosVehiculos();
        }

        public List<SelectListItem> ObtenerListaTipoVehiculo()
        {
            return objSiniestrosNeg.listaTipoVehiculos();
        }
        public List<SelectListItem> ObtenerListadoTipoZona()
        {
            return objSiniestrosNeg.listaDatosTipoZona();
        }

        public List<SelectListItem> ObtenerListadoCondicionAtmosferica()
        {
            return objSiniestrosNeg.listadatosCondicionAtmosferica();
        }
        public List<SelectListItem> ObtenerlistadatosCondicionVia()
        {
            return objSiniestrosNeg.listadatosCondicionVia();
        }
        public List<SelectListItem> ObtenerlistadatosLuzArtificial()
        {
            return objSiniestrosNeg.listadatosLuzArtificial();
        }
        public List<SelectListItem> ObtenerlistadatosTipoVia()
        {
            return objSiniestrosNeg.listadatosTipoVia();
        }
        public List<SelectListItem> ObtenerlistadatosLimiteVelocidad()
        {
            return objSiniestrosNeg.listadatosLimiteVelocidad();
        }
        public List<SelectListItem> ObtenerlistadatosControlInterseccion()
        {
            return objSiniestrosNeg.listadatosControlInterseccion();
        }
        public List<SelectListItem> ObtenerlistadatosMaterialSuperficieVia()
        {
            return objSiniestrosNeg.listadatosMaterialSuperficieVia();
        }
        public List<SelectListItem> ObtenerlistadatosObstaculoVia()
        {
            return objSiniestrosNeg.listadatosObstaculoVia();
        }
        public List<SelectListItem> ObtenerlistadatosLugarVia()
        {
            return objSiniestrosNeg.listadatosLugarVia();
        }
        public List<SelectListItem> ObtenerlistadatosNumeroCarriles()
        {
            return objSiniestrosNeg.listadatosNumeroCarriles();
        }
        public List<SelectListItem> ObtenerlistadatosSenialitica()
        {
            return objSiniestrosNeg.listadatosSenialitica();
        }
        public List<SelectListItem> ObtenerlistadatosCurvaExistente()
        {
            return objSiniestrosNeg.listadatosCurvaExistente();
        }
        public List<SelectListItem> ObtenerlistadatosTransporteMaterialPeligroso()
        {
            return objSiniestrosNeg.listadatosTransporteMaterialPeligroso();
        }

        /*******************************************
         * DATOS VICTIMAS
         * *****************************************/
        public List<SelectListItem> ObtenerlistadatosTipoIdentificacion()
        {
            return objSiniestrosNeg.listadatosTipoIdentificacion();
        }
        public List<SelectListItem> ObtenerlistadatosSexo()
        {
            return objSiniestrosNeg.listadatosSexo();
        }
        public List<SelectListItem> ObtenerlistadatosCondicionVictimas24()
        {
            return objSiniestrosNeg.listadatosCondicionVictimas24();
        }
        public List<SelectListItem> ObtenerlistadatosCondicionVictimas30()
        {
            return objSiniestrosNeg.listadatosCondicionVictimas30();
        }
        public List<SelectListItem> ObtenerlistadatosTipoParticipante()
        {
            return objSiniestrosNeg.listadatosTipoParticipante();
        }
        public List<SelectListItem> ObtenerlistadatosPosicionPlaza()
        {
            return objSiniestrosNeg.listadatosPosicionPlaza();
        }

        public List<SelectListItem> ObtenerlistadatosAccionesPeaton()
        {
            return objSiniestrosNeg.listadatosAccionesPeaton();
        }
        public List<SelectListItem> ObtenerlistadatosValoresSiNo()
        {
            return objSiniestrosNeg.listadatosSiNo();
        }

        
        public List<SelectListItem> ObtenerlistaPeatones(int codsin)
        {
            return objSiniestrosNeg.listadatosPeatones(codsin);
        }
        public List<SelectListItem> ObtenerlistadaVehiculosInvolucrados(int codsin)
        {
            return objSiniestrosNeg.listadaVehiculosInvolucrados(codsin);
        }

        public List<SelectListItem> ObtenerlistaCantonesPorProvincia(int codprov)
        {
            return objSiniestrosNeg.listaCantones(codprov);
        }
        public List<SelectListItem> ObtenerlistaParroquiasPorCantones(int codcant, int codprov)
        {
            return objSiniestrosNeg.listaParroquiasPorCantones(codcant, codprov);
        }
        public List<SelectListItem> ObtenerlistaParroquias()
        {
            return objSiniestrosNeg.listarTodasParroquias();
        }
        
        public List<SelectListItem> TraerNumLesionadosFallecidos(int codsin)
        {
            return objSiniestrosNeg.TraerNumLesionadosFallecidos(codsin);
        }

        public List<Vehiculo> ListaVehiculos(int codsin)
        {
            return objSiniestrosNeg.ListaVehiculos(codsin);
        }
        public List<Victimas> listaVictimasInvolucrados(int codsin)
        {
            return objSiniestrosNeg.listaVictimasInvolucrados(codsin);
        }
        public List<AccionesPeaton> listaVistaAccionesPeatones(int codsin)
        {
            return objSiniestrosNeg.listaVistaAccionesPeatones(codsin);
        }
        public List<DanioMaterial> listaVistaDaniosTerceros(int codsin)
        {
            return objSiniestrosNeg.listaVistaDaniosTerceros(codsin);
        }

        public List<Modelo.Entity.Siniestro> listaVistaSiniestros(int codsin)
        {
            return objSiniestrosNeg.listaVistaSiniestros(codsin);
        }
        public List<Georeferencias> listaGeoreferencias(string codaut)
        {
            return objSiniestrosNeg.listaGeoreferencias(codaut);
        }
        public List<Georeferencias> listaGeoreferenciasMovil(int codusuario)
        {
            return objSiniestrosNeg.listaGeoreferenciasMovil(codusuario);
        }
        
        //
        public List<Vehiculo> ObtenerPlacaVhl(string parametro, string opcion)
        {
            return objSiniestrosNeg.ObtenerPlacaVhl(parametro,Convert.ToInt32( opcion));
        }
        public List<Victimas> ObtenerInformacionVictimas(string cedula, int tipoI)
        {
            return objSiniestrosNeg.ObtenerInformacionVictima(cedula, tipoI);
        }

        public List<SelectListItem> ObtenerlistaSubTipoVehiculos(int codTipoVhl)
        {
            return objSiniestrosNeg.listaSubTipoVehiculos(codTipoVhl);
        }


        public List<Modelo.Entity.Siniestro> ObtenertarerInformacionSiniestroProcerso(int codsin)
        {
            return objSiniestrosNeg.tarerInformacionSiniestroProcerso(codsin);
        }
        

        /*******************************************
         * DESCGAR ARCHIVO EXCEL
         * ************************************/

        [HttpGet]
        [Route("Export")]
        public string DescargaExcel(string sFileName, int codsin)
        {
            string retorno = "1";
           
        string sWebRootFolder =  _hostingEnvironment.WebRootPath + "/uploads/";//"C:\\uploadSin";//
            sFileName = sFileName +"."+"xlsx";
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
                    worksheet.Cells[1, 5].Value = "Longuitud";
                    worksheet.Cells[1, 6].Value = "Provincia";
                    worksheet.Cells[1, 7].Value = "Canton";
                    worksheet.Cells[1, 8].Value = "Parroquia";
                    worksheet.Cells[1, 9].Value = "Distrito";
                    worksheet.Cells[1, 10].Value = "Circuito";
                    worksheet.Cells[1, 11].Value = "Zona";
                    worksheet.Cells[1, 12].Value = "DireccionSiniestro";
                    worksheet.Cells[1, 13].Value = "#Fallecidos";
                    worksheet.Cells[1, 14].Value = "#Lesionados";
                    worksheet.Cells[1, 15].Value = "CondicionAtmosferica";
                    worksheet.Cells[1, 16].Value = "CondicionVia";
                    worksheet.Cells[1, 17].Value = "TipoVia";
                    worksheet.Cells[1, 18].Value = "LimiteVelocidad";
                    worksheet.Cells[1, 19].Value = "TrabajosVia";
                    worksheet.Cells[1, 20].Value = "#Carriles";
                    worksheet.Cells[1, 21].Value = "MaterialSuperfice";
                    worksheet.Cells[1, 22].Value = "ControlInterseccion";
                    worksheet.Cells[1, 23].Value = "ObtsaculosVia";
                    worksheet.Cells[1, 24].Value = "LugarVia";
                    worksheet.Cells[1, 25].Value = "CurvaExistente";
                    worksheet.Cells[1, 26].Value = "SeñalizacionExistente";
                    worksheet.Cells[1, 27].Value = "TipoSiniestro";
                    worksheet.Cells[1, 28].Value = "CausaProbable";
                    worksheet.Cells[1, 29].Value = "LuzArtificial";

                    // busco y recorrro el siniestro a buscar
                    foreach (var d in objSiniestrosNeg.listaVistaSiniestros(codsin))
                    {
                        // coloco los valores en los campos correspondientes
                        worksheet.Cells["A2"].Value = d.codsin.ToString();
                        worksheet.Cells["B2"].Value = d.fecsin;
                        worksheet.Cells["C2"].Value = d.horsin.ToString();
                        worksheet.Cells["D2"].Value = d.latsin;
                        worksheet.Cells["E2"].Value = d.lonsin;
                        worksheet.Cells["F2"].Value = d.provincia;
                        worksheet.Cells["G2"].Value = d.canton;
                        worksheet.Cells["H2"].Value = d.parroquia;
                        worksheet.Cells["I2"].Value = d.distrito;
                        worksheet.Cells["J2"].Value = d.circuito;
                        worksheet.Cells["K2"].Value = d.zonsin;
                        worksheet.Cells["L2"].Value = d.dirsin;
                        worksheet.Cells["M2"].Value = d.numfalsin;
                        worksheet.Cells["N2"].Value = d.numlessin;
                        worksheet.Cells["O2"].Value = d.conatmsin;
                        worksheet.Cells["P2"].Value = d.conviasin;
                        worksheet.Cells["Q2"].Value = d.desviasin;
                        worksheet.Cells["R2"].Value = d.limvelsin;
                        worksheet.Cells["S2"].Value = d.traviasin == true ? "SI" : "NO";
                        worksheet.Cells["T2"].Value = d.numcarsin;
                        worksheet.Cells["U2"].Value = d.matsupviasin;
                        worksheet.Cells["V2"].Value = d.intsin;
                        worksheet.Cells["W2"].Value = d.obsviasin;
                        worksheet.Cells["X2"].Value = d.lugviasin;
                        worksheet.Cells["Y2"].Value = d.cursin;
                        worksheet.Cells["Z2"].Value = d.sensin;
                        worksheet.Cells["AA2"].Value = d.tiposiniestro;
                        worksheet.Cells["AB2"].Value = d.causa_probable;
                        worksheet.Cells["AC2"].Value = d.luzartsin== "SELECCIONAR" ?"": d.luzartsin;

                    }

                    // añado el  2do sheet de vehiculos
                    excel.ExcelWorksheet worksheet1 = package.Workbook.Worksheets.Add("Vehiculos");
                    //coloco las cabeceras del primer sheet
                    worksheet1.Cells[1, 1].Value = "#Placa";
                    worksheet1.Cells[1, 2].Value = "DanioMaterial";
                    worksheet1.Cells[1, 3].Value = "MatriculaVigente";
                    worksheet1.Cells[1, 4].Value = "Chasis";
                    worksheet1.Cells[1, 5].Value = "Marca";
                    worksheet1.Cells[1, 6].Value = "Modelo";
                    worksheet1.Cells[1, 7].Value = "Año";
                    worksheet1.Cells[1, 8].Value = "Cilindraje";
                    worksheet1.Cells[1, 9].Value = "SeguroPrivado";
                    worksheet1.Cells[1, 10].Value = "MaterialPeligroso";
                    worksheet1.Cells[1, 11].Value = "TipoServicio";
                    worksheet1.Cells[1, 12].Value = "TipoVehiculo";

                    int fila = 1;
                    // busco y recorrro el vehiculo a buscar
                    foreach (var d in objSiniestrosNeg.ListaVehiculos(codsin))
                    {
                        fila++;
                        // coloco los valores en los campos correspondientes
                        worksheet1.Cells["A" + "" + fila].Value = d.placvehinv.ToString();
                        worksheet1.Cells["B" + "" + fila].Value = d.danmatvehinv == true ? "SI" : "NO";
                        worksheet1.Cells["C" + "" + fila].Value = d.matvigvehinv == true ? "SI" : "NO"; ;
                        worksheet1.Cells["D" + "" + fila].Value = d.chavehinv;
                        worksheet1.Cells["E" + "" + fila].Value = d.marvehinv;
                        worksheet1.Cells["F" + "" + fila].Value = d.modvehinv;
                        worksheet1.Cells["G" + "" + fila].Value = d.anivehinv.ToString();
                        worksheet1.Cells["H" + "" + fila].Value = d.cilvehinv.ToString();
                        worksheet1.Cells["I" + "" + fila].Value = d.segprivehinv == true ? "SI" : "NO";
                        worksheet1.Cells["J" + "" + fila].Value = d.matpelvehinv;
                        worksheet1.Cells["K" + "" + fila].Value = d.desser;
                        worksheet1.Cells["L" + "" + fila].Value = d.destipveh;
                        
                    }

                    // añado el  3er sheet de victimas
                    excel.ExcelWorksheet worksheet2 = package.Workbook.Worksheets.Add("Victimas");
                    //coloco las cabeceras del primer sheet
                    worksheet2.Cells[1, 1].Value = "TipoIdentificacion";
                    worksheet2.Cells[1, 2].Value = "#Indentificacion";
                    worksheet2.Cells[1, 3].Value = "Edad";
                    worksheet2.Cells[1, 4].Value = "Sexo";
                    worksheet2.Cells[1, 5].Value = "CondicionVictima24h";
                    worksheet2.Cells[1, 6].Value = "CondicionVictima30d";
                    worksheet2.Cells[1, 7].Value = "TipoParticipante";
                    worksheet2.Cells[1, 8].Value = "UsoCasco";
                    worksheet2.Cells[1, 9].Value = "UsoCinturon";
                    worksheet2.Cells[1, 10].Value = "PosicionPlaza";
                    worksheet2.Cells[1, 11].Value = "SospechaConsumoAlcohol";
                    worksheet2.Cells[1, 12].Value = "#PlacaVehiculo";

                    int fila1 = 1;
                    // busco y recorrro la victima a buscar
                    foreach (var d in objSiniestrosNeg.listaVictimasInvolucrados(codsin))
                    {
                        fila1++;
                        // coloco los valores en los campos correspondientes
                        worksheet2.Cells["A" + "" + fila1].Value = d.tipidenvicinv.ToString();
                        worksheet2.Cells["B" + "" + fila1].Value = d.numidenvicinv.ToString() ;
                        worksheet2.Cells["C" + "" + fila1].Value = d.edavicinv.ToString() ;
                        worksheet2.Cells["D" + "" + fila1].Value = d.sexo;
                        worksheet2.Cells["E" + "" + fila1].Value = d.convicinv24;
                        worksheet2.Cells["F" + "" + fila1].Value = d.convicinv30;
                        worksheet2.Cells["G" + "" + fila1].Value = d.tipparvicinv.ToString();
                        worksheet2.Cells["H" + "" + fila1].Value = d.USO_CASO.ToString();
                        worksheet2.Cells["I" + "" + fila1].Value = d.USO_CINTU;
                        worksheet2.Cells["J" + "" + fila1].Value = d.posvicinv;
                        worksheet2.Cells["K" + "" + fila1].Value = d.CONS_ALCOHOL;
                        worksheet2.Cells["L" + "" + fila1].Value = d.PLACAVHL;

                    }
                    // añado el  4er sheet de acciones peaton
                    excel.ExcelWorksheet worksheet3 = package.Workbook.Worksheets.Add("AccionesPeaton");
                    //coloco las cabeceras del primer sheet
                    worksheet3.Cells[1, 1].Value = "#Indetificacion";
                    worksheet3.Cells[1, 2].Value = "AccionPeaton";
                    worksheet3.Cells[1, 3].Value = "CondicionVictima24h";
                    worksheet3.Cells[1, 4].Value = "#PlacaVehiculo";

                    int fila2 = 1;
                    // busco y recorrro la victima a buscar
                    foreach (var d in objSiniestrosNeg.listaVistaAccionesPeatones(codsin))
                    {
                        fila2++;
                        // coloco los valores en los campos correspondientes
                        worksheet3.Cells["A" + "" + fila2].Value = d.numidenvicinv.ToString();
                        worksheet3.Cells["B" + "" + fila2].Value = d.desaccpea.ToString();
                        worksheet3.Cells["C" + "" + fila2].Value = d.convicinv24.ToString();
                        worksheet3.Cells["D" + "" + fila2].Value = d.placvehinv;
                        

                    }

                    // añado el  5to sheet de danios a terceros
                    excel.ExcelWorksheet worksheet4 = package.Workbook.Worksheets.Add("DaniosTerceros");
                    //coloco las cabeceras del primer sheet
                    worksheet4.Cells[1, 1].Value = "TipoDaño";
                    worksheet4.Cells[1, 2].Value = "Observaciones";
                    worksheet4.Cells[1, 3].Value = "#Siniestro";
                    

                    int fila3 = 1;
                    // busco y recorrro la victima a buscar
                    foreach (var d in objSiniestrosNeg.listaVistaDaniosTerceros(codsin))
                    {
                        fila3++;
                        // coloco los valores en los campos correspondientes
                        worksheet4.Cells["A" + "" + fila3].Value = d.destipdater.ToString();
                        worksheet4.Cells["B" + "" + fila3].Value = d.obsdater.ToString();
                        worksheet4.Cells["C" + "" + fila3].Value = d.codsin.ToString();
                        


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

        public static decimal ConvertirEnDecimal(string valor)
        {
            decimal retorno = 0;

            if (!string.IsNullOrEmpty(valor.Trim()))
            {
                NumberFormatInfo nfi = new CultureInfo("en-US").NumberFormat;
                nfi.NumberDecimalSeparator = ".";

                try
                {
                    retorno = Convert.ToDecimal(valor, nfi);
                }
                catch
                {
                }
            }

            return retorno;
        }

        public string GetWSObject()
        {
            string resultado = "";
            XNamespace ns = "http://schemas.xmlsoap.org/soap/envelope/";
            XNamespace myns = "http://mynamespace.com";

            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            XNamespace xsd = "http://www.w3.org/2001/XMLSchema";

            XDocument soapRequest = new XDocument(
                new XDeclaration("1.0", "UTF-8", "no"),
                new XElement(ns + "Envelope",
                    new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                    new XAttribute(XNamespace.Xmlns + "xsd", xsd),
                    new XAttribute(XNamespace.Xmlns + "soap", ns),
                    new XElement(ns + "Body",
                        new XElement(myns + "getFichaGeneral",
                            new XElement(myns + "client",
                                new XElement(myns + "Username", "iNtrAdRANt"),
                                new XElement(myns + "Password", "j2x43$uE!4")),
                            new XElement(myns + "codigoPaquete", "117"),
                            new XElement(myns + "numeroIdentificacion", "1719690651")
                        )
                    )
                ));
            try
            {
                using (var client = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip }) )
                {
                    var request = new HttpRequestMessage()
                    {
                        RequestUri = new Uri("http://interoperabilidad.dinardap.gob.ec:7979/interoperador?wsdl"),
                        Method = HttpMethod.Post
                    };

                    request.Content = new StringContent(soapRequest.ToString(), Encoding.UTF8, "text/xml");

                    request.Headers.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("text/xml");
                    request.Headers.Add("SOAPAction", "http://servicio.interoperabilidadws.interoperacion.dinardap.gob.ec/");

                    HttpResponseMessage response = client.SendAsync(request).Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception();
                    }

                    Task<Stream> streamTask = response.Content.ReadAsStreamAsync();
                    Stream stream = streamTask.Result;
                    var sr = new StreamReader(stream);
                    var soapResponse = XDocument.Load(sr);
                    Console.WriteLine(soapResponse);
                    string getFichaGeneralResponse = "";
                    var xml = soapResponse.Descendants(myns + "getFichaGeneralResponse").FirstOrDefault().ToString();
                    resultado = xml;
                    // var purchaseOrderResult = Deserialize<getFichaGeneralResponse>(xml);
                }
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException is TaskCanceledException)
                {
                    throw ex.InnerException;
                    resultado = ex.ToString();
                }
                else
                {
                    throw ex;
                    resultado = ex.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
                resultado = ex.ToString();
            }
            return  resultado;
        }
        public static T Deserialize<T>(string xmlStr)
        {
            var serializer = new XmlSerializer(typeof(T));
            T result;
            using (TextReader reader = new StringReader(xmlStr))
            {
                result = (T)serializer.Deserialize(reader);
            }
            return result;
        }

        private  async Task ProcessRepositories()
        {
            string res = "";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/xml"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var stringTask = client.GetStringAsync("http://interoperabilidad.dinardap.gob.ec:7979/interoperador?wsdl");

            var msg = await stringTask;
            res = msg;
          //  return res;
            Console.Write(msg);
        }

        public class AuthCallout
        {

           
        }
    }
}
