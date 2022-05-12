
//  ...later on in the code
int choix = 1;
int valeur, gain ;  
var options = new JsonSerializerOptions {WriteIndented = true};
String? temp ; 
String? address , address2;
String jsonString ;
Console.Write("Choisir le niveau de sécurité : ");
temp = Console.ReadLine();
if (int.TryParse(temp, out choix)){} else {};
Console.Write("Déterminer le montant du gain pour le minage d'un bloc : ");
temp = Console.ReadLine();
if (int.TryParse(temp, out gain)){} else {};
var myBlockchain = new Chain(choix, gain);
choix = 1 ; 
while(choix > 0 && choix < 5){
    Console.WriteLine("Que voulez vous faire ? \n1 : Ajoutez une transaction \n2 : Affichez l'etat de votre blockchain\n3 : Miner le prochain block\n4 : Vérifiez l'intégrité de la blockchain\n0 : Quitter la simulation");
    temp = Console.ReadLine();
   if (int.TryParse(temp, out choix)){} else {};
    //Méthode archaïque mais c'st pour tester toutes les fonctionnalités de la blockchain
    switch (choix)
    {
        case 1 : 
                Console.WriteLine("Indiquez le montant de votre transaction");
                temp = Console.ReadLine();
                if (int.TryParse(temp, out valeur)){} else {};
                Console.WriteLine("Indiquez votre adresse : ");
                address = Console.ReadLine();
                Console.WriteLine("Indiquez l'adresse réceptrice : ");
                address2 = Console.ReadLine();
                myBlockchain.ajoutTransaction(address, address2, valeur);
        break ;
        case 2: 
                jsonString = JsonSerializer.Serialize<Chain>(myBlockchain, options);
                Console.WriteLine(jsonString);
        break  ;
        case 3: 
                myBlockchain.ajoutBlockAttente("Thibaut");
        break  ;
        case 4 : 
                    Console.WriteLine(myBlockchain.validChain());
        break  ;
        default: break ; 
    }
}








