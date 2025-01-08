using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DepartementService.Models;
using DepartementService.Context;
using DepartementService.Utils;

namespace DepartementService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartementController : Controller
{
    private readonly DepartementContext _context;

    public DepartementController(DepartementContext context)
    {
      _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse>> Create([Bind("Code,Designation")] Departement departement)
    {
        Dictionary<string, object> response = new Dictionary<string, object>();
        _context.Departements.Add(departement);
        await _context.SaveChangesAsync();
        departement = await _context.Departements.OrderBy(d => d.IdDepartement).LastAsync();
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
    public async Task<ActionResult<ApiResponse>> Edit(int? id, [Bind("IdDepartement,Code,Designation")] Departement departement)
    {
        if(id == null)
        {
            return NotFound();
        }
        _context.Departements.Update(departement);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetDepartementDetails), new { id = departement.IdDepartement }, new ApiResponse
        {
            Data = await _context.Departements.FirstOrDefaultAsync(d => d.IdDepartement == departement.IdDepartement),
            ViewBag = null,
            IsSuccess = true,
            Message = "Created successfully",
            StatusCode = 201
        });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int? id)
    {
        Dictionary<string, object> response = new Dictionary<string, object>();
        if(id == null)
        {
            return NotFound();
        }
        var departement = await _context.Departements.FirstOrDefaultAsync(m => m.IdDepartement == id);
        if(departement == null)
        {
            return NotFound();
        }
        _context.Departements.Remove(departement);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult> GetDepartement(int position, int pageSize)
    {
        Dictionary<string, object> response = new Dictionary<string, object>();
        int nbrPerPage = pageSize;
        var nombre = await _context.Departements.CountAsync();
        response["nbrPerPage"] = nbrPerPage;
        response["TotalCount"] = _context.Departements.Count();
        response["nbrLinks"] = Math.Ceiling((double)_context.Departements.Count() / nbrPerPage);

            response["position"] = position;
            List<Departement> departements = await _context.Departements.Skip(((int)response["position"]-1) * nbrPerPage).Take(nbrPerPage).ToListAsync();
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
        if (id == null)
        {
            return BadRequest();
        }
        Departement? departement = await _context.Departements.FindAsync(id);
        if (departement == null)
        {
            return NotFound();
        }
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
