
namespace blockchainC_
{
    public class Wallet
    {
        public string publicKey ;
        public string privateKey ;
        
        public X509Certificate2? cert; 
        
        public Wallet(){
            this.publicKey ="NULL" ; 
            this.privateKey="NULL" ; 
            generatePairKey();
        }


        public void generatePairKey(){

           this.cert = new CertificateRequest("cn=Test", ECDsa.Create(ECCurve.NamedCurves.nistP256), HashAlgorithmName.SHA256)
                .CreateSelfSigned(DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(2));
            ECDsa? temp = cert.GetECDsaPrivateKey() ; 
            if (temp is null)
            {
                throw new Exception ("Erreur création du certificat, pas de clé privé associé");
            }
            var privateKeybyte = temp.ExportECPrivateKey();
            this.privateKey = Convert.ToBase64String(privateKeybyte);
            ECDsa? tempPublic = cert.GetECDsaPublicKey();
            if (tempPublic is null)
            {
                throw new Exception("Pas de clé ECDsa Publique lors de la création du cerificat");
            }
            var publicKeybyte = tempPublic.ExportSubjectPublicKeyInfo();
            this.publicKey = Convert.ToBase64String(publicKeybyte);
        }
        
        public void exportKey(){
            Console.WriteLine("Privatekey :"+this.privateKey);
            Console.WriteLine("Publickey :"+this.publicKey);
        }
        
    }
}