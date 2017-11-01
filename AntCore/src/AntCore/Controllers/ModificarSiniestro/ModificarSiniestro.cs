using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Modelo.Entity;
using Modelo.Negocios;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AntCore.Controllers.ModificarSiniestro
{

   
    public class ModificarSiniestro : Controller
    {

        const string SessionKeyCodUsuario = "_CodUsuario";
        SiniestroNegocio objSiniestrosNeg;
        public ModificarSiniestro()
        {
            objSiniestrosNeg = new SiniestroNegocio();

        }
        // GET: /<controller>/
        public IActionResult Index(int id)
        {
            Modelo.Entity.Siniestro obj = new Modelo.Entity.Siniestro(); 
            ViewBag.ObtenerListaVheiculos= ListaVehiculos(id);
            ViewBag.ObtenerListaVictimas = listaVictimasInvolucrados(id);
            ViewBag.ObtenerListaTipoVehiculo = ObtenerListaTipoVehiculo();
            ViewBag.ObtenerlistadatosTransporteMaterialPeligroso = ObtenerlistadatosTransporteMaterialPeligroso();
            ViewBag.ObtenerlistaServiciosVehiculos = ObtenerlistaServiciosVehiculos();
            ViewBag.ObtenerlistadatosTipoIdentificacion = ObtenerlistadatosTipoIdentificacion();
            ViewBag.ObtenerlistadatosSexo = ObtenerlistadatosSexo();
            ViewBag.ObtenerlistadatosCondicionVictimas24 = ObtenerlistadatosCondicionVictimas24();
            ViewBag.ObtenerlistadatosCondicionVictimas30 = ObtenerlistadatosCondicionVictimas30();
            ViewBag.ObtenerlistadatosTipoParticipante = ObtenerlistadatosTipoParticipante();
            ViewBag.ObtenerlistadatosPosicionPlaza = ObtenerlistadatosPosicionPlaza();
            ViewBag.ObtenerlistadaVehiculosInvolucrados = ObtenerlistadaVehiculosInvolucrados(id);
            ViewBag.listaVistaAccionesPeatones = listaVistaAccionesPeatones(id);
            ViewBag.ObtenerlistadatosAccionesPeaton = ObtenerlistadatosAccionesPeaton();
            ViewBag.ObtenerlistaPeatones = ObtenerlistaPeatones(id);
            ViewBag.ObtenerlistaVistaDaniosTerceros = listaVistaDaniosTerceros(id);
            ViewBag.ObtenerlistaTipoDaniosTerceros = ObtenerlistaTipoDaniosTerceros();
            ViewBag.listaProvincias = objSiniestrosNeg.listaProvinciasEdit();
            ViewBag.ObtenerlistatipoSiniestros = ObtenerlistatipoSiniestros();
            ViewBag.ObtenerlistaCausaProbableSiniestros = ObtenerlistaCausaProbableSiniestros();
            ViewBag.ObtenerlistaCausaRealSiniestros = ObtenerlistaCausaRealSiniestros();
            ViewBag.ObtenerlistadatosValoresSiNo = ObtenerlistadatosValoresSiNo();
            var sin = listaSiniestrosPorCodigo(id);
            foreach (var d in sin)
            {
                obj.codprov = d.codprov;
                obj.codcant = d.codcant;
                obj.codpar = d.codpar;
                obj.codestprocsin = d.codestprocsin;

            }
            ViewBag.ObtenerlistaCantonesPorProvincia = ObtenerlistaCantonesPorProvincia(obj.codprov);
            ViewBag.ObtenerlistaParroquiasPorCantones = ObtenerlistaParroquiasPorCantones(Convert.ToInt32(obj.codcant),obj.codprov );
            ViewBag.ObtenerlistaCiruitos = ObtenerlistaCiruitos(Convert.ToInt32(obj.codcant),Convert.ToInt32(obj.codpar));
            ViewBag.ObtenerlistaDistrito = ObtenerlistaDistrito(Convert.ToInt32(obj.codprov), Convert.ToInt32(obj.codcant));

            ViewBag.CodigoSiniestroFind = id.ToString();
            ViewBag.codigoProcesoSin = obj.codestprocsin.ToString();

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
            ViewBag.ObtenerlistadatosCurvaExistente = ObtenerlistadatosCurvaExistente();
            ViewBag.ObtenerlistaTodosSubTipoVehiculos = ObtenerlistaTodosSubTipoVehiculo();
            objSiniestrosNeg.ModificarSiniestrosNumLesionadosFallecidos(id, "", 1);
            objSiniestrosNeg.ModificarSiniestrosNumLesionadosFallecidos(id, "", 2);
            return View();
        }
        public IActionResult Edit(int id)
        {
            var jsonResult = ListaVehiculosPorCodigo(id);
            return Json(jsonResult);

        }
     
        public JsonResult JsonDatosSiniestroPorCodigo(int id)
        {
            var jsonResult = listaSiniestrosPorCodigo(id);
            return Json(jsonResult);
        }
        public JsonResult JsonDatosVhlPorCodigo(int id)
        {
            var jsonResult = ListaVehiculosPorCodigo(id);
            return Json(jsonResult);
        }
        public JsonResult JsonDatosVictimasPorCodigo(int id)
        {
            var jsonResult = objSiniestrosNeg.listaVictimasInvolucradosPorCodigo(id);
            return Json(jsonResult);
        }

        public JsonResult JsonAccionesPeatonPorCodigo(int id)
        {
            var jsonResult = objSiniestrosNeg.listaVistaAccionesPeatonesPorCodigo(id);
            return Json(jsonResult);
        }//
        public JsonResult JsonlistaVistaDaniosTercerosPorCodigo(int id)
        {
            var jsonResult = objSiniestrosNeg.listaVistaDaniosTercerosPorCodigo(id);
            return Json(jsonResult);
        }
        public JsonResult ObtenerTipoVehiculoVictimas(int codVehiculo)
        {
            var retorno = "";
            retorno = objSiniestrosNeg.obtenerTipoVehiculo(codVehiculo);
            return Json(retorno);
        }
        public JsonResult ObtenerlistaSubTipoVehiculo(int codTipoVhl)
        {
            var jsonResult = ObtenerlistaSubTipoVehiculos(codTipoVhl);

            return Json(jsonResult);
        }

        public List<SelectListItem> ObtenerlistaTodosSubTipoVehiculo()
        {
            return objSiniestrosNeg.listaTodosSubTipoVehiculos();
        }
        public List<SelectListItem> ObtenerlistaSubTipoVehiculos(int codTipoVhl)
        {
            return objSiniestrosNeg.listaSubTipoVehiculos(codTipoVhl);
        }
        public List<Vehiculo> ListaVehiculos(int codsin)
        {
             return objSiniestrosNeg.ListaVehiculos(codsin);
        }

        public List<Vehiculo> ListaVehiculosPorCodigo(int codVhl)
        {
            return objSiniestrosNeg.ListaVehiculosPorCodigo(codVhl);
        }
        public List<SelectListItem> ObtenerListaTipoVehiculo()
        {
            return objSiniestrosNeg.listaTipoVehiculos();
        }
        public List<SelectListItem> ObtenerlistadatosTransporteMaterialPeligroso()
        {
            return objSiniestrosNeg.listadatosTransporteMaterialPeligroso();
        }
        public List<SelectListItem> ObtenerlistaServiciosVehiculos()
        {
            return objSiniestrosNeg.listaServiciosVehiculos();
        }
        public List<SelectListItem> ObtenerlistadatosValoresSiNo()
        {
            return objSiniestrosNeg.listadatosSiNo();
        }
        public JsonResult JsonModificarDatosVehiculosInvolucrados(int codVhl, string placa, string chavehinv, string marvehinv, string modvehinv, string cilvehinv, string matpelvehinv, string codser, string codtipve, string seguroPrivado, string danioMaterial, string matriculaVigente, int anivehinv, int codsubtipoVhl)
        {
            var jsonmensaje = "";
            var jsonResult = "0";// ListaVehiculos(0);
            Vehiculo v = new Vehiculo();
            v.placvehinv = placa;
            v.danmatvehinv = danioMaterial == "1" ? true: false ;
            v.matvigvehinv = matriculaVigente == "1" ? true: false;
            v.chavehinv = chavehinv;
            v.marvehinv = marvehinv;
            v.modvehinv = modvehinv;
            v.cilvehinv = Convert.ToString(cilvehinv);
            v.segprivehinv = seguroPrivado == "1" ? true : false; 
            v.matpelvehinv = matpelvehinv;
            v.anivehinv = anivehinv;
            v.codser = Convert.ToInt32(codser);
            v.codtipve = Convert.ToInt32(codtipve);
            v.codvehinv = codVhl;
            v.codsubtipoVHL = codsubtipoVhl;
            jsonmensaje = objSiniestrosNeg.ModificarVehiculosInvolucrados(v);
            if (jsonmensaje != "" && jsonmensaje != "0" && Convert.ToInt32(jsonmensaje) > 0)
            {
                jsonResult = v.codvehinv.ToString();
               // ViewBag.ObtenerListaVheiculos = ListaVehiculos(v.codsin);
               //   jsonResult = ListaVehiculosPorCodigo(v.codvehinv);
            }
            return Json(jsonResult);
            
        }

        [HttpGet]
        public ActionResult cargaVhlEdit(int codsin)
        {


            ViewBag.ObtenerListaVheiculos = ListaVehiculos(codsin);




            return Json(ViewBag.ObtenerListaVheiculos = ListaVehiculos(codsin));

        }
        [HttpGet]
        public ActionResult cargaVictimasEdit(int codsin)
        {

            return Json(ViewBag.ObtenerListaVheiculos = listaVictimasInvolucrados(codsin));

        }

        public ActionResult cargaDaniosEdit(int codsin)
        {

            return Json(ViewBag.ObtenerlistaVistaDaniosTerceros = listaVistaDaniosTerceros(codsin));

        }
        


        public JsonResult JsonModificaVictimasInvolucradas(int codVictima,string tipidenvicinv, string numidenvicinv, int edavicinv, string genvicinv, string convicinv24, string convicinv30, string tipparvicinv, string casvicinv, string cinvicinv, string posvicinv, string conalcvicinv, int codveh, string[] desaccpea, string ccc, int codSiniestro,string nombreVictima)
        {
            var retorno = "";
            var ccc1 = "";
            ccc1 = ccc;
            var jsonResult = listaVictimasInvolucrados(0);
            Victimas v = new Victimas();
            v.codsin = codSiniestro;
            v.tipidenvicinv = tipidenvicinv;
            v.numidenvicinv = numidenvicinv;
            v.edavicinv = edavicinv;
            v.genvicinv = Convert.ToChar(genvicinv);
            v.convicinv24 = convicinv24;
            v.convicinv30 = "";
            v.tipparvicinv = tipparvicinv;
            v.casvicinv = casvicinv == "1" ? true :false;
            v.cinvicinv = cinvicinv == "1" ? true : false; 
            v.posvicinv = posvicinv;
            v.conalcvicinv = conalcvicinv == "1" ? true : false; ;
            v.codvicinv = codVictima;
            v.codveh = codveh;
            v.desaccpea = ccc1 == null ? "" : ccc1.ToString();
            v.nombreVictima = nombreVictima;
            retorno = objSiniestrosNeg.ModificaVictimasInvolucradas(v);
            if (retorno != "" && retorno != "0" && Convert.ToInt32(retorno) > 0)
            {
                jsonResult = objSiniestrosNeg.listaVictimasInvolucradosPorCodigo(v.codvicinv);
            }
            return Json(jsonResult);
        }
        public JsonResult JsonModificarAccionesPeaton(int codaccion, string desaccpea, int codvicinv)
        {
            var jsonResult = objSiniestrosNeg.listaVistaAccionesPeatonesPorCodigo(0);
            
            var retorno = objSiniestrosNeg.ModificarAccionesPeaton(codaccion, desaccpea, codvicinv);

            if (retorno != "0")
            {
                jsonResult = objSiniestrosNeg.listaVistaAccionesPeatonesPorCodigo(Convert.ToInt32(codaccion));
            }
            return Json(jsonResult);
        }
        public JsonResult JsonModificarDaniosTerceros(int codDanio, string obsdater, int codtipdater)
        {
            var jsonResult = objSiniestrosNeg.listaVistaDaniosTercerosPorCodigo(0);
            DanioMaterial d = new DanioMaterial();
            d.coddater = codDanio;
            d.obsdater = obsdater == null ? "" : obsdater.ToUpper();
            d.codtipdater = codtipdater;
            var retorno = objSiniestrosNeg.ModificarDaniosTerceros(d);

            if (retorno != "0")
            {
                jsonResult = objSiniestrosNeg.listaVistaDaniosTercerosPorCodigo(Convert.ToInt32(codDanio));
            }
            return Json(jsonResult);
        }

        public JsonResult JsonModificarSiniestro(int codsin,string fecsin, string horsin, string latsin, string lonsin, string dirsin, int numfalsin, int numlessin, string zonsin, string traviasin, string conatmsin, string conviasin, string luzartsin, string desviasin, int limvelsin, string intsin, string matsupviasin, string obsviasin, string lugviasin, string cursin, int numcarsin, string sensin, int codtipsin, int codpar, string codsubcir, int codcant, int codprov, string codcaupro, string codcaurea, string codcir, string coddis, int codgeo, string latsinGeo, string lonsinGeo, string codproceso)
        {
            var jsonmensaje = "";

            try
            {

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
                if (codgeo != 0 && latsin.Replace(',', '.') == latsinGeo && lonsin.Replace(',', '.') == lonsinGeo)// comparo si  la georeferencia son identicas (ingreso por buesqueda o ingreso manual)
                {
                    latsin = latsinGeo;
                    lonsin = lonsinGeo;
                }
                
                var codUsuario = HttpContext.Session.GetString(SessionKeyCodUsuario);
                string codAutoridad = HttpContext.Session.GetString("codAutoridad");
                jsonmensaje = objSiniestrosNeg.ModificarSiniestros(codsin,Convert.ToDateTime(fecsin), horsin, latsin.Replace(',', '.'), lonsin.Replace(',', '.'), dirsin.ToUpper(), numfalsin, numlessin, 1, false, Convert.ToInt32(codUsuario), Convert.ToInt32(codUsuario), zonsin, traviasin == "SI" ? true : false, conatmsin, conviasin, luzartsin, desviasin, limvelsin, intsin, matsupviasin, obsviasin, lugviasin, cursin, numcarsin, sensin, Convert.ToInt32(codUsuario), codAutoridad.ToUpper(), codtipsin, codpar, codsubcir, codcant, codprov, codcaupro, codcaurea, codcir, coddis, Convert.ToInt32( codUsuario));
                if (jsonmensaje != "0" &&  1 > Convert.ToInt32( codproceso))
                {
                    objSiniestrosNeg.ModificarSiniestrosFinProceso(codsin, 1);
                }
            }
            catch (Exception ex)
            {
                jsonmensaje = "0";
            }

            return Json(jsonmensaje.ToString());
        }

      
        public JsonResult JsoninsertarDatosVehiculosInvolucrados(int codsin, string placa, string chavehinv, string marvehinv, string modvehinv, string cilvehinv, string matpelvehinv, string codser, string codtipve, string seguroPrivado, string danioMaterial, string matriculaVigente, int anivehinv, int codsubtipoVhl, int codproceso)
        {
            var jsonmensaje = "";
            var jsonResult = ListaVehiculos(0);
            Vehiculo v = new Vehiculo();
            v.placvehinv = placa;
            v.danmatvehinv = danioMaterial == "1" ? true: false;
            v.matvigvehinv = matriculaVigente == "1" ? true : false;
            v.chavehinv = chavehinv;
            v.marvehinv = marvehinv;
            v.modvehinv = modvehinv;
            v.cilvehinv = Convert.ToString(cilvehinv);
            v.segprivehinv = seguroPrivado == "1" ? true : false;
            v.matpelvehinv = matpelvehinv;
            v.anivehinv = anivehinv;
            v.codser = Convert.ToInt32(codser);
            v.codtipve = Convert.ToInt32(codtipve);
            v.codsin = codsin;
            v.codsubtipoVHL = codsubtipoVhl;
            jsonmensaje = objSiniestrosNeg.insertaVehiculosInvolucrados(v);
            if (jsonmensaje != "" && jsonmensaje != "0" && Convert.ToInt32(jsonmensaje) > 0)
            {
                jsonResult = ListaVehiculos(codsin);

                if (jsonmensaje != "0" &&   2 > codproceso)
                {
                    objSiniestrosNeg.ModificarSiniestrosFinProceso(codsin, 2);
                }
            }
            return Json(jsonResult);
        }

        public JsonResult GuardarVictimasInvolucradas(string tipidenvicinv, string numidenvicinv, int edavicinv, string genvicinv, string convicinv24, string convicinv30, string tipparvicinv, string casvicinv, string cinvicinv, string posvicinv, string conalcvicinv, int codsin, int codveh, string[] desaccpea, string ccc, int codproceso)
        {
            var retorno = "";
            var ccc1 = "";
            ccc1 = ccc;
            var jsonResult = listaVictimasInvolucrados(0);
            Victimas v = new Victimas();
            v.tipidenvicinv = tipidenvicinv;
            v.numidenvicinv = numidenvicinv;
            v.edavicinv = edavicinv;
            v.genvicinv = Convert.ToChar(genvicinv);
            v.convicinv24 = convicinv24;
            v.convicinv30 = "";
            v.tipparvicinv = tipparvicinv;
            v.casvicinv = casvicinv == "1" ? true : false;
            v.cinvicinv = cinvicinv == "1" ? true : false; ;
            v.posvicinv = posvicinv ;
            v.conalcvicinv = conalcvicinv == "1" ? true : false; ;
            v.codsin = codsin;
            v.codveh = codveh;
            v.desaccpea = ccc1; //desaccpea == null ? "": desaccpea.ToString();
            retorno = objSiniestrosNeg.insertarVictimasInvolucradas(v);
            if (retorno != "" && retorno != "0" && Convert.ToInt32(retorno) > 0)
            {
                jsonResult = listaVictimasInvolucrados(codsin);
                if (retorno != "0" &&  3 > codproceso)
                {
                    objSiniestrosNeg.ModificarSiniestrosFinProceso(codsin, 3);
                }
                //  return Json(jsonResult);
            }
            return Json(jsonResult);
        }

        public JsonResult GuardarDanioTercero(int codsin, string obsdater, int codtipdater,int codproceso)
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
                jsonResult = listaVistaDaniosTerceros(codsin);
                if (retorno != "0" &&   4 > codproceso)
                {
                    objSiniestrosNeg.ModificarSiniestrosFinProceso(codsin, 4);
                }
                //  return Json(jsonResult);
            }
            //else
            //{
            //    jsonResult = "0";
            //}
            return Json(jsonResult);
        }
        public JsonResult ObtenertarerInformacionSiniestroProcersos(int codsin)
        {
            var jsonResult = ObtenertarerInformacionSiniestroProcerso(codsin);
            return Json(jsonResult);
        }
        public JsonResult FinalizarProcesoSiniestroEdit(int codsin,int codproceso)
        {
            var retorno = codsin.ToString();
            if (retorno != "0" && 5 > codproceso)
            {
                objSiniestrosNeg.ModificarSiniestrosFinProceso(codsin, 5);
                retorno = "1";
            }
          //  var jsonResult = ObtenertarerInformacionSiniestroProcerso(codsin);
            return Json(retorno);
        }

        public JsonResult ObtenerInformacionVictima(string cedula, string tipoI)
        {
            var jsonResult = ObtenerInformacionVictimas(cedula, Convert.ToInt32(tipoI));

            return Json(jsonResult);
        }
       

        public JsonResult ObtenerTraerNumLesionadosFallecidos(int codsin)
        {
            var jsonResult = TraerNumLesionadosFallecidos(codsin);
            return Json(jsonResult);
        }
        public List<SelectListItem> TraerNumLesionadosFallecidos(int codsin)
        {
            return objSiniestrosNeg.TraerNumLesionadosFallecidos(codsin);
        }

        public JsonResult traerDatosVehiculosInvolucradosVictimas(int codsin)
        {
            var jsonResult = ObtenerlistadaVehiculosInvolucrados(codsin);
            return Json(jsonResult);
        }
        public JsonResult ObtenerPlacaVehiculo(string parametro, string opcion)
        {
            var jsonResult = ObtenerPlacaVhl(parametro, opcion);
            return Json(jsonResult);
        }
        public JsonResult CargarParroquiasPorCantonesEdit(string codcant, string codprov)
        {
            var jsonResult = ObtenerlistaParroquiasPorCantones(Convert.ToInt32( codcant),Convert.ToInt32( codprov));

            return Json(jsonResult);
        }
        public JsonResult ObtenerlistaDistritosEdit(int codProv, int codcant)
        {

            var jsonResult = ObtenerlistaDistrito(codProv, codcant);
            return Json(jsonResult);
        }
        public JsonResult ObtenerlistaCircuitosEdit(int codCant, int codPar)
        {
            var jsonResult = ObtenerlistaCiruitos(codCant, codPar);
            return Json(jsonResult);
        }



        public JsonResult eliminaVehiculos(int codVehiculo, int codsin)
        {
            var jsonResult = ListaVehiculos(0);
            var _jsonResult = "";
            int cod = 0;
             if (objSiniestrosNeg.VerificaListaVictimasPorCodigoVeh(codVehiculo).Count() > 0)
            {
                _jsonResult = "-1";
            }
            else {
               cod = objSiniestrosNeg.eliminaVehiculos(codVehiculo);// elimina_vehiculos
                jsonResult = ListaVehiculos(codsin);
            }
       
           
           
            if (_jsonResult == "-1")
                return Json(_jsonResult);
            else
                return Json(jsonResult);
        }


        public JsonResult eliminaVictimas(int codVictima, int codsin)
        {
            var jsonResult = listaVictimasInvolucrados(0);
            var _jsonResult = "";
            int cod = 0;
            cod = objSiniestrosNeg.EliminaVictimasPorCodVic(codVictima);
           if (cod != 0) {

                if (cod != 0)
                {
                    objSiniestrosNeg.ModificarSiniestrosNumLesionadosFallecidos(codsin, "FALLECIDO", 2);
                    objSiniestrosNeg.ModificarSiniestrosNumLesionadosFallecidos(codsin, "LESIONADO", 1);
                }
                cod = objSiniestrosNeg.EliminaAccionesPeatonPorCodigoVictima(Convert.ToInt32(codVictima));


                jsonResult = listaVictimasInvolucrados(codsin);
            }
            else
            {
                _jsonResult = "-1";
            }

          

            if (_jsonResult == "-1")
                return Json(_jsonResult);
            else
                return Json(jsonResult);
        }

        public JsonResult eliminaDaniosTerceros(int codDanio, int codsin)
        {
           // var jsonResult = listaVictimasInvolucrados(0);
            var _jsonResult = "";
            int cod = 0;
            cod = objSiniestrosNeg.EliminaDaniosTercerosPorCodigo(codDanio);
            if (cod != 0)
            {
                _jsonResult = "1";
            }
            else
            {
                _jsonResult = "-1";
            }
            return Json(_jsonResult);
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
                    datos.edad = d.edad;
                    datos.sexo = d.sexo;
                    datos.nombre = d.nombre;
                    datos.fechaNacimiento = d.fechaNacimiento;
                    listDatos.Add(datos);
                }
            }

            return Json(listDatos);
        }

        public List<Vehiculo> ObtenerPlacaVhl(string parametro, string opcion)
        {
            return objSiniestrosNeg.ObtenerPlacaVhl(parametro, Convert.ToInt32(opcion));
        }
        public List<Victimas> listaVictimasInvolucrados(int codsin)
        {
            return objSiniestrosNeg.listaVictimasInvolucrados(codsin);
        }
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
        public List<SelectListItem> ObtenerlistadaVehiculosInvolucrados(int codsin)
        {
            return objSiniestrosNeg.listadaVehiculosInvolucrados(codsin);
        }
        public List<AccionesPeaton> listaVistaAccionesPeatones(int codsin)
        {
            return objSiniestrosNeg.listaVistaAccionesPeatones(codsin);
        }
        public List<SelectListItem> ObtenerlistaPeatones(int codsin)
        {
            return objSiniestrosNeg.listadatosPeatones(codsin);
        }
        public List<DanioMaterial> listaVistaDaniosTerceros(int codsin)
        {
            return objSiniestrosNeg.listaVistaDaniosTerceros(codsin);
        }
        public List<DanioMaterial> listaVistaDaniosTercerosPorCodigo(int codDanio)
        {
            return objSiniestrosNeg.listaVistaDaniosTercerosPorCodigo(codDanio);
        }
        public List<SelectListItem> ObtenerlistaTipoDaniosTerceros()
        {
            return objSiniestrosNeg.listaTipoDaniosTerceros();
        }
        public List<Modelo.Entity.Siniestro> listaSiniestrosPorCodigo(int codSin)
        {
            return objSiniestrosNeg.listaSiniestrosPorCodigo(codSin);
        }
        public JsonResult CargarCantonesPorProvincia(int codprov)
        {
            var jsonResult = ObtenerlistaCantonesPorProvincia(codprov);

            return Json(jsonResult);
        }
        public List<SelectListItem> ObtenerlistaCantonesPorProvincia(int codprov)
        {
            return objSiniestrosNeg.listaCantones(codprov);
        }
        public List<SelectListItem> ObtenerlistaParroquiasPorCantones(int codcant, int codprov)
        {
            return objSiniestrosNeg.listaParroquiasPorCantones(codcant, codprov);
        }
        public List<SelectListItem> ObtenerlistaCiruitos(int codCant, int codPar)
        {
            return objSiniestrosNeg.listaCiruitos(codCant, codPar);
        }
        public List<SelectListItem> ObtenerlistaDistrito(int codProv, int codCant)
        {
            return objSiniestrosNeg.listaDistritos(codProv, codCant);
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
        public List<Modelo.Entity.Siniestro> ObtenertarerInformacionSiniestroProcerso(int codsin)
        {
            return objSiniestrosNeg.tarerInformacionSiniestroProcerso(codsin);
        }
        public List<Victimas> ObtenerInformacionVictimas(string cedula, int tipoI)
        {
            return objSiniestrosNeg.ObtenerInformacionVictima(cedula, tipoI);
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
