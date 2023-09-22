using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class MessageController : Controller{
    public IActionResult RedirectMessage(string phone_number, string message){
        var status = SendMessage(phone_number, message);
        ViewData["Status"] = status;
        return View();
    }
    public async Task<string> SendMessage(string phone_number, string message){
            string Response = await MessageModel.MessageModel.SendMessageAsync((int)HttpContext.Session.GetInt32("user.id"),phone_number, message);
            System.Console.WriteLine(Response);
            string[] split = Response.Split(",");
            return split[2];
    }

    public async Task<string> ReportDetailed(DateTime BeginingDate, DateTime EndDate, string Delivered, int Receiver){
        string Response = await MessageModel.MessageModel.RequestReportDetailed(BeginingDate, EndDate, Delivered, Receiver);
        return Response;
    }
}