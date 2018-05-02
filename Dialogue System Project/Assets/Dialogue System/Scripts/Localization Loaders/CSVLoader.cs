using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu(menuName = "Dialogue/Localization/CSV Loader")]
    public class CSVLoader : LocalizationLoader
    {
        [SerializeField]
        TextAsset localizationData;

        public override Dictionary<string, string> LoadLanguage(string locale)
        {
            //TODO just use standard csv parser
            
            string[] fileLines = localizationData.text.Split(new string[] { "\n\r" },StringSplitOptions.RemoveEmptyEntries);
            string[] header = fileLines[0].Split(',');  //TODO Fix this: breaks because quotes aren't escaped
            int localeIndex = Array.IndexOf(header, locale);
            if(localeIndex >= 0)
            {
                Dictionary<string, string> loadedLines = new Dictionary<string, string>();
                for (int i = 1; i < fileLines.Length; ++i)
                {
                    string[] values = fileLines[i].Split(',');
                    try {
                        string key = values[0];
                        string translation = values[localeIndex];
                        loadedLines.Add(key, translation);
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Debug.LogWarning("Translation not found on line " + i.ToString());
                    }
                }
                return loadedLines;
            }
            else
            {
                // Locale not found
                return null;
            }
        }
    }
}