
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
        if (activeUser == null)
        {
          Console.WriteLine($"\nNo users were found with the given email/password");
          Console.Write("\nPress ENTER to continue. ");
          Console.ReadLine();
          currentMenu = Menu.None;
        }
        continue;

      case Menu.Register:
        try { Console.Clear(); } catch { }
        Console.WriteLine("\n\nRegister a new account.\n");

        Console.Write("\nName: ");
        string newName = Console.ReadLine().Trim();

        if (name is valid)
        {
          be om email
          if (email is valid)
          {
            be om lösen
            if
          }
          else
        }
        else

        if (string.IsNullOrEmpty(newName))
        {
          Console.Write("\nName can't be empty. PRess ENTER to continue. ");
          Console.ReadLine();
          currentMenu = Menu.None;
          continue;
        }

        Console.Write("\nEmail: ");
        string newEmail = Console.ReadLine().Trim();

        if (string.IsNullOrEmpty(newEmail))
        {
          Console.Write("\nEmail can't be empty. PRess ENTER to continue. ");
          Console.ReadLine();
          currentMenu = Menu.None;
          continue;
        }

        Console.Write("Repeat email: ");
        string repEmail = Console.ReadLine().Trim();

        if (newEmail != repEmail)
        {
          Console.WriteLine("\nEmail doesn't match.");
          Console.Write("\nPress ENTER to continue: ");
          Console.ReadLine();
          currentMenu = Menu.None;
          continue;
        }

        bool new_user_already_exists = false;
        foreach (User user in users)
        {
          if (newEmail == user.Email)
          {
            Console.WriteLine("\nThere is another user already registered with that email.");
            Console.Write("\nPress ENTER to continue. ");
            Console.ReadLine();
            currentMenu = Menu.None;
            new_user_already_exists = true;
            break;
          }
        }

        if (!new_user_already_exists)
        {
          Console.Write("\nPassword: ");
          string newPass = Console.ReadLine().Trim();

          if (string.IsNullOrEmpty(newPass))
          {
            Console.Write("\nPassword can't be empty. PRess ENTER to continue. ");
            Console.ReadLine();
            currentMenu = Menu.None;

          }
          else
          {
            Console.Write("Repeat password: ");
            string repPass = Console.ReadLine().Trim();

            if (newPass != repPass)
            {
              Console.WriteLine("\nPassword doesn't match.");
              Console.Write("\nPress ENTER to continue: ");
              Console.ReadLine();
              currentMenu = Menu.None;
              continue;
            }
          }



          if (newEmail == newPass || newName == newPass)
          {
            Console.WriteLine("\nPassword can't be the same as name or email.");
            Console.Write("\nPress ENTER to continue: ");
            Console.ReadLine();
            currentMenu = Menu.None;
            continue;
          }

          if (newEmail == repEmail && newPass == repPass)
          {
            users.Add(new User(newName, newEmail, newPass));
            Console.WriteLine($"\nNew user created. Welcome {newName}!");
            Console.Write("\nPress ENTER to continue. ");
            Console.ReadLine();
            currentMenu = Menu.None;
            continue;
          }
        }
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
