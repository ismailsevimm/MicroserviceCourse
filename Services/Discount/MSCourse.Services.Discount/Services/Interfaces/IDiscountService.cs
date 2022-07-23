using MSCourse.Services.Discount.Dtos;
using MSCourse.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSCourse.Services.Discount.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<Response<List<DiscountDto>>> GetAll();
        Task<Response<DiscountDto>> GetById(int id);
        Task<Response<List<DiscountDto>>> GetByUserId(string userId);
        Task<Response<DiscountDto>> GetByCodeAndUserId(string code, string userId);
        Task<Response<NoContent>> Create(DiscountCreateDto discount);
        Task<Response<NoContent>> Update(DiscountUpdateDto discount);
        Task<Response<NoContent>> DeleteByUserId(string userId);
        Task<Response<NoContent>> Delete(int id);
    }
}
