using JjOnlineStore.Common.Enumeration;
using JjOnlineStore.Common.ViewModels.OrderItems;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using static JjOnlineStore.Common.Cryptography.Cipher;

namespace JjOnlineStore.Common.ViewModels.Orders
{
    public class OrderVm
    {
        public long Id { get; set; }

        public DateTime CreatedOn { get; set; }

        //Delivery Information
        [Required(ErrorMessage = "Please enter your first name.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter a country name.")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Please enter a city name.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter a state name.")]
        public string State { get; set; }

        public string Zip { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public bool Shipped { get; set; }

        //Payment Information
        [Required(ErrorMessage = "Please enter a Cardholder Name.")]
        public string CardholderName { get; set; }

        [Required(ErrorMessage = "Please enter a Card Number.")]
        public string CardNumber { get; set; }

        [Required]
        public int ExpireDateMonth { get; set; }

        [Required]
        public int ExpireDateYear { get; set; }

        [Required(ErrorMessage = "Please enter a CW.")]
        public string Cvv { get; set; }

        public TransportationType TransportationType { get; set; }

        public IEnumerable<OrderItemVm> OrderedItems { get; set; }

        public long? InvoiceId { get; set; }

        public string UserId { get; set; }


        public decimal GrandTotal() =>
            OrderedItems.Sum(oi => oi.Product.Price * oi.Quantity);

        public OrderVm EncryptSensitiveData(OrderVm order)
        {
            order.CardholderName = Encrypt(order.CardholderName);
            order.CardNumber = Encrypt(order.CardNumber);
            order.Cvv = Encrypt(order.Cvv);

            return order;
        }

        public OrderVm DecryptSensitiveData(OrderVm order)
        {
            order.CardholderName = Decrypt(order.CardholderName);
            order.CardNumber = Decrypt(order.CardNumber);
            order.Cvv = Decrypt(order.Cvv);

            return order;
        }

        public DateTime GetExpireDateTime() =>
            (ExpireDateMonth == 0 || ExpireDateYear == 0)
            ? DateTime.UtcNow
            : new DateTime(ExpireDateYear, ExpireDateMonth, 1);
    }
}