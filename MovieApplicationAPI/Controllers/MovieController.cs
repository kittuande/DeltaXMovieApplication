using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApplicationAPI.Models;
using MovieApplicationAPI.MovieData;

namespace MovieApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        IMovieData _movieData;
        public MovieController(IMovieData movieData)
        {
            _movieData = movieData;
        }
        [Route("[Action]")]
        [HttpGet]
        public IActionResult GetMovies()
        {
            return Ok(_movieData.FetchMovieDetails());
        }
        [Route("[Action]")]
        [HttpPost]
        public IActionResult AddMovieDetails(MovieDetails movieDetails)
        {
            _movieData.AddMovieDetails(movieDetails);
            return Ok(movieDetails);
        }

    }
}
