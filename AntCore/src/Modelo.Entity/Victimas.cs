using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Entity
{
    public class Victimas
    {
  //      codvicinv serial NOT NULL,
  //tipidenvicinv character varying(30) NOT NULL, -- TIPO DE IDENTIFICACIÓN VÍCTIMAS INVOLUCRADAS
  //numidenvicinv character varying(20) NOT NULL, -- NÚMERO DE IDENTIFICACIÓN DE LA VÍCTIMA EN EL CASO DE NO TENER ESTE DATO, INGRESAR NN
  //edavicinv integer NOT NULL, -- EDAD DE LA VÍCTIMA
  //genvicinv character(1) NOT NULL, -- GÉNERO DE LA VÍCTIMA :M=MASCULINO,F=FEMENINO
  //convicinv24 character varying(15) NOT NULL, -- ILESO/LESIONADO/FALLECIDO
  //convicinv30 character varying(15), -- ILESO/LESIONADO/FALLECIDO
  //tipparvicinv character varying(25) NOT NULL, -- CONDUCTOR/PASAJERO/PEATON
  //casvicinv boolean NOT NULL DEFAULT false, -- USO DEL CASCO DE LA VÍCTIMA
  //cinvicinv boolean NOT NULL DEFAULT false, -- USO DEL CINTURON DE SEGURIDAD DE LA VÍCTIMA
  //posvicinv character varying(35) NOT NULL, -- POSICIÓN DE LA VÍCTIMA INVOLUCRADA: CONDUCTOR/ASIENTO DELANTERO CENTRAL, ASIENTO DELANTEROI DERECHO
  //conalcvicinv boolean NOT NULL DEFAULT false, -- SOSPECHA DE CONSUMO DE ALCOHOL
  //codsin integer NOT NULL,
  //codveh integer NOT NULL,

        public int codvicinv { get; set; }
        public string tipidenvicinv { get; set; }
        public string numidenvicinv { get; set; }
        public int edavicinv { get; set; }
        public char genvicinv { get; set; }
        public string convicinv24 { get; set; }
        public string convicinv30 { get; set; }
        public string tipparvicinv { get; set; }
        public bool casvicinv { get; set; }
        public bool cinvicinv { get; set; }
        public string posvicinv { get; set; }
        public bool conalcvicinv { get; set; }
        public int codsin { get; set; }
        public int codveh { get; set; }

        public string desaccpea { get; set; }

        public string sexo { get; set; }
        public string USO_CASO { get; set; }
        public string USO_CINTU { get; set; }
        public string CONS_ALCOHOL { get; set; }
        public string PLACAVHL { get; set; }
        public string nombre_completo { get; set; }
        public string fecha_nacimiento { get; set; }

        public string estado_civil { get; set; }

        public string codaccpea { get; set; }
        public string nombreVictima { get; set; }
        //
    }
}
