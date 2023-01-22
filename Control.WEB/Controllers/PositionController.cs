using Control.BLL.Exceptions;
using Control.BLL.Interfaces;
using Control.BLL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Control.WEB.Controllers
{
    public sealed class PositionController : Controller
    {
        private readonly ILogger<PositionController> _logger;
        private readonly IPositionService _positionService;
        private readonly ICategoryService _categoryService;
        private readonly IMeasuringService _measuringService;
        private readonly INominationService _nominationService;
        private readonly IOperationService _operationService;
        private readonly IOwnerService _ownerService;
        private readonly IPeriodService _periodService;

        public PositionController(
            ILogger<PositionController> logger,
            IPositionService positionService,
            ICategoryService categoryService,
            IMeasuringService measuringService,
            INominationService nominationService,
            IOperationService operationService,
            IOwnerService ownerService,
            IPeriodService periodService)
        {
            _logger=logger;
            _positionService=positionService;
            _categoryService=categoryService;
            _measuringService=measuringService;
            _nominationService=nominationService;
            _operationService=operationService;
            _ownerService=ownerService;
            _periodService=periodService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var vms = await _positionService.GetAsync();
                return View(vms);
            }

            catch (ObjectNotFoundException ex)
            {
                string message = ex.Message;
                _logger.LogError(message);
                return NotFound(message);
            }

            catch (Exception ex)
            {
                string message = ex.Message;
                _logger.LogError(message);
                return BadRequest(message);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                var categories = _categoryService.GetAsync().Result;
                var measurings = _measuringService.GetAsync().Result;
                var nominations = _nominationService.GetAsync().Result;
                var operations = _operationService.GetAsync().Result;
                var owners = _ownerService.GetAsync().Result;
                var periods = _periodService.GetAsync().Result;

                PositionCreatingVM positionCreatingVM = new()
                {
                    PositionVM=new(),

                    Categories=categories.Select(_ => new SelectListItem
                    {
                        Value=_.CategoryId.ToString(),
                        Text=_.Name
                    }),

                    Measurings=measurings.Select(_ => new SelectListItem
                    {
                        Value=_.MeasuringId.ToString(),
                        Text=_.Name
                    }),

                    Nominations=nominations.Select(_ => new SelectListItem
                    {
                        Value=_.NominationId.ToString(),
                        Text=_.Name
                    }),

                    Operations=operations.Select(_ => new SelectListItem
                    {
                        Value=_.OperationId.ToString(),
                        Text=_.Name
                    }),

                    Owners=owners.Select(_ => new SelectListItem
                    {
                        Value=_.OwnerId.ToString(),
                        Text=$"{_.Shop} {_.Production}",
                    }),

                    Periods=periods.Select(_ => new SelectListItem
                    {
                        Value=_.PeriodId.ToString(),
                        Text=_.Name
                    })
                };

                return View(positionCreatingVM);
            }

            catch (Exception ex)
            {
                string message = ex.Message;
                _logger.LogError(message);
                return BadRequest(message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PositionCreatingVM positionCreatingVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var vm = positionCreatingVM.PositionVM;
                    await _positionService.CreateAsync(vm!);
                    return RedirectToAction("Index");
                }

                else return View(positionCreatingVM);
            }

            catch (Exception ex)
            {
                string message = ex.Message;
                _logger.LogError(message);
                return BadRequest(message);
            }
        }

        //[HttpGet]
        //public async Task<IActionResult> Update(Guid id)
        //{
        //    try
        //    {
        //        var vm = await _positionService.GetByIdAsync(id);
        //        return View(vm);
        //    }

        //    catch (ObjectNotFoundException ex)
        //    {
        //        string message = ex.Message;
        //        _logger.LogError(message);
        //        return NotFound(message);
        //    }

        //    catch (Exception ex)
        //    {
        //        string message = ex.Message;
        //        _logger.LogError(message);
        //        return BadRequest(message);
        //    }
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Update(PositionVM vm)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            await _positionService.UpdateAsync(vm);
        //            return RedirectToAction();
        //        }

        //        else return View(vm);
        //    }

        //    catch (Exception ex)
        //    {
        //        string message = ex.Message;
        //        _logger.LogError(message);
        //        return BadRequest(message);
        //    }
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    try
        //    {
        //        var vm = await _positionService.GetByIdAsync(id);
        //        await _positionService.DeleteAsync(vm);
        //        return RedirectToAction("Index");
        //    }

        //    catch (ObjectNotFoundException ex)
        //    {
        //        string message = ex.Message;
        //        _logger.LogError(message);
        //        return NotFound(message);
        //    }

        //    catch (Exception ex)
        //    {
        //        string message = ex.Message;
        //        _logger.LogError(message);
        //        return BadRequest(message);
        //    }
        //}
    }
}
