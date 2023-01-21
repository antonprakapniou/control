using Control.DAL.Models;

namespace Control.DAL.Interfaces
{
	public interface IUnitOfWork
	{
		public IGenericRepository<Measuring> Measurings { get;}
		public IGenericRepository<Nomination> Nominations { get; }
		public IGenericRepository<Operation> Operations { get; }
		public IGenericRepository<Owner> Owners { get; }
		public IGenericRepository<Period> Periods { get; }
		public IGenericRepository<Position> Positions { get; }
		public IGenericRepository<Status> Statuses { get; }
		public IGenericRepository<Units> Units { get; }

		public Task SaveAsync(); 
	}
}
