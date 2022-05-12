namespace blockchainC_
{
    public class Chain
    {
        public int security {get; set;}
        public List<Block>chain {get ; set;} 
        public List<Transaction>attente {get; set;} 
        public int gain {get; set;}


        //Constructeur 
        public Chain(int security, int gain){
            this.security = security ;
            this.gain = gain ; 
            this.chain = new List<Block>();
            this.attente = new List<Transaction>();
            initialiserChain();
        }


        public void initialiserChain(){
            //Creation du premier block de la chaine  
            Block firstBlock = new Block ();
            firstBlock.mineBlock(this.security);
            this.chain.Add(firstBlock); 
        }

        public Block getLastBlock(){
            return this.chain[this.chain.Count -1];
        } 

        
        /*
        *
        *
        * Fonction qui ajoute toutes les transactions en attente dans le block 
        *   puis les réalise, elle reverse un gain au mineur
        */
        public void ajoutBlockAttente (string miningAdress){
            Block nvBlock = new Block();
            for (int i = 0; i < this.attente.Count; i++)
            {
                nvBlock.transactions.Add(this.attente[i]);
            }
            nvBlock.mineBlock(this.security);
            this.chain.Add(nvBlock);
            /*
            * On vide les transactions en attente et on y ajoute celle du mining qui vient d'etre effectué
            */
            this.attente.RemoveRange(0, this.attente.Count);
            Transaction nvTransaction = new Transaction(this.gain, miningAdress, "NULL"); 
            this.attente.Add(nvTransaction);
        }
        
        /*
        * Fonction qui permet d'ajouter une transaction à la liste de celles déjà en attente
        */
        public void ajoutTransaction (string receptionAddress, string envoiAddress, int data){
            Transaction nvTransaction = new Transaction(data, receptionAddress, envoiAddress);
            this.attente.Add(nvTransaction);
        }

        public Boolean validChain(){
            // On vérifie manuellement pour le bloc d'origine 
            if(this.chain[0].hash != this.chain[0].calculateHash()){
                return false ; 
            }
            // On part du max pour retourner au bloc d'origine sans vérifier pour lui car il n'a pas de previous hash 
            for (int i = this.chain.Count; i > 2; i--)
            {
                Block actualBlock =  this.chain[i-1]; 
                Block nextBlock = this.chain[i-2];
                if( actualBlock.hash != actualBlock.calculateHash()){
                    return false ;
                }
                if (actualBlock.previousHash != nextBlock.hash){
                    return false ; 
                }
            }
            return true ; 
        }
    }
}