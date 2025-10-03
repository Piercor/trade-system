
namespace App;

class Item
{
  public string Name;
  public string Description;
  public User Owner;

  public Item(string n, string d, User o)
  {
    Name = n;
    Description = d;
    Owner = o;
  }
  public string ShowItems(string owner)
  {
    if (owner == "me")
    { return $"\n• {Name}\n{Description}"; }
    else { return $"\n• {Name}\n{Description}\n{Owner.Name}"; }
  }
}