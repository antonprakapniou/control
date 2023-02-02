namespace Control.DAL.Models;

public sealed class Measuring : BaseModel
{
    #region Own properties

    public string? Name { get; set; }
    public string? Code { get; set; }
    public ICollection<Position>? Positions { get; set; }

    #endregion
}