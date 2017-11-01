using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Entity
{
    public class Georeferencias
    {
        public int codgeo { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public string latgeo { get; set; }
        public string longeo { get; set; }

        public string usuario { get; set; }
        public string autoridad { get; set; }
        public string observaciones { get; set; }

        public string fotprigeo { get; set; }
        public string fotsegeo { get; set; }
    }
}
