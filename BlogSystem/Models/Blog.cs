namespace BlogSystem.Models
{
    public class Blog
    {
        public int id { get; set; }
        public string blogHeading { get; set; }

        public string blogThumbnailImg { get; set; }

        public string blogDocument { get; set;   }
        public DateTime publishDate { get; set; }

        public User adminId { get; set; }

       // public string blogURI { get; set; }

       // public List<Rating>? ratings { get; set; }

    }
    public class Rating
    {
        public int id { get; set; }
        public int rating { get; set; }

        public User user { get; set; }

        public Blog blog { get; set; }
    }
}
