using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
            if(!Cnpj.VerificaEFormata(nf.CnpjDestinatario, out string cnpjDestinatarioFormatado))
                return BadRequest("Cnpj do destinatário inválido.");
                
            if(!Cnpj.VerificaEFormata(nf.CnpjEmissor, out string cnpjEmissorFormatado))
                return BadRequest("Cnpj do emissor inválido.");

            if (cnpjDestinatarioFormatado == cnpjEmissorFormatado)
                return BadRequest("Cpnj do emissor e destinatário não podem ser iguais.");

            if (nf.Data > DateTime.UtcNow)
                return BadRequest($"Data da nota fiscal não pode ser depois de {DateTime.Now}.");

            if (nf.Data == DateTime.MinValue)
                return BadRequest("Data da nota fiscal inválida.");

            Nota notaFiscal = new() {
                NumeroNf = nf.Numero,
                ValorTotal = nf.Valor,
                DataNf = nf.Data,
                CnpjEmissorNf = cnpjEmissorFormatado,
                CnpjDestinatarioNf = cnpjDestinatarioFormatado
            };

            _context.Add(notaFiscal);
            _context.SaveChanges();

            return Ok(BuscarPorId(notaFiscal.NotaFiscalId));
        }

        [HttpGet("buscarid/{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var nf = _context.NotasFiscais.Find(id);
            if (nf == null)
                return NotFound();
            
            return Ok(nf);
        }

        [HttpGet("listarNotas")]
        public IActionResult Listar()
        {
            List<Nota> notas = _context.NotasFiscais.ToList();

            return Ok(notas);
        }

        [HttpGet("listarPorDestinatario/{cnpj}")]
        public IActionResult ListarPorDestinatario(string cnpj)
        {
            if (cnpj.Any(c => !char.IsNumber(c)))
            {
                cnpj = Cnpj.RemoveDigitos(cnpj);
                cnpj = Cnpj.FormataCnpj(cnpj);
            }

            if (cnpj.All(c => char.IsNumber(c)))
                cnpj = Cnpj.FormataCnpj(cnpj);

            
            List<Nota> notas = _context.NotasFiscais.Where(nf => nf.CnpjDestinatarioNf == cnpj).ToList();

            return Ok(notas);
        }

        [HttpGet("listarPorEmissor/{cnpj}")]
        public IActionResult ListarPorEmissor(string cnpj)
        {
            if (cnpj.Any(c => !char.IsNumber(c)))
            {
                cnpj = Cnpj.RemoveDigitos(cnpj);
                cnpj = Cnpj.FormataCnpj(cnpj);
            }
            
            if (cnpj.All(c => char.IsNumber(c)))
                cnpj = Cnpj.FormataCnpj(cnpj);

            List<Nota> notas = _context.NotasFiscais.Where(c => c.CnpjEmissorNf == cnpj).ToList();
            
            return Ok(notas);
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