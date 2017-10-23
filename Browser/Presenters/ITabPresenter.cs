namespace Browser.Presenters
{
    using System.Drawing;

    using Browser.Requests;

    /// <summary>
    /// The TabPresenter interface.
    /// </summary>
    public interface ITabPresenter
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Pushes a url, or alternatively a google search to the tab history which then triggers a page load.
        /// </summary>
        /// <param name="url">The url to push.</param>
        void Push(Url url);

        /// <summary>
        /// Instructs the presenter to reload the tab.
        /// </summary>
        void Reload();

        /// <summary>
        /// Returns the url that the page is currently visiting.
        /// </summary>
        /// <returns>
        /// The <see cref="TabPresenter.Url"/>.
        /// </returns>
        Url Url();
    }
}