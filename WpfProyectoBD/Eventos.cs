using System;

namespace WpfProyectoBD
{
    public class Eventos : Servicio
    {
        //PROPIEDADES
        public Eventos() : base()
        {
            CatServ = "Evento";
        }

        public Eventos(string nom, double prc, string hora, string fecha)
            : base(nom, prc, "Evento", hora, fecha)
        {
        }
        //FUNCIONALIDAD

        public override string VerDatos()
        {
            return $"EVENTO: {base.VerDatos()}";
        }
    }
}