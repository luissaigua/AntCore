using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Entity
{
    public class Consultas
    {

        public string codsin { get; set; }

        [Required(ErrorMessage = "Digite el número de identificación de la víctima.")]
        [MaxLength(15, ErrorMessage = "El número de identificación de la víctima debe ntener maximo 10 dígitos")]
        public string id { get; set; }
        public string fecsin { get; set; }

        public string numidenvicinv { get; set; }
        public string nomprov { get; set; }

        public string descant { get; set; }
        public string convicinv24 { get; set; }
        public string nomvicinv { get; set; }

        
    }
}
