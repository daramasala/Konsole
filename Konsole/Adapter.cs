using System;

namespace Konsole
{
    /// <summary>
    /// Implement the logic of a single menu. Override this class and hand it to a <see cref="Menu"/> instance.
    /// </summary>
    public class Adapter
    {
        private readonly Action<int> onSelect;
        public string[] Items { get; }
        public string Title { get; }

        public void OnSelect(int selectedIndex)
        {
            onSelect(selectedIndex);
        }

        /// <summary>
        /// Create a new adapter. The associated <see cref="Menu"/> will call the <see cref="onSelect"/> delegate and pass the index of the selected option.
        /// </summary>
        /// <param name="title">The title of the menu</param>
        /// <param name="items">The items displayed in the menu</param>
        /// <param name="onSelect">Delegate to call when an item is selected</param>
        public Adapter(string title, string[] items, Action<int> onSelect)
        {
            this.onSelect = onSelect;
            Title = title;
            Items = items;
        }

        /// <summary>
        /// Create a new adapter. The associated <see cref="Menu"/> will call the <see cref="onSelect"/> delegate element in the index of the selected option.
        /// </summary>
        /// <param name="title">The title of the menu</param>
        /// <param name="items">The items displayed in the menu</param>
        /// <param name="onSelect">Delegate to call when an item is selected</param>
        public Adapter(string title, string[] items, Action[] actions):
            this(title, items, i => actions[i]())
        {
        }
    }
}