
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
  string[] itemSplitData = itemData.Split(",");
  User? thisUser = null;

  foreach (User user in users)
  {
    thisUser = user;
  }

  Debug.Assert(thisUser != null);
  userItems.Add(new Item(itemSplitData[0], itemSplitData[1], thisUser));
}

/* test code
Console.WriteLine(userItems[3].Name);
Console.WriteLine(userItems[3].Description);
Console.WriteLine(userItems[3].Owner.Name);
*/
