using MSCourse.Web.Models.PaymentModels;
using System.Threading.Tasks;

namespace MSCourse.Web.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> ReceivePayment(PayWithCardInput payWithCardInput);
    }
}
