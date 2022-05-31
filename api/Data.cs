namespace blockchainC_
{
    public class Data
    {
        public int montant {get; set;}

        public string? nom {get; set;}


        // Constructeurs 
        //Constructeur pour le cas d'une cryptomonnaie
        public Data(int montant, string sigle){
            this.montant = montant ; 
            this.nom = sigle ; 
        }

        //Constructeur pour le cas d'une election 

        public Data (string nom){
            this.nom = nom ; 
            this.montant = 0 ; 
        }
    }
    
}