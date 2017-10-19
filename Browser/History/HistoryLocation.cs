namespace Browser.History
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Browser.Requests;

    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The history entry.
    /// </summary>
    public class HistoryLocation
    {
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
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        public Url Url { get; set; }
    }
}