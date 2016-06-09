using System;

namespace Konsole
{
    /// <summary>
    /// Implement this interface to display a progress indicator while running <see cref="BackgroundJob{TResult}"/>
    /// </summary>
    public interface IProgressIndicator
    {
        /// <summary>
        /// Called when the indicator is first displayed
        /// </summary>
        void Start();
        /// <summary>
        /// Define the interval for indicator updates (use for animation)
        /// </summary>
        TimeSpan UpdateInterval { get; }
        /// <summary>
        /// Update the progress indicator. Called every <see cref="UpdateInterval"/>.
        /// </summary>
        void Update();

        void Finish();
    }
}