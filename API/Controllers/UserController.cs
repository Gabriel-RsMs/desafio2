using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UserClass;
using UserModel;
using API.Models;

namespace API.Controllers;

public class UserController : Controller{

    public static int ReturnBalance(User user){
        return UserModel.UserModel.CheckBalance(user);
    }

    public static int ReturnType(User user){
        return UserModel.UserModel.CheckType(user);
    }

    public static string Provide(User user, int[] ReceiverID, int value){
        return UserModel.UserModel.GiveBalance(user, ReceiverID, value);
    }
    public static Tuple<string, int> Charge(User user, int Value){
        return UserModel.UserModel.DeductBalance(user, Value);
    }

    public static string Login(string EmailGiv, string PasswdGiv){
        return UserModel.UserModel.LoginCheck(EmailGiv, PasswdGiv);
    }

    public static string NewUser(User user, string passwd){
        UserModel.UserModel.CreateUser(user, passwd);
        return "Usuario criado";
    }
    
}