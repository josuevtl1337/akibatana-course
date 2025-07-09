using TMPro;
using UnityEngine;

public class HUDPlayerWeapon : MonoBehaviour
{
    public static HUDPlayerWeapon Instance;

    public TextMeshProUGUI magazineText;

    void Awake()
    {
        Instance = this;
    }
 
    public void UpdateMagazineWeapon(int currentMagazine, int maxMagazine)
    {
            magazineText.text = currentMagazine + " / " + maxMagazine;
    }

    public void UpdateReloading()
    {
            magazineText.text = "Reloading ...";
    }
}
