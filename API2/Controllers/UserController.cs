using Microsoft.AspNetCore.Mvc;
using UserClass;
using System.Security.Claims;
using System.Text;

namespace API.Controllers;

public class UserController : Controller{

    public IActionResult Balance(User user){
        if (!HttpContext.Session.TryGetValue("user.accounttype", out byte[] accountTypeBytes) || accountTypeBytes == null){
            System.Console.WriteLine("acesso negado entre com uma conta");
            return RedirectToAction("Login");
        }
        int balance = UserModel.UserModel.CheckBalance((int)HttpContext.Session.GetInt32("user.id"));
        ViewData["Balance"] = balance;
        return View();
    }
    public IActionResult Register (){
        return View();
    }
    public IActionResult Login(){
        return View();
    }

    public IActionResult Unathorized(){
        return View();
    }

    public IActionResult Menager(){
        if(HttpContext.Session.GetString("user.accounttype") == "ADMIN"){

            return View();
        }
        return RedirectToAction("Unathorized");
    }

    public RedirectToActionResult CreateUser(string Email, string password, string phone_number, string CPF){
       // Deu erro concertar
        if(UserModel.UserModel.CreateUser(Email, password, phone_number, CPF)){
            return RedirectToAction("Login");
        }else{
            return RedirectToAction("Register");
            //  "Credenciais Incorretas!";
        }
    }

    public (User?, string?) GetProfile(User user){
        if (User.Identity is ClaimsIdentity claimsIdentity)
        {
            var userRoleClaim = claimsIdentity.FindFirst(ClaimTypes.Role);
            var userIDClaim = claimsIdentity.FindFirst("ID");

            if (userRoleClaim != null && userIDClaim != null)
            {
                var userRole = userRoleClaim.Value;
                var userID = userIDClaim.Value;

                user.ACCOUNTTYPE = userRole;
                user.ID = int.Parse(userID);

                return (user, null); 
            }
        }

        return (null, "Error message");
    }

// ver de mandar para o front uma tuple de redirect e uma string para aparecer em um alert se ele foi logado ou n ou se as credenciais estao erradas
    public RedirectToActionResult LoginUser(string Email, string password){
        if (!User.Identity.IsAuthenticated)
        {
            if (UserModel.UserModel.LoginCheck(Email, password) == "Logado")
            {
                User user = new()
                {
                    ID = UserModel.UserModel.GetIdbyEmail(Email),
                    ACCOUNTTYPE = UserModel.UserModel.TypeByEmail(Email)
                };
                HttpContext.Session.SetInt32("user.id", (int)user.ID);
                HttpContext.Session.SetString("user.accounttype", user.ACCOUNTTYPE);

                return RedirectToAction("Balance", user);
            }
            return RedirectToAction("Login");
        }

        System.Console.WriteLine("ja logado");
        return RedirectToAction("Login");
    }


    public static string Provide(User user, int[] ReceiverID, int value){
        return UserModel.UserModel.GiveBalance(user, ReceiverID, value);
    }
    public static Tuple<string, int> Charge(int ID, int Value){
        return UserModel.UserModel.DeductBalance(ID, Value);
    }
}
