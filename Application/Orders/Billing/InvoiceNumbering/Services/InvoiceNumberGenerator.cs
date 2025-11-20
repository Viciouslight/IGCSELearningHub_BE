using IGCSELearningHub.Application.Orders.Billing.InvoiceNumbering.Interfaces;
using System.Linq;

namespace IGCSELearningHub.Application.Orders.Billing.InvoiceNumbering.Services
{
    public class InvoiceNumberGenerator : IInvoiceNumberGenerator
    {
        private readonly IUnitOfWork _uow;

        public InvoiceNumberGenerator(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<string> GenerateAsync()
        {
            string number;
            do
            {
                number = $"INV-{DateTime.UtcNow:yyyyMMddHHmmssfff}";
            } while (!IsValid(number) || await _uow.InvoiceRepository.GetByInvoiceNumberAsync(number) != null);
            return number;
        }

        private static bool IsValid(string number)
        {
            return number.Length <= 32
                   && number.StartsWith("INV-")
                   && number.Length >= 8
                   && number.Skip(4).All(char.IsDigit);
        }
    }
}
