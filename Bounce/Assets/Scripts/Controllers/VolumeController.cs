using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeController : MonoBehaviour
{
    public Volume volumeComponent;
    public VolumeProfile newProfile;
    public void AdjustVignette()
    {
        Vignette vignette;
        if (volumeComponent.profile.TryGet(out vignette))
        {
            // Change intensity and force the override state
            vignette.intensity.Override(0.5f);
        }
    }
}
