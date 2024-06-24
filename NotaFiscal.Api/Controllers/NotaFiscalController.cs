using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using NotaFiscal.Api.Context;
using NotaFiscal.Api.Models;

namespace NotaFiscal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotaFiscalController : ControllerBase
    {
        private readonly NotaFiscalContext _context;
        public NotaFiscalController(NotaFiscalContext context)
        {
            _context = context;
        }

        // endpoints

        [HttpPost]
        public IActionResult Create(NotaBase nf)
        {
            Nota notaFiscal = new() {
                NumeroNf = nf.Numero,
                ValorTotal = nf.Valor,
                DataNf = DateTime.Now,
                CnpjEmissorNf = nf.CnpjEmissor,
                CnpjDestinatarioNf = nf.CnpjDestinatario
            };

            _context.Add(notaFiscal);
            _context.SaveChanges();

            return Ok(BuscarPorId(notaFiscal.NotaFiscalId));
        }

        [HttpGet("/buscarid/{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var nf = _context.NotasFiscais.Find(id);
            if (nf == null)
                return NotFound();
            
            return Ok(nf);
        }

        [HttpPut("editar/{id}")]
        public IActionResult Editar(int id, [FromBody] NotaBaseEdit nota)
        {
            var nf = _context.NotasFiscais.Find(id);

            if (nf == null)
                return NotFound("Nota fiscal inválida ou inexistente.");
            
            if (!string.IsNullOrEmpty(nota.Numero))
                nf.NumeroNf = nota.Numero;
            
            if (nota.Valor != null)
                nf.ValorTotal = nota.Valor.Value;

             if (nota.Data.HasValue && nota.Data != DateTime.MinValue)
                nf.DataNf = nota.Data.Value;

            if (!string.IsNullOrEmpty(nota.CnpjEmissor))
                nf.CnpjEmissorNf = nota.CnpjEmissor;

            if (!string.IsNullOrEmpty(nota.CnpjDestinatario))
                nf.CnpjDestinatarioNf = nota.CnpjDestinatario;

            _context.Update(nf);
            _context.SaveChanges();
            
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            Nota nf = _context.NotasFiscais.Find(id);
            if (nf == null)
                return NotFound();

            string num = nf.NumeroNf;
            
            _context.Remove(nf);
            _context.SaveChanges();
            return Ok($"A nota fiscal de número {num} foi deletada.");
        }
    }
}