
namespace App;

class Item
{
  public string Name;
  public string Description;
  public User Owner;
  public string itemID;

  public Item(string n, string d, User o, string id)
  {
    Name = n;
    Description = d;
    Owner = o;
    itemID = id;
  }
  public string ShowItems(User user)
  {
    if (user == Owner)
    { return $"{Name}\n  {Description}"; }
    else if (user != Owner)
    { return $"{Name}\n  {Description}\n  Owner: {Owner.Name}"; }
    else { return $"\nNo items found"; }
  }
}