
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
        Functionality.TopMenu("Welcome");
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
            Functionality.PrintMessage("", "inv", "cont");
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
          Functionality.PrintMessage("No users were found with the given email and password", "", "prev");
          currentMenu = Menu.None;
        }
        break;

      case Menu.Register:

        Functionality.TopMenu("Create an account");
        Console.Write("\nName: ");
        string? newName = Console.ReadLine()?.Trim();

        if (newName != null && newName != "")
        {
          Console.Write("\nEmail: ");
          string? newEmail = Console.ReadLine()?.Trim();

          if (newEmail != null && newEmail != "")
          {
            bool continueRegistration = true;

            foreach (User user in users)
            {
              if (user.Email == newEmail)
              {
                Functionality.PrintMessage("There is another user registered with that email already", "", "prev");
                currentMenu = Menu.None;
                continueRegistration = false;
                break;
              }
            }
            if (continueRegistration)
            {
              Console.Write("Repeat Email: ");
              string? repEmail = Console.ReadLine();
              Debug.Assert(repEmail != null);

              if (newEmail != repEmail)
              {
                Functionality.PrintMessage("", "Email doesn't match", "prev");
                currentMenu = Menu.None; break;
              }
              else
              {
                Console.Write("\nPassword: ");
                string? newPass = Console.ReadLine();

                if (newPass != null && newPass != "")
                {
                  if (newPass == newEmail)
                  {
                    Functionality.PrintMessage("Password can't be the same as email", "", "prev");
                    currentMenu = Menu.None;
                    break;
                  }
                  else if (newPass == newName)
                  {
                    Functionality.PrintMessage("Password can't be the same as name", "", "prev");
                    currentMenu = Menu.None; break;
                  }
                  else
                  {
                    Console.Write("Repeat password: ");
                    string? repPass = Console.ReadLine();

                    if (repPass != newPass)
                    {
                      Functionality.PrintMessage("Password doesn't match", "", "prev");
                      currentMenu = Menu.None; break;
                    }
                    else
                    {
                      Debug.Assert(newName != null);
                      Debug.Assert(newPass != null);
                      users.Add(new User(newName, newEmail, newPass));
                      string newUserLine = $"{newName},{newEmail},{newPass}";
                      File.AppendAllText("Users.csv", newUserLine + Environment.NewLine);
                      Console.WriteLine($"\nNew user sucessfully created. Welcome {newName}!");
                      Functionality.PrintMessage("", "", "prev");
                      currentMenu = Menu.None;
                      break;
                    }
                  }
                }
                else
                {
                  Functionality.PrintMessage("", "inv", "prev"); break;
                }
              }
            }
          }
          else { Functionality.PrintMessage("", "inv", "prev"); }
        }
        else { Functionality.PrintMessage("", "inv", "prev"); }
        currentMenu = Menu.None; break;
    }
  }
  else if (activeUser is User u)
  {
    switch (currentMenu)
    {
      case Menu.Main:
        Functionality.TopMenu($"Welcome, {u.Name}");
        Functionality.NewMenu(menuOptions: new[] { "My items", "See the market", "Trade history", "Log out" });
        switch (Console.ReadLine())
        {
          case "1": // my items
            currentMenu = Menu.Items; break;

          case "2": // see market

            break;

          case "3": // trade history

            break;

          case "4": // back 
            currentMenu = Menu.None; activeUser = null; break;

          default:
            Functionality.PrintMessage("", "inv", "cont"); break;
        }
        break;

      case Menu.Items:
        Functionality.TopMenu("My items");
        Functionality.NewMenu(menuOptions: new[] { "See my items", "Trade requests", "Back to previous menu" });
        switch (Console.ReadLine())
        {
          case "1": // my items >> see my items
            Functionality.TopMenu("See my items");
            foreach (Item item in userItems)
            {
              if (item.Owner.Email == u.Email)
              {
                Console.WriteLine(item.ShowItems("me"));
              }
            }
            Console.WriteLine();
            Functionality.NewMenu(menuOptions: new[] { "Add an item", "Back to previous menu" });
            switch (Console.ReadLine())
            {
              case "1":
                Functionality.TopMenu("Add an item");
                Console.Write("\nItem's name: ");
                string? newItemName = Console.ReadLine()?.Trim();

                if (newItemName != null && newItemName != "")
                {
                  Console.Write("\nItem's description: ");
                  string? newItemDescription = Console.ReadLine()?.Trim();
                  if (newItemDescription != null && newItemDescription != "")
                  {
                    Debug.Assert(newItemName != null);
                    Debug.Assert(newItemDescription != null);
                    userItems.Add(new Item(newItemName, newItemDescription, u));
                    string newItemLine = $"{newItemName};{newItemDescription};{u.Email}";
                    File.AppendAllText("Items.csv", newItemLine + Environment.NewLine);
                    Functionality.PrintMessage($"New item '{newItemName}' sucessfully added", "", "prev");
                  }
                  else
                  {
                    Functionality.PrintMessage("Item's description can't be empty", "", "prev");
                  }
                }
                else
                {
                  Functionality.PrintMessage("Item's name can't be empty", "", "prev");
                }
                break;

              case "2":
                currentMenu = Menu.Items; break;

              default:
                Functionality.PrintMessage("", "inv", "cont"); break;
            }
            break;

          case "2": // my items >> trade request

            break;

          case "3": // my items >> previous menu
            currentMenu = Menu.Main; break;

          default:
            Functionality.PrintMessage("", "inv", "cont"); break;
        }
        break;

      case Menu.Market:

        break;

      case Menu.History:

        break;
    }
  }
}