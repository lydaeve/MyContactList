using MyContactList.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyContactList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddContact : ContentPage
    {
        Contact contact = new Contact();
        private bool isEdit = false;
        private bool isAdd = false;
        private string existingName = "";


        public AddContact()
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);

            nameEntry.ReturnCommand = new Command(() => emailEntry.Focus());
            emailEntry.ReturnCommand = new Command(() => phoneEntry.Focus());
            phoneEntry.ReturnCommand = new Command(() => AddressEntry.Focus());
            isAdd = true;
        }
        public AddContact(Contact contact)
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);

            nameEntry.Text = contact.Name;
            emailEntry.Text = contact.Email;
            phoneEntry.Text = contact.Phone;
            AddressEntry.Text = contact.Address;
            isEdit = true;
            existingName = nameEntry.Text;

        }

        private async void AddContact_ButtonClicked(object sender, EventArgs e)
        {
             if ((string.IsNullOrWhiteSpace(nameEntry.Text)) || (string.IsNullOrWhiteSpace(emailEntry.Text)) ||
                (string.IsNullOrWhiteSpace(phoneEntry.Text)) || (string.IsNullOrWhiteSpace(AddressEntry.Text)) ||
                (string.IsNullOrEmpty(nameEntry.Text)) || (string.IsNullOrEmpty(emailEntry.Text)) ||
                (string.IsNullOrEmpty(phoneEntry.Text)) || (string.IsNullOrEmpty(AddressEntry.Text)))
            {
                await DisplayAlert("Enter Data", " Please enter valid data", "OK");
            }
            else
            {
                contact.Name = nameEntry.Text;
                contact.Email = emailEntry.Text;
                contact.Phone = phoneEntry.Text;
                contact.Address = AddressEntry.Text.ToString();
                
                try
                {
                    bool existContact = false;

                    MyContactList.Helpers.Utils.Write(contact, isEdit, isAdd, ref existContact, existingName);
                    if (!existContact)
                    {
                        await Navigation.PushAsync(new MainPage());
                    }
                    else
                    {
                        await DisplayAlert("", " Contact already exist try again", "Cancel");
                    }                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw new Exception(ex.Message);
                }
            }
        }


    }
}
