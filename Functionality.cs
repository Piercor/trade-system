
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
      Console.WriteLine($"---------- {inMenu} ----------");
    }
  }

  public static void NewMenu(string[] menuOptions)
  {
    string menuChoice = "";

    for (int i = 0; i < menuOptions.Length; i++)
    {
      menuChoice = $"\n[{i + 1}] {menuOptions[i]}";
      Console.WriteLine(menuChoice);
    }
    Console.WriteLine($"\nSelect an option [1-{menuOptions.Length}]");
  }
}