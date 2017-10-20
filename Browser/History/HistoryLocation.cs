namespace Browser.History
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;

    using Browser.Annotations;
    using Browser.Requests;

    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The history entry.
    /// </summary>
    public class HistoryLocation : INotifyPropertyChanged
    {
        /// <summary>
        /// The url.
        /// </summary>
        private Url url;

        /// <summary>
        /// The title.
        /// </summary>
        private string title;

        /// <summary>
        /// The date.
        /// </summary>
        private DateTime date;

        /// <summary>
        /// Initializes a new instance of the <see cref="HistoryLocation"/> class.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        public HistoryLocation(Url url)
        {
            this.Url = url;
            this.Date = DateTime.Now;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HistoryLocation"/> class.
        /// </summary>
        public HistoryLocation()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HistoryLocation"/> class.
        /// </summary>
        /// <param name="l">
        /// The l.
        /// </param>
        public HistoryLocation(Location l)
        {
            this.Url = l.Url;
            this.Title = l.Title;
            this.Date = DateTime.Now;
        }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        [Key]
        public DateTime Date
        {
            get => this.date;
            set
            {
                this.date = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title
        {
            get => this.title;
            set
            {
                this.title = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        public Url Url
        {
            get => this.url;
            set
            {
                this.url = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// The property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The on property changed.
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}