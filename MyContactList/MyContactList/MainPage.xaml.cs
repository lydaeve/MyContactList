using MyContactList.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyContactList
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Contact> list;
        List<Contact> Contact = new List<Contact>() ;

        public ObservableCollection<Contact> ContactList
        {
            get { return list; }
            set { list = value; }
        }

        public MainPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, true);

            ContactList = new ObservableCollection<Contact>();

            ContactList.Add(new Contact()
            {
                Id = 1,
                Name ="Maria Gomez", 
                Phone = "78625698770",
                Address = "2455 sw 34st",
                Email = "luis@gmail.com"
            });
            ContactList.Add(new Contact()
            {
                Id = 2,
                Name = "Sandra Gomez",
                Phone = "7862111111",
                Address = "24 nw 34st",
                Email = "lidian@gmail.com"
            });
            //  MyContactList.Helpers.Utils.Write(ContactList);
            Contact = MyContactList.Helpers.Utils.Read();

            ContactList2.ItemsSource = Contact;


        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddContact());
        }
        private async void Menu_Item_Edit(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item;
            Contact contac = new Contact();
            contac.Name = ((Contact)(object)item).Name;
            contac.Email = ((Contact)(object)item).Email;
            contac.Phone = ((Contact)(object)item).Phone;
            contac.Address = ((Contact)(object)item).Address;
            await Navigation.PushAsync(new AddContact(contac));

        }
    }
}
