using UnityEngine;

[CreateAssetMenu]
public class SequencerDependencies : ScriptableObject
{
    public IMediaType MediaType => new JsonMediaType();
}