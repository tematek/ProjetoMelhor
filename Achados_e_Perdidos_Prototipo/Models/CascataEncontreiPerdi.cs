using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Achados_e_Perdidos_Prototipo.Models
{
	public class CascataEncontreiPerdi
	{
		public string Erro;

		public List<Uf> ListaUF()
		{

			List<Uf> ListaEstados = new List<Uf>();

			String Qry;

			SQLServer SQL = new SQLServer();
			string retornoCnn = SQL.ConectaDB();

            if (retornoCnn.Contains("ERRO") == true)
            {
                Erro = retornoCnn;
                return ListaEstados;

            }
            
			Qry = "select * from tblEstado order by nomeEstado";

			SqlDataReader Rs = SQL.DR(Qry);

            

			while (Rs.Read())
			{
				Uf Estado = new Uf();
				Estado.UF = Rs["nomeEstado"].ToString();
				Estado.idUF = Convert.ToInt32(Rs["codEstado"].ToString());

				ListaEstados.Add(Estado);


			}

            SQL.Cnx.Close();

			return ListaEstados;




		}

		public List<Cidades> ListaCitys(string estados)
		{

			List<Cidades> ListaCitys = new List<Cidades>();

			String Qry;

			SQLServer SQL = new SQLServer();
			SQL.ConectaDB();

			Qry = "select * from tblCidade where codEstado = '" + estados + "' order by nomeCidade";

			SqlDataReader Rs = SQL.DR(Qry);


			while (Rs.Read())
			{
				Cidades Cdd = new Cidades();
				Cdd.City = Rs["nomeCidade"].ToString();
				Cdd.idCity = Convert.ToInt32(Rs["codCidade"].ToString());
				ListaCitys.Add(Cdd);

			}

            SQL.Cnx.Close();
			return ListaCitys;

		}


        //lista bairros
        public List<Bairros> ListaBairros(string cidade)
        {

            List<Bairros> ListaBairros = new List<Bairros>();

            String Qry;

            SQLServer SQL = new SQLServer();
            SQL.ConectaDB();

            Qry = "select * from tblBairro where codCidade = " + cidade + " order by nomeBairro";

            SqlDataReader Rs = SQL.DR(Qry);


            while (Rs.Read())
            {
                Bairros Brrs = new Bairros();

                Brrs.idBairro = Convert.ToInt32(Rs["codBairro"].ToString());
                Brrs.Bairro = Rs["nomeBairro"].ToString();

                ListaBairros.Add(Brrs);
                

            }

            SQL.Cnx.Close();
            return ListaBairros;

        }

		//Lista categoria :)
		public List<Categoria> ListaCategoria()
		{

			List<Categoria> ListaCategoria = new List<Categoria>();

			String Qry;

			SQLServer SQL = new SQLServer();
			SQL.ConectaDB();

			Qry = "select * from tblCategoria order by nomeCategoria";

			SqlDataReader Rs = SQL.DR(Qry);


			while (Rs.Read())
			{
				Categoria Catego = new Categoria();
				Catego.Cat = Rs["nomeCategoria"].ToString();
				Catego.idCategoria = Convert.ToInt32(Rs["codCategoria"].ToString());

				ListaCategoria.Add(Catego);


			}

            SQL.Cnx.Close();

			return ListaCategoria;

		}
		//Lista SubCat
		public List<SubCat> ListaSubCat(string categoria)
		{

			List<SubCat> ListaSubCat = new List<SubCat>();

			String Qry;

			SQLServer SQL = new SQLServer();
			SQL.ConectaDB();

            Qry = "select codCategoria, nomeSubCategoria from tblSubCategoria where codCategoria = '" + categoria +
                "'  group by codCategoria, nomeSubCategoria order by codCategoria, nomeSubCategoria";

			SqlDataReader Rs = SQL.DR(Qry);


			while (Rs.Read())
			{
				SubCat SubCatego = new SubCat();
                SubCatego.SubCatego = Rs["nomeSubCategoria"].ToString();
                SubCatego.idSubCat = Convert.ToInt32(Rs["codCategoria"].ToString());

				ListaSubCat.Add(SubCatego);


			}

            SQL.Cnx.Close();
			return ListaSubCat;

		}


		//Lista Descrição
		public List<Descricao> ListaDescricao(string subcat)
		{

			List<Descricao> ListaDescr = new List<Descricao>();

			String Qry;

			SQLServer SQL = new SQLServer();
			SQL.ConectaDB();

            Qry = "select codSubCategoria, nomeSubCategoria, detalheSubCategoria from tblSubCategoria where nomeSubCategoria = '" + subcat + "'";

			SqlDataReader Rs = SQL.DR(Qry);


			while (Rs.Read())
			{
                Descricao Desc = new Descricao();


                Desc.Descri = Rs["detalheSubCategoria"].ToString();
                Desc.idDescricao = Rs["codSubCategoria"].ToString();


                ListaDescr.Add(Desc);


			}

            SQL.Cnx.Close();
            return ListaDescr;

		}


	}
}