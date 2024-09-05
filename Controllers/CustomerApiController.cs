using Microsoft.AspNetCore.Mvc;
using MvcMaxDebtProject.Models;
using System.Threading.Tasks;

[ApiController]
[Route("api/customer")] // Route düzenlendi
public class CustomerApiController : ControllerBase
{
    private readonly CustomerService _customerService;

    public CustomerApiController(CustomerService customerService)
    {
        _customerService = customerService;
    }

    // Tüm müşterileri getiren API endpoint'i
    [HttpGet]
    public async Task<IActionResult> GetAllCustomers()
    {
        try
        {
            var customers = await _customerService.GetCustomersAsync();
            return Ok(customers);
        }
        catch (Exception ex)
        {
            // Hata mesajını konsola yazdır
            Console.WriteLine($"An error occurred while getting all customers: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    // ID ile müşteri bilgisini getiren API endpoint'i
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerById(int id)
    {
        try
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }
        catch (Exception ex)
        {
            // Hata mesajını konsola yazdır
            Console.WriteLine($"An error occurred while getting the customer by ID: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }
}
