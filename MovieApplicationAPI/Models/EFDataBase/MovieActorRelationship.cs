using System;
using System.Collections.Generic;

namespace MovieApplicationAPI.Models.EFDataBase
{
    public partial class MovieActorRelationship
    {
        public int MovieActorRelationshipId { get; set; }
        public int MovieId { get; set; }
        public int ActorId { get; set; }

        public virtual Actor Actor { get; set; } = null!;
        public virtual Movie Movie { get; set; } = null!;
    }
}
