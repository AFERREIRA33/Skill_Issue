using UnityEngine;
using UnityEngine.EventSystems;

public class selectedMap : MonoBehaviour , ISelectHandler
{
    [SerializeField] private int nbMap = 1;
    [SerializeField] private ChangePreview target;

    
    public void OnSelect(BaseEventData eventData)
    {
        target.ChangeImagePreview(nbMap);
    }
}
