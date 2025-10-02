
using System.Data;

namespace App;

abstract class Functionality
{
  public static void TopMenu(string inMenu)
  {
    try { Console.Clear(); } catch { }
    if (inMenu == "none")
    {
      Console.WriteLine("\n\nWelcome to The Trader's Peninsula\n");
    }
    else
    {
      Console.WriteLine("\n\n----- The Trader's Peninsula -----\n");
      Console.WriteLine($"----- {inMenu} -----");
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
  public static void ErrorMsg(string cause, string action)
  {
    if (action == "continue")
    {
      action = "cont";
    }
    else if (action == "prev")
    {
      action = "go back to previous menu";
    }
    if (cause == "inv")
    {
      cause = "Invalid input";
    }
    else if (cause == "")
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