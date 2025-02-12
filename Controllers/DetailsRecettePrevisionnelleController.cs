using LimsDepartementService.Models;
using LimsDepartementService.Service;
using Microsoft.AspNetCore.Mvc;

namespace LimsDepartementService.Controllers;

[ApiController]
[Route("/api/details/recette/previsionnelle")]
public class DetailsRecettePrevisionnelleController : Controller
{
    private readonly IDetailsRecettePrevisionnelleService _detailsRecettePrevisionnelleService;
    public DetailsRecettePrevisionnelleController(IDetailsRecettePrevisionnelleService detailsRecettePrevisionnelleService)
    {
        _detailsRecettePrevisionnelleService = detailsRecettePrevisionnelleService;
    }

    [HttpPost]
    public async Task<ActionResult<DetailsRecettePrevisionnelle>> CreateDetailsRecettePrevisionnelle(DetailsRecettePrevisionnelle detailsRecettePrevisionnelle)
    {
        DetailsRecettePrevisionnelle detailsRecettePrevisionnelleCreated = await _detailsRecettePrevisionnelleService.CreateDetailsRecettePrevisionnelle(detailsRecettePrevisionnelle);
        return NoContent();
    }
}