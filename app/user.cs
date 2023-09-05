using System.Security.AccessControl;

namespace UserType{
    public class User {
        public string Email { get; }
        public string Password { get; }
        public int ID { get; }

        public User()
        {
            Email = "vdaasdda";
            Password = "adads";
            ID = 1;
        }
    }
}