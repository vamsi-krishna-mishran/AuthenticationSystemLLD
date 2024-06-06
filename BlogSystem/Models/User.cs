using BlogSystem.Enums;

namespace BlogSystem.Models
{
    public class User
    {
        public string? id { get; set; }
        public string name { get; set; }
        public string userName { get; set; }
        public int age { get; set; }
        //      public System system { get; set; }

        public UserType userType { get; set; }
        public Address? address { get; set; }
        public string password { get; set; }
    }
    public class Student : User
    {
        public Student()
        {
            this.userType = UserType.STUDENT;
        }
    }

    public class Admin : User
    {
        public Admin()
        {
            this.userType = UserType.ADMIN;
        }
    }

}
