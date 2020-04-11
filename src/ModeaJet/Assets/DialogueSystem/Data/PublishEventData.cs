using System;
using System.Collections.Generic;

[Serializable]
public class PublishEventData
{
    public string NextID;
    public string EventType;
    public Dictionary<string, object> Properties;
    public List<ScriptableData> Scriptables;
}
