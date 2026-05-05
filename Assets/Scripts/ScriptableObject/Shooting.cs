using UnityEngine;

[CreateAssetMenu(fileName = "Shooting", menuName = "Scriptable Objects/Shooting")]
public class Shooting : ScriptableObject
{
    public float fireForce;
    public float fireRate;
    public float damage;
    public float lifetime;
    public string bulletPattern;
}
