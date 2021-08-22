using System;
using System.IO;

namespace Triggergram.Core.Services.DTO
{
    public class MediaPostRecord
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Stream MediaStream { get; set; }
        public Guid AccountId { get; set; }
    }
}
