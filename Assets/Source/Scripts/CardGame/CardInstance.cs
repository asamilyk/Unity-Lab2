using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CardGame
{
    public class CardInstance
    {
        internal readonly CardAsset Asset;

        public int CardPosition { get; set; }
        public int LayoutId { get; set; }
        internal CardStatus Status { get; set; }
        

        public CardInstance(CardAsset asset)
        {
            Asset = asset;
        }
    }
}