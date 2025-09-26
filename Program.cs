
using App;

List<User> users = new List<User>();

users.Add(new User("testuser1", "t@1", "pass"));
users.Add(new User("testuser2", "t@2", "pass"));

User activeUser = null;

bool isRunning = true;

while (isRunning)
{
  if (activeUser == null)
  {
    Console.WriteLine("\n\nWelcome to The Trader's Peninsula.\n");
    Console.WriteLine("\nLog in\n");
    Console.Write("\nUsername: ");
    string username = Console.ReadLine();
    Console.Write("\nPassword: ");
    string password = Console.ReadLine();

    foreach (User user in users)
    {
      if (user.Login(username, password))
      {
        activeUser = user;
        break;
      }
    }
  }
  else
  {
    if (activeUser is User u)
    {
      Console.WriteLine("\n\n----- The Trader's Peninsula -----\n");
      Console.WriteLine($"\n\nWelcome, {u.Name} \n");
    }
    break;
  }




}