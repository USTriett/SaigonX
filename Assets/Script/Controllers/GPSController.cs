using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GPSController : MonoBehaviour
{
    private Vector2 _location;

    private async void Awake()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("Location services are not enabled by the user.");
            return;
        }

        await StartLocationServiceAsync();
    }

    private async Task StartLocationServiceAsync()
    {
        Input.location.Start();

        int maxWait = 20; // Max wait time to initialize location service

        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            await Task.Delay(1000);
            maxWait--;
        }

        if (maxWait <= 0)
        {
            Debug.Log("Timed out waiting for location services to initialize.");
            return;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location.");
            return;
        }
        else
        {
            _location = new Vector2(
                Input.location.lastData.latitude,
                Input.location.lastData.longitude
            );
            Debug.Log($"Location: {_location.x}, {_location.y}");
        }
    }
}
