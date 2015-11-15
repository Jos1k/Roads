using System;
using System.Collections.Generic;
using System.Data.Entity;
using NUnit.Framework;
using Roads.Common.Integrations;
using Roads.DataBase.Model.Models;
using Roads.Tests.Mocks;

namespace Roads.Tests.Helpers
{
    public class DataBaseMockFiller : SingletonBase<DataBaseMockFiller>
    {
        public void Fill(DataBaseMock datacontext)
        {
            // Add data to Languages.
            datacontext.Languages.AddRange(new List<Language>
            {
                new Language {IsDefault = true, LanguageId = 1, Name = "UA"},
                new Language {IsDefault = false, LanguageId = 2, Name = "RU"},
                new Language {IsDefault = false, LanguageId = 3, Name = "EN"}
            });

            datacontext.SaveChanges();

            // Add data regions.
            datacontext.RegionNodes.AddRange(new List<RegionNode>
            {
                new RegionNode {LanguageKey = "r1", RegionNodeId = 1},
                new RegionNode() {LanguageKey = "r2", RegionNodeId = 2},
                new RegionNode() {LanguageKey = "r3", RegionNodeId = 2}
            });

            datacontext.SaveChanges();

            // Add data to translations.
            datacontext.CityNodes.AddRange(new List<CityNode>
            {
                new CityNode {CityNodeId = 1, RegionNodeId = 1, LanguageKey = "c1"},
                new CityNode {CityNodeId = 2, RegionNodeId = 1, LanguageKey = "c2"},
                new CityNode {CityNodeId = 3, RegionNodeId = 1, LanguageKey = "c3"},
                new CityNode {CityNodeId = 4, RegionNodeId = 1, LanguageKey = "c4"},
                new CityNode {CityNodeId = 5, RegionNodeId = 2, LanguageKey = "c5"},
                new CityNode {CityNodeId = 6, RegionNodeId = 2, LanguageKey = "c6"},
                new CityNode {CityNodeId = 7, RegionNodeId = 2, LanguageKey = "c7"},
                new CityNode {CityNodeId = 8, RegionNodeId = 3, LanguageKey = "c8"},
                new CityNode {CityNodeId = 9, RegionNodeId = 3, LanguageKey = "c9"},
                new CityNode {CityNodeId = 10, RegionNodeId = 3, LanguageKey = "c10"}

            });

            datacontext.SaveChanges();

            // Add data to MapObjectTranslations.
            datacontext.MapObjectTranslations.AddRange(new List<MapObjectTranslation>
            {
                new MapObjectTranslation {MapObjectId = 1, LanguageId = 1, LanguageKey = "r1", Value = "Київcкая область"},
                new MapObjectTranslation {MapObjectId = 2, LanguageId = 2, LanguageKey = "r1", Value = "Киевская область"},
                new MapObjectTranslation {MapObjectId = 3, LanguageId = 3, LanguageKey = "r1", Value = "Kiev region"},

                new MapObjectTranslation {MapObjectId = 4, LanguageId = 1, LanguageKey = "r2", Value = "Одеса"},
                new MapObjectTranslation {MapObjectId = 5, LanguageId = 2, LanguageKey = "r2", Value = "Одесса"},
                new MapObjectTranslation {MapObjectId = 6, LanguageId = 3, LanguageKey = "r2", Value = "Оdesa"},

                new MapObjectTranslation {MapObjectId = 7, LanguageId = 1, LanguageKey = "r3", Value = "Львів"},
                new MapObjectTranslation {MapObjectId = 8, LanguageId = 2, LanguageKey = "r3", Value = "Львов"},
                new MapObjectTranslation {MapObjectId = 9, LanguageId = 3, LanguageKey = "r3", Value = "Lviv"},

                new MapObjectTranslation {MapObjectId = 10, LanguageId = 1, LanguageKey = "c1", Value = "Бердичів"},
                new MapObjectTranslation {MapObjectId = 11, LanguageId = 2, LanguageKey = "c1", Value = "Бердичев"},
                new MapObjectTranslation {MapObjectId = 12, LanguageId = 3, LanguageKey = "c1", Value = "Berdichiv"},

                new MapObjectTranslation {MapObjectId = 13, LanguageId = 1, LanguageKey = "c2", Value = "Корсарівка"},
                new MapObjectTranslation {MapObjectId = 14, LanguageId = 2, LanguageKey = "c2", Value = "Корсаровка"},
                new MapObjectTranslation {MapObjectId = 15, LanguageId = 3, LanguageKey = "c2", Value = "Korsarivka"},

                new MapObjectTranslation {MapObjectId = 16, LanguageId = 1, LanguageKey = "c3", Value = "Знам'янівка"},
                new MapObjectTranslation {MapObjectId = 17, LanguageId = 2, LanguageKey = "c3", Value = "Знамянка"},
                new MapObjectTranslation {MapObjectId = 18, LanguageId = 3, LanguageKey = "c3", Value = "Znamyanivka"},

                new MapObjectTranslation {MapObjectId = 19, LanguageId = 1, LanguageKey = "c4", Value = "Хрущі"},
                new MapObjectTranslation {MapObjectId = 20, LanguageId = 2, LanguageKey = "c4", Value = "Хрущи"},
                new MapObjectTranslation {MapObjectId = 21, LanguageId = 3, LanguageKey = "c4", Value = "Hrushchi"},

                new MapObjectTranslation {MapObjectId = 22, LanguageId = 1, LanguageKey = "c5", Value = "Жмеренка"},
                new MapObjectTranslation {MapObjectId = 23, LanguageId = 2, LanguageKey = "c5", Value = "Жмеринка"},
                new MapObjectTranslation {MapObjectId = 24, LanguageId = 3, LanguageKey = "c5", Value = "Jmerinka"},

                new MapObjectTranslation {MapObjectId = 25, LanguageId = 1, LanguageKey = "c6", Value = "Бердичів"},
                new MapObjectTranslation {MapObjectId = 26, LanguageId = 2, LanguageKey = "c6", Value = "Бердичев"},
                new MapObjectTranslation {MapObjectId = 27, LanguageId = 3, LanguageKey = "c6", Value = "Berdichiv"},

                new MapObjectTranslation {MapObjectId = 28, LanguageId = 1, LanguageKey = "c7", Value = "Конотоп"},
                new MapObjectTranslation {MapObjectId = 29, LanguageId = 2, LanguageKey = "c7", Value = "Конотоп"},
                new MapObjectTranslation {MapObjectId = 30, LanguageId = 3, LanguageKey = "c7", Value = "Konotop"},

                new MapObjectTranslation {MapObjectId = 31, LanguageId = 1, LanguageKey = "c8", Value = "Косаново"},
                new MapObjectTranslation {MapObjectId = 32, LanguageId = 2, LanguageKey = "c8", Value = "Косаново"},
                new MapObjectTranslation {MapObjectId = 33, LanguageId = 3, LanguageKey = "c8", Value = "Cosanovo"},

                new MapObjectTranslation {MapObjectId = 34, LanguageId = 1, LanguageKey = "c9", Value = "Косарівці"},
                new MapObjectTranslation {MapObjectId = 35, LanguageId = 2, LanguageKey = "c9", Value = "Косаровцы"},
                new MapObjectTranslation {MapObjectId = 36, LanguageId = 3, LanguageKey = "c9", Value = "Cosarivci"},

                new MapObjectTranslation {MapObjectId = 37, LanguageId = 1, LanguageKey = "c10", Value = "Київ"},
                new MapObjectTranslation {MapObjectId = 38, LanguageId = 2, LanguageKey = "c10", Value = "Киев"},
                new MapObjectTranslation {MapObjectId = 39, LanguageId = 3, LanguageKey = "c10", Value = "Kiev"}
            });

            datacontext.SaveChanges();
        }

