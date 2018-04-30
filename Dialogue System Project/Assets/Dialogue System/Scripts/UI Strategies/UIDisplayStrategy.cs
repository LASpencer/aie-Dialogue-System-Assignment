using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    [RequireComponent(typeof(DialogueUI))]
    public class UIDisplayStrategy : MonoBehaviour
    {
        [SerializeField]
        protected DialogueUI uiManager;

        public virtual void DisplayDialogueEntry(DialogueEntry entry)
        {
            throw new System.NotImplementedException();
        }

        public virtual void DisplayResponse(Response response, int ID, bool possible)
        {
            throw new System.NotImplementedException();
        }

        public virtual void ClearResponses()
        {
            throw new System.NotImplementedException();
        }

        public virtual void OnConversationStart()
        {

        }

        public virtual void OnConversationEnd()
        {

        }
    }
}