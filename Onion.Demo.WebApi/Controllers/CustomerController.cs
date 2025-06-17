using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Onion.Demo.Domain.Interfaces;
using Onion.Demo.Domain.Models;
using Onion.Demo.Infra.Data.UnitOfWork;

namespace Onion.Demo.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customer = await _unitOfWork.Customer.GetAllAsync();
            return Ok(customer);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var customer = await _unitOfWork.Customer.GetByIdAsync(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Customer customer)
        {
            await _unitOfWork.Customer.AddAsync(customer);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
        }

    }
}
