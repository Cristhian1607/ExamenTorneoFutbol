using Microsoft.AspNetCore.Mvc;
using TorneosFutbolMVC.Models;
using TorneosFutbolMVC.Services;

namespace TorneosFutbolMVC.Controllers
{
    public class TorneosController : Controller
    {
        private readonly TorneoService _torneoService;
        private readonly EquipoService _equipoService = new EquipoService();
        private readonly PartidoService _partidoService = new PartidoService();

        public TorneosController()
        {
            _torneoService = new TorneoService();
        }

        public async Task<IActionResult> Index()
        {
            var torneos = await _torneoService.ObtenerTorneosActivos();
            return View(torneos);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Torneo torneo)
        {
            if (ModelState.IsValid)
            {
                await _torneoService.CrearTorneo(torneo);
                TempData["Exito"] = "Torneo creado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(torneo);
        }

        [HttpPost]
        public async Task<IActionResult> Desactivar(int id)
        {
            try
            {
                await _torneoService.DesactivarTorneo(id);
                TempData["Exito"] = "Torneo desactivado.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message; // Da error si tiene equipos
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detalles(int id)
        {
            var torneo = await _torneoService.ObtenerTorneoPorId(id);
            if (torneo == null)
            {
                return NotFound();
            }

            var equipos = await _equipoService.ObtenerEquiposPorTorneo(id);
            var partidos = await _partidoService.ObtenerPartidosPorTorneo(id);

            var viewModel = new TorneosFutbolMVC.ViewModels.TorneoDetalleViewModel
            {
                Torneo = torneo,
                Equipos = equipos,
                Partidos = partidos
            };

            return View(viewModel);
        }
    }
}