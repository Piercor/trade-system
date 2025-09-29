
namespace App;

class Trade
{
  public Item Item;
  public User Sender;
  public User Receiver;
  public Enum Status;

  public Trade(Item i, User s, User r, Enum st)
  {
    Item = i;
    Sender = s;
    Receiver = r;
    Status = st;
  }
}