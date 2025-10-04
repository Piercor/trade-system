
namespace App;

class Item
{
  public string Name;
  public string Description;
  public User Owner;
  bool Trading;

  public Item(string n, string d, User o, bool t)
  {
    Name = n;
    Description = d;
    Owner = o;
    Trading = t;
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