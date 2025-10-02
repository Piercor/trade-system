
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

User? activeUser = null;

bool isRunning = true;

Menu currentMenu = Menu.None;

while (isRunning)
{
  if (activeUser == null)
  {
    switch (currentMenu)
    {
      case Menu.None:
        Functionality.TopMenu("none");
        Functionality.NewMenu(menuOptions: new[] { "Login", "Create account", "Quit" });

        switch (Console.ReadLine())
        {
          case "1":
            currentMenu = Menu.Login;
            break;

          case "2":
            currentMenu = Menu.Register;
            break;

          case "3":
            isRunning = false;
            break;

          default:
            Functionality.ErrorMsg("inv", "cont");
            break;
        }
        break;

      case Menu.Login:

        Functionality.TopMenu("Login");

        Console.Write("\nEmail: ");
        string? email = Console.ReadLine();

        Console.Write("\nPass: ");
        string? pass = Console.ReadLine();

        Debug.Assert(email != null);
        Debug.Assert(pass != null);

        foreach (User user in users)
        {
          if (user.Login(email, pass))
          {
            activeUser = user;
            currentMenu = Menu.Main;
            break;
          }
        }
        if (activeUser == null)
        {
          Console.WriteLine("\n\nNo users were found with the given email and password.\n");
          Functionality.ErrorMsg("", "prev");
          currentMenu = Menu.None;
        }

        break;

      case Menu.Register:

        break;
    }
  }
  else if (activeUser is User u)
  {
    switch (currentMenu)
    {
      case Menu.Main:

        break;

      case Menu.Items:

        break;

      case Menu.Market:

        break;

      case Menu.History:

        break;
    }
  }
}