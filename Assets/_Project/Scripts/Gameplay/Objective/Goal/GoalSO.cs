using UnityEngine;

namespace Deliverance.Gameplay.Objective
{
    [CreateAssetMenu(fileName = "New Goal", menuName = "Deliverance/Objectives/Goal")]
    public class GoalSO : ScriptableObject
    {
        public string title;
        public string description;
    }
}