using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboViewManager : Singleton<ComboViewManager>
{
	public void ShowCombo(int Number)
    {
        var comboItem = AddComboItem();
        comboItem.Initialize(Number);
        Destroy(comboItem.gameObject, 1f);
    }

    ComboItemView AddComboItem()
    {
        var prefab = Instantiate<GameObject>(Resources.Load<GameObject>(PrefabPath.ComboItemView), transform);
        var comboItem = prefab.GetComponent<ComboItemView>();
        return comboItem;
    }
}
