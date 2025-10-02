
using System.Data;

namespace App;

abstract class Functionality
{

  public static void TopMenu(string inMenu)
  {
    try { Console.Clear(); } catch { }
    Console.WriteLine("\n\nWelcome to The Trader's Peninsula\n");
    if (inMenu == "none")
    {
      Console.WriteLine();
    }
    else
    {
      Console.WriteLine($"---------- {inMenu} ----------");
    }
  }
}