using MvcMaxDebtProject.Data;
using MvcMaxDebtProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CustomerService
{
    private readonly AppDbContext _context;

    public CustomerService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<DateTime?> GetDateOfMaximumDebtAsync(int customerId)
    {
        try
        {
            // Müşterinin faturalarını al
            var customerInvoices = await _context.MusteriFatura
                .Where(f => f.MusteriId == customerId)
                .ToListAsync();

            // Eğer hiç fatura yoksa, null döndür
            if (!customerInvoices.Any())
                return null;

            // Olayları tarihe göre sıralamak için bir liste oluştur
            var transactions = new List<(DateTime Date, decimal Amount)>();

            // Fatura tutarlarını ekle
            foreach (var invoice in customerInvoices)
            {
                transactions.Add((invoice.FaturaTarihi, invoice.FaturaTutari));
                
                // Eğer ödeme tarihi varsa, ödeme tutarını çıkar
                if (invoice.OdemeTarihi.HasValue)
                {
                    transactions.Add((invoice.OdemeTarihi.Value, -invoice.FaturaTutari));
                }
            }

            // Tarihe göre sıralama
            transactions = transactions.OrderBy(t => t.Date).ToList();

            // Net borç ve maksimum borç hesaplama
            decimal netDebt = 0;
            decimal maxDebt = 0;
            DateTime? maxDebtDate = null;

            foreach (var transaction in transactions)
            {
                netDebt += transaction.Amount;

                // Maksimum borç ve tarihi güncelle
                if (netDebt > maxDebt)
                {
                    maxDebt = netDebt;
                    maxDebtDate = transaction.Date;
                }
            }

            return maxDebtDate;
        }
        catch (Exception ex)
        {
            // Hata mesajını konsola yazdır
            Console.WriteLine($"An error occurred while getting the maximum debt date: {ex.Message}");
            return null;
        }
    }

    public async Task<MusteriTanim?> GetCustomerByIdAsync(int id)
    {
        try
        {
            return await _context.MusteriTanim.FirstOrDefaultAsync(c => c.Id == id);
        }
        catch (Exception ex)
        {
            // Hata mesajını konsola yazdır
            Console.WriteLine($"An error occurred while getting the customer by ID: {ex.Message}");
            return null;
        }
    }

    public async Task<List<MusteriTanim>> GetCustomersAsync()
    {
        try
        {
            return await _context.MusteriTanim.ToListAsync();
        }
        catch (Exception ex)
        {
            // Hata mesajını konsola yazdır
            Console.WriteLine($"An error occurred while getting the customers: {ex.Message}");
            return new List<MusteriTanim>();
        }
    }

    public async Task<List<(DateTime Date, decimal NetDebt)>> GetNetDebtHistoryAsync(int customerId)
    {
        try
        {
            var invoices = await _context.MusteriFatura
                .Where(f => f.MusteriId == customerId)
                .ToListAsync();

            var transactions = new List<(DateTime Date, decimal Amount)>();

            // Fatura ve ödeme işlemlerini ekleme
            foreach (var invoice in invoices)
            {
                transactions.Add((invoice.FaturaTarihi, invoice.FaturaTutari));

                if (invoice.OdemeTarihi.HasValue)
                {
                    transactions.Add((invoice.OdemeTarihi.Value, -invoice.FaturaTutari));
                }
            }

            // İşlemleri tarihe göre sıralama
            transactions = transactions.OrderBy(t => t.Date).ToList();

            // Net borçları hesaplama
            var netDebtHistory = new List<(DateTime Date, decimal NetDebt)>();
            decimal netDebt = 0;

            foreach (var transaction in transactions)
            {
                netDebt += transaction.Amount;
                netDebtHistory.Add((transaction.Date, netDebt));
            }
            return netDebtHistory;
        }
        catch (Exception ex)
        {
            // Hata mesajını konsola yazdır
            Console.WriteLine($"An error occurred while getting the net debt history: {ex.Message}");
            return new List<(DateTime Date, decimal NetDebt)>();
        }
    }
}
