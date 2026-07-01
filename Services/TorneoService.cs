
using TorneosFutbolMVC.Models;
using static Supabase.Postgrest.Constants;

namespace TorneosFutbolMVC.Services
{
    public class TorneoService
    {
        public async Task<List<Torneo>> ObtenerTorneosActivos()
        {
            var client = await SupabClient.GetClient();
            var response = await client.From<Torneo>().Filter("activo", Operator.Equals, "true").Get();
            return response.Models;
        }

        public async Task CrearTorneo(Torneo torneo)
        {
            var client = await SupabClient.GetClient();
            await client.From<Torneo>().Insert(torneo);
        }

        public async Task DesactivarTorneo(int id)
        {
            var client = await SupabClient.GetClient();

            // Regla de no desactivar si tiene equipos
            var equiposResponse = await client.From<Equipo>().Filter("torneo_id", Operator.Equals, id).Get();
            if (equiposResponse.Models.Any())
            {
                throw new Exception("No se puede desactivar un torneo que tiene equipos inscritos.");
            }

            var torneo = (await client.From<Torneo>().Filter("id", Operator.Equals, id).Get()).Models.FirstOrDefault();
            if (torneo != null)
            {
                torneo.Activo = false;
                await client.From<Torneo>().Update(torneo);
            }
        }
        public async Task<Torneo> ObtenerTorneoPorId(int id)
        {
            var client = await SupabClient.GetClient();
            var response = await client.From<Torneo>().Filter("id", Operator.Equals, id).Get();
            return response.Models.FirstOrDefault();
        }
    }
}