namespace Browser.Presenters
{
    /// <summary>
    ///     The base interface for static data source singletons.
    /// </summary>
    public interface IDataSource
    {
        /// <summary>
        ///     Saves the data explicitly.
        /// </summary>
        void Save();
    }
}