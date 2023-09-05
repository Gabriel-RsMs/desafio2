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
            // string connectionString = @"sqlConnection";
        // SqlConnection con = new SqlConnection(connectionString);

        // subir um banco de testes, montar a classe USER, dar uma olhada em como fazer o front(por enqt so fiz a parte dos models) com a tecnologia que o diego mencionou, perguntar para ele amanha
        // subir o codigo da moura para o git para consulta, estudar a API 


            enum UserType{
                admin,
                common
            }

            static string Error(){
                return "Alguma falha ocorreu, tente novamente mais tearde";
            }

            static int CalcExpense(string Message){
                float res = (float)Message.Length/153;
                int rounded = (int)Math.Ceiling(res);
                return rounded;
        }

        /*
                    void RequestBD(SQL){
                        SqlCommand cmd = new SqlCommand(SQL, con);
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader reader;
                        con.Open();
                        try{
                            reader = cmd.ExecuteReader();
                        }catch(Exception ex){
                            MessageBox.Show("Erro: " + ex.ToString());
                        }
                        finally{
                            con.Close();
                        }  
                    }

                    void SendToBD(SQL){

                        SqlCommand cmd = new SqlCommand(SQL, con);
                        cmd.CommandType = CommandType.Text;
                        con.Open();

                        try{
                            int i = cmd.ExecuteNonQuery();
                            if (i > 0)
                                MessageBox.Show("Processo realizado com sucesso!");
                        }
                        catch (Exception ex){
                            MessageBox.Show("Erro: " + ex.ToString());
                        }
                        finally{
                            con.Close();
                        }
                    }
                    
                    void CheckBalance(User user){
                        string CheckSQL = "SELECT BALANCE FROM USERS WHERE ACCOUNTID = " + user.ID + ";";
                        return RequestBD(CheckSQL);
                    }

                    int CheckType(User user){
                        string CheckSQL = "SELECT ACCOUNTTYPE FROM USERS WHERE ACCOUNTID = " + user.ID + ";";
                        return RequestBD(CheckSQL);
                    }
                    
                    static string GiveBalance(User user, int ReceiverID, int value){
                        if(CheckType == 0){
                            Error();
                        }
                        string GiveSQL = "UPDATE USERS SET BALANCE = BALANCE + "+ value +" WHERE ACCOUNTID = " + ReceiverID + ";";
                        SendToBD(GiveSQL);
                    } 
                    void DeductBalance(User user){

                        if(CheckBalance == 0){
                             Error();
                        }
                        string DeductSQL = "UPDATE USERS SET BALANCE = BALANCE - 1 WHERE ACCOUNTID = " + user.ID + ";" ;
                        SendToBD(DeductSQL);
                    }
                    void CreateUser(User user, string SecPass){
                        string InsertSQL = "INSERT INTO USERS (EMAIL, PASSWD, PHONE, CPF) " + " VALUES ('" + user.Email + "', '" + SecPass + "', '" + user.PHONE "', '" + user.CPF + "');";
                        SendToBD(InsertSQL);
            }
        */
            static void Main(string[] args) {
                User user = new User();
                PasswordHasher passwordHasher = new PasswordHasher();

                string SecurePass = PasswordHasher.HashPassword(user.Password);
            }
    }
}