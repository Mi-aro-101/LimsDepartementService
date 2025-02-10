using Microsoft.AspNetCore.Mvc;
using LimsDepartementService.Models;
using LimsDepartementService.Service;
using LimsUtils.Api; // Library from ./lib/*.dll
using System.Text.Json;

namespace LimsDepartementService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartementController : Controller
{
    private readonly IDepartementService _departementService;

    public DepartementController(IDepartementService departementService)
    {
      _departementService = departementService;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse>> Create([Bind("Code,Designation")] Departement departement)
    {
        Console.WriteLine(JsonSerializer.Serialize(departement));
        departement = await _departementService.CreateDepartement(departement);
        return CreatedAtAction(nameof(GetDepartementDetails), new { id = departement.IdDepartement}, new ApiResponse
        {
            Data = departement,
            ViewBag = null,
            IsSuccess = true,
            Message = "Created successfully",
            StatusCode = 201
        });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse>> Edit(int id, [Bind("IdDepartement,Code,Designation")] Departement departement)
    {
        await _departementService.UpdateDepartement(id, departement);
        return CreatedAtAction(nameof(GetDepartementDetails), new { id = departement.IdDepartement }, new ApiResponse
        {
            Data = _departementService.GetDepartement(departement.IdDepartement),
            ViewBag = null,
            IsSuccess = true,
            Message = "Created successfully",
            StatusCode = 201
        });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _departementService.DeleteDepartement(id);
        return NoContent();
    }

    [HttpGet]
    [Route("/api/departement/all")]
    public async Task<ActionResult> GetAllDepartements()
    {
        List<Departement> departements = await _departementService.GetAllDepartements();
        return Ok(new ApiResponse
        {
            Data = departements,
            ViewBag = null,
            IsSuccess = true,
            Message = "Datas retrieved successfully.",
            StatusCode = 200
        });
    }

    [HttpGet]
    public async Task<ActionResult> GetDepartements(int position, int pageSize)
    {
        Dictionary<string, object> response = new Dictionary<string, object>();
        int nbrPerPage = pageSize;
        response["nbrPerPage"] = nbrPerPage;
        response["TotalCount"] = _departementService.CountDepartement();
        response["nbrLinks"] = Math.Ceiling((double)_departementService.CountDepartement() / nbrPerPage);

            response["position"] = position;
            int skiped = (position-1) * pageSize;
            List<Departement> departements = await _departementService.GetDepartementsFrom(skiped, pageSize);
            return Ok(new ApiResponse
            {
                Data = departements,
                ViewBag = response,
                IsSuccess = true,
                Message = "Datas retrieved successfully.",
                StatusCode = 200
            });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse>> GetDepartementDetails(int id)
    {
        Departement departement = await _departementService.GetDepartement(id);
        if(departement == null) return NotFound();
        return Ok( new ApiResponse
        {
            Data = departement,
            ViewBag = null,
            IsSuccess = true,
            Message = "Data retrieved successfully.",
            StatusCode = 200
        });
    }
}
