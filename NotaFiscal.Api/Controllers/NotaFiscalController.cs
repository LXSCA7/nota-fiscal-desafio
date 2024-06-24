using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using NotaFiscal.Api.Context;

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
        public IActionResult Create(Nota notaFiscal)
        {
            _context.Add(notaFiscal);
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet("/buscarid/{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var nf = _context.NotasFiscais.Where(nf => nf.NotaFiscalId == id);
            if (nf == null)
                return NotFound();
            
            return Ok(nf);
        }
    }
}