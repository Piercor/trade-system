
namespace App;

class User
{
  public string Name;
  public string Email;
  string _password;

  public User(string n, string e, string p)
  {
    Name = n;
    Email = e;
    _password = p;
  }

  public bool Login(string email, string password)
  {
    return email == Email && password == _password;
  }
}