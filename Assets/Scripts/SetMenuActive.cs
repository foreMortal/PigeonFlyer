using DG.Tweening;
using UnityEngine;

public class SetMenuActive : MonoBehaviour
{
    [SerializeField] private GameObject active, unactive;

    public void SetToggleActive(bool state)
    {
        active.SetActive(state);
    }

    public void SetButtonActive(bool state)
    {
        Tweener a = DOTween.To(() => unactive.transform.localPosition, x => unactive.transform.localPosition = x, new Vector3(0, 1000f, 0), 0.5f);
        a.onComplete += () => { unactive.SetActive(false); };
        
        active.SetActive(state);
        active.transform.localPosition = new Vector3(0f, -1000f, 0f);
        DOTween.To(() => active.transform.localPosition, x => active.transform.localPosition = x, new Vector3(0, 0f, 0), 0.5f);
    }
}
