
using App;
using System.Diagnostics;

List<User> users = new List<User>();

string[] userCsv = File.ReadAllLines("Users.csv");
foreach (string userData in userCsv)
{
  string[] userSplitData = userData.Split(",");
  users.Add(new User(userSplitData[0], userSplitData[1], userSplitData[2]));
}

List<Item> userItems = new List<Item>();

string[] itemsCsv = File.ReadAllLines("Items.csv");


foreach (string itemData in itemsCsv)
{
  string[] itemSplitData = itemData.Split(";");
  foreach (User user in users)
  {
    if (user.Email == itemSplitData[2])
    {
      userItems.Add(new Item(itemSplitData[0], itemSplitData[1], user));
      break;
    }
  }
}

/* test code
Console.WriteLine(userItems[0].Name);
Console.WriteLine(userItems[0].Description);
Console.WriteLine(userItems[0].Owner.Name);

Console.WriteLine(userItems[1].Name);
Console.WriteLine(userItems[1].Description);
Console.WriteLine(userItems[1].Owner.Name);

Console.WriteLine(userItems[2].Name);
Console.WriteLine(userItems[2].Description);
Console.WriteLine(userItems[2].Owner.Name);

Console.WriteLine(userItems[3].Name);
Console.WriteLine(userItems[3].Description);
Console.WriteLine(userItems[3].Owner.Name);
*/
