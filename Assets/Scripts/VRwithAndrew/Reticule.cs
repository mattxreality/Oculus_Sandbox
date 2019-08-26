using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticule : MonoBehaviour
{

    public Pointer m_Pointer; // Instantiate public class 'Pointer'
    public SpriteRenderer m_CircleRenderer;

    public Sprite m_OpenSprite;
    public Sprite m_ClosedSprite;

    private Camera m_Camera = null;

    private void Awake()
    {
        m_Pointer.OnPointerUpdate += UpdateSprite; // Use info from subscribed 'Pointer' event
        m_Camera = Camera.main;
    }

    void Update()
    {
        // Orient reticule towards camera
        transform.LookAt(m_Camera.gameObject.transform);
    }

    private void OnDestroy()
    {
        m_Pointer.OnPointerUpdate -= UpdateSprite;
    }

    private void UpdateSprite(Vector3 point, GameObject hitObject)
    {
        transform.position = point;

        if(hitObject)
        {
            m_CircleRenderer.sprite = m_ClosedSprite;
        }
        else
        {
            m_CircleRenderer.sprite = m_OpenSprite;
        }
    }
}
