using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class Nota
    {
        [Key]
        public int NotaFiscalId { get; set; }
        public string NumeroNf { get; set; } 
        public decimal ValorTotal { get; set; }
        public DateTime DataNf { get; set; }
        public string CnpjEmissorNf { get; set; }
        public string CnpjDestinatarioNf { get; set; }
    }
}