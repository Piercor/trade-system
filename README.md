The Trader's Peninsula

Console based program that handles trades between users.

When starting the program, the user is presented with logging in, creating a new account or quitting the program.

• Log in: the user enters their email and password to access the program. If the email or password isn’t correct, a warning message is shown. After log in the users access the main user menu.

• Account creation: the user can write their name, an email with confirmation check, and a password, with same check as     email.
Other checks in place when creating an account are that the email is not being already used and that the password is not the same as the name or email of the user.

Main user menu

Here is the user presented with four menu options: My items, See the market, Trade history, and Log out.

• My items: here is the user presented with a submenu containing: "See my items", "Received trade requests", and "Back to previous menu (BtPM from now on)".
See my items: here can the user see the items they possess and add new ones. When adding a new item, the user is asked to give it a name and a description.
Received trade requests: Here the user can see other users trade requests and manage (accept/deny) them. If a trade request is accepted, the items in them would change owner (sender of the request gets the receiver's item and they get the sender’s, if any).

• See the market: here is the user presented with 3 menu options, "See other people's items", "My trade requests" and "BtPM".
See other people's items: here the user can see all the items other users have and make trade requests for them. When making a trade request, the user can choose one of their items in exchange.
My trade requests: here can the user see the pending trade requests they sent and the items involved.

• Trade history: here can the user see all the trades they sent or received, and what was the outcome (accepted/denied).


Code discussion:

I used some classes/enums that weren't explicitly required for the project, such as Functionality class and Menu enums. 
In the case of Menu enums, they were added to make the menu switching easier and clearer. 
In the case of Functionality class, I wanted to have a class that would be used to create methods that maybe couldn't be part of another class, even though some of the methods in there could probably been in another class.

Some of the methods I created may seem not entirely necessary, but as methods is a concept I'm not 100% strong with, I wanted to practice. This also explains why some parts of the code could probably have been solved with methods, but in those cases I wasn't 100% sure how to.

I didn't use any inheritance or interfaces since the classes share little to none variables or methods with each other, so I deemed it unnecessary to use them.

Thanks for reading and enjoy the program!

Best regards,
Pierino, founder and developer, The Trader's Peninsula