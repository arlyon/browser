namespace Browser.History
{
    using System;

    using Browser.Requests;

    /// <summary>
    ///     The type of historyLocation change.
    /// </summary>
    public enum ChangeType
    {
        /// <summary>
        /// The current historyLocation changed due to navigating forward.
        /// </summary>
        Forward,

        /// <summary>
        /// The current historyLocation changed due to navigating back.
        /// </summary>
        Backward,

        /// <summary>
        /// The current historyLocation changed due to navigating to a new page.
        /// </summary>
        Push
    }

    /// <summary>
    /// The TabHistory interface.
    /// </summary>
    public interface ITabHistory
    {
        /// <summary>
        /// The on current historyLocation change.
        /// </summary>
        event CurrentLocationChangeHandler OnCurrentLocationChange;

        /// <summary>
        /// Moves the history back a step.
        /// </summary>
        /// <returns>
        /// The <see cref="HistoryLocation"/> previous historyLocation.
        /// </returns>
        HistoryLocation Back();

        /// <summary>
        /// Checks whether the page can be added to favorites.
        /// </summary>
        /// <returns>Whether the page can be added to favorites.</returns>
        bool CanAddToFavorites();

        /// <summary>
        /// Checks if it is possible to go back in history.
        /// </summary>
        /// <returns>
        /// Whether it is possible to go back in history.
        /// </returns>
        bool CanGoBackward();

        /// <summary>
        ///     Checks if it is possible to go forwards in history.
        /// </summary>
        /// <returns></returns>
        bool CanGoForward();

        /// <summary>
        ///     Checks whether the page can be reloaded.
        /// </summary>
        /// <returns></returns>
        bool CanReload();

        /// <summary>
        ///     Returns the current historyLocation in history.
        /// </summary>
        /// <returns>The current historyLocation is history.</returns>
        HistoryLocation Current();

        /// <summary>
        ///     Goes forward a step in the history chain.
        /// </summary>
        /// <returns></returns>
        HistoryLocation Forward();

        /// <summary>
        ///     Severs any future elements from the tab
        ///     history and pushes a new element to it.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        HistoryLocation Push(Url url);

        /// <summary>
        ///     Updates the title of the current historyLocation with the proper title for the page.
        /// </summary>
        /// <param name="pageTitle"></param>
        void UpdateTitle(string pageTitle);
    }

    /// <summary>
    /// The current location change handler.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    public delegate void CurrentLocationChangeHandler(object sender, CurrentLocationChangeArgs e);

    /// <inheritdoc />
    /// <summary>
    ///     The event args passed when the historyLocation changes.
    /// </summary>
    public class CurrentLocationChangeArgs : EventArgs
    {
        public ChangeType ChangeType;

        public HistoryLocation HistoryLocation;
    }
}