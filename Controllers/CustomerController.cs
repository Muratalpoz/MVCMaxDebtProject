using Microsoft.AspNetCore.Mvc;
using MvcMaxDebtProject.Models;
using System.Threading.Tasks;

public class CustomerController : Controller
{
    private readonly CustomerService _customerService;

    public CustomerController(CustomerService customerService)
    {
        _customerService = customerService;
    }

    // Müşteri listesini görüntüler
    public async Task<IActionResult> Index()
    {
        try
        {
            var customers = await _customerService.GetCustomersAsync();
            return View(customers);
        }
        catch (Exception ex)
        {
            // Hata durumunda kullanıcıya bilgi ver veya logla
            Console.WriteLine($"An error occurred while getting the customer list: {ex.Message}");
            return StatusCode(500, "Internal server error.");
        }
    }

    // Maksimum borç tarihini görüntüler
    public async Task<IActionResult> MaximumDebtDate(int id)
    {
        try
        {
            var date = await _customerService.GetDateOfMaximumDebtAsync(id);
            if (date == null)
                return NotFound();

            ViewBag.Date = date.Value.ToString("yyyy-MM-dd");

            var netDebtHistory = await _customerService.GetNetDebtHistoryAsync(id);
            var dates = netDebtHistory.Select(nd => nd.Date.ToString("yyyy-MM-dd")).ToArray();
            var netDebts = netDebtHistory.Select(nd => nd.NetDebt).ToArray();

            ViewBag.Dates = dates;
            ViewBag.NetDebts = netDebts;
            
            return View();
        }
        catch (Exception ex)
        {
            // Hata durumunda kullanıcıya bilgi ver veya logla
            Console.WriteLine($"An error occurred while getting the maximum debt date: {ex.Message}");
            return StatusCode(500, "Internal server error.");
        }
    }
}
