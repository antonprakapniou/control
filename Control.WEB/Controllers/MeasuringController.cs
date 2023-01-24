using Control.BLL.Exceptions;
using Control.BLL.Interfaces;
using Control.BLL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Control.WEB.Controllers
{
	public sealed class MeasuringController : Controller
	{
		private readonly ILogger<MeasuringController> _logger;
		private readonly IMeasuringService _service;

		public MeasuringController(
			ILogger<MeasuringController> logger,
			IMeasuringService service)
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
		public async Task<IActionResult> Create(MeasuringVM vm)
		{
			try
			{
				if (ModelState.IsValid)
				{
					await _service.CreateAsync(vm);
					return RedirectToAction("Index");
				}

				else return View(vm) ;


            }

            catch (Exception ex)
			{
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
		public async Task<IActionResult> Update(MeasuringVM vm)
		{
			try
			{
				if (ModelState.IsValid)
				{
                    await _service.UpdateAsync(vm);
                    return RedirectToAction();
                }

				else return	View(vm);
				
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
		public async Task<IActionResult> Delete(Guid id)
		{
            try
            {
                await _service.DeleteAsync(id);
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