        public void AddDataForRouteManagerTest(DataBaseMock datacontext)
        {

            // Add data to Languages.
            datacontext.Languages.AddRange(new List<Language>
            {
                new Language {IsDefault = true, LanguageId = 1, Name = "UA"},
                new Language {IsDefault = false, LanguageId = 2, Name = "RU"},
                new Language {IsDefault = false, LanguageId = 3, Name = "EN"}
            });

            // Add data regions.
            datacontext.RegionNodes.AddRange(new List<RegionNode>
            {
                new RegionNode {LanguageKey = "r1", RegionNodeId = 1},
                new RegionNode {LanguageKey = "r2", RegionNodeId = 2},
                new RegionNode {LanguageKey = "r3", RegionNodeId = 3}
            });

            // Add data to translations.
            datacontext.CityNodes.AddRange(new List<CityNode>
            {
                new CityNode {CityNodeId = 1, RegionNodeId = 1, LanguageKey = "c1"},
                new CityNode {CityNodeId = 2, RegionNodeId = 1, LanguageKey = "c2"},
                new CityNode {CityNodeId = 3, RegionNodeId = 1, LanguageKey = "c3"},
                new CityNode {CityNodeId = 4, RegionNodeId = 1, LanguageKey = "c4"},
                new CityNode {CityNodeId = 5, RegionNodeId = 2, LanguageKey = "c5"},
                new CityNode {CityNodeId = 6, RegionNodeId = 2, LanguageKey = "c6"},
                new CityNode {CityNodeId = 7, RegionNodeId = 2, LanguageKey = "c7"},
                new CityNode {CityNodeId = 8, RegionNodeId = 3, LanguageKey = "c8"},
                new CityNode {CityNodeId = 9, RegionNodeId = 3, LanguageKey = "c9"},
                new CityNode {CityNodeId = 10, RegionNodeId = 3, LanguageKey = "c10"}

            });

            datacontext.RouteNodes.AddRange(new List<RouteNode>
            {
                new RouteNode {RouteNodeId = 1, OriginCityNodeId = 1, DestinationCityNodeId = 2},
                new RouteNode {RouteNodeId = 2, OriginCityNodeId = 1, DestinationCityNodeId = 4},
                new RouteNode {RouteNodeId = 3, OriginCityNodeId = 1, DestinationCityNodeId = 8},

                new RouteNode {RouteNodeId = 4, OriginCityNodeId = 2, DestinationCityNodeId = 3 },
                new RouteNode {RouteNodeId = 5, OriginCityNodeId = 2, DestinationCityNodeId = 6 },
                new RouteNode {RouteNodeId = 6, OriginCityNodeId = 2, DestinationCityNodeId = 9 },

                new RouteNode {RouteNodeId = 7, OriginCityNodeId = 1, DestinationCityNodeId = 3 },
                new RouteNode {RouteNodeId = 8, OriginCityNodeId = 3, DestinationCityNodeId = 5 },
                new RouteNode {RouteNodeId = 9, OriginCityNodeId = 3, DestinationCityNodeId = 7 },

                new RouteNode {RouteNodeId = 10, OriginCityNodeId = 4, DestinationCityNodeId = 5 },
                new RouteNode {RouteNodeId = 11, OriginCityNodeId = 4, DestinationCityNodeId = 9 },
                new RouteNode {RouteNodeId = 12, OriginCityNodeId = 4, DestinationCityNodeId = 10 },

                new RouteNode {RouteNodeId = 13, OriginCityNodeId = 5, DestinationCityNodeId = 7 },
                new RouteNode {RouteNodeId = 14, OriginCityNodeId = 2, DestinationCityNodeId = 5 },
                new RouteNode {RouteNodeId = 15, OriginCityNodeId = 5, DestinationCityNodeId = 8 },

                new RouteNode {RouteNodeId = 16, OriginCityNodeId = 6, DestinationCityNodeId = 8 },
                new RouteNode {RouteNodeId = 17, OriginCityNodeId = 6, DestinationCityNodeId = 9 },
                new RouteNode {RouteNodeId = 18, OriginCityNodeId = 6, DestinationCityNodeId = 10 },

                new RouteNode {RouteNodeId = 19, OriginCityNodeId = 1, DestinationCityNodeId = 7 },
                new RouteNode {RouteNodeId = 20, OriginCityNodeId = 7, DestinationCityNodeId = 10 },
                new RouteNode {RouteNodeId = 21, OriginCityNodeId = 2, DestinationCityNodeId = 7 },
            });
            
            datacontext.Settings.Add(new Setting { SettingName = "SearchDepth", SettingValue = "5" });

            datacontext.Users.AddRange(new List<User>
            {
                new User{ UserId = 1, Name = "Serhii", Password = "Serhii", UserType = ""},
                new User{ UserId = 2, Name = "Ihor", Password = "Ihor", UserType = ""},
                new User{ UserId = 3, Name = "Olexander", Password = "Olexander", UserType = ""}
            });

            datacontext.Feedbacks.AddRange(new List<Feedback>
            {
                new Feedback{ RouteNodeId = 1, SubmitTime = DateTime.Now, FeedbackId = 1, UserId = 1 },
                new Feedback{ RouteNodeId = 1, SubmitTime = DateTime.Now, FeedbackId = 2, UserId = 2 },
                new Feedback{ RouteNodeId = 1, SubmitTime = DateTime.Now, FeedbackId = 3, UserId = 3 },

                new Feedback{ RouteNodeId = 6, SubmitTime = DateTime.Now, FeedbackId = 4, UserId = 1 },
                new Feedback{ RouteNodeId = 6, SubmitTime = DateTime.Now, FeedbackId = 5, UserId = 2 },
                new Feedback{ RouteNodeId = 6, SubmitTime = DateTime.Now, FeedbackId = 6, UserId = 3 },

                new Feedback{ RouteNodeId = 11, SubmitTime = DateTime.Now, FeedbackId = 7, UserId = 1 },
                new Feedback{ RouteNodeId = 11, SubmitTime = DateTime.Now, FeedbackId = 8, UserId = 2 },
                new Feedback{ RouteNodeId = 11, SubmitTime = DateTime.Now, FeedbackId = 9, UserId = 3 },

            });

            datacontext.FeedbackModels.Add(new FeedbackModel {FeedbackModelId = 1});

            datacontext.FeedbackItems.AddRange(new List<FeedbackItem>
            {
                new FeedbackItem
                {
                    FeedbackItemId = 1,
                    IsNumeric = true,
                    DescriptionTranslationKey = "fi-1",
                    NameTranslationKey = "fi-n-1",
                    SortNumber = 1,
                    FeedbackModelId = 1,
                    Mandatory = true
                }
            });

            datacontext.MapObjectTranslations.AddRange(new List<MapObjectTranslation>
            {
                new MapObjectTranslation() {LanguageId = 1, LanguageKey = "c1", Value = "Вінниця"},
                new MapObjectTranslation() {LanguageId = 2, LanguageKey = "c1", Value = "Винница"},
                new MapObjectTranslation() {LanguageId = 3, LanguageKey = "c1", Value = "Vinnitsya"},
                new MapObjectTranslation() {LanguageId = 1, LanguageKey = "c5", Value = "Львів"},
                new MapObjectTranslation() {LanguageId = 2, LanguageKey = "c5", Value = "Львов"},
                new MapObjectTranslation() {LanguageId = 3, LanguageKey = "c5", Value = "Lviv"},
                new MapObjectTranslation() {LanguageId = 1, LanguageKey = "r1", Value = "Вінницька область"},
                new MapObjectTranslation() {LanguageId = 2, LanguageKey = "r1", Value = "Винницкая область"},
                new MapObjectTranslation() {LanguageId = 3, LanguageKey = "r1", Value = "Vinnitsya state"},
                new MapObjectTranslation() {LanguageId = 1, LanguageKey = "r2", Value = "Львівська область"},
                new MapObjectTranslation() {LanguageId = 2, LanguageKey = "r2", Value = "Львовская область"},
                new MapObjectTranslation() {LanguageId = 3, LanguageKey = "r2", Value = "Lviv state"}
            });

            datacontext.SaveChanges();

            datacontext.FeedbackValues.AddRange(new List<FeedbackValue>
            {
                new FeedbackValue{ FeedbackId = 1, Value = "3", FeedbackItemId = 1, FeedbackValueId = 1},
                new FeedbackValue{ FeedbackId = 2, Value = "3", FeedbackItemId = 1, FeedbackValueId = 2},
                new FeedbackValue{ FeedbackId = 3, Value = "4", FeedbackItemId = 1, FeedbackValueId = 3},
                new FeedbackValue{ FeedbackId = 4, Value = "1", FeedbackItemId = 1, FeedbackValueId = 4},
                new FeedbackValue{ FeedbackId = 5, Value = "1", FeedbackItemId = 1, FeedbackValueId = 5},
                new FeedbackValue{ FeedbackId = 6, Value = "1", FeedbackItemId = 1, FeedbackValueId = 6},
                new FeedbackValue{ FeedbackId = 7, Value = "1", FeedbackItemId = 1, FeedbackValueId = 7},
                new FeedbackValue{ FeedbackId = 8, Value = "1", FeedbackItemId = 1, FeedbackValueId = 8},
                new FeedbackValue{ FeedbackId = 9, Value = "2", FeedbackItemId = 1, FeedbackValueId = 9},
            });

            datacontext.SaveChanges();
        }

