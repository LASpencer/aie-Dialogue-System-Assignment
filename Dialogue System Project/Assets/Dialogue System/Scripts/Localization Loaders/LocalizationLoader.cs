using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue {
    public class LocalizationLoader : ScriptableObject
    {
        public virtual Dictionary<string, string> LoadLanguage(string locale)
        {
            throw new System.NotImplementedException();
        }
    }
}
