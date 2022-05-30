
int choix = 1;
int valeur, gain ;  
var options = new JsonSerializerOptions {WriteIndented = true};
String? temp ; 
String? address , address2;
String jsonString ;
List<Wallet> wallets = new List<Wallet>() ;  

Console.Write("Choisir le niveau de sécurité : ");
temp = Console.ReadLine();
if (int.TryParse(temp, out choix)){} else {};
Console.Write("Déterminer le montant du gain pour le minage d'un bloc : ");
temp = Console.ReadLine();
if (int.TryParse(temp, out gain)){} else {};
var myBlockchain = new Chain(choix, gain);
choix = 1 ; 
while(choix > 0 && choix < 7){
    Console.WriteLine("Que voulez vous faire ? \n1 : Ajoutez une transaction \n2 : Affichez l'etat de votre blockchain\n3 : Miner le prochain block, indiquez votre adresse(publicKey) pour les rewards\n4 : Vérifiez l'intégrité de la blockchain\n5 : Créer un Wallet\n 6 : Checker la balance de cette adresse(publicKey):\n0 : Quitter la simulation");
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
                                        Transaction nvTransaction = new Transaction (valeur, address2, address);
                                        nvTransaction.signTransaction(wal.cert);
                                        myBlockchain.ajoutTransaction(nvTransaction);
                                        //On laisse les montants negatifs par soucis de facilité d'utilisation
                                        wal.balance=wal.balance - valeur ;
                                        walR.balance = walR.balance + valeur ; 
                                }
                        }
                        
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
                        myBlockchain.ajoutBlockAttente(address);
                    }
                }
        break  ;
        case 4 : 
                    Console.WriteLine(myBlockchain.validChain());
        break  ;
        case 5 : 
                Wallet wallet = new Wallet();
                wallets.Add(wallet);
        break ; 
        case 6 : 
                Console.WriteLine("Indiquez votre adresse (PublicKey) : ");
                address = Console.ReadLine();
                foreach (Wallet wal in wallets)
                {
                    if (address == wal.publicKey){
                       Console.WriteLine(wal.getBalance() ); 
                    }
                }
        break ;
        default: break ; 
    }
}/*
Wallet wal = new Wallet();
var envoiAdress = "thibaut";
var receptionAdress = "arthur";
var signature = "NULL" ; 
var data = 12 ; 

var cert = new CertificateRequest("cn=Test", ECDsa.Create(ECCurve.NamedCurves.nistP256), HashAlgorithmName.SHA256)
                .CreateSelfSigned(DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(2));



byte[] myhash = Encoding.ASCII.GetBytes(envoiAdress + receptionAdress + data);
            var sig = cert.GetECDsaPrivateKey().SignData(myhash,HashAlgorithmName.SHA256); 
            var sigtemp = cert.GetECDsaPrivateKey().ExportECPrivateKey(); 
            var publicKey = cert.GetECDsaPublicKey().ExportSubjectPublicKeyInfo();
            string testprv = Convert.ToBase64String(sigtemp);
            string test = Convert.ToBase64String(publicKey);
            //test = test.Replace("-", "");
            byte[]? repbk = Convert.FromBase64String(test); 
            byte[]sigtemp2 = Convert.FromBase64String(testprv);
            Console.WriteLine("pbk :"+ test);
            signature = Convert.ToBase64String(sig);
            Console.Write("signature : ");
            Console.WriteLine(signature);
            int temp;
            ECDsa ecr = ECDsa.Create();
            ecr.ImportSubjectPublicKeyInfo(repbk, out temp);
            ECDsa ecr2 = ECDsa.Create();
            ecr2.ImportECPrivateKey(sigtemp2,out temp);
            var final = ecr2.SignData(sigtemp, HashAlgorithmName.SHA256);
            string fin = Convert.ToBase64String(final);
            Console.WriteLine("siganture :"+fin);
            Console.WriteLine(ecr.VerifyData(myhash,sig, HashAlgorithmName.SHA256));
            Console.WriteLine(cert.GetECDsaPublicKey().VerifyData(myhash,sig, HashAlgorithmName.SHA256));

*/




