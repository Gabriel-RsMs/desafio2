using System.Data;
using System.Data.SqlClient;
using Cript;
using UserClass;
using System.Net.Http.Headers;
using System.Text;
using System.Runtime.CompilerServices;

namespace UserModel
{
    public class Model{
        private PasswordHasher passwordHasher = new();

        private static string connectionString = @"Server=KAHDYMUS-PC\SQLEXPRESS;Database=DESAFIO;Trusted_Connection=True;";

        // string connectionString = @"Server=DESKTOP-UK193S6\SQLEXPRESS;Database=DESAFIO;Trusted_Connection=True;";

        protected static SqlConnection con = new(connectionString);

        protected static (T1 Value1, T2 Value2) RequestTuple<T1, T2>(string SQL){
            SqlCommand cmd = new(SQL, con)
            {
                CommandType = CommandType.Text
            };
            con.Open();

            try{
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()){
                    T1 value1 = default;
                    T2 value2 = default;

                    if (typeof(T1) == typeof(int)){
                        value1 = (T1)(object)Convert.ToInt32(reader[0]);
                    }
                    else if (typeof(T1) == typeof(string)){
                        value1 = (T1)(object)Convert.ToString(reader[0]);
                    }

                    if (typeof(T2) == typeof(int)){
                        value2 = (T2)(object)Convert.ToInt32(reader[1]);
                    }
                    else if (typeof(T2) == typeof(string)){
                        value2 = (T2)(object)Convert.ToString(reader[1]);
                    }

                    return (value1, value2);
                }

                return (default(T1), default(T2));
            }
            catch (Exception ex){
                System.Console.WriteLine("Error: " + ex.ToString());
                return (default(T1), default(T2));
            }
            finally{
                con.Close();
            }
        }

