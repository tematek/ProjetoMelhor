using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Achados_e_Perdidos_Prototipo.Models
{
    public class ItemsMapa: CascataEncontreiPerdi
    {
        public string Erro;

        public List<CamposItemMapa> ListaItemsMapa(string tipoItems)
        {

            

            List<CamposItemMapa> ListaItemsMapa = new List<CamposItemMapa>();

            String Qry;

            SQLServer SQL = new SQLServer();
            string retornoCnn = SQL.ConectaDB();

            if (retornoCnn.Contains("ERRO") == true)
            {
                Erro = retornoCnn;
                return ListaItemsMapa;

            }

            String filtros1 = null;
            String filtros2 = null;

            //verifica se parâmetro contem filtro
            if (tipoItems.Contains("-") == true)
            {

                String[] vSplit = tipoItems.Split('-');

                filtros1 = vSplit[1];
                filtros2 = vSplit[2];

            }

            if (tipoItems.Contains("Encontrados") == true)
            {


                Qry = "Select A.Latitude, A.Longitude, A.codItemPerdido, "
                    + "D.emailUsuario, C.nomeCategoria, "
                    + "B.nomeSubCategoria, D.nomeUsuario, D.sobrenomeUsuario, D.telFixo, D.telCelular"
                    + " from tblItemPerdido as A inner Join tblSubCategoria as B"
                    + " on (A.codSubCategoria = B.codSubCategoria) #filtros1 #filtros2" 
                    + " inner join tblCategoria as C on (a.codCategoria = c.codCategoria)"
                    + " inner join tblUsuario as D on (a.idUsuario = d.idUsuario)";

                if (filtros1 != "0" && filtros1 != null)
                {

                    Qry = Qry.Replace("#filtros1", " and A.codCategoria = "
                        + filtros1);

                }
                else {

                    Qry = Qry.Replace("#filtros1", "");
                }

                if (filtros2 != "0" && filtros2 != null)
                {

                    Qry = Qry.Replace("#filtros2", " and A.codSubCategoria = "
                        + filtros2);

                }
                else
                {

                    Qry = Qry.Replace("#filtros2", "");
                }
               
            }
            else
            {

                Qry = "Select A.codItemEncontrado, A.Latitude, A.Longitude, D.emailUsuario, "
                    + " C.nomeCategoria, B.nomeSubCategoria, D.nomeUsuario, D.sobrenomeUsuario, "
                    + "D.telFixo, D.telCelular"
                    + " from tblItemEncontrado as A inner Join tblSubCategoria as B"
                    + " on (A.codSubCategoria = B.codSubCategoria) #filtros1 #filtros2"
                    + " inner join tblCategoria as C on (a.codCategoria = c.codCategoria)"
                    + " inner join tblUsuario as D on (a.idUsuario = d.idUsuario)";

                if (filtros1 != "0" && filtros1 != null)
                {

                    Qry = Qry.Replace("#filtros1", " and A.codCategoria = "
                        + filtros1);

                }
                else
                {

                    Qry = Qry.Replace("#filtros1", "");
                }

                if (filtros2 != "0" && filtros2 != null)
                {

                    Qry = Qry.Replace("#filtros2", " and A.codSubCategoria = "
                        + filtros2);

                }
                else
                {

                    Qry = Qry.Replace("#filtros2", "");
                }

            }



            SqlDataReader Rs = SQL.DR(Qry);

            while (Rs.Read())
            {
                CamposItemMapa ItemsMapa = new CamposItemMapa();

                if (tipoItems.Contains("Encontrados") == true)
                {
                    ItemsMapa.idItem = Convert.ToInt32(Rs["codItemPerdido"].ToString());
                }
                else
                {
                    ItemsMapa.idItem = Convert.ToInt32(Rs["codItemEncontrado"].ToString());
                }

                ItemsMapa.Categoria = Rs["nomeCategoria"].ToString();
                ItemsMapa.SubCategoria = Rs["nomeSubCategoria"].ToString();
                ItemsMapa.email = Rs["emailUsuario"].ToString();
                ItemsMapa.Nome = Rs["nomeUsuario"].ToString();
                ItemsMapa.Sobrenome = Rs["sobrenomeUsuario"].ToString();
                ItemsMapa.Tel = Rs["telFixo"].ToString();
                ItemsMapa.Celular = Rs["telCelular"].ToString();
                ItemsMapa.email = Rs["emailUsuario"].ToString();
                ItemsMapa.Latitude = Rs["longitude"].ToString().Replace(",",".");
                ItemsMapa.Longitude = Rs["latitude"].ToString().Replace(",", ".");


                ListaItemsMapa.Add(ItemsMapa);


            }

            SQL.Cnx.Close();

            ////********TRECHO DE TESTES - HABILITAR OS CODIGOS ACIMA QUANDO CONSTATADO QUE FUNCIONALIDADE ESTÁ OK.
            //CamposItemMapa ItemsMapa = new CamposItemMapa();

            //ItemsMapa.idItem = 1;
            //ItemsMapa.Categoria = "Animal";
            //ItemsMapa.email = "boti.fernando@gmail.com";
            //ItemsMapa.Latitude = "-46.70011240000002";
            //ItemsMapa.Longitude = "-23.54984692299866";

            //ListaItemsMapa.Add(ItemsMapa);

            return ListaItemsMapa;

        }
    }
}