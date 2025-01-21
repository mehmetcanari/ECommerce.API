using Microsoft.AspNetCore.Mvc;
using OnlineStoreWeb.API.Services.Account;

namespace OnlineStoreWeb.API.Controllers.Admin.Account;

[ApiController]
[Route("api/admin/accounts")]
public class AdminAccountController(IAccountService accountService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllAccounts()
    {
        try
        {
            var accounts = await accountService.GetAllAccountsAsync();
            return Ok(accounts);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An unexpected error occurred while fetching accounts" + ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAccountById(int id)
    {
        try
        {
            var account = await accountService.GetAccountWithIdAsync(id);
            return Ok(account);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An unexpected error occurred while fetching the account" + ex.Message);
        }
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAccount(int id)
    {
        try
        {
            await accountService.DeleteAccountAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An unexpected error occurred while deleting the account" + ex.Message);
        }
    }
}