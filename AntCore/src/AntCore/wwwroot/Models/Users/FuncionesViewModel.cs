using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AntCore.Models.Funciones
{
    public class FuncionesViewModel:BaseEntity
    {
        [Required]
        [Display(Name = "Nombre Funcion")]
        public string PD000 { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public bool PD001 { get; set; }
    }
}
