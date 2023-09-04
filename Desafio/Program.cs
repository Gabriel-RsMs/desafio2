using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


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


            static int CalcExpense(string Message){
                float res = (float)Message.Length/153;
                int rounded = (int)Math.Ceiling(res);
                return rounded;
        }

        static void Main(string[] args) {
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

            int CheckBalance(USER){
                string CheckSQL = "SELECT FROM USERS BALANCE WHERE USERID = " + USER.ID + ";";
                return RequestBD(CheckSQL);
            }

            int CheckType(USER){
                string CheckSQL = "SELECT FROM USERS ACCOUNTTYPE WHERE USERID = " + USER.ID + ";";
                return RequestBD(CheckSQL);
            }

            void GiveBalance(USER, ReceiverID, value){
                if(CheckType == 0){
                    return "nuh uh vc n e admin";
                }
                string GiveSQL = "INSERT INTO USERS (BALANCE) VALUES ("+ value +") WHERE ACCOUNTID = " + ReceiverID + ";";
                SendToBD(GiveSQL);
            } 

            void DeductBalance(USER){
                
                if(CheckBalance == 0){
                    return "A conta náo possui saldo";
                }
                
                string DeductSQL = "UPDATE USERS SET BALANCE = BALANCE - 1 WHERE " + USER.ID + ";" ;

            }

            void CreateUser(USER){
                string InsertSQL = "INSERT INTO USERS (EMAIL, PASSWD) " + " VALUES ('" + USER.EMAIL + "', '" + USER.PASSWD + "');";
                SendToBD(InsertSQL);
            }*/
    }
}