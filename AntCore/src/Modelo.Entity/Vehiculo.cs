using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Entity
{
    public class Vehiculo
    {
        public int codvehinv { get; set; }
        public string placvehinv { get; set; }
        public bool danmatvehinv { get; set; }
        public bool matvigvehinv { get; set; }
        public string chavehinv { get; set; }
        public string marvehinv { get; set; }
        public string modvehinv { get; set; }
        public int anivehinv { get; set; }
        public string cilvehinv { get; set; }
        public bool segprivehinv { get; set; }
        public string matpelvehinv { get; set; }
        public int codser { get; set; }
        public int codtipve { get; set; }
        public int codsin { get; set; }
        public string desser { get; set; }
        public string destipveh { get; set; }
        public SelectList provinciasLista { get; set; }
        public string tipoVehiculo { get; set; }
        public string matriculaVigente { get; set; }
        public int codsubtipoVHL { get; set; }
        public string dessubveh { get; set; }
        public int codsubveh { get; set; }

    }
}
