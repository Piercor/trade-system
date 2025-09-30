
namespace App;

class Trade
{
  public string Item;
  public string Seller;
  public string Buyer;
  public TradeStatus Status;

  public Trade(string i, string s, string r, TradeStatus st)
  {
    Item = i;
    Seller = s;
    Buyer = r;
    Status = st;
  }
}