        public void AddDataForRouteRepositoryTest(DataBaseMock datacontext)
        {
            datacontext.Settings.Add(new Setting { SettingName = "SearchDepth", SettingValue = "10" });

            datacontext.Treks.Add(new Trek() { Track = "1075-147-1439", Hash = "39C5B77C", OriginCityNodeId = 1075, DesinationCityNodeId = 1439 ,TrekDate = DateTime.Now});
            datacontext.Treks.Add(new Trek() { Track = "1075-147-2249-1986-1439", Hash = "1A9A4B89", OriginCityNodeId = 1075, DesinationCityNodeId = 1439, TrekDate = DateTime.Now });
            datacontext.Treks.Add(new Trek() { Track = "1439-147-2249", Hash = "F06342A6", OriginCityNodeId = 1439, DesinationCityNodeId = 2249, TrekDate = DateTime.Now });

            datacontext.SaveChanges();
        }

		public void AddDataForSettingsTest(DataBaseMock datacontext)
		{
			datacontext.Settings.AddRange(new List<Setting>
			{
				new Setting {SettingId = 1, SettingName = "SearchDepth", SettingValue = "10"},
				new Setting {SettingId = 2, SettingName = "NumberOfRecordPerPage", SettingValue = "10"}
			});

			datacontext.SaveChanges();
		}