        protected static T RequestBD<T>(string SQL){
            SqlCommand cmd = new(SQL, con)
            {
                CommandType = CommandType.Text
            };
            con.Open();

            try{
                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value){
                    if (typeof(T) == typeof(int)){
                        return (T)(object)Convert.ToInt32(result);
                    }else if (typeof(T) == typeof(string)){
                        return (T)(object)Convert.ToString(result);
                    }
                }

                return default(T);
            }
            catch (Exception ex){
                System.Console.WriteLine("Error: " + ex.ToString());
                return default(T);
            }
            finally{
                con.Close();
            }
        }

        protected static void InputBD(string SQL){

            SqlCommand cmd = new(SQL, con);
            cmd.CommandType = CommandType.Text;
            con.Open();

            try{
                int i = cmd.ExecuteNonQuery();
                if (i > 0){
                    System.Console.WriteLine("Processo realizado com sucesso!");
                }
            }
            catch (Exception ex){
                System.Console.WriteLine("Erro: " + ex.ToString());
            }
            finally{
                con.Close();
            }
        }

        // Busca o valor do saldo do usuario no banco
        public static int CheckBalance(User user){
            string CheckSQL = "SELECT BALANCE FROM USERS WHERE ACCOUNTID = " + user.ID + ";";
            int balance = RequestBD<int>(CheckSQL);
            return balance;
        }

        // Checa o tipo de usuario
        public static int CheckType(User user){
            string CheckSQL = "SELECT ACCOUNTTYPE FROM USERS WHERE ACCOUNTID = " + user.ID + ";";
            return RequestBD<int>(CheckSQL);
        }


        public static string LoginCheck(string EmailGiv, string PassGiv){
            string CheckSQL = "SELECT PASSWD, SALT FROM USERS WHERE EMAIL = '" + EmailGiv + "';";
            // RequestTuple<Tuple<string, string>>(CheckSQL);
            (string storedPassword, string storedSalt) =  RequestTuple<string, string>(CheckSQL);
            if (storedPassword == null|| storedSalt == null){
                return "Email incorreto";
            }

            if (PasswordHasher.VerifyPassword(PassGiv, storedPassword, storedSalt)){
                return "Logado";
            }

            return "Credenciais incorretas!";

        }

        public static bool UserExists(int receiverID){
        try{
            string checkSQL = "SELECT COUNT(*) FROM USERS WHERE ACCOUNTID = " + receiverID + ";";
                
                int count = RequestBD<int>(checkSQL);
                
                return count > 0;
        }
        catch (Exception ex){
            Console.WriteLine("Error: " + ex.ToString());
            return false;
        }
    }

        public static string GiveBalance(User user, int[] receiverIDs, int value){
            int type = CheckType(user);
            if (type == 1){
                return "Acesso negado!";
            }
            else{
                foreach (int receiverID in receiverIDs){
                    if (UserExists(receiverID)){
                        string giveSQL = "UPDATE USERS SET BALANCE = BALANCE + " + value + " WHERE ACCOUNTID = " + receiverID + ";";
                        InputBD(giveSQL);
                    }
                    else{
                        return "Usuário não encontrado";
                    }
                }
                return "Saldo entregue";
            }
        }

        // Cobra do saldo do usuario
        public static Tuple<string, int> DeductBalance(User user, int Value){
            int balance = CheckBalance(user);
            if (balance <= 0){
                return new Tuple<string, int>("Saldo insuficiente!!", balance);
            }
            else if ((balance - Value) < 0){
                return new Tuple<string, int>("Saldo insuficiente!!!", balance);
            }
            string DeductSQL = "UPDATE USERS SET BALANCE = BALANCE - " + Value + " WHERE ACCOUNTID = " + user.ID + ";";
            InputBD(DeductSQL);
            return new Tuple<string, int>("Credito descontado", CheckBalance(user));
        }
        public static string CreateUser(User user, string passwd){
            string existSQL = "SELECT EMAIL FROM USERS WHERE EMAIL = '" + user.Email + "';";
            if(RequestBD<string>(existSQL) != null){
                return "Email ja registrado!!!";
            }
            var (HashedPassword, Salt) = PasswordHasher.HashPassword(passwd);
            string SecPass = HashedPassword;
            string salt = Salt;
            string InsertSQL = "INSERT INTO USERS (EMAIL, PASSWD, SALT, PHONE, CPF, BALANCE) " + " VALUES ('" + user.Email + "', '" + SecPass + "', '" + salt +"', '" + user.PHONE + "', '" + user.CPF + "', 0);";
            InputBD(InsertSQL);
            return "usuario criado com sucesso"; 
        }

        public static User GetUserById(int ID){
            string GetUserByIdSQL = "SELECT * FROM USERS WHERE ACCOUNTID = " + ID + ";";
            return RequestBD<User>(GetUserByIdSQL);
        }

        public static string UpdateUser(User user, string newEmail, string passwd, string newPhone, string newCPF){
            var (HashedPassword, Salt) = PasswordHasher.HashPassword(passwd);
            string UpSQL = "UPDATE USERS SET EMAIL = '"+ newEmail +"', PASSWD = '"+ HashedPassword  + "', SALT = '" + Salt + "', PHONE ='" + newPhone + "', CPF = '" + newCPF + ", WHERE ACCOUNTID = " +  user.ID+ ";";
            InputBD(UpSQL);
            return "usuario atualizado";
        }

        public static string DeleteUser(User user, bool confirmation){
            if(confirmation){
                string DelSQL = "DELETE FROM USERS WHERE ACCOUNTID = " + user.ID + ";";
                InputBD(DelSQL);
                return "Conta apagada";
            }
            return "Entendido, a conta sera mantida";
        }

        public static async Task SendMessageAsync(User user, string phone_number, string message){
            string url = "https://sms.comtele.com.br/api/v2/send";
            using (HttpClient httpClient = new()){
                httpClient.DefaultRequestHeaders.Add("auth-key", "7888ffef-72f1-45ce-bd7f-82faf12d7fd6");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string payload = $"{{\"Sender\":\"{user.ID}\",\"Receivers\":\"{phone_number}\",\"Content\":\"{message}\"}}";
                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
                System.Console.WriteLine("ta aq");

                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url)){
                        request.Content = content;
                        HttpResponseMessage response = await httpClient.SendAsync(request);
                        string responseContent = await response.Content.ReadAsStringAsync();
                        System.Console.WriteLine("ta aq dentro");

                        Console.WriteLine($"Response: {responseContent}");
                }
            }
        }

    }
}
