using UnityEngine;

namespace Player
{
    public class SelectCard : MonoBehaviour
    {
        [SerializeField] private GameObject cardPanel;
        
        private GatherInputs inputs;
        
        public SelectCard Instance;
        
        private void Awake()
        {
            Instance = this;
            inputs = new GatherInputs();
        }
    
    }
}
