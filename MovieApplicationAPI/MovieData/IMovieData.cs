using MovieApplicationAPI.Models;

namespace MovieApplicationAPI.MovieData
{
    
    public interface IMovieData
    {
        List<MovieDetails> FetchMovieDetails();
        Boolean AddMovieDetails(MovieDetails movieDetails);    

        Boolean UpdateMovieDetails(MovieDetails movieDetails);
        Boolean DeleteMovieDetails(String moviename);
        List<String> GetAllActors();
        List<String> GetAllProducers();
    }
}
