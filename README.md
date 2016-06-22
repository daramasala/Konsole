# Konsole
A console UI library in C#.

![Screenshot](Screenshot%202016-06-09%2009.04.49.png)

Here is how you can create a console application that uses this library:

1. Create a new Console Application solution
2. Copy the `Konsole` folder (the folder that contains the `Konsole.csproj` file) to the root of the new solution
3. Open the new solution in Visual Studio
4. Right click the solution node (the root node) in the Solution Explorer
5. Choose 'Add Project'
6. Select the `Konsole.csproj` file

This added the project to your solution. In order to use it in the console application, you need to add a reference to it:

1. In the solution explorer, expand your console application project
2. Right click the 'References' node
3. Click 'Add Reference...'
4. In the navigation bar on the left, click 'Projects' and select the `Konsole` project

Now you can start using the classes in the library in your project. Don't forget to add `using` for the relevant namespace.

## Usage Examples
You can create a new screen by extending the `Screen` class:

    public class MainScreen : Screen
    {
        public override void Run()
        {
            Console.WriteLine("This is the main screen");
            Console.ReadLine();
        }
    }
To start the UI call `NavigationController.Start()` in the `Main` method:

    static void Main(string[] args)
    {
        NavigationController.Instance.Start(new MainScreen());
    }
To open a new screen use `Screen`'s `NavigationController` property to access the navigation controller:

    NavigationController.PushScreen(new SecondScreen());
To close the current screen use `Screen.NavigationController`
## Menus
To create a menu, create an `Adapter` with data and callbacks and pass it to a new `Menu` instance.

The following code creates a new menu with 3 options - 'Blue Screen', 'Red Screen' and 'Exit'. If the user chooses 'Exit' or presses `Esc`, the screen will close itself:

    // Create the adapter
    var adapter = new Adapter("Welcome", 
            new [] {"Blue Screen", "Red Screen", "Exit"},
            new Action[]
            {
                ()=>NavigationController.PushScreen(new BlueScreen()),
                ()=>NavigationController.PushScreen(new RedScreen()),
                ()=>NavigationController.Close()
            });
        var menu = new Menu(adapter);
        if (!menu.Run()) // menu.Run() returns false if the user pressed Escape instead of choosing one of the options
        {
            NavigationController.Close();
        }
## Background Jobs
If you have a long running job (e.g. database access, network access), you can run it in a background job and present a progress indicator to the user. 

First you need to implement a progress indicator. See [this gist](https://gist.github.com/daramasala/2b30b7ebd21e4c2ece78375a823410f4) for an example of implementing an `IProgressIndicator`.

The following code will run a job in the background that fetches data from mongodb:

    public class ListAllMessagesScreen : Screen
    {
        public override void Run()
        {
            Console.Clear();
            
            // Create a new job that returns List<Message>
            var job = new BackgroundJob<List<Message>>(
                ()=> db.GetCollection<Message>("messages").ToList(), // Fetch a list of messages
                new MyProgressIndicator()
            );
            var messages = job.Run(); // Run the job
            
            // display the messages
            // .. (code to display the messages)
            
            Console.ReadLine();
            
            // close the current screen
            NavigationController.Close();
        }
        
Note that when you create a new instance of `BackgroundJob` you define the type of the `BackgroundJob.Run()` return value.

The 2 c'tor parameters are the action to perform in the background and an instance of `IProgressIndicator` to update the display.
