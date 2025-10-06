
using System.Data;

namespace App;

/// <summary>
/// Functionality class with methods that serve to the functionality of the program. 
/// </summary>
class Functionality
{
  /// <summary>
  /// Method to "draw" the top label of a menu with a default (The Trader's Peninsula) 
  /// and a variable to write what it wants to be written.
  /// </summary>
  /// <param name="menuTitle">String to write something in the top menu.</param>
  public static void TopMenu(string menuTitle)
  {
    try { Console.Clear(); } catch { }
    if (menuTitle == "none")
    {
      Console.WriteLine("\n\n----- The Trader's Peninsula -----\n\n");
    }
    else
    {
      Console.WriteLine("\n\n----- The Trader's Peninsula -----\n");
      Console.WriteLine($"----- {menuTitle} -----\n");
    }
  }

  /// <summary>
  /// Method to write a given set of menu options.
  /// </summary>
  /// <param name="menuOptions">Array of strings. Each string of the array is a menu option.</param>
  public static void NewMenu(string[] menuOptions)
  {
    // A for loop to write the menu option index.
    for (int i = 0; i < menuOptions.Length; i++)
    {
      // Checks if the array have any empty strings and gives an error if so.
      if (menuOptions[i].Trim() == "")
      {
        Console.WriteLine("\nMENU OPTIONS CAN'T BE EMPTY! PROGRAM WOULD FINISH NOW!");
        Environment.Exit(0);
      }
      // Otherwise would write the different menu options with their index number (plus one to avoid 0). 
      Console.WriteLine($"\n[{i + 1}] {menuOptions[i].Trim()}.");
    }
    // And lastly it writes the "select an option" message with the amount of possible options.
    Console.Write($"\n\nSelect an option [{(menuOptions.Length > 1 ? "1-" + menuOptions.Length : menuOptions.Length)}]: ");
  }

  /// <summary>
  /// Method used to print different messages, mostly when something goes wrong.
  /// </summary>
  /// <param name="msg">String to write a message to the user.</param>
  /// <param name="cause">String to check what was the cause of the problem.</param>
  /// <param name="action">String to tell the user what would happen next.</param>
  public static void PrintMessage(string msg, string cause, string action)
  {
    // Program checks if there is any message to write, and if so, writes it on its own.
    if (msg != "")
    {
      Console.WriteLine($"\n\n{msg}.");
    }
    // Program checks the which action message should be shown.
    if (action == "cont")
    {
      action = "continue";
    }
    else if (action == "prev")
    {
      action = "go back to previous menu";
    }

    // Program checks which cause message should be shown.
    if (cause == "inv")
    {
      cause = "Invalid input";
    }
    if (cause == "")
    {
      Console.Write($"\nPress ENTER to {action}. ");
    }
    else
    {
      Console.Write($"\n\n{cause}. Press ENTER to {action}. ");
    }
    Console.ReadLine();
  }

  /// <summary>
  /// Method to generate random ID alphanumeric ID numbers, to both items and trades.
  /// </summary>
  /// <returns></returns>
  public static string RandomIdGenerator()
  {
    // Creates a random object that would be used to generate random numbers.
    Random rnd = new Random();

    // A string with the possible characters of the ID.
    string alphaNumeric = "abcdefghijklmnopqrstuvwxyz0123456789";

    // String that will store the generated ID.
    string randomAlphaNumeric = "";

    // For loop to generate 6 random characters.
    for (int i = 0; i < 6; i++)
    {
      // Generates a random int between 0 and the lenght of alphaNumeric. x would be a random index for the character.
      int x = rnd.Next(alphaNumeric.Length);
      // Adds a randomly selected character from alphaNumeric to the final string.
      randomAlphaNumeric = randomAlphaNumeric + alphaNumeric[x];
    }
    // Returns the randomly generated 6 character alpha numeric ID.
    return randomAlphaNumeric;
  }

  /// <summary>
  /// Method to update the state of a trade.
  /// </summary>
  /// <param name="tradeId">String with the ID of the trade.</param>
  /// <param name="newStatus">Enum with the new status of the trade.</param>
  public static void UpdateTradeStatus(string tradeId, TradeStatus newStatus)
  {
    // Reads all lines from the .csv file and stores them in a string array.
    string[] tradeLines = File.ReadAllLines("Trades.csv");

    // For loop through each line, using an index for them.
    for (int i = 0; i < tradeLines.Length; i++)
    {
      // Splits the current line in parts, separated by commas.
      string[] splitTradeLines = tradeLines[i].Split(",");
      // If the first part of a line (index 0) is same as the tradeId given...
      if (splitTradeLines[0] == tradeId)
      {
        //... changes the current status (which is stored in index 3 of the line) to the new one.
        // Since the status is an Enum, it has to be converted to a string (ToString()) to be able to store it.
        splitTradeLines[3] = newStatus.ToString();
        // The program rebuilds the line with the updated status, using a string.Join.
        tradeLines[i] = string.Join(",", splitTradeLines);
        break;
      }
    }
    // Writes all lines back to the .csv file, including the updated one.
    File.WriteAllLines("Trades.csv", tradeLines);
  }

  /// <summary>
  /// Method to update an item owner.
  /// </summary>
  /// <param name="itemName">String with the name of the item.</param>
  /// <param name="itemDescription">String with the description of te item.</param>
  /// <param name="oldOwnerEmail">String with the email of the current (old) owner.</param>
  /// <param name="newOwnerEmail">String with the email of the future (new) owner.</param>

  // This method works in a similar way as the UpdateTradeStatus.
  public static void UpdateItemOwner(string itemName, string itemDescription, string oldOwnerEmail, string newOwnerEmail)
  {
    string[] itemLines = File.ReadAllLines("Items.csv");

    for (int i = 0; i < itemLines.Length; i++)
    {
      string[] splitItemLines = itemLines[i].Split(";");

      if (splitItemLines[0] == itemName && splitItemLines[1] == itemDescription && splitItemLines[2] == oldOwnerEmail)
      {
        splitItemLines[2] = newOwnerEmail;
        itemLines[i] = string.Join(";", splitItemLines);
      }
    }
    File.WriteAllLines("Items.csv", itemLines);
  }
}
