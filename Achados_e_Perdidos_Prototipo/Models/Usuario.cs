using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;


namespace Achados_e_Perdidos_Prototipo
{
    class Usuario
    {
        public void DetalhaUsuario(string Email, string Senha,
            out string Retorno)
        {
            try
            {
                

                String Qry;

                SQLServer SQL = new SQLServer();
                SQL.ConectaDB();

                Qry = "select * from tblUsuario where emailUsuario = '" + Email.Trim() + "' and senhaUsuario = '" + Senha.Trim() + "'";

                SqlDataReader Rs = SQL.DR(Qry);

                if (Rs.Read() == false)
                {

					Retorno = "ERRO: O e-mail ou a senha inseridos estão incorretos.";

                }
                else
                {
                    //desloga caso já exista um logon feito.
                    Deslogar();

                    FormsAuthentication.SignOut();

                    //Guarda o usuário e senha encriptado em um cookie.
                    FormsAuthentication.SetAuthCookie(Email + ";" + Senha, false);
                    Retorno = "OK";
                }
                
                

            }

            catch (Exception Ex)
            {
                Retorno =  "ERRO: " + Ex.Message;
            }
            

            
        }

        //Método consulta o cookie de logon e valida usuário e senha contidos nele. 
        public bool getUsuarioLogado()
        {
            //Carrega variável com strings contidas no cookie criado, caso ele exista.
            string Login = HttpContext.Current.User.Identity.Name;

            if (Login == "")
            {
                return false;
            }
            else
            {
                string[] Usr = Login.Split(';');
                string usuario = Usr[0];
                string senha = Usr[1];
                string Resp = "";

                DetalhaUsuario(usuario, senha, out Resp);

                if (Resp.Contains("ERRO") == true)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
        }
        //Méto que desloga o usuário.
        public void Deslogar()
        {

            FormsAuthentication.SignOut();
        }



        internal void ValidaEmailRecSenha(string EmailID, out string Retorn)
        {
            throw new NotImplementedException();
        }
    }
}
