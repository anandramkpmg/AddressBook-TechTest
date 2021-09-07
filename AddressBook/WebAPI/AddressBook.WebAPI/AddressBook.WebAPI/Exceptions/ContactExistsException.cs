using System;
using System.Globalization;

namespace AddressBook.WebAPI.Exceptions
{
    public class ContactExistsException : Exception
    {
        public ContactExistsException() : base()
        {
        }

        public ContactExistsException(string message) : base(message)
        {
        }

        public ContactExistsException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
