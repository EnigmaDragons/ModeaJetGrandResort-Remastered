using System;
using Tiny;

public class JsonMediaType : IMediaType
{
    public string ConvertTo<T>(T obj) => Json.Encode(obj);
    public T ConvertFrom<T>(string media) => Json.Decode<T>(media);
    public object ConvertFrom(Type type, string media) => Json.Decode(media, type);
}