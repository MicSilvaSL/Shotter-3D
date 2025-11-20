using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Entity Data", menuName = "Create Player Data")]
public class PlayerData : ScriptableObject
{
    [SerializeField] private int maxHp = 100;
    [SerializeField] private WeaponSO[] startingWeapons;

    public int MaxLife => maxHp;
    public PlayerEvents Events { get; private set; }

	private void OnValidate()
	{
        if (maxHp <= 0)
            maxHp = 1;
	}
}

public struct PlayerEvents
{
    public Action<float> OnTakeDamage;
    public Action OnDeath;

    public Action OnShot;
    public Action OnCharingWeapon;
    public Action<WeaponSO> OnChangeWeapon;

}
