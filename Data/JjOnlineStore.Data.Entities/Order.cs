using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JjOnlineStore.Data.Entities.Base;

namespace JjOnlineStore.Data.Entities
{
    public class Order : BaseDeletableModel<long>
    {
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
        public DateTime ExpireDate { get; set; }

        [Required(ErrorMessage = "Please enter a CW.")]
        public string Cvv { get; set; }

        [ForeignKey(nameof(Cart))]
        public long CartId { get; set; }

        public virtual Cart Cart { get; set; }
    }
}