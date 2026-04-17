using Microsoft.AspNetCore.Identity;
using MinhasFinancas.Common.Data;
using MinhasFinancas.Common.Models;
using MinhasFinancas.Common.Models.Usuarios;
using MinhasFinancas.Common.Services.Interfaces;
using RT.Comb;

namespace MinhasFinancas.Common.Services
{
    public class SeedService : ISeedService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<Funcao> _roleManager;
        private readonly MinhasFinancasDbContext _context;
        private readonly ICombProvider _comb;

        private const string ClientRole = "Cliente";
        private const string AdminRole = "Admin";

        public SeedService(UserManager<Usuario> userManager, RoleManager<Funcao> roleManager, MinhasFinancasDbContext context, ICombProvider comb)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _comb = comb;
        }

        public void Seed()
        {
            CreateRole(ClientRole).GetAwaiter().GetResult();
            CreateRole(AdminRole).GetAwaiter().GetResult();
            CreateUser("admin@minhasfinancas.com", "Admin", "Teste@2026!", roles: new List<string> { ClientRole, AdminRole }).GetAwaiter().GetResult();
            SeedCategories().GetAwaiter().GetResult();
        }

        private async Task<IdentityResult?> CreateRole(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var role = new Funcao
                {
                    Name = roleName
                };
                return await _roleManager.CreateAsync(role);
            }
            return default;
        }

        private async Task<IdentityResult?> CreateUser(string email, string name, string password, IEnumerable<string> roles)
        {
            var request = await _userManager.FindByEmailAsync(email);
            if (request == null)
            {
                var user = new Usuario
                {
                    UserName = email,
                    Email = email,
                    NomeCompleto = name,
                    EmailConfirmed = true,
                };

                IdentityResult result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRolesAsync(user, roles);
                }

                return result;
            }
            else
            {
                return default;
            }
        }

        private async Task SeedCategories()
        {
            if (!_context.Categorias.Any())
            {
                var categories = new List<Categoria>
                {
                    new Categoria { CategoriaId = _comb.Create(), Nome = "Alimentação", UsuarioId = null },
                    new Categoria { CategoriaId = _comb.Create(), Nome = "Transporte", UsuarioId = null },
                    new Categoria { CategoriaId = _comb.Create(), Nome = "Lazer", UsuarioId = null },
                    new Categoria { CategoriaId = _comb.Create(), Nome = "Educação", UsuarioId = null },
                    new Categoria { CategoriaId = _comb.Create(), Nome = "Saúde", UsuarioId = null },
                    new Categoria { CategoriaId = _comb.Create(), Nome = "Moradia", UsuarioId = null },
                    new Categoria { CategoriaId = _comb.Create(), Nome = "Serviços", UsuarioId = null },
                    new Categoria { CategoriaId = _comb.Create(), Nome = "Compras", UsuarioId = null },
                    new Categoria { CategoriaId = _comb.Create(), Nome = "Viagem", UsuarioId = null },
                    new Categoria { CategoriaId = _comb.Create(), Nome = "Salário", UsuarioId = null },
                    new Categoria { CategoriaId = _comb.Create(), Nome = "Investimentos", UsuarioId = null },
                    new Categoria { CategoriaId = _comb.Create(), Nome = "Presentes", UsuarioId = null },
                    new Categoria { CategoriaId = _comb.Create(), Nome = "Impostos", UsuarioId = null },
                    new Categoria { CategoriaId = _comb.Create(), Nome = "Assinaturas", UsuarioId = null }
                };

                _context.Categorias.AddRange(categories);
                await _context.SaveChangesAsync();
            }
        }
    }
}
