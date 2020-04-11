using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemPresentConditionNode : INodeContent
{
    private readonly IElement _itemNameElement;

    private string _itemName;

    public string Name => NodeTypes.ItemPresentCondition;
    public float Width => _itemNameElement.Width;
    public float Height => _itemNameElement.Height;

    public ItemPresentConditionNode(IMediaType mediaType, string media)
        : this(mediaType.ConvertFrom<ItemPresentConditionNodeData>(media)) { }
    public ItemPresentConditionNode() : this("") { }
    private ItemPresentConditionNode(ItemPresentConditionNodeData data) : this(data.ItemName) { }
    private ItemPresentConditionNode(string itemName)
    {
        Dictionary<string, Action> itemDictionary = new Dictionary<string, Action>();
        ScriptableExtensions.GetAllInstances<Item>().ForEach(x => itemDictionary[x.DisplayName] = () => _itemName = x.DisplayName);
        itemName = itemDictionary.ContainsKey(itemName) ? itemName : itemDictionary.First().Key;
        _itemNameElement = new ElementLabel(new OptionsElement(itemDictionary, itemName, 200), "Item");
        _itemName = itemName;
    }

    public void Draw(Vector2 position)
    {
        _itemNameElement.Draw(position);
    }

    public INodeContent Duplicate() => new ItemPresentConditionNode(_itemName);

    public string Save(IMediaType mediaType) => mediaType.ConvertTo(new ItemPresentConditionNodeData { ItemName = _itemName });
}

[Serializable]
public class ItemPresentConditionNodeData
{
    public string ItemName;
}