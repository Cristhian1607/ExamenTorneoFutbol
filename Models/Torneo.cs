using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System.ComponentModel.DataAnnotations;
using ColumnAttribute = Supabase.Postgrest.Attributes.ColumnAttribute; 

namespace TorneosFutbolMVC.Models
{
    [Table("torneos")]
    public class Torneo : BaseModel
    {
        [PrimaryKey("id", false)] // false = autogenerado por la BD
        public int Id { get; set; }

        [Column("nombre")]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Column("edicion")]
        [Required]
        [Range(2000, 2100, ErrorMessage = "Ingrese un año válido")]
        public int Edicion { get; set; }

        [Column("activo")]
        public bool Activo { get; set; } = true;

        [Column("creado_en")]
        public DateTime CreadoEn { get; set; } = DateTime.UtcNow;
    }

}