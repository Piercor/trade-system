
using System.ComponentModel;
using App;

List<User> users = new List<User>();

users.Add(new User("testuser1", "t@1", "pass"));
users.Add(new User("testuser2", "t@2", "pass"));

User activeUser = null;

bool isRunning = true;

Menu currentMenu = Menu.None;

while (isRunning)
{
  if (activeUser == null)
  {
    switch (currentMenu)
    {
      case Menu.None:
        try { Console.Clear(); } catch { }
        Console.WriteLine("\n\nWelcome to The Trader's Peninsula.\n");
        Console.WriteLine("\n[1] Log in.");
        Console.WriteLine("\n[2] Create an account.");
        Console.WriteLine("\n[3] Quit");

        Console.Write("\nSelect an option [1-3]: ");
        switch (Console.ReadLine())
        {
          case "1":
            currentMenu = Menu.Login;
            continue;
          case "2":
            currentMenu = Menu.Register;
            continue;
          case "3":
            isRunning = false;
            continue;

          default:
            Console.Write("\nInvalid input. Press ENTER to continue. ");
            Console.ReadLine();
            continue;
        }

      case Menu.Login:

        try { Console.Clear(); } catch { }
        Console.WriteLine("\n\nLog in\n\n");
        Console.Write("\nEmail: ");
        string email = Console.ReadLine();
        Console.Write("\nPassword: ");
        string password = Console.ReadLine();

        foreach (User user in users)
        {
          if (user.Login(email, password))
          {
            activeUser = user;
            currentMenu = Menu.Main;
            break;
          }
        }

        // Console.WriteLine($"\nNo users were found with the given email/password");
        // Console.Write("\nPress ENTER to continue. ");
        // Console.ReadLine();
        // currentMenu = Menu.None;
        continue;

      case Menu.Register:
        Console.WriteLine("\nSoon.");
        Console.ReadLine();
        break;
    }
  }
  else
  {
    switch (currentMenu)
    {
      case Menu.Main:
        if (activeUser is User u)
        {
          try { Console.Clear(); } catch { }
          Console.WriteLine("\n\n----- The Trader's Peninsula -----\n");
          Console.WriteLine($"\n\nWelcome, {u.Name} \n");
        }
        break;
    }
  }
  break;
}
