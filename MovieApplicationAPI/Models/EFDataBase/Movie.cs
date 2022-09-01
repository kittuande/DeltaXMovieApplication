using System;
using System.Collections.Generic;

namespace MovieApplicationAPI.Models.EFDataBase
{
    public partial class Movie
    {
        public Movie()
        {
            MovieActorRelationships = new HashSet<MovieActorRelationship>();
        }

        public int MovieId { get; set; }
        public string MovieName { get; set; } = null!;
        public DateTime DateOfRelease { get; set; }
        public int ProducerId { get; set; }
        public string? Description { get; set; }

        public virtual Producer Producer { get; set; } = null!;
        public virtual ICollection<MovieActorRelationship> MovieActorRelationships { get; set; }
    }
}
