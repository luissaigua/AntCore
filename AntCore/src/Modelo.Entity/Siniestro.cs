using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Modelo.Entity
{
    public class Siniestro
    {
        /***********************************
       ENTIDADES DE LA TABLA SINISTROS
       ***********************************/

        [DataType(DataType.Date)]
        [Required]
        [DefaultValue("2017-06-07")]
        public string fecsin { get; set ; }

        [Required]
        public string horsin { get; set; }
        public string latsin { get; set; }
        public string lonsin { get; set; }

        [Required]
        public string dirsin { get; set; }

        [Required]
        public int numfalsin { get; set; }

        [Required]
        public int numlessin { get; set; }

        [Required]
        public string estsin { get; set; }

        [Required]
        public bool regvalsin { get; set; }

        [Required]
        public int ageressin { get; set; }

        [Required]
        public int supressIn { get; set; }

        [Required]
        public string zonsin { get; set; }

        [Required]
        public bool traviasin { get; set; }

        [Required]
        public string conatmsin { get; set; }

        [Required]
        public string conviasin { get; set; }

        [Required]
        public string luzartsin { get; set; }

        [Required]
        public string desviasin { get; set; }

        [Required]
        public int limvelsin { get; set; }

        [Required]
        public string intsin { get; set; }

        [Required]
        public string matsupviasin { get; set; }

        [Required]
        public string obsviasin { get; set; }

        [Required]
        public string lugviasin { get; set; }

        [Required]
        public string cursin { get; set; }

        [Required]
        public int numcarsin { get; set; }

       

        [Required]
        public int PUCOD { get; set; }

        [Required]
        public string codaut { get; set; }

        [Required]
        public string codcausin { get; set; }

        [Required]
        public int codtipsin { get; set; }

        [Required]
        public int codpar { get; set; }

        [Required]
        public string codsubcir { get; set; }

        [Required]
        public string codcir { get; set; }

        
        public int? codcant { get; set; }

        [Required]
        public int codprov { get; set; }


        [Required]
        public string sensin { get; set; }
        public string codcaupro { get; set; }
        public string coddis { get; set; }

        public string codestprocsin { get; set; }
        


        /*******************************************************
         * * VARIABLES DEL VEHICULO INVOLUCRADO
         * ******************************************************/
        [Required(ErrorMessage = "Ingrese la placa")]
        public string placvehinv { get; set; }
        public bool danmatvehinv { get; set; }
        public bool matvigvehinv { get; set; }

        [Required(ErrorMessage = "Ingrese el chasis")]
        public string chavehinv { get; set; }

        [Required(ErrorMessage = "Ingrese la marca")]
        public string marvehinv { get; set; }

        [Required(ErrorMessage = "Ingrese el modelo")]
        public string modvehinv { get; set; }

        [Required(ErrorMessage = "Ingrese el año")]
        public int anivehinv { get; set; }

        [Required(ErrorMessage = "Ingrese el cilindraje")]
        public int cilvehinv { get; set; }

        
        public bool segprivehinv { get; set; }

        [Required(ErrorMessage = "Seleccione el material peligroso")]
        public string matpelvehinv { get; set; }

        [Required(ErrorMessage = "Seleccione el tipo de VHL")]
        public int codtipve { get; set; }





       // [Required(ErrorMessage = "Para ingresar un vehiculo debe ingresar primero el siniestro")]
        public int codsin { get; set; } // codigo del siniestro
        public string nomprov { get; set; }

        
        public string descant { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Provincia")]
        [UIHint("List")]
        
        public SelectList provinciasLista { get; set; }
        public SelectList ciudadesLista { get; set; }
        public string desser { get; set; }


        [Required(ErrorMessage = "Seleccione el tipo de servicio")]
        public int codser { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("tipoServicioVehiculosLista")]
        [UIHint("List")]
        public SelectList tipoServicioVehiculosLista { get; set; }
        public int codtipveh { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("tipoVehiculoLista")]
        [UIHint("List")]
        public SelectList tipoVehiculoLista { get; set; }
        public string destipveh { get; set; }

        public string codigo { get; set; }
        public string nombre { get; set; }
        public SelectList tipoZonaLista;


        public int coddater { get; set; }
        public string obsdater { get; set; }

        [Required(ErrorMessage = "Seleccione el tipo de daño")]
        public int codtipdater { get; set; }

        public string agente_responsable { get; set; }
        public string supervisor_responsable { get; set; }
        public string USUARIO_REGISTRO { get; set; }
        public string REGISTRO_VALIDADO { get; set; }
        
        public string autoridad { get; set; }
        public string tiposiniestro { get; set; }
        public string parroquia { get; set; }
        public string subcircuito { get; set; }

        public string canton { get; set; }

        public string provincia { get; set; }

        public string causa_probable { get; set; }

        public string causa_real { get; set; }
        public string circuito { get; set; }
        public string distrito { get; set; }

        public SelectList EmployeeList { get; set; }
        public string fecIni { get; set; }
        public string fecFin { get; set; }

        public string obscarga { get; set; }
        public string fecsinCM { get; set; }
        
        public string desprocsin { get; set; }
        public string codprocsin { get; set; }
        public string fotprigeo { get; set; }
        public string fotsegeo { get; set; }

        public string num_sininiestros { get; set; }
        public string num_sininiestros_val { get; set; }
        public string num_vehiculos { get; set; }
        public string num_victimas { get; set; }
        public string num_acciones { get; set; }
        public string num_danios_terceros { get; set; }

        public string fecha_hasta { get; set; }
        public string fecha_desde { get; set; }
    }



}
