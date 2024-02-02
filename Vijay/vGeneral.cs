using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace Vijay
{
    public class vGeneral
    {
        // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
        // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
        private const string initVector = "pemgail9uzpgzl88";
        // This constant is used to determine the keysize of the encryption algorithm
        private const int keysize = 256;
        public string convertDateForDB(string dt)
        {
            string[] strArrays = new string[] { dt.Substring(6, 4), "-", dt.Substring(3, 2), "-", dt.Substring(0, 2) };
            return string.Concat(strArrays);
        }
        public string convertDateForForm(string dt)
        {
            string[] strArrays = new string[] { dt.Substring(0, 2), "/", dt.Substring(3, 2), "/", dt.Substring(6, 4) };
            return string.Concat(strArrays);
        }
        public decimal getNumberD(string val)
        {
            decimal num = new decimal(0);
            decimal num1 = new decimal(0);
            num = (!decimal.TryParse(val, out num1) ? new decimal(0) : num1);
            return num;
        }
        public int getNumberV(string val)
        {
            int num = 0;
            int num1 = 0;
            num = (!int.TryParse(val, out num1) ? 0 : num1);
            return num;
        }
        public string sendEmail(string ename, string email, string subject, string message, string strHost, int intPort, string strUid, string strPwd, bool blnSSL, string eFrom, List<string> filePath)
        {
            string msg = "";
            try
            {
                MailMessage mailmessage = new MailMessage(eFrom, email, subject, message);
                Attachment attachment = null;

                if (filePath!= null)
                {
                    for (int inc = 0; inc < filePath.Count; inc++)
                    {
                        attachment = new Attachment(filePath[inc], "application/octet-stream");
                        ContentDisposition contentDisposition = attachment.ContentDisposition;
                        contentDisposition.CreationDate = System.IO.File.GetCreationTime(filePath[inc]);
                        contentDisposition.ModificationDate = System.IO.File.GetLastWriteTime(filePath[inc]);
                        contentDisposition.ReadDate = System.IO.File.GetLastWriteTime(filePath[inc]);
                        mailmessage.Attachments.Add(attachment);
                        attachment.Dispose();
                    }
                }

                SmtpClient client = new SmtpClient(strHost, intPort);
                client.EnableSsl = blnSSL;
                client.Timeout = 0;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = true;
                client.Credentials = new NetworkCredential(strUid, strPwd);
                client.Send(mailmessage);

                /*
                ContentDisposition disposition2 = attachment.ContentDisposition;
                object obj2 = ((msg + "Content disposition\r\n") + disposition2.ToString() + "\r\n") + "File :" + disposition2.FileName + Environment.NewLine;
                object obj3 = string.Concat(new object[] { obj2, "Size :", disposition2.Size, Environment.NewLine });
                object obj4 = string.Concat(new object[] { obj3, "Creation :", disposition2.CreationDate, Environment.NewLine });
                object obj5 = string.Concat(new object[] { obj4, "Modification : ", disposition2.ModificationDate, Environment.NewLine });
                object obj6 = string.Concat(new object[] { obj5, "Read : ", disposition2.ReadDate, Environment.NewLine });
                object obj7 = string.Concat(new object[] { obj6, "Inline : ", disposition2.Inline, Environment.NewLine });
                msg = string.Concat(new object[] { obj7, "Parameters: ", disposition2.Parameters.Count, Environment.NewLine });
                foreach (DictionaryEntry entry in disposition2.Parameters)
                {
                    object obj8 = "SUCCESS:" + msg;
                    msg = string.Concat(new object[] { obj8, entry.Key, " ", entry.Value, Environment.NewLine });
                }
                attachment.Dispose();*/
            }
            catch (Exception ex)
            {
                msg = "ERROR:" + ex.ToString();
            }
            return msg;
        }
        //////////////////
        //Encrypt
        public string EncryptString(string plainText, string passPhrase)
        {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            System.Security.Cryptography.PasswordDeriveBytes password = new System.Security.Cryptography.PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            System.Security.Cryptography.RijndaelManaged symmetricKey = new System.Security.Cryptography.RijndaelManaged();
            symmetricKey.Mode = System.Security.Cryptography.CipherMode.CBC;
            System.Security.Cryptography.ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            System.Security.Cryptography.CryptoStream cryptoStream = new System.Security.Cryptography.CryptoStream(memoryStream, encryptor, System.Security.Cryptography.CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(cipherTextBytes);
        }
        //Decrypt
        public string DecryptString(string cipherText, string passPhrase)
        {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            System.Security.Cryptography.PasswordDeriveBytes password = new System.Security.Cryptography.PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            System.Security.Cryptography.RijndaelManaged symmetricKey = new System.Security.Cryptography.RijndaelManaged();
            symmetricKey.Mode = System.Security.Cryptography.CipherMode.CBC;
            System.Security.Cryptography.ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(cipherTextBytes);
            System.Security.Cryptography.CryptoStream cryptoStream = new System.Security.Cryptography.CryptoStream(memoryStream, decryptor, System.Security.Cryptography.CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }
    }
}
