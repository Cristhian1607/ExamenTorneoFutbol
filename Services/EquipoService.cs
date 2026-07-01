
using TorneosFutbolMVC.Models;
using static Supabase.Postgrest.Constants;

namespace TorneosFutbolMVC.Services
{
    public class EquipoService
    {
        public async Task EliminarEquipo(int id)
        {
            var client = await SupabClient.GetClient();

            // Regla de no eliminar si participo en un partido
            var partidosLocal = await client.From<Partido>().Filter("equipo_local_id", Operator.Equals, id).Get();
            var partidosVisitante = await client.From<Partido>().Filter("equipo_visitante_id", Operator.Equals, id).Get();

            if (partidosLocal.Models.Any() || partidosVisitante.Models.Any())
            {
                throw new Exception("No se puede eliminar un equipo que ya está registrado en un partido.");
            }

            await client.From<Equipo>().Filter("id", Operator.Equals, id).Delete();
        }
        public async Task<List<Equipo>> ObtenerEquiposPorTorneo(int torneoId)
        {
            var client = await SupabClient.GetClient();
            var response = await client.From<Equipo>().Filter("torneo_id", Operator.Equals, torneoId).Get();
            return response.Models;
        }
        public async Task<Equipo> ObtenerEquipoPorId(int id)
        {
            var client = await SupabClient.GetClient();
            var response = await client.From<Equipo>().Filter("id", Operator.Equals, id).Get();
            return response.Models.FirstOrDefault();
        }

        public async Task CrearEquipo(Equipo equipo)
        {
            var client = await SupabClient.GetClient();
            await client.From<Equipo>().Insert(equipo);
        }

        public async Task ActualizarEquipo(Equipo equipo)
        {
            var client = await SupabClient.GetClient();
            await client.From<Equipo>().Update(equipo);
        }
    }
}