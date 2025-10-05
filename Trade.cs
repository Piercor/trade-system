
namespace App;

class Trade
{
  public string TradeID;
  public User Sender;
  public User Receiver;
  public TradeStatus Status;
  public List<Item> Items;

  public Trade(string ti, User s, User r, TradeStatus st, List<Item> i)
  {
    TradeID = ti;
    Sender = s;
    Receiver = r;
    Status = st;
    Items = i;
  }
}