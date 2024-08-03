using TVMazeAPI.Interfaces;

namespace TVMazeAPI.Services
{
    public class ShowDetailsService : IShowDetailsService
    {
        private List<Show> Shows = new();

        private readonly HttpClient _httpClient = new();
    
        public async Task<List<Show>> GetShowsWithCastDetails()
        {
            try
            {
                if (Shows.Count > 0)
                {
                    return Shows;
                }
                else
                {
                    var showsResponse = await _httpClient.GetAsync("https://api.tvmaze.com/shows");

                    Shows = await showsResponse.Content.ReadFromJsonAsync<List<Show>>();

                    foreach (var show in Shows)
                    {
                        var castResponse = await _httpClient.GetAsync("https://api.tvmaze.com/shows/" + show.Id + "/cast");
                        var castList = await castResponse.Content.ReadFromJsonAsync<List<Cast>>();
                        show.Cast.AddRange(castList.Select(x => x.Person).OrderByDescending(x => x.Birthday));
                    }                    
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Shows;
        }
    }
}

