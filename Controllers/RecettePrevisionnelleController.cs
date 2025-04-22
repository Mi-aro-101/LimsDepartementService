using Microsoft.AspNetCore.Mvc;
using LimsDepartementService.Models;
using LimsDepartementService.Service;
using LimsUtils.Api; // Library from ./lib/*.dll
using System.Text.Json;
using LimsDepartementService.Dtos;

namespace LimsDepartementService.Controllers;

[ApiController]
[Route("/api/recette/previsionnelle")]
public class RecettePrevisionelleController : Controller
{
    private readonly IRecettePrevisionnelleService _recettePrevisionnelleService;
    private readonly IDetailsRecettePrevisionnelleService _detailsRecettePrevisionnelleService;
    public RecettePrevisionelleController(IRecettePrevisionnelleService recettePrevisionnelleService, IDetailsRecettePrevisionnelleService detailsRecettePrevisionnelleService)
    {
        _recettePrevisionnelleService = recettePrevisionnelleService;
        _detailsRecettePrevisionnelleService = detailsRecettePrevisionnelleService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetRecettePrevisionnelle(int id)
    {
        try
        {
            RecettePrevisionnelle recettePrevisionnelle = await _recettePrevisionnelleService.GetRecettePrevisionnelle(id);
            if (recettePrevisionnelle == null) return NotFound();
            return Ok(new ApiResponse
            {
                Data = recettePrevisionnelle,
                IsSuccess = true,
                Message = "Data retrieved successfully.",
                StatusCode = 200
            });
        }catch(Exception)
        {
            throw;
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateRecettePrevisionnelle(RecettePrevisionnelleDto recettePrevisionnelle)
    {
        try
        {

            RecettePrevisionnelle recettePrevisionnelleCreated = await _recettePrevisionnelleService.CreateRecettePrevisionnelle(recettePrevisionnelle);
            return CreatedAtAction(nameof(GetRecettePrevisionnelle), new { id = recettePrevisionnelleCreated.IdRecettePrevisionnelle}, new ApiResponse
            {
                Data = recettePrevisionnelleCreated,
                IsSuccess = true,
                Message = "Created successfully",
                StatusCode = 201
            });
        }catch(Exception ex)
        {
            return StatusCode(500, new ApiResponse
            {
                IsSuccess = false,
                Message = ex.Message,
                StatusCode = 500
            });
        }
    }

    [HttpGet("comparaison/prevision/realite/{annee}")]
    public async Task<ActionResult> GetComparaisonPrevisionRealite(int annee)
    {
        try
        {
            VComparaisonRecette[] recettePrevisionnelle = await _detailsRecettePrevisionnelleService.GetComparaisonRecetteRealiteDepartement(annee);
            if (recettePrevisionnelle == null) return NotFound();
            return Ok(new ApiResponse
            {
                Data = recettePrevisionnelle,
                IsSuccess = true,
                Message = "Data retrieved successfully.",
                StatusCode = 200
            });
        }catch(Exception)
        {
            throw;
        }
    }
}