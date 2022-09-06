using System;
using System.Collections.Generic;

namespace WSVentas.Models
{
    public partial class Usuario
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
