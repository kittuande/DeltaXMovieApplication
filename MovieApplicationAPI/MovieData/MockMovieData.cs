using MovieApplicationAPI.Models;
using MovieApplicationAPI.Models.EFDataBase;

namespace MovieApplicationAPI.MovieData
{
    public class MockMovieData: IMovieData
    {
        //Declaring EF context class 
        private DeltaXMovieApplicationContext deltaXMovieApplicationContext;
        public MockMovieData(DeltaXMovieApplicationContext deltaXMovieApplicationContext)
        {
            this.deltaXMovieApplicationContext = deltaXMovieApplicationContext;
        }
        //Method to get all moviedetails from database
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
                        int actorid = i.ActorId;
                        String? actorName = deltaXMovieApplicationContext.Actors.Find(actorid).ActorName;
                        actorsList.Add(actorName);
                    }
                }
                m.Actors = actorsList;
                moviesDetails.Add(m);
            }
            return moviesDetails;
        }
        //Method to add new movie to database by taking moviedetails as input
        public Boolean AddMovieDetails(MovieDetails movieDetails)
        {
            Movie? movie = FindMovie(movieDetails.MovieName);
            if (movie == null)
            {
                Producer? producer = FindProducer(movieDetails.ProducerName);
                if (producer == null)
                {
                    producer = new Producer()
                    {
                        ProducerName = movieDetails.ProducerName
                    };
                    deltaXMovieApplicationContext.Producers.Add(producer);
                    deltaXMovieApplicationContext.SaveChanges();
                }
                int producerId = producer.ProducerId;
                movie = new Movie()
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
                    Actor? actor = FindActor(_actor);
                    if (actor == null)
                    {
                        actor = new Actor() { ActorName = _actor };
                        deltaXMovieApplicationContext.Actors.Add(actor);
                        deltaXMovieApplicationContext.SaveChanges();
                    }
                    MapMovieActor(movie.MovieId, actor.ActorId);
                }
                return true;
            }
            return false;
        }

        
        //Method to update movie details in database by taking movie details as input
        public Boolean UpdateMovieDetails(MovieDetails movieDetails)
        {
            Movie? movie = FindMovie(movieDetails.MovieName);
            if(movie != null)
            {
                movie.MovieName=movieDetails.MovieName; 
                movie.Description = movieDetails.MovieDescription;
                movie.DateOfRelease = movieDetails.ReleasedDate;
                deltaXMovieApplicationContext.SaveChanges();
                Producer? producer = FindProducer(movieDetails.ProducerName);
                if (producer == null)               
                {
                    producer = new Producer() { ProducerName = movieDetails.ProducerName };
                    deltaXMovieApplicationContext.Producers.Add(producer);
                    deltaXMovieApplicationContext.SaveChanges();                   
                }
                movie.ProducerId = producer.ProducerId;
                deltaXMovieApplicationContext.SaveChanges();
                List<MovieActorRelationship> removeRelationship = deltaXMovieApplicationContext.MovieActorRelationships.Where(a => a.MovieId == movie.MovieId).ToList();
                List<Actor> actors = new List<Actor>();
                foreach (var _actorName in movieDetails.Actors)
                {
                    Actor? actor = FindActor(_actorName);
                    if(actor == null)
                    {
                        actor = new Actor() { ActorName = _actorName };
                        deltaXMovieApplicationContext.Actors.Add(actor);
                        deltaXMovieApplicationContext.SaveChanges();                       
                    }
                    MapMovieActor(movie.MovieId, actor.ActorId);
                    actors.Add(actor);
                }
                foreach (var relationship in removeRelationship)
                {
                    var flag = actors.Where(a => a.ActorId == relationship.ActorId).FirstOrDefault();
                    if (flag == null)
                    {
                        deltaXMovieApplicationContext.MovieActorRelationships.Remove(relationship);
                        deltaXMovieApplicationContext.SaveChanges();
                    }
                }
                return true;
            }
            return false;
        }

        //Delete movie details from database using movie id
        public Boolean DeleteMovieDetails(String movieName)
        {
            Movie? movie = deltaXMovieApplicationContext.Movies.Where(m=>m.MovieName==movieName).FirstOrDefault();
            if (movie != null)
            {
                List<MovieActorRelationship> removeRelationship = deltaXMovieApplicationContext.MovieActorRelationships.Where(a => a.MovieId == movie.MovieId).ToList();
                foreach (var relationship in removeRelationship)
                {
                    deltaXMovieApplicationContext.MovieActorRelationships.Remove(relationship);
                    deltaXMovieApplicationContext.SaveChanges();
                }
                deltaXMovieApplicationContext.Remove(movie);
                deltaXMovieApplicationContext.SaveChanges();
                return true;
            }
            return false;
        }

        //Updates replationship between movie and actors. Accepts movieId and actorId as inputs.
        private void MapMovieActor(int movieId, int actorId)
        {
            var movieActorRelationship = deltaXMovieApplicationContext.MovieActorRelationships.Where(p => p.MovieId.Equals(movieId) && p.ActorId.Equals(actorId)).FirstOrDefault();
            if (movieActorRelationship == null)
            {
                movieActorRelationship = new MovieActorRelationship() { MovieId = movieId, ActorId = actorId };
                deltaXMovieApplicationContext.MovieActorRelationships.Add(movieActorRelationship);
                deltaXMovieApplicationContext.SaveChanges();
            }
        }

        //Finds producer by comparing string
        public Producer? FindProducer(String producerName)
        {
            return deltaXMovieApplicationContext.Producers.Where(p => p.ProducerName.Equals(producerName)).FirstOrDefault();
        }
        //Finds actor by comparing string
        public Actor? FindActor(String actorName)
        {
            return deltaXMovieApplicationContext.Actors.Where(p => p.ActorName.Equals(actorName)).FirstOrDefault();
        }
        //Finds movie by comparing string
        public Movie? FindMovie(String movieName)
        {
            return deltaXMovieApplicationContext.Movies.Where(p => p.MovieName.Equals(movieName)).FirstOrDefault();
        }
        //Returns all actornames
        public List<String> GetAllActors()
        {
            List<String> actors = new List<String>();
            foreach (var actor in deltaXMovieApplicationContext.Actors)
            {
                actors.Add(actor.ActorName);
            }
            return actors;
        }
        //Returns all producernames
        public List<String> GetAllProducers()
        {
            List<String> producers = new List<String>();
            foreach (var producer in deltaXMovieApplicationContext.Producers)
            {
                producers.Add(producer.ProducerName);
            }
            return producers;
        }       
    }
}
