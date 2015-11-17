using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Achados_e_Perdidos_Prototipo
{
    class SQLServer
    {
        public string DBError = "";
        public SqlConnection Cnx;
        SqlTransaction Trans;

        public void BeginTrans()
        {
            Trans = Cnx.BeginTransaction();

        }

        public void CommitTrans()
        {
            Trans.Commit();
        }

        public void RollBackTrans()
        {
            Trans.Rollback();
        }


        public string ConectaDB()
        {
            try
            {

                string StrConn;

				//string de conexão oficial do sistema.
				//StrConn = "Server=tcp:nsau5brohn.database.windows.net,1433;Database=AchadosPerdidos;Uid=benfeitores@ki2o0d4n08;Pwd=Projeto032015;Encrypt=yes;Connection Timeout=30;";


				StrConn = "Server=tcp:nsau5brohn.database.windows.net,1433;Database=AchadosPerdidos;User ID=AchadosPerdidos@nsau5brohn;Password=Projeto032015;Trusted_Connection=False;Encrypt=True;Connection Timeout=30";

                //string de conexão abaixo é usada pelo Fernando 
                //quando ele realiza testes localmente na máquina dele. 
                //Ela está comentada, então não influência em nada na execução real.

				//StrConn = "Data Source=IKONAS-PC\\SQLEXPRESS;Initial Catalog=AchadosPerdidosOficial;User Id=sa;Password=ikonas";
                
                Cnx = new SqlConnection(StrConn);


                Cnx.Open();
                return "OK";

            }
            catch (Exception Ex)
            {
                return "ERRO: " + Ex.Message;
            }

        }

        public SqlDataReader DR(String Qry)
        {
            DBError = "";
            try
            {
                SqlCommand Cmd = new SqlCommand(Qry, Cnx);
                Cmd.Transaction = Trans;
                SqlDataReader DR = Cmd.ExecuteReader(CommandBehavior.CloseConnection);


                return DR;
            }
            catch (Exception Ex)
            {

                DBError = "ERRO: " + Ex.Message;
                return null;
            }


        }

        public double ExecQry(string[] campos, string[] valores, string StrQry)
        {
            //System.Threading.Thread.Sleep(15);
            try
            {

                SqlCommand cmd = new SqlCommand(StrQry, Cnx);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 500;

                cmd.CommandText = StrQry;
                cmd.Transaction = Trans;

                for (int i = 0; i < valores.Length; i++)
                {
                    if (string.IsNullOrEmpty(valores[i]))
                    {
                        cmd.Parameters.AddWithValue(campos[i], DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(campos[i], valores[i]);
                    }
                }
                DBError = "";



                double RecAffected = cmd.ExecuteNonQuery();

                cmd.Dispose();
                return RecAffected;


            }
            catch (Exception Ex)
            {
                DBError = "ERRO: " + Ex.Message;
                return -9;
            }


        }

        public double ExecQry(string StrQry)
        {

            if (DBError != "")

                throw new System.ArgumentException(DBError);

            try
            {

                SqlCommand cmd = new SqlCommand(StrQry, Cnx);

                cmd.CommandType = CommandType.Text;

                cmd.CommandTimeout = 5500;

                cmd.Transaction = Trans;

                double RecAffected = cmd.ExecuteNonQuery();

                cmd.Dispose();

                return RecAffected;

            }

            catch (Exception Ex)
            {

                DBError = "ERRO: " + Ex.Message;

                return -9;

            }

        }


    }

}
