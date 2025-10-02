
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
            Functionality.ErrorMsg("", "inv", "cont");
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
          Functionality.ErrorMsg("No users were found with the given email and password", "", "prev");
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
                Functionality.ErrorMsg("There is another user registered with that email already", "", "prev");
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
                Functionality.ErrorMsg("", "Email doesn't match", "prev");
                currentMenu = Menu.None;
                break;
              }
              else
              {
                Console.Write("\nPassword: ");
                string? newPass = Console.ReadLine();

                if (newPass != null && newPass != "")
                {
                  if (newPass == newEmail)
                  {
                    Functionality.ErrorMsg("Password can't be the same as email", "", "prev");
                    currentMenu = Menu.None;
                    break;
                  }
                  else if (newPass == newName)
                  {
                    Functionality.ErrorMsg("Password can't be the same as name", "", "prev");
                    currentMenu = Menu.None;
                    break;
                  }
                  else
                  {
                    Console.Write("Repeat password: ");
                    string? repPass = Console.ReadLine();

                    if (repPass != newPass)
                    {
                      Functionality.ErrorMsg("Password doesn't match", "", "prev");
                      currentMenu = Menu.None;
                      break;
                    }
                    else
                    {
                      Debug.Assert(newName != null && newPass != null);
                      users.Add(new User(newName, newEmail, newPass));
                      string newUserLine = $"{newName},{newEmail},{newPass}";
                      File.AppendAllText("Users.csv", newUserLine + Environment.NewLine);
                      Console.WriteLine($"\nNew user sucessfully created. Welcome {newName}!");
                      Functionality.ErrorMsg("", "", "prev");
                      currentMenu = Menu.None;
                      break;
                    }
                  }
                }
                else
                {
                  Functionality.ErrorMsg("", "inv", "prev"); break;
                }
              }
            }
          }
          else { Functionality.ErrorMsg("", "inv", "prev"); }
        }
        else { Functionality.ErrorMsg("", "inv", "prev"); }
        currentMenu = Menu.None;
        break;

    }
  }
  else if (activeUser is User u)
  {
    switch (currentMenu)
    {
      case Menu.Main:
        Functionality.TopMenu($"Welcome, {u.Name}");
        Functionality.NewMenu(menuOptions: new[] { "Test" });
        Console.ReadLine();
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