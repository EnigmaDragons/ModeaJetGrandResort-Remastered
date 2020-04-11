using System;

public interface IMediaType
{
    string ConvertTo<T>(T obj);
    T ConvertFrom<T>(string media);
    object ConvertFrom(Type type, string media);
}