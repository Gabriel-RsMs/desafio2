using Modelclass;
using UserClass;

namespace ControllerClass{
    public class Controller{

        public static int ReturnBalance(User user){
            return Model.CheckBalance(user);
        }

        public static int ReturnType(User user){
            return Model.CheckType(user);
        }

        public static string Provide(User user, int ReceiverID, int value){
            return Model.GiveBalance(user, ReceiverID, value);
        }
        public static string Charge(User user){
            return Model.DeductBalance(user);
        }

        public static string Login(string EmailGiv, string PasswdGiv){
            return Model.LoginCheck(EmailGiv, PasswdGiv);
        }

        public static string NewUser(User user, string passwd){
            Model.CreateUser(user, passwd);
            return "Usuario criado";
        }
        
    }
}