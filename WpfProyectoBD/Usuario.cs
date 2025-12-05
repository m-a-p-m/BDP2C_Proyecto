using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfProyectoBD
{
    public class Usuario
    {
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Contrasena { get; set; }

        public Usuario(string nombre, string email, string contrasena)
        {
            Nombre = nombre;
            Email = email;
            Contrasena = contrasena;
        }
    }
}