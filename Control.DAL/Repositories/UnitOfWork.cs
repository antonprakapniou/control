﻿using Control.DAL.EF;
using Control.DAL.Interfaces;
using Control.DAL.Models;

namespace Control.DAL.Repositories
{
	public sealed class UnitOfWork:IUnitOfWork
	{
		private readonly AppDbContext _db;

		#region Repositories
		public IGenericRepository<Measuring> Measurings { get; }
		public IGenericRepository<Nomination> Nominations { get; }
		public IGenericRepository<Operation> Operations { get; }
		public IGenericRepository<Owner> Owners { get; }
		public IGenericRepository<Period> Periods { get; }
		public IGenericRepository<Position> Positions { get; }
		public IGenericRepository<Status> Statuses { get; }
		public IGenericRepository<Units> Units { get; }

		#endregion

		public UnitOfWork(AppDbContext db)
		{
			_db= db;
			Measurings=new GenericRepository<Measuring>(_db);
			Nominations=new GenericRepository<Nomination>(_db);
			Operations=new GenericRepository<Operation>(_db);
			Owners=new GenericRepository<Owner>(_db);
			Periods=new GenericRepository<Period>(_db);
			Positions=new GenericRepository<Position>(_db);
			Statuses=new GenericRepository<Status>(_db);
			Units=new GenericRepository<Units>(_db);
		}

		public async Task SaveAsync()
		{
			await _db.SaveChangesAsync();
		}
	}
}
