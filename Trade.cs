
namespace App;

class Trade
{
  public string Item;
  public string Sender;
  public string Receiver;
  public Enum Status;

  public Trade(string i, string s, string r, Enum st)
  {
    Item = i;
    Sender = s;
    Receiver = r;
    Status = st;
  }
}