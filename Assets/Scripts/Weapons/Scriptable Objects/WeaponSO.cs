using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    [TextArea(5,5)]public string WeaponDescription;
    public float WeaponDamage;
    public float Cadency;
    public int BulletsOnMagazineSize;
    public int MaxBulletsOnMagazineSize;
    public float ReloadTime;

    [Header("Weapon model")]
    public WeaponModel WeaponModel;

    [Header("Animation settings")]
    public AnimatorOverrideController AnimatorOverrideController;
}
