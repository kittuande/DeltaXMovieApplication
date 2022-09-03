using MovieApplicationAPI.Models;

namespace MovieApplicationAPI.MovieData
{
    
    public interface IMovieData
    {
        List<MovieDetails> FetchMovieDetails();
        Boolean AddMovieDetails(MovieDetails movieDetails);    

        Boolean UpdateMovieDetails(MovieDetails movieDetails,String movieName);
        Boolean DeleteMovieDetails(String movieName);
        List<String> GetAllActors();
        List<String> GetAllProducers();
    }
}
