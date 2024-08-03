using Microsoft.AspNetCore.Mvc;
using TVMazeAPI.Interfaces;

namespace TVMazeAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TVMazeAPIController : ControllerBase
{
    private readonly IShowDetailsService _showDetailsService;

    public TVMazeAPIController(IShowDetailsService showDetailsService)
    {
        _showDetailsService = showDetailsService;
    }

    [HttpGet]
    [Route("GetTVShowsWithCasts")]
    public async Task<IActionResult> GetTVShowsWithCasts(int pageNumber, int numberOfObjectsPerPage)
    {
        try
        {
            var showsList = await _showDetailsService.GetShowsWithCastDetails();

            var paginatedShowsList = showsList
                         .Skip(numberOfObjectsPerPage * (pageNumber - 1))
                         .Take(numberOfObjectsPerPage);


            if (numberOfObjectsPerPage > showsList.Count)
            {
                numberOfObjectsPerPage = showsList.Count;
                paginatedShowsList = showsList;
            }
               
            return Ok(paginatedShowsList);
            
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}



