namespace Konsole
{
    /// <summary>
    /// Base class for screens. Implement the screen's setup and logic in the Run method.
    /// </summary>
    public abstract class Screen
    {
        protected NavigationController NavigationController => NavigationController.Instance;
        public abstract void Run();
    }
}