using UnityEngine;

[CreateAssetMenu(fileName = "New Planet", menuName = "Planet Settings", order = 1)]
public class PlanetSettings : ScriptableObject
{
    public Color color;
    public float gravity;
    public PlanetType type;
}
