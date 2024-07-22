using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject[] _images;
    private int _count;

    void Start()
    {
        _count = transform.childCount;
        _images = new GameObject[_count];
        for (int i = 0; i < _count; i++)
        {
            _images[i] = transform.GetChild(i).gameObject;
        }
    }

    public void LoadScene()
    {
        for (int i = 0; i < _count; i++)
        {
            _images[i].GetComponent<RectTransform>().DOPivot(new Vector2(0.5f, 0.5f), 2f);
        }
    }
}
