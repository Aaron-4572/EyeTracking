using UnityEngine;


using Eyeware.BeamEyeTracker.Unity;

public class EyeCrosshair : BeamEyeTrackerMonoBehaviour
{

    public RectTransform crosshair;

    public RectTransform canvasRect;

    //How quickly the crosshair follows 
    public float smoothSpeed = 15f;

    private void Update()
    {

        //make sure beam is connected   
        if (betInputDevice == null)
        {
            return;
        }

        //make sure tracking is activated
        if (betInputDevice.trackingStatus.ReadValue() != 1)
        {
            return;
        }

        //Finding position player is looking 
        Vector2 gazePosition = betInputDevice.viewportGazePosition.ReadValue();

        //Ensuring look position wont leave the screen
        float x = Mathf.Clamp01(gazePosition.x);
        float y = Mathf.Clamp01(gazePosition.y);

        //Converting gaze position to canvas coordinates 
        Vector2 targetPosition = new Vector2((x - 0.5f) * canvasRect.rect.width, (y - 0.5f) * canvasRect.rect.height);


        //Making crosshair movement smooth 
        crosshair.anchoredPosition = Vector2.Lerp(crosshair.anchoredPosition, targetPosition, 1f - Mathf.Exp(-smoothSpeed * Time.deltaTime));
    }

    

}
