using System.Data.Entity;

namespace Roads.MocksObjects
{
    /// <summary>
    /// The DataBaseMock class of current data base instead using moq objects.
    /// </summary>
    public class DataBaseMock: DbContext 
    {
        public virtual DbSet<RegionNode> RegionNodes { get; set; }

        public virtual DbSet<CityNode> CityNodes { get; set; }

        public virtual DbSet<Language> Languages { get; set; }

        public virtual DbSet<MapObjectTranslation> MapObjectTranslations { get; set; }
    }

    /// <summary>
    /// The RegionNode mock of RegionNode class.
    /// </summary>
    public class RegionNode
    {
        public int RegionNodeId { get; set; }

        public string LanguageKey { get; set; }
    }

    /// <summary>
    /// The CityNode mock of CityNode class.
    /// </summary>
    public class CityNode
    {
        public int CityNodeId { get; set; }

        public long RegionNodeId { get; set; }

        public string LanguageKey { get; set; }        
    }

    /// <summary>
    /// The Language mock of Language class.
    /// </summary>
    public class Language
    {
        public int LanguageId { get; set; }

        public string Name { get; set; }

        public bool IsDefault { get; set; }
    }

    /// <summary>
    /// The MapObjectTranslation mock of MapObjectTranslation class.
    /// </summary>
    public class MapObjectTranslation
    {
        public long TranslationId { get; set; }

        public string LanguageKey { get; set; }

        public string Value { get; set; }

        public int LanguageId { get; set; }
    }

}
