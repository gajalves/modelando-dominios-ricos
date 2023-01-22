using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PaymentContext.Domain.ValueObjects
{
    public class Name : ValueObject
    {        
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            if (string.IsNullOrEmpty(FirstName))
                AddNotification("Name.FirstName", "Nome inválido");

            if (string.IsNullOrEmpty(LastName))
                AddNotification("Name.LastName", "Sobrenome inválido");


            AddNotifications(new Contract<Name>()
                .Requires()
                .IsGreaterThan(FirstName, 3 , "Name.FirstName", "Nome deve contar pelo menos 3 caracteres")                             
            );            
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
