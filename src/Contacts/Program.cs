using System;
using Google.GData.Client;
using Google.GData.Contacts;

namespace Contacts
{
    class Program
    {
        static void Main()
        {
            Service service = new ContactsService("My Contacts Application");

            service.setUserCredentials("your_email_address_here@gmail.com", "yourpassword");
            var token = service.QueryClientLoginToken();
            service.SetAuthenticationToken(token);

            var uri = ContactsQuery.CreateContactsUri("your_email_address_here@gmail.com");
            var query = new ContactsQuery(uri);

            ContactsFeed feed = (ContactsFeed) service.Query(query);

            bool stop = false;
            while (!stop)
            {
                foreach (ContactEntry contact in feed.Entries)
                {
                    Console.WriteLine(contact.Title.Text);
                }

                if (!String.IsNullOrEmpty(feed.NextChunk))
                {
                    query = new ContactsQuery(feed.NextChunk);
                    feed = (ContactsFeed) service.Query(query);
                }
                else
                {
                    stop = true;
                }
            }

            Console.WriteLine("Done. Press any key to continue...");
            Console.WriteLine();
        }
    }
}
