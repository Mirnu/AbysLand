using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Player.Inventory.BackPack;
using Assets.Scripts.Player.Inventory.View;
using Assets.Scripts.Resources.Crafting;
using Assets.Scripts.Resources.Data;
using Zenject;

namespace Assets.Scripts.Inventory.Crafting {
    public class CraftingUIManager : IInitializable, IDisposable {
        
        private List<SelectableSlotView> _craftingGridSlots = new List<SelectableSlotView>();
        private SelectableSlotView _craftingResultSlot;

        private List<RecipeComponent> _components = new List<RecipeComponent>();
        private RecipeContainer _recipeComp;
        private ContainerSelectableSlots _container;

        public CraftingUIManager(List<SelectableSlotView> craftingGridSlots, SelectableSlotView craftingResultSlot, RecipeContainer cont, ContainerSelectableSlots container) {
            _recipeComp = cont;
            _container = container;
            _craftingGridSlots = craftingGridSlots;
            _craftingResultSlot = craftingResultSlot;
        }

        public void GridClearAll() { _craftingGridSlots.ForEach(x => x.Delete()); }

        public void GridClearSmart(RecipeComponent craftRes) {
            _craftingGridSlots.Where(x => x.TryGet(out Resource res))
            .ToList().ForEach(j => { 
                if(j.GetCount() > craftRes.count) { j.SetCount(j.GetCount() - craftRes.count); }
                else { j.Delete(); }
            });
        }

        public List<RecipeComponent> Retrieve() {
            _components.Clear();
            _craftingGridSlots.Where(x => x.TryGet(out Resource res))
            .ToList().ForEach(j => { 
                j.TryGet(out Resource res);
                _components.Add(new RecipeComponent(res, j.GetCount()));
            });
            return _components;
        }

        public void DisplayRes(RecipeComponent res) {
            _craftingResultSlot.TrySet(res.resource);
            _craftingResultSlot.SetCount(res.count);
        }

        public void RetrieveCraft(SelectableSlotView slot) {
            _recipeComp.TryFindCraft(Retrieve(), out RecipeComponent res);
            slot.TrySet(res.resource);
            slot.SetCount(res.count);
        }

        public void Initialize()
        {
            _craftingGridSlots.ForEach(x => x.LeftMouseClick += delegate { RetrieveCraft(x); });
            _craftingResultSlot.LeftMouseClick += delegate { _container.bindCraftLeft(_craftingResultSlot);};
        }

        public void Dispose()
        {
            _craftingResultSlot.LeftMouseClick -= delegate { _container.bindCraftLeft(_craftingResultSlot);};
        }
    }
}