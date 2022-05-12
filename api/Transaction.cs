namespace blockchainC_{
    public class Transaction
    {
        public String receptionAdress {get; set;}
        public String envoiAdress {get; set;}
        public int data {get; set;}
        
        //Constructeur
        public Transaction(int data, string receptionAdress, string envoiAdress){
            this.receptionAdress = receptionAdress;
            this.envoiAdress = envoiAdress;
            this.data = data;
        }
    }
}