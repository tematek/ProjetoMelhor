using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Achados_e_Perdidos_Prototipo.Models;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text.RegularExpressions;
using System.Net.Configuration;


namespace Achados_e_Perdidos_Prototipo
{

	public class HomeController : Controller
	{

        //teste

		//
		// GET: /Home/

		public ActionResult Index()
		{
			return View();
		}

        public ActionResult MapaPerdi()
        {
            Usuario User = new Usuario();

            //Verificar se usuário está logado.

            bool Resp = User.getUsuarioLogado();

            //Método retornou false => usuário não está logado.
            if (Resp == false)
            {
                Login log = new Login();
                log.Result = "É necessário logar para cadastrar um item.";
                return View("Entrar", log);
            }
            else
            {
                return View();
            }
            
        }

        public ActionResult MapaEncontrei()
        {
            Usuario User = new Usuario();

            //Verificar se usuário está logado.

            bool Resp = User.getUsuarioLogado();

            //Método retornou false => usuário não está logado.
            if (Resp == false)
            {
                Login log = new Login();
                log.Result = "É necessário logar para cadastrar um item.";
                return View("Entrar", log);
            }
            else
            {
                return View();
            }
        }

		public ActionResult Encontrei()
		{
            Usuario User = new Usuario();
            
            //Verificar se usuário está logado.
            
            bool Resp = User.getUsuarioLogado();

            //Método retornou false => usuário não está logado.
            if (Resp == false)
            {
                Login log = new Login();
                log.Result = "É necessário logar para cadastrar um item.";
                return View("Entrar", log);
            }
            //Método retornou true => usuário está logado.
            else
            {
                //Busca ESTADOS
                ViewBag.idUF = new SelectList
                (
                    new CascataEncontreiPerdi().ListaUF(),
                    "idUF",
                    "UF"
                );
                //Busca CATEGORIAS
                ViewBag.idCategoria = new SelectList
                (
                    new CascataEncontreiPerdi().ListaCategoria(),
                    "idCategoria",
                    "Cat"
                );

                return View();
            }
			
		}
		//RETORNA CIDADES
		public JsonResult GetCidades(string estado)
		{

			CascataEncontreiPerdi Enc = new CascataEncontreiPerdi();

			List<Cidades> ListaCitys = new List<Cidades>();

			ListaCitys = Enc.ListaCitys(estado);

			return this.Json(ListaCitys, JsonRequestBehavior.AllowGet);


		}


        //RETORNA BAIRROS
        public JsonResult GetBairros(string cidade)
        {

            CascataEncontreiPerdi Enc = new CascataEncontreiPerdi();

            List<Bairros> ListaBairros = new List<Bairros>();

            ListaBairros = Enc.ListaBairros(cidade);

            return this.Json(ListaBairros, JsonRequestBehavior.AllowGet);


        }

        //RETORNA pontos que serão exibidos no mada
        public JsonResult GetItemMapa(string tipoItems)
        {

            ItemsMapa MapaItems = new ItemsMapa();

            List<CamposItemMapa> ListaItemMapa = new List<CamposItemMapa>();

            ListaItemMapa = MapaItems.ListaItemsMapa(tipoItems);

            return this.Json(ListaItemMapa, JsonRequestBehavior.AllowGet);


        }


		//RETORNA SUB-CATEGORAIS
		public JsonResult GetsubCategoria(string categoria)
		{

			CascataEncontreiPerdi Enc = new CascataEncontreiPerdi();

			List<SubCat> ListaSubCategorias = new List<SubCat>();

			ListaSubCategorias = Enc.ListaSubCat(categoria);

			return this.Json(ListaSubCategorias, JsonRequestBehavior.AllowGet);


		}


		//RETORNA DESCRIÇÃO SUB-CATEGORAIS
		public JsonResult GetDescricaosubCategoria(string subcategoria)
		{

			CascataEncontreiPerdi Enc = new CascataEncontreiPerdi();

            List<Descricao> ListaDescricao = new List<Descricao>();

            ListaDescricao = Enc.ListaDescricao(subcategoria);

			return this.Json(ListaDescricao, JsonRequestBehavior.AllowGet);


		}

		public ActionResult Perdi()
		{

            Usuario User = new Usuario();
            
            //Verificar se usuário está logado.
            
            bool Resp = User.getUsuarioLogado();

            //Método retornou false => usuário não está logado.
            if (Resp == false)
            {
                Login log = new Login();
                log.Result = "É necessário logar para cadastrar um item.";
                return View("Entrar", log);
            }
            //Método retornou true => usuário está logado.
            else
            {
                //Busca ESTADOS
                ViewBag.idUF = new SelectList
                (
                    new CascataEncontreiPerdi().ListaUF(),
                    "idUF",
                    "UF"
                );
                //Busca CATEGORIAS
                ViewBag.idCategoria = new SelectList
                (
                    new CascataEncontreiPerdi().ListaCategoria(),
                    "idCategoria",
                    "Cat"
                );

                return View();

            }


		}

