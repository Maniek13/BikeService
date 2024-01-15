using System.Text;
using System.Security.Cryptography;
using System.Text;


Console.WriteLine("Write password");

string password = Console.ReadLine();

SHA256 sha256Hash = SHA256.Create();

byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

var sBuilder = new StringBuilder();

for (int i = 0; i < data.Length; i++)
    sBuilder.Append(data[i].ToString("x2"));


Console.WriteLine("Encrypted password: " + sBuilder.ToString());
