using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectViewManager : Singleton<EffectViewManager>
{
    Transform effectContainer;

	// Use this for initialization
	void Awake () {
       effectContainer = transform.Find("EffectContainer");
        if (effectContainer != null)
            Debug.Log("Found Effects");
	}

    public void GenerateEffects(string effectName, Vector3 position, float duration)
    {
        var effect = Instantiate(Resources.Load<GameObject>(PrefabPath.EffectPath + effectName), position, Quaternion.identity, effectContainer);
        Destroy(effect.gameObject, duration);
    }
}
