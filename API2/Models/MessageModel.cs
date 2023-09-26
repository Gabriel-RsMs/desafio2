using System.Net.Http.Headers;
using System.Text;

namespace MessageModel
{
    public class MessageModel{
        public static async Task<string> SendMessageAsync(int ID, string phone_number, string message){
            string url = "https://sms.comtele.com.br/api/v2/send";
            using (HttpClient httpClient = new()){
                httpClient.DefaultRequestHeaders.Add("auth-key", "7888ffef-72f1-45ce-bd7f-82faf12d7fd6");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string payload = $"{{\"Sender\":\"{ID}\",\"Receivers\":\"{phone_number}\",\"Content\":\"{message}\"}}";
                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url)){
                        request.Content = content;
                        HttpResponseMessage response = await httpClient.SendAsync(request);
                        string responseContent = await response.Content.ReadAsStringAsync();
                        return responseContent;
                }
            }
        }

        public static async Task<string> MakeGetRequest(string url){
            using (HttpClient httpClient = new())
            {
                httpClient.DefaultRequestHeaders.Add("auth-key", "7888ffef-72f1-45ce-bd7f-82faf12d7fd6");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                }
                else
                {
                    return $"Error: {response.StatusCode}";
                }
            }
        }

        public static async Task<string> RequestReportResponse(DateTime BeginningDate, DateTime EndDate){
            string url = "https://sms.comtele.com.br/api/v2/replyreporting";

            string BD = BeginningDate.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz");
            string ED = EndDate.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz");

            string queryParams = $"StartDate={Uri.EscapeDataString(BD)}&EndDate={Uri.EscapeDataString(ED)}";
            url += "?" + queryParams;
            System.Console.WriteLine(url);
            return await MakeGetRequest(url);
        }


        public static async Task<string> RequestReportDetailed(DateTime BeginningDate, DateTime EndDate, string Receiver, string RequestMessage)
        {
            string url = "https://sms.comtele.com.br/api/v2/detailedreporting";

            string BD = BeginningDate.ToString("yyyy/MM/dd");
            string ED = EndDate.ToString("yyyy/MM/dd");
            
            if(Receiver != ""){Receiver = "55" + Receiver;}
            System.Console.WriteLine(Receiver);

            string queryParams = $"StartDate={Uri.EscapeDataString(BD)}&EndDate={Uri.EscapeDataString(ED)}&Receiver={Uri.EscapeDataString(Receiver.ToString())}&RequestMessage={Uri.EscapeDataString(RequestMessage.ToString())}";
            url += "?" + queryParams;

            System.Console.WriteLine(url);
            
            return await MakeGetRequest(url);
        }

    }
}