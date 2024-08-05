using TVMazeAPI.Interfaces;

namespace TVMazeAPI.Services
{
    public class ShowDetailsService : IShowDetailsService
    {
        private List<Show> _shows = new();

        private DateTime _scheduler = DateTime.Now;

        private readonly HttpClient _httpClient = new();
    
        public async Task<List<Show>> GetShowsWithCastDetails()
        {
            try
            {
                if (_shows.Count > 0 && _scheduler.AddMinutes(5) >= DateTime.Now )
                {
                    return _shows;
                }
                else
                {
                    _scheduler = DateTime.Now;

                    var showsResponse = await _httpClient.GetAsync("https://api.tvmaze.com/shows");

                    _shows = await showsResponse.Content.ReadFromJsonAsync<List<Show>>();

                    foreach (var show in _shows)
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

            return _shows;
        }
    }
}

