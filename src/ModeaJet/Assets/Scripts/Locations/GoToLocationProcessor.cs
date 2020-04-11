using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class GoToLocationProcessor : OnMessage<GoToLocation>
{
    [SerializeField] private LocationName startingLocation;
    [SerializeField] private Location[] locations;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject gameWorld;
    [SerializeField] private FloatReference loadingScreenDurationSeconds = new FloatReference(1f);
    
    private readonly Dictionary<LocationName, GameObject> _locations = new Dictionary<LocationName, GameObject>();
    private Location _currentLocation;
    private GameObject _current;
    
    private void Start() => Execute(new GoToLocation { Location = startingLocation });
    
    protected override void Execute(GoToLocation msg)
    {
        ShowLoadingScreen();
        DisableCurrentLocation();
        LoadLocation(msg);
        StartCoroutine(ShowNewLocationAfterDelay());
    }

    private void DisableCurrentLocation()
    {
        if (_current != null)
            _current.SetActive(false);
    }

    private IEnumerator ShowNewLocationAfterDelay()
    {
        yield return new WaitForSeconds(loadingScreenDurationSeconds);
        _current.SetActive(true);
        Message.Publish(new ArrivedAtLocation { Location = _currentLocation });
        loadingScreen.SetActive(false);
    }

    private void LoadLocation(GoToLocation msg)
    {
        var location = locations.First(x => x.LocationName == msg.Location);
        if (!_locations.ContainsKey(msg.Location))
            _locations[msg.Location] = Instantiate(location.Prefab, gameWorld.transform);
        _currentLocation = location;
        _current = _locations[msg.Location];
        _current.SetActive(false);
    }

    private void ShowLoadingScreen() => loadingScreen.SetActive(true);
}
