
namespace App;

class Trade
{
  public User Sender;
  public User Receiver;
  public List<Item> Items;

  public Trade(User s, User r, List<Item> i)
  {
    Sender = s;
    Receiver = r;
    Items = i;
  }
}