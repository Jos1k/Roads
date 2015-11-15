using System.Data.Entity;
using System.Linq;
using Roads.Common.Integrations;
using Roads.DataBase.Model.Models;

namespace Roads.Tests.Mocks
{
    /// <summary>
    /// The DataBaseMock class of current data base instead using moq objects.
    /// </summary>
    public class DataBaseMock : DbContext
    {
        public DataBaseMock()
        {
            Database.SetInitializer<DataBaseMock>(new CreateDatabaseIfNotExists<DataBaseMock>());
            //Database.SetInitializer<SchoolDBContext>(new DropCreateDatabaseIfModelChanges<SchoolDBContext>());
            Database.SetInitializer<DataBaseMock>(new DropCreateDatabaseAlways<DataBaseMock>());
            //Database.SetInitializer<SchoolDBContext>(new SchoolDBInitializer());
        }

        public virtual DbSet<RegionNode> RegionNodes { get; set; }

        public virtual DbSet<CityNode> CityNodes { get; set; }

        public virtual DbSet<Language> Languages { get; set; }

        public virtual DbSet<MapObjectTranslation> MapObjectTranslations { get; set; }

        public virtual DbSet<RouteNode> RouteNodes { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Feedback> Feedbacks { get; set; }

        public virtual DbSet<FeedbackItem> FeedbackItems { get; set; }

        public virtual DbSet<FeedbackModel> FeedbackModels { get; set; }

        public virtual DbSet<FeedbackValue> FeedbackValues { get; set; }

        public virtual DbSet<DynamicTranslations> DynamicTranslations { get; set; }

        public virtual DbSet<StaticTranslation> StaticTranslations { get; set; }

        public virtual DbSet<Setting> Settings { get; set; }

        public virtual DbSet<Trek> Treks { get; set; } 
    }


    /// <summary>
    /// The DataContextMock class.
    /// </summary>
    public class DataContextMock : IDataContext
    {
        public DataBaseMock DataContext = new DataBaseMock();

        public void Dispose()
        {
            DataContext.Dispose();
        }

        public IQueryable<RouteNode> RouteNodes
        {
            get { return DataContext.RouteNodes; }
        }

        public IQueryable<CityNode> CityNodes
        {
            get { return DataContext.CityNodes; }
        }

        public IQueryable<RegionNode> RegionNodes
        {
            get { return DataContext.RegionNodes; }
        }

        public IQueryable<User> Users
        {
            get { return DataContext.Users; }
        }

        public IQueryable<Feedback> Feedbacks
        {
            get { return DataContext.Feedbacks; }
        }

        public IQueryable<FeedbackItem> FeedbackItems
        {
            get { return DataContext.FeedbackItems; }
        }

        public IQueryable<FeedbackModel> FeedbackModels
        {
            get { return DataContext.FeedbackModels; }
        }

        public IQueryable<Language> Languages
        {
            get { return DataContext.Languages; }
        }

        public IQueryable<DynamicTranslations> DynamicTranslations
        {
            get { return DataContext.DynamicTranslations; }
        }

        public IQueryable<StaticTranslation> StaticTranslations
        {
            get { return DataContext.StaticTranslations; }
        }

        public IQueryable<MapObjectTranslation> MapObjectTranslations
        {
            get { return DataContext.MapObjectTranslations; }
        }

        public IQueryable<FeedbackValue> FeedbackValues
        {
            get { return DataContext.FeedbackValues; }
        }

        public IQueryable<Trek> Treks
        {
            get { return DataContext.Treks; }
        }

        public IQueryable<Setting> Settings
        {
            get { return DataContext.Settings; }
        }

        #region Publick Methods

        public void AddDataObject<T>(T entity) where T : class
        {
            DataContext.Set<T>().Add(entity);
        }

        public void AddTranslationObject<T>(T entity) where T : class
        {
            DataContext.Set<T>().Add(entity);
        }

        public void DeleteDataObject<T>(T entity) where T : class
        {
            //throw new System.NotImplementedException();
            DataContext.Set<T>().Remove(entity);
        }

        public void DeleteTranslationObject<T>(T entity) where T : class
        {
            throw new System.NotImplementedException();
        }

        public void SaveChanges()
        {
            DataContext.SaveChanges();
        }

        #endregion

    }

}
