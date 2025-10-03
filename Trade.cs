
namespace App;

class Trade
{
  public User Sender;
  public User Receiver;
  public TradeStatus Status;
  public List<Item> Items;

  public Trade(User s, User r, TradeStatus st, List<Item> i)
  {
    Sender = s;
    Receiver = r;
    Status = st;
    Items = i;
  }
}