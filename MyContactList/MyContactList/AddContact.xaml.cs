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

        public AddContact()
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);

            nameEntry.ReturnCommand = new Command(() => emailEntry.Focus());
            emailEntry.ReturnCommand = new Command(() => phoneEntry.Focus());
            phoneEntry.ReturnCommand = new Command(() => AddressEntry.Focus());
        }
        public AddContact(Contact contact)
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);

            nameEntry.Text = contact.Name;
            emailEntry.Text = contact.Email;
            phoneEntry.Text = contact.Phone;
            AddressEntry.Text = contact.Address;
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
                    MyContactList.Helpers.Utils.Write(null, contact);
                    await Navigation.PushAsync(new MainPage());
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
