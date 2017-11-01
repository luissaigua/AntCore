using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Entity
{
    public class Funciones : BaseEntity
    {
        [Display(Name = "Codigo")]
        public int PFCOD { get; set; }
        [Required]
        [Display(Name = "Nombre Funcion")]
        [StringLength(80)]
        public string PF000 { get; set; }

        [Required]
        [Display(Name = "Estado")]

        public int PF001 { get; set; }

       
    }
}
