﻿using AutoMapper;
using Control.BLL.Exceptions;
using Control.BLL.Interfaces;
using Control.BLL.ViewModels;
using Control.DAL.Interfaces;
using Control.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Control.BLL.Services
{
	public sealed class PositionService:GenericService<PositionVM,Position>,IPositionService
	{
        private readonly ILogger<GenericService<PositionVM, Position>> _logger;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Position> _positionRepository;
        private readonly IGenericRepository<Period> _periodRepository;

        public PositionService(
            ILogger<GenericService<PositionVM, Position>> logger,
            IMapper mapper,
            IGenericRepository<Position> positionRepository,
            IGenericRepository<Period> periodRepository) 
            : base(logger, mapper, positionRepository)
        {
            _logger=logger;
            _mapper = mapper;
            _positionRepository = positionRepository;
            _periodRepository = periodRepository;
        }

        public override async Task<IEnumerable<PositionVM>> GetAllAsync()
        {
            var models = await _positionRepository.GetAllByAsync(
                include:query=>query
                    .Include(_ => _.Category)
                    .Include(_ => _.Measuring)
                    .Include(_ => _.Nomination)
                    .Include(_ => _.Operation)
                    .Include(_ => _.Owner)
                    .Include(_ => _.Period)!);

            if (models is null)
            {
                string errorMessage = $"'{models!.GetType().Name}' collection not found ";
                _logger.LogError(errorMessage);
                throw new ObjectNotFoundException(errorMessage);
            }

            var orderModels = models
                .OrderBy(_ => _.Measuring!.Code)
                .ThenBy(_ => _.Nomination!.Name)
                .ThenBy(_ => _.OwnerId)
                .ThenBy(_ => _.DeviceType)
                .ThenBy(_ => _.FactoryNumber)
                .ToList();

            var viewModels = _mapper.Map<IEnumerable<PositionVM>>(orderModels);
            return viewModels;
        }
        public override async Task CreateAsync(PositionVM vm)
        {
            var model = _mapper.Map<Position>(vm);
            var period = await _periodRepository.GetOneByAsync(_ => _.Id.Equals(vm.PeriodId));

            if (period is null) throw new ObjectNotFoundException($"'{period!.GetType().Name}' with id: '{period.Id}' not found ");
            else model.NextDate = model.PreviousDate.AddMonths(period.Month);

            model.Created=DateTime.Now;
            await _positionRepository.CreateAsync(model);
        }
        public override async Task UpdateAsync(PositionVM viewModel)
        {
            var model = _mapper.Map<Position>(viewModel);
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
