﻿using MovieApplicationAPI.Models;

namespace MovieApplicationAPI.MovieData
{
    public interface IMovieData
    {
        List<MovieDetails> FetchMovieDetails();

        MovieDetails AddMovieDetails(MovieDetails movieDetails);    
    }
}
