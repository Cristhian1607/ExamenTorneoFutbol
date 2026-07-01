using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System.ComponentModel.DataAnnotations;
using ColumnAttribute = Supabase.Postgrest.Attributes.ColumnAttribute; 

namespace TorneosFutbolMVC.Models
{
    [Table("equipos")]
    public class Equipo : BaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }

        [Column("torneo_id")]
        public int TorneoId { get; set; }

        [Column("nombre")]
        [Required(ErrorMessage = "El nombre del equipo es obligatorio")]
        public string Nombre { get; set; }

        [Column("ciudad")]
        [Required(ErrorMessage = "La ciudad es obligatoria")]
        public string Ciudad { get; set; }
    }


}