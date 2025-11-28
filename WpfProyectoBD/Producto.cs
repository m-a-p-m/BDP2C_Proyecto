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

        public string CatProd { get; set; }

        //CONSTRUCTORES
        public Producto()
        {
            NomProd = "NO DEFINIDO >:V";
            PrecProd = 0;
            CatProd = "NO DEFINIDO";
        }

        public Producto(string nom, double prc, string cat)
        {
            NomProd = nom;
            PrecProd = prc;
            CatProd = cat;
        }

        //FUNCIONALIDAD
        public string verDatos()
        {
            return (NomProd + "" + PrecProd.ToString() + "" + CatProd);
        }
    }
}
