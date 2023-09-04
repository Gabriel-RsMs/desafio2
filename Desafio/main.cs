using system;
using system.IO;


namespace desafio2{
    class desafio2{
        void main(string[] args) {
            SqlConnection con = new SqlConnection(connectionString);
    
            static enum UserType(){
                admin,
                common
            }

            void RequestBD(SQL){
                SqlCommand cmd = new SqlCommand(sql, con);
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
                return RequestID(CheckSQL);
            }

            int CheckType(USER){
                string CheckSQL = "SELECT FROM USERS ACCOUNTTYPE WHERE USERID = " + USER.ID + ";";
                return RequestBD(CheckSQL);
            }

            void GiveBalance(USER, ReceiverID, value){
                if()
                string GiveSQL = "INSERT INTO USERS (BALANCE) VALUES ("+ value +") WHERE ACCOUNTID = " + ReceiverID + ";";
                SendToBD(GiveSQL);
            } 

            void DeductBalance(USER){
                
                if(CheckSQL == 0){
                    return "A conta náo possui saldo";
                
                }
                
                string DeductSQL = "UPDATE USERS SET BALANCE = BALANCE - 1 WHERE " + USER.ID + ";" ;

            }

            void CreateUser(USER){
                string InsertSQL = "INSERT INTO USERS (EMAIL, PASSWD) " + " VALUES ('" + USER.EMAIL + "', '" + USER.PASSWD + "');";
                SendToBD(InsertSQL);
            }
        }
    }
}