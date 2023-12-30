using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Fadeandstuff : MonoBehaviour
{

    [SerializeField] private CanvasGroup fadingGroup;
    public bool isfaded = true;

    public void Fader()
    {
        isfaded = !isfaded;
        if (isfaded)
        {
            fadingGroup.DOFade(1, 2);
        }
        else
        {
            fadingGroup.DOFade(1, 2);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
