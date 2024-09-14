using UnityEngine;

namespace Deliverance.Gameplay.Objective
{
    [CreateAssetMenu(fileName = "New Objective", menuName = "Deliverance/Objectives/Objective")]
    public class ObjectiveSO : ScriptableObject
    {
        public string title;
        public string description;
    }
}