
namespace App;

class Trade
{
  public string Item;
  public string Sender;
  public string Receiver;
  public TradeStatus Status;

  public Trade(string i, string s, string r, TradeStatus st)
  {
    Item = i;
    Sender = s;
    Receiver = r;
    Status = st;
  }
}