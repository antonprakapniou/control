using Control.BLL.Exceptions;
using Control.BLL.Interfaces;
using Control.BLL.ViewModels;
using Control.DAL.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace Control.WEB.Controllers
{
    public sealed class OwnerController : Controller
    {
        private readonly ILogger<OwnerController> _logger;
        private readonly IOwnerService _service;

        public OwnerController(
            ILogger<OwnerController> logger,
            IOwnerService service)
        {
            _logger=logger;
            _service=service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var vms = await _service.GetAllAsync();
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
                return View();
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
        public async Task<IActionResult> Create(OwnerVM vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _service.CreateAsync(vm);
                    TempData[AppConstants.ToastrSuccess]=AppConstants.ToastrCreateSuccess;
                    return RedirectToAction("Index");
                }

                else return View();

            }

            catch (Exception ex)
            {
                TempData[AppConstants.ToastrError]=AppConstants.ToastrCreateError;
                string message = ex.Message;
                _logger.LogError(message);
                return BadRequest(message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            try
            {
                var vm = await _service.GetByIdAsync(id);
                return View(vm);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(OwnerVM vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _service.UpdateAsync(vm);
                    TempData[AppConstants.ToastrSuccess]=AppConstants.ToastrUpdateSuccess;
                    return RedirectToAction();
                }

                else return View();
            }

            catch (Exception ex)
            {
                TempData[AppConstants.ToastrError]=AppConstants.ToastrUpdateError;
                string message = ex.Message;
                _logger.LogError(message);
                return BadRequest(message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
                TempData[AppConstants.ToastrSuccess]=AppConstants.ToastrDeleteSuccess;
                return RedirectToAction("Index");
            }

            catch (ObjectNotFoundException ex)
            {
                TempData[AppConstants.ToastrError]=AppConstants.ToastrDeleteError;
                string message = ex.Message;
                _logger.LogError(message);
                return NotFound(message);
            }

            catch (Exception ex)
            {
                TempData[AppConstants.ToastrError]=AppConstants.ToastrDeleteError;
                string message = ex.Message;
                _logger.LogError(message);
                return BadRequest(message);
            }
        }
    }
}
