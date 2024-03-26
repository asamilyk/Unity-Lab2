using UnityEngine;

namespace CardGame
{
    /// <summary>
    /// Class for card prefab
    /// </summary>
    [CreateAssetMenu(fileName = "Asset", menuName = "CardAsset", order=51)]
    public class CardAsset : ScriptableObject
    {
        [SerializeField] private string cardName;
        [SerializeField] private CardColor color;
        [SerializeField] internal Sprite on;
        [SerializeField] internal Sprite off;
    }
        public enum CardColor
        {
            Black,
            Red
        }
}
