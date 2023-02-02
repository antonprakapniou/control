namespace Control.DAL.Models;

public sealed class Operation : BaseModel
{
    #region Own properties

    public string? Name { get; set; }
    public ICollection<Position>? Positions { get; set; }

    #endregion
}