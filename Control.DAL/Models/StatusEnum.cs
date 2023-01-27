namespace Control.DAL.Models
{
    [Flags]
    public enum StatusEnum
    {
        Indefined = 0,
        Valid=0b1,
        NextMonthControl=0b1_0,
        CurrentMonthControl=0b1_00,
        Invalid=0b1_000,
        All=0b1_0000
    }
}
