using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Modelo.Entity;

namespace Model.Datos
{

    public class CargaDropDownList
    {
       public string codigo { get; set; }
        public string nombre { get; set; }
        public string zona { get; set; }
    }
    public class CatalogosSiniestros
    {
        public List<SelectListItem> datosTipoZona()
        {
            return new List<SelectListItem>() {
                new SelectListItem() {
                    Text = "SELECCIONAR",
                    Value = "0"
                },
                new SelectListItem() {
                    Text = "RURAL",
                    Value = "1"
                },
                 new SelectListItem() {
                    Text = "URBANA",
                    Value = "2"
                }
            };

        }

        public List<SelectListItem> datosCondicionAtmosferica()
        {
            return new List<SelectListItem>() {
                new SelectListItem() {
                    Text = "SELECCIONAR",
                    Value = "0"
                },
                new SelectListItem() {
                    Text = "NUBLADO",
                    Value = "1"
                },
                 new SelectListItem() {
                    Text = "LLUVIA",
                    Value = "2"
                },
                 new SelectListItem() {
                    Text = "NIEVE",
                    Value = "3"
                },
                 new SelectListItem() {
                    Text = "GRANIZO",
                    Value = "4"
                },
                 new SelectListItem() {
                    Text = "DESPEJADO",
                    Value = "5"
                }
            };

        }

        public List<SelectListItem> datosCondicionVia()
        {
            return new List<SelectListItem>() {
                new SelectListItem() {
                    Text = "SELECCIONAR",
                    Value = "0"
                },
                new SelectListItem() {
                    Text = "BUENO",
                    Value = "1"
                },
                 new SelectListItem() {
                    Text = "MALO",
                    Value = "2"
                },
                 new SelectListItem() {
                    Text = "REGULAR",
                    Value = "3"
                }

            };
        }
        public List<SelectListItem> datosLuzArtificial()
        {
            return new List<SelectListItem>() {
                new SelectListItem() {
                    Text = "SELECCIONAR",
                    Value = "0"
                },
                new SelectListItem() {
                    Text = "ENCENDIDA Y ADECUADA",
                    Value = "1"
                },
                 new SelectListItem() {
                    Text = "ENCENDIDA E INSUFICIENTE",
                    Value = "2"
                },
                 new SelectListItem() {
                    Text = "NO ENCENDIDA",
                    Value = "3"
                },
                 new SelectListItem() {
                    Text = "NO EXISTENTE",
                    Value = "4"
                }

            };
        }

        public List<SelectListItem> datosTipoVia()
        {
            return new List<SelectListItem>() {
                new SelectListItem() {
                    Text = "SELECCIONAR",
                    Value = "0"
                },
                new SelectListItem() {
                    Text = "AUTOPISTA DE PEAJE",
                    Value = "1"
                },
                 new SelectListItem() {
                    Text = "AUTOPISTA LIBRE",
                    Value = "2"
                },
                 new SelectListItem() {
                    Text = "AUTOVÍA",
                    Value = "3"
                },
                 new SelectListItem() {
                    Text = "CALLE",
                    Value = "4"
                },
                 new SelectListItem() {
                    Text = "CAMINO VECINAL",
                    Value = "5"
                },
                 new SelectListItem() {
                    Text = "VÍA CICLISTA",
                    Value = "6"
                },
                 new SelectListItem() {
                    Text = "VÍA DE SERVICIO",
                    Value = "7"
                },
                 new SelectListItem() {
                    Text = "RAMAL DE ENLACE",
                    Value = "8"
                }

            };
        }

        public List<SelectListItem> datosLimiteVelocidad()
        {
            return new List<SelectListItem>() {
                new SelectListItem() {
                    Text = "SELECCIONAR",
                    Value = "0"
                },
                new SelectListItem() {
                    Text = "10",
                    Value = "1"
                },
                 new SelectListItem() {
                    Text = "30",
                    Value = "2"
                },
                 new SelectListItem() {
                    Text = "40",
                    Value = "3"
                },
                 new SelectListItem() {
                    Text = "50",
                    Value = "4"
                },
                 new SelectListItem() {
                    Text = "70",
                    Value = "5"
                },
                 new SelectListItem() {
                    Text = "90",
                    Value = "6"
                },
                 new SelectListItem() {
                    Text = "100",
                    Value = "7"
                }
            };
        }

