namespace Browser.Views
{
    using System;
    using System.Windows.Forms;

    using global::Browser.Favorites;
    using global::Browser.Requests;

    /// <summary>
    /// The edit favorites window.
    /// </summary>
    public partial class EditFavoritesWindow : Form, IEditFavorites
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditFavoritesWindow"/> class.
        /// </summary>
        public EditFavoritesWindow()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// The save_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Save_Click(object sender, EventArgs e)
        {
            this.SaveButtonClicked?.Invoke(sender, e);
        }

        /// <summary>
        /// The cancel_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Cancel_Click(object sender, EventArgs e)
        {
            this.CancelButtonClicked?.Invoke(sender, e);
        }

        /// <summary>
        /// The delete_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Delete_Click(object sender, EventArgs e)
        {
            this.DeleteButtonClicked?.Invoke(sender, e);
        }

        /// <summary>
        /// The save button clicked.
        /// </summary>
        public event EventHandler SaveButtonClicked;

        /// <summary>
        /// The cancel button clicked.
        /// </summary>
        public event EventHandler CancelButtonClicked;

        /// <inheritdoc />
        /// <summary>
        /// The delete button clicked.
        /// </summary>
        public event EventHandler DeleteButtonClicked;

        /// <inheritdoc />
        /// <summary>
        /// The display favorites location.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="url"></param>
        public void DisplayFavoritesLocation(string name, string url)
        {
            this.textBox1.Text = name;
            this.textBox2.Text = url;
        }

        /// <inheritdoc />
        /// <summary>
        /// The get update.
        /// </summary>
        /// <returns>
        /// The <see cref="T:Browser.Favorites.FavoritesLocation" />.
        /// </returns>
        public FavoritesLocation GetUpdate()
        {
            return new FavoritesLocation() { Name = this.textBox1.Text, Url = new Url(this.textBox2.Text) };
        }
    }
}
