
namespace App;

class Item
{
  public string Name;
  public string Description;
  public string Owner;
  public List<string> Interested;

  public Item(string n, string d, string o, List<string> i)
  {
    Name = n;
    Description = d;
    Owner = o;
    Interested = i;
  }
}