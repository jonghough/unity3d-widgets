using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class ScrollWheel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

	private List<RectTransform> _itemList;
	private Vector3[] _parentDim = new Vector3[4];
	private float _radiusY = 130;
	private float _radiusX = 80;
	private bool _isMoving = false;
	float _pWidth, _pHeight;
	private float angleDiff;
	private Vector2 dragPosition;

	// Use this for initialization
	void Start()
	{


		this.GetComponent<RectTransform>().GetLocalCorners(_parentDim);

		_pWidth = Mathf.Abs(_parentDim[2].x - _parentDim[0].x);
		_pHeight = Mathf.Abs(_parentDim[1].y - _parentDim[0].y);

	}

	// Update is called once per frame
	void Update()
	{

		if (_isMoving)
		{
			foreach (var item in _itemList)
			{
				item.SetParent(this.GetComponent<RectTransform>());
				item.gameObject.SetActive(true);
				float angle = item.GetComponent<ScrollWheelItem>().currentAngle + angleDiff;

				float y = this.transform.position.y + _radiusY * Mathf.Sin(angle);

				float x = this.transform.position.x + _radiusX * Mathf.Cos(angle);
				float z = this.transform.position.z;
				item.position = new Vector3(x, y, z);

				item.position = new Vector3(x, y, z);

			}
		}
	}

	public void Setup(List<RectTransform> items)
	{
		_itemList = items;
		Vector3 center = new Vector3(0, 0, 0);
		float angle = 0;
		float dAngle = Mathf.PI * 2.0f / _itemList.Count;
		//_radius =  (0.5f* 100.0f) / Mathf.Sin(dAngle * 0.5f);

		foreach (var item in _itemList)
		{
			item.SetParent(this.GetComponent<RectTransform>());
			item.gameObject.SetActive(true);

			item.pivot = new Vector2(0.5f, 0.5f);
			item.anchorMin = new Vector2(0.5f, 0.5f);
			item.anchorMax = new Vector2(1f, 1f);


			float y = this.transform.position.y + _radiusY * Mathf.Sin(angle);
			float x = this.transform.position.x + _radiusX * Mathf.Cos(angle);
			float z = this.transform.position.z;
			item.position = new Vector3(x, y, z);

			item.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 40);

			item.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 40f);
			var scrollWheelItem = item.GetComponent<ScrollWheelItem>();


			scrollWheelItem.currentAngle = angle;

			item.transform.forward = Vector3.forward;
			angle += dAngle;
		}

	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		dragPosition = eventData.position;
		SetDraggedPosition(eventData);
	}

	public void OnDrag(PointerEventData data)
	{
		SetDraggedPosition(data);
	}

	private void SetDraggedPosition(PointerEventData data)
	{
		if (_itemList == null || _itemList.Count == 0)
			return;
		Debug.Log("delta " + data.delta);
		_isMoving = true;
		float angle = Mathf.Atan((data.position.y - this.transform.position.y) / (data.position.x - this.transform.position.x));

		float preangle = Mathf.Atan2((dragPosition.x - this.transform.position.x), (dragPosition.y - this.transform.position.y));
		float anglediff = angle - preangle;
		if (data.position.x - this.transform.position.x < 0)
		{
			//anglediff += Mathf.PI;
		}
		angleDiff = anglediff;
	}


	public void OnEndDrag(PointerEventData eventData)
	{
		foreach (var item in _itemList)
		{
			var scrollWheelItem = item.GetComponent<ScrollWheelItem>();
			float a = Mathf.Atan2((item.transform.position.x - this.transform.position.x), (item.transform.position.y - this.transform.position.y));

			//scrollWheelItem.currentAngle = a;

		}
	}



}
