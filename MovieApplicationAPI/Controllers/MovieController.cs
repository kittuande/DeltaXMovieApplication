using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApplicationAPI.Models;
using MovieApplicationAPI.MovieData;

namespace MovieApplicationAPI.Controllers
{
    //controller for MovieApplication API
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        
        private readonly IMovieData _movieData;
        public MovieController(IMovieData movieData)
        {
            _movieData = movieData;
        }
        //method used to fetch movie details of all movies
        [Route("[Action]")]
        [HttpGet]
        public IActionResult GetMovies()
        {
            return Ok(_movieData.FetchMovieDetails());
        }
        //mothod used to add movie details
        [Route("[Action]")]
        [HttpPost]
        public IActionResult AddMovieDetails(MovieDetails movieDetails)
        {
            Boolean flag=_movieData.AddMovieDetails(movieDetails);
            if(flag)
                return Ok(movieDetails);
            return BadRequest("Movie with this name already exists");
        }
        //method to edit movie details
        [Route("[Action]/{movieName}")]
        [HttpPut]
        public IActionResult EditMovieDetails(MovieDetails movieDetails,String movieName)
        {
            Boolean flag=_movieData.UpdateMovieDetails(movieDetails,movieName);
            if(flag)
                return Ok(movieDetails);
            return BadRequest("Movie with this name does not exist");
        }
        //Method to delete movie permanently
        [Route("[Action]/{movieName}")]
        [HttpDelete]
        public IActionResult DeleteMovie(String movieName)
        {
            Boolean flag= _movieData.DeleteMovieDetails(movieName);  
            if(flag)
                return Ok("Movie Deleted");
            return BadRequest("Moviename does not exist");
        }
        //Returns all actornames from database
        [Route("[Action]")]
        [HttpGet]
        public IActionResult GetActors()
        {
            return Ok(_movieData.GetAllActors());
        }
        //Returns all producernames from database
        [Route("[Action]")]
        [HttpGet]
        public IActionResult GetProducers()
        {
            return Ok(_movieData.GetAllProducers());
        }
    }
}
