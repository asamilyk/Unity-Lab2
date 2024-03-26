using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace CardGame
{

    public class CardGame
    {
        private static CardGame _instance;
        public static CardGame Instance
        {
            get => _instance ??= new CardGame();
        }

        private static int _cnt = 1;
        public List<CardLayout> Layouts = new();
        private readonly Dictionary<CardInstance, CardView> _cardDictionary = new(); 
        private List<CardAsset> _initialCards;
        public int HandCapacity;
        private CardLayout _bucketLayout;
        public CardLayout _centerLayout;

        public void Init(List<CardLayout> layouts, List<CardAsset> assets, int capacity, CardLayout center, CardLayout bucket)
        {
            Layouts = layouts;
            _initialCards = assets;
            HandCapacity = capacity;
            _centerLayout = center;
            _bucketLayout = bucket;
            
            StartGame();
        }

        /// <summary>
        /// Creating cars for each layout
        /// </summary>
        private void StartGame()
        {
            foreach (var layout in Layouts)
            {
                foreach (var cardAsset in _initialCards)
                {
                    CreateCard(cardAsset, layout.LayoutId);
                }
            }
        }

        private void CreateCard(CardAsset asset, int layoutNumber)
        {
            var instance = new CardInstance(asset)
            {
                LayoutId = layoutNumber,
                CardPosition = Layouts[layoutNumber].NowInLayout++
            };
            CreateCardView(instance);
            MoveToLayout(instance, layoutNumber);
        }

        private void CreateCardView(CardInstance instance)
        {
            GameObject newCardInstance = new GameObject($"Card {_cnt++}");
            CardView view = newCardInstance.AddComponent<CardView>();
            Image image = newCardInstance.AddComponent<Image>();
            
            view.Init(instance, image);
            Button button = newCardInstance.AddComponent<Button>();
            button.onClick.AddListener(view.PlayCard);
            newCardInstance.transform.SetParent(Layouts[instance.LayoutId].transform);

            _cardDictionary[instance] = view;
        }
        
        private void MoveToLayout(CardInstance card, int layoutId)
        {
            int temp = card.LayoutId;
            card.LayoutId = layoutId;
            
            _cardDictionary[card].transform.SetParent(Layouts[layoutId].transform);
            
            RecalculateLayout(layoutId);
            RecalculateLayout(temp);
        }

        public void MoveToCenter(CardInstance card)
        {
            int temp = card.LayoutId;
            card.LayoutId = _centerLayout.LayoutId;
            _cardDictionary[card].transform.SetParent(_centerLayout.transform);
            RecalculateLayout(_centerLayout.LayoutId);
            RecalculateLayout(temp);
        }
        
        public void MoveToTrash(CardInstance card)
        {
            int temp = card.LayoutId;
            card.LayoutId = _bucketLayout.LayoutId;
            _cardDictionary[card].transform.SetParent(_bucketLayout.transform);
            
            try
            {
                Button button = _cardDictionary[card].GetComponent<Button>();
                button.enabled = false;
                button.onClick.RemoveAllListeners();
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
            
            RecalculateLayout(_bucketLayout.LayoutId);
            RecalculateLayout(temp);
        }

        public List<CardView> GetCardsInLayout(int layoutId)
        {
            return _cardDictionary.Where(x => x.Key.LayoutId == layoutId).Select(x => x.Value).ToList();
        }
        
        private List<CardInstance> GetInstancesInLayout(int layoutId)
        {
            return _cardDictionary.Where(x => x.Key.LayoutId == layoutId).Select(x => x.Key).ToList();
        }

        public void StartTurn()
        {
            foreach (var layout in Layouts)
            {
                ShuffleLayout(layout.LayoutId);
                layout.FaceUp = true;
                
                var cards = GetCardsInLayout(layout.LayoutId);
                for (int i = 0; i < HandCapacity; ++i)
                {
                    cards[i].StatusOfCard = CardStatus.Hand;
                }
            }
        }

        private void ShuffleLayout(int layoutId)
        {
            var cards = GetInstancesInLayout(layoutId);
            List<(int, int)> pairs = new List<(int, int)>();
            for (int i = 0; i < cards.Count; ++i)
            {
                for (int j = i + 1; j < cards.Count; ++j)
                {
                    pairs.Add((i, j));
                }
            }

            Random rnd = new Random();
            pairs = pairs.OrderBy(_ => rnd.Next()).ToList();

            for (var i = 1; i < cards.Count; ++i)
            {
                _cardDictionary[cards[pairs[i].Item1]].transform.SetSiblingIndex(pairs[i].Item2);
            }
        }

        private void RecalculateLayout(int layoutId)
        {
            var games = GetCardsInLayout(layoutId);

            for (int i = 0; i < games.Count; ++i)
            {
                games[i].CardPosition = i;
            }
        }
    }
}