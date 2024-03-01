using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChangePreview : MonoBehaviour
{
    //private SpriteRenderer _this;
    [SerializeField] private Sprite[] maps;

    /*public void Start()
    {
        _this = gameObject.GetComponent<SpriteRenderer>();
    }*/
    public void ChangeImagePreview(int nbMap)
    {
        Debug.Log(maps[nbMap - 1].name);
        this.gameObject.GetComponent<RawImage>().texture = maps[nbMap - 1].texture;
    }
}
