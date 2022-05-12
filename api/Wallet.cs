namespace blockchainC_
{
    public class Wallet
    {
        public string publicKey {get; set;}
        private string privateKey {get; set;}
        public int balance {get; set;}
        
        public Wallet(){
            this.publicKey ="a" ; 
            this.privateKey="a" ; 
            this.balance = 0 ; 
        }
    }
}