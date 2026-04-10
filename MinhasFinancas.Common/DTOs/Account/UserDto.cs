using System;
using System.Collections.Generic;
using System.Text;

namespace MinhasFinancas.Common.DTOs.Account
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public List<string> Roles { get; set; }
    }
}
