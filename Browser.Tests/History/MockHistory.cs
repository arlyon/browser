using System;

namespace Browser.Tests.History
{
    using System.ComponentModel;

    using Browser.History;
    using Browser.Tests.Favicon;

    /// <inheritdoc />
    /// <summary>
    /// The mock history.
    /// </summary>
    internal class MockHistory : IHistory
    {
        /// <summary>
        /// The _view model.
        /// </summary>
        private readonly BindingList<HistoryViewModel> _viewModel;

        /// <inheritdoc />
        /// <summary>
        /// The history updated.
        /// </summary>
        public event HistoryPushEventHandler HistoryUpdated;

        /// <summary>
        /// Initializes a new instance of the <see cref="MockHistory"/> class.
        /// </summary>
        public MockHistory()
        {
            this._viewModel = new BindingList<HistoryViewModel>();
            this.HistoryUpdated += this.AddToViewModel;
        }

        /// <summary>
        /// The add to view model.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void AddToViewModel(object sender, HistoryPushEventArgs args)
        {
            this._viewModel.Add(HistoryViewModel.FromHistoryLocation(args.Change, new MockFaviconCache()));
        }

        /// <inheritdoc />
        /// <summary>
        /// The push.
        /// </summary>
        /// <param name="historyLocation">
        /// The history Location.
        /// </param>
        public void Push(HistoryLocation historyLocation)
        {
            this.HistoryUpdated?.Invoke(this, new HistoryPushEventArgs(historyLocation));
        }

        /// <inheritdoc />
        /// <summary>
        /// The get view model.
        /// </summary>
        /// <returns>
        /// The <see cref="BindingList{T}" />.
        /// </returns>
        public BindingList<HistoryViewModel> GetViewModel()
        {
            return this._viewModel;
        }
    }
}
