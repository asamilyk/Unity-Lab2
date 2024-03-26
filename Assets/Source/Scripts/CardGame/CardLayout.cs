using System;
using UnityEngine;

namespace CardGame
{
    public class CardLayout : MonoBehaviour
    {
        internal int NowInLayout = 0;
        
        [SerializeField] private Vector2 offset;
        [SerializeField] private Vector2 cardOffset;
        internal int LayoutId;

        internal bool FaceUp;

        internal void Update()
        {
            var cardsInLayout = CardGame.Instance.GetCardsInLayout(LayoutId);
            
            foreach (var card in cardsInLayout)
            {
                try
                {
                    Transform cardTransform = card.GetComponent<Transform>();
                    
                    switch (card.StatusOfCard)
                    {
                        case CardStatus.CardDeck:
                        {
                            FaceUp = false;
                            cardTransform.localPosition = CalculatePosition(card.CardPosition, CardStatus.CardDeck);
                            card.Rotate(FaceUp);
                            break;
                        }
                        case CardStatus.Center:
                            FaceUp = true;
                            cardTransform.position = CardGame.Instance._centerLayout.transform.position;
                            card.Rotate(FaceUp);
                            break;
                        case CardStatus.Deleted:
                            FaceUp = false;
                            cardTransform.localPosition = CalculatePosition(card.CardPosition, CardStatus.Deleted);
                            card.Rotate(FaceUp);
                            break;
                        case CardStatus.Hand:
                        {
                            FaceUp = true;
                            cardTransform.localPosition = CalculatePosition(card.CardPosition, CardStatus.Hand);
                            card.Rotate(FaceUp);
                            break;
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
                }
                
            }
        }
        
        private Vector2 CalculatePosition(int siblingIndex, CardStatus status)
        {
            switch (status)
            {
                case CardStatus.CardDeck:
                    return new Vector2(siblingIndex * offset.x, 0);
                case CardStatus.Hand:
                    return new Vector2(siblingIndex * offset.x, cardOffset.y);
                case CardStatus.Deleted:
                    return new Vector2(0, cardOffset.y * siblingIndex);
                
            }

            return new Vector2();
        }

    }
}