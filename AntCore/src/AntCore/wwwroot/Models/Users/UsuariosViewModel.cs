using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace AntCore.Models.Users
{
    public class UsuariosViewModel:BaseEntity
    {
        [Required]
        [Display(Name = " Usuario")]
        public string PU000 { get; set; }

        [Required]
        [Display(Name = "Clave")]
        public string PU001 { get; set; }

        [Required]
        [Display(Name = "Nombres")]
        public string PU002 { get; set; }

        [Required]
        [Display(Name = "Apellidos")]
        public string PU003 { get; set; }

        [Required]
        [Display(Name = "Token")]
        public string PU004 { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public bool PU005 { get; set; }

        [Required]
        [Display(Name = "Departamento")]
        public int PDCOD { get; set; }

        [Required]
        [Display(Name = "Función")]
        public int PFCOD { get; set; }

        [Required]
        [Display(Name = "Codigo Autoridad")]
        public int codaut { get; set; }


    }
}
