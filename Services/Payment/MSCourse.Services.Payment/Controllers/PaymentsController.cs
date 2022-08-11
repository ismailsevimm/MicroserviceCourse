﻿using Microsoft.AspNetCore.Mvc;
using MSCourse.Services.Payment.Models;
using MSCourse.Shared.ControllerBases;
using MSCourse.Shared.Dtos;
using MSCourse.Shared.Services.Interfaces;

namespace MSCourse.Services.Payment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : CustomControllerBase
    {
        private readonly ISharedIdentityService _sharedIdentityService;

        public PaymentsController(ISharedIdentityService sharedIdentityService)
        {
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpPost]
        public IActionResult ReceivePayment(PaymentDto paymentDto)
        {
            return CreateActionResultInstance(Response<string>.Success(_sharedIdentityService.GetUserId,200));
        }
    }
}
