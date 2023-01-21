using AutoMapper;
using Control.BLL.ViewModels;
using Control.DAL.Models;

namespace Control.BLL.Utilities
{
	public sealed class MapPropfile:Profile
	{
		public MapPropfile()
		{
			CreateMap<Measuring,MeasuringVM>().ReverseMap();
			CreateMap<Nomination, NominationVM>().ReverseMap();
			CreateMap<Operation, OperationVM>().ReverseMap();
			CreateMap<Owner, OwnerVM>().ReverseMap();
			CreateMap<Period, PeriodVM>().ReverseMap();
			CreateMap<Position, PositionVM>().ReverseMap();
			CreateMap<Status, StatusVM>().ReverseMap();
			CreateMap<Units, UnitsVM>().ReverseMap();
		}
	}
}