        public List<SelectListItem> datosControlInterseccion()
        {
            return new List<SelectListItem>() {
                new SelectListItem() {
                    Text = "SELECCIONAR",
                    Value = "0"
                },
                new SelectListItem() {
                    Text = "POLICÍA",
                    Value = "1"
                },
                 new SelectListItem() {
                    Text = "SEMÁFORO",
                    Value = "2"
                },
                 new SelectListItem() {
                    Text = "SEÑAL DE PARE",
                    Value = "3"
                },
                 new SelectListItem() {
                    Text = "SEÑAL DE CEDA EL PASO",
                    Value = "4"
                },
                 new SelectListItem() {
                    Text = "SEÑALIZACIÓN HORIZONTAL",
                    Value = "5"
                },
                 new SelectListItem() {
                    Text = "SEÑALIZACIÓN VERTICAL",
                    Value = "6"
                },
                 new SelectListItem() {
                    Text = "PASO CEBRA",
                    Value = "7"
                },
                 
                 new SelectListItem() {
                    Text = "NINGUNA",
                    Value = "8"
                }
            };
        }

        public List<SelectListItem> datosMaterialSuperficieVia()
        {
            return new List<SelectListItem>() {
                new SelectListItem() {
                    Text = "SELECCIONAR",
                    Value = "0"
                },
                new SelectListItem() {
                    Text = "HORMIGÓN",
                    Value = "1"
                },
                 new SelectListItem() {
                    Text = "ASFALTO",
                    Value = "2"
                },
                 new SelectListItem() {
                    Text = "LASTRADO",
                    Value = "3"
                },
                 new SelectListItem() {
                    Text = "TIERRA",
                    Value = "4"
                },
                 new SelectListItem() {
                    Text = "ARENA",
                    Value = "5"
                },
                 new SelectListItem() {
                    Text = "OTRO",
                    Value = "6"
                }
            };
        }

        public List<SelectListItem> datosObstaculoVia()
        {
            return new List<SelectListItem>() {
                new SelectListItem() {
                    Text = "SELECCIONAR",
                    Value = "0"
                },
                new SelectListItem() {
                    Text = "DESPEJADO",
                    Value = "1"
                },
                 new SelectListItem() {
                    Text = "ÁRBOLES",
                    Value = "2"
                },
                 new SelectListItem() {
                    Text = "EDIFICIO",
                    Value = "3"
                },

                 new SelectListItem() {
                    Text = "POSTES",
                    Value = "4"
                },
                 new SelectListItem() {
                    Text = "CARTELES PUBLICIDAD",
                    Value = "5"
                },
                 new SelectListItem() {
                    Text = "OTROS OBSTÁCULOS RÍGIDOS",
                    Value = "6"
                }
            };
        }

        public List<SelectListItem> datosLugarVia()
        {
            return new List<SelectListItem>() {
                new SelectListItem() {
                    Text = "SELECCIONAR",
                    Value = "0"
                },
                new SelectListItem() {
                    Text = "RECTA",
                    Value = "1"
                },
                 new SelectListItem() {
                    Text = "INTERSECCIÓN EN T",
                    Value = "2"
                },
                 new SelectListItem() {
                    Text = "INTERSECCIÓN EN CRUZ",
                    Value = "3"
                },
                 new SelectListItem() {
                    Text = "INTERSECCIÓN EN Y",
                    Value = "4"
                },
                 new SelectListItem() {
                    Text = "REDONDEL",
                    Value = "5"
                },
                 new SelectListItem() {
                    Text = "CURVA",
                    Value = "6"
                },
                 new SelectListItem() {
                    Text = "PASO DE FERROCARRIL",
                    Value = "7"
                },
                 new SelectListItem() {
                    Text = "PUENTE",
                    Value = "8"
                },
                 new SelectListItem() {
                    Text = "INTERCAMBIADOR",
                    Value = "9"
                }
                 
            };
        }

