
using TorneosFutbolMVC.Models;
using static Supabase.Postgrest.Constants;

namespace TorneosFutbolMVC.Services
{
    public class PartidoService
    {
        public async Task RegistrarResultado(int partidoId, int golesLocal, int golesVisitante)
        {
            var client = await SupabClient.GetClient();
            var partido = (await client.From<Partido>().Filter("id", Operator.Equals, partidoId).Get()).Models.FirstOrDefault();

            if (partido == null) throw new Exception("Partido no encontrado.");

            // Regla de no modificar si ya fue jugado
            if (partido.Jugado) throw new Exception("El partido ya fue jugado y no se puede modificar.");

            partido.GolesLocal = golesLocal;
            partido.GolesVisitante = golesVisitante;
            partido.Jugado = true;

            await client.From<Partido>().Update(partido);
        }

        public async Task EliminarPartido(int id)
        {
            var client = await SupabClient.GetClient();
            var partido = (await client.From<Partido>().Filter("id", Operator.Equals, id).Get()).Models.FirstOrDefault();

            if (partido != null && partido.Jugado)
            {
                throw new Exception("No se puede eliminar un partido que ya ha sido jugado.");
            }

            await client.From<Partido>().Filter("id", Operator.Equals, id).Delete();
        }
        public async Task CrearPartido(Partido partido)
        {
            var client = await SupabClient.GetClient();
            await client.From<Partido>().Insert(partido);
        }
        public async Task<List<Partido>> ObtenerPartidosPorTorneo(int torneoId)
        {
            var client = await SupabClient.GetClient();
            var response = await client.From<Partido>()
                                       .Filter("torneo_id", Operator.Equals, torneoId)
                                       .Get();
            return response.Models;
        }
    }
}