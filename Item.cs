
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
}