        public List<SelectListItem> datosNumeroCarriles()
        {
            return new List<SelectListItem>() {
                new SelectListItem() {
                    Text = "SELECCIONAR",
                    Value = "0"
                },
                new SelectListItem () {
                    Text = "1",
                    Value = "1"
                },
                new SelectListItem(){
                    Text = "2",
                    Value = "2"
                },
                new SelectListItem(){
                    Text = "3",
                    Value = "3"
                },
                new SelectListItem(){
                    Text = "4",
                    Value = "4"
                },
                new SelectListItem(){
                    Text = "5",
                    Value = "5"
                },
                new SelectListItem(){
                    Text = "6",
                    Value = "6"
                },
                new SelectListItem(){
                    Text = "7",
                    Value = "7"
                },
                new SelectListItem(){
                    Text = "8",
                    Value = "8"
                }
            };
        }


        public List<SelectListItem> datosSenialitica()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem() {
                    Text = "SELECCIONAR",
                    Value = "0"
                },
                new SelectListItem() {
                    Text ="SEÑALIZACIÓN HORIZONTAL",
                    Value ="1"
                },
                new SelectListItem() {
                    Text ="SEÑALIZACIÓN VERTICAL",
                    Value ="2"
                },
                 new SelectListItem() {
                    Text ="AMBAS",
                    Value ="3"
                },
                  new SelectListItem() {
                    Text ="NINGUNA",
                    Value ="4"
                }

            };
        }

        public List<SelectListItem> datosTransporteMaterialPeligroso()
        {
            return new List<SelectListItem>() {
                 new SelectListItem() {
                    Text = "SELECCIONAR",
                    Value = "-1"
                },
                new SelectListItem() {
                    Text = "NINGUNO",
                    Value = "0"
                },
                new SelectListItem () {
                    Text = "EXPLOSIVOS",
                    Value = "1"
                },
                new SelectListItem(){
                    Text = "GASES",
                    Value = "2"
                },
                new SelectListItem(){
                    Text = "LÍQUIDOS",
                    Value = "3"
                },
                new SelectListItem(){
                    Text = "SÓLIDOS",
                    Value = "4"
                },
                new SelectListItem(){
                    Text = "OXIDANTES",
                    Value = "5"
                },
                new SelectListItem(){
                    Text = "VENENOS",
                    Value = "6"
                },
                new SelectListItem(){
                    Text = "RADIOACTIVOS",
                    Value = "7"
                },
                new SelectListItem(){
                    Text = "CORROSIVOS",
                    Value = "8"
                },
                new SelectListItem(){
                    Text = "MEZCLAS PELIGROSAS",
                    Value = "9"
                }
            };
        }


        /*****************************************
         * DATOS VICTIMAS
         * ***************************************/
        public List<SelectListItem> datosTipoIdentificacion()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem() {
                    Value ="-1",
                    Text ="SELECCIONAR",
                },
                new SelectListItem() {
                    Value ="1",
                    Text ="CÉDULA",
                },
                new SelectListItem() {
                    Value ="2",
                    Text ="LICENCIA",
                },
                new SelectListItem() {
                    Value ="3",
                    Text ="PASAPORTE",
                },
                new SelectListItem() {
                    Value ="4",
                    Text ="NO IDENTIFICADO",
                }
            };
        }
        public List<SelectListItem> datosSexo()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem() {
                    Value ="-1",
                    Text ="SELECCIONAR",
                },
                new SelectListItem() {
                    Value ="M",
                    Text ="MUJER",
                },
                new SelectListItem() {
                    Value ="H",
                    Text ="HOMBRE",
                }
                //new SelectListItem() {
                //    Value ="N",
                //    Text ="NO IDENTIFICADO",
                //}
            };
        }

        public List<SelectListItem> datosCondicionVictimas24()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem() {
                    Value ="-1",
                    Text ="SELECCIONAR",
                },
                new SelectListItem() {
                    Value ="1",
                    Text ="ILESO",
                },
                new SelectListItem() {
                    Value ="2",
                    Text ="LESIONADO",
                },
                new SelectListItem() {
                    Value ="3",
                    Text ="FALLECIDO",
                }
                //new SelectListItem() {
                //    Value ="4",
                //    Text="NO VERIFICADO"
                //}
            };
        }

        public List<SelectListItem> datosCondicionVictimas30()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem() {
                    Value ="-1",
                    Text ="SELECCIONAR",
                },
                new SelectListItem() {
                    Value ="1",
                    Text ="ILESO",
                },
                new SelectListItem() {
                    Value ="2",
                    Text ="LESIONADO",
                },
                new SelectListItem() {
                    Value ="3",
                    Text ="FALLECIDO",
                },
                new SelectListItem() {
                    Value ="4",
                    Text ="NO DETERMINADO",
                }
            };
        }

        public List<SelectListItem> datosTipoParticipante()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem() {
                    Value ="-1",
                    Text ="SELECCIONAR",
                },
                new SelectListItem() {
                    Value ="1",
                    Text ="CONDUCTOR",
                },
                new SelectListItem() {
                    Value ="2",
                    Text ="PASAJERO",
                },
                new SelectListItem() {
                    Value ="3",
                    Text ="PEATÓN",
                }
            };
        }

        public List<SelectListItem> datosPosicionPlaza()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem() {
                    Value ="-1",
                    Text ="SELECCIONAR",
                },
                new SelectListItem() {
                    Value ="1",
                    Text ="FRONTAL IZQUIERDO",
                },
                new SelectListItem() {
                    Value ="2",
                    Text ="FRONTAL CENTRAL",
                },
                new SelectListItem() {
                    Value ="3",
                    Text ="FRONTAL DERECHO",
                },
                new SelectListItem() {
                    Value ="4",
                    Text ="CENTRAL IZQUIERDO",
                }
                ,
                new SelectListItem() {
                    Value ="5",
                    Text ="CENTRAL",
                }
                ,
                new SelectListItem() {
                    Value ="6",
                    Text ="CENTRAL DERECHO",
                }
                ,
                new SelectListItem() {
                    Value ="7",
                    Text ="TRASERO IZQUIERDO",
                },
                new SelectListItem() {
                    Value ="8",
                    Text ="TRASERO CENTRAL",
                },
                new SelectListItem() {
                    Value ="9",
                    Text ="TRASERO DERECHO",
                },
                new SelectListItem() {
                    Value ="10",
                    Text ="BALDE",
                },
                new SelectListItem() {
                    Value ="11",
                    Text ="DE PIE",
                },
                new SelectListItem() {
                    Value ="12",
                    Text ="OTROS",
                },
                new SelectListItem() {
                    Value ="13",
                    Text ="NIÑO EN BRAZOS",
                }

            };
        }


        public List<SelectListItem> datosAccionesPeaton()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem() {
                    Value ="-1",
                    Text ="------SELECCIONAR------",
                },
                new SelectListItem() {
                    Value ="1",
                    Text ="USO DEL CELULAR",
                },
                new SelectListItem() {
                    Value ="2",
                    Text ="USO DE ELEMENTOS DISTRACTORES",
                },
                new SelectListItem() {
                    Value ="3",
                    Text ="CRUCE DE VÍA A LUGARES NO AUTORIZADO",
                },
                new SelectListItem() {
                    Value ="4",
                    Text ="PRESUNCIÓN DE INGESTA DE ALCOHOL",
                }
                ,
                new SelectListItem() {
                    Value ="5",
                    Text ="CRUCE DE VÍA SIN PREFERENCIA",
                }
                ,
                new SelectListItem() {
                    Value ="6",
                    Text ="PRESUNCIÓN DE INGESTA  DE SUSTANCIAS ESTUPERFACIENTES O PSICOTRÓPICAS Y/O MEDICAMENTOS",
                }
                ,
                new SelectListItem() {
                    Value ="7",
                    Text ="NINGUNA",
                }

            };
        }


        public List<SelectListItem> datosCurvaExistente()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem() {
                    Value ="-1",
                    Text ="SELECCIONAR",
                },
                new SelectListItem() {
                    Value ="1",
                    Text ="VERTICAL",
                },
                new SelectListItem() {
                    Value ="2",
                    Text ="HORIZONTAL",
                },
                new SelectListItem() {
                    Value ="3",
                    Text ="NINGUNA",
                },
                new SelectListItem() {
                    Value ="4",
                    Text ="AMBAS",
                }


            };
        }

        public List<SelectListItem> datosSenializacionExistente()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem() {
                    Value ="-1",
                    Text ="SELECCIONAR",
                },
                new SelectListItem() {
                    Value ="1",
                    Text ="SI",
                },
                new SelectListItem() {
                    Value ="2",
                    Text ="NO",
                }
            };
        }
    }
}
