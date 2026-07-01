using Microsoft.AspNetCore.Mvc;
using TorneosFutbolMVC.Models;
using TorneosFutbolMVC.Services;

namespace TorneosFutbolMVC.Controllers
{
    public class PartidosController : Controller
    {
        private readonly PartidoService _partidoService = new PartidoService();
        private readonly EquipoService _equipoService = new EquipoService();

        //Formulario para programar el partido
        public async Task<IActionResult> Crear(int torneoId)
        {
            // Pasamos los equipos a la vista 
            ViewBag.Equipos = await _equipoService.ObtenerEquiposPorTorneo(torneoId);
            var partido = new Partido { TorneoId = torneoId, FechaPartido = DateTime.Now };
            return View(partido);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Partido partido)
        {
            if (partido.EquipoLocalId == partido.EquipoVisitanteId)
            {
                ModelState.AddModelError("", "Un equipo no puede jugar contra sí mismo.");
            }

            if (ModelState.IsValid)
            {
                await _partidoService.CrearPartido(partido);
                TempData["Exito"] = "Partido programado correctamente.";
                return RedirectToAction("Detalles", "Torneos", new { id = partido.TorneoId });
            }

            ViewBag.Equipos = await _equipoService.ObtenerEquiposPorTorneo(partido.TorneoId);
            return View(partido);
        }

        //Formulario para ingresar el resultado
        public IActionResult Resultado(int id)
        {
            ViewBag.PartidoId = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Resultado(int partidoId, int golesLocal, int golesVisitante, int torneoId)
        {
            try
            {
                await _partidoService.RegistrarResultado(partidoId, golesLocal, golesVisitante);
                TempData["Exito"] = "Resultado registrado exitosamente.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message; //Da error si ya fue jugado
            }
            return RedirectToAction("Detalles", "Torneos", new { id = torneoId });
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id, int torneoId)
        {
            try
            {
                await _partidoService.EliminarPartido(id);
                TempData["Exito"] = "Partido cancelado.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message; // Da error si ya fue jugado
            }
            return RedirectToAction("Detalles", "Torneos", new { id = torneoId });
        }
    }
}