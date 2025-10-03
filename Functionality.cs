
using System.Data;

namespace App;

abstract class Functionality
{
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

  public static void NewMenu(string[] menuOptions)
  {
    for (int i = 0; i < menuOptions.Length; i++)
    {
      if (menuOptions[i].Trim() == "")
      {
        Console.WriteLine("\nMENU OPTIONS CAN'T BE EMPTY! PROGRAM WOULD FINISH NOW!");
        Environment.Exit(0);
      }
      Console.WriteLine($"\n[{i + 1}] {menuOptions[i].Trim()}.");
    }
    Console.Write($"\n\nSelect an option [{(menuOptions.Length > 1 ? "1-" + menuOptions.Length : menuOptions.Length)}]: ");
  }
  public static void PrintMessage(string msg, string cause, string action)
  {
    if (msg != "")
    {
      Console.WriteLine($"\n\n{msg}.");
    }

    if (action == "cont")
    {
      action = "continue";
    }
    else if (action == "prev")
    {
      action = "go back to previous menu";
    }

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
}