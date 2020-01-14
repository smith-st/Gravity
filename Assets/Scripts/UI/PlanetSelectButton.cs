using UnityEngine;
using UnityEngine.EventSystems;

public class PlanetSelectButton : MonoBehaviour, IPointerClickHandler
{
    public PlanetType type;

    private IPlanetChanger _listener;

    public void OnPointerClick(PointerEventData eventData)
    {
        _listener?.ChangeCurrentPlanet(type);
    }

    public void AddListener(IPlanetChanger listener)
    {
        _listener = listener;
    }
}