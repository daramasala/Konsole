using System;
using System.Linq;

namespace Konsole
{
    /// <summary>
    /// Display a multiple options menu.
    /// </summary>
    public class Menu
    {
        private readonly Adapter adapter;
        private int selectedIndex;
        private int firstItemTop;
        private readonly int width;
        private const ConsoleColor SelectedBgColor = ConsoleColor.DarkGreen;
        private const ConsoleColor SelectedFgColor = ConsoleColor.White;
        private readonly int top;
        private readonly int left;

        public Menu(Adapter adapter, int top) : this(adapter)
        {
            this.top = 0;
        }

        public Menu(Adapter adapter)
        {
            this.adapter = adapter;
            selectedIndex = 0;
            width = adapter.Items.Concat(new[] { adapter.Title }).Max(i => i.Length) + 5;
            top = (Console.WindowHeight - (adapter.Items.Length + 2)) / 2;
            left = (Console.WindowWidth - width) / 2;
        }

        private int NormalizePosition(int nextPosition)
        {
            var count = adapter.Items.Length;
            return (nextPosition % count + count) % count;
        }

        /// <summary>
        /// Display the menu and let the user select an option.
        /// 
        /// </summary>
        /// <returns><code>true</code> if the user selected an option. <code>false</code> if the user exited by pressing ESC</returns>
        public bool Run()
        {
            Print();
            while (true)
            {
                var keyInfo = Console.ReadKey(true);
                var prevSelectedIndex = selectedIndex;
                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        selectedIndex = NormalizePosition(selectedIndex + 1);
                        break;
                    case ConsoleKey.UpArrow:
                        selectedIndex = NormalizePosition(selectedIndex -1);
                        break;
                    case ConsoleKey.Escape:
                        return false;
                    case ConsoleKey.Enter:
                        adapter.OnSelect(selectedIndex);
                        return true;
                }
                PrintItem(prevSelectedIndex);
                PrintItem(selectedIndex);
            }
        }



        private void Print()
        {
            Console.SetCursorPosition(left, top);
            Console.WriteLine(adapter.Title);
            Console.SetCursorPosition(left, top +1);
            Console.WriteLine("".PadLeft(width, '-'));
            firstItemTop = Console.CursorTop;
            for (var i = 0; i < adapter.Items.Length; i++)
            {
                PrintItem(i);
            }
        }

        private void PrintItem(int itemIndex)
        {
            Console.SetCursorPosition(left, firstItemTop + itemIndex);
            if (itemIndex == selectedIndex)
            {
                Console.ForegroundColor = SelectedFgColor;
                Console.BackgroundColor = SelectedBgColor;
            }
            Console.WriteLine($"{itemIndex}: {adapter.Items[itemIndex]}".PadRight(width));
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}