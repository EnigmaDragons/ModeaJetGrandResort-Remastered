using UnityEngine;

public class RandomlyEnableOneChild : MonoBehaviour
{
    private void OnEnable()
    {
        var numChildren = transform.childCount;
        var selectedItem = Rng.Int(numChildren);
        for(var i = 0; i < numChildren; i++)
            transform.GetChild(i).gameObject.SetActive(i == selectedItem);
    }
}
