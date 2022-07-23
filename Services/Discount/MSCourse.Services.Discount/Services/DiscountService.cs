using AutoMapper;
using Dapper;
using Microsoft.Extensions.Configuration;
using MSCourse.Services.Discount.Dtos;
using MSCourse.Services.Discount.Services.Interfaces;
using MSCourse.Shared.Dtos;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MSCourse.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;
        private readonly IMapper _mapper;

        public DiscountService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<NoContent>> Create(DiscountCreateDto discount)
        {
            var result = await _dbConnection.ExecuteAsync
                ("INSERT INTO discount(userid, code, rate, activationendtime) VALUES(@UserId,@Code, @Rate, @ActivationEndTime)", discount);

            if (result < 1)
                return Response<NoContent>.Fail("Discount Could Not Create", 500);

            return Response<NoContent>.Success(204);
        }

        public async Task<Response<NoContent>> Update(DiscountUpdateDto discount)
        {
            var result = await _dbConnection.ExecuteAsync
                ("UPDATE discount SET userid = @UserId, code = @Code, rate = @Rate, activationendtime = @ActivationEndTime WHERE id = @Id", discount);

            if (result < 1)
                return Response<NoContent>.Fail("Discount Not Found", 404);

            return Response<NoContent>.Success(204);
        }

        public async Task<Response<NoContent>> DeleteByUserId(string userId)
        {
            var result = await _dbConnection.ExecuteAsync
                ("DELETE FROM discount WHERE userid=@UserId", new { UserId = userId });

            if (result < 1)
                return Response<NoContent>.Fail("Discount Not Found", 404);

            return Response<NoContent>.Success(204);
        }

        public async Task<Response<NoContent>> Delete(int id)
        {
            var result = await _dbConnection.ExecuteAsync
                ("DELETE FROM discount WHERE id=@Id", new { Id = id});

            if (result < 1)
                return Response<NoContent>.Fail("Discount Not Found", 404);

            return Response<NoContent>.Success(204);
        }

        public async Task<Response<List<DiscountDto>>> GetAll()
        {
            var discounts = await _dbConnection.QueryAsync<Models.Discount>
                ("SELECT * FROM discount");

            return Response<List<DiscountDto>>.Success(_mapper.Map<List<DiscountDto>>(discounts.ToList()), 200);
        }

        public async Task<Response<List<DiscountDto>>> GetByUserId(string userId)
        {
            var discounts = await _dbConnection.QueryAsync<Models.Discount>
                ("SELECT * FROM discount WHERE userid=@UserId", new { UserId = userId });

            return Response<List<DiscountDto>>.Success(_mapper.Map<List<DiscountDto>>(discounts.ToList()), 200);
        }

        public async Task<Response<DiscountDto>> GetByCodeAndUserId(string code, string userId)
        {
            var discount = await _dbConnection.QueryFirstOrDefaultAsync<Models.Discount>
                ("SELECT * FROM discount WHERE userid=@UserId AND code=@Code", new { UserId = userId, Code = code });

            if (discount == null)
                return Response<DiscountDto>.Fail("Discount Not Found", 404);

            return Response<DiscountDto>.Success(_mapper.Map<DiscountDto>(discount), 200);
        }

        public async Task<Response<DiscountDto>> GetById(int id)
        {
            var discount = await _dbConnection.QueryFirstOrDefaultAsync<Models.Discount>
                ("SELECT * FROM discount WHERE id=@Id", new { Id = id });

            if (discount == null)
                return Response<DiscountDto>.Fail("Discount Not Found", 404);

            return Response<DiscountDto>.Success(_mapper.Map<DiscountDto>(discount), 200);
        }
    }
}
