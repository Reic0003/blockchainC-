
int choix = 1;
int valeur, gain ;  
var options = new JsonSerializerOptions {WriteIndented = true};
String? temp, sigle ; 
String? address , address2;
String jsonString ;
List<Wallet> wallets = new List<Wallet>() ; 
Console.WriteLine("\nChoisir quel type de blockchain voulez vous mettre en place \n1: CryptoBlockchain\n2: Blockchain élection");
temp = Console.ReadLine(); 
if (int.TryParse(temp, out choix)){} else {};
switch (choix)
{
        case 1:
                Console.Write("Choisir le niveau de sécurité : ");
                temp = Console.ReadLine();
                if (int.TryParse(temp, out choix)){} else {};
                Console.Write("Déterminer le montant du gain pour le minage d'un bloc : ");
                temp = Console.ReadLine();
                if (int.TryParse(temp, out gain)){} else {};
                Console.Write("Déterminer le nom de votre monnaie : ");
                sigle = Console.ReadLine();
                if (sigle is null)
                {
                        sigle ="";
                }
                var myBlockchain = new Chain(choix, gain, sigle);
                choix = 1 ; 
                while(choix > 0 && choix < 7){
                        Console.WriteLine("Que voulez vous faire ? \n1 : Ajoutez une transaction de pair à pair \n2 : Affichez l'etat de votre blockchain\n3 : Miner le prochain block, indiquez votre adresse(publicKey) pour les rewards\n4 : Vérifiez l'intégrité de la blockchain\n5 : Créer un Wallet\n6 : Checker la balance de cette adresse(publicKey):\n0 : Quitter la simulation");
                        temp = Console.ReadLine();
                        if (int.TryParse(temp, out choix)){} else {};
                        //Méthode archaïque mais c'st pour tester toutes les fonctionnalités de la blockchain
                        switch (choix)
                        {
                                case 1 : 
                                        Console.WriteLine("Indiquez votre adresse(privateKey) : ");
                                        address = Console.ReadLine();
                                        foreach (Wallet wal in wallets)
                                        {
                                        if (address == wal.privateKey)
                                        {
                                                address = wal.publicKey;
                                                Console.WriteLine("Indiquez le montant de votre transaction");
                                                temp = Console.ReadLine();
                                                if (int.TryParse(temp, out valeur)){} else {};
                                                
                                                Console.WriteLine("Indiquez l'adresse réceptrice(publicKey) : ");
                                                address2 = Console.ReadLine(); 
                                                foreach (Wallet walR in wallets)
                                                {
                                                        if(address2 == walR.publicKey){
                                                                Transaction nvTransaction = new Transaction(valeur, sigle, address2, address);
                                                                nvTransaction.signTransaction(wal.cert);
                                                                myBlockchain.ajoutTransaction(nvTransaction);
                                                        }
                                                }
                                                
                                        }else{
                                                Console.WriteLine("Wallet associé à cette clé inexistant");
                                        }    
                                        }
                                break ;
                                case 2: 
                                        jsonString = JsonSerializer.Serialize<Chain>(myBlockchain, options);
                                        Console.WriteLine(jsonString);
                                break  ;
                                case 3: 
                                        Console.WriteLine("Indiquez votre adresse : ");
                                        address = Console.ReadLine();
                                        foreach (Wallet wal in wallets)
                                        {
                                        if (address == wal.publicKey){
                                                myBlockchain.ajoutBlockAttente(wal);
                                        }
                                        }
                                break  ;
                                case 4 : 
                                        Console.WriteLine(myBlockchain.validChain());
                                break  ;
                                case 5 : 
                                        Wallet wallet = new Wallet();
                                        wallet.exportKey();
                                        wallets.Add(wallet);
                                break ; 
                                case 6 : 
                                        Console.WriteLine("Indiquez votre adresse (PublicKey) : ");
                                        address = Console.ReadLine();
                                        Console.WriteLine("solde :" +myBlockchain.getBalanceOfAdress(address) +myBlockchain.sigle);
                                break ;
                                default: break ; 
                        }
                }
        break ; 
    
        case 2:
                Console.Write("Choisir le niveau de sécurité : ");
                temp = Console.ReadLine();
                if (int.TryParse(temp, out choix)){} else {};
                var myBlockchainElection = new Chain(choix);
                List<string> candidat = new List<string>();
                candidat.Add("");
                while(choix > -1 && choix < 10){
                        Console.WriteLine("Que voulez vous faire ?\n1 : Ajoutez un vote dans la blockchain\n2 : Affichez l'etat de votre blockchain\n3 : Miner le prochain block pour inscrire les votes dans la blockchain\n4 : Vérifiez l'intégrité de la blockchain\n5 : Créer un Wallet \n6 : Ajouter un candidat\n7 : Générer 100 vote aléatoire parmi les candidats\n8 : Compter les votes\n9 : Dépouiller les votes \n0 : Afficher la liste de canddiat");
                        temp = Console.ReadLine();
                        if (int.TryParse(temp, out choix)){} else {};
                        switch (choix)
                        {
                                case 1: Console.WriteLine("Indiquez votre adresse(privateKey) : ");
                                        address = Console.ReadLine();
                                        foreach (Wallet wal in wallets)
                                        {
                                                //On verifie que l'adresse existe et qu'elle n'a jamais été utlisé pour voter 
                                                if (address == wal.privateKey)
                                                {
                                                        address = wal.publicKey;
                                                        if(myBlockchainElection.verifVote(address) == false){
                                                                Console.WriteLine("Vous avez déjà voter");
                                                                break ; 
                                                        }
                                                        Console.WriteLine("Indiquez le nom du candidat");
                                                        temp = Console.ReadLine();
                                                        if (temp is null) temp = "";

                                                        Transaction nvTransaction = new Transaction(temp,"NULL",address);
                                                        nvTransaction.signTransaction(wal.cert);
                                                        myBlockchainElection.ajoutTransaction(nvTransaction); 
                                                }    
                                        }
                                break ; 
                                case 2: 
                                        jsonString = JsonSerializer.Serialize<Chain>(myBlockchainElection, options);
                                        Console.WriteLine(jsonString);
                                break  ;
                                case 3: 
                                //Comme le but n'est pas un systeme economique viable on estime que c'est l'etat/l'institution qui mine les votes 
                                        
                                        myBlockchainElection.ajoutBlockAttente();
                                break  ;
                                case 4 : 
                                        Console.WriteLine(myBlockchainElection.validChain());
                                break  ;
                                case 5 : 
                                        Wallet wallet = new Wallet();
                                        wallet.exportKey();
                                        wallets.Add(wallet);
                                break ; 
                                case 6 : Console.WriteLine("Indiquez le nom du nouveau candidat : ");
                                        address = Console.ReadLine();
                                        if(address is null){Console.WriteLine("Candidat non valide");break;}
                                        var tempInt =  0; 
                                        foreach (string cand in candidat)
                                        {
                                                if(address.ToUpper() == cand.ToUpper()){
                                                        Console.WriteLine("Candidat déjà présenté");
                                                        tempInt = 1 ; 
                                                }
                                        }
                                        if(tempInt  == 0){
                                                candidat.Add(address);
                                        }
                                        
                                break  ;
                                case 7 :
                                        if (candidat.Count == 0)
                                        {
                                                break ;
                                        }
                                        Random rand = new Random();
                                        for (int i = 0; i < 100; i++){
                                                Wallet tempWal = new Wallet();
                                                
                                                Transaction tempTx = new Transaction(candidat[rand.Next()%candidat.Count],"NULL",tempWal.publicKey);
                                                tempTx.signTransaction(tempWal.cert);
                                                myBlockchainElection.ajoutTransaction(tempTx);
                                                if(i %10 == 0 ){
                                                        myBlockchainElection.ajoutBlockAttente();
                                                }
                                        }
                                
                                        
                                break  ;
                                case 8 : 
                                        myBlockchainElection.compterVote();
                                break  ;
                                case 9 : 
                                        myBlockchainElection.depouillage(candidat);
                                break  ;
                                case 0 : 
                                        foreach (string cand in candidat)
                                        {
                                            Console.WriteLine(cand);    
                                        }
                                break ;
                                
                                default: break ; 
                        //fin switch blockchainElection
                        }
                //fin while
                }
        break ; 

        default: throw new Exception("Aucun choix valide");
}






