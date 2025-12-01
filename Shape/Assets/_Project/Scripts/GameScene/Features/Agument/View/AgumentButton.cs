using UnityEngine;
using UnityEngine.EventSystems;

public class AgumentButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator _anim;

    void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_anim != null)
            _anim.SetBool("Hover", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_anim != null)
            _anim.SetBool("Hover", false);
    }
}
