using UnityEngine;

namespace CardGame
{
    [CreateAssetMenu(fileName = "Asset", menuName = "CardAsset", order=51)]
    public class CardAsset : ScriptableObject
    {
        [SerializeField] private string cardName;
        [SerializeField] private CardColor color;
        
        [SerializeField] internal Sprite onSprite;
        [SerializeField] internal Sprite offSprite;
    }
        public enum CardColor
        {
            Black,
            Red
        }
}
