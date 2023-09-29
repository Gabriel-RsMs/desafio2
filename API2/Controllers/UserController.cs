using Microsoft.AspNetCore.Mvc;
using UserClass;
using System.Security.Claims;
using System.Text;

namespace API.Controllers;

public class UserController : Controller{

    public IActionResult Balance(){
        if (!HttpContext.Session.TryGetValue("user.accounttype", out byte[] accountTypeBytes) || accountTypeBytes == null){
            System.Console.WriteLine("acesso negado entre com uma conta");
            return RedirectToAction("Login");
        }
        int balance = UserModel.UserModel.CheckBalance((int)HttpContext.Session.GetInt32("user.id"));
        ViewData["Balance"] = balance;
        ViewData["ID"] = (int)HttpContext.Session.GetInt32("user.id");
        return View();
    }
    public IActionResult Login(){
        var status = TempData["status"] as string;
        if(status != "Logado"){
            ViewData["status"] = status;
        }
        return View();
    }

    public IActionResult Logout(){
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    public IActionResult Unathorized(){
        return View();
    }

    public IActionResult ReportResponse(){
        if (!HttpContext.Session.TryGetValue("user.accounttype", out byte[] accountTypeBytes) || accountTypeBytes == null){
            System.Console.WriteLine("acesso negado entre com uma conta");
            return RedirectToAction("Login");
        }
        return View();
    }

    public IActionResult Report(){
        if (!HttpContext.Session.TryGetValue("user.accounttype", out byte[] accountTypeBytes) || accountTypeBytes == null){
            System.Console.WriteLine("acesso negado entre com uma conta");
            return RedirectToAction("Login");
        }
        return View();
    }

    public IActionResult ReportList(){
        var response = TempData["response"] as string;
        if(response != "Fail"){
            ViewData["response"] = response;
            System.Console.WriteLine(response);
            return View();
        }
        return RedirectToAction("Report");
    }

    public IActionResult ReportResponseList(){
        var response = TempData["response"] as string;
        if(response != "Fail"){
            ViewData["response"] = response;
            System.Console.WriteLine(response);
            return View();
        }
        return RedirectToAction("ReportResponse");
    }

    public IActionResult GiveBalance(string status){
        if (!HttpContext.Session.TryGetValue("user.accounttype", out byte[] accountTypeBytes) || accountTypeBytes == null){
            System.Console.WriteLine("acesso negado entre com uma conta");
            return RedirectToAction("Login");
        }
        ViewData["status"] = TempData["status"];    
        if(HttpContext.Session.GetString("user.accounttype") != "ADMIN"){
            System.Console.WriteLine("acesso nao autorizado");
            return RedirectToAction("Unathorized");
        }
        return View();
    }

    public RedirectToActionResult CreateUser(string Email, string password, string phone_number, string CPF){
        UserModel.UserModel.CreateUser(Email, password, phone_number, CPF);
        return RedirectToAction("Login");
    }

    // public (User?, string?) GetProfile(User user){
    //     if (User.Identity is ClaimsIdentity claimsIdentity)
    //     {
    //         var userRoleClaim = claimsIdentity.FindFirst(ClaimTypes.Role);
    //         var userIDClaim = claimsIdentity.FindFirst("ID");

    //         if (userRoleClaim != null && userIDClaim != null)
    //         {
    //             var userRole = userRoleClaim.Value;
    //             var userID = userIDClaim.Value;

    //             user.ACCOUNTTYPE = userRole;
    //             user.ID = int.Parse(userID);

    //             return (user, null); 
    //         }
    //     }

    //     return (null, "Error message");
    //}

    public RedirectToActionResult LoginUser(string Email, string password){
        if (!User.Identity.IsAuthenticated)
        {
            string status = UserModel.UserModel.LoginCheck(Email, password);
            if (status == "Logado")
            {
                User user = new()
                {
                    ID = UserModel.UserModel.GetIdbyEmail(Email),
                    ACCOUNTTYPE = UserModel.UserModel.TypeByEmail(Email)
                };
                HttpContext.Session.SetInt32("user.id", (int)user.ID);
                System.Console.WriteLine((int)HttpContext.Session.GetInt32("user.id"));
                HttpContext.Session.SetString("user.accounttype", user.ACCOUNTTYPE);

                return RedirectToAction("Balance");
            }
            TempData["status"] = status;
            return RedirectToAction("Login");
        }

        System.Console.WriteLine("ja logado");
        return RedirectToAction("Login");
    }


    public RedirectToActionResult Provide(int[] ReceiverID, int value){
        string status = UserModel.UserModel.GiveBalance(HttpContext.Session.GetString("user.accounttype"), ReceiverID, value);
        TempData["status"] = status;
        return RedirectToAction("GiveBalance");
    }
    public Tuple<string, int> Charge(int Value){
        return UserModel.UserModel.DeductBalance((int)HttpContext.Session.GetInt32("user.id"), Value);
    }

    public async Task<IActionResult> SendMessage(string phone_number, string message, int value){
        int ID = (int)HttpContext.Session.GetInt32("user.id");
        string Response = await MessageModel.MessageModel.SendMessageAsync(ID, phone_number, message);
        System.Console.WriteLine(Response);
        string[] split = Response.Split(",");
        System.Console.WriteLine(split[0]);
        if( split[0] == "{\"Success\":true"){
            (string ChargeStatus, int LeftBalance) = Charge(value);
            return RedirectToAction("Balance");
        }
        ViewData["SendFailed"] = true;
        return RedirectToAction("Balance");
    }

    public async Task<IActionResult> GetReportResponse(DateTime BeginningDate, DateTime EndDate){
        string Response = await MessageModel.MessageModel.RequestReportResponse(BeginningDate, EndDate);
        string[] split = Response.Split(",");

        if(split[0] == "{\"Success\":true"){
        TempData["response"] = Response;
        return RedirectToAction("ReportResponseList");
    }
        return RedirectToAction("ReportResponse");
    }

    public async Task<IActionResult> GetReportDetailed(DateTime BeginningDate, DateTime EndDate, string? Receiver){
        // if(Delivered == null){
        //     Delivered = "";
        // }
        if(Receiver == null){
            Receiver = "";
        }
        string Response = await MessageModel.MessageModel.RequestReportDetailed(BeginningDate, EndDate, Receiver);
        string[] split = Response.Split(",");
        System.Console.WriteLine(split[0]);
        if(split[0] == "{\"Success\":true"){
            TempData["response"] = Response;
            return RedirectToAction("ReportList");
        }
        return RedirectToAction("Report");
    }

}
