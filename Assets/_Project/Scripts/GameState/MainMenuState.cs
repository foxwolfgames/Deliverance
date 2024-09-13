using Deliverance.UI;
using FWGameLib.Common.StateMachine;
using UnityEngine;

namespace Deliverance.GameState
{
    public class MainMenuState : MonoBehaviour, IState
    {
        [SerializeField] private bool isActive;
        [SerializeField] private bool gameIsStarting;

        void Start()
        {
            DeliveranceGameManager.Instance.EventRegister.UIButtonPressEventEventHandler += OnUIButtonPressEvent;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            Debug.Log("MainMenuState enter");
            gameIsStarting = false;
            isActive = true;
        }

        public void OnExit()
        {
            Debug.Log("MainMenuState exit");
            isActive = false;
        }

        public bool CanTransitionGameIsStarting()
        {
            return gameIsStarting;
        }

        private void OnUIButtonPressEvent(object _, UIButtonPressEvent e)
        {
            if (e.EventName == UIButtonEvents.StartGame)
            {
                gameIsStarting = true;
            }
        }
    }
}