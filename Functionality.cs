
using System.Data;

namespace App;

abstract class Functionality
{
  public static void TopMenu(string menuTitle)
  {
    try { Console.Clear(); } catch { }
    if (menuTitle == "none")
    {
      Console.WriteLine("\n\nWelcome to The Trader's Peninsula\n");
    }
    else
    {
      Console.WriteLine("\n\n----- The Trader's Peninsula -----\n");
      Console.WriteLine($"----- {menuTitle} -----");
    }
  }

  public static void NewMenu(string[] menuOptions)
  {
    for (int i = 0; i < menuOptions.Length; i++)
    {
      Console.WriteLine($"\n[{i + 1}] {menuOptions[i]}");
    }
    Console.Write($"\n\nSelect an option [1-{menuOptions.Length}]: ");
  }
  public static void ErrorMsg(string msg, string cause, string action)
  {
    if (msg != "")
    {
      Console.WriteLine($"\n{msg}.");
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
      Console.Write($"\n{cause}. Press ENTER to {action}. ");
    }
    Console.ReadLine();
  }
}