using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Nota
    {
        public int NotaFiscalId { get; set; }
        public string NumeroNf { get; set; }
        public decimal ValorTotal { get; set; }
        public string CnpjEmissorNf { get; set; }
        public string CnpjDestinatarioNf { get; set; }
    }
}