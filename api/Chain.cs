namespace blockchainC_
{
    public class Chain
    {
        public int security {get; set;}
        public int gain {get; set;}
        public string sigle {get; set;}
        public List<Block>chain {get ; set;} 
        public List<Transaction>attente {get; set;} 
        


        //Constructeur 
        public Chain(int security, int gain, string sigle){
            this.security = security ;
            this.gain = gain ;
            this.sigle = sigle ; 
            this.chain = new List<Block>();
            this.attente = new List<Transaction>();
            initialiserChain();
        }

        public Chain(int security){
            this.security = security ;
            this.gain = 0 ;
            this.sigle = "" ; 
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
        public void ajoutBlockAttente (Wallet wal){
            Block nvBlock = new Block();
            for (int i = 0; i < this.attente.Count; i++)
            {
                nvBlock.transactions.Add(this.attente[i]);
            }
            nvBlock.previousHash = this.getLastBlock().hash;
            nvBlock.mineBlock(this.security);
            this.chain.Add(nvBlock);
            /*
            * On vide les transactions en attente et on y ajoute celle du mining qui vient d'etre effectué
            */
            this.attente.RemoveRange(0, this.attente.Count);
            Transaction nvTransaction = new Transaction(this.gain, this.sigle , wal.publicKey, "NULL");
            nvTransaction.signTransaction(wal.cert); 
            this.attente.Add(nvTransaction);
        }
        //surcharge de la fonction pour la rende viable pour une blockchain message/systeme de vote 
        public void ajoutBlockAttente (){
            Block nvBlock = new Block();
            for (int i = 0; i < this.attente.Count; i++)
            {
                nvBlock.transactions.Add(this.attente[i]);
            }
            nvBlock.previousHash = this.getLastBlock().hash;
            nvBlock.mineBlock(this.security);
            this.chain.Add(nvBlock);
            /*
            * On vide les transactions en attente
            */
            this.attente.RemoveRange(0, this.attente.Count);      
        }
        
        /*
        * Fonction qui permet d'ajouter une transaction à la liste de celles déjà en attente
        */
        public void ajoutTransaction (Transaction nvTransaction){

            if(nvTransaction.isValid() == false){
                throw new Exception ("Transaction non valide");
            }
            
            this.attente.Add(nvTransaction);
        }

        public Boolean validChain(){
            // On vérifie manuellement pour le bloc d'origine 
            if(this.chain[0].hash != this.chain[0].calculateHash()){
                Console.WriteLine("pb origine");
                return false ; 
            }
            // On part du max pour retourner au bloc d'origine sans vérifier pour lui car il n'a pas de previous hash 
            for (int i = this.chain.Count; i > 1; i--)
            {
                Block actualBlock =  this.chain[i-1]; 
                Block nextBlock = this.chain[i-2];

                if(actualBlock.validTransaction() == false){
                    Console.WriteLine("pb signature");
                    return false ; 
                }

                if( actualBlock.hash != actualBlock.calculateHash()){
                    Console.WriteLine("pb hash");
                    return false ;
                }
                if (actualBlock.previousHash != nextBlock.hash){
                    Console.WriteLine("pb hash suivant");
                    return false ; 
                }
            }
            return true ; 
        }

        public int getBalanceOfAdress(string? adress){
            int res = 0 ; 
            foreach (Block block in chain)
            {
                foreach (Transaction tx in block.transactions)
                {
                    if (tx.receptionAdress == adress)
                    {
                        res = res + tx.data.montant ; 
                    }
                    if (tx.envoiAdress == adress)
                    {
                        res = res - tx.data.montant ;
                    }
                }
            }
            return res ;
        }

        public Boolean verifVote(string address){
            foreach(Block block in chain){
                foreach (Transaction tx in block.transactions)
                {
                    if (address == tx.envoiAdress){
                        return false ; 
                    }
                }
            }
            foreach (Transaction tx in attente)
            {
                if (address == tx.envoiAdress){
                        return false ; 
                    }
            }
            return true ; 
        }

        public void compterVote(){
            List<string> resultat = new List<string>();
            foreach (Block block in chain)
            {
                foreach (Transaction tx in block.transactions)
                {
                    if(tx.data.nom is null){tx.data.nom="";}
                    //passage de tout en majuscule pour faciliter le decompte
                    resultat.Add(tx.data.nom.ToUpper());
                }
            }
            Console.WriteLine("Nombre de vote: "+resultat.Count);
        }
        // Fonction qui depouille les voix et les comptabilise
        public void depouillage(List<string> candidat){
            List<string> resultat = new List<string>();
            foreach (Block block in chain)
            {
                foreach (Transaction tx in block.transactions)
                {
                    if(tx.data.nom is null){tx.data.nom="";}
                    //passage de tout en majuscule pour faciliter le decompte
                    resultat.Add(tx.data.nom.ToUpper());
                }
            } 
            foreach (string cand in candidat)
            {
                int temp = 0 ; 
                foreach (string vote in resultat)
                {
                    if (cand.ToUpper() == vote)
                    {
                        temp ++ ; 
                    }
                }
                Console.WriteLine(cand + " nb voix : "+ temp);
            }
                Console.WriteLine("Nb Total voix :"+resultat.Count);
        }
    }
}