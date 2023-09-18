using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using UserModel;
using UserClass;
using UserClass.Security;

namespace API.Controllers;

public class UserController : Controller{

    public IActionResult Register (){
        return View();
    }

    public IActionResult Login(){
        return View();
    }
    public IActionResult Balance(User user){
        int balance = UserModel.UserModel.CheckBalance(user);
        ViewData["Balance"] = balance;
        return View();
    }
    public static void CreateToken(User user){
        var token = TokenGen.GenerateToken(user);
        System.Console.WriteLine(token);
    }
    public RedirectToActionResult CreateUser(string Email, string passwd, string phone_number, string CPF){
        if(UserModel.UserModel.CreateUser(Email, passwd, phone_number, CPF)){
            return RedirectToAction("Login");
        }else{
            return RedirectToAction("Register");
            //  "Credenciais Incorretas!";
        }
    }
    public RedirectToActionResult LoginUser(string Email, string passwd){
        System.Console.WriteLine(Email, passwd);
        User user = UserModel.UserModel.GetUserByEmail(Email);
        System.Console.WriteLine(user);
        if(UserModel.UserModel.LoginCheck(Email, passwd) == "Logado"){
            // CreateToken(user);
            return RedirectToAction("Index");
        }
        return RedirectToAction("Login");
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
}

