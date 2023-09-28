using System.Data;
using System.Data.SqlClient;
using Cript;
using UserClass;

namespace UserModel
{
    public class UserModel{

        private static string connectionString = @"Server=KAHDYMUS-PC\SQLEXPRESS;Database=DESAFIO;Trusted_Connection=True;"; // <- note do diego

        // string connectionString = @"Server=DESKTOP-UK193S6\SQLEXPRESS;Database=DESAFIO;Trusted_Connection=True;";  <- meu pc

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
        public static int CheckBalance(int ID){
            string CheckSQL = "SELECT BALANCE FROM USERS WHERE ACCOUNTID = " + ID + ";";
            int balance = RequestBD<int>(CheckSQL);
            return balance;
        }

        public static string LoginCheck(string EmailGiv, string PassGiv){
            string CheckSQL = "SELECT PASSWD, SALT FROM USERS WHERE EMAIL = '" + EmailGiv + "';";
            (string storedPassword, string storedSalt) =  RequestTuple<string, string>(CheckSQL);
            if (storedPassword == null|| storedSalt == null){
                return "Credenciais incorretas!";
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

        public static string GiveBalance(string accounttype, int[] receiverIDs, int value){     
            if (accounttype != "ADMIN"){
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
        public static Tuple<string, int> DeductBalance(int ID, int Value){
            int balance = CheckBalance(ID);
            System.Console.WriteLine(balance);
            if (balance <= 0){
                return new Tuple<string, int>("Saldo insuficiente!!", balance);
            }
            else if ((balance - Value) < 0){
                return new Tuple<string, int>("Saldo insuficiente!!!", balance);
            }
            string DeductSQL = "UPDATE USERS SET BALANCE = BALANCE - " + Value + " WHERE ACCOUNTID = " + ID + ";";
            System.Console.WriteLine(DeductSQL);
            InputBD(DeductSQL);
            return new Tuple<string, int>("Credito descontado", CheckBalance(ID));
    }
        public static bool CreateUser(string Email, string password, string phone_number, string CPF){
            string existSQL = "SELECT EMAIL FROM USERS WHERE EMAIL = '" + Email + "';";
            if(RequestBD<string>(existSQL) != null){
                return false;
            }
            try{
            var (HashedPassword, Salt) = PasswordHasher.HashPassword(password);
            string SecPass = HashedPassword;
            string salt = Salt;
            string InsertSQL = "INSERT INTO USERS (EMAIL,PASSWD, SALT, PHONE, CPF) " + " VALUES ('" + Email + "', '" + SecPass + "', '" + salt +"', '" + phone_number + "', '" + CPF + "');";
            InputBD(InsertSQL);
            return true;
            }catch (Exception ex){
                System.Console.WriteLine("Error: " + ex.ToString());
                return false;
            }
            
        }

        public static int GetIdbyEmail(string Email){
            string GetIdSQL = "SELECT ACCOUNTID FROM USERS WHERE EMAIL = '" + Email + "';";
            return RequestBD<int>(GetIdSQL);
        }

        public static string TypeByEmail(string Email){
            string TypeByEmailSQL = "SELECT ACCOUNTTYPE FROM USERS WHERE EMAIL = '" + Email + "';";

            return RequestBD<string>(TypeByEmailSQL);
        }

        public static string UpdateUser(User user, string newEmail, string password, string newPhone, string newCPF){
            var (HashedPassword, Salt) = PasswordHasher.HashPassword(password);
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

    }
}
