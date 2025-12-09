using System;

namespace WpfProyectoBD
{
    public class Servicio
    {
        public string NomServ { get; set; }
        public double PrecServ { get; set; }
        public string CatServ { get; set; }
        public string HoraServ { get; set; }
        public string FechaServ { get; set; }

        public Servicio()
        {
            NomServ = "NO DEFINIDO";
            PrecServ = 0;
            CatServ = "NO DEFINIDO";
            HoraServ = "00:00-00:00";
            FechaServ = "0/0/00";
        }

        public Servicio(string nom, double prc, string cat, string hora, string fecha)
        {
            NomServ = nom;
            PrecServ = prc;
            CatServ = cat;
            HoraServ = hora;
            FechaServ = fecha;
        }

        public string verDatos()
        {
            return $"{NomServ} | {PrecServ} | {CatServ} | {HoraServ} | {FechaServ}";
        }
    }
}