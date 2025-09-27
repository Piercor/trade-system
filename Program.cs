
using System.ComponentModel;
using System.Security.Cryptography;
using App;

List<User> users = new List<User>();

users.Add(new User("testuser1", "t@1", "pass"));
users.Add(new User("testuser2", "t@2", "pass"));

Dictionary<string, List<Item>> userItems = new Dictionary<string, List<Item>>();

userItems.Add("testuser1", new List<Item> { new Item("Pants", "Good condition", "testuser1") });
userItems["testuser1"].Add(new Item("Shirt", "Needs love", "testuser1"));

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
            break;
          case "2":
            currentMenu = Menu.Register;
            break;
          case "3":
            isRunning = false;
            break;

          default:
            Console.Write("\nInvalid input. Press ENTER to continue. ");
            Console.ReadLine();
            break;
        }
        break;

      case Menu.Login:

        try { Console.Clear(); } catch { }
        Console.WriteLine("\n\n----- The Trader's Peninsula -----\n");
        Console.WriteLine("\n--- Log in ---\n");
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
        break;

      case Menu.Register:

        try { Console.Clear(); } catch { }
        Console.WriteLine("\n\n----- The Trader's Peninsula -----\n");
        Console.WriteLine("\n--- Register a new account ---\n");

        Console.Write("\nName: ");
        string newName = Console.ReadLine().Trim();

        if (newName != null && newName != "")
        {
          Console.Write("\nEmail: ");
          string newEmail = Console.ReadLine().Trim();

          if (newEmail != null && newEmail != "")
          {
            bool existingUser = false;
            foreach (User user in users)
            {
              if (newEmail == user.Email)
              {
                Console.WriteLine("\nThere is another user already registered with that email.");
                Console.Write("\nPress ENTER to continue. ");
                Console.ReadLine();
                existingUser = true;
                currentMenu = Menu.None;
                break;
              }
            }
            if (!existingUser)
            {
              Console.Write("Repeat email: ");
              string repEmail = Console.ReadLine().Trim();

              if (newEmail != repEmail)
              {
                Console.WriteLine("\nEmail doesn't match.");
                Console.Write("\nPress ENTER to continue: ");
                Console.ReadLine();
                currentMenu = Menu.None;
                break;
              }
              else
              {
                Console.Write("\nPassword: ");
                string newPass = Console.ReadLine().Trim();

                if (newPass != null && newPass != "")
                {
                  Console.Write("Repeat password: ");
                  string repPass = Console.ReadLine().Trim();

                  if (newPass != repPass)
                  {
                    Console.WriteLine("\nPassword doesn't match.");
                    Console.Write("\nPress ENTER to continue: ");
                    Console.ReadLine();
                    currentMenu = Menu.None;
                    break;
                  }
                  else if (newEmail == newPass || newName == newPass)
                  {
                    Console.WriteLine("\nPassword can't be the same as name or email.");
                    Console.Write("\nPress ENTER to continue: ");
                    Console.ReadLine();
                    currentMenu = Menu.None;
                    break;
                  }
                  else if (newEmail == repEmail && newPass == repPass)
                  {
                    users.Add(new User(newName, newEmail, newPass));
                    Console.WriteLine($"\nNew user created. Welcome {newName}!");
                    Console.Write("\nPress ENTER to continue. ");
                    Console.ReadLine();
                    currentMenu = Menu.None;
                    break;
                  }
                }
                else
                {
                  Console.Write("\nPassword can't be empty. Press ENTER to continue. ");
                  Console.ReadLine();
                  currentMenu = Menu.None;
                  break;
                }
              }
            }
            break;
          }
          else
          {
            Console.Write("\nEmail can't be empty. Press ENTER to continue. ");
            Console.ReadLine();
            currentMenu = Menu.None;
          }
          break;
        }
        else
        {
          Console.Write("\nName can't be empty. Press ENTER to continue. ");
          Console.ReadLine();
          currentMenu = Menu.None;
          break;
        }
    }
  }
  else if (activeUser is User u)
  {
    switch (currentMenu)
    {
      case Menu.Main:

        try { Console.Clear(); } catch { }
        Console.WriteLine("\n\n----- The Trader's Peninsula -----\n");
        Console.WriteLine($"\n--- Welcome, {u.Name} ---\n");
        Console.WriteLine("\n[1] My items.");
        Console.WriteLine("\n[2] See the market.");
        Console.WriteLine("\n[3] Trade history.");
        Console.WriteLine("\n[4] Logout.");
        Console.Write("\n\nSelect an option [1-4]: ");

        switch (Console.ReadLine())
        {
          case "1":
            currentMenu = Menu.Items;
            break;

          case "2":
            currentMenu = Menu.Market;
            break;

          case "3":
            currentMenu = Menu.History;
            break;

          case "4":
            currentMenu = Menu.None;
            activeUser = null;
            break;

          default:
            Console.Write("\nInvalid input. Press ENTER to continue. ");
            Console.ReadLine();
            break;

        }
        break;

      case Menu.Items:
        try { Console.Clear(); } catch { }
        Console.WriteLine("\n\n----- The Trader's Peninsula -----\n");
        Console.WriteLine("\n--- My items ---\n");
        Console.WriteLine("\n[1] See my items.");
        Console.WriteLine("\n[2] Sell an item.");
        Console.WriteLine("\n[3] Buy requests."); // if possible, add a notification when somebody wants to buy an item
        Console.WriteLine("\n[4] Back to previous menu.");
        Console.Write("\n\nSelect an option [1-4]: ");

        switch (Console.ReadLine())
        {
          case "1":
            try { Console.Clear(); } catch { }
            Console.WriteLine("\n\n----- The Trader's Peninsula -----\n");
            Console.WriteLine("\n--- See my items ---\n");

            if (userItems.ContainsKey(u.Name))
            {
              foreach ((string user, List<Item> itemList) in userItems)
              {
                if (user == u.Name)
                {
                  foreach (Item item in itemList)
                  {
                    Console.WriteLine($"\n[{itemList.IndexOf(item) + 1}] - {item.Name}\n"
                    + $"{item.Description}");
                  }
                }
              }
            }
            else
            {
              Console.WriteLine("\n\nNo items to show.");
            }

            Console.Write("\n\nPress ENTER to go back to previous menu. ");
            Console.ReadLine();
            break;

          case "2":
            try { Console.Clear(); } catch { }
            Console.WriteLine("\n\n----- The Trader's Peninsula -----\n");
            Console.WriteLine("\n--- Sell an item ---\n");

            Console.WriteLine("\nWhat do you want to sell?");
            Console.Write("\nName: ");
            string newItem = Console.ReadLine();

            if (newItem != null && newItem != "")
            {
              Console.WriteLine("\nGive a descripton:");
              string newDescription = Console.ReadLine();

              if (newDescription != null && newDescription != "")
              {
                if (!userItems.ContainsKey(u.Name))
                {
                  userItems.Add(u.Name, new List<Item>());
                }
                userItems[u.Name].Add(new Item(newItem, newDescription, u.Name));
                Console.WriteLine($"\nYour new item '{newItem}' has been added!");
              }
              else
              {
                Console.WriteLine("\nItem's description can't be empty");
              }
            }
            else
            {
              Console.WriteLine("\nItem's name can't be empty");
            }
            Console.Write("\n\nPress ENTER to go back to previous menu. ");
            Console.ReadLine();
            break;


          case "4":
            currentMenu = Menu.Main;
            break;

          default:
            Console.Write("\nInvalid input. Press ENTER to continue. ");
            Console.ReadLine();
            break;
        }

        break;

      case Menu.Market:
        try { Console.Clear(); } catch { }
        Console.WriteLine("\n\n----- The Trader's Peninsula -----\n");
        Console.WriteLine("\n--- See the market ---\n");
        Console.WriteLine("\n[1] Browse items.");
        Console.WriteLine("\n[2] My requests."); // if possible, add notification when an trade have been aproved/denied.
        Console.WriteLine("\n[3] Back to previous menu.");
        Console.Write("\n\nSelect an option [1-3]: ");

        switch (Console.ReadLine())
        {
          case "3":
            currentMenu = Menu.Main;
            break;

          default:
            Console.Write("\nInvalid input. Press ENTER to continue. ");
            Console.ReadLine();
            break;
        }

        break;


      case Menu.History:
        try { Console.Clear(); } catch { }
        Console.WriteLine("\n\n----- The Trader's Peninsula -----\n");
        Console.WriteLine("\n--- Trade history ---\n");
        Console.WriteLine("[1] My items.");
        Console.WriteLine("[2] Others items.");
        Console.WriteLine("[3] Back to previous menu.");
        Console.Write("\n\nSelect an option [1-3]: ");

        switch (Console.ReadLine())
        {
          case "3":
            currentMenu = Menu.Main;
            break;

          default:
            Console.Write("\nInvalid input. Press ENTER to continue. ");
            Console.ReadLine();
            break;
        }

        break;
    }
  }
}
