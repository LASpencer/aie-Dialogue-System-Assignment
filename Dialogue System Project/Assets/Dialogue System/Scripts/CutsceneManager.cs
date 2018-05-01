﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue {
    public class CutsceneManager : MonoBehaviour {

        [SerializeField]
        StringAnimatorDict Animators;
        Dictionary<string, Animator> AnimatorDictionary;

        private void Awake()
        {
            AnimatorDictionary = Animators.ToDictionary();
        }

        public void DoCutsceneEvents(List<CutsceneEvent> events)
        {
            foreach(CutsceneEvent e in events)
            {
                Animator target = AnimatorDictionary[e.target];
                if(target != null)
                {
                    target.Play(e.animation);
                } else
                {
                    Debug.LogError("Animator \"" + e.target + "\" not found in Cutscene Manager");
                }
            }
        }
    }
}
