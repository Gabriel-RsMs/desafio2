using System.Data;
using System.Data.SqlClient;
using Cript;
using UserClass;

namespace Modelclass
{
    public class Model{
        private PasswordHasher passwordHasher = new();

        private static readonly string connectionString = @"Server=KAHDYMUS-PC\SQLEXPRESS;Database=DESAFIO;Trusted_Connection=True;";

        // string connectionString = @"Server=DESKTOP-UK193S6\SQLEXPRESS;Database=DESAFIO;Trusted_Connection=True;";

        protected static SqlConnection con = new(connectionString);

        protected static T RequestBD<T>(string SQL){
            SqlCommand cmd = new(SQL, con);
            cmd.CommandType = CommandType.Text;
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

            SqlCommand cmd = new(SQL, con)
            {
                CommandType = CommandType.Text
            };
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
            Tuple<string, string> credentials = RequestBD<Tuple<string, string>>(CheckSQL);
            if (credentials == null)
            {
                return "Email incorreto";
            }

            string storedPassword = credentials.Item1;
            string storedSalt = credentials.Item2;

            if (PasswordHasher.VerifyPassword(storedPassword, PassGiv, storedSalt))
            {
                return "Logado";
            }

            return "Credenciais incorretas!"; ;

        }

        // metodo usado pelo admin para dar saldo
        public static string GiveBalance(User user, int ReceiverID, int value){
            int type = CheckType(user);
            if(type == 1){
                return "Acesso negado!";
            }else{
                //checar se ReceiverID existe
                string GiveSQL = "UPDATE USERS SET BALANCE = BALANCE + "+ value +" WHERE ACCOUNTID = " + ReceiverID + ";";
                InputBD(GiveSQL);
                return "Saldo entregue";
            }
        } 
        // Cobra do saldo do usuario
        public static string DeductBalance(User user){
            int balance = CheckBalance(user);
            if(balance <= 0){
                return "Saldo insuficiente!!";
            }else{  
                //implementar um sistema que fala o quanto de creditos que foram descontados
                string DeductSQL = "UPDATE USERS SET BALANCE = BALANCE - 1 WHERE ACCOUNTID = " + user.ID + ";" ;
                InputBD(DeductSQL);
                return "Credito descontado";
            }
        }
        public static void CreateUser(User user, string passwd){
            var (HashedPassword, Salt) = PasswordHasher.HashPassword(passwd);
            string SecPass = HashedPassword;
            string salt = Salt;
            string InsertSQL = "INSERT INTO USERS (EMAIL, PASSWD, SALT, PHONE, CPF, BALANCE) " + " VALUES ('" + user.Email + "', '" + SecPass + "', '" + salt +"', '" + user.PHONE + "', '" + user.CPF + "', 0);";
            System.Console.WriteLine(InsertSQL);
            InputBD(InsertSQL);
        }
    }
}
