using Deliverance.Gameplay.Objective;
using FWGameLib.InProject.AudioSystem;
using UnityEngine;

namespace Deliverance.Development
{
    public class ObjectiveEnterTwoTriggers : MonoBehaviour
    {
        public void StartObjective()
        {
            new ActivateEnterTriggerCriteriaEvent("trigger1").Invoke();
            new ActivateEnterTriggerCriteriaEvent("trigger2").Invoke();
        }

        public void CompleteObjective()
        {
            DeliveranceGameManager.Instance.Audio.PlaySound(Sounds.SFX_UI_BUTTON_CLICK);
        }
    }
}