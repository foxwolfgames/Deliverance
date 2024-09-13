using Deliverance.UI;
using FWGameLib.Common.AudioSystem.Event;
using FWGameLib.Common.StateMachine;
using FWGameLib.InProject.AudioSystem;
using UnityEngine;

namespace Deliverance.GameState
{
    public class MainMenuState : MonoBehaviour, IState
    {
#pragma warning disable CS0414 // Field is assigned but its value is never used
        [SerializeField] private bool isActive;
#pragma warning restore CS0414 // Field is assigned but its value is never used

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
            DeliveranceGameManager.Instance.Audio.PlaySound(Sounds.MUSIC_MAIN_MENU);
        }

        public void OnExit()
        {
            Debug.Log("MainMenuState exit");
            isActive = false;
            new FWGLStopSoundEvent(Sounds.MUSIC_MAIN_MENU).Invoke();
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