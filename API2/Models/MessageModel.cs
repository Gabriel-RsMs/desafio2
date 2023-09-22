using System.Net.Http.Headers;
using System.Text;

namespace MessageModel
{
    public class MessageModel{
        // ver com o diego se as respostas devem ser respondidas na plataforma ou se devem pegar de numeros reais
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

        public static async Task<string> RequestReportDetailed(DateTime BeginingDate, DateTime EndDate, string Delivered, int Receiver){
            string url = "https://sms.comtele.com.br/api/v2/detailedreporting";
            using (HttpClient httpClient = new()){
                httpClient.DefaultRequestHeaders.Add("auth-key", "7888ffef-72f1-45ce-bd7f-82faf12d7fd6");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string payload = $"{{\"StartDate\":\"{BeginingDate}\",\"EndDate\":\"{EndDate}\",\"Delivered\":\"{Delivered}\",\"Receiver\":\"{Receiver}\"}}";
                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url)){
                        request.Content = content;
                        HttpResponseMessage response = await httpClient.SendAsync(request);
                        string responseContent = await response.Content.ReadAsStringAsync();
                        return responseContent;
                }
            }
            
        }
    }
}