using System;

namespace WpfProyectoBD
{
    public class Eventos : Servicio
    {
        //CONSTRUCTORES
        public Eventos() : base()
        {
            CatServ = "Evento";
        }

        public Eventos(string nom, double prc, string hora, string fecha, string v)
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