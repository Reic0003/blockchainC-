
namespace blockchainC_
{
    public class Wallet
    {
        public string publicKey ;
        public string privateKey ;
        public int balance ;
        
        public X509Certificate2? cert; 
        
        public Wallet(){
            this.publicKey ="NULL" ; 
            this.privateKey="NULL" ; 
            this.balance = 0 ; 
            generatePairKey();
        }


        public void generatePairKey(){

           this.cert = new CertificateRequest("cn=Test", ECDsa.Create(ECCurve.NamedCurves.nistP256), HashAlgorithmName.SHA256)
                .CreateSelfSigned(DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(2));
            var privateKeybyte = cert.GetECDsaPrivateKey().ExportECPrivateKey();
            this.privateKey = Convert.ToBase64String(privateKeybyte);
            var publicKeybyte = cert.GetECDsaPublicKey().ExportSubjectPublicKeyInfo();
            this.publicKey = Convert.ToBase64String(publicKeybyte);
            Console.WriteLine("private key :"+this.privateKey);
            Console.WriteLine("public key :"+this.publicKey);
        }
        
        public int getBalance(){
            return this.balance ; 
        }
        
    }
}