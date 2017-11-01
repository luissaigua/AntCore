using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Modelo.Entity
{
    public class DanioMaterial
    {
          /******************************************
           * ENTIDADES DE LA TABLA DANIOS MATERIALES
           * ******************************************/
        public string obsdater { get; set; }

        public int coddater { get; set; } // codigo tipo de danio tercero
        public int codtipdater { get; set; } // codigo tipo de danio material
        public int codsin { get; set; } // codigo del siniestro

        public string destipdater { get; set; }
    }
}
