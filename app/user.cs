using System.Security.AccessControl;

namespace UserType{
    public class User {
        public string Email { get; set;}
        public string Password { get; set;}
        public int ID { get; set; }
        public int PHONE { get; set; }
        public int CPF { get; set; }

        public User()
        {
            Email = "vdaasdda";
            Password = "adads";
            ID = 2;
            PHONE = 23424324;
            CPF = 423423423;
        }
    }
}