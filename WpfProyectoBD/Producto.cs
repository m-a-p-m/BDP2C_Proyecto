using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfProyectoBD
{
public class Producto
    {
        //PROPIEDADES
        public string NomProd { get; set; }
        public double PrecProd { get; set; }

        //CONSTRUCTORES
        public Producto()
        {
            NomProd = "NO DEFINIDO >:V";
            PrecProd = 0;
        }

        public Producto(string nom, double prc)
        {
            NomProd = nom;
            PrecProd = prc;
        }

        //FUNCIONALIDAD
        public string verDatos()
        {
            return (NomProd + "" + PrecProd.ToString());
        }
    }
}
