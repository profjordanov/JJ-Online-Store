using System;
using System.Collections.Generic;
using JjOnlineStore.Data.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace JjOnlineStore.Data.Entities
{
	public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
	{
		public ApplicationUser()
		{
			this.Id = Guid.NewGuid().ToString();
			this.Roles = new HashSet<IdentityUserRole<string>>();
			this.Claims = new HashSet<IdentityUserClaim<string>>();
			this.Logins = new HashSet<IdentityUserLogin<string>>();
		}

		// Audit info
		public DateTime CreatedOn { get; set; }

		public DateTime? ModifiedOn { get; set; }

		// Deletable entity
		public bool IsDeleted { get; set; }

		public DateTime? DeletedOn { get; set; }

	    public virtual ICollection<Cart> Carts { get; set; } = new HashSet<Cart>();

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

		public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

		public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
	}
}
