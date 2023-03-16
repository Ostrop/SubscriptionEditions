using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionEditionsPrEP.Models
{
    public class Recipients
    {
        public int recipient_id;
        public string surname;
        public string name;
        public string patronymic;
        public string address;
        public string login;
        public string password;
        public string Image;
        public bool isAdmin;

        public Recipients(int recipient_id, string surname, string name, string patronymic, string address,
            string login, string password, string Image, bool isAdmin)
        {
            this.recipient_id = recipient_id;
            this.surname = surname;
            this.name = name;
            this.patronymic = patronymic;
            this.address = address;
            this.login = login;
            this.password = password;
            this.Image = Image;
            this.isAdmin = isAdmin;
        }
    }
}
