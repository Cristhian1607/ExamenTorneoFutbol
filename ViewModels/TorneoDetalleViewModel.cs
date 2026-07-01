using TorneosFutbolMVC.Models;

namespace TorneosFutbolMVC.ViewModels
{
    public class TorneoDetalleViewModel
    {
        public Torneo Torneo { get; set; }
        public List<Equipo> Equipos { get; set; }
        public List<Partido> Partidos { get; set; }
    }
}