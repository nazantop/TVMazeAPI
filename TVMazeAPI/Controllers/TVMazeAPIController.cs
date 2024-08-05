using Microsoft.AspNetCore.Mvc;
using TVMazeAPI.Interfaces;
using TVMazeAPI.Models;

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

            var totalPage = (int)Math.Ceiling((decimal)showsList.Count / numberOfObjectsPerPage);

            if (pageNumber <= 0 || pageNumber > totalPage)
            {
                return StatusCode(400, "Page number should be greater than 0 and smaller or equal than total page number");
            }
          
            var paginatedResult = new ShowsPagination
            {
                Shows = showsList.Skip(numberOfObjectsPerPage * (pageNumber - 1)).Take(numberOfObjectsPerPage).ToList(),
                CurrentPage = pageNumber,
                PreviousPage = ((numberOfObjectsPerPage >= showsList.Count) || (pageNumber == 1)) ? null : pageNumber - 1,
                NextPage = ((numberOfObjectsPerPage >= showsList.Count) || (pageNumber == totalPage)) ? null : pageNumber + 1,
                TotalPage = totalPage,
            };
               
            return Ok(paginatedResult);
            
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}



