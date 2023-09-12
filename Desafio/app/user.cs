namespace UserClass{
    public class User {
        public string Email { get; set;}
        public string Password { get; set;}
        public int ID { get; set; }
        public int PHONE { get; set; }
        public int CPF { get; set; }

        public User()
        {
            Email = "Eu@eu";
            Password = "adads";
            ID = 1;
            PHONE = 23424324;
            CPF = 423423423;
        }
    }
}