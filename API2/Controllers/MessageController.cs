using Microsoft.AspNetCore.Mvc;
using UserClass;
using MessageModel;

namespace API.Controllers;

public class MessageController : Controller{
     public static async Task<string> SendMessage(User user, string phone_number, string message){
            await MessageModel.MessageModel.SendMessageAsync(user, phone_number, message);
            return "mensagem enviada";
        }
}