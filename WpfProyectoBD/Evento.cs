using System;

namespace WpfProyectoBD
{
    public class Evento : Servicio
    {
        //CONSTRUCTORES
        public Evento() : base()
        {
            CatServ = "Evento";
        }

        public Evento(string nom, double prc, string hora, string fecha, string v)
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