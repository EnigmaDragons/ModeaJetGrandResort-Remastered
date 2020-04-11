using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class CurrentChoices : ScriptableObject
{
    public List<Choice> List
    {
        get
        {
            return TextList.Select((text, index) => new Choice { Text = text, NextID = NextIDList[index] }).ToList();
        }
        set
        {
            TextList = new List<string>();
            NextIDList = new List<string>();
            value.ForEach(x =>
            {
                TextList.Add(x.Text);
                NextIDList.Add(x.NextID);
            });
        }
    }

    public List<string> TextList;
    public List<string> NextIDList;
    public bool IsShowing;
}

public class Choice
{
    public string NextID;
    public string Text;
}