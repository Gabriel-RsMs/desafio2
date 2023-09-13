namespace UserClass{
    public class User {
        public string Email { get; set;}
        public string Password { get; set;}
        public int ID { get; set; }
        public int PHONE { get; set; }
        public int CPF { get; set; }

        public User()
        {
            Email = "Eu@eu3";
            Password = "casa";
            ID = 3;
            PHONE = 23424324;
            CPF = 423423423;
        }
    }
}