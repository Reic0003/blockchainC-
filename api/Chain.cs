namespace blockchainC_
{
    public class Chain
    {
        public List<Block>chain {get ; set;}  

        //Constructeur 
        public Chain(){
            this.chain = new List<Block>();
            initialiserChain();
        }


        public void initialiserChain(){
            //Creation du premier block de la chaine  
            Block firstBlock = new Block (0);
            this.chain.Add(firstBlock); 
        }

        public Block getLastBlock(){
            return this.chain[this.chain.Count -1];
        }

        public void ajouterBlock(int data){
            //On creer les liens pour lier les blocks 
            Block nvBlock = new Block(data);
            nvBlock.previousHash = this.getLastBlock().hash;
            //On doit recalculer son hash maintenant que son previous hash a changé pour ne pas fausser la blockchain
            nvBlock.hash = nvBlock.calculateHash();
            this.chain.Add(nvBlock);
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