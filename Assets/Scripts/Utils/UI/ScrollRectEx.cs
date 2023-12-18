using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace ParagorGames.Utils.UI
{
    public class ScrollRectEx : ScrollRect
    {
        [SerializeField] private ScrollRect _parentScroll;
        
        private bool _routeToParent = false;

        protected override void OnEnable()
        {
            if (_parentScroll == null)
            {
                _parentScroll = transform.parent.GetComponentInParent<ScrollRect>();
            }
            
            base.OnEnable();
        }

        public override void OnInitializePotentialDrag(PointerEventData eventData)
        {
            if (_parentScroll != null)
            {
                ((IInitializePotentialDragHandler)_parentScroll).OnInitializePotentialDrag(eventData);
            }
            
            base.OnInitializePotentialDrag(eventData);
        }
 
        public override void OnDrag(PointerEventData eventData)
        {
            if (_routeToParent)
            {
                if (_parentScroll != null)
                {
                    ((IDragHandler)_parentScroll).OnDrag(eventData);
                }
            }
            else
            {
                base.OnDrag(eventData);
            }
        }
 
        public override void OnBeginDrag(PointerEventData eventData)
        {
            if (!horizontal && Math.Abs(eventData.delta.x) > Math.Abs(eventData.delta.y))
            {
                _routeToParent = true;
            }
            else if (!vertical && Math.Abs(eventData.delta.x) < Math.Abs(eventData.delta.y))
            {
                _routeToParent = true;
            }
            else
            {
                _routeToParent = false;
            }
 
            if (_routeToParent)
            {
                if (_parentScroll != null)
                {
                    ((IBeginDragHandler)_parentScroll).OnBeginDrag(eventData);
                }
            }
            else
            {
                base.OnBeginDrag(eventData);
            }
        }
 
        public override void OnEndDrag(PointerEventData eventData)
        {
            if (_routeToParent)
            {
                if (_parentScroll != null)
                {
                    ((IEndDragHandler)_parentScroll).OnEndDrag(eventData);
                }
            }
            else
            {
                base.OnEndDrag(eventData);
            }
            
            _routeToParent = false;
        }
    }
}