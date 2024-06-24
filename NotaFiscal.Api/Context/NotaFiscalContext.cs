using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace NotaFiscal.Api.Context
{
    public class NotaFiscalContext : DbContext
    {
        public NotaFiscalContext(DbContextOptions<NotaFiscalContext> options) : base (options)
        {

        }
        public DbSet<Nota> NotasFiscais { get; set; }
    }
}