		public ActionResult Sobre()
		{
			return View();
		}

		public ActionResult Cadastro()
		{
            var cadastro = new CamposCadastro();
            return View(cadastro);
		}



		public ActionResult Parcerias()
		{
			return View();
		}

		public ActionResult Contribua()
		{
			return View();
		}

        public ActionResult Termo()
        {
            return View();
        }

		public ActionResult Entrar(Login logon)
		{

			return View(logon);

		}

        //Passa os parâmetros de ENCONTREI e PERDI.
        [HttpPost]
        public JsonResult SubmeteEncontreiPerdi(ParametrosEncontreiPerdi ParametrosEncont)
        {
            if (ParametrosEncont.TipoItem == "Encontrei")
            {
                ParametrosEncont.InsereItemEncontrado(ParametrosEncont);
            }
            else
            {
                ParametrosEncont.InsereItemPerdido(ParametrosEncont);
            }
            
            return Json(new { Status = true }, JsonRequestBehavior.DenyGet);
        }

		[HttpPost]
		public ActionResult ValidaLogon(string EmailID, string SenhaID)
		{

			Usuario User = new Usuario();

			string Retorn;

			User.DetalhaUsuario(EmailID, SenhaID, out Retorn);

			if (Retorn.Contains("ERRO") == true)
			{

				Login log = new Login();

				log.Result = Retorn;

				return View("Entrar", log);

			}
			else
			{
				return View("Index");

			}

		}


        //Valida Cadastro
        [HttpPost]
        public ActionResult ValidaCadastro(string Nome, string SobreNome, 
            string Cpf, string Tel, string Cel,
            string Email, string Senha, string ConfirmSenha)
        {

            CamposCadastro CCadastro = new CamposCadastro();
            ParametrosCadastro PC = new ParametrosCadastro();

            string Retorn;

            Retorn = PC.ValidaCamposCadastros(Nome,SobreNome,Cpf,
                Tel.Replace("-",""),Cel.Replace("-",""),Email,Senha,ConfirmSenha);

            if (Retorn.Contains("ERRO") == true)
            {


                CCadastro.ResultadoCadastro = Retorn;

                return View("Cadastro", CCadastro);

            }
            else
            {

                Retorn = PC.InsereCadastro(Nome, SobreNome, Cpf.Replace(".","").Replace("-",""),
                    Tel.Replace("-", ""), Cel.Replace("-", ""), Email, Senha, ConfirmSenha);

                if (Retorn.Contains("ERRO") == true)
                {


                    CCadastro.ResultadoCadastro = Retorn;

                    return View("Cadastro", CCadastro);

                }

                Login log = new Login();

                log.Result = Retorn;

                return View("Entrar", log);


            }

        }


        //Valida Envio
        [HttpPost]
        public ActionResult ValidaEnvioCantato(string Nome, string Email,
            string Assunto, string Msg)
        {
            ContactModel Contact = new ContactModel();

            try
            {
                ParametrosCadastro PC = new ParametrosCadastro();

                bool Resp = PC.ValidaEmail(Email);

                if (Resp == false)
                {
                    Contact.Resultado = "ERRO: E-mail inválido";
                    return View("Contato", Contact);
                }

                // Gmail Remetente
                var fromAddress = "contato.benfeitores@gmail.com";

                // Email destino
                var toAddress = "contato.benfeitores@gmail.com";

                //Senha do e-mail remetente
                const string fromPassword = "Projeto032015";

                // Assunto
                string subject = Assunto;

                // Corpo do e-mail
                string body = "De: " + Nome + "\n";
                body += "Email: " + Email + "\n";
                body += "Assunto: " + Assunto + "\n";
                body += "Mensagem: \n" + Msg + "\n";

                // smtp settings
                var smtp = new System.Net.Mail.SmtpClient();
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                    smtp.Timeout = 20000;
                }

                // Realiza os envios.
                smtp.Send(fromAddress, toAddress, subject, body);

                Contact.Resultado = "E-mail enviado com sucesso. Obrigado por entrar em contato conosco.";
                return View("Contato", Contact);

            }
            catch (Exception ex)
            {
                Contact.Resultado = "ERRO: " + ex.InnerException.ToString();
                return View("Contato", Contact);

            }

        }

        public ActionResult Contato()
        {

            ContactModel contModel = new ContactModel();


            Usuario User = new Usuario();

            //Verificar se usuário está logado.

            bool Resp = User.getUsuarioLogado();

            //Método retornou false => usuário não está logado.
            if (Resp == false)
            {
                Login log = new Login();
                log.Result = "É necessário logar para entrar em contato conosco.";
                return View("Entrar", log);
            }

            return View(contModel);
		}


        public ActionResult Contact(ContactModel newModel)
        {
            //Do what you need to do
            return View();
        }


	}
}
