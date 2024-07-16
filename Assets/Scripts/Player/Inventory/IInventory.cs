using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class IInventory {
    //КЛАСС - ЗАГЛУШКА
    private List<int> itemsList = new List<int>();

    public Action OnInvChanged;

    public IInventory(List<int> items) {
        itemsList = items;
    }

    public void Clear() {
        itemsList.Clear();
    }

    public bool TryMoveItem(int toIndex) {
        if(toIndex != 0) { return false; }
        return true;
    }

    public void MoveItem(int fromIndex, int toIndex) {
        if(TryMoveItem(toIndex)) {
            toIndex = fromIndex;
            fromIndex = 0;
        }
    }
}