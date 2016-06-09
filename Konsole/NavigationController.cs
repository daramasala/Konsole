using System;
using System.Collections.Generic;
using System.Linq;

namespace Konsole
{
    /// <summary>
    /// Controls which screen is currently displayed.
    /// </summary>
    public class NavigationController
    {
        class NextScreenCommand
        {
            
        }

        public static NavigationController Instance => instance ?? (instance = new NavigationController());

        private static NavigationController instance;
        private readonly Stack<Screen> screenStack = new Stack<Screen>(); 

        /// <summary>
        /// Start the program with <code>screen</code>
        /// </summary>
        /// <param name="screen">The startup screen</param>
        public void Start(Screen screen)
        {
            screenStack.Push(screen);
            Run();
        }

        private void Run()
        {
            while (screenStack.Any())
            {
                var screen = screenStack.Peek();
                Console.Clear();
                screen.Run();
            }
        }

        /// <summary>
        /// Close the current screen.
        /// </summary>
        public void Close()
        {
            screenStack.Pop();
        }

        /// <summary>
        /// Open a new screen.
        /// </summary>
        /// <param name="screen">The new screen to open.</param>
        public void PushScreen(Screen screen)
        {
            screenStack.Push(screen);
        }
    }
}