namespace BlogServices.Models
{
    public class Articles
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Content { get; set; }
        public Status Status { get; set; }
        public DateTime PublishedDate { get; set; }
        public DateTime CreationDate { get; set; }
    }

    public enum Status
    {
        Draft,
        Published
    }
}
