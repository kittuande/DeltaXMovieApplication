using System;
using System.Collections.Generic;

namespace MovieApplicationAPI.Models.EFDataBase
{
    public partial class Producer
    {
        public Producer()
        {
            Movies = new HashSet<Movie>();
        }

        public int ProducerId { get; set; }
        public string ProducerName { get; set; } = null!;

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
