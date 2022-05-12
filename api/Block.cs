using System.Text.Json.Serialization;
using System.Text.Json;
using System.Security.Cryptography;
using System.Text;
namespace blockchainC_
{
    /*
    *
    * Idtotal est à usage de variable globale
    *
    */
    static class Compteur {
        public static int idtotal = 0 ;
    }
    public class Block
    {
        public String timestamp {get ; set;}    
        public String previousHash {get ; set;}   
        /* Pour lier les blocs et la vérification  */
        public String hash {get ; set;}  
        /* Hash du bloc actuel */
        public int id {get ; set;} 
        /* n° du bloc */ 
        public ulong salt {get ; set;} 
        /*salt utile pour securiser la blockchain en plus du hash*/
        public List<Transaction> transactions {get; set;}
        
        //Constructeur
        public Block(){
            this.timestamp = GetTimestamp(DateTime.Now);
            this.transactions = new List<Transaction>(); 
            this.previousHash = "NULL" ; 
            this.hash = this.calculateHash();
            this.id = Compteur.idtotal ; 
            this.salt = 0 ; 
            Compteur.idtotal ++ ; 
        }

        public String calculateHash(){
            //Recuperation des infos du bloc sous forme de string
            // On utilise un JSONStringify pour les transactions 
            string temp = (this.id  + this.previousHash + this.timestamp + this.salt + JsonSerializer.Serialize(this.transactions).ToString()) ;
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
        /*
        *
        * Fonction qui renvoie un timeStamp dans un String
        *
        */
        public String GetTimestamp(DateTime value)
        {
            //Calcul du temps lors de la creation du block impossible de falsifier
            return value.ToString("yyyyMMddHHmmssffff");
        }
        /*
        *
        *   Fonction qui implément le POW, via la valeur security on definit le nombre de 0 
        *   que l'on souhaite au début de cahque hash : Cela complexifie le calcul et rend a blockchain plus sécurisé
        *   Pour la falsifier le malfaiteur devra avoir une grosse puissance de calcul (exemple du Bitcoin)
        *   Afin de changer le hash on doit modifier l'une des valeurs que l'on prends pour le calculer 
        *   d'où l'ajout de la variable salt, qui s'incrémente au fur et à mesure des essais
        */
        public void mineBlock (int security){
                Console.WriteLine("Minage du bloc en cours...");
                List <String> comparTab = new List<string>() ; 
                for (int i = 0; i < security; i++)
                {
                    comparTab.Add("0");
                }
                string? compar = String.Join("",comparTab);
                while (String.Compare(this.hash, 0, compar, 0, security)!=0)
                {
                    this.salt ++ ; 
                    this.hash = calculateHash();
                    if (this.salt %10000 == 0){
                        Console.Write(".");
                    } 
                }
                Console.WriteLine("Bloc miné !");
            }
        }
}