﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PaymentContext.Tests.Entites
{
    [TestClass]
    public class StudentTests
    {
        private readonly Name _name;
        private readonly Document _document;
        private readonly Email _email;
        private readonly Address _address;
        private readonly Student _student;
        private readonly Subscription _subscription;

        public StudentTests()
        {
            _name = new Name("Marty", "MacFly");
            _document = new Document("25631474589", EDocumentType.CPF);
            _email = new Email("macfly@mail.com");
            _address = new Address("Rua 1", "8", "Centro", "Rolandia", "SP", "BR", "000");
            _student = new Student(_name, _document, _email);
            _subscription = new Subscription(null);            
        }

        [TestMethod]
        public void ShoulReturnErrorWhenHadActiveSubscription()
        {
            var payment = new PayPalPayment("12345", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "MacFly CORP", _document, _address, _email);
            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);            
            
            Assert.IsFalse(_student.IsValid);
        }

        [TestMethod]
        public void ShoulReturnErrorWhenSubscriptionHasNoPayment()
        {
            _student.AddSubscription(_subscription);
            Assert.IsFalse(_student.IsValid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenAddSubscription()
        {
            var payment = new PayPalPayment("12345", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "MacFly CORP", _document, _address, _email);
            _subscription.AddPayment(payment);            
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.IsValid);
        }
    }
}
