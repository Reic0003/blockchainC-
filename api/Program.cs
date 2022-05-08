
//  ...later on in the code
int choix = 1;
int valeur, fake ;  
var options = new JsonSerializerOptions {WriteIndented = true};
String jsonString ; 
Console.WriteLine("Création de votre premier bloc en cours ...");
var myBlockchain = new Chain();
while(choix > 0 && choix < 5){
    Console.WriteLine("Que voulez vous faire ? \n1 : Ajoutez une transaction \n2 : Affichez l'etat de votre blockchain\n3 : Faussez un block\n4 : Vérifiez l'intégrité de la blockchain\n0 : Quitter la simulation");
    choix = (int.Parse(Console.ReadLine()));
    //Méthode archaïque mais c'st pour tester toutes les fonctionnalités de la blockchain
    switch (choix)
    {
        case 1 : 
                Console.WriteLine("Indiquez le montant de votre transaction");
                valeur = (int.Parse(Console.ReadLine()));
                myBlockchain.ajouterBlock(valeur);
        break ;
        case 2: 
                jsonString = JsonSerializer.Serialize<Chain>(myBlockchain, options);
                Console.WriteLine(jsonString);
        break  ;
        case 3 : 
                Console.WriteLine("Indiquez l'index du bloc que vous voulez fausser");
                fake = (int.Parse(Console.ReadLine()));
                Console.WriteLine("Indiquez le nouveau montant");
                valeur = (int.Parse(Console.ReadLine()));
                myBlockchain.chain[fake].data = valeur ; 
        break  ;
        case 4 : 
                    Console.WriteLine(myBlockchain.validChain());
        break  ;
        default: break ; 
    }
}








