using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Achados_e_Perdidos_Prototipo
{
	public class Bluemix
	{

		public string RetornaRespostaFaq(string topclass)
		{
			string RespostaFaq = "";

			switch (topclass)
			{
				case "doacao":
					RespostaFaq = "Para doar entre no link Doação";
					break;
				case "senha":
					RespostaFaq = "faça uma senha forte";
					break;
				case "leipena":
					RespostaFaq = "não é lei maria da penha";
					break;
				case "encontrei":
					RespostaFaq = "eu não sou o senhor do tempo";
					break;

				default:
					RespostaFaq = "Desculpe, não entedi sua dúvida.";
					break;


			}

			return RespostaFaq;
		}

	}
}