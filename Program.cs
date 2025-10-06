
using App;
using System.Data;
using System.Diagnostics;
using System.Net;

// List of the users that have already an account in the program.
List<User> users = new List<User>();

// The list of users is stored in the "Users.csv" file.
string[] userCsv = File.ReadAllLines("Users.csv");

// Here the program looks in every line of the file...
foreach (string userData in userCsv)
{//... and stores the different parts of the line (separated by a comma) in an array.
  string[] userSplitData = userData.Split(",");
  // Then, every index of the array (every part of the line) is used to add the different User parameters (name, email and password) to the users list.
  users.Add(new User(userSplitData[0], userSplitData[1], userSplitData[2]));
}

// Here is similar as with users, but with items.
List<Item> userItems = new List<Item>();

// The program gets its item info from the "Items.csv" file...
string[] itemsCsv = File.ReadAllLines("Items.csv");

//... and looks in every line of the file...
foreach (string itemData in itemsCsv)

{//... storing every part of the line, this time separated by a semicolon, in an array.
 // why semicolon and not comma? Because the item description can have commas, which renders the file unreadable.
  string[] itemSplitData = itemData.Split(";");

  // Now the program runs a foreach loop to check all users in the users list...                  
  foreach (User user in users)
  {
    //... and if the user Email is the same as the third index of the Items.csv file (which is the item owner's email)...
    if (user.Email == itemSplitData[2])
    {
      //... adds the item to the userItems list
      userItems.Add(new Item(itemSplitData[0], itemSplitData[1], user, itemSplitData[3]));
      break;
    }
  }
}

// One more time, a list to store (while the progam is running) the user trades...
List<Trade> userTrades = new List<Trade>();

//... and a "Trades.csv" file.
string[] tradesCsv = File.ReadAllLines("Trades.csv");

// Program looks every line of the file.
foreach (string tradeData in tradesCsv)
{
  // Creates a null user for the trade sender...
  User? tradeSender = null;
  // ...and for the receiver
  User? tradeReceiver = null;
  // A TradeStatus enum to check the status of the trade.
  TradeStatus tradeStatus = TradeStatus.Pending;
  // A list of the items that the trade is made of...
  List<Item> tradeList = new List<Item>();
  //... and said items. First the Receiver items...
  Item? firstItem = null;
  //... and then the Sender. Sender not always have items, so this object is going to be check for null later.
  Item? secondItem = null;

  // Array to store the parts of the lines of the .csv file.
  string[] tradeSplitData = tradeData.Split(",");

  // A forloop to check every user.
  foreach (User user in users)
  {
    // Program checks if the current user email is the same as the store in index 1 of the line...
    if (user.Email == tradeSplitData[1])
    {
      //... and if so, makes it the Sender of the trade.
      tradeSender = user;
    }
    // Same as before, but to check the Receiver of the trade.
    if (user.Email == tradeSplitData[2])
    {
      tradeReceiver = user;
    }
  }
  // Here checks the status of the trade.
  if (tradeSplitData[3] == "Pending")
  {
    tradeStatus = TradeStatus.Pending;
  }
  else if (tradeSplitData[3] == "Accepted")
  {
    tradeStatus = TradeStatus.Accepted;
  }
  else
  {
    tradeStatus = TradeStatus.Denied;
  }

  // And here a loop to check the userItems.
  foreach (Item item in userItems)
  {
    // If the item owner email is the same as index 4, and the item name is the same as index 5...
    if (item.itemID == tradeSplitData[4] && item.Name == tradeSplitData[5])
    {
      //... the first item (the trade receiver item), is temporaly stored as the firstItem object.
      firstItem = item;
    }
    // Same as before, but to check the items of the Sender...
    if (item.itemID == tradeSplitData[6] && item.Name == tradeSplitData[7])
    {
      //... which is stored as secondItem
      secondItem = item;
    }
  }

  Debug.Assert(firstItem != null);

  // After the items loop, the first item is added to the tradeList object...
  tradeList.Add(firstItem);

  //... and if a Sender item exist (if secondItem is not null), is added as well.
  if (secondItem != null) { tradeList.Add(secondItem); }

  Debug.Assert(tradeSender != null); Debug.Assert(tradeReceiver != null);

  // And here all the parts of the trade are added to form the trade and to add it to the userTrades list.
  userTrades.Add(new Trade(tradeSplitData[0], tradeSender, tradeReceiver, tradeStatus, tradeList));
}

