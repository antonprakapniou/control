namespace Control.BLL.Utilities;

public sealed class MapPropfile : Profile
{
    public MapPropfile()
    {
        CreateMap<Category, CategoryVM>().ReverseMap();
        CreateMap<Measuring, MeasuringVM>().ReverseMap();
        CreateMap<IdentityMaster, IdentityMasterVM>().ReverseMap();
        CreateMap<Nomination, NominationVM>().ReverseMap();
        CreateMap<Operation, OperationVM>().ReverseMap();
        CreateMap<Owner, OwnerVM>().ReverseMap();
        CreateMap<Period, PeriodVM>().ReverseMap();
        CreateMap<Position, PositionVM>().ReverseMap();
    }
}