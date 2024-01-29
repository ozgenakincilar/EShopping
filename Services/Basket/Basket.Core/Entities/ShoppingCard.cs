namespace Basket.Core.Entities
{
    public class ShoppingCard
    {
        public string UserName { get; set; }
        public List<ShoppingCardItem> Items { get; set; } = new List<ShoppingCardItem>();
        public ShoppingCard()
        {

        }

        public ShoppingCard(string userName)
        {
            UserName=userName;
        }

    }
}
