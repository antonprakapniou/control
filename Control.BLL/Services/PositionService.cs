using AutoMapper;
using Control.BLL.Exceptions;
using Control.BLL.Interfaces;
using Control.BLL.ViewModels;
using Control.DAL.Interfaces;
using Control.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Control.BLL.Services
{
	public sealed class PositionService:IPositionService
	{
        private const string _typeName = "Position";

        private readonly ILogger<PositionService> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PositionService(
            ILogger<PositionService> logger,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _logger=logger;
            _mapper=mapper;
            _unitOfWork=unitOfWork;
        }

        public async Task<IEnumerable<PositionVM>> GetAllAsync()
        {
            var models = await _unitOfWork.Positions.GetAllByAsync(
                    include: query => query
                        .Include(_ => _.Category)
                        .Include(_ => _.Measuring)
                        .Include(_ => _.Nomination)
                        .Include(_ => _.Operation)
                        .Include(_ => _.Owner)
                        .Include(_ => _.Period)!);

            if (models==null)
            {
                string errorMessage = $"{_typeName} collection not found ";
                _logger.LogError(errorMessage);
                throw new ObjectNotFoundException(errorMessage);
            }

            else
            {
                var modelsVM = _mapper.Map<IEnumerable<PositionVM>>(models);
                return modelsVM;
            }
        }

        public async Task<PositionVM> GetByIdAsync(Guid id)
        {
            var model = await _unitOfWork.Positions.GetOneByAsync(
                expression: _ => _.Id.Equals(id),
                    include: query => query
                        .Include(_ => _.Category)
                        .Include(_ => _.Measuring)
                        .Include(_ => _.Nomination)
                        .Include(_ => _.Operation)
                        .Include(_ => _.Owner)
                        .Include(_ => _.Period)!);

            if (model==null)
            {
                string errorMessage = $"{_typeName} collection not found ";
                _logger.LogError(errorMessage);
                throw new ObjectNotFoundException(errorMessage);
            }

            else
            {
                var modelVM = _mapper.Map<PositionVM>(model);
                return modelVM;
            }
        }

        public async Task CreateAsync(PositionVM vm)
        {
            var model = _mapper.Map<Position>(vm);
            var period = await _unitOfWork.Periods.GetOneByAsync(_ => _.Id.Equals(vm.PeriodId));

            if (model.Period==null) throw new ObjectNotFoundException($"{_typeName} model with id: {vm.Id} not found ");
            else model.NextDate = model.PreviousDate.AddMonths(model.Period.Month);

            model.Status=SetStatus(model.NextDate);
            model.Created=DateTime.Now;
            _unitOfWork.Positions.Create(model);
            await _unitOfWork.SaveAsync();
        }        

        public async Task DeleteAsync(Guid id)
        {
            var model = await _unitOfWork.Positions.GetOneByAsync(_ => _.Id.Equals(id));

            if (model==null)
            {
                string errorMessage = $"{_typeName} model with id: {id} not found ";
                _logger.LogError(errorMessage);
                throw new ObjectNotFoundException(errorMessage);
            }

            else
            {
                _unitOfWork.Positions.Delete(model);
                await _unitOfWork.SaveAsync();
            }
        }

        private static StatusEnum SetStatus(DateTime nextDate)
        {
            DateTime currentDate = DateTime.Now;

            if (currentDate>nextDate) return StatusEnum.Invalid;
            else if (currentDate.AddMonths(2)>nextDate&&currentDate.Month.Equals(nextDate.Month)) return StatusEnum.CurrentMonthControl;
            else if (currentDate.AddMonths(2)>nextDate&&currentDate.AddMonths(1).Month.Equals(nextDate.Month)) return StatusEnum.CurrentMonthControl;
            else return StatusEnum.Valid;
        }

        public Task UpdateAsync(PositionVM vm)
        {
            throw new NotImplementedException();
        }
    }
}
