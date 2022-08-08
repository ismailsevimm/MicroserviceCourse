namespace MSCourse.Web.Models.BasketModels
{
    public class BasketItemViewModel
    {
        public int Quantity { get; set; }

        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        private decimal? DiscountAppliedPrice { get; set; }

        public decimal GetCurrentPrice { get => DiscountAppliedPrice != null ? DiscountAppliedPrice.Value : Price; }

        public void AppliedDiscount(decimal discountPrice)
        {
            DiscountAppliedPrice = discountPrice;
        }
    }
}
