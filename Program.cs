
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Security.Cryptography;
using App;

List<User> users = new List<User>();

// test users

users.Add(new User("testuser1", "t@1", "pass"));
users.Add(new User("testuser2", "t@2", "pass"));
users.Add(new User("Alice", "a@1", "pass"));
users.Add(new User("Bob", "b@2", "pass"));
users.Add(new User("Carla", "c@3", "pass"));
users.Add(new User("Dan", "d@4", "pass"));
users.Add(new User("Eve", "e@5", "pass"));
users.Add(new User("Frank", "f@6", "pass"));

Dictionary<string, List<Item>> userItems = new Dictionary<string, List<Item>>();
Dictionary<string, Dictionary<string, List<Trade>>> userTrades = new Dictionary<string, Dictionary<string, List<Trade>>>();

// test items

userItems.Add("testuser1", new List<Item> { new Item("Pants", "Good condition", "testuser1", new List<string>()) });
userItems["testuser1"].Add(new Item("Shirt", "Needs love", "testuser1", new List<string>()));

userItems.Add("Alice", new List<Item>
{
    new Item("Dress", "Like new", "Alice", new List<string>()),
    new Item("Book: The Alchemist", "Worn cover, all pages intact", "Alice", new List<string>())
});

userItems.Add("Bob", new List<Item>
{
    new Item("Wireless Mouse", "Good condition", "Bob", new List<string>())
});

userItems.Add("Carla", new List<Item>
{
    new Item("Yoga Mat", "Used, but clean", "Carla", new List<string>()),
    new Item("Blouse", "Needs stitching", "Carla", new List<string>()),
    new Item("Bluetooth Speaker", "Loud and clear", "Carla", new List<string>())
});

userItems.Add("Dan", new List<Item>
{
       new Item("Board Game: Catan", "All pieces included", "Dan", new List<string>()),
    new Item("Wrist Watch", "Battery needs replacement", "Dan", new List<string>())
});

userItems.Add("Eve", new List<Item>
{
    new Item("Backpack", "Zipper broken", "Eve", new List<string>())
});

