using System.Data;
using System.Data.SqlClient;
using UserClass;
using Cript;
using Modelclass;
using System.ComponentModel;

namespace desafio2
{
    class desafio2{
        // Server=DESKTOP-UK193S6\SQLEXPRESS;Database=DESAFIO;Trusted_Connection=True;

        // ver como armazenar o salt, desaltar as senhas, como conectar o blazor com esse programa aq 


            enum UserType{
                admin,
                common
            }

            static int CalcExpense(string Message){
                float res = (float)Message.Length/153;
                int rounded = (int)Math.Ceiling(res);
                return rounded;
        }

            //         static T RequestBD<T>(string SQL, SqlConnection con) where T : IConvertible{
            //             SqlCommand cmd = new(SQL, con);
            //             cmd.CommandType = CommandType.Text;
            //             con.Open();

            //             try
            //             {
            //                 object result = cmd.ExecuteScalar();
            //                 if (result != null && result != DBNull.Value)
            //                 {
            //                     if (typeof(T) == typeof(int))
            //                     {
            //                         return (T)(object)Convert.ToInt32(result);
            //                     }
            //                     else if (typeof(T) == typeof(string)){
            //                         return (T)(object)Convert.ToString(result);
            //                     }
                                
            //                 }

            //             return default(T);
            //             }
            //             catch (Exception ex){
            //                 System.Console.WriteLine("Error: " + ex.ToString());
            //                 return default(T);
            //             }
            //             finally{
            //                 con.Close();
            //             }
            //         }

            //         static void InputBD(string SQL, SqlConnection con){

            //             SqlCommand cmd = new(SQL, con);
            //             cmd.CommandType = CommandType.Text;
            //             con.Open();

            //             try{
            //                 int i = cmd.ExecuteNonQuery();
            //                 if (i > 0)
            //                     System.Console.WriteLine("Processo realizado com sucesso!");
            //             }
            //             catch (Exception ex){
            //                 System.Console.WriteLine("Erro: " + ex.ToString());
            //             }
            //             finally{
            //                 con.Close();
            //             }
            //         }

            //         // Busca o valor do saldo do usuario no banco
            //         static int CheckBalance(User user, SqlConnection con){
            //             string CheckSQL = "SELECT BALANCE FROM USERS WHERE ACCOUNTID = " + user.ID + ";";
            //             int balance = RequestBD<int>(CheckSQL, con);
            //             return balance;
            //         }

            //         // Checa o tipo de usuario
            //         static int? CheckType(User user, SqlConnection con){
            //             string CheckSQL = "SELECT ACCOUNTTYPE FROM USERS WHERE ACCOUNTID = " + user.ID + ";";
            //             return RequestBD<int>(CheckSQL, con);
            //         }
                    
            //         static string SendPass(SqlConnection con, string EmailGiv){
            //             string CheckSQL = "SELECT PASSWD FROM USERS WHERE EMAIL = '" + EmailGiv + "';";
            //             string pass = RequestBD<string>(CheckSQL, con);
            //             if(pass == null){
            //                 return "Email incorreto";
            //             }
            //             return pass;
            //         }
            //         static string CheckLogin(bool Equal){
            //             if(Equal){
            //                 return "Logado";
            //             }
            //             return "Credenciais incorretas!";
            //         }

            //         // metodo usado pelo admin para dar saldo
            //         static void GiveBalance(User user, int ReceiverID, int value, SqlConnection con){
            //             int? type = CheckType(user, con);
            //             if(type == 1){
            //                 System.Console.WriteLine("Acesso negado!");
            //             }else{
            //                 string GiveSQL = "UPDATE USERS SET BALANCE = BALANCE + "+ value +" WHERE ACCOUNTID = " + ReceiverID + ";";
            //                 InputBD(GiveSQL, con);
            //             }
            //         } 
            //         // Cobra do saldo do usuario
            //         static void DeductBalance(User user, SqlConnection con){
            //             int balance = CheckBalance(user, con);
            //             if(balance <= 0){
            //                 System.Console.WriteLine("Saldo insuficiente!!");
            //             }else{  
            //             string DeductSQL = "UPDATE USERS SET BALANCE = BALANCE - 1 WHERE ACCOUNTID = " + user.ID + ";" ;
            //             InputBD(DeductSQL, con);
            //             }
            //         }
            //         static void CreateUser(User user, string SecPass, SqlConnection con){
            //             string InsertSQL = "INSERT INTO USERS (EMAIL, PASSWD, PHONE, CPF, BALANCE) " + " VALUES ('" + user.Email + "', '" + SecPass + "', '" + user.PHONE + "', '" + user.CPF + "', 0);";
            //             InputBD(InsertSQL, con);
            // }
            static void Main(string[] args) {
                User user = new();

                PasswordHasher passwordHasher = new();

                // string SecurePass;
                // SecurePass = PasswordHasher.HashPassword(user.Password);
                // string EmailGiv = "vdaasdda";
                // string hashedPassword = Model.SendPass(con, EmailGiv);
                // if(hashedPassword == "Email incorreto"){
                //     System.Console.WriteLine(hashedPassword);
                // }else{
                // string PassInputed = "adads";
                
                // System.Console.WriteLine(Model.CheckLogin(PasswordHasher.VerifyPassword(hashedPassword, PassInputed)));
                // }

                // Model.GiveBalance(user,1, 10, con);

                // DeductBalance(user, con);

                // CreateUser(user, SecurePass, con);

                // string sqlselect = @"INSERT INTO USERS ( EMAIL, PASSWD, PHONE, CPF, BALANCE)VALUES ('DASAD@DASDSA', '213131', 312231, 3131222, 0);";
            }
    }
}