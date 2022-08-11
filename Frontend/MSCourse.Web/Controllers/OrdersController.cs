using Microsoft.AspNetCore.Mvc;
using MSCourse.Web.Models.OrderModels;
using MSCourse.Web.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace MSCourse.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        public OrdersController(IBasketService basketService, IOrderService orderService)
        {
            _basketService = basketService;
            _orderService = orderService;
        }

        public async Task<IActionResult> Checkout()
        {
            var basket = await _basketService.Get();

            ViewBag.basket = basket;
            ViewBag.discountError = null;

            return View(new CheckoutInfoInput());
        }


        /// <summary>
        /// Senkron iletişimde aşağıdaki metod kullanılıyor
        /// </summary>
        /// <param name="checkoutInfoInput"></param>
        /// <returns></returns>
        //[HttpPost]
        //public async Task<IActionResult> Checkout(CheckoutInfoInput checkoutInfoInput)
        //{
        //    var orderStatus = await _orderService.Create(checkoutInfoInput);

        //    if (!orderStatus.IsSuccessful)
        //    {
        //        var basket = await _basketService.Get();

        //        ViewBag.basket = basket;
        //        ViewBag.discountError = orderStatus.Error;
        //        return View();

        //        //TempData["orderError"] = orderStatus.Error;
        //        //return RedirectToAction(nameof(Checkout));
        //    }

        //    return RedirectToAction(nameof(Successful), new {orderId = orderStatus.OrderId});
        //}


        /// <summary>
        /// Asenkron iletişimde aşağıdaki metod kullanılıyor
        /// </summary>
        /// <param name="checkoutInfoInput"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutInfoInput checkoutInfoInput)
        {
            var orderSuspend = await _orderService.SuspendOrder(checkoutInfoInput);

            if (!orderSuspend.IsSuccessful)
            {
                var basket = await _basketService.Get();

                ViewBag.basket = basket;
                ViewBag.discountError = orderSuspend.Error;
                return View();
            }

            return RedirectToAction(nameof(Successful), new { orderId = new Random().Next(1,1000000) });
        }

        public IActionResult Successful(int orderId)
        {
            ViewBag.orderId = orderId;

            return View();
        }
    }
}
