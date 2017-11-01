using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Entity
{
    public class Usuarios : BaseEntity
    {
        [Display(Name = "Codigo")]
        public int PUCOD { get; set; }

        [Display(Name = "Usuario")]
        [Required (ErrorMessage ="El campo usuario es obligatorio")]
        [MaxLength (10,ErrorMessage ="El campo usuario debe contener 10 dígitos")]
        public string PU000 { get; set; }

       
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "El campo password es obligatorio")]
        [MaxLength(10, ErrorMessage = "El campo password debe contener 10 caracteres")]
        public string PU001 { get; set; }

        [Required (ErrorMessage ="El campo Nombres es obligatorio")]
        [MaxLength(80, ErrorMessage = "El campo Nombres debe contener 40 caracteres  maximo")]
        [Display(Name = "Nombres")]
        public string PU002 { get; set; }

        [Required (ErrorMessage ="El campo Apellidos es obligatorio")]
        [MaxLength(40,ErrorMessage ="El campo apellidos debe contener máximo 40 caracteres")]
        [Display(Name = "Apellidos")]
        public string PU003 { get; set; }


        [Display(Name = "Token")]
        public string PU004 { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public int PU005 { get; set; }

        [Required(ErrorMessage ="El campo email es obligatorio")]
        [MaxLength(50,ErrorMessage ="El campo email debe contener máximo 50 caracteres")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$", ErrorMessage = "No es un email válido.")]
        [Display(Name = "Email")]
        public String PU006 { get; set; }

        [Required(ErrorMessage ="Seleccione el campo departamento")]
        [Display(Name = "Departamento")]
        public int PDCOD { get; set; }

        [Required (ErrorMessage ="Seleccione el campo Función")]
        [Display(Name = "Función")]
        public int PFCOD { get; set; }

        [Required (ErrorMessage = "Seleccione el campo Autoridad")]
        [Display(Name = "Codigo Autoridad")]
        public string CODAUT { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }


        public string permisoGeneral { get; set; }
        public string permisoAgente { get; set; }
        public string permisoValidador { get; set; }
        public string permisoGestorValid { get; set; }
        public string permisoSupervisorAnt { get; set; }

        public string caraut { get; set; }

        public string departamento { get; set; }
        //public string FUNCION { get; set; }
        public string autoridad { get; set; }
        public string funcion { get; set; }
        //public Usuarios(string PU000, int PFCOD)
        //{
        //    this.PU000 = PU000;
        //    this.PFCOD = PFCOD;

        //    this.PU001 = "Hola Mundo";
        //}

        //public Usuarios(string Usuario, String Password)
        //{
        //    this.PU000 = Usuario;
        //    this.PU001 = Password;
        //}


        //public Usuarios(int PUCOD, string PU000, string PU001, string PU002, string PU003, string PU004, int PU005, int PDCOD, int PFCOD, string codaut, string PU006)
        //{
        //    this.PUCOD = PUCOD;
        //    this.PU000 = PU000;

        //    this.PU002 = PU002;
        //    this.PU003 = PU003;
        //    this.PU004 = PU004;
        //    this.PU005 = PU005;
        //    this.PDCOD = PDCOD;
        //    this.PFCOD = PFCOD;
        //    this.CODAUT = CODAUT;
        //    this.PU006 = PU006;


        //}

    }
}