        public void AddDataForFeedbackSettingsTest(DataBaseMock datacontext)
        {
            datacontext.FeedbackModels.AddRange(new List<FeedbackModel>
            {
                new FeedbackModel
                {
                    FeedbackModelId = 1,
                    FeedbackModelName = "Model1",
                    HtmlCode = "HtmlCode1",
                    JavascriptCode = "JavascriptCode1"
                },
                new FeedbackModel
                {
                    FeedbackModelId = 2,
                    FeedbackModelName = "Model2",
                    HtmlCode = "HtmlCode2",
                    JavascriptCode = "JavascriptCode2"
                }
            });
            datacontext.SaveChanges();

            datacontext.FeedbackItems.AddRange(new List<FeedbackItem>
			{
				new FeedbackItem
				{
				    FeedbackItemId = 1, 
                    Mandatory = true, 
                    SortNumber = 1, 
                    IsNumeric = true, 
                    FeedbackModelId = 1, 
                    NameTranslationKey = "NameTranslKey1", 
                    DescriptionTranslationKey = "DescrTranslation Key1"
				},
				new FeedbackItem
				{
				    FeedbackItemId = 2,
                    Mandatory = false, 
                    SortNumber = 2, 
                    IsNumeric = false, 
                    FeedbackModelId = 2, 
                    NameTranslationKey = "NameTranslKey2", 
                    DescriptionTranslationKey = "DescrTranslation Key2"
				},
     		    new FeedbackItem
     		    {
     		        FeedbackItemId = 3, 
                    Mandatory = false, 
                    SortNumber = 3, 
                    IsNumeric = true, 
                    FeedbackModelId = 1,
                    NameTranslationKey = "NameTranslKey3", 
                    DescriptionTranslationKey = "DescrTranslation Key3"
     		    },
                new FeedbackItem
                {
                    FeedbackItemId = 4, 
                    Mandatory = false, 
                    SortNumber = 4, 
                    IsNumeric = false, 
                    FeedbackModelId = 2, 
                    NameTranslationKey = "NameTranslKey4", 
                    DescriptionTranslationKey = "DescrTranslation Key4"
                }
			});
            datacontext.SaveChanges();
        }

