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
                Actor? actor=FindActor(_actor);
                if (actor == null)
                {
                    actor = new Actor() { ActorName = _actor };
                    deltaXMovieApplicationContext.Actors.Add(actor);
                    deltaXMovieApplicationContext.SaveChanges();
                }
                MapMovieActor(movie.MovieId, actor.ActorId);
                //MovieActorRelationship movieActorRelationship = new MovieActorRelationship()
                //{
                //    MovieId=movie.MovieId,
                //    ActorId = actor.ActorId,
                //};
                //deltaXMovieApplicationContext.MovieActorRelationships.Add(movieActorRelationship);
                //deltaXMovieApplicationContext.SaveChanges();
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
                        String? actorName = deltaXMovieApplicationContext.Actors.Find(actorid).ActorName;
                        actorsList.Add(actorName);
                    }

                }
                m.Actors = actorsList;
                moviesDetails.Add(m);

            }
            return moviesDetails;
        }

        public void UpdateMovieDetails(MovieDetails movieDetails,int id)
        {
            Movie? movie = deltaXMovieApplicationContext.Movies.Find(id);
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
                foreach (var relationship in removeRelationship)
                {
                    deltaXMovieApplicationContext.MovieActorRelationships.Remove(relationship);
                    deltaXMovieApplicationContext.SaveChanges();
                }
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
                }
            }
        }

        public void DeleteMovieDetails(int id)
        {
            Movie? movie = deltaXMovieApplicationContext.Movies.Find(id);
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
            }

        }
        public Producer? FindProducer(String name)
        {
            return deltaXMovieApplicationContext.Producers.Where(p => p.ProducerName.Equals(name)).FirstOrDefault();
        }
        public Actor? FindActor(String name)
        {
            return deltaXMovieApplicationContext.Actors.Where(p => p.ActorName.Equals(name)).FirstOrDefault();
        }
        private void MapMovieActor(int movieId,int actorId)
        {
           var movieActorRelationship= deltaXMovieApplicationContext.MovieActorRelationships.Where(p => p.MovieId.Equals(movieId)&&p.ActorId.Equals(actorId)).FirstOrDefault();
            if (movieActorRelationship == null)
            {
                movieActorRelationship = new MovieActorRelationship() { MovieId = movieId, ActorId = actorId };
                deltaXMovieApplicationContext.MovieActorRelationships.Add(movieActorRelationship);
                deltaXMovieApplicationContext.SaveChanges();
            }
        }

        
    }
}
