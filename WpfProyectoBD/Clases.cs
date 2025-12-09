using System;

namespace WpfProyectoBD
{
    public class Clases : Servicio
    {
        //CONSTRUCTORES
        public Clases() : base()
        {
            CatServ = "Clase";
        }
        public Clases(string nom, double prc, string hora, string fecha)
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