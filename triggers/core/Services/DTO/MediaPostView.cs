namespace Triggergram.Core.Services.DTO
{
    public class MediaPostView
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Views { get; set; }
        public string CreatedAt { get; set; }

        public string Account { get; set; }
    }
}
