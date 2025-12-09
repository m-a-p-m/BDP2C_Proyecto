using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfProyectoBD
{
    public class SolicitudData
    {
        //PROPIEDADES
        public string Nombre { get; set; }
        public string Solicitud { get; set; }
        //CONSTRUCTORES
        public SolicitudData()
        {
            Nombre = "No definido";
            Solicitud = "No definido";
        }
    }
}
