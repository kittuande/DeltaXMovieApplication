using MovieApplicationAPI.Models;
using MovieApplicationAPI.Models.EFDataBase;

namespace MovieApplicationAPI.MovieData
{
    public class MockMovieData: IMovieData
    {
        private DeltaXMovieApplicationContext deltaXMovieApplicationContext;
        public MockMovieData(DeltaXMovieApplicationContext deltaXMovieApplicationContext)
        {
            this.deltaXMovieApplicationContext = deltaXMovieApplicationContext;
        }

        public MovieDetails AddMovieDetails(MovieDetails movieDetails)
        {
            Producer producer = new Producer()
            {
                ProducerName = movieDetails.ProducerName
            };
            deltaXMovieApplicationContext.Producers.Add(producer);
            deltaXMovieApplicationContext.SaveChanges();
            int producerId = producer.ProducerId;
            Movie movie = new Movie()
            {
                MovieName = movieDetails.MovieName,
                DateOfRelease = movieDetails.ReleasedDate,
                Description = movieDetails.MovieDescription,
                ProducerId = producerId,

            };
            deltaXMovieApplicationContext.Movies.Add(movie);
            deltaXMovieApplicationContext.SaveChanges();
            foreach (var _actor in movieDetails.Actors)
            {
                Actor actor = new Actor() { ActorName = _actor };
                deltaXMovieApplicationContext.Actors.Add(actor);
                deltaXMovieApplicationContext.SaveChanges();
                MovieActorRelationship movieActorRelationship = new MovieActorRelationship()
                {
                    MovieId=movie.MovieId,
                    ActorId = actor.ActorId,
                };
                deltaXMovieApplicationContext.MovieActorRelationships.Add(movieActorRelationship);
                deltaXMovieApplicationContext.SaveChanges();
            }
            return movieDetails;
        }

        public List<MovieDetails> FetchMovieDetails()
        {
            List<Movie> movies = deltaXMovieApplicationContext.Movies.ToList();
            List<MovieActorRelationship> movieActorRelationship = deltaXMovieApplicationContext.MovieActorRelationships.ToList();
            List<MovieDetails> moviesDetails = new List<MovieDetails>();
            foreach (var movie in movies)
            {
                MovieDetails m = new MovieDetails();
                m.MovieName = movie.MovieName;
                m.ReleasedDate = movie.DateOfRelease;
                m.ProducerName = deltaXMovieApplicationContext.Producers.Find(movie.ProducerId).ProducerName;
                m.MovieDescription = movie.Description;
                List<String> actorsList = new List<String>();
                foreach (var i in movieActorRelationship)
                {

                    if (i.MovieId == movie.MovieId)
                    {
                        
                        int actorid= i.ActorId;
                        String actorName = deltaXMovieApplicationContext.Actors.Find(actorid).ActorName;
                        actorsList.Add(actorName);
                    }

                }
                m.Actors = actorsList;
                moviesDetails.Add(m);

            }
            return moviesDetails;
        }
    }
}
