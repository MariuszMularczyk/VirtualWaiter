using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.Application
{
    public class ADPersonModel
    {
        public string Cn { get; set; }
        public string Company { get; set; }
        public string Department { get; set; }
        public string Description { get; set; }
        public string DisplayName { get; set; }
        public string TelephoneNumber { get; set; }
        public string LastLogon { get; set; }
        public string Mail { get; set; }
        public string MailNickName { get; set; }
        public string Manager { get; set; }
        public string Mobile { get; set; }
        public string Title { get; set; }
        public string ObjectGuid { get; set; }
        public string Office { get; set; }
        public string PostalCode { get; set; }
        public string StreetAddress { get; set; }
        public string WhenCreated { get; set; }
        public string St { get; set; }
        public string TetaId { get; set; }
        public byte[] ThumbnailPhoto { get; set; }
        public string[] showInAddressBook { get; set; }
        public bool Disabled { get; set; }
        public string City { get; set; }
        public string ManagerSma { get; set; }
        public string sAMAccountName { get; set; }
        public string postalAddress { get; set; }

        public string GivenName { get; set; }
        public string Surname { get; set; }

        public override string ToString()
        {
            return string.Format("an: {0}, m: {1}, dn: {2}, tn: {3}, m: {4}", sAMAccountName,
                        Mail,
                        DisplayName,
                        TelephoneNumber,
                        Mobile);
        }
    }
}
