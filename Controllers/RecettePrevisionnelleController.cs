using Microsoft.AspNetCore.Mvc;
using LimsDepartementService.Models;
using LimsDepartementService.Service;
using LimsUtils.Api; // Library from ./lib/*.dll
using System.Text.Json;

namespace LimsDepartementService.Controllers;

[ApiController]
[Route("/api/recette/previsionnelle")]
public class RecettePrevisionelleController : Controller
{
    private readonly IRecettePrevisionnelleService _recettePrevisionnelleService;
    public RecettePrevisionelleController(IRecettePrevisionnelleService recettePrevisionnelleService)
    {
        _recettePrevisionnelleService = recettePrevisionnelleService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetRecettePrevisionnelle(int id)
    {
        RecettePrevisionnelle recettePrevisionnelle = await _recettePrevisionnelleService.GetRecettePrevisionnelle(id);
        if (recettePrevisionnelle == null) return NotFound();
        return Ok(new ApiResponse
        {
            Data = recettePrevisionnelle,
            ViewBag = null,
            IsSuccess = true,
            Message = "Data retrieved successfully.",
            StatusCode = 200
        });
    }

    [HttpPost]
    public async Task<ActionResult> CreateRecettePrevisionnelle(RecettePrevisionnelle recettePrevisionnelle)
    {
        RecettePrevisionnelle recettePrevisionnelleCreated = await _recettePrevisionnelleService.CreateRecettePrevisionnelle(recettePrevisionnelle);
        return CreatedAtAction(nameof(GetRecettePrevisionnelle), new { id = recettePrevisionnelleCreated.IdRecettePrevisionnelle}, new ApiResponse
        {
            Data = recettePrevisionnelleCreated,
            ViewBag = null,
            IsSuccess = true,
            Message = "Created successfully",
            StatusCode = 201
        });
    }
}