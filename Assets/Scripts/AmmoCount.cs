using UnityEngine;
using UnityEngine.UI;

public class AmmoCount : MonoBehaviour
{
    public Text ammunationText;
    public Text magText;
    public static AmmoCount occurence;

    private void Awake()
    {
        occurence = this;
    }

    public void UpdateAmmoText(int presentAmmunation)
    {
        ammunationText.text = presentAmmunation.ToString();
    }

    public void UpdateMagText(int mag)
    {
        magText.text = mag.ToString();
    }
}
