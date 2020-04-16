using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public sealed class GameState
{
    public readonly Dictionary<string, LocationName> Memories = new Dictionary<string, LocationName>();
    public readonly HashSet<string> ViewedItems = new HashSet<string>();
    public readonly HashSet<string> Thoughts = new HashSet<string>();
    public string PlayerName { get; set; } = GameResources.DefaultPlayerCharacterName;
    public string CurrentLocation { get; set; } = GameResources.MainMenuSceneName;
    public string CurrentLocationImage { get; set; } = "";
    public bool ShowObjectives { get; set; } = true;

//    public GameState()
//    {
//        Event.SubscribeForever(EventSubscription.Create<ItemViewed>(x => ChangeState(() => _viewedItems.Add(x.Item)), this));
//        Event.SubscribeForever(EventSubscription.Create<ThoughtGained>(x => ChangeState(() => _thoughts.Add(x.Thought)), this));
//        Event.SubscribeForever(EventSubscription.Create<ThoughtLost>(x => ChangeState(() => _thoughts.Remove(x.Thought)), this));
//        Event.SubscribeForever(EventSubscription.Create<GainDialogueMemory>(x => ChangeState(() =>
//        {
//            _viewedItems.Add(x.Dialog);
//            _memories.Add(x.Dialog, x.Location);
//        }), this));
//    }
}
