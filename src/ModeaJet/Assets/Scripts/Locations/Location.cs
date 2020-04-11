using UnityEngine;

[CreateAssetMenu(menuName = "ModeaJet/Location")]
public class Location : ScriptableObject
{
    public LocationName LocationName;
    public GameObject Prefab;
    public string CustomDisplayName;
    
    public string DisplayName => string.IsNullOrWhiteSpace(CustomDisplayName) 
        ? LocationName.ToString().WithSpaceBetweenWords() 
        : CustomDisplayName;
}
