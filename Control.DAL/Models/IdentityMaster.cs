namespace Control.DAL.Models;

public sealed class IdentityMaster:IdentityUser
{
	#region Own properties

	public ICollection<Owner>? Owners { get; set; }

	#endregion
}
