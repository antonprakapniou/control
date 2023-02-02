namespace Control.DAL.Models;

public sealed class Master:BaseModel
{
    #region Own properties

    public string? Name { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public ICollection<Owner>? Owners { get; set; }

    #endregion
}