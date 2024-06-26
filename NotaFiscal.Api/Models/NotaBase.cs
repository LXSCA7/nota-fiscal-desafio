using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotaFiscal.Api.Models
{
    public class NotaBase
    {
        public string Numero { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public string CnpjEmissor { get; set; }
        public string CnpjDestinatario { get; set; }
    }
}