using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CP2
{

 

    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            //GetAllContact();
        }
        private async void GetAllContact()
        {
            string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "phoneNumber.csv");
            ObservableCollection<Contact> contactsCollect = new ObservableCollection<Contact>();

            try
            {
                // cancellationToken parameter is optional
                var cancellationToken = default(CancellationToken);
                var contacts = await Contacts.GetAllAsync(cancellationToken);

                if (contacts == null)
                    return;

                foreach (var contact in contacts)
                    contactsCollect.Add(contact);

                var info = new StringBuilder();
                foreach (var x in contactsCollect)
                {
                    LabelInfo.Text += info.AppendLine(x.Phones.FirstOrDefault()?.PhoneNumber ?? string.Empty).ToString();
                }
                
                
            }
            catch (Exception ex)
            {
                // Handle exception here.
            }
        }

        private async void phoneContact()
        {
            try
            {
                var contact = await Contacts.PickContactAsync();

                if (contact == null)
                    return;

                var info = new StringBuilder();
                var id = contact.Id;
                var namePrefix = contact.NamePrefix;
                var givenName = contact.GivenName;
                var middleName = contact.MiddleName;
                var familyName = contact.FamilyName;
                var nameSuffix = contact.NameSuffix;
                var displayName = contact.DisplayName;
                info.AppendLine(contact.Phones.FirstOrDefault()?.PhoneNumber ?? string.Empty); // List of phone numbers
                var emails = contact.Emails; // List of email addresses
                LabelInfo3.Text = info.ToString();
            }
            catch (Exception ex)
            {
                // Handle exception here.
            }
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            //phoneContact();
            GetAllContact();
        }
    }
}