        public void AddDataForFindRouteDetailsTest(DataBaseMock datacontext)
        {
            // Add data to Languages.
            datacontext.Languages.AddRange(new List<Language>
            {
                new Language {IsDefault = true, LanguageId = 1, Name = "UA"},
                new Language {IsDefault = false, LanguageId = 2, Name = "RU"},
                new Language {IsDefault = false, LanguageId = 3, Name = "EN"}
            });

            datacontext.SaveChanges();

            // Add data regions.
            datacontext.RegionNodes.AddRange(new List<RegionNode>
            {
                new RegionNode() {LanguageKey = "r4", RegionNodeId = 4}
            });

            datacontext.SaveChanges();

            // Add data to translations.
            datacontext.CityNodes.AddRange(new List<CityNode>
            {
                new CityNode {CityNodeId = 11, RegionNodeId = 4, LanguageKey = "c11"}
            });

            datacontext.SaveChanges();

            // Add data to MapObjectTranslations.
            datacontext.MapObjectTranslations.AddRange(new List<MapObjectTranslation>
            {
                new MapObjectTranslation {MapObjectId = 43, LanguageId = 1, LanguageKey = "r4", Value = "Вінниця"},
                new MapObjectTranslation {MapObjectId = 44, LanguageId = 2, LanguageKey = "r4", Value = "Винница"},
                new MapObjectTranslation {MapObjectId = 45, LanguageId = 3, LanguageKey = "r4", Value = "Vinnitsa"},

                new MapObjectTranslation {MapObjectId = 40, LanguageId = 1, LanguageKey = "c11", Value = "Вінниця"},
                new MapObjectTranslation {MapObjectId = 41, LanguageId = 2, LanguageKey = "c11", Value = "Винница"},
                new MapObjectTranslation {MapObjectId = 42, LanguageId = 3, LanguageKey = "c11", Value = "Vinnitsa"}
            });

            datacontext.Treks.AddRange(new List<Trek>
            {
                new Trek { Track = "10-11",    Hash = "e7a16a5daa2abc55691a1ede30815603", OriginCityNodeId = 10, DesinationCityNodeId = 11 ,TrekDate = DateTime.Now},
                new Trek { Track = "10-5-11", Hash = "ea74f2805d7830b6bb58d9918c4dff20", OriginCityNodeId = 10, DesinationCityNodeId = 11 ,TrekDate = DateTime.Now},
                new Trek { Track = "11-10",   Hash = "f180b901a62379b199f0b40f65558964", OriginCityNodeId = 11, DesinationCityNodeId = 10 ,TrekDate = DateTime.Now},
                new Trek { Track = "11-5-10", Hash = "a67bfc184b2b9d3c8b49375b9899bff4", OriginCityNodeId = 11, DesinationCityNodeId = 10 ,TrekDate = DateTime.Now}
            });


            datacontext.RouteNodes.AddRange(new List<RouteNode>
            {
                new RouteNode {RouteNodeId = 1, OriginCityNodeId = 11, DestinationCityNodeId = 5},
                new RouteNode {RouteNodeId = 2, OriginCityNodeId = 5, DestinationCityNodeId = 10},
                new RouteNode {RouteNodeId = 3, OriginCityNodeId = 11, DestinationCityNodeId = 10}

            });

            datacontext.Users.AddRange(new List<User>
            {
                new User{ UserId = 1, Name = "testUser1", Password = "5f4dcc3b5aa765d61d8327deb882cf99", UserType = "testUser"},
                new User{ UserId = 2, Name = "testUser2", Password = "5f4dcc3b5aa765d61d8327deb882cf99", UserType = "testUser"},
                new User{ UserId = 3, Name = "testUser3", Password = "5f4dcc3b5aa765d61d8327deb882cf99", UserType = "testUser"}
            });

            datacontext.Feedbacks.AddRange(new List<Feedback>
            {
                new Feedback { FeedbackId = 1, SubmitTime = DateTime.Now , RouteNodeId = 1, UserId = 1},
                new Feedback { FeedbackId = 2, SubmitTime = DateTime.Now , RouteNodeId = 2, UserId = 2},
                new Feedback { FeedbackId = 3, SubmitTime = DateTime.Now , RouteNodeId = 2, UserId = 3},
                new Feedback { FeedbackId = 4, SubmitTime = DateTime.Now , RouteNodeId = 3, UserId = 1},
                new Feedback { FeedbackId = 5, SubmitTime = DateTime.Now , RouteNodeId = 3, UserId = 2},
                new Feedback { FeedbackId = 6, SubmitTime = DateTime.Now , RouteNodeId = 3, UserId = 3},
                new Feedback { FeedbackId = 7, SubmitTime = DateTime.Now , RouteNodeId = 3, UserId = 1},
                new Feedback { FeedbackId = 8, SubmitTime = DateTime.Now , RouteNodeId = 3, UserId = 2},
                new Feedback { FeedbackId = 9, SubmitTime = DateTime.Now , RouteNodeId = 2, UserId = 3},
                new Feedback { FeedbackId = 10, SubmitTime = DateTime.Now , RouteNodeId = 2, UserId = 1},
                new Feedback { FeedbackId = 11, SubmitTime = DateTime.Now , RouteNodeId = 2, UserId = 2},
                new Feedback { FeedbackId = 12, SubmitTime = DateTime.Now , RouteNodeId = 1, UserId = 3}
            });
            

            datacontext.FeedbackValues.AddRange(new List<FeedbackValue>
            {
                new FeedbackValue { FeedbackValueId = 1, Value = "3",               FeedbackId = 1, FeedbackItemId = 1},
                new FeedbackValue { FeedbackValueId = 2, Value = "4",                FeedbackId = 1, FeedbackItemId = 2},
                new FeedbackValue { FeedbackValueId = 3, Value = "Feedbacks value 1", FeedbackId = 1, FeedbackItemId = 3},

                new FeedbackValue { FeedbackValueId = 4, Value = "2",               FeedbackId = 2, FeedbackItemId = 1},
                new FeedbackValue { FeedbackValueId = 5, Value = "5",                FeedbackId = 2, FeedbackItemId = 2},
                new FeedbackValue { FeedbackValueId = 6, Value = "Feedbacks value 2", FeedbackId = 2, FeedbackItemId = 3},

                new FeedbackValue { FeedbackValueId = 7, Value = "1",               FeedbackId = 3, FeedbackItemId = 1},
                new FeedbackValue { FeedbackValueId = 8, Value = "5",                FeedbackId = 3, FeedbackItemId = 2},
                new FeedbackValue { FeedbackValueId = 9, Value = "Feedbacks value 3", FeedbackId = 3, FeedbackItemId = 3},

                new FeedbackValue { FeedbackValueId = 10, Value = "5",               FeedbackId = 4, FeedbackItemId = 1},
                new FeedbackValue { FeedbackValueId = 11, Value = "1",                FeedbackId = 4, FeedbackItemId = 2},
                new FeedbackValue { FeedbackValueId = 12, Value = "Feedbacks value 4", FeedbackId = 4, FeedbackItemId = 3},

                new FeedbackValue { FeedbackValueId = 13, Value = "3",               FeedbackId = 5, FeedbackItemId = 1},
                new FeedbackValue { FeedbackValueId = 14, Value = "4",                FeedbackId = 5, FeedbackItemId = 2},
                new FeedbackValue { FeedbackValueId = 15, Value = "Feedbacks value 5", FeedbackId = 5, FeedbackItemId = 3},

                new FeedbackValue { FeedbackValueId = 16, Value = "2",               FeedbackId = 6, FeedbackItemId = 1},
                new FeedbackValue { FeedbackValueId = 17, Value = "3",                FeedbackId = 6, FeedbackItemId = 2},
                new FeedbackValue { FeedbackValueId = 18, Value = "Feedbacks value 6", FeedbackId = 6, FeedbackItemId = 3},

                new FeedbackValue { FeedbackValueId = 19, Value = "3",               FeedbackId = 7, FeedbackItemId = 1},
                new FeedbackValue { FeedbackValueId = 20, Value = "4",                FeedbackId = 7, FeedbackItemId = 2},
                new FeedbackValue { FeedbackValueId = 21, Value = "Feedbacks value 7", FeedbackId = 7, FeedbackItemId = 3},

                new FeedbackValue { FeedbackValueId = 22, Value = "2",               FeedbackId = 8, FeedbackItemId = 1},
                new FeedbackValue { FeedbackValueId = 23, Value = "4",                FeedbackId = 8, FeedbackItemId = 2},
                new FeedbackValue { FeedbackValueId = 24, Value = "Feedbacks value 8", FeedbackId = 8, FeedbackItemId = 3},

                new FeedbackValue { FeedbackValueId = 25, Value = "4",               FeedbackId = 9, FeedbackItemId = 1},
                new FeedbackValue { FeedbackValueId = 26, Value = "4",                FeedbackId = 9, FeedbackItemId = 2},
                new FeedbackValue { FeedbackValueId = 27, Value = "Feedbacks value 9", FeedbackId = 9, FeedbackItemId = 3},

                new FeedbackValue { FeedbackValueId = 28, Value = "5",               FeedbackId = 9, FeedbackItemId = 1},
                new FeedbackValue { FeedbackValueId = 29, Value = "5",                FeedbackId = 9, FeedbackItemId = 2},
                new FeedbackValue { FeedbackValueId = 30, Value = "Feedbacks value 10", FeedbackId = 9, FeedbackItemId = 3},

                new FeedbackValue { FeedbackValueId = 31, Value = "3",               FeedbackId = 10, FeedbackItemId = 1},
                new FeedbackValue { FeedbackValueId = 32, Value = "1",                FeedbackId = 10, FeedbackItemId = 2},
                new FeedbackValue { FeedbackValueId = 33, Value = "Feedbacks value 11", FeedbackId = 10, FeedbackItemId = 3},

                new FeedbackValue { FeedbackValueId = 34, Value = "2",               FeedbackId = 12, FeedbackItemId = 1},
                new FeedbackValue { FeedbackValueId = 35, Value = "4",                FeedbackId = 12, FeedbackItemId = 2},
                new FeedbackValue { FeedbackValueId = 36, Value = "Feedbacks value 12", FeedbackId = 12, FeedbackItemId = 3}


            });

            datacontext.SaveChanges();
        }
    }
}
