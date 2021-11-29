using System.Security.Cryptography;
using System.IO;
using System.Text;
using System;

string encryptString(string plainText, byte[] key, byte[] iv)
{
    // Instantiate a new Aes object to perform string symmetric encryption
    Aes encryptor = Aes.Create();

    encryptor.Mode = CipherMode.CBC;
    //encryptor.KeySize = 256;
    //encryptor.BlockSize = 128;
    //encryptor.Padding = PaddingMode.Zeros;

    // Set key and IV
    encryptor.Key = key;
    encryptor.IV = iv;

    // Instantiate a new MemoryStream object to contain the encrypted bytes
    MemoryStream memoryStream = new MemoryStream();

    // Instantiate a new encryptor from our Aes object
    ICryptoTransform aesEncryptor = encryptor.CreateEncryptor();

    // Instantiate a new CryptoStream object to process the data and write it to the 
    // memory stream
    CryptoStream cryptoStream = new CryptoStream(memoryStream, aesEncryptor, CryptoStreamMode . Write);

    // Convert the plainText string into a byte array
    byte[] plainBytes = Encoding.ASCII.GetBytes(plainText);

    // Encrypt the input plaintext string
    cryptoStream.Write(plainBytes, 0, plainBytes . Length);

    // Complete the encryption process
    cryptoStream.FlushFinalBlock();

    // Convert the encrypted data from a MemoryStream to a byte array
    byte[] cipherBytes = memoryStream.ToArray();

    // Close both the MemoryStream and the CryptoStream
    memoryStream.Close();
    cryptoStream.Close();

    // Convert the encrypted byte array to a base64 encoded string
    string cipherText = Convert.ToBase64String(cipherBytes, 0, cipherBytes.Length);

    // Return the encrypted data as a string
    return cipherText;
}

string decryptString(string cipherText, byte[] key, byte[] iv)
{
    // Instantiate a new Aes object to perform string symmetric encryption
    Aes encryptor = Aes.Create();

    encryptor.Mode = CipherMode.CBC;
    encryptor.KeySize = 256;
    //encryptor.BlockSize = 128;
    //encryptor.Padding = PaddingMode.Zeros;

    // Set key and IV
    encryptor.Key = key;
    encryptor.IV = iv;

    // Instantiate a new MemoryStream object to contain the encrypted bytes
    MemoryStream memoryStream = new MemoryStream();

    // Instantiate a new encryptor from our Aes object
    ICryptoTransform aesDecryptor = encryptor.CreateDecryptor();

    // Instantiate a new CryptoStream object to process the data and write it to the 
    // memory stream
    CryptoStream cryptoStream = new CryptoStream(memoryStream, aesDecryptor, CryptoStreamMode . Write);

    // Will contain decrypted plaintext
    string plainText = String.Empty;

    try {
        // Convert the ciphertext string into a byte array
        byte[] cipherBytes = Convert.FromBase64String(cipherText);

        // Decrypt the input ciphertext string
        cryptoStream.Write(cipherBytes, 0, cipherBytes . Length);

        // Complete the decryption process
        cryptoStream.FlushFinalBlock();

        // Convert the decrypted data from a MemoryStream to a byte array
        byte[] plainBytes = memoryStream.ToArray();

        // Convert the decrypted byte array to string
        plainText = Encoding.ASCII.GetString(plainBytes, 0, plainBytes.Length);
    } finally {
        // Close both the MemoryStream and the CryptoStream
        memoryStream.Close();
        cryptoStream.Close();
    }

    // Return the decrypted data as a string
    return plainText;
}

string password = "3sc3RLrpd17";
string plaintext = "Ricardo Pereira Dias";

// Create sha256 hash
SHA256 mySHA256 = SHA256Managed.Create();
byte[] key = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(password));

// Create secret IV
byte[] iv = new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };

string encrypted = encryptString(plaintext, key, iv);
string decrypted = decryptString(encrypted, key, iv);

Console.WriteLine("-------------------------------------------------");
Console.WriteLine("C#");
Console.WriteLine("-------------------------------------------------");
Console.WriteLine("Cifrador: AES 256 CBC");
Console.WriteLine("Chave: " + password);
Console.WriteLine("Texto: " + plaintext);
Console.WriteLine("Cifrado: " + encrypted);
Console.WriteLine("Decifrado: " + decrypted);

Console.WriteLine("");
Console.WriteLine("");