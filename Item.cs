
namespace App;

/// <summary>
/// Item class with a constructor to create new items, and a method to show them.
/// </summary>
class Item
{
  public string Name;
  public string Description;
  public User Owner;
  public string itemID;

  /// <summary>
  /// Constructor to create new items.
  /// </summary>
  /// <param name="n">String with the name of the item.</param>
  /// <param name="d">String with the description of the item.</param>
  /// <param name="o">User that is the owner of the item.</param>
  /// <param name="id">String with the ID of the item.</param>
  public Item(string n, string d, User o, string id)
  {
    Name = n;
    Description = d;
    Owner = o;
    itemID = id;
  }

  /// <summary>
  /// Method to show items. Takes the given user and checks if it is or isn't the owner of the item and returns a string.
  /// </summary>
  /// <param name="user">The user to check.</param>
  /// <returns></returns>
  public string ShowItems(User user)
  {
    if (user == Owner)
    { return $"{Name}\n  {Description}"; }
    else if (user != Owner)
    { return $"{Name}\n  {Description}\n  Owner: {Owner.Name}"; }
    else { return $"\nNo items found"; }
  }
}