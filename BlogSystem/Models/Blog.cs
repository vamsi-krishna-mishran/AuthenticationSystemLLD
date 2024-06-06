namespace BlogSystem.Models
{
    public class Blog
    {
        public string blogId { get; set; }

        public DateTime publishDate { get; set; }

        public Admin adminId { get; set; }

        public List<int> ratings { get; set; }
        public Blog() { 
            ratings = new List<int>();
        }
    }
}
