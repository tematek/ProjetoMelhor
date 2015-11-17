using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Achados_e_Perdidos_Prototipo.Models
{
	public class ParametrosCadastro
	{

        public string InsereCadastro(string Nome, string SobreNome,
            string Cpf, string Tel, string Cel,
            string Email, string Senha, string ConfirmSenha)
		{
			try
			{

				String Qry = "";

				SQLServer SQL = new SQLServer();
				SQL.ConectaDB();

                Qry = "Insert into tblUsuario " +
                    "( emailUsuario, senhaUsuario, nomeUsuario, sobrenomeUsuario, telFixo, telCelular, cpf) " +
                    "values ('" + Email + "','"
                    + Senha + "','" + Nome + "','"
                    + SobreNome + "','" + Tel + "','" + Cel + "','" + Cpf + "')";

				SQL.ExecQry(Qry);

				SQL.Cnx.Close();
				return "CADASTRO REALIZADO COM SUCESSO.";

			}

			catch (Exception Ex)
			{
				return "ERRO: " + Ex.Message;
			}



		}

        public string ValidaCamposCadastros(string Nome, string SobreNome,
            string Cpf, string Tel, string Cel,
            string Email, string Senha, string ConfirmSenha)
		{
			try
			{


                bool retorn = ValidaTelefone(Cel);

				if (retorn == false)
				{
					return "ERRO: CELULAR INVÁLIDO.";
				}

                retorn = ValidaTelefone(Tel);

                if (retorn == false)
                {
                    return "ERRO: TELEFONE INVÁLIDO.";
                }

                retorn = ValidaEmail(Email);

				if (retorn == false)
				{
					return "ERRO: EMAIL INVÁLIDO.";
				}

				if (Senha.Length < 6)
				{
					return "ERRO: SENHA DEVE CONTER 6 OU MAIS CARACTÉRES.";
				}
				else
				{
					if (Senha != ConfirmSenha)
					{
						return "ERRO: CONFIRME A SENHA CORRETAMENTE.";
					}

				}

                string nCpf = Cpf.Replace(".", "").Replace("-", "");
                nCpf = nCpf.Substring(0, 9);
                string nControle = Cpf.Substring(12, 2);

				retorn = IsCpf(nCpf, nControle);

				if (retorn == false)
				{
					return "ERRO: CPF INVÁLIDO.";
				}
				else
				{
                    retorn = ConsultaCpf(Cpf.Replace("-","").Replace(".",""));
					if (retorn == false)
					{
						return "ERRO: CPF INFORMADO JÁ FOI CADASTRADO.";
					}

				}

				return "Ok";

			}

			catch (Exception Ex)
			{
				return "ERRO: " + Ex.Message;
			}



		}



		public bool ConsultaCpf(string cpf)
		{

			String Qry;

			SQLServer SQL = new SQLServer();
			string retornoCnn = SQL.ConectaDB();

			if (retornoCnn.Contains("ERRO") == true)
			{

				return false;

			}

			Qry = "select * from tblUsuario where cpf = '" + cpf + "'";

			SqlDataReader Rs = SQL.DR(Qry);

			if (Rs.Read())
			{
				return false;
			}

			
			SQL.Cnx.Close();

			return true;

		}

		//Método que valida o Email
		public bool ValidaEmail(string email)
		{
            bool validEmail = false;
            int indexArr = email.IndexOf('@');
            if (indexArr > 0)
            {
                int indexDot = email.IndexOf('.', indexArr);
                if (indexDot - 1 > indexArr)
                {
                    if (indexDot + 1 < email.Length)
                    {
                        string indexDot2 = email.Substring(indexDot + 1, 1);
                        if (indexDot2 != ".")
                        {
                            validEmail = true;
                        }
                    }
                }
            }
            return validEmail;
		}
		//Método que valida o TELEFONE
		public static bool ValidaTelefone(string telefone)
		{

            double i;

            if (double.TryParse(telefone, out i))
            {
                return true;
            }
            else
            {
                return false;
            }
		}

		public static bool IsCpf(string nCpf, string nControle)
		{
			double nResult;

			string cpf = "";

			if (double.TryParse(nCpf, out nResult) == false)
			{
				return false;
			}
			else
			{
				cpf = string.Format("{0:000000000}", nResult);
			}

			if (double.TryParse(nControle, out nResult) == false)
			{
				return false;
			}
			else
			{
				cpf += string.Format("{0:00}", nResult);
			}

			int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
			int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
			string tempCpf;
			string digito;
			int soma;
			int resto;
			cpf = cpf.Trim();
			cpf = cpf.Replace(".", "").Replace("-", "");
			if (cpf.Length != 11)
				return false;
			tempCpf = cpf.Substring(0, 9);
			soma = 0;

			for (int i = 0; i < 9; i++)
				soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
			resto = soma % 11;
			if (resto < 2)
				resto = 0;
			else
				resto = 11 - resto;
			digito = resto.ToString();
			tempCpf = tempCpf + digito;
			soma = 0;
			for (int i = 0; i < 10; i++)
				soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
			resto = soma % 11;
			if (resto < 2)
				resto = 0;
			else
				resto = 11 - resto;
			digito = digito + resto.ToString();

			return cpf.EndsWith(digito);
		}


	}
}