userItems.Add("Frank", new List<Item>
{
    new Item("Book: 1984", "Great condition", "Frank", new List<string>())
});


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
        string? email = Console.ReadLine();
        Console.Write("\nPassword: ");
        string? password = Console.ReadLine();

        Debug.Assert(email != null);
        Debug.Assert(password != null);

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
          Console.Write("\nPress ENTER to go back to previous menu. ");
          Console.ReadLine();
          currentMenu = Menu.None;
        }
        break;

      case Menu.Register:

        try { Console.Clear(); } catch { }
        Console.WriteLine("\n\n----- The Trader's Peninsula -----\n");
        Console.WriteLine("\n--- Register a new account ---\n");

        Console.Write("\nName: ");
        string? newName = Console.ReadLine()?.Trim();
        Debug.Assert(newName != null);

        if (newName != null && newName != "")
        {
          Console.Write("\nEmail: ");
          string? newEmail = Console.ReadLine()?.Trim();
          Debug.Assert(newEmail != null);

          if (newEmail != null && newEmail != "")
          {
            bool existingUser = false;
            foreach (User user in users)
            {
              if (newEmail == user.Email)
              {
                Console.WriteLine("\nThere is another user already registered with that email.");
                Console.Write("\nPress ENTER to go back to previous menu. ");
                Console.ReadLine();
                existingUser = true;
                currentMenu = Menu.None;
                break;
              }
            }
            if (!existingUser)
            {
              Console.Write("Repeat email: ");
              string? repEmail = Console.ReadLine()?.Trim();
              Debug.Assert(repEmail != null);

              if (newEmail != repEmail)
              {
                Console.WriteLine("\nEmail doesn't match.");
                Console.Write("\nPress ENTER to go back to previous menu. ");
                Console.ReadLine();
                currentMenu = Menu.None;
                break;
              }
              else
              {
                Console.Write("\nPassword: ");
                string? newPass = Console.ReadLine()?.Trim();
                Debug.Assert(newPass != null);

                if (newPass != null && newPass != "")
                {
                  Console.Write("Repeat password: ");
                  string? repPass = Console.ReadLine()?.Trim();
                  Debug.Assert(repPass != null);

                  if (newPass != repPass)
                  {
                    Console.WriteLine("\nPassword doesn't match.");
                    Console.Write("\nPress ENTER to go back to previous menu. ");
                    Console.ReadLine();
                    currentMenu = Menu.None;
                    break;
                  }
                  else if (newEmail == newPass || newName == newPass)
                  {
                    Console.WriteLine("\nPassword can't be the same as name or email.");
                    Console.Write("\nPress ENTER to go back to previous menu. ");
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
                  Console.Write("\nPassword can't be empty. Press ENTER to go back to previous menu. ");
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
            Console.Write("\nEmail can't be empty. Press ENTER to go back to previous menu. ");
            Console.ReadLine();
            currentMenu = Menu.None;
          }
          break;
        }
        else
        {
          Console.Write("\nName can't be empty. Press ENTER to go back to previous menu. ");
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
            Console.Write("\nInvalid input. Press ENTER to go back to previous menu. ");
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
          case "1": // my items >> see my items
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

          case "2": // my items >> sell item
            try { Console.Clear(); } catch { }
            Console.WriteLine("\n\n----- The Trader's Peninsula -----\n");
            Console.WriteLine("\n--- Sell an item ---\n");

            Console.WriteLine("\nWhat do you want to sell?");
            Console.Write("\nName: ");
            string? newItem = Console.ReadLine();
            Debug.Assert(newItem != null);

            if (newItem != null && newItem != "")
            {
              Console.WriteLine("\nGive a descripton:");
              string? newDescription = Console.ReadLine();
              Debug.Assert(newDescription != null);

              if (newDescription != null && newDescription != "")
              {
                if (!userItems.ContainsKey(u.Name))
                {
                  userItems.Add(u.Name, new List<Item>());
                }
                userItems[u.Name].Add(new Item(newItem, newDescription, u.Name, new List<string>()));
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

          case "3": // my items >> buy request

            try { Console.Clear(); } catch { }
            Console.WriteLine("\n\n----- The Trader's Peninsula -----\n");
            Console.WriteLine("\n--- Buy requests ---\n");

            if (userTrades.Count > 0)
            {
              foreach ((string sellerKey, Dictionary<string, List<Trade>> buyerDict) in userTrades)
              {
                if (sellerKey == u.Name)
                {
                  string soldItem = "";

                  foreach ((string buyerKey, List<Trade> tradeList) in buyerDict)
                  {
                    foreach (Trade trade in tradeList)
                    {
                      if (soldItem == trade.Item)
                      {
                        trade.Status = TradeStatus.Denied;
                      }
                      if (trade.Status == TradeStatus.Pending)
                      {
                        Console.WriteLine($"\nYou have a buy request for your item '{trade.Item}',\n"
                        + $"from {trade.Buyer}");

                        Console.Write("\n\nDo you want to accept this trade? [Y/N]: ");
                        switch (Console.ReadLine()?.ToLower())
                        {
                          case "y":
                            foreach ((string itemKey, List<Item> itemList) in userItems)
                            {
                              if (itemList.Count > 0)
                              {
                                foreach (Item item in itemList)
                                {
                                  if (itemKey == u.Name && item.Name == trade.Item)
                                  {
                                    trade.Status = TradeStatus.Accepted;
                                    // trade.Sold = true;
                                    itemList.Remove(item);
                                    Console.WriteLine($"\n\nBuy request from {trade.Buyer} accepted!");
                                    soldItem = item.Name;
                                    break;
                                  }
                                }
                              }
                            }
                            break;

                          case "n":
                            trade.Status = TradeStatus.Denied;
                            Console.WriteLine($"\n\nBuy request from {trade.Buyer} denied.");
                            // Console.Write("\nPress ENTER to go back to previous menu. ");
                            // Console.ReadLine();
                            break;

                          default:
                            Console.Write("\nInvalid input. Press ENTER to go back to previous menu. ");
                            break;
                        }
                      }

                    }
                  }
                }
                else
                {
                  Console.WriteLine("\nNo trade requests to show.");
                  // Console.Write("\nNo trade requests to show. Press ENTER to go back to previous menu. ");
                  // Console.ReadLine();
                  break;
                }
              }
            }
            else
            {
              Console.WriteLine("\nNo trade requests to show.");
              // Console.Write("\nNo trade requests to show. Press ENTER to go back to previous menu. ");
              // Console.ReadLine();
              // break;
            }
            Console.Write("\n\nPress ENTER to go back to previous menu. ");
            Console.ReadLine();
            break;

          case "4": // back to previous menu
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
          case "1": // see the market >> browse
            try { Console.Clear(); } catch { }
            Console.WriteLine("\n\n----- The Trader's Peninsula -----\n");
            Console.WriteLine("\n--- Browse items ---\n");

            foreach ((string key, List<Item> itemList) in userItems)
            {
              if (u.Name != key)
              {
                Console.WriteLine($"\nSeller: {key}");

                foreach (Item item in itemList)
                {
                  Console.WriteLine($"\n[{itemList.IndexOf(item) + 1}] {item.Name}\n"
                 + $"{item.Description}.");
                }
                Console.WriteLine("------------------------------");
              }
            }
            Console.WriteLine("\n[1] Buy something.");
            Console.WriteLine("[2] Back to previous menu.");
            Console.Write("\nSelect an option [1-2]. ");
            switch (Console.ReadLine())
            {
              case "1":
                Console.WriteLine("\n------------------------------\n");

                foreach ((string key, List<Item> itemList) in userItems)
                {
                  if (key != u.Name)
                  {
                    Console.Write($"{key} | ");
                  }
                }
                Console.Write("\n\nWrite the name of the seller you want to buy from: ");
                string? choosedSeller = Console.ReadLine();
                Debug.Assert(choosedSeller != null);

                if (choosedSeller != null && choosedSeller != "")
                {
                  if (userItems.ContainsKey(choosedSeller))
                  {
                    try { Console.Clear(); } catch { }
                    Console.WriteLine("\n\n----- The Trader's Peninsula -----\n");
                    Console.WriteLine($"\n--- Buy something from {choosedSeller} ---\n");

                    foreach ((string key, List<Item> itemList) in userItems)
                    {
                      if (choosedSeller == key)
                      {
                        foreach (Item item in itemList)
                        {
                          Console.WriteLine($"\n[{itemList.IndexOf(item) + 1}] {item.Name}\n"
                          + $"{item.Description}.");
                        }
                      }
                    }
                    Console.Write("\nSelect item's index to send a buy request. ");
                    string? choosedIndex = Console.ReadLine();
                    Debug.Assert(choosedIndex != null);
                    int index = 0;

                    if (choosedIndex != null && choosedIndex != "")
                    {
                      foreach (Item item in userItems[choosedSeller])
                      {
                        if (int.TryParse(choosedIndex, out index) && index > 0 && index <= userItems[choosedSeller].Count)
                        {
                          if (index == userItems[choosedSeller].IndexOf(item) + 1)
                          {
                            if (!userTrades.ContainsKey(item.Owner))
                            {
                              userTrades.Add(item.Owner, new Dictionary<string, List<Trade>>());
                            }
                            if (!userTrades[item.Owner].ContainsKey(u.Name))
                            {
                              userTrades[item.Owner].Add(u.Name, new List<Trade>());
                            }
                            if (item.Interested.Contains(u.Name))
                            {
                              Console.WriteLine("\n\nYou have sent a buy request already!");
                              Console.Write("\n\nPress ENTER to go back to previous menu. ");
                              Console.ReadLine();
                            }
                            else
                            {
                              userTrades[item.Owner][u.Name].Add(new Trade(item.Name, item.Owner, u.Name, TradeStatus.Pending));
                              item.Interested.Add(u.Name);
                              Console.WriteLine($"\n\nRequest to buy {item.Name} sended to {item.Owner}");
                              Console.Write("\n\nPress ENTER to go back to previous menu. ");
                              Console.ReadLine();
                            }
                            break;
                          }
                        }
                        else
                        {
                          Console.Write("\nIndex not found. Press ENTER to go back to previous menu. ");
                          Console.ReadLine();
                          break;
                        }
                      }
                    }
                    else
                    {
                      Console.Write("\nInvalid input. Press ENTER to go back to previous menu. ");
                      Console.ReadLine();
                    }

                  }
                  else
                  {
                    Console.WriteLine($"\nNo users found by the name of '{choosedSeller}'.");
                    Console.Write("\nPress ENTER to go back to previous menu. ");
                    Console.ReadLine();
                  }
                }
                else
                {
                  Console.Write("\nInvalid input. Press ENTER to go back to previous menu. ");
                  Console.ReadLine();
                }

                break;

              case "2":
                currentMenu = Menu.Market;
                break;

              default:
                Console.Write("\nInvalid input. Press ENTER to go back to previous menu. ");
                Console.ReadLine();
                break;
            }

            break;

          case "2": // see the market >> my requests 

            try { Console.Clear(); } catch { }
            Console.WriteLine("\n\n----- The Trader's Peninsula -----\n");
            Console.WriteLine("\n--- My requests ---\n");

            if (userTrades.Count > 0)
            {
              foreach ((string sellerKey, Dictionary<string, List<Trade>> buyerDict) in userTrades)
              {
                if (buyerDict.ContainsKey(u.Name))
                {
                  foreach ((string buyerKey, List<Trade> tradeList) in buyerDict)
                  {
                    if (buyerKey == u.Name && tradeList.Count > 0)
                    {
                      foreach (Trade trade in tradeList)
                      {
                        switch (trade.Status)
                        {
                          case TradeStatus.Pending:
                            Console.WriteLine($"\n{trade.Item} - sold by: {trade.Seller} - Pending...");
                            break;

                          case TradeStatus.Accepted:
                            Console.WriteLine($"\n{trade.Item} - sold by: {trade.Seller} - Accepted!");
                            break;

                          case TradeStatus.Denied:
                            Console.WriteLine($"\n{trade.Item} - sold by: {trade.Seller} - Denied :(");
                            break;
                        }
                      }
                    }

                  }
                }
                else
                {
                  Console.WriteLine("\nYou have no buying request pending.");
                }
              }
            }
            else
            {
              Console.WriteLine("\nYou have no buying request pending.");
            }
            Console.Write("\n\nPress ENTER to go back to previous menu. ");
            Console.ReadLine();
            break;

          case "3": // back to menu
            currentMenu = Menu.Main;
            break;

          default:
            Console.Write("\nInvalid input. Press ENTER to go back to previous menu. ");
            Console.ReadLine();
            break;
        }
        break;

      case Menu.History:
        try { Console.Clear(); } catch { }
        Console.WriteLine("\n\n----- The Trader's Peninsula -----\n");
        Console.WriteLine("\n--- Trade history ---\n");
        Console.WriteLine("\n[1] My items.");
        Console.WriteLine("\n[2] Other's items.");
        Console.WriteLine("\n[3] Back to previous menu.");
        Console.Write("\n\nSelect an option [1-3]: ");

        switch (Console.ReadLine())
        {
          case "1": // trade history >> my items
            try { Console.Clear(); } catch { }
            Console.WriteLine("\n\n----- The Trader's Peninsula -----\n");
            Console.WriteLine("\n--- My items ---\n");

            if (userTrades.Count > 0)
            {
              foreach ((string sellerKey, Dictionary<string, List<Trade>> buyerDict) in userTrades)
              {
                foreach ((string buyerKey, List<Trade> tradeList) in buyerDict)
                {
                  foreach (Trade trade in tradeList)
                  {
                    if (trade.Status == TradeStatus.Accepted || trade.Status == TradeStatus.Denied)
                    {
                      if (trade.Seller == u.Name)
                      {
                        Console.WriteLine($"\n[{tradeList.IndexOf(trade) + 1}] '{trade.Item}',\n"
                      + $"buyer: {trade.Buyer} - {(trade.Status == TradeStatus.Accepted ? "Accepted" : "Denied")}.");
                      }
                    }
                  }
                }
              }
            }
            else
            {
              Console.Write("\nNo transactions to show. Press ENTER to go back to previous menu. ");
              Console.ReadLine();
            }

            Console.Write("\n\nPress ENTER to go back to previous menu. ");
            Console.ReadLine();
            break;

          case "2": // trade history >> other's items

            break;

          case "3": // back to previous menu
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



