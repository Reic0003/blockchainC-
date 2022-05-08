using System.Text.Json.Serialization;
using System.Text.Json;
using System.Security.Cryptography;
using System.Text;
namespace blockchainC_
{
    static class Compteur {
        public static int idtotal = 0 ;
    }
    public class Block
    {
        public String timestamp {get ; set;}  
        public int data {get ; set;}   
        public String previousHash {get ; set;}   
        /* Pour lier les blocs et la vérification  */
        public String hash {get ; set;}  
        /* Hash du bloc actuel */
        public int id {get ; set;} 
        /* n° du bloc */ 
        
        //Constructeur
        public Block(int data){
            this.data = data ; 
            this.timestamp = GetTimestamp(DateTime.Now); 
            this.previousHash = "previousHash" ; 
            this.hash = this.calculateHash();
            this.id = Compteur.idtotal ; 
            Compteur.idtotal ++ ; 
        }

        public String calculateHash(){
            //Recuperation des infos du bloc sous forme de string
            // On utilise un JSONStringify pour la data dans le cadre d'une structure de données
            string temp = (this.id  + this.previousHash + this.timestamp + JsonSerializer.Serialize(this.data).ToString()) ;
            //hashage en SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(temp));
                //Console.WriteLine(bytes);
                StringBuilder tempHash = new StringBuilder();
                for (int i = 0 ; i < bytes.Length; i++){
                    //coersion en hexadécimal
                    tempHash.Append(bytes[i].ToString("X2"));
                }
                return tempHash.ToString() ;
            }   
        }

        public String GetTimestamp(DateTime value)
        {
            //Calcul du temps lors de la creation du block impossible de falsifier
            return value.ToString("yyyyMMddHHmmssffff");
        }
        public void afficherIdBlock(){
            Console.WriteLine(this.id);
        }
    }
}