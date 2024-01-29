namespace Basket.Application.Responses
{
    public class ShoppingCardResponse
    {
        public string UserName { get; set; }
        public List<ShoppingCardItemResponse> Items { get; set; }

        public ShoppingCardResponse()
        {
            
        }

        public ShoppingCardResponse(string userName)
        {
            UserName=userName;
        }

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;

                foreach (var item in Items)
                {
                    totalPrice += item.Price* item.Quantity;
                }
                return totalPrice;
            }
        }
    }
}
