using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace Achados_e_Perdidos_Prototipo.Models
{
    public class ParametrosEncontreiPerdi
    {
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Categoria { get; set; }
        public string Subcategoria { get; set; }

        public string TipoItem { get; set; }

        public string lttde { get; set; }

        public string lgttde { get; set; }

        public string InsereItemEncontrado(ParametrosEncontreiPerdi Parametros)
        {
            try
            {

                //Carrega variável com strings contidas no cookie criado, caso ele exista.
                string Login = HttpContext.Current.User.Identity.Name;


                string[] Usr = Login.Split(';');
                string usuario = Usr[0];
                string senha = Usr[1];

                String Qry;

                SQLServer SQL = new SQLServer();
                SQL.ConectaDB();

                Qry = "select idUsuario from tblUsuario where emailUsuario = '" + usuario.Trim() + "' and senhaUsuario = '" + senha.Trim() + "'";

                SqlDataReader Rs = SQL.DR(Qry);

                string Id = "";

                if (Rs.Read() == true)
                {

                    Id = Rs["idUsuario"].ToString();

                }

                SQL.Cnx.Close();
                SQL.ConectaDB();

                Qry = "Insert into tblItemEncontrado " +
                    "(idUsuario, codCategoria, codSubCategoria, codPais, "
                    + "dataCadastro, descricao, Latitude, Longitude) " +
                    "values (" + Id + ", " + Parametros.Categoria + ","
                    + Parametros.Subcategoria + ", 1, getdate(), 'null', '" + 
                    Parametros.lttde + "','" + Parametros.lgttde + "')";

                SQL.ExecQry(Qry);

                SQL.Cnx.Close();
                return "OK";

            }

            catch (Exception Ex)
            {
                return "ERRO: " + Ex.Message;
            }



        }

        public string InsereItemPerdido(ParametrosEncontreiPerdi Parametros)
        {
            try
            {

                //Carrega variável com strings contidas no cookie criado, caso ele exista.
                string Login = HttpContext.Current.User.Identity.Name;


                string[] Usr = Login.Split(';');
                string usuario = Usr[0];
                string senha = Usr[1];

                String Qry;

                SQLServer SQL = new SQLServer();
                SQL.ConectaDB();

                Qry = "select idUsuario from tblUsuario where emailUsuario = '" 
					+ usuario.Trim() + "' and senhaUsuario = '" + senha.Trim() + "'";

                SqlDataReader Rs = SQL.DR(Qry);

                string Id = "";

                if (Rs.Read() == true)
                {

                    Id = Rs["idUsuario"].ToString();

                }

                SQL.Cnx.Close();
                SQL.ConectaDB();

                Qry = "Insert into tblItemPerdido " +
                    "(idUsuario, codCategoria, codSubCategoria, codPais, "
                    + "dataResposta, descricao, situacao, Latitude, Longitude) " +
                    "values (" + Id + "," + Parametros.Categoria + ","
                    + Parametros.Subcategoria + ", 1, getdate(), 'null', 'null', '"
                    + Parametros.lttde + "','" + Parametros.lgttde + "')";

                SQL.ExecQry(Qry);

                SQL.Cnx.Close();
                return "OK";

            }

            catch (Exception Ex)
            {
              
                return "ERRO: " + Ex.Message;
            }



        }

    }
}