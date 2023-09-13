using UserClass;
using ControllerClass;

namespace desafio2
{
    class desafio2{
        // Server=DESKTOP-UK193S6\SQLEXPRESS;Database=DESAFIO;Trusted_Connection=True;

        // como conectar o blazor com esse programa aq, levar em consideracao de migrar os arquivos de app para o blazor 
        // conectar com a API e tentar fazer funcionar com ela, dps ver o negocio das respostas e relatorios, se preocupar com o front dps
        
        // string url = "https://sms.comtele.com.br/api/v2/send";
        // httpClient.DefaultRequestHeaders.Add("auth-key", "7888ffef-72f1-45ce-bd7f-82faf12d7fd6");
        // httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


        enum UserType{
            admin,
            common
        }

        static int CalcExpense(string Message){
            float res = (float)Message.Length/153;
            int rounded = (int)Math.Ceiling(res);
            return rounded;
        }
        static async Task Main(string[] args) {
            User user = new();
            // Controller.NewUser(user, "carro");
            string EmailGiv = "Eu@eu", SenhaGiv = "carro";
            System.Console.WriteLine(Controller.Login(EmailGiv, SenhaGiv));
            // int [] users = {3};
            // Controller.Provide(user, users, 50);
            // string Message = "dasdadddasdadddasdadddasdadddasdadddasdaddda";
            // Controller.Charge(user, CalcExpense(Message));

            // await Controller.SendMessage(user, "1312312", Message);
        }
    }
}