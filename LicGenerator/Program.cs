using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using parking;

namespace LicGenerator
{
    class Program
    {
        private static void GenerateNewKeyPair()

        {

            string withSecret;

            string woSecret;

            using (var rsaCsp = new RSACryptoServiceProvider())

            {

                withSecret = rsaCsp.ToXmlString(true);

                woSecret = rsaCsp.ToXmlString(false);

            }



            File.WriteAllText("private.xml", withSecret);

            File.WriteAllText("public.xml", woSecret);

        }
        static void Main(string[] args)
        {
            if (args.Any(a => a == "--generate"))
            {
                GenerateNewKeyPair();
            }

            var dto = new LicDto()
            {
                ValidUntil = DateTime.Now.AddDays(7)
            };

            var fileName = string.Join("", DateTime.Now.ToString().Where(c => char.IsDigit(c)));
            new LicenceGenerator().CreateLicenseFile(dto, fileName + ".gh_licence");
        }
    }

    class LicenceGenerator
    {
        private static string PrivateKey = @"<RSAKeyValue>
  <Modulus>8lBn0np/dY1q9++sTHaIMpqIuszrEWeC3iNgU4zfgJXbbWVeAVwC3rFOk0MTwzRBd2L420WKHUV1Bd5hwzK1HTPMX5uRX+zWPMolzu2uwU/EBEwA9FzhcZ96i9P3NpSdO6zokVM4HgoP8eHP4MrWN1cRjwENzrYe4DHCOs7UPME=</Modulus>
  <Exponent>AQAB</Exponent>
  <P>/ksueIJ7yUGL/lEaqxvoWqHUknRrdNXG7aL64ScGKApObCpIOAExFPEQNbDB2lyUIShuTHO1NIOsvpkvG5zD5w==</P>
  <Q>8/ClVKEdoCYinTiXZqERc0f5z8S5Q+r/NDZWlE5ZDEP9V7NjBiWp3Y4TPzHoh41Vt5v5AfOREmEbgaI9s1flFw==</Q>
  <DP>jaEn57yc3xGfu+xGEyj+94OMlmk69B6gpfgRDNekSSa8WOgPwwl+4bAFnDGvNkQ7yF/xVqlXMkFoM9uzbgiY3w==</DP>
  <DQ>kGXg2CnPKZ+pWbvIE15AwCbY/14J9OREURnhQkTmfVY5vxJXCap91MJnLj9Sz/qfxOI1EiylsWV+LMxnDaR33Q==</DQ>
  <InverseQ>q14f6zC/5u2HczbBnZiTGKeNvMJYI1PotfWu+jCSteL1ZZCQbXWg0dPdCouyHEYGEv5MVpzgdkxoN8rmSwkvew==</InverseQ>
  <D>jRwf6eWmVjmdonczem/IjteJamctOTxzbJNjZCR0f7hFSWavoNEJZtAQdxen6ZSDz1eWnnLXpIYvSNShUAA2kYHUAGadbO5bMneYOAIpwcov3hefYZ8t21BU1N32dQtDzNiEnUqFe/G8RUoPthDSE6rKmaSi8G79gLYJj+tDMxU=</D>
</RSAKeyValue>";
        public void CreateLicenseFile(LicDto dto, string fileName)

        {

            var ms = new MemoryStream();

            new XmlSerializer(typeof(LicDto)).Serialize(ms, dto);



            // Create a new CspParameters object to specify

            // a key container.



            // Create a new RSA signing key and save it in the container.

            RSACryptoServiceProvider rsaKey = new RSACryptoServiceProvider();

            rsaKey.FromXmlString(PrivateKey);



            // Create a new XML document.

            XmlDocument xmlDoc = new XmlDocument();



            // Load an XML file into the XmlDocument object.

            xmlDoc.PreserveWhitespace = true;

            ms.Seek(0, SeekOrigin.Begin);

            xmlDoc.Load(ms);



            // Sign the XML document.

            SignXml(xmlDoc, rsaKey);



            // Save the document.

            xmlDoc.Save(fileName);



        }



        // Sign an XML file.

        // This document cannot be verified unless the verifying

        // code has the key with which it was signed.

        public static void SignXml(XmlDocument xmlDoc, RSA Key)

        {

            // Check arguments.

            if (xmlDoc == null)

                throw new ArgumentException("xmlDoc");

            if (Key == null)

                throw new ArgumentException("Key");



            // Create a SignedXml object.

            SignedXml signedXml = new SignedXml(xmlDoc);



            // Add the key to the SignedXml document.

            signedXml.SigningKey = Key;



            // Create a reference to be signed.

            Reference reference = new Reference();

            reference.Uri = "";



            // Add an enveloped transformation to the reference.

            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();

            reference.AddTransform(env);



            // Add the reference to the SignedXml object.

            signedXml.AddReference(reference);



            // Compute the signature.

            signedXml.ComputeSignature();



            // Get the XML representation of the signature and save

            // it to an XmlElement object.

            XmlElement xmlDigitalSignature = signedXml.GetXml();



            // Append the element to the XML document.

            xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));



        }
    }
}
