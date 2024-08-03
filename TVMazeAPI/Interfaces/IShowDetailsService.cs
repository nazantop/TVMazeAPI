using System;
namespace TVMazeAPI.Interfaces
{
	public interface IShowDetailsService
	{        
        Task<List<Show>> GetShowsWithCastDetails();
    }
}

