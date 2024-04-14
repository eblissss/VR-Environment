using System;
using TMPro;
using UnityEngine;

public class TreeCustomizer : MonoBehaviour
{
	private GameObject _tree;

	public GameObject BirchTree;
	public GameObject PalmTree;
	public GameObject SpruceTree;
	public GameObject WillowTree;

	public Material SpringMat;
	public Material SummerMat;
	public Material FallMat;
	public Material WinterMat;

	private const double DanceTime = 2 * Math.PI;
	public bool Dancing;
	private float _danceBegin;
	
	private Vector3 _oPos;
	private Quaternion _oRot;
	
	private void Start()
	{
		if (!(BirchTree && PalmTree && SpruceTree && WillowTree))
		{
			Debug.LogError("Tree(s) not loaded!");
		}

		_tree = gameObject;
		_oPos = transform.position;
		_oRot = transform.rotation;
	}

	public void OnTypeChange(TMP_Dropdown dropdown)
	{
		var typeStr = dropdown.options[dropdown.value].text; 
		var newTree = typeStr switch
		{
			"Birch"  => BirchTree,
			"Palm"   => PalmTree,
			"Spruce" => SpruceTree,
			"Willow" => WillowTree,
			_        => null
		};
		if (newTree == null) return;
		
		var newMesh = newTree.GetComponent<MeshFilter>().mesh;
		_tree.GetComponent<MeshFilter>().mesh = newMesh;
	}
	
	public void OnSeasonChange(TMP_Dropdown dropdown)
	{
		var seasonStr = dropdown.options[dropdown.value].text; 
		Debug.Log(seasonStr);
		var newMat = seasonStr switch
		{
			"Spring" => SpringMat,
			"Summer" => SummerMat,
			"Fall"   => FallMat,
			"Winter" => WinterMat,
			_        => null
		};
		if (newMat == null) return;

		_tree.GetComponent<Renderer>().material = newMat;
	}

	public void OnSizeChange(float size)
	{
		_tree.transform.localScale = Vector3.one * size;
	}
	
	public void OnDance()
	{
		Dancing = true;
		_danceBegin = Time.time;
	}

	private void FixedUpdate()
	{
		if (!Dancing) return;

		var dTime = Time.time - _danceBegin;
		if (dTime < DanceTime)
		{
			_tree.transform.Rotate(new Vector3(
				                       (float)Math.Sin(dTime * 4), 
				                       (float)Math.Cos(dTime * 4),
				                       (float)Math.Sin(dTime * 3)));
			_tree.transform.position += new Vector3(
				(float)(Math.Acos(Math.Cos(2 * Math.PI * dTime)) - (Math.PI / 2)) / 30,
				(float)Math.Cos(4 * Math.PI * dTime) / 30,
				0);
		} else if (dTime < DanceTime + 3)
		{
			var slerpRot = Quaternion.Slerp(transform.rotation, _oRot, 0.05f);
			var slerpPos = Vector3.Slerp(transform.position, _oPos, 0.05f);
			transform.SetPositionAndRotation(slerpPos, slerpRot);
		} else
		{
			Dancing = false;
		}
	}
}
