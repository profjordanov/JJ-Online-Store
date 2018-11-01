using System;
using System.ComponentModel.DataAnnotations;
using JjOnlineStore.Common.Enumeration;
using JjOnlineStore.Common.ViewModels.ShoppingCarts;

namespace JjOnlineStore.Common.ViewModels.Orders
{
    public class OrderVm
    {
        public long Id { get; set; }

        //Delivery Methods
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

        //Payment Methods
        [Required(ErrorMessage = "Please enter a Cardholder Name.")]
        public string CardholderName { get; set; }

        [Required(ErrorMessage = "Please enter a Card Number.")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Please enter a Expire Date.")]
        [DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ExpireDate { get; set; }

        [Required(ErrorMessage = "Please enter a CW.")]
        public string Cvv { get; set; }

        public TransportationType TransportationType { get; set; }

        //Shopping Card
        public long CartId { get; set; }

        public virtual CartVm Cart { get; set; }
    }
}