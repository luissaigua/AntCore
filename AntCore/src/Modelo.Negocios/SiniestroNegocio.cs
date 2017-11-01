using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Modelo.Entity;
using Model.Datos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Modelo.Negocios
{

    public class SiniestroNegocio
    {
        private SiniestroDatos objSiniestroDao;
        private CatalogosSiniestros obCatalogosSin;
        public SelectList Provincias { get; set; }
        public SiniestroNegocio()
        {
            objSiniestroDao = new SiniestroDatos();
            obCatalogosSin = new CatalogosSiniestros();
        }
        


        [DefaultValue("codprov")]
        public int codprov { get; set; }

        [DisplayName("Provincia")]
        public string nomprov { get; set; }


        public List<Siniestro> findAll(string codaut)
        {
            return objSiniestroDao.listaSiniestros(codaut);
        }
        public List<Siniestro> listaSiniestrosPorCodigo(int codSin)
        {
            return objSiniestroDao.listaSiniestrosPorCodigo(codSin);
        }
        public List<Siniestro> listaSiniestrosPorFechas(string fecini, string fechafin, string codaut, string codprov)
        {
            return objSiniestroDao.listaSiniestrosPorFechas( fecini,  fechafin,  codaut,  codprov);
        }
        public List<Siniestro> listaSiniestrosCm(string anio, string mes, string codAutoridad)
        {
            return objSiniestroDao.listaSiniestrosCm(anio,mes, codAutoridad);
        }

        public List<Vehiculo> ListaVehiculos(int cosdin)
        {
            return objSiniestroDao.listaVehiculosInvolucrados(cosdin);
        }
        public List<Vehiculo> ListaVehiculosPorCodigo(int codvhl)
        {
            return objSiniestroDao.listaVehiculosInvolucradosPorCodigo(codvhl);
        }
        public List<Vehiculo> ListaVehiculosPorRangFechas(string codsin)
        {
            return objSiniestroDao.listaVehiculosInvolucradosPorRangoFechas(codsin);
        }
        public List<Siniestro> listaProvincias()
        {
            return objSiniestroDao.listaProvincias();
        }
        public List<SelectListItem> listaProvinciasEdit()
        {
            return objSiniestroDao.listaProvinciasEdit();
        }
        public List<SelectListItem> listaProvinciasEditVista()
        {
            return objSiniestroDao.listaProvinciasEditVista();
        }
        public List<SelectListItem> listaCantones(int codprov)
        {
            return objSiniestroDao.listaCantonesPorProvincias(codprov);
        }
        //
        public List<CargaDropDownList> listaDistritos(int codProv, int codCant, int codpar)
        {
            return objSiniestroDao.listaDistritos( codProv,  codCant,codpar);
        }
        public List<SelectListItem> listaDistritos(int codProv, int codCant)
        {
            return objSiniestroDao.listaDistritos(codProv, codCant);
        }

        
        public List<SelectListItem> listaTodosDistritos()
        {
            return objSiniestroDao.listaTodosDistritos();
        }
        public List<SelectListItem> listaParroquiasPorCantones(int codcant, int codprov)
        {
            return objSiniestroDao.listaParroquiasPorCantones(codcant,codprov);
        }
        public List<SelectListItem> listarTodasParroquias()
        {
            return objSiniestroDao.listarTodasParroquias();
        }

        public List<SelectListItem> listaServiciosVehiculos()
        {
            return objSiniestroDao.listatipoServicioVehiculos();
        }
       
        
        public List<SelectListItem> listaTipoVehiculos()
        {
            return objSiniestroDao.listaTipoVehiculos();
        }
        public List<SelectListItem> listaDatosTipoZona()
        {
            return obCatalogosSin.datosTipoZona();
        }

        public List<SelectListItem> listadatosCondicionAtmosferica()
        {
            return obCatalogosSin.datosCondicionAtmosferica();
        }

        public List<SelectListItem> listadatosCondicionVia()
        {
            return obCatalogosSin.datosCondicionVia();
        }
        public List<SelectListItem> listadatosLuzArtificial()
        {
            return obCatalogosSin.datosLuzArtificial();
        }
        public List<SelectListItem> listadatosTipoVia()
        {
            return obCatalogosSin.datosTipoVia();
        }
        public List<SelectListItem> listadatosLimiteVelocidad()
        {
            return obCatalogosSin.datosLimiteVelocidad();
        }
        public List<SelectListItem> listadatosControlInterseccion()
        {
            return obCatalogosSin.datosControlInterseccion();
        }
        public List<SelectListItem> listadatosMaterialSuperficieVia()
        {
            return obCatalogosSin.datosMaterialSuperficieVia();
        }
        public List<SelectListItem> listadatosObstaculoVia()
        {
            return obCatalogosSin.datosObstaculoVia();
        }
        public List<SelectListItem> listadatosLugarVia()
        {
            return obCatalogosSin.datosLugarVia();
        }

        public List<SelectListItem> listadatosNumeroCarriles()
        {
            return obCatalogosSin.datosNumeroCarriles();
        }
        public List<SelectListItem> listadatosSenialitica()
        {
            return obCatalogosSin.datosSenialitica();
        }
        public List<SelectListItem> listadatosTransporteMaterialPeligroso()
        {
            return obCatalogosSin.datosTransporteMaterialPeligroso();
        }

        public List<SelectListItem> listadatosCurvaExistente()
        {
            return obCatalogosSin.datosCurvaExistente();
        }

        public List<SelectListItem> listatipoSiniestros ()
        {
            return objSiniestroDao.listatipoSiniestros();
        }
        public List<SelectListItem> listaCausaProbableSiniestros()
        {
            return objSiniestroDao.listaCausaProbableSiniestros();
        }
        public List<SelectListItem> listaCausaRealSiniestros()
        {
            return objSiniestroDao.listaCausaRealSiniestros();
        }
        public List<SelectListItem> listaTipoDaniosTerceros()
        {
            return objSiniestroDao.listadaTiposDaniosTerceros();
        }
        public List<SelectListItem> listaCiruitos(int codCant, int codPar)
        {
            return objSiniestroDao.listaCiruitos( codCant,  codPar);
        }
        public List<SelectListItem> listaTodosCiruitos()
        {
            return objSiniestroDao.listaTodosCiruitos();
        }
        public string insertaVehiculosInvolucrados(Vehiculo v)
        {
            return objSiniestroDao.insertarVehiculosInvolucrados(v); 
        }
        public string ModificarVehiculosInvolucrados(Vehiculo v)
        {
            return objSiniestroDao.ModificarVehiculosInvolucrados(v);
        }
        
        public string GuardaSiniestros(DateTime fecsin, string horsin, string latsin, string lonsin, string dirsin, int numfalsin, int numlessin, int estsin, bool regvalsin, int ageressin, int supressIn, string zonsin, bool traviasin, string conatmsin, string conviasin, string luzartsin, string desviasin, int limvelsin, string intsin, string matsupviasin, string obsviasin, string lugviasin, string cursin, int numcarsin, string sensin, int PUCOD, string codaut, int codtipsin, int codpar, string codsubcir, int codcant, int codprov, string codcaupro, string codcaurea, string codcir, string coddis)
        {
            return objSiniestroDao.GuardaSiniestros( fecsin,  horsin, latsin, lonsin, dirsin, numfalsin, numlessin, estsin, regvalsin, ageressin, supressIn, zonsin, traviasin,  conatmsin, conviasin, luzartsin,  desviasin, limvelsin,  intsin,  matsupviasin,  obsviasin,  lugviasin,  cursin,  numcarsin,  sensin,  PUCOD,  codaut,  codtipsin,  codpar,  codsubcir,  codcant,  codprov,  codcaupro,  codcaurea,  codcir,  coddis);
        }
        public string ModificarSiniestros(int codSiniestro,DateTime fecsin, string horsin, string latsin, string lonsin, string dirsin, int numfalsin, int numlessin, int estsin, bool regvalsin, int ageressin, int supressIn, string zonsin, bool traviasin, string conatmsin, string conviasin, string luzartsin, string desviasin, int limvelsin, string intsin, string matsupviasin, string obsviasin, string lugviasin, string cursin, int numcarsin, string sensin, int PUCOD, string codaut, int codtipsin, int codpar, string codsubcir, int codcant, int codprov, string codcaupro, string codcaurea, string codcir, string coddis, int pucmodsin)
        {
            return objSiniestroDao.ModificarSiniestros(codSiniestro,fecsin, horsin, latsin, lonsin, dirsin, numfalsin, numlessin, estsin, regvalsin, ageressin, supressIn, zonsin, traviasin, conatmsin, conviasin, luzartsin, desviasin, limvelsin, intsin, matsupviasin, obsviasin, lugviasin, cursin, numcarsin, sensin, PUCOD, codaut, codtipsin, codpar, codsubcir, codcant, codprov, codcaupro, codcaurea, codcir, coddis, pucmodsin);
        }
        public string ModificarSiniestrosFinProceso(int codSiniestro,int codprocsin)
        {
            return objSiniestroDao.ModificarSiniestrosFinProceso(codSiniestro, codprocsin);
        }
        
        public string ModificarGeoreferenciaSiniestro(int codsin, int codgeo)
        {
            return objSiniestroDao.ModificarGeoreferenciaSiniestro(codsin, codgeo);
        }
        //
        public string insertarDaniosTerceros(DanioMaterial v)
        {
            return  objSiniestroDao.insertarDaniosTerceros(v);
        }

        public string insertarVictimasInvolucradas(Victimas v)
        {
            return objSiniestroDao.insertarVictimasInvolucradas(v);
        }
        public string ModificaVictimasInvolucradas(Victimas v)
        {
            return objSiniestroDao.ModificaVictimasInvolucradas(v);
        }
        /***************************************
         * acciones peaton insertarAccionesPeaton
         * **********************************/
        public string insertarAccionesPeaton(string desaccpea, int codvicinv)
        {
            return objSiniestroDao.insertarAccionesPeaton(desaccpea, codvicinv);
        }
        public string ModificarAccionesPeaton(int codaccion,string desaccpea, int codvicinv)
        {
            return objSiniestroDao.ModificarAccionesPeaton(codaccion,desaccpea, codvicinv);
        }

        
        /****************************************
         * DATOS DE VICTIMAS
         * **************************************/
        public List<SelectListItem> listadatosTipoIdentificacion()
        {
            return obCatalogosSin.datosTipoIdentificacion();
        }
        public List<SelectListItem> listadatosSexo()
        {
            return obCatalogosSin.datosSexo();
        }
        public List<SelectListItem> listadatosCondicionVictimas24()
        {
            return obCatalogosSin.datosCondicionVictimas24();
        }
        public List<SelectListItem> listadatosCondicionVictimas30()
        {
            return obCatalogosSin.datosCondicionVictimas30();
        }
        public List<SelectListItem> listadatosTipoParticipante()
        {
            return obCatalogosSin.datosTipoParticipante();
        }
        public List<SelectListItem> listadatosPosicionPlaza()
        {
            return obCatalogosSin.datosPosicionPlaza();
        }
        public List<SelectListItem> listadaVehiculosInvolucrados(int codsin)
        {
            return objSiniestroDao.listadaVehiculosInvolucrados(codsin);
        }

        public List<SelectListItem> listadatosAccionesPeaton()
        {
            return obCatalogosSin.datosAccionesPeaton();
        }
        public List<SelectListItem> listadatosSiNo()
        {
            return obCatalogosSin.datosSenializacionExistente();
        }
        public List<SelectListItem> listadatosPeatones(int codsin)
        {
            return objSiniestroDao.listadaDatosPeatones(codsin);
        }
        public string ModificarSiniestrosNumLesionadosFallecidos(int codsin, string parametro, int opcion)
        {
            return objSiniestroDao.ModificarSiniestrosNumLesionadosFallecidos(codsin, parametro, opcion);
        }
        public List<SelectListItem> TraerNumLesionadosFallecidos(int codsin)
        {
            return objSiniestroDao.TraerNumLesionadosFallecidos(codsin);
        }
        public List<Victimas> listaVictimasInvolucrados(int codsin)
        {
            return objSiniestroDao.listaVictimasInvolucrados(codsin);
        }
        public List<Victimas> listaVictimasInvolucradosPorRangoFechas(string codsin)
        {
            return objSiniestroDao.listaVictimasInvolucradosPorRangoFechas(codsin);
        }

        
        public List<Victimas> listaVictimasInvolucradosPorCodigo(int codVictima)
        {
            return objSiniestroDao.listaVictimasInvolucradosPorCodigo(codVictima);
        }
        
        public List<AccionesPeaton> listaVistaAccionesPeatonesPorCodigo(int codAccionPeaton)
        {
            return objSiniestroDao.listaVistaAccionesPeatonesPorCodigo(codAccionPeaton);
        }
        public List<AccionesPeaton> listaVistaAccionesPeatones(int codsin)
        {
            return objSiniestroDao.listaVistaAccionesPeatones(codsin);
        }
        public List<AccionesPeaton> listaVistaAccionesPeatonesPorRangoFechas(string codsin)
        {
            return objSiniestroDao.listaVistaAccionesPeatonesPorRangoFechas(codsin);
        }
        public List<DanioMaterial> listaVistaDaniosTerceros(int codsin)
        {
            return objSiniestroDao.listaVistaDaniosTerceros(codsin);
        }
        public List<DanioMaterial> listaVistaDaniosTercerosPorRangoFehcas(string codsin)
        {
            return objSiniestroDao.listaVistaDaniosTercerosPorRangoFehcas(codsin);
        }
        
        public List<DanioMaterial> listaVistaDaniosTercerosPorCodigo(int codDanio)
        {
            return objSiniestroDao.listaVistaDaniosTercerosPorCodigo(codDanio);
        }
        public List<Siniestro> listaVistaSiniestros(int codsin)
        {
            return objSiniestroDao.listaVistaSiniestros(codsin);
        }
        public List<Siniestro> listaVistaSiniestrosPorFechas(string tbFechaini, string tbFechafin, int tbcodprov, string tbcodautoridad)
        {
            return objSiniestroDao.listaVistaSiniestrosPorFechas( tbFechaini,  tbFechafin,  tbcodprov,  tbcodautoridad);
        }
        public string ModificarDaniosTerceros(DanioMaterial d)
        {
            return objSiniestroDao.ModificarDaniosTerceros(d);
        }

        
        public string modificaRegistroValidadoParaEstadistica(int codsin, int supervIn)
        {
            return objSiniestroDao.modificaRegistroValidadoParaEstadistica(codsin, supervIn);
        }

        public List<Georeferencias> listaGeoreferencias(string codaut)
        {
            return objSiniestroDao.listaGeoreferencias(codaut);
        }
        public List<Georeferencias> listaGeoreferenciasMovil(int codusuario)
        {
            return objSiniestroDao.listaGeoreferenciasMovil(codusuario);
        }
        //
        public List<Vehiculo> ObtenerPlacaVhl(string parametro, int opcion)
        {
            return objSiniestroDao.ObtenerPlacaVhl(parametro,opcion);
        }
        public string verificaPermiso(int codusuario, int opcion) {
            string valor = objSiniestroDao.verificaPermiso(codusuario, opcion);
            return valor;
        }//ModificarAutoridad

        public string ModificarAutoridad(string codaut)
        {
            string valor = objSiniestroDao.ModificarAutoridad(codaut);
            return valor;
        }//
        public string obtenerTipoVehiculo(int codVehiculo)
        {
            return  objSiniestroDao.obtenerTipoVehiculo(codVehiculo);
            
        }
        public List<Victimas> ObtenerInformacionVictima(string cedula, int tipoI)
        {
            return objSiniestroDao.ObtenerInformacionVictima(cedula, tipoI);
        }

        public string EnviaListaCalificaciones(List<Calificaciones> c,string anio, string mes)
        {

            verificarCalificacionesPorFecha(anio,mes);

            return objSiniestroDao.CargaCalificaciones(c);
        }

        public void verificarCalificacionesPorFecha(string anio, string mes)
        {
            var datos = objSiniestroDao.verificarCalificacionesPorFecha(anio,mes);
            if (datos.Count() > 0)
            {
                eliminarRegistrosCalificacionPorFechaMes(anio, mes);
            }
        }
        public string eliminarRegistrosCalificacionPorFechaMes(string anio, string mes)
        {
            return objSiniestroDao.eliminarRegistrosCalificacionPorFechaMes(anio,mes);
        }

        public List<Calificaciones> ListaverificarCalificacionesPorFecha(string anio, string mes)
        {
            return objSiniestroDao.verificarCalificacionesPorFecha(anio,mes);
        }
       
        public List<Calificaciones> ListaverificarCalificacionesPorRangoFecha(string fecini, string fecfin, int codprov)
        {
            return objSiniestroDao.verificarCalificacionesPorRangoFecha(fecini, fecfin,codprov);
        }

        public List<SelectListItem> listaSubTipoVehiculos(int codTipoVhl)
        {
            return objSiniestroDao.listaSubTipoVehiculos(codTipoVhl);
        }
        public List<SelectListItem> listaTodosSubTipoVehiculos()
        {
            return objSiniestroDao.listaTodosSubTipoVehiculos();
        }
        public List<SelectListItem> listadAutoridades(string codauto, string cargamasiva)
        {
            return objSiniestroDao.listadAutoridades( codauto,  cargamasiva);
        }

        public string InsertarCargaMasiva(string codaut, int totalRegistros, int codusuario, string observaciones)
        {
            return objSiniestroDao.InsertarCargaMasiva(codaut, totalRegistros, codusuario, observaciones);
        }
        public List<Siniestro> listaVistaSiniestrosCm(string siniestros)
        {
            return objSiniestroDao.listaVistaSiniestrosCm(siniestros);
        }
        public string CambioContrasenia(Usuarios u,int codusuario)
        {
            return objSiniestroDao.cambiarContraseña(u, codusuario);
        }

        public List<Siniestro> tarerInformacionSiniestroProcerso(int codsin)
        {
            return objSiniestroDao.tarerInformacionSiniestroProcerso(codsin);
        }
        public List<CargaDropDownList> buscaCircuitoZona(int codProv, int codCant, int codpar)
        {
            return objSiniestroDao.buscaCircuitoZona( codProv,  codCant,  codpar);
        }

        public List<Siniestro> VistaSiniestrosPorAutoridad(string codAutoridad, int cod_usuario)
        {
            return objSiniestroDao.VistaSiniestrosPorAutoridad( codAutoridad,  cod_usuario);
        }

        public List<Consultas> VistaBusquedaVictimas(string identificacion)
        {
            return objSiniestroDao.VistaBusquedaVictimas(identificacion);
        }

        public int eliminaVehiculos (int codVehiculo)
         {
            return objSiniestroDao.EliminaVehiculos(codVehiculo);
        }

        public int EliminaVictimasPorCodVeh(int codVehiculo)
        {
            return objSiniestroDao.EliminaVictimasPorCodVeh(codVehiculo);
        }

        public int EliminaVictimasPorCodVic(int codVictima)
        {
            return objSiniestroDao.EliminaVictimasPorCodVic(codVictima);
        }

        
        public string obtieneCodigosVictimasPorCodigoVeh(int codveh)
        {
            return objSiniestroDao.obtieneCodigosVictimasPorCodigoVeh(codveh);
        }

        public int EliminaAccionesPeatonPorCodigoVictima(int codvicinv)
        {
            return objSiniestroDao.EliminaAccionesPeatonPorCodigoVictima(codvicinv);
        }

        public List<Victimas> VerificaListaVictimasPorCodigoVeh(int codveh)
        {
            return objSiniestroDao.VerificaListaVictimasPorCodigoVeh(codveh);
        }

        public int EliminaDaniosTercerosPorCodigo(int codDanio)
        {
            return objSiniestroDao.EliminaDaniosTercerosPorCodigo(codDanio);
        }
        public async Task<List<DatosWSRegistroCivil>> ConsultaDatisWsRegistroCivil(string numeroIdentificacion)
        {
            return await objSiniestroDao.ConsultaDatisWsRegistroCivil(numeroIdentificacion);
            
        }
        public List<Victimas> listaVictimasInvolucradosSinNombre()
        {
            return objSiniestroDao.listaVictimasInvolucradosSinNombre();
        }
        
        public void ActualizaNombresVictimas ()
        {
            Victimas v = new Victimas();
            DatosWSRegistroCivil datos = new DatosWSRegistroCivil();
            var _victimas = listaVictimasInvolucradosSinNombre();
            string cedula = "";
            int cont = 0;
            if (_victimas.Count > 0)
            {
                foreach (var c in _victimas)
                {
                    cedula = c.numidenvicinv;
                    if (cedula.Length==10)
                    {
                        if (cedula != "")
                        {
                            var jsonResult = ConsultaDatisWsRegistroCivil(cedula);
                            var res = jsonResult.Result.ToArray();
                            if (res[0].codigoPaquete != "0")
                            {
                                foreach (var d in res)
                                {
                                    cont = cont + 1;
                                    if (cont == 1)
                                    {
                                        datos.codigoPaquete = d.codigoPaquete;
                                        v.edavicinv = Convert.ToInt32(d.edad);
                                        v.sexo = d.sexo =="HOMBRE" ? "H":"M";
                                        v.nombreVictima = d.nombre;
                                        v.numidenvicinv = cedula;
                                        objSiniestroDao.ActualizaNombresVictimas(v);
                                    }


                                }
                            }
                        }
                       
                    }
                 
                }
            }
        }

        public void enviaMail()
        {
            objSiniestroDao.enviarMail();
        }

    }


}
