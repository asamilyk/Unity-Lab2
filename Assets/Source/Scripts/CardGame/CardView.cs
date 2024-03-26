using UnityEngine;
using UnityEngine.UI;

namespace CardGame
{
    public class CardView : MonoBehaviour
    {
        private CardInstance _cardInstance;
        private Image _image;
        
        public CardStatus StatusOfCard
        {
            get => _cardInstance.Status;
            set => _cardInstance.Status = value;
        }
        
        public int CardPosition
        {
            get => _cardInstance.CardPosition;
            set => _cardInstance.CardPosition = value;
        }
        
        public void Rotate(bool up)
        {
            if (up)
            {
                _image.sprite = _cardInstance.Asset.on;
            }
            else
            {
                _image.sprite = _cardInstance.Asset.off;
            }
        }
        
        public void Init(CardInstance instance, Image imageObj)
        {
            _cardInstance = instance;
            _image = imageObj;
        }

        /// <summary>
        /// Change of card status 
        /// </summary>
        public void PlayCard()
        {
            switch (_cardInstance.Status)
            {
                case CardStatus.Hand:
                    CardGame.Instance.MoveToCenter(_cardInstance);
                    _cardInstance.Status = CardStatus.Center;
                    break;
                case CardStatus.Center:
                    CardGame.Instance.MoveToTrash(_cardInstance);
                    _cardInstance.Status = CardStatus.Deleted; 
                    break;
            }
        }
    }
}