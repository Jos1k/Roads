// This is examples project to test available functionality

using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using Roads.Common.CacheProvider;
using Roads.Common.Integrations;
using Roads.Common.Managers;
using Roads.Common.Models.DataContract;
using Roads.Common.Models.Enums;

namespace Roads.Examples
{
    internal class Program
    {
        private const int CitiesAmount = 20;

        private static void Main()
        {
            GetAllLanguagesDataTest();

            GetAllDynamicTranslationsDataTest();

            AddUpdateDeleteDynamicDataTest();

            GetMapObjectTranslationsTest();

            Console.WriteLine("\nPress any key to exit");
            Console.ReadKey();
        }

        private static void GetAllLanguagesDataTest()
        {
            Console.WriteLine("GetLanguageById All LanguagesData Test()");
            ILanguagesManager languagesManager = new LanguagesManager();
            List<LanguageData> languages = languagesManager.GetAllLanguagesData();

            foreach (LanguageData language in languages)
            {
                Console.WriteLine("language code:\t{0} \nlanguage id:\t{1} \nIsDefault:\t{2}\n", language.Name,
                    language.LanguageId, language.IsDefault);
            }
            Console.WriteLine("-----------------------------");
        }

        private static void GetAllDynamicTranslationsDataTest()
        {
            Console.WriteLine("GetLanguageById All DynamicTranslationsData Test");
            IResourceManager resourceManager = new ResourceManager();
            List<DynamicTranslationsData> translationDataList = resourceManager.GetAllDynamicTranslationsData();

            foreach (DynamicTranslationsData translationsData in translationDataList)
            {
                Console.WriteLine("dyn obj id:\t{3} \ndyn key:\t{0} \nlang id:\t{1} \ndyn value:\t{2}\n",
                    translationsData.DynamicKey, translationsData.LanguageId, translationsData.DescriptionValue,
                    translationsData.DynamicObjectId);
            }
            Console.WriteLine("-----------------------------");
        }

        private static void GetMapObjectTranslationsTest()
        {
            Console.WriteLine("GetLanguageById MapObjectTranslations Test()");
            ICacheProvider cacheProvider = new CacheProvider();
            IResourceManager resourceManager = new ResourceManager(cacheProvider);

            List<string> languages = resourceManager.GetLanguages();

            //Writes first [CitiesAmount] cities in all available languages
            for (int i = 1; i < CitiesAmount; i++)
            {
                for (int j = 0; j < languages.Count; j++)
                {
                    string value = resourceManager.GetResource(TranslationTypeEnum.MapObjectTranslation, "r-" + i,
                        languages[j]);
                    Console.Write("{0} \t\t", value);
                }
                Console.WriteLine();
            }
            Console.WriteLine("-----------------------------");
        }

        private static void AddUpdateDeleteDynamicDataTest()
        {
            Console.WriteLine("Add_Update_Delete_DynamicData Test");
            //arrange
            ICacheProvider cacheProvider =
                new CacheProvider(new CacheItemPolicy
                {
                    AbsoluteExpiration = DateTimeOffset.MinValue,
                    SlidingExpiration = TimeSpan.Zero
                });
            IResourceManager resourceManager = new ResourceManager(cacheProvider);

            string expectedvalue = "5";
            string expectedNewValue = "6";

            var dynamicData = new DynamicTranslationsData
            {
                DescriptionValue = "5",
                DynamicKey = "5",
                Value = "5",
                LanguageId = 1
            };

            //Add dynamic data
            resourceManager.AddResource(dynamicData);
            Console.WriteLine("The dynamic data were added successfully");

            string actualValue = resourceManager.GetResource(TranslationTypeEnum.DynamicTranslation,
                dynamicData.DynamicKey, "uk");

            Console.WriteLine("ADDING... Actual value:{0} \t Expected value:{1}", actualValue, expectedvalue);
            Console.WriteLine("Press anykey to continue...");
            Console.ReadKey();

            //Update dynamic data
            dynamicData.Value = "6";
            resourceManager.UpdateResource(dynamicData);
            Console.WriteLine("The dynamic data were updateded successfully");

            string actualNewValue = resourceManager.GetResource(TranslationTypeEnum.DynamicTranslation,
                dynamicData.DynamicKey, "uk");

            Console.WriteLine("UPDATING... Actual value:{0} \t Expected value:{1}", actualNewValue, expectedNewValue);
            Console.WriteLine("Press anykey to continue...");
            Console.ReadKey();

            //Delete dynamic data
            resourceManager.DeleteDynamicResource(new List<string>(){"5"});
            Console.WriteLine("The dynamic data were deleted successfully");
            Console.WriteLine("Press anykey to finish current test.");
            Console.WriteLine("-----------------------------");
        }
    }
}