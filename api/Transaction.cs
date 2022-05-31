namespace blockchainC_{
    public class Transaction
    {
        public String receptionAdress {get; set;}
        public String envoiAdress {get; set;}
        public Data data {get; set;}

        public String signature {get ; set;} 
        
        //Constructeur

        //Transaction pour uen cryptocurrency
        public Transaction(int montant, string sigle, string receptionAdress, string envoiAdress){
            this.receptionAdress = receptionAdress;
            this.envoiAdress = envoiAdress;
            this.data = new Data(montant, sigle);
            this.signature = "NULL"; 
        }

        //Transaction pour une election/chat
        public Transaction(string nom, string receptionAdress, string envoiAdress){
            this.receptionAdress = receptionAdress;
            this.envoiAdress = envoiAdress;
            this.data = new Data(nom);
            this.signature = "NULL"; 
        }


        
        public void signTransaction(X509Certificate2? signingkey){
            if (signingkey is null)
            {
                throw new Exception("Certificat non crée pour ce wallet");
            }
            byte[] myhash = Encoding.ASCII.GetBytes(this.envoiAdress + this.receptionAdress + this.data);
            ECDsa? temp = signingkey.GetECDsaPrivateKey();
            if (temp is null)
            {
                throw new Exception("Pas de clé RSA pour signer la transaction");
            }
            var sig = temp.SignData(myhash,HashAlgorithmName.SHA256);  
            this.signature = Convert.ToBase64String(sig);
        }

        public bool isValid(){
            if (this.envoiAdress == "NULL"){
                return true ; 
            }

            if(this.signature == "NULL"){
                throw new Exception("Pas de signature");
            }
            byte[] myhash = Encoding.ASCII.GetBytes(this.envoiAdress + this.receptionAdress + this.data);
            ECDsa ecr = ECDsa.Create();
            int temp ;
            byte[] publicKey = Convert.FromBase64String(this.envoiAdress);
            ecr.ImportSubjectPublicKeyInfo(publicKey, out temp);
            return ecr.VerifyData(myhash, Convert.FromBase64String(this.signature), HashAlgorithmName.SHA256) ; 
        }
    }
}