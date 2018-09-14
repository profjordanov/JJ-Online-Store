using System.Security.Claims;
using JjOnlineStore.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace JjOnlineStore.Data.EF
{
	public class ApplicationRoleStore : RoleStore<
		ApplicationRole ,
		JjOnlineStoreDbContext,
		string ,
		IdentityUserRole<string> ,
		IdentityRoleClaim<string>>
	{
		public ApplicationRoleStore(JjOnlineStoreDbContext context , IdentityErrorDescriber describer = null)
			: base(context , describer)
		{
		}

		protected override IdentityRoleClaim<string> CreateRoleClaim(ApplicationRole role , Claim claim) =>
			new IdentityRoleClaim<string>
			{
				RoleId = role.Id ,
				ClaimType = claim.Type ,
				ClaimValue = claim.Value
			};
	}
}