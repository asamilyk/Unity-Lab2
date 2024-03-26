using System.Collections.Generic;
using UnityEngine;

namespace CardGame
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private List<CardLayout> layouts;
        [SerializeField] private List<CardAsset> assets;
        [SerializeField] private int handCapacity;
        [SerializeField] private CardLayout centerLayout;
        [SerializeField] private CardLayout bucketLayout;

        private void Start()
        {
            int id = 0;
            foreach (var layout in layouts)
            {
                layout.LayoutId = id++;
            }

            centerLayout.LayoutId = id++;
            bucketLayout.LayoutId = id;
            
            CardGame.Instance.Init(layouts, assets, handCapacity, centerLayout, bucketLayout);
        }
        
        public void StartTurn()
        {
           CardGame.Instance.StartTurn();
        }
    }
}