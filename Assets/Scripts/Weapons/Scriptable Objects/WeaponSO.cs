using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    [TextArea(5,5)]public string weaponDescription;
    public float weaponDamage;
    public float cadency;
    public int magazineSize;
    public float reloadTime;
}
