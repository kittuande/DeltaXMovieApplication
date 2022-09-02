using MovieApplicationAPI.Models;

namespace MovieApplicationAPI.MovieData
{
    public interface IMovieData
    {
        List<MovieDetails> FetchMovieDetails();

        Boolean AddMovieDetails(MovieDetails movieDetails);    

        Boolean UpdateMovieDetails(MovieDetails movieDetails);
        void DeleteMovieDetails(int id);
    }
}
