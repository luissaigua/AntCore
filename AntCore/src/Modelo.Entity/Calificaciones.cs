using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Entity
{
    public class Calificaciones
    {
        public int codcal { get; set; }
        public string feccal { get; set; }
        public string cumcal { get; set; }
        public string puncal { get; set; }
        public string calcal { get; set; }
        public string comcal { get; set; }

        public string totcal { get; set; }

        public int codprov { get; set; }
        public string nombreProvincia { get; set; }
        public string anio { get; set; }
        public string mes { get; set; }

        public string fecinical { get; set; }
        public string fecFin { get; set; }

        public string reemplezar { get; set; }


    }

}
