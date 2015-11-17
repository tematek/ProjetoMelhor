using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Achados_e_Perdidos_Prototipo.Models
{
    public class ContactModel
    {
        //[Display(Name = "Nome")]
        //public string UsarName { get; set; }

        //[Display(Name = "Email")]
       
        //[DataType(DataType.EmailAddress, ErrorMessage = "Conteúdo do email inválido")]
        //public string Email { get; set; }

        //[Display(Name = "Assunto")]
      
        //public string Subject { get; set; }

        //[Display(Name = "Mensagem")]
        //[DataType(DataType.MultilineText)]
        //public string Message { get; set; }

        public string Nome { get; set; }

        public string Assunto { get; set; }

        public string Email { get; set; }

        public string Msg { get; set; }

        public string Resultado;
    }
}