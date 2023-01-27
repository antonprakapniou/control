using Control.BLL.Exceptions;
using Control.BLL.Interfaces;
using Control.BLL.ViewModels;
using Control.DAL.Configuration;
using Control.WEB.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Control.WEB.Controllers
{
    public sealed class PositionController : Controller
    {
        private const string _partialPath = AppConstants.PositionPartialPath;

        private readonly ILogger<PositionController> _logger;
        private readonly IPositionService _positionService;
        private readonly ICategoryService _categoryService;
        private readonly IMeasuringService _measuringService;
        private readonly INominationService _nominationService;
        private readonly IOperationService _operationService;
        private readonly IOwnerService _ownerService;
        private readonly IPeriodService _periodService;
        private readonly IFileManager _fileManager;

        public PositionController(
            ILogger<PositionController> logger,
            IPositionService positionService,
            ICategoryService categoryService,
            IMeasuringService measuringService,
            INominationService nominationService,
            IOperationService operationService,
            IOwnerService ownerService,
            IPeriodService periodService,
            IFileManager fileManager)
        {
            _logger=logger;
            _positionService=positionService;
            _categoryService=categoryService;
            _measuringService=measuringService;
            _nominationService=nominationService;
            _operationService=operationService;
            _ownerService=ownerService;
            _periodService=periodService;
            _fileManager=fileManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var vms = await _positionService.GetAllAsync();
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
        public async Task<IActionResult> Create()
        {
            try
            {
                var categories = await _categoryService.GetAllAsync();
                var measurings = await _measuringService.GetAllAsync();
                var nominations =await _nominationService.GetAllAsync();
                var operations = await _operationService.GetAllAsync();
                var owners = await _ownerService.GetAllAsync();
                var periods = await _periodService.GetAllAsync();

                PositionCreatingVM positionCreatingVM = new()
                {
                    PositionVM=new(),

                    Categories=categories.Select(_ => new SelectListItem
                    {
                        Value=_.Id.ToString(),
                        Text=_.Name
                    }),

                    Measurings=measurings.Select(_ => new SelectListItem
                    {
                        Value=_.Id.ToString(),
                        Text=_.Code
                    }),

                    Nominations=nominations.Select(_ => new SelectListItem
                    {
                        Value=_.Id.ToString(),
                        Text=_.Name
                    }),

                    Operations=operations.Select(_ => new SelectListItem
                    {
                        Value=_.Id.ToString(),
                        Text=_.Name
                    }),

                    Owners=owners.Select(_ => new SelectListItem
                    {
                        Value=_.Id.ToString(),
                        Text=$"{_.Shop} {_.Production}",
                    }),

                    Periods=periods.Select(_ => new SelectListItem
                    {
                        Value=_.Id.ToString(),
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

                    var files = HttpContext.Request.Form.Files;
                    if (files.Count()!=0)
                    {
                        _fileManager.Load(files, _partialPath);
                        vm!.Picture=_fileManager.FileName;
                    }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var model=await _positionService.GetByIdAsync(id);
                if (model.Picture is not null) _fileManager.Delete(model.Picture,_partialPath);
                await _positionService.DeleteAsync(id);
                return RedirectToAction("Index");
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
    }
}
