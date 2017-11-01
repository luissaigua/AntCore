using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Modelo.Entity
{
    public class Autoridad:BaseEntity
    {
        [Display(Name = "Codigo")]
        [StringLength(8)]
        public string CODAUT { get; set; }

        [Required]
        [Display(Name = "Nombre Autoridad")]
        [StringLength(120)]

        public string DESAUT { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        [StringLength(70)]
        public string EMAAUT { get; set; }

        [Required]
        [Display(Name = "Tipo Carga")]
        [StringLength(2)]
        public string CARAUT { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public int ESTAUT { get; set; }
    }
}
