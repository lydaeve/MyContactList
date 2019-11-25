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
        private static string filename = "contactList2.txt";
        public static void Write(ObservableCollection<Contact> ContactList, Contact contact)
        {
            try
            {
                //string filename = "contactList2.txt";
                Console.WriteLine(ContactList);
                WriteTxt(ContactList, contact, filename);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public static List<Contact> Read()
        {
            try
            {
                List<Contact> Contact = new List<Contact>();
                //string filename = "contactList2.txt";
                Contact = ReadTxt(filename);
                return Contact;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }


        public static void WriteTxt(ObservableCollection<Contact> ContactList, Contact contact, string filename)
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
                    //foreach (var item in ContactList)
                    //{
                    //    tw.WriteLine(string.Format("{0} - {1} - {2} - {3}", item.Name, item.Phone, item.Address, item.Email));
                //}
                    //tw.WriteLine("the very first line");
                }

            }
            else if (File.Exists(filePath))
            {
                //return;
                using (StreamWriter tw = File.AppendText(filePath))
                {
                    tw.WriteLine(string.Format("{0} - {1} - {2} - {3}", contact.Name, contact.Phone, contact.Address, contact.Email));
                    //    tw.WriteLine("fdrtgd2");
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
    }

}
