using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using excel = OfficeOpenXml;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.Net.Http;
using Modelo.Entity;
using Modelo.Negocios;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AntCore.Controllers.CargaMasiva
{
    public class CargaMasivaController : Controller
    {
        const string SessionKeyCodUsuario = "_CodUsuario";
        private readonly IHostingEnvironment _hostingEnvironment;
        SiniestroNegocio objSiniestrosNeg;
        //List<Modelo.Entity.CargaMasiva> listaCodificaciones = new List<Modelo.Entity.CargaMasiva>();
        
        List<SelectListItem> listaCodificaciones = new List<SelectListItem>();
        List<SelectListItem> listaCodificacionesSin = new List<SelectListItem>();
        List<SelectListItem> listaCodificacionesVhl = new List<SelectListItem>();
        List<SelectListItem> listaCodificacionesVic = new List<SelectListItem>();
        public CargaMasivaController(IHostingEnvironment environment)
        {
            _hostingEnvironment = environment;
            objSiniestrosNeg = new SiniestroNegocio();
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.ListaCodificaciones = listaCodificaciones;
            ViewBag.listaVistaSiniestrosCm = objSiniestrosNeg.listaVistaSiniestrosCm("0");
            return View();
        }
      

        [HttpPost]
        public async Task<ActionResult> Validar(ICollection<IFormFile> file, string fecsin,string obscarga)
        {

            var codUsuario = HttpContext.Session.GetString(SessionKeyCodUsuario);
            string codAutoridad = HttpContext.Session.GetString("codAutoridad");
            
            var provincias = objSiniestrosNeg.listaProvincias();
            
            int contadorProv = 0,contadorCant = 0, contadorParr = 0, contadorDis = 0, contadorCirc = 0;
            int codProv = 0, codCant = 0,   codParr =0;
            string codDis = "0", codCir = "0";
            Model.Datos.CatalogosSiniestros cat = new Model.Datos.CatalogosSiniestros();
            //Modelo.Entity.CargaMasiva obj = new Modelo.Entity.CargaMasiva();
            string nombreFile = "";
            string descripcion = "";
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploadsCm");
            string mensajeError = "", mensajeOk = "";
            var fil = file;
            int count = 1;
            string anio = "", mes = "";
            if (fecsin == null || fecsin == "")
            {
                mensajeError = "Ingrese la fecha";
            }
            else if (file.Count == 0)
            {
                mensajeError = "Seleccione un archvio";

            }
            else if (ValidaRegistrosCargadosFecha(fecsin, codAutoridad) == true)
            {

                mensajeError = "Advertencia:En la fecha seleccionada ya existen siniestros cargados.";
            }
            else if (Convert.ToDateTime(fecsin) > DateTime.Now)
            {
                mensajeError = "Advertencia:En la fecha seleccionada no puede ser mayor a la fecha actual.";
            }
            else if (ValidaCargaMasivaFechaRequerida(fecsin) == false)
            {
                mensajeError = "Advertencia: La fecha seleccionada se encuentra fuera del rango permitido.";
            }
            else if (fil.Count() > 0)
            {
                try
                {
                    foreach (var f in file)
                    {
                        if (f.Length > 0)
                        {
                            var fileStream = new FileStream(Path.Combine(uploads, f.FileName), FileMode.Create);
                            await f.CopyToAsync(fileStream);
                            fileStream.Dispose();

                            string fileName = f.FileName;// ;//esta line para win
                            string fileContentType = f.ContentType;
                            byte[] fileBytes = new byte[f.Length];
                            //string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\uploadsCal";  //esta line para win
                            string sWebRootFolder = _hostingEnvironment.WebRootPath + "/uploadsCm/";  //esta line para linux
                            FileInfo files = new FileInfo(Path.Combine(sWebRootFolder, fileName));
                            nombreFile = fileName;
                            using (var package = new ExcelPackage(files))
                            {


                                var currentSheet = package.Workbook.Worksheets;
                                var workSheet = currentSheet.First();
                                var workSheet1 = currentSheet[2];
                                var workSheet2 = currentSheet[3];
                                var workSheet3 = currentSheet[4];
                                var workSheet4 = currentSheet[5];
                                var noOfCol = workSheet.Dimension.End.Column;
                                var noOfRow = workSheet.Dimension.End.Row;
                                var noOfRow1 = workSheet1.Dimension.End.Row;
                                var noOfRow2 = workSheet2.Dimension.End.Row;
                                var noOfRow3 = workSheet3.Dimension.End.Row;
                                var noOfRow4 = workSheet4.Dimension.End.Row;



                                string hoja1 = workSheet.Name;
                                string hoja2 = workSheet1.Name;
                                string hoja3 = workSheet2.Name;
                                string hoja4 = workSheet3.Name;
                                string hoja5 = workSheet4.Name;

                                if (hoja1 == "Siniestro")
                                {

                                    for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                                    {
                                        count += 1;
                                        Model.Datos.CargaDropDownList obj = new Model.Datos.CargaDropDownList();
                                        Model.Datos.CargaDropDownList objSin = new Model.Datos.CargaDropDownList();
                                        //obj.codigo = Convert.ToString(count++);
                                        //obj.nombre = Convert.ToString(descripcion);
                                        //listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() });

                                        string codsiniestro = workSheet.Cells[rowIterator, 1].Value == null ? "" : workSheet.Cells[rowIterator, 1].Value.ToString();
                                        string fechaSiniestro = workSheet.Cells[rowIterator, 2].Value == null ? "" : workSheet.Cells[rowIterator, 2].Value.ToString();
                                        string horaSiniestro = workSheet.Cells[rowIterator, 3].Value == null ? "" : workSheet.Cells[rowIterator, 3].Value.ToString();
                                        string latitud = workSheet.Cells[rowIterator, 4].Value == null ? "" : workSheet.Cells[rowIterator, 4].Value.ToString();
                                        string longuitud = workSheet.Cells[rowIterator, 5].Value == null ? "" : workSheet.Cells[rowIterator, 5].Value.ToString();

                                        string provincia = workSheet.Cells[rowIterator, 6].Value == null ? "" : workSheet.Cells[rowIterator, 6].Value.ToString();
                                        string canton = workSheet.Cells[rowIterator, 7].Value == null ? "" : workSheet.Cells[rowIterator, 7].Value.ToString();
                                        string parroquia = workSheet.Cells[rowIterator, 8].Value == null ? "" : workSheet.Cells[rowIterator, 8].Value.ToString();
                                        //string distrito = workSheet.Cells[rowIterator, 9].Value == null ? "" : workSheet.Cells[rowIterator, 9].Value.ToString();
                                        //string circuito = workSheet.Cells[rowIterator, 10].Value == null ? "" : workSheet.Cells[rowIterator, 10].Value.ToString();

                                        //string zona = workSheet.Cells[rowIterator, 11].Value == null ? "" : workSheet.Cells[rowIterator, 11].Value.ToString();
                                        string direccion = workSheet.Cells[rowIterator, 9].Value == null ? "" : workSheet.Cells[rowIterator, 9].Value.ToString();
                                        //string numerofallecidos = workSheet.Cells[rowIterator, 13].Value == null ? "" : workSheet.Cells[rowIterator, 13].Value.ToString();
                                        //string numerolesionados = workSheet.Cells[rowIterator, 14].Value == null ? "" : workSheet.Cells[rowIterator, 14].Value.ToString();
                                        string condicionAtmosferica = workSheet.Cells[rowIterator, 10].Value == null ? "" : workSheet.Cells[rowIterator, 10].Value.ToString();

                                        string condicionVia = workSheet.Cells[rowIterator, 11].Value == null ? "" : workSheet.Cells[rowIterator, 11].Value.ToString();
                                        string tipoVia = workSheet.Cells[rowIterator, 12].Value == null ? "" : workSheet.Cells[rowIterator, 12].Value.ToString();
                                        string limiteVelocidad = workSheet.Cells[rowIterator, 13].Value == null ? "" : workSheet.Cells[rowIterator, 13].Value.ToString();
                                        string trabajosVia = workSheet.Cells[rowIterator, 14].Value == null ? "" : workSheet.Cells[rowIterator, 14].Value.ToString();
                                        string numeroCarrilles = workSheet.Cells[rowIterator, 15].Value == null ? "" : workSheet.Cells[rowIterator, 15].Value.ToString();

                                        string MaterialSuperfice = workSheet.Cells[rowIterator, 16].Value == null ? "" : workSheet.Cells[rowIterator, 16].Value.ToString();
                                        string ControlInterseccion = workSheet.Cells[rowIterator, 17].Value == null ? "" : workSheet.Cells[rowIterator, 17].Value.ToString();
                                        string ObtsaculosVia = workSheet.Cells[rowIterator, 18].Value == null ? "" : workSheet.Cells[rowIterator, 18].Value.ToString();
                                        string LugarVia = workSheet.Cells[rowIterator, 19].Value == null ? "" : workSheet.Cells[rowIterator, 19].Value.ToString();
                                        string CurvaExistente = workSheet.Cells[rowIterator, 20].Value == null ? "" : workSheet.Cells[rowIterator, 20].Value.ToString();

                                        string SeñalizacionExistente = workSheet.Cells[rowIterator, 21].Value == null ? "" : workSheet.Cells[rowIterator, 21].Value.ToString();
                                        string TipoSiniestro = workSheet.Cells[rowIterator, 22].Value == null ? "" : workSheet.Cells[rowIterator, 22].Value.ToString();
                                        string CausaProbable = workSheet.Cells[rowIterator, 23].Value == null ? "" : workSheet.Cells[rowIterator, 23].Value.ToString();
                                        string LuzArtificial = workSheet.Cells[rowIterator, 24].Value == null ? "" : workSheet.Cells[rowIterator, 24].Value == null ? "" : workSheet.Cells[rowIterator, 29].Value.ToString();

                                        // verifico los codigos de los siniestros
                                        int contadorSiniestros = 0;

                                        if (codsiniestro == "") { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "No existe el codigo del siniestro" + " - " + " Celda {A -" + count + "} "; obj.codigo = count.ToString(); listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }

                                        string _codautoridad = "";
                                        try
                                        {
                                            _codautoridad = codsiniestro.Substring(0, 3);
                                        }
                                        catch
                                        {
                                            if (_codautoridad != codAutoridad) { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "El codigo del siniestro tiene el formato incorrecto, debe contener el codigo de la autoridad al inicio ej: PNE0120170713" + " - " + " Celda {A -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        }

                                        if (_codautoridad != codAutoridad) { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "El codigo del siniestro tiene una autoridad inválida" + " - " + " Celda {A -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }

                                        if (codsiniestro != "")
                                        {
                                            objSin.codigo = count.ToString();
                                            objSin.nombre = codsiniestro;
                                            listaCodificacionesSin.Add(new SelectListItem() { Value = objSin.codigo.ToString(), Text = objSin.nombre.ToString() });

                                        }
                                        if (listaCodificacionesSin.Count > 0)
                                        {
                                            foreach (var ccs in listaCodificacionesSin)
                                            {
                                                if (ccs.Text == codsiniestro)
                                                    contadorSiniestros++;

                                            }
                                            //codsin_validador[count] = codsiniestro;
                                            if (contadorSiniestros > 1)
                                            {
                                                obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "El codigo del siniestro " + codsiniestro.ToString() + " se encuentra duplicado  " + " - " + " Celda {A -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() });
                                            }
                                        }



                                        //verifico las fechas del siniestro
                                        if (fechaSiniestro == "") { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "No existe la fecha  del siniestro" + " - " + " Celda {B -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        try
                                        {
                                            DateTime fechsin = Convert.ToDateTime(fechaSiniestro);
                                        }
                                        catch (Exception ex)
                                        { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "La fecha  del siniestro tiene un formato incorrecto, debe tener el formato (yyyy-mm-dd)" + " - " + " Celda {B -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }

                                        string aniosin = fechaSiniestro.Replace('/', '-').Split('-')[0];
                                        string messin = fechaSiniestro.Replace('/', '-').Split('-')[1];
                                        DateTime fecha_actual = DateTime.Now;
                                        string fechamin = aniosin + "-" + messin + "-" + "01";
                                        messin = Convert.ToString(Convert.ToInt32(messin) + 1);
                                        string fechamax = aniosin + "-" + messin + "-" + "10";
                                        if (Convert.ToDateTime(fechamax) >= Convert.ToDateTime(fecha_actual) && Convert.ToDateTime(fechaSiniestro) <= Convert.ToDateTime(fechamax) && Convert.ToDateTime(fechaSiniestro) >= Convert.ToDateTime(fechamin) && Convert.ToDateTime(fechaSiniestro) <= Convert.ToDateTime(fecha_actual))
                                        {
                                        }
                                        else
                                        { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "La fecha  del siniestro se encuentra fuera del rango permitido" + " - " + " Celda {B -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }

                                        //verifico las  horas del siniestro
                                        if (horaSiniestro == "") { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "No existe la hora  del siniestro" + " - " + " Celda {C -" + count + "} "; obj.codigo = count.ToString(); }
                                        if (horaSiniestro.Length < 5) { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "El formato de la hora  del siniestro es incorrecto, formato de 24 horas requerido (HH:MM) " + " - " + " Celda {C -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        if (!horaSiniestro.Contains(':')) { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "El formato de la hora  del siniestro es incorrecto, formato de 24 horas  requerido (HH:MM)" + " - " + " Celda {C -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }


                                        // verifico la latitud
                                        if (latitud == "") { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "No existe la latitud  del siniestro" + " - " + " Celda {D -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        if (!latitud.Contains('-') || latitud == "0" || latitud.Length < 3 || !latitud.Contains('.')) { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "Formato incorrecto de la latitud  del siniestro" + " - " + " Celda {D -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        try
                                        {
                                            decimal _latitud = Convert.ToDecimal(latitud);
                                        }
                                        catch
                                        {
                                            obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "Formato incorrecto de la latitud  del siniestro" + " - " + " Celda {D -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() });
                                        }
                                        //verifico la longuitud
                                        if (longuitud == "") { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "No existe la longitud  del siniestro" + " - " + " Celda {E -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        if (!longuitud.Contains('-') || longuitud == "0" || longuitud.Length < 3 || !longuitud.Contains('.')) { obj.codigo = obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "Formato incorrecto de la longuitud  del siniestro" + " - " + " Celda {E -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        try
                                        {
                                            decimal _longuitud = Convert.ToDecimal(longuitud);
                                        }
                                        catch
                                        {
                                            obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "Formato incorrecto de la longitud  del siniestro" + " - " + " Celda {E -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() });
                                        }
                                        // verifico las provincias
                                        if (provincia == "") { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "No existe la provincia  del siniestro" + " - " + " Celda {F -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        foreach (var p in provincias)
                                        {
                                            if (p.nomprov == provincia)
                                            {
                                                contadorProv++;
                                                codProv = p.codprov;
                                            }

                                        }
                                        if (contadorProv == 0) { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "La provincia ingresada no es correcta" + " - " + " Celda {F -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }

                                        // verifico los cantones
                                        if (canton == "") { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "No existe el canton  del siniestro" + " - " + " Celda {G -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        var cantones = objSiniestrosNeg.listaCantones(codProv);
                                        foreach (var c in cantones)
                                        {
                                            if (c.Text.ToString() == canton)
                                            {
                                                contadorCant++;
                                                codCant = Convert.ToInt32(c.Value);
                                            }

                                        }
                                        if (contadorCant == 0) { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "El canton ingresado no es correcto para la provincia seleccionada" + " - " + " Celda {G -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }

                                        //  verifico las parroquias
                                        if (parroquia == "") { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "No existe la parroquia  del siniestro" + " - " + " Celda {H -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }

                                        var parroquias = objSiniestrosNeg.listaParroquiasPorCantones(codCant, codProv);
                                        foreach (var par in parroquias)
                                        {
                                            if (par.Text.ToString() == parroquia)
                                            {
                                                contadorParr++;
                                                codParr = Convert.ToInt32(par.Value);
                                            }
                                        }
                                        if (contadorParr == 0) { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "La parroquia  ingresada no es correcta para el cnatón seleccionado " + " - " + " Celda {H -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }

                                        //// verifico los distritos
                                        //if (distrito == "") { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "No existe el distrito  del siniestro" + " - " + " Celda {I -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        //var distritos = objSiniestrosNeg.listaDistritos(codProv, codCant);
                                        //foreach (var d in distritos)
                                        //{
                                        //    if (d.Value == distrito)
                                        //    {
                                        //        contadorDis++;
                                        //        codDis = d.Value;
                                        //    }
                                        //}
                                        //if (contadorDis == 0) { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "El distrito ingresado no es el correcto para la provincia seleccionada" + " - " + " Celda {I -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }


                                        //// verifico los circuitos
                                        //if (circuito == "") { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "No existe el circuito  del siniestro para el canton seleccionado" + " - " + " Celda {J -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        //var circuitos = objSiniestrosNeg.listaCiruitos(codCant, codProv);
                                        //foreach (var c in circuitos)
                                        //{
                                        //    if (c.Text == circuito)
                                        //    {
                                        //        contadorCirc++;
                                        //        codCir = c.Value;
                                        //    }
                                        //}
                                        //if (contadorCirc == 0) { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "El circuito ingresado no es el correcto para el canton seleccionado " + " - " + " Celda {J -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }

                                        ////verifico la zona
                                        //if (zona == "") { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "No existe la zona  del siniestro" + " - " + " Celda {K -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        //var zonas = cat.datosTipoZona();
                                        //int contatorZonas = 0;
                                        //foreach (var z in zonas)
                                        //{
                                        //    if (z.Text == zona)
                                        //    {
                                        //        contatorZonas++;
                                        //    }
                                        //}
                                        //if (contatorZonas == 0) { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "La zona del siniestro es inocrrecta debe ser RURAL ó URBANO" + " - " + " Celda {K -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }

                                        // verifico la direccion
                                        if (direccion == "") { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "No existe la direccion  del siniestro" + " - " + " Celda {I -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        if (direccion.Length < 10) { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "La direccion  del siniestro debe contener al menos 10 caracteres " + " - " + " Celda {I -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        if (direccion.Length > 150) { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "La direccion  del siniestro debe contener máximo 150 caracteres " + " - " + " Celda {I -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }


                                        // verifico la condicion atmosferica
                                        if (condicionAtmosferica == "") { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "No existe la condicion atmosferica del siniestro" + " - " + " Celda {J -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        var codicionesAtmosfericas = cat.datosCondicionAtmosferica();
                                        int contadorCodAtmos = 0;
                                        foreach (var ca in codicionesAtmosfericas)
                                        {
                                            if (ca.Text == condicionAtmosferica)
                                            {
                                                contadorCodAtmos++;
                                            }
                                        }
                                        if (contadorCodAtmos == 0) { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "La condicion atmosferica del siniestro es incorrecta" + " - " + " Celda {J -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }

                                        // verifico las codicion via
                                        if (condicionVia == "") { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "No existe la condicion de la via  del siniestro" + " - " + " Celda {K -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        var condicionesvia = cat.datosCondicionVia();
                                        int contadorCondVia = 0;
                                        foreach (var cv in condicionesvia)
                                        {
                                            if (cv.Text == condicionVia)
                                            {
                                                contadorCondVia++;
                                            }
                                        }
                                        if (contadorCondVia == 0) { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "La condicion de la via  del siniestro seleccionada es incorrecta" + " - " + " Celda {K -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }

                                        // verifico el tipo de via
                                        if (tipoVia == "") { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "No existe el tipo de via  del siniestro" + " - " + " Celda {L -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        var tipovias = cat.datosTipoVia();
                                        int contadorTipoVia = 0;
                                        foreach (var tv in tipovias)
                                        {
                                            if (tv.Text == tipoVia)
                                            {
                                                contadorTipoVia++;
                                            }
                                        }
                                        if (contadorTipoVia == 0) { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "El tipo de via  seleccionada del siniestro es incorrecta" + " - " + " Celda {L -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }

                                        // verifico el limite de velocidad
                                        if (limiteVelocidad == "") { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "No existe el limite de velocidad  del siniestro" + " - " + " Celda {M -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        try
                                        {
                                            int limVel = Convert.ToInt32(limiteVelocidad);
                                        }
                                        catch (Exception ex)
                                        {
                                            obj.codigo = obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "El limite de velocidad  del siniestro tiene un formato incorrecto, debe ser un entero positivo" + " - " + " Celda {M -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() });
                                        }
                                        int contadorLimVel = 0;
                                        var limitesVelocidades = cat.datosLimiteVelocidad();
                                        foreach (var lv in limitesVelocidades)
                                        {
                                            if (lv.Text == limiteVelocidad)
                                                contadorLimVel++;
                                        }
                                        if (contadorLimVel == 0) { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "El limite de velocidad  del siniestro seleccionado es incorrecto" + " - " + " Celda {M -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }

                                        // verifico los trabajos en la via
                                        if (trabajosVia == "") { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "No existe la informacion de trabjos en la via  del siniestro" + " - " + " Celda {N -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        if (trabajosVia != "SI" && trabajosVia != "NO") { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "La informacion de trabjos en la via  del siniestro debe ser SI ó NO" + " - " + " Celda {N -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }

                                        // verifico los numero de carriles
                                        if (numeroCarrilles == "") { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "No existe la información del numero de carriles  del siniestro" + " - " + " Celda {O -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        try
                                        {
                                        }
                                        catch (Exception ex)
                                        {
                                            obj.codigo = obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "La información del numero de carriles  del siniestro tiene un formatoincorrecto, debe ser un entero positivo" + " - " + " Celda {O -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() });
                                        }
                                        int contNumCarrilles = 0;
                                        var numerosCarrilles = cat.datosNumeroCarriles();
                                        foreach (var nc in numerosCarrilles)
                                        {
                                            if (nc.Text == numeroCarrilles)
                                            {
                                                contNumCarrilles++;
                                            }
                                        }
                                        if (contNumCarrilles == 0) { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "El numero de carriles  del siniestro seleccionado es incorrecto." + " - " + " Celda {O -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }

                                        // valido el MaterialSuperfice
                                        if (MaterialSuperfice == "") { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "No existe la informacion del tipo de material de superficie  del siniestro" + " - " + " Celda {P -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        int contadoMatSuper = 0;
                                        var materialSuperficies = cat.datosMaterialSuperficieVia();
                                        foreach (var ms in materialSuperficies)
                                        {
                                            if (ms.Text == MaterialSuperfice)
                                            {
                                                contadoMatSuper++;
                                            }

                                        }
                                        if (contadoMatSuper == 0) { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "El tipo de material de superficie  del siniestro es incorrecto" + " - " + " Celda {P -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }

                                        // verifico ControlInterseccion
                                        if (ControlInterseccion == "") { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "No existe la informacion del control de interseccion  del siniestro" + " - " + " Celda {Q -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        int contadorControlInter = 0;
                                        var controlIntersecciones = cat.datosControlInterseccion();
                                        foreach (var ci in controlIntersecciones)
                                        {
                                            if (ci.Text == ControlInterseccion)
                                            {
                                                contadorControlInter++;
                                            }
                                        }
                                        if (contadorControlInter == 0) { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "El control de interseccion  del siniestro es incorrecto" + " - " + " Celda {Q -" + count + "} "; obj.codigo = count.ToString(); listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }

                                        // valido ObtsaculosVia
                                        if (ObtsaculosVia == "") { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "No existe la infromacion de obstaculos de la via  del siniestro" + " - " + " Celda {R -" + count + "} "; obj.codigo = count.ToString(); listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        int contadorObsVia = 0;
                                        var ObtsaculosVias = cat.datosObstaculoVia();
                                        foreach (var ov in ObtsaculosVias)
                                        {
                                            if (ov.Text == ObtsaculosVia)
                                            {
                                                contadorObsVia++;
                                            }
                                        }
                                        if (contadorObsVia == 0) { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "La infromacion de obstaculos de la via  del siniestro es incorrecta" + " - " + " Celda {R -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }

                                        // valido lugar via
                                        if (LugarVia == "") { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "No existe la informacion del lugar de la via  del siniestro" + " - " + " Celda {S -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        int contadorLugarVia = 0;
                                        var lugarvias = cat.datosLugarVia();
                                        foreach (var lv in lugarvias)
                                        {
                                            if (lv.Text == LugarVia)
                                                contadorLugarVia++;
                                        }
                                        if (contadorLugarVia == 0) { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "La informacion del lugar de la via  del siniestro es incorrecta" + " - " + " Celda {S -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }

                                        // valido CurvaExistente
                                        if (CurvaExistente == "") { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "No existe la informacion d la curva existente  del siniestro" + " - " + " Celda {T -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        int contadorCurvaExis = 0;
                                        var curvasExistentes = cat.datosCurvaExistente();
                                        foreach (var ce in curvasExistentes)
                                        {
                                            if (ce.Text == CurvaExistente)
                                                contadorCurvaExis++;

                                        }
                                        if (contadorCurvaExis == 0) { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "La informacion d la curva existente  del siniestro es incorrecta" + " - " + " Celda {T -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }


                                        // valido SeñalizacionExistente
                                        if (SeñalizacionExistente == "") { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "No existe la informacion de la señalizacion existente  del siniestro" + " - " + " Celda {U -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        int contadorSeñalizacionExistente = 0;
                                        var SeñalizacionExistentes = cat.datosSenialitica();
                                        foreach (var se in SeñalizacionExistentes)
                                        {
                                            if (se.Text == SeñalizacionExistente)
                                                contadorSeñalizacionExistente++;
                                        }
                                        if (contadorSeñalizacionExistente == 0) { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "La informacion de la señalizacion existente  del siniestro es incorrecta" + " - " + " Celda {U -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }

                                        // valido TipoSiniestro
                                        if (TipoSiniestro == "") { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "No existe la informacion del tipo del siniestro" + " - " + " Celda {V -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        int contadorTipoSiniestro = 0;
                                        var TipoSiniestros = objSiniestrosNeg.listatipoSiniestros();
                                        foreach (var ts in TipoSiniestros)
                                        {
                                            if (ts.Text == TipoSiniestro)
                                                contadorTipoSiniestro++;
                                        }
                                        if (contadorTipoSiniestro == 0) { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "La informacion del tipo del siniestro es incorrecta" + " - " + " Celda {V -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }

                                        // valido causa  probable
                                        if (CausaProbable == "") { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "No existe la informacion de la causa  probable  del siniestro" + " - " + " Celda {W -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        int contadorCausaProbable = 0;
                                        var CausaProbables = objSiniestrosNeg.listaCausaProbableSiniestros();
                                        foreach (var cp in CausaProbables)
                                        {
                                            if (cp.Text == CausaProbable)
                                                contadorCausaProbable++;

                                        }
                                        if (contadorCausaProbable == 0) { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "La informacion de la causa  probable  del siniestro es incorrecta" + " - " + " Celda {W -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }

                                        // valido LuzArtificial
                                        string _horasin = horaSiniestro.Split(':')[0];
                                        int contadorLuzArtificial = 0;
                                        if (Convert.ToInt32(_horasin) == 18 || Convert.ToInt32(_horasin) == 19 || Convert.ToInt32(_horasin) == 20 || Convert.ToInt32(_horasin) == 21 || Convert.ToInt32(_horasin) == 22 || Convert.ToInt32(_horasin) == 23 || Convert.ToInt32(_horasin) == 24 || _horasin.ToString() == "01" || (_horasin.ToString()) == "02" || (_horasin.ToString()) == "03" || (_horasin.ToString()) == "04" || (_horasin.ToString()) == "05" || (_horasin.ToString()) == "06")
                                        {
                                            if (LuzArtificial == "") { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "No existe la infromacion de la luz artificail  del siniestro" + " - " + " Celda {X -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }

                                        }
                                        if (Convert.ToInt32(_horasin) == 18 || Convert.ToInt32(_horasin) == 19 || Convert.ToInt32(_horasin) == 20 || Convert.ToInt32(_horasin) == 21 || Convert.ToInt32(_horasin) == 22 || Convert.ToInt32(_horasin) == 23 || Convert.ToInt32(_horasin) == 24 || _horasin.ToString() == "01" || (_horasin.ToString()) == "02" || (_horasin.ToString()) == "03" || (_horasin.ToString()) == "04" || (_horasin.ToString()) == "05" || (_horasin.ToString()) == "06")
                                        {

                                            var LuzArtificiales = cat.datosLuzArtificial();
                                            foreach (var la in LuzArtificiales)
                                            {
                                                if (la.Text == LuzArtificial)
                                                    contadorLuzArtificial++;
                                            }
                                            if (contadorLuzArtificial == 0) { obj.codigo = count.ToString() + " - " + hoja1; obj.nombre = "La infromacion de la luz artificail  del siniestro es incorrecta" + " - " + " Celda {X -" + count + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        }

                                    }

                                }
                                if (hoja2 == "Vehiculos" && listaCodificaciones.Count == 0)
                                {
                                    int count1 = 1;
                                    int contadorSinV = 0;
                                    for (int rowIterator = 2; rowIterator <= noOfRow1; rowIterator++)
                                    {
                                        Model.Datos.CargaDropDownList obj = new Model.Datos.CargaDropDownList();
                                        Model.Datos.CargaDropDownList objVe = new Model.Datos.CargaDropDownList();
                                        count1 += 1;
                                        string codsiniestroV = workSheet1.Cells[rowIterator, 1].Value == null ? "" : workSheet1.Cells[rowIterator, 1].Value.ToString();
                                        string placaV = workSheet1.Cells[rowIterator, 2].Value == null ? "" : workSheet1.Cells[rowIterator, 2].Value.ToString();
                                        string danioMaterialV = workSheet1.Cells[rowIterator, 3].Value == null ? "" : workSheet1.Cells[rowIterator, 3].Value.ToString();
                                        string matriculaVigenteV = workSheet1.Cells[rowIterator, 4].Value == null ? "" : workSheet1.Cells[rowIterator, 4].Value.ToString();
                                        string chasisV = workSheet1.Cells[rowIterator, 5].Value == null ? "" : workSheet1.Cells[rowIterator, 5].Value.ToString();
                                        string marcaV = workSheet1.Cells[rowIterator, 6].Value == null ? "" : workSheet1.Cells[rowIterator, 6].Value.ToString();
                                        string modeloV = workSheet1.Cells[rowIterator, 7].Value == null ? "" : workSheet1.Cells[rowIterator, 7].Value.ToString();
                                        string anioV = workSheet1.Cells[rowIterator, 8].Value == null ? "" : workSheet1.Cells[rowIterator, 8].Value.ToString();
                                        string cilindrajeV = workSheet1.Cells[rowIterator, 9].Value == null ? "" : workSheet1.Cells[rowIterator, 9].Value.ToString();
                                        string seguroPrivadoV = workSheet1.Cells[rowIterator, 10].Value == null ? "" : workSheet1.Cells[rowIterator, 10].Value.ToString();
                                        string materialPeligrosoV = workSheet1.Cells[rowIterator, 11].Value == null ? "" : workSheet1.Cells[rowIterator, 11].Value.ToString();
                                        string tipoServicioV = workSheet1.Cells[rowIterator, 12].Value == null ? "" : workSheet1.Cells[rowIterator, 12].Value.ToString();
                                        string tipoVehiculoV = workSheet1.Cells[rowIterator, 13].Value == null ? "" : workSheet1.Cells[rowIterator, 13].Value.ToString();
                                        string subTipoVehiculoV = workSheet1.Cells[rowIterator, 14].Value == null ? "" : workSheet1.Cells[rowIterator, 14].Value.ToString();


                                        // valido codsinVehiculo
                                        if (codsiniestroV == "") { obj.codigo = count1.ToString() + " - " + hoja2; obj.nombre = "No existe el codigo del siniestro" + " - " + " Celda {A -" + count1 + "} "; obj.codigo = count1.ToString(); listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        string _codautoridad = "";
                                        try
                                        {
                                            _codautoridad = codsiniestroV.Substring(0, 3);
                                        }
                                        catch
                                        {
                                            if (_codautoridad != codAutoridad) { obj.codigo = count1.ToString() + " - " + hoja2; obj.nombre = "El codigo del siniestro tiene el formato incorrecto, debe contener el codigo de la autoridad al inicio ej: PNE0120170713" + " - " + " Celda {A -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        }

                                        if (_codautoridad != codAutoridad) { obj.codigo = count1.ToString() + " - " + hoja2; obj.nombre = "El codigo del siniestro tiene una autoridad inválida" + " - " + " Celda {A -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }


                                        if (listaCodificacionesSin.Count > 0)
                                        {
                                            contadorSinV = 0;
                                            foreach (var ccs in listaCodificacionesSin)
                                            {
                                                if (ccs.Text == codsiniestroV)
                                                    contadorSinV++;

                                            }
                                            //codsin_validador[count] = codsiniestro;
                                            if (contadorSinV == 0)
                                            {
                                                obj.codigo = count1.ToString() + " - " + hoja2; obj.nombre = "El codigo del siniestro " + codsiniestroV.ToString() + " no se existe en la hoja de Siniestros   " + " - " + " Celda {A -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() });
                                            }
                                        }

                                        //VERIFICO  placaV
                                        if (placaV.Trim() == "") { obj.codigo = count1.ToString() + " - " + hoja2; obj.nombre = "No existe la placa del vehículo" + " - " + " Celda {B -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        
                                        if (placaV.Trim().Length >10) { obj.codigo = count1.ToString() + " - " + hoja2; obj.nombre = "La placa del vehículo debe contener máximo 10 caracteres" + " - " + " Celda {B -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }

                                        if (placaV.Trim() != "")
                                        {
                                            objVe.codigo = count.ToString();
                                            objVe.nombre = placaV.Trim();
                                            listaCodificacionesVhl.Add(new SelectListItem() { Value = objVe.codigo.ToString(), Text = objVe.nombre.ToString() });

                                        }
                                        //VALIDO DAÑO MATERIAL
                                        if (danioMaterialV.Trim() == "") { obj.codigo = count1.ToString() + " - " + hoja2; obj.nombre = "No existe la informacion del daño material" + " - " + " Celda {C -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        if (danioMaterialV.Trim() != "SI" && danioMaterialV.Trim() != "NO") { obj.codigo = count1.ToString() + " - " + hoja2; obj.nombre = "la informacion del daño material es incorrecta, la informacion debe ser SI ó NO" + " - " + " Celda {C -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        // VALIDO MATRICULA VIGENTE
                                        if (matriculaVigenteV.Trim() == "") { obj.codigo = count1.ToString() + " - " + hoja2; obj.nombre = "No existe la informacion de matricula vigente del vehículo" + " - " + " Celda {D -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        if (matriculaVigenteV.Trim() != "SI" && matriculaVigenteV.Trim() != "NO") { obj.codigo = count1.ToString() + " - " + hoja2; obj.nombre = "la informacion de matricula vigente del vehículo es incorrecta debe ser SI ó NO." + " - " + " Celda {D -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        // VALIDO CHASIS
                                        if (chasisV.Trim() == "") { obj.codigo = count1.ToString() + " - " + hoja2; obj.nombre = "No existe la informacion del chasis del vehículo." + " - " + " Celda {E -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        if (chasisV.Trim().Length >50) { obj.codigo = count1.ToString() + " - " + hoja2; obj.nombre = "La informacion del chasis del vehículo debe tener máximo 50 caracteres." + " - " + " Celda {E -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        //VALIDO MARCa
                                        if (marcaV.Trim() == "") { obj.codigo = count1.ToString() + " - " + hoja2; obj.nombre = "No existe la informacion da la marca del vehículo." + " - " + " Celda {F -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        if (marcaV.Trim().Length > 60) { obj.codigo = count1.ToString() + " - " + hoja2; obj.nombre = "La información da la marca del vehículo debe contener màximo 60 caracteres." + " - " + " Celda {F -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        // VALIDO EL MODELO
                                        if (modeloV.Trim() == "") { obj.codigo = count1.ToString() + " - " + hoja2; obj.nombre = "No existe la informacion del modelo del vehículo." + " - " + " Celda {G -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        if (modeloV.Trim().Length > 60) { obj.codigo = count1.ToString() + " - " + hoja2; obj.nombre = "La informacion del modelo del vehículo debe contener máximo 60 caracteres." + " - " + " Celda {G -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        //VALIDO AÑO
                                        if (anioV.Trim() == "") { obj.codigo = count1.ToString() + " - " + hoja2; obj.nombre = "No existe la informacion del año del vehículo." + " - " + " Celda {H -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        if (anioV.Trim().Length >4) { obj.codigo = count1.ToString() + " - " + hoja2; obj.nombre = "La informacion del año del vehículo debe contener màximo 4 caracteres." + " - " + " Celda {H -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        try
                                        {
                                            int _anioV = Convert.ToInt32(anioV);
                                        }
                                        catch
                                        {
                                            obj.codigo = count1.ToString() + " - " + hoja2; obj.nombre = "El año del vehículo tiene un formato incorrecto,  debe ser un entero positivo." + " - " + " Celda {H -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() });
                                        }

                                        // VALIDO CILINDRAJE
                                        if (cilindrajeV.Trim() == "") { obj.codigo = count1.ToString() + " - " + hoja2; obj.nombre = "No existe la informacion del cilindraje del vehículo." + " - " + " Celda {I -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        if (cilindrajeV.Trim().Length > 10) { obj.codigo = count1.ToString() + " - " + hoja2; obj.nombre = "La informacion del cilindraje del vehículo debe contener máximo 10 caracteres." + " - " + " Celda {I -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        try
                                        {
                                            int _cilindrajeV = Convert.ToInt32(cilindrajeV);
                                        }
                                        catch
                                        {
                                            obj.codigo = count1.ToString() + " - " + hoja2; obj.nombre = "El cilindraje del vehículo tiene un formato incorrecto,  debe ser un entero positivo." + " - " + " Celda {I -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() });
                                        }
                                        // VALIDO SEGURO PRIVADO
                                        if (seguroPrivadoV.Trim() == "") { obj.codigo = count1.ToString() + " - " + hoja2; obj.nombre = "No existe la informacion del seguro privado del  vehículo" + " - " + " Celda {J -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        if (seguroPrivadoV.Trim() != "SI" && seguroPrivadoV.Trim() != "NO") { obj.codigo = count1.ToString() + " - " + hoja2; obj.nombre = "La informacion del seguro privado del  vehículo es incorrecta, debe ser SI ó NO" + " - " + " Celda {J -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        // VALIDO MATERIAL PELIGROSO
                                        if (materialPeligrosoV.Trim() == "") { obj.codigo = count1.ToString() + " - " + hoja2; obj.nombre = "No existe la informacion si transportaba material peligroso" + " - " + " Celda {K -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        int contMaterialPeli = 0;
                                        var materialesPeligrosos = cat.datosTransporteMaterialPeligroso();
                                        foreach (var mp in materialesPeligrosos)
                                        {
                                            if (mp.Text == materialPeligrosoV.Trim())
                                                contMaterialPeli++;
                                        }
                                        if (contMaterialPeli == 0) { obj.codigo = count1.ToString() + " - " + hoja2; obj.nombre = "La informacion del material peligroso seleccionada es incorrecta" + " - " + " Celda {K -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        // valido tipo de servicio
                                        if (tipoServicioV.Trim() == "") { obj.codigo = count1.ToString() + " - " + hoja2; obj.nombre = "No existe la informacion del tipo de servicio del vehículo" + " - " + " Celda {L -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        int contipoServicioV = 0;
                                        var tiposServicios = objSiniestrosNeg.listaServiciosVehiculos();
                                        foreach (var ts in tiposServicios)
                                        {
                                            if (ts.Text == tipoServicioV.Trim())
                                                contipoServicioV++;
                                        }
                                        if (contipoServicioV == 0) { obj.codigo = count1.ToString() + " - " + hoja2; obj.nombre = "La informacion del tipo de servicio del vehiculo seleccionada es incorrecta" + " - " + " Celda {L -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        //valido tipo vehiculo
                                        if (tipoVehiculoV.Trim() == "") { obj.codigo = count1.ToString() + " - " + hoja2; obj.nombre = "No existe la informacion del tipo de  vehículo" + " - " + " Celda {M -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        int conttipoVehiculoV = 0, codtipoVhl = 0;
                                        var tipoVehiculos = objSiniestrosNeg.listaTipoVehiculos();
                                        foreach (var tv in tipoVehiculos)
                                        {
                                            if (tv.Text == tipoVehiculoV)
                                            {
                                                conttipoVehiculoV++;
                                                codtipoVhl = Convert.ToInt32(tv.Value);
                                            }
                                        }
                                        if (conttipoVehiculoV == 0) { obj.codigo = count1.ToString() + " - " + hoja2; obj.nombre = "La informacion del tipo de  vehiculo seleccionada es incorrecta" + " - " + " Celda {M -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }

                                        // VALIDO SUB TIPO VHL
                                        if (subTipoVehiculoV.Trim() == "") { obj.codigo = count1.ToString() + " - " + hoja2; obj.nombre = "No existe la informacion del sub tipo de  vehículo" + " - " + " Celda {N -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        int contsubTipoVehiculoV = 0;
                                        var subTipoVehiculos = objSiniestrosNeg.listaSubTipoVehiculos(codtipoVhl);
                                        foreach (var sv in subTipoVehiculos)
                                        {
                                            if (sv.Text == subTipoVehiculoV)
                                                contsubTipoVehiculoV++;
                                        }
                                        if (contsubTipoVehiculoV == 0) { obj.codigo = count1.ToString() + " - " + hoja2; obj.nombre = "La informacion del sub tipo de  vehiculo  es incorrecta para el tipo de vehiculo seleccionado" + " - " + " Celda {M -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                    }
                                }
                                if (hoja3 == "Victimas" && listaCodificaciones.Count == 0)
                                {
                                    int count1 = 1;
                                    int contadorSinVi = 0;
                                    for (int rowIterator = 2; rowIterator <= noOfRow2; rowIterator++)
                                    {
                                        Model.Datos.CargaDropDownList obj = new Model.Datos.CargaDropDownList();
                                        Model.Datos.CargaDropDownList objti = new Model.Datos.CargaDropDownList();

                                        count1 += 1;
                                        string codsiniestroVi = workSheet2.Cells[rowIterator, 1].Value == null ? "" : workSheet2.Cells[rowIterator, 1].Value.ToString();
                                        string placaVi = workSheet2.Cells[rowIterator, 2].Value == null ? "" : workSheet2.Cells[rowIterator, 2].Value.ToString();
                                        string TipoIdentificacionVi = workSheet2.Cells[rowIterator, 3].Value == null ? "" : workSheet2.Cells[rowIterator, 3].Value.ToString();
                                        string IndentificacionVi = workSheet2.Cells[rowIterator, 4].Value == null ? "" : workSheet2.Cells[rowIterator, 4].Value.ToString();
                                        string edadVi = workSheet2.Cells[rowIterator, 5].Value == null ? "" : workSheet2.Cells[rowIterator, 5].Value.ToString();
                                        string SexoV = workSheet2.Cells[rowIterator, 6].Value == null ? "" : workSheet2.Cells[rowIterator, 6].Value.ToString();
                                        string CondicionVictima24h = workSheet2.Cells[rowIterator, 7].Value == null ? "" : workSheet2.Cells[rowIterator, 7].Value.ToString();
                                        string TipoParticipantevI = workSheet2.Cells[rowIterator, 8].Value == null ? "" : workSheet2.Cells[rowIterator, 8].Value.ToString();
                                        string UsoCascoVi = workSheet2.Cells[rowIterator, 9].Value == null ? "" : workSheet2.Cells[rowIterator, 9].Value.ToString();
                                        string UsoCinturonVi = workSheet2.Cells[rowIterator, 10].Value == null ? "" : workSheet2.Cells[rowIterator, 10].Value.ToString();
                                        string PosicionPlazaVi = workSheet2.Cells[rowIterator, 11].Value == null ? "" : workSheet2.Cells[rowIterator, 11].Value.ToString();
                                        string SospechaConsumoAlcoholVi = workSheet2.Cells[rowIterator, 12].Value == null ? "" : workSheet2.Cells[rowIterator, 12].Value.ToString();


                                        // valido codsiniestroVi
                                        if (codsiniestroVi == "") { obj.codigo = count1.ToString() + " - " + hoja3; obj.nombre = "No existe el codigo del siniestro" + " - " + " Celda {A -" + count1 + "} "; obj.codigo = count1.ToString(); listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        string _codautoridad = "";
                                        try
                                        {
                                            _codautoridad = codsiniestroVi.Substring(0, 3);
                                        }
                                        catch
                                        {
                                            if (_codautoridad != codAutoridad) { obj.codigo = count1.ToString() + " - " + hoja3; obj.nombre = "El codigo del siniestro tiene el formato incorrecto, debe contener el codigo de la autoridad al inicio ej: PNE0120170713" + " - " + " Celda {A -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        }

                                        if (_codautoridad != codAutoridad) { obj.codigo = count1.ToString() + " - " + hoja3; obj.nombre = "El codigo del siniestro tiene una autoridad inválida" + " - " + " Celda {A -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }


                                        if (listaCodificacionesSin.Count > 0)
                                        {
                                            contadorSinVi = 0;
                                            foreach (var ccs in listaCodificacionesSin)
                                            {
                                                if (ccs.Text == codsiniestroVi)
                                                    contadorSinVi++;

                                            }
                                            //codsin_validador[count] = codsiniestro;
                                            if (contadorSinVi == 0)
                                            {
                                                obj.codigo = count1.ToString() + " - " + hoja3; obj.nombre = "El codigo del siniestro " + codsiniestroVi.ToString() + " no se existe en el sheet de Siniestros   " + " - " + " Celda {A -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() });
                                            }
                                        }

                                        //VERIFICO  placaV
                                        int contplacaVi = 0;
                                        if (placaVi.Trim() == "") { obj.codigo = count1.ToString() + " - " + hoja3; obj.nombre = "No existe la placa del vehículo" + " - " + " Celda {B -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }

                                        if (placaVi.Trim() != "" && listaCodificacionesVhl.Count > 0)
                                        {
                                            foreach (var p in listaCodificacionesVhl)
                                            {
                                                if (p.Text == placaVi.Trim())
                                                    contplacaVi++;
                                            }

                                        }
                                        if (contplacaVi == 0) { obj.codigo = count1.ToString() + " - " + hoja3; obj.nombre = "El numero de placa del vehiculo " + placaVi.ToString() + " no se existe en la hoja de Vehiculos   " + " - " + " Celda {B -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        // VALIDO EL TIPO DE IDENTIFICACION
                                        if (TipoIdentificacionVi.Trim() == "") { obj.codigo = count1.ToString() + " - " + hoja3; obj.nombre = "No existe el tipo de identificacion" + " - " + " Celda {C -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        int contTipoIdentificacionVi = 0;
                                        var TipoIdentificaciones = cat.datosTipoIdentificacion();
                                        foreach (var ti in TipoIdentificaciones)
                                        {
                                            if (ti.Text == TipoIdentificacionVi.Trim())
                                                contTipoIdentificacionVi++;
                                        }
                                        if (contTipoIdentificacionVi == 0) { obj.codigo = count1.ToString() + " - " + hoja3; obj.nombre = "El tipo de identificación seleccionado es incorrecto" + " - " + " Celda {C -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        // valido nuemero de identidicacion
                                        if (IndentificacionVi.Trim() == "") { obj.codigo = count1.ToString() + " - " + hoja3; obj.nombre = "Número de identificación se encuentra  vacio" + " - " + " Celda {D -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        if (IndentificacionVi.Trim() != "" && IndentificacionVi.Trim().Length > 20 && TipoIdentificacionVi.Trim() == "PASAPORTE") { obj.codigo = count1.ToString() + " - " + hoja3; obj.nombre = "Número de identificación debe contener máximo 20 caracteres para el tipo de identificación " + TipoIdentificacionVi + " " + " - " + " Celda {D -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        if (IndentificacionVi.Trim() != "" && IndentificacionVi.Trim().Length != 10 && ( TipoIdentificacionVi.Trim() == "CÉDULA" || TipoIdentificacionVi.Trim() == "LICENCIA")) { obj.codigo = count1.ToString() + " - " + hoja3; obj.nombre = "Número de identificación invalido para el tipo de identificación " + TipoIdentificacionVi + " debe contener 10 caracteres " + " - " + " Celda {D -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        if (IndentificacionVi.Trim().Length <1  && (TipoIdentificacionVi.Trim() == "NO IDENTIFICADO" )) { obj.codigo = count1.ToString() + " - " + hoja3; obj.nombre = "Número de identificación invalido para el tipo de identificación " + TipoIdentificacionVi + " debe contener un caracter Ejemplo 0 " + " - " + " Celda {D -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        if (IndentificacionVi.Trim() != "")
                                        {
                                            objti.codigo = count.ToString();
                                            objti.nombre = IndentificacionVi.Trim();
                                            listaCodificacionesVic.Add(new SelectListItem() { Value = objti.codigo.ToString(), Text = objti.nombre.ToString() });

                                        }


                                        // valido edad 
                                        if (edadVi == "") { obj.codigo = count1.ToString() + " - " + hoja3; obj.nombre = "La edad de la victima esta vacia" + " - " + " Celda {E -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        if (edadVi.Trim().Length <2) { obj.codigo = count1.ToString() + " - " + hoja3; obj.nombre = "La edad de la victima debe contener máximo 2 caracteres" + " - " + " Celda {E -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        try
                                        {
                                            int _edad = Convert.ToInt32(edadVi);
                                        }
                                        catch
                                        {
                                            obj.codigo = count1.ToString() + " - " + hoja3; obj.nombre = "La edad de la victima tiene un formato incorrecto" + " - " + " Celda {E -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() });
                                        }
                                        // valido sexo
                                        if (SexoV.Trim() == "") { obj.codigo = count1.ToString() + " - " + hoja3; obj.nombre = "El sexo de la victima se encuentra vacio" + " - " + " Celda {F -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        int contSexoV = 0;
                                        var sexos = cat.datosSexo();
                                        foreach (var s in sexos)
                                        {
                                            if (s.Text == SexoV)
                                                contSexoV++;
                                        }
                                        if (contSexoV == 0) { obj.codigo = count1.ToString() + " - " + hoja3; obj.nombre = "El sexo de la victima seleccionado es incorrecto" + " - " + " Celda {F -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }

                                        // valido CondicionVictima24h
                                        if (CondicionVictima24h.Trim() == "") { obj.codigo = count1.ToString() + " - " + hoja3; obj.nombre = "La condicion  de la victima a 24 horas se encuentra vacia" + " - " + " Celda {G -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        int contCondicionVictima24h = 0;
                                        var codicionesVictimas = cat.datosCondicionVictimas24();
                                        foreach (var cv in codicionesVictimas)
                                        {
                                            if (cv.Text == CondicionVictima24h)
                                                contCondicionVictima24h++;

                                        }
                                        if (contCondicionVictima24h == 0) { obj.codigo = count1.ToString() + " - " + hoja3; obj.nombre = "La condicion  de la victima a 24 horas seleccionada es incorrecta" + " - " + " Celda {G -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        //valido TipoParticipantevI
                                        if (TipoParticipantevI.Trim() == "") { obj.codigo = count1.ToString() + " - " + hoja3; obj.nombre = "El tipo de participante se  encuentra vacio" + " - " + " Celda {H -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        int contTipoParticipantevI = 0;
                                        var TipoParticipantes = cat.datosTipoParticipante();
                                        foreach (var tp in TipoParticipantes)
                                        {
                                            if (tp.Text == TipoParticipantevI)
                                                contTipoParticipantevI++;

                                        }
                                        if (contTipoParticipantevI == 0) { obj.codigo = count1.ToString() + " - " + hoja3; obj.nombre = "El tipo de participante seleccionado es incorrecto " + " - " + " Celda {H -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }

                                        //valido uso casco
                                        if (UsoCascoVi.Trim() == "") { obj.codigo = count1.ToString() + " - " + hoja3; obj.nombre = "La variable  uso de caso  encuentra vacio" + " - " + " Celda {I -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        if (UsoCascoVi.Trim() != "" && (UsoCascoVi.Trim() != "SI" && UsoCascoVi.Trim() != "NO")) { obj.codigo = count1.ToString() + " - " + hoja3; obj.nombre = "La variable  uso de caso  es    incorrecta debe ser SI  ó NO" + " - " + " Celda {I -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        //VALIDO USO CINTURON
                                        if (UsoCinturonVi.Trim() == "") { obj.codigo = count1.ToString() + " - " + hoja3; obj.nombre = "La variable  uso de CINTURON  encuentra vacio" + " - " + " Celda {J -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        if (UsoCinturonVi.Trim() != "" && (UsoCinturonVi.Trim() != "SI" && UsoCinturonVi.Trim() != "NO")) { obj.codigo = count1.ToString() + " - " + hoja3; obj.nombre = "La variable  uso de cinturon es incorrecta debe ser SI  ó NO" + " - " + " Celda {j -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }

                                        // valido PosicionPlaza

                                        if (PosicionPlazaVi.Trim() == "") { obj.codigo = count1.ToString() + " - " + hoja3; obj.nombre = "La variable  posicion de la plaza  encuentra vacio" + " - " + " Celda {K -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        int contPosicionPlazaVi = 0;
                                        var posicionesPlaza = cat.datosPosicionPlaza();
                                        foreach (var pp in posicionesPlaza)
                                        {
                                            if (pp.Text == PosicionPlazaVi.Trim())
                                                contPosicionPlazaVi++;

                                        }
                                        if (contPosicionPlazaVi == 0) { obj.codigo = count1.ToString() + " - " + hoja3; obj.nombre = "La variable  posicion de la plaza seleccionada es incorrecta" + " - " + " Celda {K -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        // variable sospecha consumo alcohol
                                        if (SospechaConsumoAlcoholVi.Trim() == "") { obj.codigo = count1.ToString() + " - " + hoja3; obj.nombre = "La variable sospecha consumo de alcohol encuentra vacio" + " - " + " Celda {L -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        if (SospechaConsumoAlcoholVi.Trim() != "" && (SospechaConsumoAlcoholVi.Trim() != "SI" && SospechaConsumoAlcoholVi.Trim() != "NO")) { obj.codigo = count1.ToString() + " - " + hoja3; obj.nombre = "La variable sospecha consumo de alcohol es incorrecta, debe ser SI ó NO " + " - " + " Celda {L -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                    }
                                }
                                if (hoja4 == "AccionesPeaton" && listaCodificaciones.Count == 0)
                                {
                                    int count1 = 1;
                                    int contadorAP = 0;
                                    for (int rowIterator = 2; rowIterator <= noOfRow3; rowIterator++)
                                    {
                                        Model.Datos.CargaDropDownList obj = new Model.Datos.CargaDropDownList();
                                        count1 += 1;
                                        string codsiniestroA = workSheet3.Cells[rowIterator, 1].Value == null ? "" : workSheet3.Cells[rowIterator, 1].Value.ToString();
                                        string placaA = workSheet3.Cells[rowIterator, 2].Value == null ? "" : workSheet3.Cells[rowIterator, 2].Value.ToString();
                                        string IndentificacionA = workSheet3.Cells[rowIterator, 3].Value == null ? "" : workSheet3.Cells[rowIterator, 3].Value.ToString();
                                        string accionPeaton = workSheet3.Cells[rowIterator, 4].Value == null ? "" : workSheet3.Cells[rowIterator, 4].Value.ToString();

                                        // valido codsiniestroVi
                                        if (codsiniestroA == "") { obj.codigo = count1.ToString() + " - " + hoja4; obj.nombre = "No existe el codigo del siniestro" + " - " + " Celda {A -" + count1 + "} "; obj.codigo = count1.ToString(); listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        string _codautoridad = "";
                                        try
                                        {
                                            _codautoridad = codsiniestroA.Substring(0, 3);
                                        }
                                        catch
                                        {
                                            if (_codautoridad != codAutoridad) { obj.codigo = count1.ToString() + " - " + hoja4; obj.nombre = "El codigo del siniestro tiene el formato incorrecto, debe contener el codigo de la autoridad al inicio ej: PNE0120170713" + " - " + " Celda {A -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        }

                                        if (_codautoridad != codAutoridad) { obj.codigo = count1.ToString() + " - " + hoja4; obj.nombre = "El codigo del siniestro tiene una autoridad inválida" + " - " + " Celda {A -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }


                                        if (listaCodificacionesSin.Count > 0)
                                        {
                                            foreach (var ccs in listaCodificacionesSin)
                                            {
                                                if (ccs.Text == codsiniestroA)
                                                    contadorAP++;

                                            }
                                            //codsin_validador[count] = codsiniestro;
                                            if (contadorAP == 0)
                                            {
                                                obj.codigo = count1.ToString() + " - " + hoja4; obj.nombre = "El codigo del siniestro " + codsiniestroA.ToString() + " no se existe en el sheet de Siniestros   " + " - " + " Celda {A -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() });
                                            }
                                        }
                                        //VERIFICO  placaV
                                        int contplacaVi = 0;
                                        if (placaA.Trim() == "") { obj.codigo = count1.ToString() + " - " + hoja4; obj.nombre = "No existe la placa del vehículo" + " - " + " Celda {B -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }

                                        if (placaA.Trim() != "" && listaCodificacionesVhl.Count > 0)
                                        {
                                            contplacaVi = 0;
                                            foreach (var p in listaCodificacionesVhl)
                                            {
                                                if (p.Text == placaA.Trim())
                                                    contplacaVi++;
                                            }

                                        }
                                        if (contplacaVi == 0) { obj.codigo = count1.ToString() + " - " + hoja4; obj.nombre = "El numero de placa del vehiculo " + placaA.ToString() + " no se existe en el sheet de Vehiculos   " + " - " + " Celda {B -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }

                                        //VERIFICO  la identificacion
                                        int contnumIdent = 0;
                                        if (IndentificacionA.Trim() == "") { obj.codigo = count1.ToString() + " - " + hoja4; obj.nombre = "No existe la placa del vehículo" + " - " + " Celda {C -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }

                                        if (IndentificacionA.Trim() != "" && listaCodificacionesVic.Count > 0)
                                        {
                                            foreach (var p in listaCodificacionesVic)
                                            {
                                                if (p.Text == IndentificacionA.Trim())
                                                    contnumIdent++;
                                            }

                                        }
                                        if (contnumIdent == 0) { obj.codigo = count1.ToString() + " - " + hoja4; obj.nombre = "El numero de identificacion de la victima " + IndentificacionA.ToString() + " no se existe en el sheet de victimas   " + " - " + " Celda {C -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }

                                        // VERIFICO LAS ACCIONES
                                        if (accionPeaton.Trim() == "") { obj.codigo = count1.ToString() + " - " + hoja4; obj.nombre = "El campo de accion peaton se encuentra vacio" + " - " + " Celda {D -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        int contaccionPeaton = 0;
                                        var accionespeatones = cat.datosAccionesPeaton();
                                        foreach (var ap in accionespeatones)
                                        {
                                            if (ap.Text == accionPeaton)
                                                contaccionPeaton++;
                                        }
                                        if (contaccionPeaton == 0) { obj.codigo = count1.ToString() + " - " + hoja4; obj.nombre = "El campo de accion peaton seleccionado es incorrecto" + " - " + " Celda {D -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }

                                    }
                                }


                                if (hoja5 == "DaniosTerceros" && listaCodificaciones.Count == 0)
                                {
                                    int count1 = 1;
                                    int contadord = 0;
                                    for (int rowIterator = 2; rowIterator <= noOfRow4; rowIterator++)
                                    {
                                        Model.Datos.CargaDropDownList obj = new Model.Datos.CargaDropDownList();
                                        count1 += 1;
                                        string codsiniestroA = workSheet4.Cells[rowIterator, 1].Value == null ? "" : workSheet4.Cells[rowIterator, 1].Value.ToString();
                                        string tipoDanio = workSheet4.Cells[rowIterator, 2].Value == null ? "" : workSheet4.Cells[rowIterator, 2].Value.ToString();
                                        string observaciones = workSheet4.Cells[rowIterator, 3].Value == null ? "" : workSheet4.Cells[rowIterator, 3].Value.ToString();

                                        // valido codsiniestroVi
                                        if (codsiniestroA == "") { obj.codigo = count1.ToString() + " - " + hoja5; obj.nombre = "No existe el codigo del siniestro" + " - " + " Celda {A -" + count1 + "} "; obj.codigo = count1.ToString(); listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        string _codautoridad = "";
                                        try
                                        {
                                            _codautoridad = codsiniestroA.Substring(0, 3);
                                        }
                                        catch
                                        {
                                            if (_codautoridad != codAutoridad) { obj.codigo = count1.ToString() + " - " + hoja5; obj.nombre = "El codigo del siniestro tiene el formato incorrecto, debe contener el codigo de la autoridad al inicio ej: PNE0120170713" + " - " + " Celda {A -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }
                                        }

                                        if (_codautoridad != codAutoridad) { obj.codigo = count1.ToString() + " - " + hoja5; obj.nombre = "El codigo del siniestro tiene una autoridad inválida" + " - " + " Celda {A -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }


                                        if (listaCodificacionesSin.Count > 0)
                                        {
                                            contadord = 0;
                                            foreach (var ccs in listaCodificacionesSin)
                                            {
                                                if (ccs.Text == codsiniestroA)
                                                    contadord++;

                                            }
                                            //codsin_validador[count] = codsiniestro;
                                            if (contadord == 0)
                                            {
                                                obj.codigo = count1.ToString() + " - " + hoja5; obj.nombre = "El codigo del siniestro " + codsiniestroA.ToString() + " no se existe en el sheet de Siniestros   " + " - " + " Celda {A -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() });
                                            }
                                        }
                                        //VERIFICO  placaV
                                        int conttipoDanio = 0;
                                        if (tipoDanio.Trim() == "") { obj.codigo = count1.ToString() + " - " + hoja5; obj.nombre = "La variable tipo de daño se encuentra vacia" + " - " + " Celda {B -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }


                                        var tipoDanios = objSiniestrosNeg.listaTipoDaniosTerceros();
                                        foreach (var ap in tipoDanios)
                                        {
                                            if (ap.Text == tipoDanio)
                                                conttipoDanio++;
                                        }
                                        if (conttipoDanio == 0) { obj.codigo = count1.ToString() + " - " + hoja5; obj.nombre = "El campo tipo de daño seleccionado es incorrecto" + " - " + " Celda {B -" + count1 + "} "; listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() }); }

                                    }
                                }

                                var _fecha = fecsin.Split('-');
                                anio = _fecha[0].ToString();
                                mes = _fecha[1].ToString();
                                //mensajeOk = objSiniestrosNeg.EnviaListaCalificaciones(listaCal, anio, mes);


                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    mensajeError = ex.ToString();
                }

            }
           
            if (listaCodificaciones.Count > 0)
            {
                mensajeError = "Existen errores en el archvio a cargar, favor verificar y corregir.";

            }
            else if (mensajeError == "" && listaCodificaciones.Count() == 0)
            {
                try
                {
                    mensajeOk = await procesoCarga(file, fecsin, codAutoridad, Convert.ToInt32(codUsuario));
                    ViewBag.listaVistaSiniestrosCm = objSiniestrosNeg.listaVistaSiniestrosCm(mensajeOk);
                    if (mensajeOk != "")
                    {
                        var drespuesta = mensajeOk.Split(',');
                        objSiniestrosNeg.InsertarCargaMasiva(codAutoridad,Convert.ToInt32( drespuesta.Count()-1),Convert.ToInt32( codUsuario), obscarga.ToUpper());
                        mensajeOk = "Registros cargados correctamente  : Total SINIESTROS ->  " + (drespuesta.Count()-1).ToString();
                        
                    }
                    
                }
                catch
                {
                    mensajeError = "Error al guardar la informacion en la base de datos.";
                }
                
            }
            
            ViewBag.ListaCodificaciones = listaCodificaciones;
            ViewBag.mensajeError = mensajeError;
            ViewBag.mensajeValidacion = mensajeOk;
            ViewBag.cantidadRegistros = listaCodificaciones.Count();
            ViewBag.fileName = nombreFile;
            return View("Index");
        }
        

        [HttpGet]
        [Route("Export")]
        public string Export(string sFileName)
        {
            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            //string sFileName = @"demo.xlsx";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            }
            using (excel.ExcelPackage package = new excel.ExcelPackage(file))
            {
                // add a new worksheet to the empty workbook
                excel.ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Employee");
                //First add the headers
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "Name";
                worksheet.Cells[1, 3].Value = "Gender";
                worksheet.Cells[1, 4].Value = "Salary (in $)";

                //Add values
                worksheet.Cells["A2"].Value = 1000;
                worksheet.Cells["B2"].Value = "Jon";
                worksheet.Cells["C2"].Value = "M";
                worksheet.Cells["D2"].Value = 5000;

                worksheet.Cells["A3"].Value = 1001;
                worksheet.Cells["B3"].Value = "Graham";
                worksheet.Cells["C3"].Value = "M";
                worksheet.Cells["D3"].Value = 10000;

                worksheet.Cells["A4"].Value = 1002;
                worksheet.Cells["B4"].Value = "Jenny";
                worksheet.Cells["C4"].Value = "F";
                worksheet.Cells["D4"].Value = 5000;

                package.Save(); //Save the workbook.
            }
            return URL;
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
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
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

        public bool ValidaRegistrosCargadosFecha(string fecha, string codAutoridad)
        {


            string anio,  mes;
            anio = fecha.Split('-')[0];
            mes = fecha.Split('-')[1];

            bool retorno = true;
            var d = objSiniestrosNeg.listaSiniestrosCm(anio, mes,codAutoridad);
            if (d.Count() > 0)
            {
                retorno = true;
            }
            else
                retorno = false;

            return retorno;
        }

        public bool ValidaCargaMasivaFechaRequerida(string fecha)
        {

            bool retorno = true;

            string aniosin = fecha.Replace('/', '-').Split('-')[0];
            string messin = fecha.Replace('/', '-').Split('-')[1];
            DateTime fecha_actual = DateTime.Now;
            string fechamin = aniosin + "-" + messin + "-" + "01";
            messin = Convert.ToString(Convert.ToInt32(messin) + 1);
            string fechamax = aniosin + "-" + messin + "-" + "10";
            if (Convert.ToDateTime(fechamax) >= Convert.ToDateTime(fecha_actual) && Convert.ToDateTime(fecha) <= Convert.ToDateTime(fechamax) && Convert.ToDateTime(fecha) >= Convert.ToDateTime(fechamin))
            {
                retorno = true;
            }
            else
            { retorno = false; }


            return retorno;
        }

        public class DatosCargaMasiva
        {
            public string codigo { get; set; }
            public string Descripcion { get; set; }
        }

        [HttpPost]


        public async Task<string> procesoCarga(ICollection<IFormFile> file, string fecsin, string  codautoridad,int codusuario   )
        {
            string codigosSiniestros = "";
            string resultado = "";
            try
            {
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploadsCm");
                
                foreach (var f in file)
                {
                    if (f.Length > 0)
                    {
                        var fileStream = new FileStream(Path.Combine(uploads, f.FileName), FileMode.Create);
                        await f.CopyToAsync(fileStream);
                        fileStream.Dispose();

                        string fileName = f.FileName;// ;//esta line para win
                        string fileContentType = f.ContentType;
                        byte[] fileBytes = new byte[f.Length];
                        //string sWebRootFolder = _hostingEnvironment.WebRootPath + "\\uploadsCal";  //esta line para win
                        string sWebRootFolder = _hostingEnvironment.WebRootPath + "/uploadsCm/";  //esta line para linux
                        FileInfo files = new FileInfo(Path.Combine(sWebRootFolder, fileName));
                        //   nombreFile = fileName;
                        using (var package = new ExcelPackage(files))
                        {


                            var currentSheet = package.Workbook.Worksheets;
                            var workSheet = currentSheet.First();
                            var workSheet1 = currentSheet[2];
                            var workSheet2 = currentSheet[3];
                            var workSheet3 = currentSheet[4];
                            var workSheet4 = currentSheet[5];
                            var noOfCol = workSheet.Dimension.End.Column;
                            var noOfRow = workSheet.Dimension.End.Row;
                            var noOfRow1 = workSheet1.Dimension.End.Row;
                            var noOfRow2 = workSheet2.Dimension.End.Row;
                            var noOfRow3 = workSheet3.Dimension.End.Row;
                            var noOfRow4 = workSheet4.Dimension.End.Row;



                            string hoja1 = workSheet.Name;
                            string hoja2 = workSheet1.Name;
                            string hoja3 = workSheet2.Name;
                            string hoja4 = workSheet3.Name;
                            string hoja5 = workSheet4.Name;
                            string retornoCodVic = "";
                            if (hoja1 == "Siniestro")
                            {

                                for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)// recorro el siniestro
                                {
                                   // count += 1;
                                    Model.Datos.CargaDropDownList obj = new Model.Datos.CargaDropDownList();
                                    Model.Datos.CargaDropDownList objSin = new Model.Datos.CargaDropDownList();
                                    //obj.codigo = Convert.ToString(count++);
                                    //obj.nombre = Convert.ToString(descripcion);
                                    //listaCodificaciones.Add(new SelectListItem() { Value = obj.codigo.ToString(), Text = obj.nombre.ToString() });

                                    string codsiniestro = workSheet.Cells[rowIterator, 1].Value == null ? "" : workSheet.Cells[rowIterator, 1].Value.ToString();
                                    string fechaSiniestro = workSheet.Cells[rowIterator, 2].Value == null ? "" : workSheet.Cells[rowIterator, 2].Value.ToString();
                                    string horaSiniestro = workSheet.Cells[rowIterator, 3].Value == null ? "" : workSheet.Cells[rowIterator, 3].Value.ToString();
                                    string latitud = workSheet.Cells[rowIterator, 4].Value == null ? "" : workSheet.Cells[rowIterator, 4].Value.ToString();
                                    string longuitud = workSheet.Cells[rowIterator, 5].Value == null ? "" : workSheet.Cells[rowIterator, 5].Value.ToString();

                                    string provincia = workSheet.Cells[rowIterator, 6].Value == null ? "" : workSheet.Cells[rowIterator, 6].Value.ToString();
                                    string canton = workSheet.Cells[rowIterator, 7].Value == null ? "" : workSheet.Cells[rowIterator, 7].Value.ToString();
                                    string parroquia = workSheet.Cells[rowIterator, 8].Value == null ? "" : workSheet.Cells[rowIterator, 8].Value.ToString();
                                    // string distrito = workSheet.Cells[rowIterator, 9].Value == null ? "" : workSheet.Cells[rowIterator, 9].Value.ToString();
                                    //  string circuito = workSheet.Cells[rowIterator, 10].Value == null ? "" : workSheet.Cells[rowIterator, 10].Value.ToString();

                                    string zona = "";// workSheet.Cells[rowIterator, 11].Value == null ? "" : workSheet.Cells[rowIterator, 11].Value.ToString();
                                    string direccion = workSheet.Cells[rowIterator, 9].Value == null ? "" : workSheet.Cells[rowIterator, 9].Value.ToString();
                                 //   string numerofallecidos = workSheet.Cells[rowIterator, 13].Value == null ? "" : workSheet.Cells[rowIterator, 13].Value.ToString();
                                  //  string numerolesionados = workSheet.Cells[rowIterator, 14].Value == null ? "" : workSheet.Cells[rowIterator, 14].Value.ToString();
                                    string condicionAtmosferica = workSheet.Cells[rowIterator, 10].Value == null ? "" : workSheet.Cells[rowIterator, 10].Value.ToString();

                                    string condicionVia = workSheet.Cells[rowIterator, 11].Value == null ? "" : workSheet.Cells[rowIterator, 11].Value.ToString();
                                    string tipoVia = workSheet.Cells[rowIterator, 12].Value == null ? "" : workSheet.Cells[rowIterator, 12].Value.ToString();
                                    string limiteVelocidad = workSheet.Cells[rowIterator, 13].Value == null ? "" : workSheet.Cells[rowIterator, 13].Value.ToString();
                                    string trabajosVia = workSheet.Cells[rowIterator, 14].Value == null ? "" : workSheet.Cells[rowIterator, 14].Value.ToString();
                                    string numeroCarrilles = workSheet.Cells[rowIterator, 15].Value == null ? "" : workSheet.Cells[rowIterator, 15].Value.ToString();

                                    string MaterialSuperfice = workSheet.Cells[rowIterator, 16].Value == null ? "" : workSheet.Cells[rowIterator, 16].Value.ToString();
                                    string ControlInterseccion = workSheet.Cells[rowIterator, 17].Value == null ? "" : workSheet.Cells[rowIterator, 17].Value.ToString();
                                    string ObtsaculosVia = workSheet.Cells[rowIterator, 18].Value == null ? "" : workSheet.Cells[rowIterator, 18].Value.ToString();
                                    string LugarVia = workSheet.Cells[rowIterator, 19].Value == null ? "" : workSheet.Cells[rowIterator, 19].Value.ToString();
                                    string CurvaExistente = workSheet.Cells[rowIterator, 20].Value == null ? "" : workSheet.Cells[rowIterator, 20].Value.ToString();

                                    string SeñalizacionExistente = workSheet.Cells[rowIterator, 21].Value == null ? "" : workSheet.Cells[rowIterator, 21].Value.ToString();
                                    string TipoSiniestro = workSheet.Cells[rowIterator, 22].Value == null ? "" : workSheet.Cells[rowIterator, 22].Value.ToString();
                                    string CausaProbable = workSheet.Cells[rowIterator, 23].Value == null ? "" : workSheet.Cells[rowIterator, 23].Value.ToString();
                                    string LuzArtificial = workSheet.Cells[rowIterator, 24].Value == null ? "" : workSheet.Cells[rowIterator, 24].Value == null ? "" : workSheet.Cells[rowIterator, 29].Value.ToString();
                                    int numfalsin = 0;
                                    int numlessin = 0;
                                    int codtipsin = 0;
                                    int codpar = 0;
                                    string codsubcir = "";
                                    int codcant = 0;
                                    int codprov = 0;
                                    string codcaupro = "";
                                    string codcaurea = "C00";
                                    string codcir = "";
                                    string coddis = "";

                                    var provincias = objSiniestrosNeg.listaProvincias();
                                    foreach (var p in provincias)
                                    {
                                        if (p.nomprov == provincia)
                                        {
                                            
                                            codprov = p.codprov;
                                        }

                                    }

                                    var cantones = objSiniestrosNeg.listaCantones(codprov);
                                    foreach (var c in cantones)
                                    {
                                        if (c.Text.ToString() == canton)
                                        {

                                            codcant = Convert.ToInt32(c.Value);
                                        }

                                    }

                                    var parroquias = objSiniestrosNeg.listaParroquiasPorCantones(codcant, codprov);
                                    foreach (var par in parroquias)
                                    {
                                        if (par.Text.ToString() == parroquia)
                                        {
                                            codpar = Convert.ToInt32(par.Value);
                                        }
                                    }

                                    //var distritos = objSiniestrosNeg.listaDistritos(codprov, codcant);
                                    //foreach (var d in distritos)
                                    //{
                                    //    if (d.Text == distrito)
                                    //    {
                                    //        coddis = d.Value;
                                    //    }
                                    //}

                                    //var circuitos = objSiniestrosNeg.listaCiruitos(codcant, codprov);
                                    //foreach (var c in circuitos)
                                    //{
                                    //    if (c.Text == circuito)
                                    //    {

                                    //        codcir = c.Value;
                                    //    }
                                    //}

                                    var TipoSiniestros = objSiniestrosNeg.listatipoSiniestros();
                                    foreach (var ts in TipoSiniestros)
                                    {
                                        if (ts.Text == TipoSiniestro)
                                            codtipsin = Convert.ToInt32(ts.Value);


                                    }

                                    var CausaProbables = objSiniestrosNeg.listaCausaProbableSiniestros();
                                    foreach (var cp in CausaProbables)
                                    {
                                        if (cp.Text == CausaProbable)
                                            codcaupro = cp.Value.ToString();

                                    }
                                    foreach (var datos in objSiniestrosNeg.buscaCircuitoZona(codprov, codcant, codpar))
                                    {

                                        zona = datos.zona.ToString();
                                        if (datos.nombre.ToString() == null || datos.nombre.ToString() == "")
                                            codcir = "-1";
                                        else
                                            codcir = datos.nombre.ToString();

                                        if (datos.codigo.ToString() == null || datos.codigo.ToString() == "")
                                            coddis = "-1";
                                        else
                                            coddis = datos.codigo.ToString();
                                    }
                                    resultado = objSiniestrosNeg.GuardaSiniestros(Convert.ToDateTime(fechaSiniestro), horaSiniestro, latitud.Replace(',', '.'), longuitud.Replace(',', '.'), direccion.ToUpper(), numfalsin, numlessin, 1, false, Convert.ToInt32(codusuario), Convert.ToInt32(codusuario), zona, trabajosVia== "SI" ? true : false, condicionAtmosferica, condicionVia, LuzArtificial, tipoVia,Convert.ToInt32( limiteVelocidad), ControlInterseccion, MaterialSuperfice, ObtsaculosVia, LugarVia, CurvaExistente, Convert.ToInt32( numeroCarrilles), SeñalizacionExistente, Convert.ToInt32(codusuario), codautoridad.ToUpper(), 
                                        codtipsin, codpar, codsubcir, codcant, codprov, codcaupro, codcaurea, codcir, coddis);
                                    if (resultado != "")
                                    {
                                        Vehiculo v = new Vehiculo();
                                        Victimas vic = new Victimas();
                                        AccionesPeaton ap = new AccionesPeaton();
                                        DanioMaterial dm = new DanioMaterial();
                                        Model.Datos.CatalogosSiniestros cat = new Model.Datos.CatalogosSiniestros();
                                        string respuestaVhl = "";
                                        if (hoja2 == "Vehiculos" && listaCodificaciones.Count == 0 && resultado != "0" && resultado != "")
                                        {
                                            int count1 = 1;
                                            int contadorSinV = 0;
                                            int codtipve = 0;
                                            int codser = 0;
                                            int codsubtipoVHL = 0;
                                            for (int rowIteratorV = 2; rowIteratorV <= noOfRow1; rowIteratorV++)// recorro vhl
                                            {

                                                Model.Datos.CargaDropDownList objVe = new Model.Datos.CargaDropDownList();
                                                count1 += 1;
                                                string codsiniestroV = workSheet1.Cells[rowIteratorV, 1].Value == null ? "" : workSheet1.Cells[rowIteratorV, 1].Value.ToString();
                                                string placaV = workSheet1.Cells[rowIteratorV, 2].Value == null ? "" : workSheet1.Cells[rowIteratorV, 2].Value.ToString();
                                                string danioMaterialV = workSheet1.Cells[rowIteratorV, 3].Value == null ? "" : workSheet1.Cells[rowIteratorV, 3].Value.ToString();
                                                string matriculaVigenteV = workSheet1.Cells[rowIteratorV, 4].Value == null ? "" : workSheet1.Cells[rowIteratorV, 4].Value.ToString();
                                                string chasisV = workSheet1.Cells[rowIteratorV, 5].Value == null ? "" : workSheet1.Cells[rowIteratorV, 5].Value.ToString();
                                                string marcaV = workSheet1.Cells[rowIteratorV, 6].Value == null ? "" : workSheet1.Cells[rowIteratorV, 6].Value.ToString();
                                                string modeloV = workSheet1.Cells[rowIteratorV, 7].Value == null ? "" : workSheet1.Cells[rowIteratorV, 7].Value.ToString();
                                                string anioV = workSheet1.Cells[rowIteratorV, 8].Value == null ? "" : workSheet1.Cells[rowIteratorV, 8].Value.ToString();
                                                string cilindrajeV = workSheet1.Cells[rowIteratorV, 9].Value == null ? "" : workSheet1.Cells[rowIteratorV, 9].Value.ToString();
                                                string seguroPrivadoV = workSheet1.Cells[rowIteratorV, 10].Value == null ? "" : workSheet1.Cells[rowIteratorV, 10].Value.ToString();
                                                string materialPeligrosoV = workSheet1.Cells[rowIteratorV, 11].Value == null ? "" : workSheet1.Cells[rowIteratorV, 11].Value.ToString();
                                                string tipoServicioV = workSheet1.Cells[rowIteratorV, 12].Value == null ? "" : workSheet1.Cells[rowIteratorV, 12].Value.ToString();
                                                string tipoVehiculoV = workSheet1.Cells[rowIteratorV, 13].Value == null ? "" : workSheet1.Cells[rowIteratorV, 13].Value.ToString();
                                                string subTipoVehiculoV = workSheet1.Cells[rowIteratorV, 14].Value == null ? "" : workSheet1.Cells[rowIteratorV, 14].Value.ToString();

                                                if (codsiniestroV == codsiniestro)// verifico el codigo del siniestro vs el codigo del siniestro de vhl
                                                {
                                                    var tipoVehiculos = objSiniestrosNeg.listaTipoVehiculos();
                                                    foreach (var tv in tipoVehiculos)
                                                    {
                                                        if (tv.Text == tipoVehiculoV)
                                                        {
                                                            codtipve = Convert.ToInt32(tv.Value);
                                                        }
                                                    }
                                                    var tiposServicios = objSiniestrosNeg.listaServiciosVehiculos();
                                                    foreach (var ts in tiposServicios)
                                                    {
                                                        if (ts.Text == tipoServicioV.Trim())
                                                            codser = Convert.ToInt32(ts.Value);
                                                    }
                                                    var subTipoVehiculos = objSiniestrosNeg.listaSubTipoVehiculos(codtipve);
                                                    foreach (var sv in subTipoVehiculos)
                                                    {
                                                        if (sv.Text == subTipoVehiculoV)
                                                            codsubtipoVHL = Convert.ToInt32(sv.Value);
                                                    }

                                                    v.placvehinv = placaV;
                                                    v.danmatvehinv = danioMaterialV == "SI" ? true : false;
                                                    v.matvigvehinv = matriculaVigenteV == "SI" ? true : false;
                                                    v.chavehinv = chasisV;
                                                    v.marvehinv = marcaV;
                                                    v.modvehinv = modeloV;
                                                    v.cilvehinv = Convert.ToString(cilindrajeV);
                                                    v.segprivehinv = seguroPrivadoV == "SI" ? true : false;
                                                    v.matpelvehinv = materialPeligrosoV;
                                                    v.anivehinv = Convert.ToInt32(anioV);
                                                    v.codser = Convert.ToInt32(codser);
                                                    v.codtipve = Convert.ToInt32(codtipve);
                                                    v.codsin = Convert.ToInt32(resultado);
                                                    v.codsubtipoVHL = codsubtipoVHL;
                                                    respuestaVhl = objSiniestrosNeg.insertaVehiculosInvolucrados(v);// guardo el vehiculo
                                                    if (hoja3 == "Victimas" && listaCodificaciones.Count == 0 && respuestaVhl != "" && respuestaVhl != "0") // verifico para guardar las victimas
                                                    {
                                                        int countv = 1;
                                                        string codsexo = "";
                                                        //  int codtipve = 0;
                                                        // int codser = 0;
                                                        //int codsubtipoVHL = 0;
                                                        for (int rowIteratorVic = 2; rowIteratorVic <= noOfRow2; rowIteratorVic++)//recorrro la hoja para guardar las victimas
                                                        {
                                                            //Model.Datos.CargaDropDownList obj = new Model.Datos.CargaDropDownList();
                                                            Model.Datos.CargaDropDownList objti = new Model.Datos.CargaDropDownList();

                                                            count1 += 1;
                                                            string codsiniestroVi = workSheet2.Cells[rowIteratorVic, 1].Value == null ? "" : workSheet2.Cells[rowIteratorVic, 1].Value.ToString();
                                                            string placaVi = workSheet2.Cells[rowIteratorVic, 2].Value == null ? "" : workSheet2.Cells[rowIteratorVic, 2].Value.ToString();
                                                            string TipoIdentificacionVi = workSheet2.Cells[rowIteratorVic, 3].Value == null ? "" : workSheet2.Cells[rowIteratorVic, 3].Value.ToString();
                                                            string IndentificacionVi = workSheet2.Cells[rowIteratorVic, 4].Value == null ? "" : workSheet2.Cells[rowIteratorVic, 4].Value.ToString();
                                                            string edadVi = workSheet2.Cells[rowIteratorVic, 5].Value == null ? "" : workSheet2.Cells[rowIteratorVic, 5].Value.ToString();
                                                            string SexoV = workSheet2.Cells[rowIteratorVic, 6].Value == null ? "" : workSheet2.Cells[rowIteratorVic, 6].Value.ToString();
                                                            string CondicionVictima24h = workSheet2.Cells[rowIteratorVic, 7].Value == null ? "" : workSheet2.Cells[rowIteratorVic, 7].Value.ToString();
                                                            string TipoParticipantevI = workSheet2.Cells[rowIteratorVic, 8].Value == null ? "" : workSheet2.Cells[rowIteratorVic, 8].Value.ToString();
                                                            string UsoCascoVi = workSheet2.Cells[rowIteratorVic, 9].Value == null ? "" : workSheet2.Cells[rowIteratorVic, 9].Value.ToString();
                                                            string UsoCinturonVi = workSheet2.Cells[rowIteratorVic, 10].Value == null ? "" : workSheet2.Cells[rowIteratorVic, 10].Value.ToString();
                                                            string PosicionPlazaVi = workSheet2.Cells[rowIteratorVic, 11].Value == null ? "" : workSheet2.Cells[rowIteratorVic, 11].Value.ToString();
                                                            string SospechaConsumoAlcoholVi = workSheet2.Cells[rowIteratorVic, 12].Value == null ? "" : workSheet2.Cells[rowIteratorVic, 12].Value.ToString();

                                                            var sexos = cat.datosSexo();
                                                            foreach (var s in sexos)
                                                            {
                                                                if (s.Text == SexoV)
                                                                    codsexo = Convert.ToString(s.Value);

                                                            }

                                                            if (codsiniestroVi == codsiniestro && v.placvehinv == placaVi.Trim() && respuestaVhl != "" && respuestaVhl != "0")
                                                            {
                                                                vic.tipidenvicinv = TipoIdentificacionVi;
                                                                vic.numidenvicinv = IndentificacionVi;
                                                                vic.edavicinv = Convert.ToInt32(edadVi);
                                                                vic.genvicinv = Convert.ToChar(codsexo);
                                                                vic.convicinv24 = CondicionVictima24h;
                                                                vic.convicinv30 = "Ninguno";
                                                                vic.tipparvicinv = TipoParticipantevI;
                                                                vic.casvicinv = UsoCascoVi == "SI" ? true : false;
                                                                vic.cinvicinv = UsoCinturonVi == "SI" ? true : false;
                                                                vic.posvicinv = PosicionPlazaVi;
                                                                vic.conalcvicinv = SospechaConsumoAlcoholVi == "SI" ? true : false;
                                                                vic.codsin = Convert.ToInt32(resultado);
                                                                vic.codveh = Convert.ToInt32(respuestaVhl);
                                                                vic.desaccpea = ""; //desaccpea == null ? "": desaccpea.ToString();
                                                                retornoCodVic = objSiniestrosNeg.insertarVictimasInvolucradas(vic);

                                                                if (retornoCodVic != "0" && retornoCodVic != "" && hoja4 == "AccionesPeaton" && listaCodificaciones.Count == 0 && (vic.tipparvicinv == "PEATÓN" || vic.tipparvicinv == "CONDUCTOR"))// verifico para ver informacion de accion del peaton
                                                                {
                                                                    for (int rowIteratorAcp = 2; rowIteratorAcp <= noOfRow3; rowIteratorAcp++)
                                                                    {

                                                                        count1 += 1;
                                                                        string codsiniestroA = workSheet3.Cells[rowIteratorAcp, 1].Value == null ? "" : workSheet3.Cells[rowIteratorAcp, 1].Value.ToString();
                                                                        string placaA = workSheet3.Cells[rowIteratorAcp, 2].Value == null ? "" : workSheet3.Cells[rowIteratorAcp, 2].Value.ToString();
                                                                        string IndentificacionA = workSheet3.Cells[rowIteratorAcp, 3].Value == null ? "" : workSheet3.Cells[rowIteratorAcp, 3].Value.ToString();
                                                                        string accionPeaton = workSheet3.Cells[rowIteratorAcp, 4].Value == null ? "" : workSheet3.Cells[rowIteratorAcp, 4].Value.ToString();
                                                                        if (codsiniestroA == codsiniestro && retornoCodVic != "0" && vic.numidenvicinv == IndentificacionA.Trim())
                                                                        {
                                                                            string retornoAcp = objSiniestrosNeg.insertarAccionesPeaton(accionPeaton, Convert.ToInt32(retornoCodVic));
                                                                        }
                                                                    }

                                                                }
                                                            }

                                                        }
                                                    }
                                                }

                                                
                                            }
                                        }// fin vehiculos
                                        if (hoja5 == "DaniosTerceros" && listaCodificaciones.Count == 0 && resultado != "0" && resultado != "")// verifico para danios terceros
                                        {
                                            for (int rowIteratordt = 2; rowIteratordt <= noOfRow4; rowIteratordt++)
                                            {
                                                string codsiniestroA = workSheet4.Cells[rowIteratordt, 1].Value == null ? "" : workSheet4.Cells[rowIteratordt, 1].Value.ToString();
                                                string tipoDanio = workSheet4.Cells[rowIteratordt, 2].Value == null ? "" : workSheet4.Cells[rowIteratordt, 2].Value.ToString();
                                                string observaciones = workSheet4.Cells[rowIteratordt, 3].Value == null ? "" : workSheet4.Cells[rowIteratordt, 3].Value.ToString();
                                                int _codtipdater = 0;
                                                var tipoDanios = objSiniestrosNeg.listaTipoDaniosTerceros();
                                                foreach (var tp in tipoDanios)
                                                {
                                                    if (tp.Text == tipoDanio)
                                                    {
                                                        _codtipdater = Convert.ToInt32(tp.Value);
                                                    }
                                                }
                                                if (codsiniestroA == codsiniestro)
                                                {
                                                    dm.codsin =Convert.ToInt32(resultado);
                                                    dm.codtipdater = _codtipdater;
                                                    dm.obsdater = observaciones.ToString().ToUpper();
                                                    var retorno = objSiniestrosNeg.insertarDaniosTerceros(dm);
                                                }
                                            }
                                       }
                                        codigosSiniestros += resultado + ",";
                                    }// fin resulatdo

                                }// fin for siniesros

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return codigosSiniestros;
        }

        

        public FileResult DownloadFormatoCargaMasiva()
        {
            var retorno = "0";//
            string nombreArchivo = "";
            nombreArchivo = "FormatoCargaMasiva.xlsm";
            //retorno = DescargaExcel(nombreArchivo, id);
            var fileName = nombreArchivo;
            string sWebRootFolder = _hostingEnvironment.WebRootPath + "/uploads";
            //var filepath = $"C:/uploadSin/{fileName}";
            var filepath = sWebRootFolder + "/" + fileName;
            byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
            return File(fileBytes, "application/x-msdownload", fileName);
        }


        public IActionResult GetHola()
        {
            HttpContext.Session.Clear();
            //var name = HttpContext.Session.GetString(SessionKeyUsuario);
            return RedirectToAction("Index", "Login");
        }


    }
}
