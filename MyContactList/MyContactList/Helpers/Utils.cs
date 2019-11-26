using MyContactList.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace MyContactList.Helpers
{
    public class Utils
    {
        private static string filename = "contactListNew.txt";
        public static void Write(Contact contact, bool isEdit, bool isAdd, ref bool existContact, string existName)
        {            
            if (isAdd)
            {
                try
                {
                    bool contactExist = ContactExist(filename, contact);
                    if (!contactExist)
                    {
                        WriteTxt(contact, filename);
                    }
                    else
                    {
                        existContact = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw new Exception(ex.Message);
                }
            }
            else if (isEdit)
            {
                EditTxt(contact, filename, existName);
                //i have to read the file and create a temp file to rewrite the data 
            }

        }
        public static List<Contact> Read()
        {
            try
            {
                List<Contact> Contact = new List<Contact>();             
                Contact = ReadTxt(filename);
                return Contact;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }


        public static void WriteTxt(Contact contact, string filename)
        {

            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);
            //if (File.Exists(filePath))
            //{
            //    File.Delete(filePath);
            //}

            if (!File.Exists(filePath))
            {
                File.CreateText(filePath).Dispose();
                using (TextWriter tw = new StreamWriter(filePath, true))
                {
                    tw.WriteLine(string.Format("{0} - {1} - {2} - {3}", contact.Name, contact.Phone, contact.Address, contact.Email));
                }

            }
            else if (File.Exists(filePath))
            {                
                using (StreamWriter tw = File.AppendText(filePath))
                {
                    tw.WriteLine(string.Format("{0} - {1} - {2} - {3}", contact.Name, contact.Phone, contact.Address, contact.Email));                
                }
            }
        }

        public static void EditTxt(Contact contact, string filename, string existingName)
        {
            string tempFile = "tempContact.txt";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);
            var tempPath = Path.Combine(documentsPath, tempFile);

            if (File.Exists(filePath))
            {
                using (StreamReader st = new StreamReader(filePath))
                using (StreamWriter tw = new StreamWriter(tempPath, true))
                {
                    string line = "";
                    while ((line = st.ReadLine()) != null)
                    {
                        if (!line.Contains(existingName))
                        {
                            tw.WriteLine(line);
                        }
                        else
                        {
                            tw.WriteLine(string.Format("{0} - {1} - {2} - {3}", contact.Name, contact.Phone, contact.Address, contact.Email));
                        }
                    }
                }                
                File.Delete(filePath);
                File.Move(tempPath, filePath);
                using (StreamReader st = new StreamReader(filePath))
                {
                    string content2 = st.ReadToEnd();
                }                    
            }
        }

        public static List<Contact> ReadTxt(string filename)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);

            if (File.Exists(filePath))
            {
                List<Contact> contact = new List<Contact>();
                using (StreamReader st = new StreamReader(filePath))
                {
                    //string content2 = st.ReadToEnd();
                    while (!st.EndOfStream)
                    {
                        string content = st.ReadLine();
                        if (content != null)
                        {
                            string[] grouplist = content.Split('-');
                            contact.Add(new Contact()
                            {
                                Name = grouplist[0],
                                Phone = grouplist[1],
                                Address = grouplist[2],
                                Email = grouplist[3]
                            });
                        }
                        System.Diagnostics.Debug.WriteLine(content);
                    }
                }
                return contact;
            }
            else
            {
                return null;
            }
        }
        private static bool ContactExist(string filename, Contact contact)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);
            if (File.Exists(filePath))
            {
                if (File.ReadAllText(filePath).Contains(contact.Name))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }

}
