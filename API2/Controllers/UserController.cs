using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using UserModel;
using UserClass;

namespace API.Controllers;

public class UserController : Controller{

    public IActionResult Balance(User user){
        int balance = UserModel.UserModel.CheckBalance(user);
        ViewData["Balance"] = balance;
        return View();
    }
    public IActionResult Register (){
        return View();
    }

    public IActionResult Login(){
        return View();
    }

    public RedirectToActionResult CreateUser(string Email, string passwd, string phone_number, string CPF){
       // Deu erro concertar
        if(UserModel.UserModel.CreateUser(Email, passwd, phone_number, CPF)){
            return RedirectToAction("Login");
        }else{
            return RedirectToAction("Register");
            //  "Credenciais Incorretas!";
        }
    }

}
/*
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
    */