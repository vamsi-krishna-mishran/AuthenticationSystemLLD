namespace BlogSystem.Models
{
    public class Blog
    {
        public string blogId { get; set; }

        public DateTime publishDate { get; set; }

        public Admin adminId { get; set; }

        public List<Rating> ratings { get; set; }

    }
    public class Rating
    {
        public string id { get; set; }
        public int rating { get; set; }
    }
}
