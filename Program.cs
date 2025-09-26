
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
  switch (currentMenu)
  {

    case Menu.None:
      Console.WriteLine("\n\nWelcome to The Trader's Peninsula.\n");
      Console.WriteLine("\n[1] Log in.");
      Console.WriteLine("\n[2] Create an account.");
      Console.WriteLine("\n[3] Quit");

      Console.Write("\nSelect an option [1-3]");


      break;

    case Menu.Login:

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
          break;
        }
      }
      Console.WriteLine($"\nNo users were found with the given email/password");
      Console.Write("\nPress ENTER to continue. ");
      Console.ReadLine();
      currentMenu = Menu.None;
      break;

    case Menu.Main:
      if (activeUser is User u)
      {
        Console.WriteLine("\n\n----- The Trader's Peninsula -----\n");
        Console.WriteLine($"\n\nWelcome, {u.Name} \n");
      }
      break;
  }
}
