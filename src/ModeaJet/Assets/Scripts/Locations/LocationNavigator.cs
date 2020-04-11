using UnityEngine;

[CreateAssetMenu(menuName = "ModeaJet/LocationNavigator")]
public sealed class LocationNavigator : ScriptableObject
{
    public void GoToDockingBay() => GoTo(LocationName.DockingBay);
    public void GoTo(LocationName location) => Message.Publish(new GoToLocation { Location = location });
}
