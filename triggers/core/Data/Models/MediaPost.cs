using System;

namespace Triggergram.Core.Data.Models
{
    public class MediaPost
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Views { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid AccountId { get; set; }
    }
}
