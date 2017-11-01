using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AntCore.Models.Users
{
    public class AutoridadesViewModel:BaseEntity
    {
        [Required]
        [Display(Name = "Nombre Autoridade")]
        public string desaut { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string emaaut { get; set; }

        [Required]
        [Display(Name = "Tipo Cargo")]
        public string caraut { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public bool estaut { get; set; }
    }
}
