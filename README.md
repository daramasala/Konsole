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
