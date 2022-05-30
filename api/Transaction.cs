namespace blockchainC_{
    public class Transaction
    {
        public String receptionAdress {get; set;}
        public String envoiAdress {get; set;}
        public int data {get; set;}

        public String signature {get ; set;} 
        
        //Constructeur
        public Transaction(int data, string receptionAdress, string envoiAdress){
            this.receptionAdress = receptionAdress;
            this.envoiAdress = envoiAdress;
            this.data = data;
            this.signature = "NULL"; 
        }


        
        public void signTransaction(X509Certificate2 signingkey){

            byte[] myhash = Encoding.ASCII.GetBytes(this.envoiAdress + this.receptionAdress + this.data);
            var sig = signingkey.GetECDsaPrivateKey().SignData(myhash,HashAlgorithmName.SHA256);  
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