using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class MessageController : Controller{
    public IActionResult Redirect(string phone_number, string message){
        var status = SendMessage(phone_number, message);
        ViewData["Status"] = status;
        return View();
    }
    public async Task<string> SendMessage(string phone_number, string message){
            string Response = await MessageModel.MessageModel.SendMessageAsync((int)HttpContext.Session.GetInt32("user.id"),phone_number, message);
            string[] splited = Response.Split(",");
            return splited[2];
    }
}