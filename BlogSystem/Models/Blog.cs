namespace BlogSystem.Models
{
    public class Blog
    {
        public int blogId { get; set; }

        public DateTime publishDate { get; set; }

        public Admin adminId { get; set; }

        public string blogURI { get; set; }

        public List<Rating> ratings { get; set; }

    }
    public class Rating
    {
        public int id { get; set; }
        public int rating { get; set; }
    }
}
