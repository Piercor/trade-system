
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
