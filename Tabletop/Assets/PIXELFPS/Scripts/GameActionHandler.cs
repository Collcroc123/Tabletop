using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameActionHandler : MonoBehaviour
{ // Created by Mario
    [System.Serializable]
    public struct ActionEventPair
    {
        public string name;
        
        public GameAction action;

        public UnityEvent actionEvent;

        public void Initialize()
        {
            if (action == null) return;

            action.raiseNoArgs += OnAction;
        }

        public void Remove()
        {
            if (action == null) return;

            action.raiseNoArgs -= OnAction;
        }

        private void OnAction()
        {
            actionEvent.Invoke();
        }
    }

    public List<ActionEventPair> actionEventPairs = new List<ActionEventPair>(1);

    private void OnEnable()
    {
        foreach (var pair in actionEventPairs)
        {
            pair.Initialize();
        }
    }

    private void OnDisable()
    {
        foreach (var pair in actionEventPairs)
        {
            pair.Remove();
        }
    }
}