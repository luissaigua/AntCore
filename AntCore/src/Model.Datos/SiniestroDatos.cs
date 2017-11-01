using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Modelo.Entity;
using Npgsql;
using System.Data;
using Dapper;
using Npgsql.PostgresTypes;
using NpgsqlTypes;
using System.ComponentModel;
using System.Data.SqlClient;
using Newtonsoft.Json; // Use for JsonConvert
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Globalization;
using System.Security.Cryptography;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;
using Newtonsoft.Json.Linq;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;

namespace Model.Datos
{
    public class SiniestroDatos//:Obigatorio<Siniestro>
    {
        private ConexionDB objConexinDB;
        private NpgsqlCommand comando;
        private Encriptacion enc = new Encriptacion();
        public SiniestroDatos()
        {
            objConexinDB = ConexionDB.saberEstado();

        }
        public class CamposDropDown
        {
            public string cod_auto { get; set; }
            public string descripcion { get; set; }
        }
        public string GuardaSiniestros(DateTime fecsin, string horsin, string latsin, string lonsin, string dirsin, int numfalsin, int numlessin, int estsin, bool regvalsin, int ageressin, int supressIn, string zonsin, bool traviasin, string conatmsin, string conviasin, string luzartsin, string desviasin, int limvelsin, string intsin, string matsupviasin, string obsviasin, string lugviasin, string cursin, int numcarsin, string sensin, int PUCOD, string codaut, int codtipsin, int codpar, string codsubcir, int codcant, int codprov, string codcaupro, string codcaurea, string codcir, string coddis)
        {
            int codsin = 0;
            decimal _lat = ConvertirEnDecimal(latsin), _long = ConvertirEnDecimal(lonsin);
            estsin = 1;
            NumberFormatInfo nfi = new CultureInfo("en-US").NumberFormat;
            nfi.NumberDecimalSeparator = ".";
            DateTime feccresin = DateTime.Now;
            if (luzartsin == "SELECCIONAR")
                luzartsin = "";
            string create = "";// "INSERT INTO siniestros  ( fecsin, horsin, latsin, lonsin, dirsin, numfalsin, numlessin,  estsin, regvalsin, ageressin, \"supressIn\", zonsin, traviasin, conatmsin, conviasin, luzartsin, desviasin, limvelsin, intsin, matsupviasin, obsviasin, lugviasin, cursin, numcarsin, sensin, \"PUCOD\", codaut, codtipsin, codpar,  codcant, codprov, codcaupro, codcaurea, codcir, coddis) values ('"+Convert.ToDateTime(fecsin) +"', '"+ horsin + "',"+ _lat + ","+ _long + ",'"+ dirsin + "',"+ numfalsin + ","+ numlessin + ","+ estsin + ", "+ regvalsin + ","+ ageressin + ","+ supressIn + ",'"+ zonsin + "',"+ traviasin + ",'"+ conatmsin + "','"+ conviasin + "','"+ luzartsin + "','"+ desviasin + "',"+ limvelsin + ",'"+ intsin + "','"+ matsupviasin + "','"+ obsviasin + "','"+ lugviasin + "','"+ cursin + "',"+ numcarsin + ",'"+ sensin + "',"+ PUCOD + ",'"+ codaut + "',"+ codtipsin + ","+ codpar + ","+ codcant + ","+ codprov + ",'"+ codcaupro + "','"+ codcaurea + "','"+ codcir + "','"+coddis+"') ";
            create = "INSERT INTO siniestros  ( fecsin, horsin, latsin, lonsin, dirsin, numfalsin, numlessin,  estsin, regvalsin, ageressin, \"supressIn\", zonsin, traviasin, conatmsin, conviasin, luzartsin, desviasin, limvelsin, intsin, matsupviasin, obsviasin, lugviasin, cursin, numcarsin, sensin, \"PUCOD\", codaut, codtipsin, codpar,  codcant, codprov, codcaupro, codcaurea, codcir, coddis,feccresin,codestprocsin) values ('" + Convert.ToDateTime(fecsin) + "', '" + horsin + "'," + latsin.Replace(',', '.') + "," + lonsin.Replace(',', '.') + ",'" + dirsin + "'," + numfalsin + "," + numlessin + "," + estsin + ", " + regvalsin + "," + ageressin + "," + supressIn + ",'" + zonsin + "'," + traviasin + ",'" + conatmsin + "','" + conviasin + "','" + luzartsin + "','" + desviasin + "'," + limvelsin + ",'" + intsin + "','" + matsupviasin + "','" + obsviasin + "','" + lugviasin + "','" + cursin + "'," + numcarsin + ",'" + sensin + "'," + PUCOD + ",'" + codaut + "'," + codtipsin + "," + codpar + "," + codcant + "," + codprov + ",'" + codcaupro + "','C00','" + codcir + "','" + coddis + "','" + feccresin + "',1) ";
            try
            {
                comando = new NpgsqlCommand(create, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                comando.ExecuteNonQuery();
                codsin = 1;
            }
            catch (Exception e)
            {
                codsin = 0;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            if (codsin == 1)
                codsin = Convert.ToInt32(MAXcodSiniestro());

            return codsin.ToString();
        }
        public string ModificarSiniestros(int codSiniestro, DateTime fecsin, string horsin, string latsin, string lonsin, string dirsin, int numfalsin, int numlessin, int estsin, bool regvalsin, int ageressin, int supressIn, string zonsin, bool traviasin, string conatmsin, string conviasin, string luzartsin, string desviasin, int limvelsin, string intsin, string matsupviasin, string obsviasin, string lugviasin, string cursin, int numcarsin, string sensin, int PUCOD, string codaut, int codtipsin, int codpar, string codsubcir, int codcant, int codprov, string codcaupro, string codcaurea, string codcir, string coddis, int pucmodsin)
        {
            int codsin = 0;
            decimal _lat = ConvertirEnDecimal(latsin), _long = ConvertirEnDecimal(lonsin);
            NumberFormatInfo nfi = new CultureInfo("en-US").NumberFormat;
            nfi.NumberDecimalSeparator = ".";
            DateTime fechaModificacion = DateTime.Now;
            string create = "";// "INSERT INTO siniestros  ( fecsin, horsin, latsin, lonsin, dirsin, numfalsin, numlessin,  estsin, regvalsin, ageressin, \"supressIn\", zonsin, traviasin, conatmsin, conviasin, luzartsin, desviasin, limvelsin, intsin, matsupviasin, obsviasin, lugviasin, cursin, numcarsin, sensin, \"PUCOD\", codaut, codtipsin, codpar,  codcant, codprov, codcaupro, codcaurea, codcir, coddis) values ('"+Convert.ToDateTime(fecsin) +"', '"+ horsin + "',"+ _lat + ","+ _long + ",'"+ dirsin + "',"+ numfalsin + ","+ numlessin + ","+ estsin + ", "+ regvalsin + ","+ ageressin + ","+ supressIn + ",'"+ zonsin + "',"+ traviasin + ",'"+ conatmsin + "','"+ conviasin + "','"+ luzartsin + "','"+ desviasin + "',"+ limvelsin + ",'"+ intsin + "','"+ matsupviasin + "','"+ obsviasin + "','"+ lugviasin + "','"+ cursin + "',"+ numcarsin + ",'"+ sensin + "',"+ PUCOD + ",'"+ codaut + "',"+ codtipsin + ","+ codpar + ","+ codcant + ","+ codprov + ",'"+ codcaupro + "','"+ codcaurea + "','"+ codcir + "','"+coddis+"') ";
            create = "UPDATE public.siniestros " +
                     "SET  fecsin ='" + Convert.ToDateTime(fecsin) + "',  horsin ='" + horsin + "', latsin =" + latsin.Replace(',', '.') + ", lonsin =" + lonsin.Replace(',', '.') + ", dirsin ='" + dirsin + "', numfalsin =" + numfalsin + ", " +
                     "numlessin =" + numlessin + ",  " +
                     "zonsin ='" + zonsin + "', traviasin =" + Convert.ToBoolean(traviasin) + ", conatmsin ='" + conatmsin.ToString() + "', conviasin ='" + conviasin + "', luzartsin ='" + luzartsin + "', " +
                     "desviasin ='" + desviasin + "', limvelsin =" + limvelsin + ", intsin ='" + intsin + "', matsupviasin ='" + matsupviasin + "', obsviasin ='" + obsviasin + "', " +
                     "lugviasin ='" + lugviasin + "', cursin ='" + cursin + "', numcarsin =" + numcarsin + ", sensin ='" + sensin + "',  " +
                     "codtipsin =" + codtipsin + ", codpar =" + codpar + ", codcant =" + codcant + ", codprov =" + codprov + ", codcaupro ='" + codcaupro + "', codcaurea ='" + codcaurea + "',  " +
                     "codcir ='" + codcir + "', coddis ='" + coddis + "', fecactsin ='" + fechaModificacion + "', pucmodsin =" + pucmodsin + " " +
                     " WHERE codsin =" + codSiniestro + " ";
            try
            {
                comando = new NpgsqlCommand(create, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                comando.ExecuteNonQuery();
                codsin = 1;
            }
            catch (Exception e)
            {
                codsin = 0;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            if (codsin == 1)
                codsin = Convert.ToInt32(codSiniestro);

            return codsin.ToString();
        }
        public string ModificarSiniestrosFinProceso(int codSiniestro,int codprocsin)
        {
            int codsin = 0;
           
            string create = "";// "INSERT INTO siniestros  ( fecsin, horsin, latsin, lonsin, dirsin, numfalsin, numlessin,  estsin, regvalsin, ageressin, \"supressIn\", zonsin, traviasin, conatmsin, conviasin, luzartsin, desviasin, limvelsin, intsin, matsupviasin, obsviasin, lugviasin, cursin, numcarsin, sensin, \"PUCOD\", codaut, codtipsin, codpar,  codcant, codprov, codcaupro, codcaurea, codcir, coddis) values ('"+Convert.ToDateTime(fecsin) +"', '"+ horsin + "',"+ _lat + ","+ _long + ",'"+ dirsin + "',"+ numfalsin + ","+ numlessin + ","+ estsin + ", "+ regvalsin + ","+ ageressin + ","+ supressIn + ",'"+ zonsin + "',"+ traviasin + ",'"+ conatmsin + "','"+ conviasin + "','"+ luzartsin + "','"+ desviasin + "',"+ limvelsin + ",'"+ intsin + "','"+ matsupviasin + "','"+ obsviasin + "','"+ lugviasin + "','"+ cursin + "',"+ numcarsin + ",'"+ sensin + "',"+ PUCOD + ",'"+ codaut + "',"+ codtipsin + ","+ codpar + ","+ codcant + ","+ codprov + ",'"+ codcaupro + "','"+ codcaurea + "','"+ codcir + "','"+coddis+"') ";
            create = "UPDATE public.siniestros " +
                     "SET  estsin = 1 ,codestprocsin = " + codprocsin+"  "+
                     " WHERE codsin =" + codSiniestro + " ";
            try
            {
                comando = new NpgsqlCommand(create, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                comando.ExecuteNonQuery();
                codsin = 1;
            }
            catch (Exception e)
            {
                codsin = 0;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            if (codsin == 1)
                codsin = Convert.ToInt32(codSiniestro);

            return codsin.ToString();
        }

      

        public string ObtieneNumeLesionadosFallecidos(int codsin, string parametro, int opcion)
        {
            int numeroTotal = 0;

            string create = "";

            if (opcion == 1)//lesionados
                create = "select count(*) from VICTIMAS_INVOLUCRADAS WHERE CODSIN = " + codsin + " AND convicinv24 = 'LESIONADO' and estvicinv = 1; ";
            if (opcion == 2)//fallecidos
                create = "select count(*) from VICTIMAS_INVOLUCRADAS WHERE CODSIN = " + codsin + " AND convicinv24 = 'FALLECIDO'  and estvicinv = 1; ";
            try
            {
                comando = new NpgsqlCommand(create, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                //  comando.ExecuteNonQuery();
                NpgsqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    numeroTotal = Convert.ToInt32(reader[0].ToString());
                }

            }
            catch (Exception e)
            {
                codsin = 0;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            if (codsin == 1)
                codsin = Convert.ToInt32(MAXcodSiniestro());

            return numeroTotal.ToString();
        }

        public string ModificarSiniestrosNumLesionadosFallecidos(int codsin, string parametro, int opcion)
        {
            string create = "";
            string total = ObtieneNumeLesionadosFallecidos(codsin, parametro, opcion);
            if (opcion == 1)// LESIONADOS
                create = "update siniestros set  numlessin = " + Convert.ToInt32(total) + "  where CODSIN = " + codsin + " ";
            if (opcion == 2)// FALLECIDOS
                create = "update siniestros set  numfalsin = " + Convert.ToInt32(total) + "  where CODSIN = " + codsin + " ";
            try
            {
                comando = new NpgsqlCommand(create, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                comando.ExecuteNonQuery();
                codsin = 1;
            }
            catch (Exception e)
            {
                codsin = 0;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            if (codsin == 1)
                codsin = Convert.ToInt32(MAXcodSiniestro());

            return codsin.ToString();
        }

        public List<SelectListItem> TraerNumLesionadosFallecidos(int codsin)
        {
            List<SelectListItem> listaNumLesionadosFallecidos = new List<SelectListItem>();

            string findAll = "select numlessin,numfalsin from siniestros where codsin  =  " + codsin + "";
            try
            {

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());
                objConexinDB.getCon().Open();

                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    CargaDropDownList objSiniestro = new CargaDropDownList();
                    objSiniestro.codigo = Convert.ToString(reader[0].ToString());
                    objSiniestro.nombre = Convert.ToString(reader[1].ToString());
                    listaNumLesionadosFallecidos.Add(new SelectListItem() { Value = objSiniestro.codigo.ToString(), Text = objSiniestro.nombre.ToString() });
                }
            }
            catch (Exception ex)
            {
                throw;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaNumLesionadosFallecidos;

        }

        public List<Siniestro> listaSiniestros(string codaut)
        {
            List<Siniestro> listaSiniestro = new List<Siniestro>();
            //PDAC tabla Departamentos
            string findAll = "Select s.codsin, to_char(s.fecsin,'YYYY-MM-DD') as fecsin ,s.numfalsin,s.zonsin,upper(s.dirsin) as dirsin, upper(u.\"PU002\" ) || ' ' || upper ( u.\"PU003\") as \"agente_responsable\", upper(u1.\"PU002\" ) || ' ' || upper ( u1.\"PU003\") as \"usuario_registro\", upper(u2.\"PU002\" ) || ' ' || upper ( u2.\"PU003\") as \"SUPERVISOR\" " +
                             ",a.desaut as \"autoridad\", " +
                             " case when s.regvalsin = 't' then 'OK' ELSE 'PENDIENTE' END AS \"REGISTRO_VALIDADO\" ,p.nomprov,c.descant,s.numlessin,ps.desprocsin, ps.codprocsin  ,g.fotprigeo ,g.fotsegeo" +
                             " FROM siniestros s " +
                             " inner join \"PUAC\" u on s.ageressin = u.\"PUCOD\" " +
                             " inner join autoridades a on a.codaut = s.codaut and a.estaut = 1 " +
                             " inner join \"PUAC\" u1 on s.\"PUCOD\" = u1.\"PUCOD\" " +
                             " left outer join \"PUAC\" u2 on s.\"supressIn\" = u2.\"PUCOD\" " +
                             " inner join provincias p on p.codprov = s.codprov" +
                             " inner join cantones c on c.codcant = s.codcant" +
                             " left outer join proceso_siniestros ps on ps.codprocsin = s.codestprocsin" +
                             " left outer join georeferencias g on g.codsin = s.codsin " +
                             " where s.estsin = 1 and s.codaut = '" + codaut + "'  order by  1; ";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Siniestro objSiniestro = new Siniestro();
                    objSiniestro.codsin = Convert.ToInt32(reader[0].ToString());
                    objSiniestro.fecsin = Convert.ToString(Convert.ToDateTime(reader[1].ToString()).ToString("yyyy-MM-dd")).Replace('/', '-'); //Convert.ToString(reader[1].ToString());
                    objSiniestro.numfalsin = Convert.ToInt32(reader[2].ToString());
                    objSiniestro.zonsin = Convert.ToString(reader[3].ToString());
                    objSiniestro.dirsin = Convert.ToString(reader[4].ToString());
                    objSiniestro.agente_responsable = Convert.ToString(reader[5].ToString());
                    objSiniestro.USUARIO_REGISTRO = Convert.ToString(reader[6].ToString());
                    objSiniestro.supervisor_responsable = Convert.ToString(reader[7].ToString());
                    objSiniestro.autoridad = Convert.ToString(reader[8].ToString());
                    objSiniestro.REGISTRO_VALIDADO = Convert.ToString(reader[9].ToString());
                    objSiniestro.nomprov = Convert.ToString(reader[10].ToString());
                    objSiniestro.descant = Convert.ToString(reader[11].ToString());
                    objSiniestro.numlessin = Convert.ToInt32(reader[12].ToString());
                    objSiniestro.desprocsin = Convert.ToString(reader[13].ToString());
                    objSiniestro.codprocsin = reader[14].ToString() == null || reader[14].ToString() == "" ? "0" : Convert.ToString(reader[14].ToString());
                    objSiniestro.fotprigeo = Convert.ToString(reader[15].ToString());
                    objSiniestro.fotsegeo = Convert.ToString(reader[16].ToString());

                    listaSiniestro.Add(objSiniestro);

                }
            }
            catch (Exception)
            {
                throw;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaSiniestro;

        }

        public List<Siniestro> listaSiniestrosPorFechas(string fecini, string fechafin, string codaut, string codprov   )
        {
            List<Siniestro> listaSiniestro = new List<Siniestro>();
            string findAll = "";
            if (codaut != "-1" && codprov != "-1" )
            {
                findAll = "Select s.codsin, to_char(s.fecsin,'YYYY-MM-DD') as fecsin ,s.horsin,s.zonsin,p.nomprov as nomprov,c.descant as descant,upper(s.dirsin) as dirsin, upper(u.\"PU002\" ) || ' ' || upper ( u.\"PU003\") as \"agente_responsable\", upper(u1.\"PU002\" ) || ' ' || upper ( u1.\"PU003\") as \"usuario_registro\", upper(u2.\"PU002\" ) || ' ' || upper ( u2.\"PU003\") as \"SUPERVISOR\" " +
                             ",a.desaut as \"autoridad\", " +
                             "case when s.regvalsin = 't' then 'OK' ELSE 'PENDIENTE' END AS \"REGISTRO_VALIDADO\" " +
                             " FROM siniestros s " +
                             " inner join \"PUAC\" u on s.ageressin = u.\"PUCOD\" " +
                             " inner join autoridades a on a.codaut = s.codaut and a.estaut = 1 " +
                             " inner join \"PUAC\" u1 on s.\"PUCOD\" = u1.\"PUCOD\" " +
                             " left outer join \"PUAC\" u2 on s.\"supressIn\" = u2.\"PUCOD\" " +
                             " INNER JOIN provincias p on  p.codprov =   s.codprov " +
                             " inner join cantones c on c.codcant = s.codcant" +
                             " where s.codaut = '" + codaut + "' and s.estsin = 1 " +
                             " and s.codprov = " + codprov + " " +
                             " and to_char(s.fecsin, 'YYYY-MM-DD') >= '" + fecini + "' and to_char(s.fecsin, 'YYYY-MM-DD') <= '" + fechafin + "' " +
                             "order by  1; ";

            }
            if (codaut == "-1" && codprov == "-1")
            {
                findAll = "Select s.codsin, to_char(s.fecsin,'YYYY-MM-DD') as fecsin ,s.horsin,s.zonsin,p.nomprov as nomprov,c.descant as descant,upper(s.dirsin) as dirsin, upper(u.\"PU002\" ) || ' ' || upper ( u.\"PU003\") as \"agente_responsable\", upper(u1.\"PU002\" ) || ' ' || upper ( u1.\"PU003\") as \"usuario_registro\", upper(u2.\"PU002\" ) || ' ' || upper ( u2.\"PU003\") as \"SUPERVISOR\" " +
                            ",a.desaut as \"autoridad\", " +
                            "case when s.regvalsin = 't' then 'OK' ELSE 'PENDIENTE' END AS \"REGISTRO_VALIDADO\" " +
                            " FROM siniestros s " +
                            " inner join \"PUAC\" u on s.ageressin = u.\"PUCOD\" " +
                            " inner join autoridades a on a.codaut = s.codaut and a.estaut = 1 " +
                            " inner join \"PUAC\" u1 on s.\"PUCOD\" = u1.\"PUCOD\" " +
                            " inner join \"PUAC\" u2 on s.\"supressIn\" = u2.\"PUCOD\" " +
                            " INNER JOIN provincias p on  p.codprov =   s.codprov " +
                            " inner join cantones c on c.codcant = s.codcant" +
                            " where  s.estsin = 1 and to_char(s.fecsin, 'YYYY-MM-DD') >= '" + fecini + "' and to_char(s.fecsin, 'YYYY-MM-DD') <= '" + fechafin + "' " +
                            "order by  1; ";
            }
            if (codaut == "-1" && codprov != "-1")
            {
                findAll = "Select s.codsin, to_char(s.fecsin,'YYYY-MM-DD') as fecsin ,s.horsin,s.zonsin,p.nomprov as nomprov,c.descant as descant,upper(s.dirsin) as dirsin, upper(u.\"PU002\" ) || ' ' || upper ( u.\"PU003\") as \"agente_responsable\", upper(u1.\"PU002\" ) || ' ' || upper ( u1.\"PU003\") as \"usuario_registro\", upper(u2.\"PU002\" ) || ' ' || upper ( u2.\"PU003\") as \"SUPERVISOR\" " +
                            ",a.desaut as \"autoridad\", " +
                            "case when s.regvalsin = 't' then 'OK' ELSE 'PENDIENTE' END AS \"REGISTRO_VALIDADO\" " +
                            " FROM siniestros s " +
                            " inner join \"PUAC\" u on s.ageressin = u.\"PUCOD\" " +
                            " inner join autoridades a on a.codaut = s.codaut and a.estaut = 1 " +
                            " inner join \"PUAC\" u1 on s.\"PUCOD\" = u1.\"PUCOD\" " +
                            " inner join \"PUAC\" u2 on s.\"supressIn\" = u2.\"PUCOD\" " +
                            " INNER JOIN provincias p on  p.codprov =   s.codprov " +
                            " inner join cantones c on c.codcant = s.codcant" +
                            " where  to_char(s.fecsin, 'YYYY-MM-DD') >= '" + fecini + "' and to_char(s.fecsin, 'YYYY-MM-DD') <= '" + fechafin + "' " +
                            "and s.codprov = " + codprov + " and s.estsin = 1 " +
                            "order by  1; ";
            }
            if (codaut != "-1" && codprov == "-1")
            {
                findAll = "Select s.codsin, to_char(s.fecsin,'YYYY-MM-DD') as fecsin ,s.horsin,s.zonsin,p.nomprov as nomprov,c.descant as descant,upper(s.dirsin) as dirsin, upper(u.\"PU002\" ) || ' ' || upper ( u.\"PU003\") as \"agente_responsable\", upper(u1.\"PU002\" ) || ' ' || upper ( u1.\"PU003\") as \"usuario_registro\", upper(u2.\"PU002\" ) || ' ' || upper ( u2.\"PU003\") as \"SUPERVISOR\" " +
                            ",a.desaut as \"autoridad\", " +
                            "case when s.regvalsin = 't' then 'OK' ELSE 'PENDIENTE' END AS \"REGISTRO_VALIDADO\" " +
                            " FROM siniestros s " +
                            " inner join \"PUAC\" u on s.ageressin = u.\"PUCOD\" " +
                            " inner join autoridades a on a.codaut = s.codaut and a.estaut = 1 " +
                            " inner join \"PUAC\" u1 on s.\"PUCOD\" = u1.\"PUCOD\" " +
                            " inner join \"PUAC\" u2 on s.\"supressIn\" = u2.\"PUCOD\" " +
                            " INNER JOIN provincias p on  p.codprov =   s.codprov " +
                            " inner join cantones c on c.codcant = s.codcant" +
                            " where  to_char(s.fecsin, 'YYYY-MM-DD') >= '" + fecini + "' and to_char(s.fecsin, 'YYYY-MM-DD') <= '" + fechafin + "' " +
                            "and s.codaut = '" + codaut + "' and s.estsin = 1" +
                            "order by  1; ";
            }
            //s.estsin = 
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Siniestro objSiniestro = new Siniestro();
                    objSiniestro.codsin = Convert.ToInt32(reader[0].ToString());
                    objSiniestro.fecsin = Convert.ToString(reader[1].ToString());
                    objSiniestro.horsin = Convert.ToString(reader[2].ToString());
                    objSiniestro.zonsin = Convert.ToString(reader[3].ToString());
                    objSiniestro.nomprov = Convert.ToString(reader[4].ToString());
                    objSiniestro.descant = Convert.ToString(reader[5].ToString());
                    objSiniestro.dirsin = Convert.ToString(reader[6].ToString());
                    objSiniestro.agente_responsable = Convert.ToString(reader[7].ToString());
                    objSiniestro.USUARIO_REGISTRO = Convert.ToString(reader[8].ToString());
                    objSiniestro.supervisor_responsable = Convert.ToString(reader[9].ToString());
                    objSiniestro.autoridad = Convert.ToString(reader[10].ToString());
                    objSiniestro.REGISTRO_VALIDADO = Convert.ToString(reader[11].ToString());
                    listaSiniestro.Add(objSiniestro);

                }
            }
            catch (Exception)
            {
                throw;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaSiniestro;

        }
        public List<Siniestro> listaSiniestrosPorCodigo(int codSin)
        {
            List<Siniestro> listaSiniestro = new List<Siniestro>();
            //PDAC tabla Departamentos
            string findAll = "select *from siniestros where codsin = " + codSin + "; ";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Siniestro objSiniestro = new Siniestro();
                    objSiniestro.codsin = Convert.ToInt32(reader[0].ToString());
                    objSiniestro.fecsin = Convert.ToString(Convert.ToDateTime(reader[1].ToString()).ToString("yyyy-MM-dd")).Replace('/', '-');
                    objSiniestro.horsin = Convert.ToString(reader[2].ToString()).Substring(0, 5);
                    objSiniestro.latsin = Convert.ToString(reader[3].ToString()).Replace(',', '.');
                    objSiniestro.lonsin = Convert.ToString(reader[4].ToString()).Replace(',', '.');
                    objSiniestro.dirsin = Convert.ToString(reader[5].ToString());
                    objSiniestro.numfalsin = Convert.ToInt32(reader[6].ToString());
                    objSiniestro.numlessin = Convert.ToInt32(reader[7].ToString());
                    objSiniestro.zonsin = Convert.ToString(reader[12].ToString());
                    objSiniestro.traviasin = Convert.ToBoolean(reader[13].ToString());
                    objSiniestro.conatmsin = Convert.ToString(reader[14].ToString());
                    objSiniestro.conviasin = Convert.ToString(reader[15].ToString());
                    objSiniestro.luzartsin = Convert.ToString(reader[16].ToString());
                    objSiniestro.desviasin = Convert.ToString(reader[17].ToString());
                    objSiniestro.limvelsin = Convert.ToInt32(reader[18].ToString());
                    objSiniestro.intsin = Convert.ToString(reader[19].ToString());
                    objSiniestro.matsupviasin = Convert.ToString(reader[20].ToString());
                    objSiniestro.obsviasin = Convert.ToString(reader[21].ToString());
                    objSiniestro.lugviasin = Convert.ToString(reader[22].ToString());
                    objSiniestro.cursin = Convert.ToString(reader[23].ToString());
                    objSiniestro.numcarsin = Convert.ToInt32(reader[24].ToString());
                    objSiniestro.sensin = Convert.ToString(reader[25].ToString());
                    objSiniestro.codtipsin = Convert.ToInt32(reader[28].ToString());
                    objSiniestro.codpar = Convert.ToInt32(reader[29].ToString());
                    objSiniestro.codcant = Convert.ToInt32(reader[30].ToString());
                    objSiniestro.codprov = Convert.ToInt32(reader[31].ToString());
                    objSiniestro.codcaupro = Convert.ToString(reader[32].ToString());
                    objSiniestro.codcir = Convert.ToString(reader[34].ToString());
                    objSiniestro.coddis = Convert.ToString(reader[35].ToString());
                    objSiniestro.codestprocsin = reader[39].ToString() == null ? "0" : Convert.ToString(reader[39].ToString());
                    listaSiniestro.Add(objSiniestro);

                }
            }
            catch (Exception ex)
            {
                throw;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaSiniestro;

        }
        public List<Siniestro> listaVistaSiniestros(int codsin)
        {
            List<Siniestro> listaSiniestro = new List<Siniestro>();
            //PDAC tabla Departamentos
            string findAll = "select to_char(s.fecsin,'YYYY/MM/DD')  as fecsin,s.horsin,s.latsin,s.lonsin,UPPER(s.dirsin) AS dirsin,s.numfalsin,s.numlessin,UPPER(ag.\"PU002\")  || ' ' || UPPER(ag.\"PU003\")   as \"agente_responsable\",  " +
                             " UPPER(sup.\"PU002\") || ' ' || UPPER(sup.\"PU003\") as \"supervisor_responsable\", s.zonsin,s.traviasin,s.conatmsin,s.conviasin,s.luzartsin,s.desviasin,s.limvelsin,s.intsin, " +
                             " s.matsupviasin,s.obsviasin,s.lugviasin,s.cursin,s.numcarsin,s.sensin,UPPER(us.\"PU002\") || ' ' || UPPER(us.\"PU003\") as \"USUARIO_REGISTRO\", " +
                             " upper(au.desaut) as \"autoridad\", ts.destipsin as \"tiposiniestro\", p.despar as \"parroquia\", c.descir as \"subcircuito\", cant.descant as \"canton\", " +
                             " prov.nomprov as \"provincia\", cp.descaupro as \"causa_probable\", cr.descaurea as \"causa_real\", c1.descir as \"circuito\", dis.desdis as \"distrito\" ,s.codsin " +
                             " from siniestros s " +
                             " inner join \"PUAC\" ag on s.ageressin = ag.\"PUCOD\" " +
                             " inner join \"PUAC\" sup on s.\"supressIn\" = sup.\"PUCOD\" " +
                             " inner join \"PUAC\" us on s.\"PUCOD\" = us.\"PUCOD\" " +
                             " inner join autoridades au on au.codaut = s.codaut " +
                             " inner join tipos_siniestros ts on ts.codtipsin = s.codtipsin " +
                             " inner join parroquias p on p.codpar = s.codpar " +
                             " inner join circuitos c on c.codcir = s.codcir " +
                             " inner join cantones cant on cant.codcant = s.codcant " +
                             " inner join provincias prov on prov.codprov = s.codprov " +
                             " inner join causa_probable cp on cp.codcaupro = s.codcaupro " +
                             " inner join causa_real cr on cr.codcaurea = s.codcaurea " +
                             " inner join circuitos c1 on c1.codcir = s.codcir " +
                             " inner join distritos dis on  dis.coddis = s.coddis " +
                             " where codsin = " + codsin + "";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Siniestro objSiniestro = new Siniestro();
                    objSiniestro.fecsin = Convert.ToString(Convert.ToDateTime(reader[0].ToString()).ToString("yyyy-MM-dd")).Replace('/', '-'); //Convert.ToString(reader[0].ToString()).Replace('/', '-');
                    objSiniestro.horsin = Convert.ToString(reader[1].ToString()).Substring(0, 5);
                    objSiniestro.latsin = Convert.ToString(reader[2].ToString()).Replace(',', '.');
                    objSiniestro.lonsin = Convert.ToString(reader[3].ToString()).Replace(',', '.');
                    objSiniestro.dirsin = Convert.ToString(reader[4].ToString());

                    objSiniestro.numfalsin = Convert.ToInt32(reader[5].ToString());
                    objSiniestro.numlessin = Convert.ToInt32(reader[6].ToString());
                    objSiniestro.agente_responsable = Convert.ToString(reader[7].ToString());
                    objSiniestro.supervisor_responsable = Convert.ToString(reader[8].ToString());
                    objSiniestro.zonsin = Convert.ToString(reader[9].ToString());
                    objSiniestro.traviasin = Convert.ToBoolean(reader[10].ToString());
                    objSiniestro.conatmsin = Convert.ToString(reader[11].ToString());
                    objSiniestro.conviasin = Convert.ToString(reader[12].ToString());
                    objSiniestro.luzartsin = Convert.ToString(reader[13].ToString());
                    objSiniestro.desviasin = Convert.ToString(reader[14].ToString());
                    objSiniestro.limvelsin = Convert.ToInt32(reader[15].ToString());
                    objSiniestro.intsin = Convert.ToString(reader[16].ToString());
                    objSiniestro.matsupviasin = Convert.ToString(reader[17].ToString());
                    objSiniestro.obsviasin = Convert.ToString(reader[18].ToString());
                    objSiniestro.lugviasin = Convert.ToString(reader[19].ToString());
                    objSiniestro.cursin = Convert.ToString(reader[20].ToString());
                    objSiniestro.numcarsin = Convert.ToInt32(reader[21].ToString());
                    objSiniestro.sensin = Convert.ToString(reader[22].ToString());
                    objSiniestro.USUARIO_REGISTRO = Convert.ToString(reader[23].ToString());
                    objSiniestro.autoridad = Convert.ToString(reader[24].ToString());
                    objSiniestro.tiposiniestro = Convert.ToString(reader[25].ToString());
                    objSiniestro.parroquia = Convert.ToString(reader[26].ToString());
                    objSiniestro.subcircuito = Convert.ToString(reader[27].ToString());
                    objSiniestro.canton = Convert.ToString(reader[28].ToString());
                    objSiniestro.provincia = Convert.ToString(reader[29].ToString());
                    objSiniestro.causa_probable = Convert.ToString(reader[30].ToString());
                    objSiniestro.causa_real = Convert.ToString(reader[31].ToString());
                    objSiniestro.circuito = Convert.ToString(reader[32].ToString());
                    objSiniestro.distrito = Convert.ToString(reader[33].ToString());
                    objSiniestro.codsin = Convert.ToInt32(reader[34].ToString());

                    listaSiniestro.Add(objSiniestro);

                }
            }
            catch (Exception ex)
            {
                throw ex;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaSiniestro;

        }


        public List<Siniestro> listaVistaSiniestrosPorFechas(string tbFechaini, string tbFechafin, int tbcodprov, string tbcodautoridad)
        {
            List<Siniestro> listaSiniestro = new List<Siniestro>();
            string findAll = "";
            if (tbcodprov == -1 && tbcodautoridad == "-1")// todos
            {
                findAll = "select to_char(s.fecsin,'YYYY/MM/DD')  as fecsin,s.horsin,s.latsin,s.lonsin,UPPER(s.dirsin) AS dirsin,s.numfalsin,s.numlessin,UPPER(ag.\"PU002\")  || ' ' || UPPER(ag.\"PU003\")   as \"agente_responsable\",  " +
                                 " UPPER(sup.\"PU002\") || ' ' || UPPER(sup.\"PU003\") as \"supervisor_responsable\", s.zonsin,s.traviasin,s.conatmsin,s.conviasin,s.luzartsin,s.desviasin,s.limvelsin,s.intsin, " +
                                 " s.matsupviasin,s.obsviasin,s.lugviasin,s.cursin,s.numcarsin,s.sensin,UPPER(us.\"PU002\") || ' ' || UPPER(us.\"PU003\") as \"USUARIO_REGISTRO\", " +
                                 " upper(au.desaut) as \"autoridad\", ts.destipsin as \"tiposiniestro\", p.despar as \"parroquia\", c.descir as \"subcircuito\", cant.descant as \"canton\", " +
                                 " prov.nomprov as \"provincia\", cp.descaupro as \"causa_probable\", cr.descaurea as \"causa_real\", c1.descir as \"circuito\", dis.desdis as \"distrito\" ,s.codsin , s.regvalsin,s.codaut " +
                                 " from siniestros s " +
                                 " left outer join \"PUAC\" ag on s.ageressin = ag.\"PUCOD\" " +
                                 " left outer join \"PUAC\" sup on s.\"supressIn\" = sup.\"PUCOD\" " +
                                 " left outer join \"PUAC\" us on s.\"PUCOD\" = us.\"PUCOD\" " +
                                 " inner join autoridades au on au.codaut = s.codaut " +
                                 " inner join tipos_siniestros ts on ts.codtipsin = s.codtipsin " +
                                 " inner join parroquias p on p.codpar = s.codpar " +
                                 " inner join circuitos c on c.codcir = s.codcir " +
                                 " inner join cantones cant on cant.codcant = s.codcant " +
                                 " inner join provincias prov on prov.codprov = s.codprov " +
                                 " inner join causa_probable cp on cp.codcaupro = s.codcaupro " +
                                 " inner join causa_real cr on cr.codcaurea = s.codcaurea " +
                                 " left outer join circuitos c1 on c1.codcir = s.codcir " +
                                 " left outer join distritos dis on  dis.coddis = s.coddis " +
                                 " where   to_char(s.fecsin,'YYYY-MM-DD') >= '"+ tbFechaini + "' and to_char(s.fecsin,'YYYY-MM-DD') <= '"+ tbFechafin + "' " +
                                 "and s.estsin = 1    "+
                                 " order by s.codsin  ";
            }
            if (tbcodprov != -1 && tbcodautoridad == "-1")// por provincias
            {
                findAll = "select to_char(s.fecsin,'YYYY/MM/DD')  as fecsin,s.horsin,s.latsin,s.lonsin,UPPER(s.dirsin) AS dirsin,s.numfalsin,s.numlessin,UPPER(ag.\"PU002\")  || ' ' || UPPER(ag.\"PU003\")   as \"agente_responsable\",  " +
                                 " UPPER(sup.\"PU002\") || ' ' || UPPER(sup.\"PU003\") as \"supervisor_responsable\", s.zonsin,s.traviasin,s.conatmsin,s.conviasin,s.luzartsin,s.desviasin,s.limvelsin,s.intsin, " +
                                 " s.matsupviasin,s.obsviasin,s.lugviasin,s.cursin,s.numcarsin,s.sensin,UPPER(us.\"PU002\") || ' ' || UPPER(us.\"PU003\") as \"USUARIO_REGISTRO\", " +
                                 " upper(au.desaut) as \"autoridad\", ts.destipsin as \"tiposiniestro\", p.despar as \"parroquia\", c.descir as \"subcircuito\", cant.descant as \"canton\", " +
                                 " prov.nomprov as \"provincia\", cp.descaupro as \"causa_probable\", cr.descaurea as \"causa_real\", c1.descir as \"circuito\", dis.desdis as \"distrito\" ,s.codsin,s.regvalsin ,s.codaut " +
                                 " from siniestros s " +
                                 " left outer join \"PUAC\" ag on s.ageressin = ag.\"PUCOD\" " +
                                 " left outer join \"PUAC\" sup on s.\"supressIn\" = sup.\"PUCOD\" " +
                                 " left outer join \"PUAC\" us on s.\"PUCOD\" = us.\"PUCOD\" " +
                                 " inner join autoridades au on au.codaut = s.codaut " +
                                 " inner join tipos_siniestros ts on ts.codtipsin = s.codtipsin " +
                                 " inner join parroquias p on p.codpar = s.codpar " +
                                 " inner join circuitos c on c.codcir = s.codcir " +
                                 " inner join cantones cant on cant.codcant = s.codcant " +
                                 " inner join provincias prov on prov.codprov = s.codprov " +
                                 " inner join causa_probable cp on cp.codcaupro = s.codcaupro " +
                                 " inner join causa_real cr on cr.codcaurea = s.codcaurea " +
                                 " left outer join circuitos c1 on c1.codcir = s.codcir " +
                                 " left outer join distritos dis on  dis.coddis = s.coddis " +
                                 " where   to_char(s.fecsin,'YYYY-MM-DD') >= '" + tbFechaini + "' and to_char(s.fecsin,'YYYY-MM-DD') <= '" + tbFechafin + "' " +
                                 " and s.codprov = "+tbcodprov+ " and s.estsin =  1 " +
                                  "and s.estsin = 1" +
                                 " order by s.codsin  ";
            }
            if (tbcodprov == -1 && tbcodautoridad != "-1")// por autoridad
            {
                findAll = "select to_char(s.fecsin,'YYYY/MM/DD')  as fecsin,s.horsin,s.latsin,s.lonsin,UPPER(s.dirsin) AS dirsin,s.numfalsin,s.numlessin,UPPER(ag.\"PU002\")  || ' ' || UPPER(ag.\"PU003\")   as \"agente_responsable\",  " +
                                 " UPPER(sup.\"PU002\") || ' ' || UPPER(sup.\"PU003\") as \"supervisor_responsable\", s.zonsin,s.traviasin,s.conatmsin,s.conviasin,s.luzartsin,s.desviasin,s.limvelsin,s.intsin, " +
                                 " s.matsupviasin,s.obsviasin,s.lugviasin,s.cursin,s.numcarsin,s.sensin,UPPER(us.\"PU002\") || ' ' || UPPER(us.\"PU003\") as \"USUARIO_REGISTRO\", " +
                                 " upper(au.desaut) as \"autoridad\", ts.destipsin as \"tiposiniestro\", p.despar as \"parroquia\", c.descir as \"subcircuito\", cant.descant as \"canton\", " +
                                 " prov.nomprov as \"provincia\", cp.descaupro as \"causa_probable\", cr.descaurea as \"causa_real\", c1.descir as \"circuito\", dis.desdis as \"distrito\" ,s.codsin,s.regvalsin,s.codaut  " +
                                 " from siniestros s " +
                                 "  left outer join   \"PUAC\" ag on s.ageressin = ag.\"PUCOD\" " +
                                 "  left outer join   \"PUAC\" sup on s.\"supressIn\" = sup.\"PUCOD\" " +
                                 "  left outer join   \"PUAC\" us on s.\"PUCOD\" = us.\"PUCOD\" " +
                                 "  left outer join   autoridades au on au.codaut = s.codaut " +
                                 "  left outer join   tipos_siniestros ts on ts.codtipsin = s.codtipsin " +
                                 "  left outer join   parroquias p on p.codpar = s.codpar " +
                                 " inner join circuitos c on c.codcir = s.codcir " +
                                 "  left outer join   cantones cant on cant.codcant = s.codcant " +
                                 " inner join provincias prov on prov.codprov = s.codprov " +
                                 " inner join causa_probable cp on cp.codcaupro = s.codcaupro " +
                                 "  left outer   join causa_real cr on cr.codcaurea = s.codcaurea " +
                                 "  left outer   join circuitos c1 on c1.codcir = s.codcir " +
                                 "  left outer   join distritos dis on  dis.coddis = s.coddis " +
                                 " where   to_char(s.fecsin,'YYYY-MM-DD') >= '" + tbFechaini + "' and to_char(s.fecsin,'YYYY-MM-DD') <= '" + tbFechafin + "' " +
                                 " and s.codaut = '" + tbcodautoridad + "'  and s.estsin =  1 " +
                                  "and s.estsin = 1" +
                                 " order by s.codsin  ";
            }
         
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Siniestro objSiniestro = new Siniestro();
                    objSiniestro.fecsin = Convert.ToString(Convert.ToDateTime(reader[0].ToString()).ToString("yyyy-MM-dd")).Replace('/', '-');// Convert.ToString(reader[0].ToString()).Replace('/', '-');
                    objSiniestro.horsin = Convert.ToString(reader[1].ToString()).Substring(0, 5);
                    objSiniestro.latsin = Convert.ToString(reader[2].ToString()).Replace(',', '.');
                    objSiniestro.lonsin = Convert.ToString(reader[3].ToString()).Replace(',', '.');
                    objSiniestro.dirsin = Convert.ToString(reader[4].ToString());

                    objSiniestro.numfalsin = Convert.ToInt32(reader[5].ToString());
                    objSiniestro.numlessin = Convert.ToInt32(reader[6].ToString());
                    objSiniestro.agente_responsable = Convert.ToString(reader[7].ToString());
                    objSiniestro.supervisor_responsable = Convert.ToString(reader[8].ToString());
                    objSiniestro.zonsin = Convert.ToString(reader[9].ToString());
                    objSiniestro.traviasin = Convert.ToBoolean(reader[10].ToString());
                    objSiniestro.conatmsin = Convert.ToString(reader[11].ToString());
                    objSiniestro.conviasin = Convert.ToString(reader[12].ToString());
                    objSiniestro.luzartsin = Convert.ToString(reader[13].ToString());
                    objSiniestro.desviasin = Convert.ToString(reader[14].ToString());
                    objSiniestro.limvelsin = Convert.ToInt32(reader[15].ToString());
                    objSiniestro.intsin = Convert.ToString(reader[16].ToString());
                    objSiniestro.matsupviasin = Convert.ToString(reader[17].ToString());
                    objSiniestro.obsviasin = Convert.ToString(reader[18].ToString());
                    objSiniestro.lugviasin = Convert.ToString(reader[19].ToString());
                    objSiniestro.cursin = Convert.ToString(reader[20].ToString());
                    objSiniestro.numcarsin = Convert.ToInt32(reader[21].ToString());
                    objSiniestro.sensin = Convert.ToString(reader[22].ToString());
                    objSiniestro.USUARIO_REGISTRO = Convert.ToString(reader[23].ToString());
                    objSiniestro.autoridad = Convert.ToString(reader[24].ToString());
                    objSiniestro.tiposiniestro = Convert.ToString(reader[25].ToString());
                    objSiniestro.parroquia = Convert.ToString(reader[26].ToString());
                    objSiniestro.subcircuito = Convert.ToString(reader[27].ToString());
                    objSiniestro.canton = Convert.ToString(reader[28].ToString());
                    objSiniestro.provincia = Convert.ToString(reader[29].ToString());
                    objSiniestro.causa_probable = Convert.ToString(reader[30].ToString());
                    objSiniestro.causa_real = Convert.ToString(reader[31].ToString());
                    objSiniestro.circuito = Convert.ToString(reader[32].ToString());
                    objSiniestro.distrito = Convert.ToString(reader[33].ToString());
                    objSiniestro.codsin = Convert.ToInt32(reader[34].ToString());
                    objSiniestro.regvalsin = Convert.ToBoolean(reader[35].ToString());
                    objSiniestro.codaut = Convert.ToString(reader[36].ToString());

                    listaSiniestro.Add(objSiniestro);

                }
            }
            catch (Exception ex)
            {
                throw ex;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaSiniestro;

        }
        public List<Vehiculo> listaVehiculosInvolucrados(int codsin)
        {
            List<Vehiculo> listaVehiculosInvolucrado = new List<Vehiculo>();
            if (codsin != 0)
            {
                string findAll = "SELECT a.placvehinv,a.danmatvehinv,a.matvigvehinv,a.chavehinv,a.marvehinv,a.modvehinv,a.anivehinv,a.cilvehinv,a.segprivehinv,a.matpelvehinv,b.desser,c.destipveh, a.codvehinv,s.dessubveh FROM vehiculos_involucrados a INNER JOIN servicio_vehiculos b ON a.codser = b.codser INNER JOIN tipo_vehiculos c ON c.codtipveh = a.codtipve left outer join subtipo_vehiculos s on a.codsubveh = s.codsubveh where a.codsin in (" + codsin + ") and a.estvehinv = 1 order by  a.codvehinv desc; ";
                try
                {


                    comando = new NpgsqlCommand(findAll, objConexinDB.getCon());
                    comando.CommandType = CommandType.Text;
                    objConexinDB.getCon().Open();
                    NpgsqlTransaction t = objConexinDB.getCon().BeginTransaction();
                    NpgsqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        Vehiculo objVhl = new Vehiculo();
                        objVhl.placvehinv = Convert.ToString(reader[0].ToString());
                        objVhl.danmatvehinv = Convert.ToBoolean(reader[1].ToString());
                        objVhl.matvigvehinv = Convert.ToBoolean(reader[2].ToString());
                        objVhl.chavehinv = Convert.ToString(reader[3].ToString());
                        objVhl.marvehinv = Convert.ToString(reader[4].ToString());
                        objVhl.modvehinv = Convert.ToString(reader[5].ToString());
                        objVhl.anivehinv = Convert.ToInt32(reader[6].ToString());
                        objVhl.cilvehinv = Convert.ToString(reader[7].ToString());
                        objVhl.segprivehinv = Convert.ToBoolean(reader[8].ToString());
                        objVhl.matpelvehinv = Convert.ToString(reader[9].ToString());
                        objVhl.desser = Convert.ToString(reader[10].ToString());
                        objVhl.destipveh = Convert.ToString(reader[11].ToString());
                        objVhl.codvehinv = Convert.ToInt32(reader[12].ToString());
                        objVhl.dessubveh = Convert.ToString(reader[13].ToString());

                        listaVehiculosInvolucrado.Add(objVhl);

                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                    objConexinDB.getCon().Close();
                    objConexinDB.closeDB();
                }
                finally
                {
                    objConexinDB.getCon().Close();
                    objConexinDB.closeDB();
                }
            }
          

            return listaVehiculosInvolucrado;

        }
        public List<Vehiculo> listaVehiculosInvolucradosPorRangoFechas(string codsin)
        {
            List<Vehiculo> listaVehiculosInvolucrado = new List<Vehiculo>();

            codsin = codsin.TrimEnd(','); 
            string findAll = "SELECT a.placvehinv,a.danmatvehinv,a.matvigvehinv,a.chavehinv,a.marvehinv,a.modvehinv,a.anivehinv,a.cilvehinv,a.segprivehinv,a.matpelvehinv,b.desser,c.destipveh, a.codvehinv,s.dessubveh,a.codsin FROM vehiculos_involucrados a INNER JOIN servicio_vehiculos b ON a.codser = b.codser INNER JOIN tipo_vehiculos c ON c.codtipveh = a.codtipve left outer join subtipo_vehiculos s on a.codsubveh = s.codsubveh where a.codsin in (" + codsin + ") and a.estvehinv = 1 order by a.codsin ; ";
            try
            {


                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());
                comando.CommandType = CommandType.Text;
                objConexinDB.getCon().Open();
                NpgsqlTransaction t = objConexinDB.getCon().BeginTransaction();
                NpgsqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Vehiculo objVhl = new Vehiculo();
                    objVhl.placvehinv = Convert.ToString(reader[0].ToString());
                    objVhl.danmatvehinv = Convert.ToBoolean(reader[1].ToString());
                    objVhl.matvigvehinv = Convert.ToBoolean(reader[2].ToString());
                    objVhl.chavehinv = Convert.ToString(reader[3].ToString());
                    objVhl.marvehinv = Convert.ToString(reader[4].ToString());
                    objVhl.modvehinv = Convert.ToString(reader[5].ToString());
                    objVhl.anivehinv = Convert.ToInt32(reader[6].ToString());
                    objVhl.cilvehinv = Convert.ToString(reader[7].ToString());
                    objVhl.segprivehinv = Convert.ToBoolean(reader[8].ToString());
                    objVhl.matpelvehinv = Convert.ToString(reader[9].ToString());
                    objVhl.desser = Convert.ToString(reader[10].ToString());
                    objVhl.destipveh = Convert.ToString(reader[11].ToString());
                    objVhl.codvehinv = Convert.ToInt32(reader[12].ToString());
                    objVhl.dessubveh = Convert.ToString(reader[13].ToString());
                    objVhl.codsin = Convert.ToInt32(reader[14].ToString());
                    
                    listaVehiculosInvolucrado.Add(objVhl);

                }
            }
            catch (Exception ex)
            {
                throw ex;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaVehiculosInvolucrado;

        }
        public List<Vehiculo> listaVehiculosInvolucradosPorCodigo(int codvhl)
        {
            List<Vehiculo> listaVehiculosInvolucrado = new List<Vehiculo>();
            // objConexinDB = ConexionDB.saberEstado();
            string findAll = "select  a.*,s.dessubveh from vehiculos_involucrados a  left outer join subtipo_vehiculos s on a.codsubveh = s.codsubveh where a.codvehinv = " + codvhl + "; ";
            try
            {


                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());
                comando.CommandType = CommandType.Text;
                objConexinDB.getCon().Open();
                NpgsqlTransaction t = objConexinDB.getCon().BeginTransaction();
                NpgsqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Vehiculo objVhl = new Vehiculo();
                    objVhl.codvehinv = Convert.ToInt32(reader[0].ToString());
                    objVhl.placvehinv = Convert.ToString(reader[1].ToString());
                    objVhl.danmatvehinv = Convert.ToBoolean(reader[2].ToString());
                    objVhl.matvigvehinv = Convert.ToBoolean(reader[3].ToString());
                    objVhl.chavehinv = Convert.ToString(reader[4].ToString());
                    objVhl.marvehinv = Convert.ToString(reader[5].ToString());
                    objVhl.modvehinv = Convert.ToString(reader[6].ToString());
                    objVhl.anivehinv = Convert.ToInt32(reader[7].ToString());
                    objVhl.cilvehinv = Convert.ToString(reader[8].ToString());
                    objVhl.segprivehinv = Convert.ToBoolean(reader[9].ToString());
                    objVhl.matpelvehinv = Convert.ToString(reader[10].ToString());
                    objVhl.codser = Convert.ToInt32(reader[11].ToString());
                    objVhl.codtipve = Convert.ToInt32(reader[12].ToString());
                    objVhl.codsubveh = Convert.ToInt32(reader[14].ToString());
                    objVhl.dessubveh = Convert.ToString(reader[15].ToString());
                    

                    listaVehiculosInvolucrado.Add(objVhl);

                }
            }
            catch (Exception ex)
            {
                throw ex;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaVehiculosInvolucrado;

        }

        public List<Victimas> listaVictimasInvolucrados(int codsin)
        {
            List<Victimas> listaVictimasInvolucrado = new List<Victimas>();
            //  objConexinDB = ConexionDB.saberEstado();
            string findAll = " select a.tipidenvicinv,numidenvicinv,a.edavicinv,   CASE WHEN a.sexvicinv='M' THEN 'MUJER'  WHEN a.sexvicinv='H' THEN 'HOMBRE' ELSE 'NO IDENTIFICADO' end AS SEXO, " +
                              " a.convicinv24,a.convicinv30,a.tipparvicinv,case when a.casvicinv = 'T' then 'SI' ELSE 'NO' END AS \"USO_CASO\", CASE WHEN a.cinvicinv = 'T' THEN 'SI' ELSE 'NO' END AS \"USO_CINTU\",posvicinv, CASE WHEN conalcvicinv = 'T' THEN 'SI' ELSE 'NO' END AS \"CONS_ALCOHOL\", trim(b.placvehinv) AS \"PLACAVHL\", a.codvicinv " +
                              " from victimas_involucradas a inner join vehiculos_involucrados b on a.codveh = b.codvehinv where a.codsin = " + codsin + " and a.estvicinv = 1 order by 12; ";
            try
            {
                //if (objConexinDB.getCon().State.ToString() == "Closed")


                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                NpgsqlTransaction t = objConexinDB.getCon().BeginTransaction();
                NpgsqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Victimas objVict = new Victimas();
                    objVict.tipidenvicinv = Convert.ToString(reader[0].ToString());
                    objVict.numidenvicinv = Convert.ToString(reader[1].ToString());
                    objVict.edavicinv = Convert.ToInt32(reader[2].ToString());
                    objVict.sexo = Convert.ToString(reader[3].ToString());
                    objVict.convicinv24 = Convert.ToString(reader[4].ToString());
                    objVict.convicinv30 = Convert.ToString(reader[5].ToString());
                    objVict.tipparvicinv = Convert.ToString(reader[6].ToString());
                    objVict.USO_CASO = Convert.ToString(reader[7].ToString());
                    objVict.USO_CINTU = Convert.ToString(reader[8].ToString());
                    objVict.posvicinv = Convert.ToString(reader[9].ToString());
                    objVict.CONS_ALCOHOL = Convert.ToString(reader[10].ToString());
                    objVict.PLACAVHL = Convert.ToString(reader[11].ToString());
                    objVict.codvicinv = Convert.ToInt32(reader[12].ToString());
                    listaVictimasInvolucrado.Add(objVict);

                }
            }
            catch (Exception ex)
            {
                throw ex;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaVictimasInvolucrado;

        }
        public List<Victimas> listaVictimasInvolucradosPorRangoFechas(string codsin)
        {
            List<Victimas> listaVictimasInvolucrado = new List<Victimas>();
            codsin = codsin.TrimEnd(',');
            string findAll = " select a.tipidenvicinv,numidenvicinv,a.edavicinv,   CASE WHEN a.sexvicinv='H' THEN 'HOMBRE'  WHEN a.sexvicinv='M' THEN 'MUJER' ELSE 'NO IDENTIFICADO' end AS SEXO, " +
                              " a.convicinv24,a.convicinv30,a.tipparvicinv,case when a.casvicinv = 'T' then 'SI' ELSE 'NO' END AS \"USO_CASO\", CASE WHEN a.cinvicinv = 'T' THEN 'SI' ELSE 'NO' END AS \"USO_CINTU\",posvicinv, CASE WHEN conalcvicinv = 'T' THEN 'SI' ELSE 'NO' END AS \"CONS_ALCOHOL\", trim(b.placvehinv) AS \"PLACAVHL\", a.codvicinv ,a.codsin" +
                              " from victimas_involucradas a inner join vehiculos_involucrados b on a.codveh = b.codvehinv where a.codsin in( " + codsin + ") and a.estvicinv = 1 order by a.codsin; ";
            try
            {
                //if (objConexinDB.getCon().State.ToString() == "Closed")


                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                NpgsqlTransaction t = objConexinDB.getCon().BeginTransaction();
                NpgsqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Victimas objVict = new Victimas();
                    objVict.tipidenvicinv = Convert.ToString(reader[0].ToString());
                    objVict.numidenvicinv = Convert.ToString(reader[1].ToString());
                    objVict.edavicinv = Convert.ToInt32(reader[2].ToString());
                    objVict.sexo = Convert.ToString(reader[3].ToString());
                    objVict.convicinv24 = Convert.ToString(reader[4].ToString());
                    objVict.convicinv30 = Convert.ToString(reader[5].ToString());
                    objVict.tipparvicinv = Convert.ToString(reader[6].ToString());
                    objVict.USO_CASO = Convert.ToString(reader[7].ToString());
                    objVict.USO_CINTU = Convert.ToString(reader[8].ToString());
                    objVict.posvicinv = Convert.ToString(reader[9].ToString());
                    objVict.CONS_ALCOHOL = Convert.ToString(reader[10].ToString());
                    objVict.PLACAVHL = Convert.ToString(reader[11].ToString());
                    objVict.codvicinv = Convert.ToInt32(reader[12].ToString());
                    objVict.codsin = Convert.ToInt32(reader[13].ToString());
                    
                    listaVictimasInvolucrado.Add(objVict);

                }
            }
            catch (Exception ex)
            {
                throw ex;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaVictimasInvolucrado;

        }
        public List<Victimas> listaVictimasInvolucradosPorCodigo(int codVictima)
        {
            List<Victimas> listaVictimasInvolucrado = new List<Victimas>();

            string _acciones_peaton = listaAccionesPeatonPorCodigoVic(codVictima);
             

            string findAll = " select a.* " +
                              " from victimas_involucradas a  where a.codvicinv = " + codVictima + " order by 1; ";
            try
            {
                //if (objConexinDB.getCon().State.ToString() == "Closed")


                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                NpgsqlTransaction t = objConexinDB.getCon().BeginTransaction();
                NpgsqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Victimas objVict = new Victimas();
                    objVict.codvicinv = Convert.ToInt32(reader[0].ToString());
                    objVict.tipidenvicinv = Convert.ToString(reader[1].ToString());
                    objVict.numidenvicinv = Convert.ToString(reader[2].ToString());
                    objVict.edavicinv = Convert.ToInt32(reader[3].ToString());
                    objVict.sexo = Convert.ToString(reader[4].ToString());
                    objVict.convicinv24 = Convert.ToString(reader[5].ToString());
                    objVict.convicinv30 = Convert.ToString(reader[6].ToString());
                    objVict.tipparvicinv = Convert.ToString(reader[7].ToString());
                    objVict.casvicinv = Convert.ToBoolean(reader[8].ToString());
                    objVict.cinvicinv = Convert.ToBoolean(reader[9].ToString());
                    objVict.posvicinv = Convert.ToString(reader[10].ToString());
                    objVict.conalcvicinv = Convert.ToBoolean(reader[11].ToString());
                    objVict.codveh = Convert.ToInt32(reader[13].ToString());
                    objVict.codaccpea = _acciones_peaton;
                    listaVictimasInvolucrado.Add(objVict);

                }
            }
            catch (Exception ex)
            {
                throw ex;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaVictimasInvolucrado;

        }
        public List<AccionesPeaton> listaVistaAccionesPeatones(int codsin)
        {
            List<AccionesPeaton> listaAccionesPeaton = new List<AccionesPeaton>();
            //  objConexinDB = ConexionDB.saberEstado();
            string findAll = " select v.numidenvicinv,a.desaccpea,v.convicinv24,c.placvehinv,a.codaccpea from acciones_peaton a " +
                               "inner join victimas_involucradas v on a.codvicinv = v.codvicinv " +
                               "inner join  vehiculos_involucrados c on c.codvehinv = v.codveh " +
                               "where a.estaccpea =1 and  v.codsin = " + codsin + "  order by 1; ";
            try
            {



                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                NpgsqlTransaction t = objConexinDB.getCon().BeginTransaction();
                NpgsqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    AccionesPeaton objAccionesPeaton = new AccionesPeaton();
                    objAccionesPeaton.numidenvicinv = Convert.ToString(reader[0].ToString());
                    objAccionesPeaton.desaccpea = Convert.ToString(reader[1].ToString());
                    objAccionesPeaton.convicinv24 = Convert.ToString(reader[2].ToString());
                    objAccionesPeaton.placvehinv = Convert.ToString(reader[3].ToString());
                    objAccionesPeaton.codaccpea = Convert.ToInt32(reader[4].ToString());

                    listaAccionesPeaton.Add(objAccionesPeaton);

                }
            }
            catch (Exception ex)
            {
                throw ex;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaAccionesPeaton;

        }
        public List<AccionesPeaton> listaVistaAccionesPeatonesPorRangoFechas(string codsin)
        {
            List<AccionesPeaton> listaAccionesPeaton = new List<AccionesPeaton>();
            codsin = codsin.TrimEnd(',');
            string findAll = " select v.numidenvicinv,a.desaccpea,v.convicinv24,c.placvehinv,a.codaccpea,v.codsin from acciones_peaton a " +
                               "inner join victimas_involucradas v on a.codvicinv = v.codvicinv " +
                               "inner join  vehiculos_involucrados c on c.codvehinv = v.codveh " +
                               "where a.estaccpea =1 and  v.codsin in ( " + codsin + ")  order by v.codsin; ";
            try
            {



                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                NpgsqlTransaction t = objConexinDB.getCon().BeginTransaction();
                NpgsqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    AccionesPeaton objAccionesPeaton = new AccionesPeaton();
                    objAccionesPeaton.numidenvicinv = Convert.ToString(reader[0].ToString());
                    objAccionesPeaton.desaccpea = Convert.ToString(reader[1].ToString());
                    objAccionesPeaton.convicinv24 = Convert.ToString(reader[2].ToString());
                    objAccionesPeaton.placvehinv = Convert.ToString(reader[3].ToString());
                    objAccionesPeaton.codaccpea = Convert.ToInt32(reader[4].ToString());
                    objAccionesPeaton.codsin = Convert.ToInt32(reader[5].ToString());

                    listaAccionesPeaton.Add(objAccionesPeaton);

                }
            }
            catch (Exception ex)
            {
                throw ex;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaAccionesPeaton;

        }

        public List<AccionesPeaton> listaVistaAccionesPeatonesPorCodigo(int codAccionPeaton)
        {
            List<AccionesPeaton> listaAccionesPeaton = new List<AccionesPeaton>();
            //  objConexinDB = ConexionDB.saberEstado();
            string findAll = " select * from acciones_peaton where codaccpea  =  " + codAccionPeaton + " order by 1; ";
            try
            {



                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                NpgsqlTransaction t = objConexinDB.getCon().BeginTransaction();
                NpgsqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    AccionesPeaton objAccionesPeaton = new AccionesPeaton();
                    objAccionesPeaton.codaccpea = Convert.ToInt32(reader[0].ToString());
                    objAccionesPeaton.desaccpea = Convert.ToString(reader[1].ToString());
                    objAccionesPeaton.codvicinv = Convert.ToInt32(reader[2].ToString());

                    listaAccionesPeaton.Add(objAccionesPeaton);

                }
            }
            catch (Exception ex)
            {
                throw ex;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaAccionesPeaton;

        }


        public List<DanioMaterial> listaVistaDaniosTerceros(int codsin)
        {
            List<DanioMaterial> listaDanioTercero = new List<DanioMaterial>();
            ///  objConexinDB = ConexionDB.saberEstado();
            string findAll = "select b.destipdater, a.obsdater,a.codsin,a.coddater from danios_terceros a inner join tipos_danios_terceros b on b.codtipdater = a.codtipdater where a.codsin = " + codsin + " and a.estdater = 1 order by 1; ";
            try
            {

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                NpgsqlTransaction t = objConexinDB.getCon().BeginTransaction();
                NpgsqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    DanioMaterial objDanioTerc = new DanioMaterial();
                    objDanioTerc.destipdater = Convert.ToString(reader[0].ToString());
                    objDanioTerc.obsdater = Convert.ToString(reader[1].ToString());
                    objDanioTerc.codsin = Convert.ToInt32(reader[2].ToString());
                    objDanioTerc.coddater = Convert.ToInt32(reader[3].ToString());
                    listaDanioTercero.Add(objDanioTerc);

                }
            }
            catch (Exception ex)
            {
                throw ex;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaDanioTercero;

        }
        public List<DanioMaterial> listaVistaDaniosTercerosPorRangoFehcas(string codsin)
        {
            List<DanioMaterial> listaDanioTercero = new List<DanioMaterial>();
            codsin = codsin.TrimEnd(',');
            string findAll = "select b.destipdater, a.obsdater,a.codsin,a.coddater from danios_terceros a inner join tipos_danios_terceros b on b.codtipdater = a.codtipdater where a.codsin in ( " + codsin + " ) and a.estdater = 1 order by  a.codsin; ";
            try
            {

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                NpgsqlTransaction t = objConexinDB.getCon().BeginTransaction();
                NpgsqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    DanioMaterial objDanioTerc = new DanioMaterial();
                    objDanioTerc.destipdater = Convert.ToString(reader[0].ToString());
                    objDanioTerc.obsdater = Convert.ToString(reader[1].ToString());
                    objDanioTerc.codsin = Convert.ToInt32(reader[2].ToString());
                    objDanioTerc.coddater = Convert.ToInt32(reader[3].ToString());
                    listaDanioTercero.Add(objDanioTerc);

                }
            }
            catch (Exception ex)
            {
                throw ex;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaDanioTercero;

        }
        public List<DanioMaterial> listaVistaDaniosTercerosPorCodigo(int codDanio)
        {
            List<DanioMaterial> listaDanioTercero = new List<DanioMaterial>();
            ///  objConexinDB = ConexionDB.saberEstado();
            string findAll = "select  * from danios_terceros where coddater = " + codDanio + " order by 1; ";
            try
            {

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                NpgsqlTransaction t = objConexinDB.getCon().BeginTransaction();
                NpgsqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    DanioMaterial objDanioTerc = new DanioMaterial();
                    objDanioTerc.coddater = Convert.ToInt32(reader[0].ToString());
                    objDanioTerc.obsdater = Convert.ToString(reader[1].ToString());
                    objDanioTerc.codtipdater = Convert.ToInt32(reader[2].ToString());
                    objDanioTerc.codsin = Convert.ToInt32(reader[3].ToString());
                    listaDanioTercero.Add(objDanioTerc);

                }
            }
            catch (Exception ex)
            {
                throw ex;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaDanioTercero;

        }
        public List<Siniestro> listaProvincias()
        {
            List<Siniestro> listaProvincia = new List<Siniestro>();

            //PDAC tabla Departamentos
            string findAll = "Select * FROM provincias WHERE CODPROV not in( 91,90);";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Siniestro objSiniestro = new Siniestro();
                    objSiniestro.codprov = Convert.ToInt32(reader[0].ToString());
                    objSiniestro.nomprov = Convert.ToString(reader[1].ToString());

                    listaProvincia.Add(objSiniestro);

                }
            }
            catch (Exception)
            {
                throw;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaProvincia;

        }
        public List<SelectListItem> listaProvinciasEdit()
        {
            List<SelectListItem> listaProvincia = new List<SelectListItem>();

            //PDAC tabla Departamentos
            string findAll = "Select * FROM provincias WHERE CODPROV not in( 91,90);";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    CargaDropDownList objSiniestro = new CargaDropDownList();
                    objSiniestro.codigo = Convert.ToString(reader[0].ToString());
                    objSiniestro.nombre = Convert.ToString(reader[1].ToString());
                    listaProvincia.Add(new SelectListItem() { Value = objSiniestro.codigo.ToString(), Text = objSiniestro.nombre.ToString() });


                }
            }
            catch (Exception)
            {
                throw;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaProvincia;

        }
        public List<SelectListItem> listaProvinciasEditVista()
        {
            List<SelectListItem> listaProvincia = new List<SelectListItem>();

            //PDAC tabla Departamentos
            string findAll = "Select '-1','TODOS' FROM provincias WHERE CODPROV not in( 91,90) " +
                            " UNION "+
                            "Select* FROM provincias WHERE CODPROV not in( 91,90) " +
                            "ORDER BY 1; ";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    CargaDropDownList objSiniestro = new CargaDropDownList();
                    objSiniestro.codigo = Convert.ToString(reader[0].ToString());
                    objSiniestro.nombre = Convert.ToString(reader[1].ToString());
                    listaProvincia.Add(new SelectListItem() { Value = objSiniestro.codigo.ToString(), Text = objSiniestro.nombre.ToString() });


                }
            }
            catch (Exception)
            {
                throw;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaProvincia;

        }
        public List<SelectListItem> listaCantonesPorProvincias(int codprov)
        {
            List<SelectListItem> listaCantones = new List<SelectListItem>();

            //PDAC tabla Departamentos
            string findAll = "Select  codcant,descant FROM cantones where  codprov = " + codprov + "  ORDER BY 1 ";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    CargaDropDownList objSiniestro = new CargaDropDownList();
                    objSiniestro.codigo = Convert.ToString(reader[0].ToString());
                    objSiniestro.nombre = Convert.ToString(reader[1].ToString());
                    listaCantones.Add(new SelectListItem() { Value = objSiniestro.codigo.ToString(), Text = objSiniestro.nombre.ToString() });
                }
            }
            catch (Exception ex)
            {
                throw ex;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaCantones;

        }
        public List<SelectListItem> listaParroquiasPorCantones(int codcant, int codprov)
        {
            List<SelectListItem> listaParroquias = new List<SelectListItem>();

            //PDAC tabla Departamentos
            string findAll = "select codpar,despar,zona from parroquias where estpar = 1 and codcant = " + codcant + " ;";//and codprov = "+codprov+"
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    CargaDropDownList objSiniestro = new CargaDropDownList();
                    objSiniestro.codigo = Convert.ToString(reader[0].ToString());
                    objSiniestro.nombre = Convert.ToString(reader[1].ToString());
                    objSiniestro.zona = Convert.ToString(reader[2].ToString());
                    listaParroquias.Add(new SelectListItem() { Value = objSiniestro.codigo.ToString(), Text = objSiniestro.nombre.ToString()  });
                }
            }
            catch (Exception ex)
            {
                throw ex;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaParroquias;

        }

        public List<SelectListItem> listarTodasParroquias()
        {
            List<SelectListItem> listaParroquias = new List<SelectListItem>();

            //PDAC tabla Departamentos
            string findAll = "select '-1','SELECCIONAR' UNION select codpar,despar from parroquias where estpar = 1 AND codpar <> 1  ORDER BY 1;";//and codprov = "+codprov+"
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    CargaDropDownList objSiniestro = new CargaDropDownList();
                    objSiniestro.codigo = Convert.ToString(reader[0].ToString());
                    objSiniestro.nombre = Convert.ToString(reader[1].ToString());
                    listaParroquias.Add(new SelectListItem() { Value = objSiniestro.codigo.ToString(), Text = objSiniestro.nombre.ToString() });
                }
            }
            catch (Exception ex)
            {
                throw ex;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaParroquias;

        }
        public List<SelectListItem> listaCiruitos(int codCant, int codPar)
        {
            List<SelectListItem> listaCircuito = new List<SelectListItem>();

            //PDAC tabla Departamentos " and c.codparcir = "+ codPar + 
            string findAll = "select DISTINCT ON ( descir) descir ,codcir  from circuitos c where estcir = 1 and c.cancir = " + codCant + "   order by 1;";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    CargaDropDownList objSiniestro = new CargaDropDownList();
                    objSiniestro.codigo = Convert.ToString(reader[1].ToString());
                    objSiniestro.nombre = Convert.ToString(reader[0].ToString());
                    listaCircuito.Add(new SelectListItem() { Value = objSiniestro.codigo.ToString(), Text = objSiniestro.nombre.ToString() });
                }
            }
            catch (Exception)
            {
                throw;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaCircuito;

        }
        public List<SelectListItem> listaTodosCiruitos()
        {
            List<SelectListItem> listaCircuito = new List<SelectListItem>();

            //PDAC tabla Departamentos " and c.codparcir = "+ codPar + 
            string findAll = " select 'SELECCIONAR','-1'  UNION select DISTINCT ON ( descir) descir ,codcir  from circuitos c where estcir = 1  order by 1;";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    CargaDropDownList objSiniestro = new CargaDropDownList();
                    objSiniestro.codigo = Convert.ToString(reader[1].ToString());
                    objSiniestro.nombre = Convert.ToString(reader[0].ToString());
                    listaCircuito.Add(new SelectListItem() { Value = objSiniestro.codigo.ToString(), Text = objSiniestro.nombre.ToString() });
                }
            }
            catch (Exception)
            {
                throw;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaCircuito;

        }
        public List<CargaDropDownList> listaDistritos(int codProv, int codCant,int codpar)
        {
            List<CargaDropDownList> listaDistrito = new List<CargaDropDownList>();
            string codcirc = "";
            string zona = "";
            string coddistri = "";
            if (codCant == 1703) codCant = 1705;
            if (codCant == 1704) codCant = 1702;
            if (codCant == 1708 || codCant == 1709) codCant = 1707;
            /// busco elcodigo del circuito
            var a = listaParroquiaPorcodigo(codpar,codCant);
            if (a.Count() > 0)
            {
                foreach (var d in a)
                {
                    codcirc = d.codigo.ToString();
                    zona = d.nombre;
                    coddistri = codcirc.Substring(0,5);

                }
            }

            string findAll = "select coddis,desdis from distritos d where estdis = 1  AND d.provdis = " + codProv + " and d.candis = "+ codCant + " order by 1 ;";
            try
            {
               // objConexinDB.getCon().Open();

                //comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

               // NpgsqlDataReader reader = comando.ExecuteReader();
               // while (reader.Read())
               // {
                    CargaDropDownList objSiniestro = new CargaDropDownList();
                    objSiniestro.codigo = Convert.ToString(codcirc);
                    objSiniestro.nombre = Convert.ToString(zona);
                    objSiniestro.zona = Convert.ToString(coddistri);
                listaDistrito.Add(objSiniestro);
              //  }
            }
            catch (Exception ex)
            {
                throw ex;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaDistrito;

        }
        



        public List<CargaDropDownList> listaParroquiaPorcodigo(int codpar,int codcant)
        {
            List<CargaDropDownList> listaDistrito = new List<CargaDropDownList>();
            //and   d.candis = "+codCant+" 
          

            string findAll = "select codcir,zona from parroquias where codpar = "+ codpar + " and codcant = "+ codcant + "  order by 1 ;";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    CargaDropDownList objGeo = new CargaDropDownList();
                    objGeo.codigo = Convert.ToString(reader[0].ToString());
                    objGeo.nombre = Convert.ToString(reader[1].ToString());

                    listaDistrito.Add(objGeo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaDistrito;

        }
        public List<SelectListItem> listaDistritos(int codProv, int codCant)
        {
            List<SelectListItem> listaDistrito = new List<SelectListItem>();
            string codcirc = "";
            string zona = "";
            string coddistri = "";
            if (codCant == 1703) codCant = 1705;
            if (codCant == 1704) codCant = 1702;
            if (codCant == 1708 || codCant == 1709) codCant = 1707;
            /// busco elcodigo del circuito
           

            string findAll = "select coddis,desdis from distritos d where estdis = 1  AND d.provdis = " + codProv + " and d.candis = " + codCant + " order by 1 ;";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    CargaDropDownList objSiniestro = new CargaDropDownList();
                    objSiniestro.codigo = Convert.ToString(reader[1].ToString());
                    objSiniestro.nombre = Convert.ToString(reader[0].ToString());
                    listaDistrito.Add(new SelectListItem() { Value = objSiniestro.codigo.ToString(), Text = objSiniestro.nombre.ToString() });
                }
            }
            catch (Exception ex)
            {
                throw ex;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }


            return listaDistrito;

        }
        public List<SelectListItem> listaTodosDistritos()
        {
            List<SelectListItem> listaDistrito = new List<SelectListItem>();
            //and   d.candis = "+codCant+" 
           

            string findAll = "select 'SELECCIONAR' ,'-1' UNION  select desdis,coddis from distritos d where estdis = 1   order by 2 ;";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    CargaDropDownList objSiniestro = new CargaDropDownList();
                    objSiniestro.codigo = Convert.ToString(reader[1].ToString());
                    objSiniestro.nombre = Convert.ToString(reader[0].ToString());
                    listaDistrito.Add(new SelectListItem() { Value = objSiniestro.codigo.ToString(), Text = objSiniestro.nombre.ToString() });
                }
            }
            catch (Exception ex)
            {
                throw ex;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaDistrito;

        }
        public string datos(object carga)
        {
            // Volver JSON data 


            string retJSON = JsonConvert.ToString(carga);
            return retJSON.Replace("\\\" ", " ");

        }
        public class CiudadEnt
        {
            public string descant { get; set; }
            public int codcant { get; set; }
        }

        public List<SelectListItem> listatipoServicioVehiculos()
        {
            List<SelectListItem> listaServiciosVehiculos = new List<SelectListItem>();


            string findAll = "Select -1 as codser,'SELECCIONAR' as desser UNION Select codser,desser FROM servicio_vehiculos where estser = 1 ORDER BY 1;";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());
                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Siniestro objSiniestro = new Siniestro();
                    objSiniestro.codser = Convert.ToInt32(reader[0].ToString());
                    objSiniestro.desser = Convert.ToString(reader[1].ToString());

                    listaServiciosVehiculos.Add(new SelectListItem() { Value = objSiniestro.codser.ToString(), Text = objSiniestro.desser.ToString() });

                }

            }
            catch (Exception)
            {
                throw;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaServiciosVehiculos;

        }

        public List<SelectListItem> listatipoSiniestros()
        {
            List<SelectListItem> listaTipoSiniestro = new List<SelectListItem>();


            string findAll = "select codtipsin,destipsin from tipos_siniestros  where esttipsin = 1 union select '-1','SELECCIONAR'order by 1;";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());
                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    CargaDropDownList objSiniestro = new CargaDropDownList();
                    objSiniestro.codigo = Convert.ToString(reader[0].ToString());
                    objSiniestro.nombre = Convert.ToString(reader[1].ToString());

                    listaTipoSiniestro.Add(new SelectListItem() { Value = objSiniestro.codigo.ToString(), Text = objSiniestro.nombre.ToString() });

                }

            }
            catch (Exception)
            {
                throw;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaTipoSiniestro;

        }
        public List<SelectListItem> listaCausaProbableSiniestros()
        {
            List<SelectListItem> listaCausaProbableSiniestro = new List<SelectListItem>();
            string findAll = "select codcaupro,descaupro from causa_probable where estcaupro = 1 union select '-1','SELECCIONAR' order by 1;";
            try
            {
                objConexinDB.getCon().Open();
                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());
                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    CargaDropDownList objSiniestro = new CargaDropDownList();
                    objSiniestro.codigo = Convert.ToString(reader[0].ToString());
                    objSiniestro.nombre = Convert.ToString(reader[1].ToString());
                    listaCausaProbableSiniestro.Add(new SelectListItem() { Value = objSiniestro.codigo.ToString(), Text = objSiniestro.nombre.ToString() });

                }
            }
            catch (Exception)
            {
                throw;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaCausaProbableSiniestro;
        }

        public List<SelectListItem> listaCausaRealSiniestros()
        {
            List<SelectListItem> listaCausaRealSiniestro = new List<SelectListItem>();
            string findAll = "select codcaurea,descaurea from causa_real where estcaurea = 1  order by 1;";
            try
            {
                objConexinDB.getCon().Open();
                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());
                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    CargaDropDownList objSiniestro = new CargaDropDownList();
                    objSiniestro.codigo = Convert.ToString(reader[0].ToString());
                    objSiniestro.nombre = Convert.ToString(reader[1].ToString());
                    listaCausaRealSiniestro.Add(new SelectListItem() { Value = objSiniestro.codigo.ToString(), Text = objSiniestro.nombre.ToString() });

                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaCausaRealSiniestro;
        }

        public List<SelectListItem> listaTipoVehiculos()
        {
            List<SelectListItem> listaTipoVehiculos = new List<SelectListItem>();
            string findAll = "Select -1 as codtipveh,'SELECCIONAR' as destipveh UNION Select codtipveh,destipveh FROM tipo_vehiculos where esttipveh = 1 ORDER BY 1;";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Siniestro objSiniestro = new Siniestro();
                    objSiniestro.codtipveh = Convert.ToInt32(reader[0].ToString());
                    objSiniestro.destipveh = Convert.ToString(reader[1].ToString());
                    listaTipoVehiculos.Add(new SelectListItem() { Value = objSiniestro.codtipveh.ToString(), Text = objSiniestro.destipveh.ToString() });
                }

            }
            catch (Exception ex)
            {
                throw;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return listaTipoVehiculos;
        }

        public List<SelectListItem> listaSubTipoVehiculos(int codTipoVhl)
        {
            List<SelectListItem> listaSubTipoVehiculos = new List<SelectListItem>();
            string findAll = "select codsubveh,dessubveh from subtipo_vehiculos where codtipveh = "+ codTipoVhl + " and estsubveh = 1 ORDER BY 1";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    CargaDropDownList objSiniestro = new CargaDropDownList();
                    objSiniestro.codigo = Convert.ToString(reader[0].ToString());
                    objSiniestro.nombre = Convert.ToString(reader[1].ToString());
                    listaSubTipoVehiculos.Add(new SelectListItem() { Value = objSiniestro.codigo.ToString(), Text = objSiniestro.nombre.ToString() });
                }

            }
            catch (Exception ex)
            {
                throw;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return listaSubTipoVehiculos;
        }
        public List<SelectListItem> listaTodosSubTipoVehiculos()
        {
            List<SelectListItem> listaSubTipoVehiculos = new List<SelectListItem>();
            string findAll = "select '-1','SELECCIONAR' UNION select codsubveh,dessubveh from subtipo_vehiculos where   estsubveh = 1 ORDER BY 1";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    CargaDropDownList objSiniestro = new CargaDropDownList();
                    objSiniestro.codigo = Convert.ToString(reader[0].ToString());
                    objSiniestro.nombre = Convert.ToString(reader[1].ToString());
                    listaSubTipoVehiculos.Add(new SelectListItem() { Value = objSiniestro.codigo.ToString(), Text = objSiniestro.nombre.ToString() });
                }

            }
            catch (Exception ex)
            {
                throw;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return listaSubTipoVehiculos;
        }


        public List<SelectListItem> listaCircuitos()
        {
            List<SelectListItem> listaCircuito = new List<SelectListItem>();
            string findAll = "select codcir,descir from circuitos where estcir = 1 union select '-1','SELECCIONAR' order by 1 ;";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    CargaDropDownList objSiniestro = new CargaDropDownList();
                    objSiniestro.codigo = Convert.ToString(reader[0].ToString());
                    objSiniestro.nombre = Convert.ToString(reader[1].ToString());
                    listaCircuito.Add(new SelectListItem() { Value = objSiniestro.codigo.ToString(), Text = objSiniestro.nombre.ToString() });
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return listaCircuito;
        }
        public List<SelectListItem> listaSubCircuitos()
        {
            List<SelectListItem> listaCircuito = new List<SelectListItem>();
            string findAll = "select codcir,descir from circuitos where estcir = 1 union select '-1','SELECCIONAR' order by 1 ;";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    CargaDropDownList objSiniestro = new CargaDropDownList();
                    objSiniestro.codigo = Convert.ToString(reader[0].ToString());
                    objSiniestro.nombre = Convert.ToString(reader[1].ToString());
                    listaCircuito.Add(new SelectListItem() { Value = objSiniestro.codigo.ToString(), Text = objSiniestro.nombre.ToString() });
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return listaCircuito;
        }
        public string insertarVehiculosInvolucrados(Vehiculo v)
        {
            //codvehinv,

            v.codvehinv = Convert.ToInt32(MAXcodvehinv());

            string create = "INSERT INTO vehiculos_involucrados(codvehinv, placvehinv, danmatvehinv, matvigvehinv, chavehinv, marvehinv, modvehinv, anivehinv, cilvehinv, segprivehinv, matpelvehinv, codser, codtipve, codsin,codsubveh) values(" + v.codvehinv + ",'" + v.placvehinv + "'," + v.danmatvehinv + "," + v.matvigvehinv + ", '" + v.chavehinv + "' , '" + v.marvehinv + "', '" + v.modvehinv + "'," + v.anivehinv + "," + v.cilvehinv + "," + v.segprivehinv + ",'" + v.matpelvehinv + "', " + v.codser + "," + v.codtipve + "," + v.codsin + " ,"+v.codsubtipoVHL+" )";
            try
            {
                comando = new NpgsqlCommand(create, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                comando.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                v.codvehinv = 0;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return v.codvehinv.ToString();
        }
        public string ModificarVehiculosInvolucrados(Vehiculo v)
        {
            //codvehinv,

            //v.codvehinv = Convert.ToInt32(MAXcodvehinv());

            string create = "UPDATE public.vehiculos_involucrados  " +
                            "SET placvehinv ='" + v.placvehinv + "', danmatvehinv =" + v.danmatvehinv + ", matvigvehinv =" + v.matvigvehinv + ", chavehinv = '" + v.chavehinv + "',  " +
                            "marvehinv ='" + v.marvehinv + "', modvehinv ='" + v.modvehinv + "', anivehinv =" + v.anivehinv + ", cilvehinv =" + v.cilvehinv + ", segprivehinv =" + v.segprivehinv + ", " +
                            "matpelvehinv ='" + v.matpelvehinv + "', codser =" + v.codser + ", codtipve =" + v.codtipve + ", codsubveh = "+v.codsubtipoVHL+" " +
                            "WHERE codvehinv = " + v.codvehinv + "; ";

            try
            {
                comando = new NpgsqlCommand(create, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                comando.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                v.codvehinv = 0;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return v.codvehinv.ToString();
        }


        public string insertarDaniosTerceros(DanioMaterial v)
        {
            //codvehinv,

            v.coddater = Convert.ToInt32(MAXDaniosTerceros());

            string create = "INSERT INTO danios_terceros( coddater, obsdater, codtipdater, codsin)    VALUES (" + v.coddater + ",'" + v.obsdater + "', " + v.codtipdater + ", " + v.codsin + ");";
            try
            {
                comando = new NpgsqlCommand(create, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                comando.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                v.coddater = 0;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return v.coddater.ToString();
        }
        public string MAXcodvehinv()
        {
            string valor = "";
            string create = "select MAX(codvehinv) +1 AS codvehinv from vehiculos_involucrados;";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(create, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    valor = Convert.ToString(reader[0].ToString());
                }
                if (valor == "" || valor == null)
                    valor = "1";
            }
            catch (Exception e)
            {
                valor = "0";
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return valor;
        }
        public string MAXcodSiniestro()
        {
            string valor = "";
            string create = "select MAX(codsin)  AS codsin from siniestros;";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(create, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    valor = Convert.ToString(reader[0].ToString());
                }
                if (valor == "" || valor == null)
                    valor = "1";
            }
            catch (Exception e)
            {
                valor = "0";
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return valor;
        }
        public string MAXDaniosTerceros()
        {
            string valor = "";
            string create = "select max(coddater)+1 from danios_terceros";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(create, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    valor = Convert.ToString(reader[0].ToString());
                }
                if (valor == "" || valor == null)
                    valor = "1";

            }
            catch (Exception e)
            {
                valor = "0";
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return valor;
        }

        public List<SelectListItem> listadaTiposDaniosTerceros()
        {
            List<SelectListItem> listaDaniosTerceros = new List<SelectListItem>();
            string findAll = "select '-1','SELECCIONAR' from tipos_danios_terceros UNION select codtipdater,destipdater from tipos_danios_terceros order by 1;";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    TipoDaniosTerceros objSiniestro = new TipoDaniosTerceros();
                    objSiniestro.codtipdater = Convert.ToInt32(reader[0].ToString());
                    objSiniestro.destipdater = Convert.ToString(reader[1].ToString());
                    listaDaniosTerceros.Add(new SelectListItem() { Value = objSiniestro.codtipdater.ToString(), Text = objSiniestro.destipdater.ToString() });
                }

            }
            catch (Exception)
            {
                throw;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return listaDaniosTerceros;
        }

        public string ModificarDaniosTerceros(DanioMaterial d)
        {
            //codvehinv,

            //v.codvehinv = Convert.ToInt32(MAXcodvehinv());

            string create = "UPDATE public.danios_terceros " +
                            "SET  obsdater ='" + d.obsdater + "', codtipdater =" + d.codtipdater + " " +
                            "WHERE coddater = " + d.coddater + " ;";


            try
            {
                comando = new NpgsqlCommand(create, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                comando.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                d.coddater = 0;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return d.coddater.ToString();
        }


        public string MaxVictimasInvolucradas()
        {
            string valor = "";
            string create = "select max(CODVICINV)+1 from victimas_involucradas;";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(create, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    valor = Convert.ToString(reader[0].ToString());
                }
                if (valor == "" || valor == null)
                    valor = "1";

            }
            catch (Exception e)
            {
                valor = "0";
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return valor;
        }


        public string insertarVictimasInvolucradas(Victimas v)
        {
            //codvehinv,

            v.codvicinv = Convert.ToInt32(MaxVictimasInvolucradas());
            v.convicinv30 = "";
            string create = "INSERT INTO victimas_involucradas( codvicinv, tipidenvicinv, numidenvicinv, edavicinv, sexvicinv, convicinv24, convicinv30, tipparvicinv, casvicinv, cinvicinv,  posvicinv, conalcvicinv, codsin, codveh,nomvicinv)    VALUES (" + v.codvicinv + ",'" + v.tipidenvicinv + "','" + v.numidenvicinv + "' ," + v.edavicinv + ",'" + v.genvicinv + "', '" + v.convicinv24 + "','" + v.convicinv30 + "' ,'" + v.tipparvicinv + "'," + v.casvicinv + " ," + v.cinvicinv + ",'" + v.posvicinv + "' ," + v.conalcvicinv + "," + v.codsin + "," + v.codveh + ",'" + v.nombreVictima + "');";
            try
            {
                comando = new NpgsqlCommand(create, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                comando.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                v.codvicinv = 0;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            if (v.codvicinv != 0 && (v.tipparvicinv == "PEATÓN" || v.tipparvicinv == "CONDUCTOR"))
            {

                string descripcion = "";
                var descripacc = v.desaccpea.Split(',');
                //JsonConvert.DeserializeObject(descripacc.ToString());
                // descripacc = v.desaccpea;
                //  descripacc = descripacc.ToString().Split(',');
                if (v.desaccpea != "")
                {
                    foreach (string d in descripacc)
                    {
                        if (d.ToString() != "-1")
                        {
                            if (d.ToString() == "1") descripcion = "USO DEL CELULAR";
                            if (d.ToString() == "2") descripcion = "USO DE ELEMENTOS DISTRACTORES";
                            if (d.ToString() == "3") descripcion = "CRUCE DE VÍA A LUGARES NO AUTORIZADO";
                            if (d.ToString() == "4") descripcion = "PRESUNCIÓN DE INGESTA DE ALCOHOL";
                            if (d.ToString() == "5") descripcion = "CRUCE DE VÍA SIN PREFERENCIA";
                            if (d.ToString() == "6") descripcion = "PRESUNCIÓN DE INGESTA  DE SUSTANCIAS ESTUPERFACIENTES O PSICOTRÓPICAS Y/O MEDICAMENTOS";
                            if (d.ToString() == "7") descripcion = "NINGUNA";
                            //foreach(var c in cs.datosAccionesPeaton)
                            insertarAccionesPeaton(descripcion.ToString(), v.codvicinv);
                        }
                    }
                }
                

            }
            if (v.codvicinv != 0)
            {
                if (v.convicinv24 == "LESIONADO")
                    ModificarSiniestrosNumLesionadosFallecidos(v.codsin, v.convicinv24, 1);
                if (v.convicinv24 == "FALLECIDO")
                    ModificarSiniestrosNumLesionadosFallecidos(v.codsin, v.convicinv24, 2);
            }

            return v.codvicinv.ToString();
        }
        public string ModificaVictimasInvolucradas(Victimas v)
        {
            //codvehinv,

            // v.codvicinv = Convert.ToInt32(MaxVictimasInvolucradas());

            string create = "UPDATE public.victimas_involucradas " +
                        " SET  tipidenvicinv ='" + v.tipidenvicinv + "', numidenvicinv ='" + v.numidenvicinv + "', edavicinv =" + v.edavicinv + ", sexvicinv ='" + v.genvicinv + "', " +
                        " convicinv24 ='" + v.convicinv24 + "', convicinv30 ='" + v.convicinv30 + "', tipparvicinv ='" + v.tipparvicinv + "', casvicinv =" + v.casvicinv + ", cinvicinv =" + v.cinvicinv + ",  " +
                        " posvicinv ='" + v.posvicinv + "', conalcvicinv =" + v.conalcvicinv + ", codveh =" + v.codveh + ", nomvicinv = '"+v.nombreVictima+"' " +
                        " WHERE  codvicinv = " + v.codvicinv + "";
            try
            {
                comando = new NpgsqlCommand(create, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                comando.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                v.codvicinv = 0;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            /*******************
             *  VERIFICO SI LA VICTIMA SE CAMBIO DE PEATÓN A OTRO TIPO E INACTIVAR ESOS REGISTROS DE ACCIONES DE PEATON
             * *******************/
            if (v.codvicinv != 0 && v.tipparvicinv != "PEATÓN" && v.tipparvicinv != "CONDUCTOR") {
                var lista = ListaAccionesPeatonPorCodigoVictima(v.codvicinv);
                if (lista.Count > 0)
                {
                    inactivaAccionesPeatonPorCodVictima(v.codvicinv);
                }
            }
            if (v.codvicinv != 0 && (v.tipparvicinv == "PEATÓN" || v.tipparvicinv == "CONDUCTOR") && v.desaccpea.Length > 0)
            {

                string descripcion = "";
                var descripacc = v.desaccpea.Split(',');
                inactivaAccionesPeatonPorCodVictima(v.codvicinv);
                var lista = ListaAccionesPeatonPorCodigoVictima(v.codvicinv);

                if (lista.Count == 0)
                {
                    foreach (string d in descripacc)
                    {
                        if (d.ToString() != "-1")
                        {
                            if (d.ToString() == "1") descripcion = "USO DEL CELULAR";
                            if (d.ToString() == "2") descripcion = "USO DE ELEMENTOS DISTRACTORES";
                            if (d.ToString() == "3") descripcion = "CRUCE DE VÍA A LUGARES NO AUTORIZADO";
                            if (d.ToString() == "4") descripcion = "PRESUNCIÓN DE INGESTA DE ALCOHOL";
                            if (d.ToString() == "5") descripcion = "CRUCE DE VÍA SIN PREFERENCIA";
                            if (d.ToString() == "6") descripcion = "PRESUNCIÓN DE INGESTA  DE SUSTANCIAS ESTUPERFACIENTES O PSICOTRÓPICAS Y/O MEDICAMENTOS";
                            insertarAccionesPeaton(descripcion.ToString(), v.codvicinv);
                        }
                    }
                }


            }
            if (v.codvicinv != 0)
            {
               // if (v.convicinv24 == "LESIONADO")
                    ModificarSiniestrosNumLesionadosFallecidos(v.codsin, v.convicinv24, 1);
               // else
                    ModificarSiniestrosNumLesionadosFallecidos(v.codsin, v.convicinv24, 2);
            }

            return v.codvicinv.ToString();
        }
        public void inactivaAccionesPeatonPorCodVictima(int codVictima)
        {
            string findAll = "update ACCIONES_PEATON set estaccpea = 0 where CODVICINV = " + codVictima + " ";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();


            }
            catch (Exception)
            {
                
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
        }
        public List<AccionesPeaton> ListaAccionesPeatonPorCodigoVictima(int codVict)
        {
            List<AccionesPeaton> listaAccionesPeaton = new List<AccionesPeaton>();
            string findAll = "   SELECT* FROM ACCIONES_PEATON WHERE  CODVICINV  =" + codVict + "  and estaccpea = 1 order by 1;";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    AccionesPeaton objSiniestro = new AccionesPeaton();
                    objSiniestro.codaccpea = Convert.ToInt32(reader[0].ToString());
                    objSiniestro.desaccpea = Convert.ToString(reader[1].ToString());
                    listaAccionesPeaton.Add(objSiniestro);
                }

            }
            catch (Exception)
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return listaAccionesPeaton;
        }

        public string insertarAccionesPeaton(string desaccpea, int codvicinv)
        {
            //codvehinv,

            int codaccpea = Convert.ToInt32(MaxAccionesPeaton());

            string create = "INSERT INTO acciones_peaton(codaccpea, desaccpea, codvicinv)VALUES (" + codaccpea + ",'" + desaccpea + "', " + codvicinv + ");";
            try
            {
                comando = new NpgsqlCommand(create, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                comando.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
                codaccpea = 0;
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }


            return codaccpea.ToString();
        }

        public string ModificarAccionesPeaton(int codaccpea, string desaccpea, int codvicinv)
        {
            //codvehinv,

            // int codaccpea = Convert.ToInt32(MaxAccionesPeaton());

            string create = "UPDATE public.acciones_peaton " +
                            "SET  desaccpea ='" + desaccpea + "', codvicinv =" + codvicinv + ", estaccpea =1 " +
                            " WHERE codaccpea = " + codaccpea + " ";
            try
            {
                comando = new NpgsqlCommand(create, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                comando.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                codaccpea = 0;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }


            return codaccpea.ToString();
        }

        //datosPeatones
        public List<SelectListItem> listadaDatosPeatones(int codsin)
        {
            List<SelectListItem> listaPeatones = new List<SelectListItem>();
            string findAll = "SELECT a.codvicinv ,a.numidenvicinv  || '(' || a.convicinv24 || ')' as Victima FROM victimas_involucradas a where  a.tipparvicinv = 'PEATÓN' AND a.codsin = " + codsin + " order by 2;";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    CargaDropDownList objSiniestro = new CargaDropDownList();
                    objSiniestro.codigo = Convert.ToString(reader[0].ToString());
                    objSiniestro.nombre = Convert.ToString(reader[1].ToString());
                    listaPeatones.Add(new SelectListItem() { Value = objSiniestro.codigo.ToString(), Text = objSiniestro.nombre.ToString() });
                }

            }
            catch (Exception)
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return listaPeatones;
        }
        public List<SelectListItem> listadaVehiculosInvolucrados(int codsin)
        {
            List<SelectListItem> listaVhl = new List<SelectListItem>();
            string findAll = "select v.codvehinv, case when v.codtipve in (18,16) then v.placvehinv || '(' || t.destipveh || ')' else v.placvehinv || '(' ||  t.destipveh  || ')' end as vehiculo  from vehiculos_involucrados v inner join tipo_vehiculos t on v.codtipve = t.codtipveh  where v.codsin =  " + codsin + " and v.estvehinv = 1 order by 1;";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    CargaDropDownList objSiniestro = new CargaDropDownList();
                    objSiniestro.codigo = Convert.ToString(reader[0].ToString());
                    objSiniestro.nombre = Convert.ToString(reader[1].ToString());
                    listaVhl.Add(new SelectListItem() { Value = objSiniestro.codigo.ToString(), Text = objSiniestro.nombre.ToString() });
                }

            }
            catch (Exception)
            {
                
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return listaVhl;
        }

        public string MaxAccionesPeaton()
        {
            string valor = "";
            string create = "select max(codaccpea) + 1 from acciones_peaton;";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(create, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    valor = Convert.ToString(reader[0].ToString());
                }
                if (valor == "" || valor == null)
                    valor = "1";

            }
            catch (Exception e)
            {
                valor = "0";
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return valor;
        }


        // modifica REGISTRO VALIDADO PARA LECTURA ESTADÍSTICA true=OK, false=PENDIENTE
        public string modificaRegistroValidadoParaEstadistica(int codsin, int supervisorIn)
        {
            string valor = "";
            string create = "update siniestros set regvalsin = true, \"supressIn\" = " + supervisorIn + " where codsin = " + codsin + ";";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(create, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    valor = Convert.ToString(reader[0].ToString());
                }
                if (valor == "" || valor == null)
                    valor = "1";

            }
            catch (Exception e)
            {
                valor = "0";
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return valor;
        }
        //
        public List<Georeferencias> listaGeoreferencias(string codaut)
        {
            List<Georeferencias> listaGeoreferencia = new List<Georeferencias>();

            //PDAC tabla Departamentos
            string findAll = "select  g.codgeo, to_char(g.fecgeo,'YYYY/MM/DD') as fecha,to_char(g.fecgeo,'HH24:MI:SS') as hora,g.latgeo,g.longeo,upper(u.\"PU003\") || ' ' || upper( u.\"PU002\") as \"usuario\" ,a.codaut ||' '|| upper (a.desaut) as \"autoridad\", g.obsgeo as \"observaciones\", g.fotprigeo, g.fotsegeo from GEOREFERENCIAS g inner join \"PUAC\" u on g.\"PUCOD\" = u.\"PUCOD\" inner join autoridades a on a.codaut = g.codaut WHERE g.valgeo = 0 and  a.codaut = '" + codaut + "'; ";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Georeferencias objGeo = new Georeferencias();
                    objGeo.codgeo = Convert.ToInt32(reader[0].ToString());
                    objGeo.fecha = Convert.ToString(Convert.ToDateTime(reader[1].ToString()).ToString("yyyy-MM-dd")).Replace('/', '-'); //Convert.ToString(reader[1].ToString());
                    objGeo.hora = Convert.ToString(reader[2].ToString());
                    objGeo.latgeo = Convert.ToString(reader[3].ToString()).Replace(',', '.');
                    objGeo.longeo = Convert.ToString(reader[4].ToString()).Replace(',', '.');
                    objGeo.usuario = Convert.ToString(reader[5].ToString());
                    objGeo.autoridad = Convert.ToString(reader[6].ToString());
                    objGeo.observaciones = Convert.ToString(reader[7].ToString());
                    objGeo.fotprigeo = Convert.ToString(reader[8].ToString());
                    objGeo.fotsegeo = Convert.ToString(reader[9].ToString());
                    

                    listaGeoreferencia.Add(objGeo);

                }
            }
            catch (Exception)
            {
                throw;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaGeoreferencia;

        }

        public List<Georeferencias> listaGeoreferenciasMovil(int  codusuario)
        {
            List<Georeferencias> listaGeoreferencia = new List<Georeferencias>();

            //PDAC tabla Departamentos
            string findAll = "select  g.codgeo, to_char(g.fecgeo,'YYYY/MM/DD') as fecha,to_char(g.fecgeo,'HH24:MI:SS') as hora,g.latgeo,g.longeo,upper(u.\"PU003\") || ' ' || upper( u.\"PU002\") as \"usuario\" ,a.codaut ||' '|| upper (a.desaut) as \"autoridad\", g.obsgeo as \"observaciones\", g.fotprigeo, g.fotsegeo from GEOREFERENCIAS g inner join \"PUAC\" u on g.\"PUCOD\" = u.\"PUCOD\" inner join autoridades a on a.codaut = g.codaut WHERE g.valgeo = 0 and u.\"PUCOD\" = " + codusuario + "; ";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Georeferencias objGeo = new Georeferencias();
                    objGeo.codgeo = Convert.ToInt32(reader[0].ToString());
                    objGeo.fecha = Convert.ToString(Convert.ToDateTime(reader[1].ToString()).ToString("yyyy-MM-dd")).Replace('/', '-'); //Convert.ToString(reader[1].ToString());
                    objGeo.hora = Convert.ToString(reader[2].ToString());
                    objGeo.latgeo = Convert.ToString(reader[3].ToString()).Replace(',', '.');
                    objGeo.longeo = Convert.ToString(reader[4].ToString()).Replace(',', '.');
                    objGeo.usuario = Convert.ToString(reader[5].ToString());
                    objGeo.autoridad = Convert.ToString(reader[6].ToString());
                    objGeo.observaciones = Convert.ToString(reader[7].ToString());
                    objGeo.fotprigeo = Convert.ToString(reader[8].ToString());
                    objGeo.fotsegeo = Convert.ToString(reader[9].ToString());


                    listaGeoreferencia.Add(objGeo);

                }
            }
            catch (Exception)
            {
                throw;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaGeoreferencia;

        }
        public string ModificarGeoreferenciaSiniestro(int codsin, int codgeo)
        {
            string create = "update georeferencias set valgeo = 1 , codsin = " + codsin + " where codgeo = " + codgeo + " ";
            try
            {
                comando = new NpgsqlCommand(create, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                comando.ExecuteNonQuery();
                codgeo = 1;
            }
            catch (Exception e)
            {
                codgeo = 0;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }


            return codgeo.ToString();
        }

        public List<Vehiculo> ObtenerPlacaVhl(string parametro, int opcion)
        {
            List<Vehiculo> listaVhl = new List<Vehiculo>();
            string findAll = "";
            if (opcion == 1) // placa
                findAll = "select m.placa,m.vin,m.marca,m.modelo,m.anio_fabricacion,m.cilindraje,m.clase_vehiculo,m.matricula_estado from matriculas m where m.placa = '" + parametro.ToUpper() + "' order by 1;";
            if (opcion == 2) // chasis
                findAll = "select m.placa,m.vin,m.marca,m.modelo,m.anio_fabricacion,m.cilindraje,m.clase_vehiculo,m.matricula_estado from matriculas m where m.vin like '%" + parametro.ToUpper() + "%' order by 1;";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Vehiculo objVHL = new Vehiculo();
                    objVHL.placvehinv = Convert.ToString(reader[0].ToString());
                    objVHL.chavehinv = Convert.ToString(reader[1].ToString());
                    objVHL.marvehinv = Convert.ToString(reader[2].ToString());
                    objVHL.modvehinv = Convert.ToString(reader[3].ToString());
                    objVHL.anivehinv = Convert.ToInt32(reader[4].ToString().Trim());
                    objVHL.cilvehinv = Convert.ToString(reader[5].ToString().Trim());
                    objVHL.tipoVehiculo = Convert.ToString(reader[6].ToString().Trim());
                    objVHL.matriculaVigente = Convert.ToString(reader[7].ToString().Trim());
                    listaVhl.Add(objVHL);
                }

            }
            catch (Exception)
            {
                throw;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return listaVhl;
        }

        public string verificaPermiso(int codusuario, int opcion)
        {
            string valor = "0";
            string cons = "";
            if (opcion == 1)
            {// administradores
                cons = "select  '1' as valor from \"PUAC\" u  inner join autoridades a on u.codaut = a.codaut inner join \"PFAC\" f on u.\"PFCOD\" = f.\"PFCOD\" where a.estaut = 1 and u.\"PUCOD\" = " + codusuario + " and f.\"PFCOD\" in (1) and u.codaut = 'ANT' ;";
            }
            if (opcion == 2)// agentes crear siniestro
            {
                cons = "select  '2' as valor  from \"PUAC\" u  inner join autoridades a on u.codaut = a.codaut inner join \"PFAC\" f on u.\"PFCOD\" = f.\"PFCOD\" where a.estaut = 1 and u.\"PUCOD\" = " + codusuario + " and f.\"PFCOD\" in (2) ;";
            }
            if (opcion == 3)//  VALIDADOR
            {
                cons = "select  '3' as valor  from \"PUAC\" u  inner join autoridades a on u.codaut = a.codaut inner join \"PFAC\" f on u.\"PFCOD\" = f.\"PFCOD\" where a.estaut = 1 and u.\"PUCOD\" = " + codusuario + " and f.\"PFCOD\" in (3) ;";
            }
            if (opcion == 4)// GESTOR VALIDADOR
            {
                cons = "select  '4' as valor  from \"PUAC\" u  inner join autoridades a on u.codaut = a.codaut inner join \"PFAC\" f on u.\"PFCOD\" = f.\"PFCOD\" where a.estaut = 1 and u.\"PUCOD\" = " + codusuario + " and f.\"PFCOD\" in (4) ;";
            }
            if (opcion == 6)// supervisor
            {
                cons = "select  '6' as valor  from \"PUAC\" u  inner join autoridades a on u.codaut = a.codaut inner join \"PFAC\" f on u.\"PFCOD\" = f.\"PFCOD\" where a.estaut = 1 and u.\"PUCOD\" = " + codusuario + " and f.\"PFCOD\" in (6) ;";
            }
            if (opcion == 7)// carga calificacion
            {
                cons = "select  '7' as valor  from \"PUAC\" u  inner join autoridades a on u.codaut = a.codaut inner join \"PFAC\" f on u.\"PFCOD\" = f.\"PFCOD\" where a.estaut = 1 and u.\"PUCOD\" = " + codusuario + " and f.\"PFCOD\" in (7) ;";
            }
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(cons, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    valor = Convert.ToString(reader[0].ToString());
                }
                if (valor == "" || valor == null)
                    valor = "0";

            }
            catch (Exception e)
            {
                valor = "0";
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return valor;
        }

        public string obtenerTipoVehiculo(int codVehiculo)
        {
            string valor = "0";
            string cons = "";
            cons = "select t.codtipveh from tipo_vehiculos t inner join vehiculos_involucrados v on  v.codtipve = t.codtipveh   where  v.codvehinv = " + codVehiculo + " ;";

            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(cons, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    valor = Convert.ToString(reader[0].ToString());
                }
                if (valor == "" || valor == null)
                    valor = "0";

            }
            catch (Exception e)
            {
                valor = "0";
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return valor;
        }

        public List<Victimas> ObtenerInformacionVictima(string cedula, int tipoI)
        {
            List<Victimas> listaVictima = new List<Victimas>();
            string findAll = "";
            if (tipoI == 1)// cedulas y pasaporte
                findAll = "SELECT tipo_id,identificacion,nombre_completo,to_char(fecha_nacimiento,'YYYY-MM-DD') as fecha_nacimiento,sexo,estado_civil FROM licencias where identificacion like '" + cedula.Trim() + "' order by 1;";
            if (tipoI == 2)// licencias
                findAll = "SELECT tipo_id,identificacion,nombre_completo,to_char(fecha_nacimiento,'YYYY-MM-DD') as fecha_nacimiento,sexo,estado_civil FROM licencias where identificacion like '" + cedula.Trim() + "' order by 1;";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Victimas objVictima = new Victimas();
                    objVictima.tipidenvicinv = Convert.ToString(reader[0].ToString());
                    objVictima.numidenvicinv = Convert.ToString(reader[1].ToString());
                    objVictima.nombre_completo = Convert.ToString(reader[2].ToString());
                    objVictima.fecha_nacimiento = Convert.ToString(reader[3].ToString());
                    objVictima.sexo = Convert.ToString(reader[4].ToString().Trim());
                    objVictima.estado_civil = Convert.ToString(reader[5].ToString().Trim());
                    objVictima.edavicinv = Convert.ToInt32(CalcularEdad(objVictima.fecha_nacimiento)) == 117 ? 0 : Convert.ToInt32(CalcularEdad(objVictima.fecha_nacimiento));
                    listaVictima.Add(objVictima);
                }

            }
            catch (Exception)
            {
                throw;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return listaVictima;
        }

        public string CalcularEdad(string fecha)
        {
            string edad = "0";
            try
            {
                DateTime fechaDeNacimiento;
                if (fecha.Length == 10)
                    fechaDeNacimiento = Convert.ToDateTime(fecha);
                else
                    fechaDeNacimiento = Convert.ToDateTime("01/01/1900");
                int edadEnDias = ((TimeSpan)(DateTime.Now - fechaDeNacimiento)).Days / 365;
                if (fechaDeNacimiento.Month == DateTime.Now.Month && DateTime.Now.Day < fechaDeNacimiento.Day)
                    edadEnDias = edadEnDias - 1;
                edad = edadEnDias.ToString();

            }
            catch (Exception)
            {
                edad = "0";
            }
            return edad;
        }

        public static decimal ConvertirEnDecimal(string valor)
        {
            decimal retorno = 0;

            if (!string.IsNullOrEmpty(valor.Trim()))
            {
                NumberFormatInfo nfi = new CultureInfo("en-US").NumberFormat;
                nfi.NumberDecimalSeparator = ",";

                try
                {
                    retorno = Convert.ToDecimal(valor, nfi);
                }
                catch
                {
                }
            }

            return retorno;
        }
        public string guardarCalififcaciones(string fecha, string cumcal, string puncal, string calcal, string comcal, string totcal, int codprov)
        {
            string retorno = "0";

            string create = "INSERT INTO public.calificacion(    feccal, cumcal, puncal, calcal, comcal, totcal, codprov)" +
                    "VALUES('"+Convert.ToDateTime(fecha.ToString().Replace('/','-'))+"', "+ cumcal.Replace(',', '.') + ", "+puncal.Replace(',', '.') + ", "+calcal.Replace(',', '.') + ", "+comcal.Replace(',', '.') + ", "+totcal.Replace(',', '.') + ", "+codprov+"); ";
            try
            {
                comando = new NpgsqlCommand(create, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                comando.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                retorno = "0";
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }


            return retorno;
        }
        public string eliminarRegistrosCalificacionPorFechaMes(string anio, string mes)
        {
            string retorno = "0";

            string create = "delete  from calificacion where extract(year from feccal) = '" + anio + "' and extract(month from feccal) = '" + mes + "' ";
                    
            try
            {
                comando = new NpgsqlCommand(create, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                comando.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                retorno = "0";
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }


            return retorno;
        }
        public string CargaCalificaciones(List<Calificaciones> c)
        {
            string valores = "0";
            int codprov = 0;
            int contador = 0;
            try
            {
                
                foreach (var d in c)
                {
                    var provincias = listaProvincias();
                    foreach (var p in provincias)
                    {
                        codprov = 0;
                        if (p.nomprov == d.nombreProvincia)
                            codprov = p.codprov;
                        if (codprov != 0)
                        {
                            guardarCalififcaciones(d.feccal, d.cumcal, d.puncal, d.calcal, d.comcal, d.totcal, codprov);
                            contador++;
                        }
                           
                    }


                }
                valores = contador.ToString();


            }
            catch (Exception ex)
            {
                ex.ToString();
                valores = "0";
            }
           
            return valores;
        }

        public List<Calificaciones> verificarCalificacionesPorFecha(string fecha, string mes)
        {
            List<Calificaciones> listaCal = new List<Calificaciones>();

            string findAll = "select p.nomprov,a.codcal,to_char(a.feccal,'YYYY/MM/DD') as feccal,a.cumcal,a.puncal,a.calcal,a.comcal,a.totcal,a.codprov  from calificacion a inner join provincias p on a.codprov = p.codprov where TO_CHAR(feccal,'MM') = '" + mes.ToString()+"' AND  TO_CHAR(feccal,'YYYY') = '"+fecha.ToString()+"'";
            try
            {

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());
                objConexinDB.getCon().Open();

                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Calificaciones objSiniestro = new Calificaciones();
                    objSiniestro.nombreProvincia = Convert.ToString(reader[0].ToString());
                    objSiniestro.codcal = Convert.ToInt32(reader[1].ToString());
                    objSiniestro.feccal = Convert.ToString(reader[2].ToString()).Replace('/','-');
                    objSiniestro.cumcal = Convert.ToString(Convert.ToDecimal( reader[3].ToString()).ToString("N2")).Replace(',','.');
                    objSiniestro.puncal = Convert.ToString(Convert.ToDecimal(reader[4].ToString()).ToString("N2")).Replace(',', '.');
                    objSiniestro.calcal = Convert.ToString(Convert.ToDecimal(reader[5].ToString()).ToString("N2")).Replace(',', '.');
                    objSiniestro.comcal = Convert.ToString(Convert.ToDecimal(reader[6].ToString()).ToString("N2")).Replace(',', '.');
                    objSiniestro.totcal = Convert.ToString(Convert.ToDecimal(reader[7].ToString()).ToString("N2")).Replace(',', '.');
                    listaCal.Add(objSiniestro);
                }
            }
            catch (Exception ex)
            {
                throw;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaCal;

        }

        public List<Calificaciones> verificarCalificacionesPorRangoFecha(string fecini,string fecfin, int codprov)
        {
            List<Calificaciones> listaCal = new List<Calificaciones>();
            string findAll = "";
            if (codprov == -1)
            {
                findAll = "select p.nomprov,a.codcal,to_char(a.feccal,'YYYY/MM/DD') as feccal,a.cumcal,a.puncal,a.calcal,a.comcal,a.totcal,a.codprov   " +
                        "from calificacion a inner " +
                        "join provincias p on a.codprov = p.codprov " +
                        "where to_char(a.feccal, 'YYYY/MM/DD') >= '" + fecini + "' AND to_char(a.feccal, 'YYYY/MM/DD') <= '" + fecfin + "' order by 1 ";
            }
            if (codprov != -1)
            {
                findAll = "select p.nomprov,a.codcal,to_char(a.feccal,'YYYY/MM/DD') as feccal,a.cumcal,a.puncal,a.calcal,a.comcal,a.totcal,a.codprov   " +
                        "from calificacion a inner " +
                        "join provincias p on a.codprov = p.codprov " +
                        "where to_char(a.feccal, 'YYYY/MM/DD') >= '" + fecini + "' AND to_char(a.feccal, 'YYYY/MM/DD') <= '" + fecfin + "'  and a.codprov = "+codprov+"  order by 1 ";
            }
            try
            {

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());
                objConexinDB.getCon().Open();

                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Calificaciones objSiniestro = new Calificaciones();
                    objSiniestro.nombreProvincia = Convert.ToString(reader[0].ToString());
                    objSiniestro.codcal = Convert.ToInt32(reader[1].ToString());
                    objSiniestro.feccal = Convert.ToString(reader[2].ToString()).Replace('/', '-');
                    objSiniestro.cumcal = Convert.ToString(Convert.ToDecimal(reader[3].ToString()).ToString("N2")).Replace(',', '.');
                    objSiniestro.puncal = Convert.ToString(Convert.ToDecimal(reader[4].ToString()).ToString("N2")).Replace(',', '.');
                    objSiniestro.calcal = Convert.ToString(Convert.ToDecimal(reader[5].ToString()).ToString("N2")).Replace(',', '.');
                    objSiniestro.comcal = Convert.ToString(Convert.ToDecimal(reader[6].ToString()).ToString("N2")).Replace(',', '.');
                    objSiniestro.totcal = Convert.ToString(Convert.ToDecimal(reader[7].ToString()).ToString("N2")).Replace(',', '.');
                    listaCal.Add(objSiniestro);
                }
            }
            catch (Exception ex)
            {
                throw;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaCal;

        }

        public string  ModificarAutoridad(string codaut)
        {
            string create = "";
            create = "UPDATE AUTORIDADES SET ESTAUT = 0 WHERE CODAUT = '"+ codaut + "'";
            try
            {
                comando = new NpgsqlCommand(create, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                comando.ExecuteNonQuery();
                
            }
            catch (Exception e)
            {
                e.ToString();
                codaut = "";
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }


            return codaut;
        }

        public List<SelectListItem> listadAutoridades(string codauto, string cargamasiva)
        {
            List<SelectListItem> listadAutoridad = new List<SelectListItem>();
            string findAll = "";
            if (codauto == "ANT")
            {
                findAll = "SELECT CODAUT,DESAUT FROM autoridades Where estaut = 1 UNION select '-1' ,'TODOS' ORDER BY 1";
            }
            else if (cargamasiva == "CM")
            {
                findAll = "SELECT CODAUT,DESAUT FROM autoridades Where estaut = 1 AND CODAUT = '"+ codauto + "' ORDER BY 1";
            }
            else if (codauto != "ANT" && cargamasiva != "CM")
            {
                findAll = "SELECT CODAUT,DESAUT FROM autoridades Where estaut = 1 AND CODAUT = '" + codauto + "' ORDER BY 1";
            }
            
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    CargaDropDownList objSiniestro = new CargaDropDownList();
                    objSiniestro.codigo = Convert.ToString(reader[0].ToString());
                    objSiniestro.nombre = Convert.ToString(reader[1].ToString());
                    listadAutoridad.Add(new SelectListItem() { Value = objSiniestro.codigo.ToString(), Text = objSiniestro.nombre.ToString() });
                }

            }
            catch (Exception)
            {
                throw;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return listadAutoridad;
        }

        public List<SelectListItem> ListaCasosCargaMasiva(string codigo, string descripcion)
        {
            List<SelectListItem> listaCm = new List<SelectListItem>();
          
            try
            {
              
                    CargaDropDownList objSiniestro = new CargaDropDownList();
                    objSiniestro.codigo = Convert.ToString(codigo);
                    objSiniestro.nombre = Convert.ToString(descripcion);
                listaCm.Add(new SelectListItem() { Value = objSiniestro.codigo.ToString(), Text = objSiniestro.nombre.ToString() });
                

            }
            catch (Exception)
            {
                throw;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
           
            return listaCm;
        }

        public List<Siniestro> listaSiniestrosCm(string anio, string mes,string codAutoridad)
        {
            List<Siniestro> listaSiniestro = new List<Siniestro>();
            //PDAC tabla Departamentos
            string findAll = "Select * from siniestros s where "+
                            "to_char(s.fecsin, 'YYYY') = '"+ anio + "' and to_char(s.fecsin, 'mm') = '"+ mes + "' and s.codaut = '"+ codAutoridad + "' ;";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Siniestro objSiniestro = new Siniestro();
                    objSiniestro.codsin = Convert.ToInt32(reader[0].ToString());
                    objSiniestro.fecsin = Convert.ToString(reader[1].ToString());
                 

                    listaSiniestro.Add(objSiniestro);

                }
            }
            catch (Exception)
            {
                throw;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaSiniestro;

        }

        /****
        

         * */

        public string InsertarCargaMasiva(string codaut,int totalRegistros, int codusuario,string observaciones)
        {
            string create = "";
            DateTime fecha = DateTime.Now;
            string codcar = "0";
            create = " INSERT INTO public.cargas_masivas( feccarmas, totregcarmas, obscarmas, codaut, \"PUCOD\")    VALUES('"+ fecha + "',"+totalRegistros+", '"+ observaciones + "', '"+ codaut + "', "+ codusuario + "); ";
            try
            {
                comando = new NpgsqlCommand(create, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                comando.ExecuteNonQuery();
                codcar = "1";
            }
            catch (Exception e)
            {
                e.ToString();
                codcar = "0";
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }


            return codcar;
        }

        public List<Siniestro> listaVistaSiniestrosCm(string codsin)
        {
            List<Siniestro> listaSiniestro = new List<Siniestro>();
            codsin = codsin.TrimEnd(',');
            string findAll = "select to_char(s.fecsin,'YYYY/MM/DD')  as fecsin,s.horsin,s.latsin,s.lonsin,UPPER(s.dirsin) AS dirsin,s.numfalsin,s.numlessin,UPPER(ag.\"PU002\")  || ' ' || UPPER(ag.\"PU003\")   as \"agente_responsable\",  " +
                             " UPPER(sup.\"PU002\") || ' ' || UPPER(sup.\"PU003\") as \"supervisor_responsable\", s.zonsin,s.traviasin,s.conatmsin,s.conviasin,s.luzartsin,s.desviasin,s.limvelsin,s.intsin, " +
                             " s.matsupviasin,s.obsviasin,s.lugviasin,s.cursin,s.numcarsin,s.sensin,UPPER(us.\"PU002\") || ' ' || UPPER(us.\"PU003\") as \"USUARIO_REGISTRO\", " +
                             " upper(au.desaut) as \"autoridad\", ts.destipsin as \"tiposiniestro\", p.despar as \"parroquia\", c.descir as \"subcircuito\", cant.descant as \"canton\", " +
                             " prov.nomprov as \"provincia\", cp.descaupro as \"causa_probable\", cr.descaurea as \"causa_real\", c1.descir as \"circuito\", dis.desdis as \"distrito\" ,s.codsin " +
                             " from siniestros s " +
                             " inner join \"PUAC\" ag on s.ageressin = ag.\"PUCOD\" " +
                             " inner join \"PUAC\" sup on s.\"supressIn\" = sup.\"PUCOD\" " +
                             " inner join \"PUAC\" us on s.\"PUCOD\" = us.\"PUCOD\" " +
                             " inner join autoridades au on au.codaut = s.codaut " +
                             " inner join tipos_siniestros ts on ts.codtipsin = s.codtipsin " +
                             " inner join parroquias p on p.codpar = s.codpar " +
                             " inner join circuitos c on c.codcir = s.codcir " +
                             " inner join cantones cant on cant.codcant = s.codcant " +
                             " inner join provincias prov on prov.codprov = s.codprov " +
                             " inner join causa_probable cp on cp.codcaupro = s.codcaupro " +
                             " inner join causa_real cr on cr.codcaurea = s.codcaurea " +
                             " inner join circuitos c1 on c1.codcir = s.codcir " +
                             " inner join distritos dis on  dis.coddis = s.coddis " +
                             " where codsin in (" + codsin + ")";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Siniestro objSiniestro = new Siniestro();
                    objSiniestro.fecsin = Convert.ToString(reader[0].ToString()).Replace('/', '-');
                    objSiniestro.horsin = Convert.ToString(reader[1].ToString()).Substring(0, 5);
                    objSiniestro.latsin = Convert.ToString(reader[2].ToString()).Replace(',', '.');
                    objSiniestro.lonsin = Convert.ToString(reader[3].ToString()).Replace(',', '.');
                    objSiniestro.dirsin = Convert.ToString(reader[4].ToString());

                    objSiniestro.numfalsin = Convert.ToInt32(reader[5].ToString());
                    objSiniestro.numlessin = Convert.ToInt32(reader[6].ToString());
                    objSiniestro.agente_responsable = Convert.ToString(reader[7].ToString());
                    objSiniestro.supervisor_responsable = Convert.ToString(reader[8].ToString());
                    objSiniestro.zonsin = Convert.ToString(reader[9].ToString());
                    objSiniestro.traviasin = Convert.ToBoolean(reader[10].ToString());
                    objSiniestro.conatmsin = Convert.ToString(reader[11].ToString());
                    objSiniestro.conviasin = Convert.ToString(reader[12].ToString());
                    objSiniestro.luzartsin = Convert.ToString(reader[13].ToString());
                    objSiniestro.desviasin = Convert.ToString(reader[14].ToString());
                    objSiniestro.limvelsin = Convert.ToInt32(reader[15].ToString());
                    objSiniestro.intsin = Convert.ToString(reader[16].ToString());
                    objSiniestro.matsupviasin = Convert.ToString(reader[17].ToString());
                    objSiniestro.obsviasin = Convert.ToString(reader[18].ToString());
                    objSiniestro.lugviasin = Convert.ToString(reader[19].ToString());
                    objSiniestro.cursin = Convert.ToString(reader[20].ToString());
                    objSiniestro.numcarsin = Convert.ToInt32(reader[21].ToString());
                    objSiniestro.sensin = Convert.ToString(reader[22].ToString());
                    objSiniestro.USUARIO_REGISTRO = Convert.ToString(reader[23].ToString());
                    objSiniestro.autoridad = Convert.ToString(reader[24].ToString());
                    objSiniestro.tiposiniestro = Convert.ToString(reader[25].ToString());
                    objSiniestro.parroquia = Convert.ToString(reader[26].ToString());
                    objSiniestro.subcircuito = Convert.ToString(reader[27].ToString());
                    objSiniestro.canton = Convert.ToString(reader[28].ToString());
                    objSiniestro.provincia = Convert.ToString(reader[29].ToString());
                    objSiniestro.causa_probable = Convert.ToString(reader[30].ToString());
                    objSiniestro.causa_real = Convert.ToString(reader[31].ToString());
                    objSiniestro.circuito = Convert.ToString(reader[32].ToString());
                    objSiniestro.distrito = Convert.ToString(reader[33].ToString());
                    objSiniestro.codsin = Convert.ToInt32(reader[34].ToString());

                    listaSiniestro.Add(objSiniestro);

                }
            }
            catch (Exception ex)
            {
                throw ex;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaSiniestro;

        }


        public string  cambiarContraseña(Usuarios item,int codusuario)
        {
            
            string _codusuario = "0";
            MD5 md5Hash = MD5.Create();
            if (item.PU001 != "" && item.PU001 != null)
            {
                string Password = enc.GetMd5Hash(md5Hash, item.PU001);
                item.PU001 = Password;
                item.PU005 = 1;
                item.PU004 = "ND";
                string create = "";
                create = "update public.\"PUAC\" set \"PU001\" = '" + item.PU001 + "' WHERE \"PUCOD\" =  " + codusuario + " ";
                try
                {
                    comando = new NpgsqlCommand(create, objConexinDB.getCon());
                    objConexinDB.getCon().Open();
                    comando.ExecuteNonQuery();
                    _codusuario = codusuario.ToString();
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    _codusuario = "0";
                }
                finally
                {
                    objConexinDB.getCon().Close();
                    objConexinDB.closeDB();
                }
            }
           

            return _codusuario;
            
        }

        public List<Siniestro> tarerInformacionSiniestroProcerso(int codisn)
        {
            List<Siniestro> listaSiniestro = new List<Siniestro>();
            //PDAC tabla Departamentos
            string findAll = "Select s.codsin, to_char(s.fecsin,'YYYY/MM/DD') as fecsin ,s.numfalsin,s.zonsin,upper(s.dirsin) as dirsin, upper(u.\"PU002\" ) || ' ' || upper ( u.\"PU003\") as \"agente_responsable\", upper(u1.\"PU002\" ) || ' ' || upper ( u1.\"PU003\") as \"usuario_registro\", upper(u2.\"PU002\" ) || ' ' || upper ( u2.\"PU003\") as \"SUPERVISOR\" " +
                             ",a.desaut as \"autoridad\", " +
                             " case when s.regvalsin = 't' then 'OK' ELSE 'PENDIENTE' END AS \"REGISTRO_VALIDADO\" ,p.nomprov,c.descant,s.numlessin " +
                             " FROM siniestros s " +
                             " inner join \"PUAC\" u on s.ageressin = u.\"PUCOD\" " +
                             " inner join autoridades a on a.codaut = s.codaut and a.estaut = 1 " +
                             " inner join \"PUAC\" u1 on s.\"PUCOD\" = u1.\"PUCOD\" " +
                             " inner join \"PUAC\" u2 on s.\"supressIn\" = u2.\"PUCOD\" " +
                             " inner join provincias p on p.codprov = s.codprov" +
                             " inner join cantones c on c.codcant = s.codcant" +
                             " where s.codsin = '" + codisn + "' order by  1; ";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Siniestro objSiniestro = new Siniestro();
                    objSiniestro.codsin = Convert.ToInt32(reader[0].ToString());
                    objSiniestro.fecsin = Convert.ToString(reader[1].ToString());
                    objSiniestro.numfalsin = Convert.ToInt32(reader[2].ToString());
                    objSiniestro.zonsin = Convert.ToString(reader[3].ToString());
                    objSiniestro.dirsin = Convert.ToString(reader[4].ToString());
                    objSiniestro.agente_responsable = Convert.ToString(reader[5].ToString());
                    objSiniestro.USUARIO_REGISTRO = Convert.ToString(reader[6].ToString());
                    objSiniestro.supervisor_responsable = Convert.ToString(reader[7].ToString());
                    objSiniestro.autoridad = Convert.ToString(reader[8].ToString());
                    objSiniestro.REGISTRO_VALIDADO = Convert.ToString(reader[9].ToString());
                    objSiniestro.nomprov = Convert.ToString(reader[10].ToString());
                    objSiniestro.descant = Convert.ToString(reader[11].ToString());
                    objSiniestro.numlessin = Convert.ToInt32(reader[12].ToString());

                    listaSiniestro.Add(objSiniestro);

                }
            }
            catch (Exception)
            {
                throw;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaSiniestro;

        }


        public List<CargaDropDownList> buscaCircuitoZona(int codProv, int codCant, int codpar)
        {
            List<CargaDropDownList> listaDistrito = new List<CargaDropDownList>();
            string codcirc = "";
            string zona = "";
            string coddistri = "";
          
            /// busco elcodigo del circuito
            var a = listaDistritos(codProv, codCant).Take(1);
            if (a.Count() > 0)
            {
                foreach (var d in a)
                {
                    
                    coddistri = d.Text.ToString();
                   

                }
            }

            string findAll = "select p.zona ,p.codcir from parroquias p  where  p.codprov = " + codProv + " and p.codcant = " + codCant + " and p.codpar = "+ codpar + " order by 1 ;";
            try
            {
                 objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                 NpgsqlDataReader reader = comando.ExecuteReader();
                 while (reader.Read())
                 {
                        CargaDropDownList objSiniestro = new CargaDropDownList();
                        
                    objSiniestro.zona = Convert.ToString(reader[0].ToString());
                    objSiniestro.nombre = Convert.ToString(reader[1].ToString());
                    objSiniestro.codigo = Convert.ToString(coddistri.ToString());

                    listaDistrito.Add(objSiniestro);
                }
            }
            catch (Exception ex)
            {
                throw ex;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaDistrito;

        }


        public string listaAccionesPeatonPorCodigoVic(int codvictima)
        {
            List<AccionesPeaton> listaAccPeaton = new List<AccionesPeaton>();
            string acciones_peaton = "";
            string findAll = "select * from acciones_peaton where estaccpea = 1 and codvicinv = "+codvictima +" ;";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    AccionesPeaton objSiniestro = new AccionesPeaton();
                    objSiniestro.codaccpea = Convert.ToInt32(reader[0].ToString());
                    objSiniestro.desaccpea = Convert.ToString(reader[1].ToString());


                    listaAccPeaton.Add(objSiniestro);
                    acciones_peaton += objSiniestro.desaccpea + ",";

                }
            }
            catch (Exception)
            {
                throw;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return acciones_peaton;

        }


        public List<Siniestro> VistaSiniestrosPorAutoridad(string codAutoridad, int cod_usuario)
        {
            List<Siniestro> listaSiniestro = new List<Siniestro>();
            string findAll = "";
            if (cod_usuario == 2 || codAutoridad == "ANT")
            {
                findAll = "select  (select count(*) from siniestros s where s.estsin = 1 and s.codsin in (select s.codsin from siniestros s  WHERE s.estsin = 1 and s.codaut = a.codaut) )  as siniestros , (select count(*) from siniestros b  where b.estsin = 1  and b.regvalsin = true and a.codaut = b.codaut ) as siniestros_val, " +
                                           " (select count(*) from VEHICULOS_INVOLUCRADOS v  where v.estvehinv = 1 and v.codsin in (select s.codsin from siniestros s  WHERE  s.regvalsin = true and   s.estsin = 1 and s.codaut = a.codaut) ) as vehiculos, " +
                                           " (select count(*) from victimas_involucradas v  where v.estvicinv = 1 and v.codsin in (select s.codsin from siniestros s  WHERE  s.regvalsin = true and  s.estsin = 1 and s.codaut = a.codaut) ) as victimas, " +
                                           " (select count(*) from danios_terceros v  where  v.estdater = 1 and  v.codsin in (select s.codsin from siniestros s  WHERE  s.regvalsin = true and   s.estsin = 1 and s.codaut = a.codaut) ) as danios_terceros, " +
                                           " (select count(*) from acciones_peaton   where estaccpea = 1 and codvicinv  in ((select v.codvicinv from victimas_involucradas v  where v.codsin in (select s.codsin from siniestros s  WHERE  s.regvalsin = true and s.estsin = 1 and s.codaut = a.codaut) ) )) as acciones_peaton, " +
                                           " au.desaut as autoridad,  max( to_char(a.fecsin,'YYYY-MM-DD') ) as fecha_hasta, min( to_char(a.fecsin,'YYYY-MM-DD') ) as fecha_desde " +
                                           " from siniestros a inner " +
                                           " join autoridades au on a.codaut = au.codaut " +
                                           "  where a.estsin = 1 " +
                                           "  and a.\"PUCOD\" > 17 " +
                                           "group by au.desaut,a.codaut";
            }
            else
            {
                findAll = "select  (select count(*) from siniestros s where s.estsin = 1 and s.codsin in (select s.codsin from siniestros s  WHERE s.estsin = 1 and s.codaut = a.codaut) )  as siniestros , (select count(*) from siniestros b  where b.estsin = 1  and b.regvalsin = true and a.codaut = b.codaut ) as siniestros_val, " +
                                                           " (select count(*) from VEHICULOS_INVOLUCRADOS v  where v.estvehinv = 1 and v.codsin in (select s.codsin from siniestros s  WHERE  s.regvalsin = true and  s.estsin = 1 and s.codaut = a.codaut) ) as vehiculos, " +
                                                           " (select count(*) from victimas_involucradas v  where v.estvicinv = 1  and v.codsin in (select s.codsin from siniestros s  WHERE  s.regvalsin = true and  s.estsin = 1 and s.codaut = a.codaut) ) as victimas, " +
                                                           " (select count(*) from danios_terceros v  where v.estdater = 1 and  v.codsin in (select s.codsin from siniestros s  WHERE  s.regvalsin = true and  s.estsin = 1 and s.codaut = a.codaut) ) as danios_terceros, " +
                                                           " (select count(*) from acciones_peaton   where estaccpea = 1 and codvicinv  in ((select v.codvicinv from victimas_involucradas v  where v.codsin in (select s.codsin from siniestros s  WHERE  s.regvalsin = true and  s.estsin = 1 and s.codaut = a.codaut) ) )) as acciones_peaton, " +
                                                           " au.desaut as autoridad , max( to_char(a.fecsin,'YYYY-MM-DD') ) as fecha_hasta, min( to_char(a.fecsin,'YYYY-MM-DD') ) as fecha_desde" +
                                                           " from siniestros a inner " +
                                                           " join autoridades au on a.codaut = au.codaut " +
                                                           "  where a.estsin = 1 " +
                                                           "  and a.\"PUCOD\" > 17 and a.codaut = '"+codAutoridad+"' " +
                                                           "group by au.desaut,a.codaut";
            
          }
            
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Siniestro objSiniestro = new Siniestro();
                    objSiniestro.num_sininiestros = Convert.ToString(reader[0].ToString());
                    objSiniestro.num_sininiestros_val = Convert.ToString(reader[1].ToString());
                    objSiniestro.num_vehiculos = Convert.ToString(reader[2].ToString());
                    objSiniestro.num_victimas = Convert.ToString(reader[3].ToString());
                    objSiniestro.num_danios_terceros = Convert.ToString(reader[4].ToString());
                    objSiniestro.num_acciones = Convert.ToString(reader[5].ToString());
                    objSiniestro.autoridad = Convert.ToString(reader[6].ToString());
                    objSiniestro.fecha_hasta = Convert.ToString(reader[7].ToString());
                    objSiniestro.fecha_desde = Convert.ToString(reader[8].ToString());


                    listaSiniestro.Add(objSiniestro);

                }
            }
            catch (Exception)
            {
                throw;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaSiniestro;

        }





        public List<Consultas> VistaBusquedaVictimas(string identificacion)
        {
            List<Consultas> listaSiniestro = new List<Consultas>();
            string findAll = "";
            
                findAll = "SELECT s.codsin, to_char(s.fecsin,'YYYY-MM-DD') as fecsin,v.numidenvicinv,p.nomprov,c.descant,v.convicinv24, v.nomvicinv FROM victimas_involucradas v  " +
                          "inner join siniestros s on v.codsin = s.codsin  and s.estsin = 1" +
                          "inner join provincias p on p.codprov = s.codprov " +
                          "inner join cantones c on c.codcant = s.codcant " +
                          "where numidenvicinv = '" + identificacion + "' and v.estvicinv = 1 " +
                          "order by 1";
          

            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Consultas objSiniestro = new Consultas();
                    objSiniestro.codsin = Convert.ToString(reader[0].ToString());
                    objSiniestro.fecsin = Convert.ToString(reader[1].ToString());
                    objSiniestro.numidenvicinv = Convert.ToString(reader[2].ToString());
                    objSiniestro.nomprov = Convert.ToString(reader[3].ToString());
                    objSiniestro.descant = Convert.ToString(reader[4].ToString());
                    objSiniestro.convicinv24 = Convert.ToString(reader[5].ToString());
                    objSiniestro.nomvicinv = Convert.ToString(reader[6].ToString());

                    listaSiniestro.Add(objSiniestro);

                }
            }
            catch (Exception)
            {
                throw;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaSiniestro;

        }




        public int EliminaVehiculos(int codVehiculo)
        {
            string findAll = "update vehiculos_involucrados set  estvehinv = 0 where codvehinv =  " + codVehiculo + " ";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();


            }
            catch (Exception)
            {
                codVehiculo = 0;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return codVehiculo;
        }

        public int EliminaVictimasPorCodVeh(int codVehiculo)
        {
            string findAll = "update victimas_involucradas set  estvicinv = 0 where codveh =  " + codVehiculo + " ";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();


            }
            catch (Exception)
            {
                codVehiculo = 0;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return codVehiculo;
        }
        public int EliminaVictimasPorCodVic(int codVictima)
        {
            string findAll = "update victimas_involucradas set  estvicinv = 0 where codvicinv =  " + codVictima + " ";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();


            }
            catch (Exception)
            {
                codVictima = 0;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
          
           
            return codVictima;
        }


        public string obtieneCodigosVictimasPorCodigoVeh(int codveh)
        {
            List<Victimas> listaVictimas = new List<Victimas>();
            string victimas = "";
            string findAll = "select  * from victimas_involucradas where codveh  = " + codveh + " ;";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Victimas objSiniestro = new Victimas();
                    objSiniestro.codvicinv = Convert.ToInt32(reader[0].ToString());

                    listaVictimas.Add(objSiniestro);
                    victimas += objSiniestro.codvicinv + ",";

                }
            }
            catch (Exception)
            {
                throw;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return victimas;

        }


        public int EliminaAccionesPeatonPorCodigoVictima( int codvicinv)
        {
            //codvehinv,

            // int codaccpea = Convert.ToInt32(MaxAccionesPeaton());

            string create = "UPDATE public.acciones_peaton " +
                            "SET   estaccpea =0 " +
                            " WHERE codvicinv = " + codvicinv + " ";
            try
            {
                comando = new NpgsqlCommand(create, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                comando.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                codvicinv = 0;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }


            return codvicinv;
        }


        public List<Victimas> VerificaListaVictimasPorCodigoVeh(int codveh)
        {
            List<Victimas> listaVictimas = new List<Victimas>();
            string victimas = "";
            string findAll = "select  * from victimas_involucradas where codveh  = " + codveh + " and estvicinv = 1 ;";
            try
            {
                objConexinDB.getCon().Open();

                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());

                NpgsqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Victimas objSiniestro = new Victimas();
                    objSiniestro.codvicinv = Convert.ToInt32(reader[0].ToString());

                    listaVictimas.Add(objSiniestro);
                  //  victimas += objSiniestro.codvicinv + ",";

                }
            }
            catch (Exception)
            {
                throw;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaVictimas;

        }



        public int EliminaDaniosTercerosPorCodigo(int codDanio)
        {
            //codvehinv,

            // int codaccpea = Convert.ToInt32(MaxAccionesPeaton());

            string create = "UPDATE public.danios_terceros " +
                            "SET   estdater =0 " +
                            " WHERE coddater = " + codDanio + " ";
            try
            {
                comando = new NpgsqlCommand(create, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                comando.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                codDanio = 0;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }


            return codDanio;
        }



        public async Task<List<DatosWSRegistroCivil>> ConsultaDatisWsRegistroCivil(string numeroIdentificacion)
        {

            List<DatosWSRegistroCivil> listaDatos = new List<DatosWSRegistroCivil>();
            DatosWSRegistroCivil d = new DatosWSRegistroCivil();
            string password = "j2x43$uE!4";
            string username = "iNtrAdRANt";
            try
            {
                string xml = "<Envelope xmlns=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
                                "<Body>" +
                                "<getFichaGeneral xmlns=\"http://servicio.interoperadorws.interoperacion.dinardap.gob.ec/\">" +
                                "<codigoPaquete xmlns=\"\">117</codigoPaquete>" +
                                "<numeroIdentificacion xmlns=\"\">" + numeroIdentificacion + "</numeroIdentificacion>" +
                                "</getFichaGeneral>" +
                                "</Body>" +
                              "</Envelope>";
                string url = "http://interoperabilidad.dinardap.gob.ec:7979/interoperador?wsdl";
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                xml = xml.Replace('\\', ' ');

                byte[] requestBytes = System.Text.Encoding.ASCII.GetBytes(xml);

                req.Method = "POST";
                req.ContentType = "text/xml;charset=utf-8";
                //req. = requestBytes.Length;
                req.Credentials = new NetworkCredential(username, password);
                Stream requestStream = await req.GetRequestStreamAsync();
                requestStream.Write(requestBytes, 0,
                requestBytes.Length);

                requestStream.Dispose();


                HttpWebResponse res = (HttpWebResponse)await req.GetResponseAsync();

                StreamReader sr = new StreamReader(res.GetResponseStream(),
                Encoding.UTF8);

                string backstr = sr.ReadToEnd();

                XmlDocument xd = new XmlDocument();

                xd.LoadXml(backstr);


                XmlNodeList nodeList = xd.GetElementsByTagName("valor");

                int cont = 0;

                for (int i = 0; i<= nodeList.Count; i++)
                {
                    cont++;
                    if (cont == 1)
                        d.nombre = nodeList[i].InnerText;
                    if (cont == 3)
                        d.fechaNacimiento = nodeList[i].InnerText;
                    if (cont == 19)
                    {
                        d.sexo = nodeList[i].InnerText;
                    }
                    if (cont == 1 || cont == 3 || cont == 19)
                    {
                        if (d.nombre != "" && d.nombre != null)
                            listaDatos.Add(d);
                        if (d.fechaNacimiento != "" && d.fechaNacimiento != null)
                        {
                            d.edad = CalcularEdad(d.fechaNacimiento);
                            listaDatos.Add(d);
                        }
                        if (d.sexo != "" && d.sexo != null)
                            listaDatos.Add(d);
                        if (cont == 19)
                        {

                            d.codigoPaquete = "1";
                            listaDatos.Add(d);
                            break;
                        }
                            
                    }
                }
                sr.Dispose();
                res.Dispose();
            }
            catch (Exception ex)
            {
                ex.ToString();
                d.codigoPaquete = "0";
                listaDatos.Add(d);
            }


            return listaDatos;
        }

        public List<Victimas> listaVictimasInvolucradosSinNombre()
        {
            List<Victimas> listaVictimasInvolucrado = new List<Victimas>();
            //  objConexinDB = ConexionDB.saberEstado();
            string findAll = " select codvicinv,numidenvicinv from victimas_involucradas where  nomvicinv = ''  order by 1;";
            try
            {
                //if (objConexinDB.getCon().State.ToString() == "Closed")


                comando = new NpgsqlCommand(findAll, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                NpgsqlTransaction t = objConexinDB.getCon().BeginTransaction();
                NpgsqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Victimas objVict = new Victimas();
                    objVict.codvicinv = Convert.ToInt32(reader[0].ToString());
                    objVict.numidenvicinv = Convert.ToString(reader[1].ToString());
                    listaVictimasInvolucrado.Add(objVict);

                }
            }
            catch (Exception ex)
            {
                throw ex;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaVictimasInvolucrado;

        }

  

        public int ActualizaNombresVictimas(Victimas v)
        {
            //codvehinv,

            int codvic = Convert.ToInt32(v.codvicinv);

            string create = "update victimas_involucradas set nomvicinv = '"+v.nombreVictima+"', sexvicinv = '"+v.sexo+"', edavicinv ="+v.edavicinv+" where numidenvicinv = '"+v.numidenvicinv+"' ";
            try
            {
                comando = new NpgsqlCommand(create, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                comando.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                codvic = 0;
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }


            return codvic;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Joe Bloggs", "jbloggs@example.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text = message };

            using (var client = new SmtpClient())
            {
                client.LocalDomain = "some.domain.com";
                await client.ConnectAsync("smtp.relay.uri", 25, SecureSocketOptions.None).ConfigureAwait(false);
                await client.SendAsync(emailMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
        }


        public void enviarMail()
        {
            try
            {
                //From Address 
                string FromAddress = "luchosdj@hotmail.com";
                string FromAdressTitle = "Email from ASP.NET Core 1.1";
                //To Address 
                string ToAddress = "lsaigua@gmdigitalecuador.com";
                string ToAdressTitle = "Microsoft ASP.NET Core";
                string Subject = "Hello World - Sending email using ASP.NET Core 1.1";
                string BodyContent = "ASP.NET Core was previously called ASP.NET 5. It was renamed in Octubre 2017. It supports cross-platform frameworks ( Windows, Linux, Mac ) for building modern cloud-based internet-connected applications like IOT, web apps, and mobile back-end.";

                //Smtp Server 
                string SmtpServer = "smtp.live.com";
                //Smtp Port Number 
                int SmtpPortNumber = 587;

                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(FromAdressTitle, FromAddress));
                mimeMessage.To.Add(new MailboxAddress(ToAdressTitle, ToAddress));
                mimeMessage.Subject = Subject;
                mimeMessage.Body = new TextPart("plain")
                {
                    Text = BodyContent

                };

                using (var client = new SmtpClient())
                {

                    client.Connect(SmtpServer, SmtpPortNumber, false);
                    // Note: only needed if the SMTP server requires authentication 
                    // Error 5.5.1 Authentication  
                    client.Authenticate("luchosdj@hotmail.com", "2017luis--");
                    client.Send(mimeMessage);
                    Console.WriteLine("The mail has been sent successfully !!");
                    Console.ReadLine();
                    client.Disconnect(true);

                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}