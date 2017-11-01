using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Entity
{
    public class Georeferencia
    {
        //[Required]
        [Display(Name = "Latidud")]
        public double LATGEO { get; set; }

        //[Required]
        [Display(Name = "Longitud")]
        public double LONGEO { get; set; }

        //[Required]
        [Display(Name = "Fecha Geolocalización")]
        public DateTime FECGEO { get; set; }

        //[Required]
        [Display(Name = "Primera Foto")]
        public String FOTPRIGEO{ get; set; }



        //[Required]
        [Display(Name = "Segunda Foto")]
        public string FOTSEGEO { get; set; }

        //[Required]
        [Display(Name = "Observación")]
        public string OBSGEO { get; set; }


        [Display(Name = "Codigo Siniestro")]
        public string CODSIN { get; set; }

        [Display(Name = "Validación")]
        public int VALGEO { get; set; }

        //[Required]
        [Display(Name = "Codigo Autoridad")]
        public string CODAUT { get; set; }

        //[Required]
        [Display(Name = "Codigo usuario")]
        public int PUCOD { get; set; }

        public int CODGEO { get; set; }


    }
}
