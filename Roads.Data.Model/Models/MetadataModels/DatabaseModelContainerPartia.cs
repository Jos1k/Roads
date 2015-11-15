namespace Roads.DataBase.Model.Models
{
    /// <summary>
    /// Partial class of DatabaseModelContainer that allow using metadata and extend it.
    /// </summary>

    public partial class DatabaseModelContainer
    {
        public DatabaseModelContainer(string connectionString)
            : base(connectionString)
        {
        }
    }
}
