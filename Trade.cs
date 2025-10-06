
namespace App;

/// <summary>
/// Trade class with a constructor to create a new trade.
/// </summary>
class Trade
{
  public string TradeID;
  public User Sender;
  public User Receiver;
  public TradeStatus Status;
  public List<Item> Items;

  /// <summary>
  /// Constructor to create new trades.
  /// </summary>
  /// <param name="ti">String with the trade ID.</param>
  /// <param name="s">User sender of the trade request.</param>
  /// <param name="r">User receiver of the trade request.</param>
  /// <param name="st">Enum with the status of the trade.</param>
  /// <param name="i">List of items being traded.</param>
  public Trade(string ti, User s, User r, TradeStatus st, List<Item> i)
  {
    TradeID = ti;
    Sender = s;
    Receiver = r;
    Status = st;
    Items = i;
  }
}