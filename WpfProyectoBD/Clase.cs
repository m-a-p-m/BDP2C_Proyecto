using System;

namespace WpfProyectoBD

{
    public class Clase : Servicio
    {
        //CONSTRUCTORES
        public Clase() : base()
        {
            CatServ = "Clase";
        }

        public Clase(string nom, double prc, string hora, string fecha)
            : base(nom, prc, "Clase", hora, fecha)
        {
        }

        //FUNCIONALIDAD
        public override string VerDatos()
        {
            return $"CLASE: {base.VerDatos()}";
        }
    }
}