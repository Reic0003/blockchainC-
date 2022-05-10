
//  ...later on in the code
int choix = 1;
int valeur, fake ;  
var options = new JsonSerializerOptions {WriteIndented = true};
String? temp ; 
String jsonString ;
Console.Write("Choisir le niveau de sécurité : ");
temp = Console.ReadLine();
if (int.TryParse(temp, out choix)){} else {};
Console.WriteLine("Minage du premier bloc en cours ...");
var myBlockchain = new Chain(choix);
choix = 1 ; 
while(choix > 0 && choix < 5){
    Console.WriteLine("Que voulez vous faire ? \n1 : Ajoutez une transaction \n2 : Affichez l'etat de votre blockchain\n3 : Faussez un block\n4 : Vérifiez l'intégrité de la blockchain\n0 : Quitter la simulation");
    temp = Console.ReadLine();
   if (int.TryParse(temp, out choix)){} else {};
    //Méthode archaïque mais c'st pour tester toutes les fonctionnalités de la blockchain
    switch (choix)
    {
        case 1 : 
                Console.WriteLine("Indiquez le montant de votre transaction");
                temp = Console.ReadLine();
                if (int.TryParse(temp, out valeur)){} else {};
                Console.WriteLine("Minage du bloc en cours...");
                myBlockchain.ajouterBlock(valeur);
                Console.WriteLine("Bloc ajouté !");
        break ;
        case 2: 
                jsonString = JsonSerializer.Serialize<Chain>(myBlockchain, options);
                Console.WriteLine(jsonString);
        break  ;
        case 3 : 
                Console.WriteLine("Indiquez l'index du bloc que vous voulez fausser");
                temp = Console.ReadLine();
                if (int.TryParse(temp, out fake)){} else {};
                Console.WriteLine("Indiquez le nouveau montant");
                temp = Console.ReadLine();
                if (int.TryParse(temp, out valeur)){} else {};
                myBlockchain.chain[fake].data = valeur ; 
        break  ;
        case 4 : 
                    Console.WriteLine(myBlockchain.validChain());
        break  ;
        default: break ; 
    }
}








