
namespace App;

/// <summary>
/// User class with a constructor to create new users and a method to log in.
/// </summary>
class User
{
  public string Name;
  public string Email;
  string _password;

  /// <summary>
  /// Constructor to create users.
  /// </summary>
  /// <param name="n">String with the name of the user.</param>
  /// <param name="e">String with the email of the user.</param>
  /// <param name="p">String with the password of the user.</param>
  public User(string n, string e, string p)
  {
    Name = n;
    Email = e;
    _password = p;
  }

  /// <summary>
  /// Method for log in. Checks if the email and pass inputs matches the Email and _password 
  /// of an user and returns a boolean, which will grant access to the system if true.
  /// </summary>
  /// <param name="email">String with the email an user trying to log in gave through an input.</param>
  /// <param name="pass">String with the password an user trying to log in gave through an input.</param>
  /// <returns></returns>
  public bool Login(string email, string pass)
  {
    return email == Email && pass == _password;
  }
}