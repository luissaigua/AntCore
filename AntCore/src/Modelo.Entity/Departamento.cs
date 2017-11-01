using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Modelo.Entity
{
    public class Departamento:BaseEntity
    {
        [Display(Name = "Codigo")]
        public int PDCOD{ get; set; }
        [Required]
        [Display(Name = "Departamento")]
        [StringLength(80)]
        public string PD000 { get; set; }
        [Required]
        [Display(Name = "Estado")]
        public int PD001 { get; set; }
        
       
    }
}
