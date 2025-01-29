using WpfApp.Model;

namespace WpfApp
{
    public class Helper
    {
        public static TradeEntities ent;
        public static TradeEntities GetContext()
        {
            if (ent == null)
            {
                ent = new TradeEntities();
            }
            return ent;
        }
    }
}
