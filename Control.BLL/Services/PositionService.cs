using AutoMapper;
using Control.BLL.Exceptions;
using Control.BLL.Interfaces;
using Control.BLL.ViewModels;
using Control.DAL.Interfaces;
using Control.DAL.Models;
using Microsoft.Extensions.Logging;

namespace Control.BLL.Services
{
	public sealed class PositionService:GenericService<PositionVM,Position>,IPositionService
	{
        private readonly ILogger<GenericService<PositionVM, Position>> _logger;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Position> _positionRepository;
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IGenericRepository<Measuring> _measuringRepository;
        private readonly IGenericRepository<Nomination> _nominationRepository;
        private readonly IGenericRepository<Operation> _operationRepository;
        private readonly IGenericRepository<Owner> _ownerRepository;
        private readonly IGenericRepository<Period> _periodRepository;

        public PositionService(
            ILogger<GenericService<PositionVM, Position>> logger,
            IMapper mapper,
            IGenericRepository<Position> positionRepository,
            IGenericRepository<Period> periodRepository,
            IGenericRepository<Category> categoryRepository,
            IGenericRepository<Measuring> measuringRepository,
            IGenericRepository<Nomination> nominationRepository,
            IGenericRepository<Operation> operationRepository,
            IGenericRepository<Owner> ownerRepository) 
            : base(logger, mapper, positionRepository)
        {
            _logger=logger;
            _mapper = mapper;
            _positionRepository = positionRepository;
            _periodRepository = periodRepository;
            _categoryRepository = categoryRepository;
            _measuringRepository = measuringRepository;
            _nominationRepository = nominationRepository;
            _operationRepository = operationRepository;
            _ownerRepository = ownerRepository;
        }

        public override async Task<IEnumerable<PositionVM>> GetAllAsync()
        {
            var vms = await _positionRepository.GetAllByAsync();

            if (vms is null)
            {
                string errorMessage = $"'{vms!.GetType().Name}' collection not found ";
                _logger.LogError(errorMessage);
                throw new ObjectNotFoundException(errorMessage);
            }

            #region Include

            foreach (var vm in vms)
            {
                vm.Category=await _categoryRepository.GetOneByAsync(_ => _.Id.Equals(vm.CategoryId));
                vm.Measuring=await _measuringRepository.GetOneByAsync(_ => _.Id.Equals(vm.MeasuringId));
                vm.Nomination=await _nominationRepository.GetOneByAsync(_ => _.Id.Equals(vm.NominationId));
                vm.Operation=await _operationRepository.GetOneByAsync(_ => _.Id.Equals(vm.OperationId));
                vm.Owner=await _ownerRepository.GetOneByAsync(_ => _.Id.Equals(vm.OwnerId));
                vm.Period=await _periodRepository.GetOneByAsync(_ => _.Id.Equals(vm.PeriodId));
            }

            #endregion

            var orderModels = vms
                .OrderBy(_ => _.Measuring?.Code)
                .ThenBy(_ => _.Nomination?.Name)
                .ThenBy(_ => _.Owner?.ShopCode)
                .ThenBy(_ => _.DeviceType)
                .ThenBy(_ => _.FactoryNumber)
                .ToList();

            var viewModels = _mapper.Map<IEnumerable<PositionVM>>(orderModels);
            return viewModels;
        }
        public override async Task<PositionVM> GetByIdAsync(Guid id)
        {
            var model = await _positionRepository.GetOneByAsync(_ => _.Id.Equals(id));

            if (model is null)
            {
                string errorMessage = $"'{model!.GetType().Name}' with id: '{id}' not found ";
                _logger.LogError(errorMessage);
                throw new ObjectNotFoundException(errorMessage);
            }

            var viewModel = _mapper.Map<PositionVM>(model);

            #region Include

            viewModel.Category=await _categoryRepository.GetOneByAsync(_ => _.Id.Equals(viewModel.CategoryId));
            viewModel.Measuring=await _measuringRepository.GetOneByAsync(_ => _.Id.Equals(viewModel.MeasuringId));
            viewModel.Nomination=await _nominationRepository.GetOneByAsync(_ => _.Id.Equals(viewModel.NominationId));
            viewModel.Operation=await _operationRepository.GetOneByAsync(_ => _.Id.Equals(viewModel.OperationId));
            viewModel.Owner=await _ownerRepository.GetOneByAsync(_ => _.Id.Equals(viewModel.OwnerId));
            viewModel.Period=await _periodRepository.GetOneByAsync(_ => _.Id.Equals(viewModel.PeriodId));

            #endregion

            return viewModel;
        }
        public override async Task CreateAsync(PositionVM vm)
        {
            vm.Created= DateTime.Now;
            var periodVm = await _periodRepository.GetOneByAsync(_ => _.Id.Equals(vm.PeriodId));

            if (periodVm is null) throw new ObjectNotFoundException($"'{periodVm!.GetType().Name}' with id: '{periodVm.Id}' not found ");
            else vm.NextDate = vm.PreviousDate.AddMonths(periodVm.Month);

            var model = _mapper.Map<Position>(vm);
            await _positionRepository.CreateAsync(model);
        }
        public override async Task UpdateAsync(PositionVM vm)
        {
            var periodVm = await _periodRepository.GetOneByAsync(_ => _.Id.Equals(vm.PeriodId));

            if (periodVm is null) throw new ObjectNotFoundException($"'{periodVm!.GetType().Name}' with id: '{periodVm.Id}' not found ");
            else vm.NextDate = vm.PreviousDate.AddMonths(periodVm.Month);

            var model = _mapper.Map<Position>(vm);
            var modelFromDb = await _positionRepository.GetOneByAsync(_ => _.Id.Equals(model.Id));

            if (modelFromDb is null)
            {
                string errorMessage = $"'{model!.GetType().Name}' with id: '{model.Id}' not found ";
                _logger.LogError(errorMessage);
                throw new ObjectNotFoundException(errorMessage);
            }

            var modelFromDbCreated = modelFromDb.Created;
            model.Created=modelFromDbCreated;
            await _positionRepository.UpdateAsync(model);
        }
        public override async Task DeleteAsync(Guid id)
        {
            var model = await _positionRepository.GetOneByAsync(_ => _.Id.Equals(id));

            if (model is null)
            {
                string errorMessage = $"'{model!.GetType().Name}' model with id: '{id}' not found ";
                _logger.LogError(errorMessage);
                throw new ObjectNotFoundException(errorMessage);
            }

            await _positionRepository.DeleteAsync(model);
        }
    }
}
