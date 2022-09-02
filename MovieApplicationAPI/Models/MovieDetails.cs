namespace MovieApplicationAPI.Models
{
    //A custom movie model class which has all required details
    public class MovieDetails
    {
        public String MovieName { get; set; }
        public String? MovieDescription { get; set; }

        public DateTime ReleasedDate { get; set; }
        public String ProducerName { get; set; }
        public List<String> Actors { get; set; }
    }
}
