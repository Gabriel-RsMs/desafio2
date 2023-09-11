using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;
using UserType;
using Cript;
using System.Net.Mail;




namespace desafio2{
    class desafio2{
        // Server=DESKTOP-UK193S6\SQLEXPRESS;Database=DESAFIO;Trusted_Connection=True;

        // transformar ReqestBD para retorna tanto int qunato strings quanto arrays, ver como armazenar o salt, desaltar as senhas, como conectar o blazor com esse programa aq 


            enum UserType{
                admin,
                common
            }

            static int CalcExpense(string Message){
                float res = (float)Message.Length/153;
                int rounded = (int)Math.Ceiling(res);
                return rounded;
        }

                    static int? RequestBD(string SQL, SqlConnection con){
                        SqlCommand cmd = new(SQL, con);
                        cmd.CommandType = CommandType.Text;
                        con.Open();
                        try{
                            object result = cmd.ExecuteScalar();
                            if (result != null && result != DBNull.Value){
                                return Convert.ToInt32(result);
                            }
                            return null;
                        }catch(Exception ex){
                            System.Console.WriteLine("Erro: " + ex.ToString());
                            return null;
                        }
                        finally{
                            con.Close();
                        }  
                    }
                    static void inputBD(string SQL, SqlConnection con){

                        SqlCommand cmd = new(SQL, con);
                        cmd.CommandType = CommandType.Text;
                        con.Open();

                        try{
                            int i = cmd.ExecuteNonQuery();
                            if (i > 0)
                                System.Console.WriteLine("Processo realizado com sucesso!");
                        }
                        catch (Exception ex){
                            System.Console.WriteLine("Erro: " + ex.ToString());
                        }
                        finally{
                            con.Close();
                        }
                    }
                    static int CheckBalance(User user, SqlConnection con){
                        string CheckSQL = "SELECT BALANCE FROM USERS WHERE ACCOUNTID = " + user.ID + ";";
                        int balance = Convert.ToInt32(RequestBD(CheckSQL, con));
                        return balance;
                    }

                    static int? CheckType(User user, SqlConnection con){
                        string CheckSQL = "SELECT ACCOUNTTYPE FROM USERS WHERE ACCOUNTID = " + user.ID + ";";
                        return RequestBD(CheckSQL, con);
                    }
                    
                    static void CheckLogin(SqlConnection con, string EmailGiv, string PassGiv){
                        string CheckSQL = "SELECT EMAIL,PASSWD FROM USERS WHERE EMAIL = '" + EmailGiv + "' AND PASSWD = '" + PassGiv + "';";

                    }

                    static void GiveBalance(User user, int ReceiverID, int value, SqlConnection con){
                        int? type = CheckType(user, con);
                        if(type == 1){
                            System.Console.WriteLine("Acesso negado!");
                        }else{
                            string GiveSQL = "UPDATE USERS SET BALANCE = BALANCE + "+ value +" WHERE ACCOUNTID = " + ReceiverID + ";";
                            inputBD(GiveSQL, con);
                        }
                    } 
                    static void DeductBalance(User user, SqlConnection con){
                        int balance = CheckBalance(user, con);
                        if(balance <= 0){
                            System.Console.WriteLine("Saldo insuficiente!!");
                        }else{  
                        string DeductSQL = "UPDATE USERS SET BALANCE = BALANCE - 1 WHERE ACCOUNTID = " + user.ID + ";" ;
                        inputBD(DeductSQL, con);
                        }
                    }
                    static void CreateUser(User user, string SecPass, SqlConnection con){
                        string InsertSQL = "INSERT INTO USERS (EMAIL, PASSWD, PHONE, CPF, BALANCE) " + " VALUES ('" + user.Email + "', '" + SecPass + "', '" + user.PHONE + "', '" + user.CPF + "', 0);";
                        inputBD(InsertSQL, con);
            }
            static void Main(string[] args) {
                // string connectionString = @"Server=KAHDYMUS-PC\SQLEXPRESS;Database=master;Trusted_Connection=True;";
                string connectionString = @"Server=DESKTOP-UK193S6\SQLEXPRESS;Database=DESAFIO;Trusted_Connection=True;";
                SqlConnection con = new(connectionString);

                User user = new();

                PasswordHasher passwordHasher = new();

                string SecurePass;
                SecurePass = PasswordHasher.HashPassword(user.Password);

                GiveBalance(user,1, 10, con);

                // DeductBalance(user, con);

                // CreateUser(user, SecurePass, con);

                // string sqlselect = @"INSERT INTO USERS ( EMAIL, PASSWD, PHONE, CPF, BALANCE)VALUES ('DASAD@DASDSA', '213131', 312231, 3131222, 0);";
            }
    }
}