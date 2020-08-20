using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{
    public LayerMask touchInputMask;
    private List<GameObject> _touchList = new List<GameObject>();
    private GameObject[] _touchesOld;
    private RaycastHit _hit;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
        {
            _touchesOld = new GameObject[_touchList.Count];
            _touchList.CopyTo(_touchesOld);
            _touchList.Clear();
            
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out _hit, touchInputMask))
                {
                    GameObject recipient = _hit.transform.gameObject;
                    
                    _touchList.Add(recipient);

                    if (Input.GetMouseButtonDown(0))
                    {
                        recipient.SendMessage("OnTouchDown",_hit.point,SendMessageOptions.DontRequireReceiver);
                    }
                
                    if (Input.GetMouseButtonUp(0))
                    {
                        recipient.SendMessage("OnTouchUp",_hit.point,SendMessageOptions.DontRequireReceiver);
                    }
                
                    if (Input.GetMouseButton(0))
                    {
                        recipient.SendMessage("OnTouchStay",_hit.point,SendMessageOptions.DontRequireReceiver);
                    }
                
                    if (Input.GetMouseButton(0))
                    {
                        recipient.SendMessage("OnTouchMove",_hit.point,SendMessageOptions.DontRequireReceiver);
                    }
                }

            foreach (GameObject g in _touchesOld)
            {
                if (!_touchList.Contains(g))
                {
                    g.SendMessage("OnTouchExit",_hit.point,SendMessageOptions.DontRequireReceiver);
                }
            }
        }
#endif
        
        if (Input.touchCount > 0)
        {
            _touchesOld = new GameObject[_touchList.Count];
            _touchList.CopyTo(_touchesOld);
            _touchList.Clear();
            
            foreach (Touch touch in Input.touches)
            {
                Ray ray = _camera.ScreenPointToRay(touch.position);

                if (Physics.Raycast(ray, out _hit, touchInputMask))
                {
                    GameObject recipient = _hit.transform.gameObject;
                    
                    _touchList.Add(recipient);

                    if (touch.phase == TouchPhase.Began)
                    {
                        recipient.SendMessage("OnTouchDown",_hit.point,SendMessageOptions.DontRequireReceiver);
                    }
                
                    if (touch.phase == TouchPhase.Ended)
                    {
                        recipient.SendMessage("OnTouchUp",_hit.point,SendMessageOptions.DontRequireReceiver);
                    }
                
                    if (touch.phase == TouchPhase.Stationary)
                    {
                        recipient.SendMessage("OnTouchStay",_hit.point,SendMessageOptions.DontRequireReceiver);
                    }
                
                    if (touch.phase == TouchPhase.Moved)
                    {
                        recipient.SendMessage("OnTouchMove",_hit.point,SendMessageOptions.DontRequireReceiver);
                    }
                
                    if (touch.phase == TouchPhase.Canceled)
                    {
                        recipient.SendMessage("OnTouchExit",_hit.point,SendMessageOptions.DontRequireReceiver);
                    }
                }
            }

            foreach (GameObject g in _touchesOld)
            {
                if (!_touchList.Contains(g))
                {
                    g.SendMessage("OnTouchExit",_hit.point,SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
}
