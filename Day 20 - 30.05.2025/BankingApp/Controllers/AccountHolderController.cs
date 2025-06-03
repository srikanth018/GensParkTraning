using System.Threading.Tasks;
using BankApp.Interfaces;
using BankApp.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountHolderController : ControllerBase
    {
        private readonly IAccountHolderService _accountHolderService;

        public AccountHolderController(IAccountHolderService accountHolderService)
        {
            _accountHolderService = accountHolderService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAccountHolder([FromBody] AddAccountHolderRequestDTO accountHolder)
        {
            if (accountHolder == null)
            {
                return BadRequest("Account holder data is required.");
            }

            var result = await _accountHolderService.AddAccountHolderAsync(accountHolder);
            if (result != null)
            {
                return Ok("Account holder created successfully.");
            }
            else
            {
                return StatusCode(500, "An error occurred while creating the account holder.");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAccountHolderById(int id)
        {
            var accountHolder = await _accountHolderService.GetAccountHolderByIdAsync(id);
            if (accountHolder == null)
            {
                return NotFound($"Account holder with ID {id} not found.");
            }
            return Ok(accountHolder);
        }

    }
}