using Microsoft.AspNetCore.Mvc;
using TorneosFutbolMVC.Models;
using TorneosFutbolMVC.Services;

namespace TorneosFutbolMVC.Controllers
{
    public class EquiposController : Controller
    {
        private readonly EquipoService _equipoService = new EquipoService();

        //Formularios de creacion
        public IActionResult Crear(int torneoId)
        {
            var equipo = new Equipo { TorneoId = torneoId };
            return View(equipo);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Equipo equipo)
        {
            if (ModelState.IsValid)
            {
                await _equipoService.CrearEquipo(equipo);
                TempData["Exito"] = "Equipo inscrito correctamente.";
                return RedirectToAction("Detalles", "Torneos", new { id = equipo.TorneoId });
            }
            return View(equipo);
        }

        // Formulario de edicion
        public async Task<IActionResult> Editar(int id)
        {
            var equipo = await _equipoService.ObtenerEquipoPorId(id);
            if (equipo == null) return NotFound();
            return View(equipo);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Equipo equipo)
        {
            if (ModelState.IsValid)
            {
                await _equipoService.ActualizarEquipo(equipo);
                TempData["Exito"] = "Equipo actualizado correctamente.";
                return RedirectToAction("Detalles", "Torneos", new { id = equipo.TorneoId });
            }
            return View(equipo);
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id, int torneoId)
        {
            try
            {
                await _equipoService.EliminarEquipo(id);
                TempData["Exito"] = "Equipo eliminado del torneo.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message; 
            }
            return RedirectToAction("Detalles", "Torneos", new { id = torneoId });
        }
    }
}