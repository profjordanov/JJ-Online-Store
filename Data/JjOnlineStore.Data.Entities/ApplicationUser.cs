using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using JjOnlineStore.Data.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace JjOnlineStore.Data.Entities
{
	public sealed class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
	{
		public ApplicationUser()
		{
			Id = Guid.NewGuid().ToString();
            Orders = new HashSet<Order>();
            ViewedItems = new HashSet<UserViewedItem>();
			Roles = new HashSet<IdentityUserRole<string>>();
			Claims = new HashSet<IdentityUserClaim<string>>();
			Logins = new HashSet<IdentityUserLogin<string>>();
		}

		// Audit info
		public DateTime CreatedOn { get; set; }

		public DateTime? ModifiedOn { get; set; }

		// Deletable entity
		public bool IsDeleted { get; set; }

		public DateTime? DeletedOn { get; set; }

        //Relations
        [ForeignKey(nameof(Cart))]
	    public long CartId { get; set; }

	    public Cart Cart { get; set; }

        public ICollection<Order> Orders { get; set; }

	    public ICollection<UserViewedItem> ViewedItems { get; set; }

	    public ICollection<IdentityUserRole<string>> Roles { get; set; }

		public ICollection<IdentityUserClaim<string>> Claims { get; set; }

		public ICollection<IdentityUserLogin<string>> Logins { get; set; }
	}
}
