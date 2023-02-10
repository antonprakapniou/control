namespace Control.DAL.Models;

public sealed class Master:IdentityUser
{
	#region Own properties

	public string? Name { get; set; }
	public ICollection<Owner>? Owners { get; set; }

	#endregion
}
