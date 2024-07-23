using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Player.Handlers {
    public class PlayerHotbarUIHandler : IInitializable
    {
        private List<Image> _slots = new List<Image>();
        private List<Image> _slots_background = new List<Image>();
        private Sprite _emptySlot;
        private Sprite _selectedSlot;

        public PlayerHotbarUIHandler(List<Image> slots, List<Image> slots_background, Sprite emptySlot, Sprite selectedSlot) {
            _slots = slots;
            _slots_background = slots_background;
            _emptySlot = emptySlot;
            _selectedSlot = selectedSlot;
        }

        public void SwitchToSlot(int index) {
            if(_slots_background.Any(x => x.sprite == _selectedSlot)) {
                _slots_background.Find(x => x.sprite == _selectedSlot).sprite = _emptySlot;
            }
            
            _slots_background[index].sprite = _selectedSlot;
        }

        public void UpdateHotbar(List<Sprite> sprites) {
            sprites.ForEach(s => {
                if(s != null) {
                    _slots[sprites.IndexOf(s)].sprite = s;
                }
            });
            _slots.ForEach(s => {
                if(s.sprite == null) {
                    s.color = Color.clear;
                }
            });
        }

        public void Initialize()
        {
            _slots_background[0].sprite = _selectedSlot;
        }
    }
}