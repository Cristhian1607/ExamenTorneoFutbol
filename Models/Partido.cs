using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System.ComponentModel.DataAnnotations;
using ColumnAttribute = Supabase.Postgrest.Attributes.ColumnAttribute; 

namespace TorneosFutbolMVC.Models
{

    [Table("partidos")]
    public class Partido : BaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }

        [Column("torneo_id")]
        public int TorneoId { get; set; }

        [Column("equipo_local_id")]
        public int EquipoLocalId { get; set; }

        [Column("equipo_visitante_id")]
        public int EquipoVisitanteId { get; set; }

        [Column("goles_local")]
        [Range(0, 100)]
        public int? GolesLocal { get; set; }

        [Column("goles_visitante")]
        [Range(0, 100)]
        public int? GolesVisitante { get; set; }

        [Column("fecha_partido")]
        [Required]
        public DateTime FechaPartido { get; set; }

        [Column("jugado")]
        public bool Jugado { get; set; } = false;
    }
}