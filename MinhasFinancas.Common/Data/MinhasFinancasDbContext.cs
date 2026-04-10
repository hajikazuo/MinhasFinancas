using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MinhasFinancas.Domain.Models;
using MinhasFinancas.Domain.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MinhasFinancas.Common.Data
{
    public class MinhasFinancasDbContext : IdentityDbContext<Usuario, Funcao, Guid>
    {
        public MinhasFinancasDbContext(DbContextOptions<MinhasFinancasDbContext> options): base(options)
        {
        }

        public DbSet<Transacao> Transacoes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}
