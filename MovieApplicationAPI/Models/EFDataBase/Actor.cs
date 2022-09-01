using System;
using System.Collections.Generic;

namespace MovieApplicationAPI.Models.EFDataBase
{
    public partial class Actor
    {
        public Actor()
        {
            MovieActorRelationships = new HashSet<MovieActorRelationship>();
        }

        public int ActorId { get; set; }
        public string ActorName { get; set; } = null!;

        public virtual ICollection<MovieActorRelationship> MovieActorRelationships { get; set; }
    }
}
