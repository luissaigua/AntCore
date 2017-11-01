using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Entity
{
    public class Catalogos
    {
        public string codigo { get; set; }
        public string nombre { get; set; }
        public SelectList tipoZonaLista;
    }

  
}