// This User object is to check which user is logged in. No user is logged in when the program starts, so starts as null.
User? activeUser = null;

// boolean to check if the program is running. This would be used to terminate the program when needed.
bool isRunning = true;

// Menu enum to change the current menu. Starts as None.
Menu currentMenu = Menu.None;

while (isRunning)
{
  // If no user is logged in, which would happen everytime the program starts...
  if (activeUser == null)
  {
    //... this switch would check which is the current Menu...
    switch (currentMenu)
    {
      //... and, as said before, the current menu is "None" when the program starts.
      case Menu.None:
        // Method to write the top menu label.
        Functionality.TopMenu("Welcome");
        // Method to write the different menu options.
        Functionality.NewMenu(menuOptions: new[] { "Login", "Create account", "Quit" });

        // Every menu option have a number that the user can choose. This menu have 3, so there is 3 possible cases in this switch...
        switch (Console.ReadLine())
        {
          //... go to login menu.
          case "1":
            currentMenu = Menu.Login;
            break;
          //... go to register menu.
          case "2":
            currentMenu = Menu.Register;
            break;
          //... or quit.
          case "3":
            isRunning = false;
            break;
          // There is also a default in case the input is not "1", "2 or "3"...
          default:
            //... this wrong input will show invalid input message throug this PrintMessage method. Something similar would happen in every default along the way.
            Functionality.PrintMessage("", "inv", "cont");
            break;
        }
        break;

      case Menu.Login:

        Functionality.TopMenu("Login");

        // Here the user writes their Email...
        Console.Write("\nEmail: ");
        string? email = Console.ReadLine();

        //...  and password.
        Console.Write("\nPass: ");
        string? pass = Console.ReadLine();

        Debug.Assert(email != null);
        Debug.Assert(pass != null);

        // The program checks in the list of users (through a foreach loop)...
        foreach (User user in users)
        {
          //... and if there is an user in the list with that email/pass combination...
          if (user.Login(email, pass))
          {
            //... that user becomes the active user...
            activeUser = user;
            // and the current menu changes to Main, which is the user "homepage".
            currentMenu = Menu.Main;
            break;
          }
        }
        // This is to check if after the login attempt failed, and if so, write a message to the user.
        if (activeUser == null)
        {
          Functionality.PrintMessage("No users were found with the given email and password", "", "prev");
          currentMenu = Menu.None;
        }
        break;

      // Here can a new user account be created.
      case Menu.Register:

        Functionality.TopMenu("Create an account");

        // Name of the user of the new account. The Trim method is to not store a string with unnecessary spaces.
        Console.Write("\nName: ");
        string? newName = Console.ReadLine()?.Trim();

        // Systems checks if the input (newName) is null or empty. This would happen with every user input in the program.
        if (newName != null && newName != "")
        {
          // Then asks for the new email.
          Console.Write("\nEmail: ");
          string? newEmail = Console.ReadLine()?.Trim();

          if (newEmail != null && newEmail != "")
          {
            // This boolean is part of the check if the given email is already in use...
            bool continueRegistration = true;

            foreach (User user in users)
            {
              if (user.Email == newEmail)
              {
                Functionality.PrintMessage("There is another user registered with that email already", "", "prev");
                currentMenu = Menu.None;
                //... and if it is, the registration would stop...
                continueRegistration = false;
                break;
              }
            }
            //... otherwise would continue with asking the user to repeat the email...
            if (continueRegistration)
            {
              Console.Write("Repeat Email: ");
              string? repEmail = Console.ReadLine();
              Debug.Assert(repEmail != null);

              //... and check if is the same as the given before.
              if (newEmail != repEmail)
              {
                Functionality.PrintMessage("", "Email doesn't match", "prev");
                currentMenu = Menu.None; break;
              }
              else
              {
                // Here the user writes a password...
                Console.Write("\nPassword: ");
                string? newPass = Console.ReadLine();

                if (newPass != null && newPass != "")
                {
                  //... checks that is not the same as the email...
                  if (newPass == newEmail)
                  {
                    Functionality.PrintMessage("Password can't be the same as email", "", "prev");
                    currentMenu = Menu.None;
                    break;
                  }
                  // or the user name.
                  else if (newPass == newName)
                  {
                    Functionality.PrintMessage("Password can't be the same as name", "", "prev");
                    currentMenu = Menu.None; break;
                  }
                  else
                  {
                    // Repeat password...
                    Console.Write("Repeat password: ");
                    string? repPass = Console.ReadLine();

                    //... and check that is the same as the one given before...
                    if (repPass != newPass)
                    {
                      Functionality.PrintMessage("Password doesn't match", "", "prev");
                      currentMenu = Menu.None; break;
                    }
                    else
                    {
                      Debug.Assert(newName != null);
                      Debug.Assert(newPass != null);

                      //... and if everything is in order, a new user would be created and added to the users list.
                      users.Add(new User(newName, newEmail, newPass));
                      // This string is to save in the "Users.csv" file...
                      string newUserLine = $"{newName},{newEmail},{newPass}";
                      //... and here is the line appended to the file. The "Enviroment.NewLine" method is to add the new line in the different enviroments (Windows, Mac, Linux).
                      File.AppendAllText("Users.csv", newUserLine + Environment.NewLine);

                      Console.WriteLine($"\nNew user sucessfully created. Welcome {newName}!");
                      Functionality.PrintMessage("", "", "prev");
                      // Program goes back to first menu, where the new user can now log in.
                      currentMenu = Menu.None;
                      break;
                    }
                  }
                }
                else
                {
                  // Those following "else" is to when the user input is not allowed, writing a message informing the user what went wrong, and what happens next.
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
  // Now, when a user is logged in, the activeUser is said user ("u").
  else if (activeUser is User u)
  {
    // Again, a switch for the menus that a logged user sees...
    switch (currentMenu)
    {
      case Menu.Main:
        Functionality.TopMenu($"Welcome, {u.Name}");
        Functionality.NewMenu(menuOptions: new[] { "My items", "See the market", "Trade history", "Log out" });
        switch (Console.ReadLine())
        {
          //... and those menus.
          case "1":
            currentMenu = Menu.Items; break;

          case "2":
            currentMenu = Menu.Market; break;

          case "3":
            currentMenu = Menu.History; break;

          case "4":
            currentMenu = Menu.None; activeUser = null; break;

          default:
            Functionality.PrintMessage("", "inv", "cont"); break;
        }
        break;

      case Menu.Items:
        Functionality.TopMenu("My items");
        Functionality.NewMenu(menuOptions: new[] { "See my items", "Received trade requests", "Back to previous menu" });
        switch (Console.ReadLine())
        {
          case "1":
            Functionality.TopMenu("See my items");

            // The program loops the items in userItems...
            foreach (Item item in userItems)
            {
              // and if the owner email of an item is the same as the active user...
              if (item.Owner.Email == u.Email)
              {
                //... those items are printed in the console, using the ShowItems method.
                Console.WriteLine("\n• " + item.ShowItems(u));
              }
            }
            Console.WriteLine();
            Functionality.NewMenu(menuOptions: new[] { "Add an item", "Back to previous menu" });

            // Now have the user the option to add an item or go back to the previous menu.
            switch (Console.ReadLine())
            {
              case "1":
                Functionality.TopMenu("Add an item");

                // String for the new item name.
                Console.Write("\nItem's name: ");
                string? newItemName = Console.ReadLine()?.Trim();

                if (newItemName != null && newItemName != "")
                {
                  // String for item description.
                  Console.Write("\nItem's description: ");
                  string? newItemDescription = Console.ReadLine()?.Trim();
                  if (newItemDescription != null && newItemDescription != "")
                  {
                    Debug.Assert(newItemName != null);
                    Debug.Assert(newItemDescription != null);

                    string newItemID = Functionality.RandomIdGenerator();
                    // Here is the new item Name, Description and Owner added to the userItems list.
                    userItems.Add(new Item(newItemName, newItemDescription, u, newItemID));
                    // String to write in the "Items.csv" file...
                    string newItemLine = $"{newItemName};{newItemDescription};{u.Email};{newItemID}";
                    //... and that line being appended to the file.
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
                Functionality.PrintMessage("", "inv", "prev"); break;
            }
            break;

          // Here can the user see the received trade requests.
          case "2":
            Functionality.TopMenu("Received trade requests");
            // Boolean to check if a trade exist. Used to print a message and break to previous menu if no trade is found.
            bool foundTrade = false;

            // Loop through all the trades in userTrades list.
            foreach (Trade trade in userTrades)
            {
              // If those trades have the status as "Pending"...
              if (trade.Status == TradeStatus.Pending)
              {
                //... and the receiver of the trade request is the active user ("u")...
                if (trade.Receiver == u)
                {
                  //... the trade is shown. First the user sending it...
                  Console.WriteLine($"\n[{userTrades.IndexOf(trade) + 1}] Trade with {trade.Sender.Name}");
                  //... then the items they want from the user.
                  { Console.WriteLine($"\nMy items:"); }

                  // Loop through the items in the list of items of the trade.
                  foreach (Item item in trade.Items)
                  {
                    // If the item owner is no the sender (that means, the receiver)...
                    if (trade.Sender != item.Owner)
                    //... then the receiver's (active user "u" in this case) items (that are part of the trade request) are shown.
                    { Console.WriteLine($"• {item.Name} - {item.Description} "); }
                  }

                  // And then the sender of the trade name...
                  Console.WriteLine($"\n{trade.Sender.Name}'s items:");
                  foreach (Item item in trade.Items)
                  {
                    //... the program checks that the receiver is the active user and the owner of the item is not the active user...
                    if (trade.Receiver == u && item.Owner != u)
                    {
                      //... and their proposed item for trade (if any).
                      Console.WriteLine($"• {item.Name} - {item.Description}");
                    }
                  }
                  // Shows the status of the trade. 
                  Console.WriteLine($"\nStatus: {trade.Status.ToString()}");
                  Console.WriteLine("\n------------------------------");

                  // A trade was found...
                  foundTrade = true;
                  break;
                }
                else { foundTrade = false; }
              }
              else { foundTrade = false; }
            }
            //... so no message would be shown, and the program would continue to the next step...
            if (!foundTrade)
            { Functionality.PrintMessage("No trades to show", "", "prev"); break; }


            Functionality.NewMenu(menuOptions: new[] { "Accept/deny trade requests", "Back to previous menu" });

            //... which is the option to accept or deny the received trade requests, or go back to previous menu.
            switch (Console.ReadLine())
            {
              // Here can the user accept or deny a trade request.
              case "1":
                // This input string is to select the index of the trade in the userTrades list.
                Console.Write("\nSelect the index of the trade you want to manage: ");
                string? choosedTradeIndex = Console.ReadLine();
                // An int variable is created as well, this would later get the value of the previous string, if the string is not worng.
                int tradeIndex = 0;
                // This Trade object is set to null and later would be the selected trade.
                Trade? selectedTrade = null;

                // Checks the string.
                if (choosedTradeIndex != null && choosedTradeIndex != "")
                {
                  // Converts the string into the tradeIndex int, using a tryParse.
                  if (int.TryParse(choosedTradeIndex, out tradeIndex) && tradeIndex > 0 && tradeIndex <= userTrades.Count)
                  {
                    // Loop through the trades list...
                    foreach (Trade trade in userTrades)
                    {
                      //... and if tradeIndex value is the same as the index of the trade (plus one), and the trade receiver is the active user...
                      if (tradeIndex == userTrades.IndexOf(trade) + 1 && trade.Receiver == u)
                      {
                        //... the trade becomes "selectedTrade".
                        selectedTrade = trade;
                      }
                    }
                  }
                  else { Functionality.PrintMessage("", "inv", "prev"); break; }
                }
                else { Functionality.PrintMessage("", "inv", "prev"); break; }

                // Checks if the selected trade is still null (no trade found), and if so, breaks to previous menu.
                if (selectedTrade == null) { Functionality.PrintMessage("", "inv", "prev"); break; }

                // Now can the receiver of the trade either accept or deny the trade.
                Console.Write($"\nDo you want to accept the trade with {selectedTrade.Sender.Name}? [Y/N]: ");
                switch (Console.ReadLine()?.ToLower())
                {
                  // If trade is accepted:
                  case "y":

                    // First will loop through the items in the trade list of the selected trade...
                    foreach (Item item in selectedTrade.Items)
                    {
                      //... then check if the selected item as a index of 0 (first item in the list of items of the trade would always be the receiver's), and if the owner is the person receiving the trade request...
                      if (selectedTrade.Items.IndexOf(item) == 0 && item.Owner == selectedTrade.Receiver)
                      {
                        //... and then update the owner in the "Items.csv" file using the UpdateItemOwner method...
                        Functionality.UpdateItemOwner(item.Name, item.Description, item.Owner.Email, selectedTrade.Sender.Email);
                        //... as well as the owner of the item within the program.
                        item.Owner = selectedTrade.Sender;
                      }
                      // Same as before but for the trade request sender's item.
                      if (selectedTrade.Items.IndexOf(item) == 1 && item.Owner == selectedTrade.Sender)
                      {
                        Functionality.UpdateItemOwner(item.Name, item.Description, item.Owner.Email, selectedTrade.Receiver.Email);
                        item.Owner = selectedTrade.Receiver;
                      }
                    }
                    // Updates the status of the trade from "Pending" to "Accepted". First within the program...
                    selectedTrade.Status = TradeStatus.Accepted;
                    //... and then in the "Trades.csv" file using the UpdateTradeStatus method.
                    Functionality.UpdateTradeStatus(selectedTrade.TradeID, TradeStatus.Accepted);
                    Functionality.PrintMessage($"Trade with {selectedTrade.Sender.Name} accepted", "", "prev");
                    break;

                  case "n":
                    // If the trade request is denied then the selected trade would change the status to "Denied", within the program...
                    selectedTrade.Status = TradeStatus.Denied;
                    //... and in the "Trades.csv" file.
                    Functionality.UpdateTradeStatus(selectedTrade.TradeID, TradeStatus.Denied);
                    Functionality.PrintMessage($"Trade with {selectedTrade.Sender.Name} denied", "", "prev");
                    break;

                  default:
                    Functionality.PrintMessage("", "inv", "prev");
                    break;
                }

                break;

              case "2":
                currentMenu = Menu.Items;
                break;

              default:
                Functionality.PrintMessage("", "inv", "cont");
                break;
            }


            break;

          case "3":
            currentMenu = Menu.Main; break;

          default: Functionality.PrintMessage("", "inv", "cont"); break;
        }
        break;

      case Menu.Market:

        Functionality.TopMenu("See the market");
        Functionality.NewMenu(menuOptions: new[] { "See other people's items", "My trade requests", "Back to previous menu" });
        switch (Console.ReadLine())
        {
          case "1":
            // This boolean is used to be able to break the "isTrading" while loop when needed.
            bool isTrading = true;

            while (isTrading)
            {
              // First shows what items the rest of the users have.
              Functionality.TopMenu("See other people's items");
              // Checks all users...
              foreach (User user in users)
              {
                //... if the user is not the active user then...
                if (user != activeUser)
                {
                  //... checks the items in the userItems list...
                  foreach (Item item in userItems)
                  {
                    //... checks who's the owner...
                    if (item.Owner.Email == user.Email)
                    {
                      //... and print their name...
                      Console.WriteLine($"\n[{users.IndexOf(user) + 1}] {user.Name}");
                      break;
                    }
                  }
                  // Same loop again, but this time to show only the items. This way you have the name of the owner once, and then all of their items.
                  foreach (Item item in userItems)
                  {
                    if (item.Owner.Email == user.Email)
                    {
                      if (item.Name != "")
                      // Show their items.
                      { Console.WriteLine($"\n• " + item.ShowItems(u)); }
                    }
                  }
                  // This is the same as before, but separated to be able to print a line AFTER all the items of the user have been listed.
                  foreach (Item item in userItems)
                  {
                    if (item.Owner.Email == user.Email)
                    {
                      Console.WriteLine("\n------------------------------");
                      break;
                    }
                  }
                }
              }
              Console.WriteLine();
              Functionality.NewMenu(menuOptions: new[] { "Send a trade request", "Back to previous menu" });

              // Again, can the user choose what to do next. Send a trade request or go back to the previous menu.
              switch (Console.ReadLine())
              {
                // Here is the same process as when selecting a trade request to accept or deny, this time selecting an user through an index.
                case "1":
                  Console.Write("\nSelect an user index to make a trade request: ");
                  string? choosedUser = Console.ReadLine();
                  int userIndex = 0;
                  // User object that will become the user selected to send a trade request.
                  User? choosedTradeUser = null;
                  // New list of items that would be the items a trade is made of.
                  List<Item> tradeItems = new List<Item>();
                  // This boolean is to avoid showing users that have no items.
                  bool haveItem = false;

                  if (choosedUser != null & choosedUser != "")
                  {
                    if (int.TryParse(choosedUser, out userIndex) && userIndex > 0 && userIndex <= users.Count)
                    {
                      // Loop through all users.
                      foreach (User user in users)
                      {
                        // If the selected index (userIndex) is the index of the user...
                        if (userIndex == users.IndexOf(user) + 1)
                        {
                          //... that user email becomes the "choosedUser" string. This is done to avoid selecting the active user as the user to send a trade request to.
                          choosedUser = user.Email;
                          choosedTradeUser = user;
                          break;
                        }
                      }
                      // Here, if choosedUser is the same as the active user ("u") email, then the choosedTradeUser is null.
                      if (choosedUser == u.Email)
                      {
                        choosedTradeUser = null;
                      }
                      // And if choosedTradeUser is null at this point, the loop would break.
                      if (choosedTradeUser == null)
                      {
                        Functionality.PrintMessage("", "inv", "cont");
                        break;
                      }
                      foreach (Item item in userItems)
                      {
                        // Here the program checks if the selected user have any items...
                        if (item.Owner.Email == choosedUser)
                        {
                          haveItem = true;
                        }
                      }
                      //... and if not, the loop breaks.
                      if (!haveItem)
                      {
                        Functionality.PrintMessage("", "inv", "cont");
                        break;
                      }
                    }
                    else
                    {
                      Functionality.PrintMessage("", "inv", "cont");
                      break;
                    }
                  }
                  else
                  {
                    Functionality.PrintMessage("", "inv", "cont");
                    break;
                  }

                  Debug.Assert(choosedTradeUser != null);
                  Functionality.TopMenu($"Make a trade request with {choosedTradeUser.Name}");

                  // This is to show the items of the selected user one more time, this time with an index, which is the item index in the userItem list.
                  foreach (Item item in userItems)
                  {
                    if (item.Owner.Email == choosedUser)
                    {
                      Console.WriteLine($"[{userItems.IndexOf(item) + 1}] " + item.ShowItems(choosedTradeUser) + "\n");
                    }
                  }

                  // Here the user selects the index of the item they want.
                  Console.Write($"\nSelect the index of the item you want to trade with {choosedTradeUser.Name}: ");
                  string? choosedIndex = Console.ReadLine();
                  int selectedIndex = 0;
                  // Empty string that would be filled with the line that goes to the "Trades.csv" file.
                  string theirItemLine = "";

                  if (choosedIndex != null && choosedIndex != "")
                  {
                    if (int.TryParse(choosedIndex, out selectedIndex) && selectedIndex > 0 && selectedIndex <= userItems.Count)
                    {
                      foreach (Item item in userItems)
                      {
                        // If the selected item is owned by selected user...
                        if (selectedIndex == userItems.IndexOf(item) + 1 && item.Owner.Email == choosedUser)
                        {
                          //... the item is added to the list of items of this trade (tradeItems).
                          tradeItems.Add(item);
                          // Here fills "theirItemLine" string with item's info.
                          theirItemLine = $"{item.itemID},{item.Name},";
                          break;
                        }
                      }
                    }
                    else { Functionality.PrintMessage("", "inv", "prev"); break; }
                  }
                  else { Functionality.PrintMessage("", "inv", "prev"); break; }

                  Console.Write($"\nDo you want to trade some of your items with {choosedTradeUser.Name}? [Y/N]: ");

                  // Here can the active user select if they want to give some of their items as part of the trade...
                  switch (Console.ReadLine()?.ToLower())
                  {
                    //... and if they do...
                    case "y":

                      foreach (Item myItem in userItems)
                      {
                        //... the active user items would be shown, with an index to select.
                        if (u.Email == myItem.Owner.Email)
                        {
                          Console.WriteLine($"\n[{userItems.IndexOf(myItem) + 1}]" + myItem.ShowItems(activeUser));
                        }
                      }
                      // Here, as done with the other user, the active user can select some of thier own items.
                      Console.Write("\nSelect the index of the item you want to trade with: ");
                      string? myChoosedItem = Console.ReadLine();
                      int myItemIndex = 0;


                      if (myChoosedItem != null && myChoosedItem != "")
                      {
                        if (int.TryParse(myChoosedItem, out myItemIndex) && myItemIndex > 0 && myItemIndex <= userItems.Count)
                        {
                          foreach (Item item in userItems)
                          {
                            if (item.Owner.Email == u.Email)
                            {
                              // When the item is selected...
                              if (myItemIndex == userItems.IndexOf(item) + 1)
                              {
                                //... a trade ID is created...
                                string tradeID = Functionality.RandomIdGenerator();

                                //... the active user item is added to the list of items of the trade...
                                tradeItems.Add(item);
                                //... and the trade is added to the userTrades list.
                                userTrades.Add(new Trade(tradeID, activeUser, choosedTradeUser, TradeStatus.Pending, tradeItems));

                                // A new string is created with the info of the active user item...
                                string myItemLine = $"{item.itemID},{item.Name},";
                                //... as well as a string with the info of the trade...
                                string tradeLine = $"{tradeID},{activeUser.Email},{choosedTradeUser.Email},{TradeStatus.Pending},";
                                //... to be added to the "Trades.csv" file.
                                File.AppendAllText("Trades.csv", tradeLine + theirItemLine + myItemLine + Environment.NewLine);
                                break;
                              }
                            }
                          }
                        }
                        else { Functionality.PrintMessage("", "inv", "cont"); }
                      }
                      else { Functionality.PrintMessage("", "inv", "cont"); }

                      break;

                    // If the active user doesn't want to give one of their items as part of the trade.
                    case "n":

                      // As before, a random trade ID is created...
                      string newTradeID = Functionality.RandomIdGenerator();
                      //... the trade is added to the userTrades list...
                      userTrades.Add(new Trade(newTradeID, activeUser, choosedTradeUser, TradeStatus.Pending, tradeItems));
                      //... a string with the info of the trade is created...
                      string newTradeLine = $"{newTradeID},{activeUser.Email},{choosedTradeUser.Email},{TradeStatus.Pending},";
                      //... and then added to the "Trades.csv" file.
                      File.AppendAllText("Trades.csv", newTradeLine + theirItemLine + Environment.NewLine);
                      break;

                    default:
                      Functionality.PrintMessage("", "inv", "cont"); break;
                  }

                  Console.Write("\nDo you want to do another trade? [Y/N]: ");
                  switch (Console.ReadLine()?.ToLower())
                  {
                    // Here can the user choose to start another trade directly...
                    case "y":
                      break;
                    //... or go back to the previous menu.
                    case "n":
                      isTrading = false;
                      break;
                  }

                  break;

                case "2": isTrading = false; currentMenu = Menu.Market; break;

                default: Functionality.PrintMessage("", "inv", "cont"); break;
              }
            }
            break;

          // Here can the active user see the sent trade requests.
          case "2":
            Functionality.TopMenu("My trade requests");

            // Firt a loop through all the trades in userTrades...
            foreach (Trade trade in userTrades)
            {
              //... and then check if the sender of the trade is the active user ("u")
              if (trade.Sender == u)
              {
                // Here shows the name of the receiver of the trade request...
                Console.WriteLine($"\nTrade with {trade.Receiver.Name}");

                { Console.WriteLine($"\n{trade.Receiver.Name}'s items:"); }
                foreach (Item item in trade.Items)
                {
                  if (trade.Receiver == item.Owner)
                  //... and their item.
                  { Console.WriteLine($"• {item.Name} - {item.Description} "); }
                }

                Console.WriteLine($"\nmy items:");
                foreach (Item item in trade.Items)
                {
                  if (trade.Sender == u && item.Owner == u)
                  {
                    // And then the active user item.
                    Console.WriteLine($"• {item.Name} - {item.Description}");
                  }
                }
              }
              // Lastly the status of the trade.
              Console.WriteLine($"\nStatus: {trade.Status.ToString()}");
              Console.WriteLine("\n------------------------------");
            }

            Functionality.PrintMessage("", "", "prev");
            break;

          case "3":
            currentMenu = Menu.Main; break;

          default: Functionality.PrintMessage("", "inv", "cont"); break;

        }
        break;

      case Menu.History:
        Functionality.TopMenu("Trade history");
        Functionality.NewMenu(menuOptions: new[] { "Sent trade requests", "Received trade requests", "Back to previous menu" });

        switch (Console.ReadLine())
        {
          // Here can we see the history of sent trade requests.
          case "1":

            Functionality.TopMenu("Sent trade requests");
            // This boolean is to print a "no trade found" message if there is no history of sent trade by the active user.
            bool foundSentTrade = false;

            // Loop through trades in userTrades list...
            foreach (Trade trade in userTrades)
            {
              //... checks that the status of the trade is NOT "Pending"...
              if (trade.Status != TradeStatus.Pending)
              {
                //... and if the sender of the trade is the active user ("u").
                if (trade.Sender == u)
                {
                  // Then the receiver of the trade...
                  Console.WriteLine($"\nTrade with {trade.Receiver.Name}");

                  { Console.WriteLine($"\n{trade.Receiver.Name}'s item:"); }
                  foreach (Item item in trade.Items)
                  {
                    if (trade.Receiver == item.Owner)
                    //... their item...
                    { Console.WriteLine($"• {item.Name} - {item.Description} "); }
                  }

                  Console.WriteLine($"\nmy item:");
                  foreach (Item item in trade.Items)
                  {
                    if (trade.Sender == u && item.Owner == u)
                    {
                      //... and lastly active user's item.
                      Console.WriteLine($"• {item.Name} - {item.Description}");
                    }
                  }
                  Console.WriteLine($"\nStatus: {trade.Status.ToString()}");
                  Console.WriteLine("\n------------------------------");
                  // Since a trade history was found, the foundSentTrade boolean is now true.
                  foundSentTrade = true;
                }
                else { foundSentTrade = false; }
              }
              else { foundSentTrade = false; }
            }
            // If the foundSentTrade boolean is false, then this message is shown:
            if (!foundSentTrade)
            { Functionality.PrintMessage("No trades history to show", "", "prev"); break; }

            Functionality.PrintMessage("", "", "prev");
            break;

          // Here are the received trade request history.
          case "2":
            // Is pretty much the same as the sent trade request history, changing who's the sender and who's the receiver
            Functionality.TopMenu("Received trade requests");
            bool foundReceivedTrade = false;

            foreach (Trade trade in userTrades)
            {
              if (trade.Status != TradeStatus.Pending)
              {
                if (trade.Receiver == u)
                {
                  Console.WriteLine($"\nTrade with {trade.Sender.Name}");

                  { Console.WriteLine($"\nMy item:"); }
                  foreach (Item item in trade.Items)
                  {
                    if (trade.Sender != item.Owner)
                    { Console.WriteLine($"• {item.Name} - {item.Description} "); }
                  }

                  Console.WriteLine($"\n{trade.Sender.Name}'s item:");
                  foreach (Item item in trade.Items)
                  {
                    if (trade.Receiver == u && item.Owner != u)
                    {
                      Console.WriteLine($"• {item.Name} - {item.Description}");
                    }
                  }
                  Console.WriteLine($"\nStatus: {trade.Status.ToString()}");
                  Console.WriteLine("\n------------------------------");
                  foundReceivedTrade = true;
                  break;
                }
                else { foundReceivedTrade = false; }
              }
              else { foundReceivedTrade = false; }
            }
            if (!foundReceivedTrade)
            { Functionality.PrintMessage("No trades history to show", "", "prev"); break; }

            Functionality.PrintMessage("", "", "prev");
            break;

          case "3": currentMenu = Menu.Main; break;

          default: Functionality.PrintMessage("", "inv", "cont"); break;
        }
        break;
    }
  }
}