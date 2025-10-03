
namespace App;

class Trade
{
  public User Sender;
  public User Receiver;
  List<Item> _items = new List<Item>();

  public Trade(User s, User r)
  {
    Sender = s;
    Receiver = r;
  }
}