//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//========================================================================
// ■ Loitering_Points
//------------------------------------------------------------------------
//	徘徊地点達のクラス。
//========================================================================
public class Loitering_Points : Mono_Behaviour_EX {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	[HideInInspector] public List<Vector3> points;
//	Transform sea;
//	Vector3 default_delta_position;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	void Start() {
		points = new List<Vector3>();
		var rs = GetComponentsInChildren<Renderer>();
		foreach (var r in rs) {
			r.enabled = Debug.isDebugBuild;
			points.Add(r.transform.position);
		}
	}
/*
		sea = GameObject.FindWithTag("Water").transform;
		default_delta_position = transform.position - sea.position;
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	void Update() {
		transform.position = sea.position + default_delta_position;
	}
*/
	//--------------------------------------------------------------------
	// ● エディタ描画
	//--------------------------------------------------------------------
	void OnDrawGizmosSelected() {
		if (points == null) {
			points = new List<Vector3>();
			var rs = GetComponentsInChildren<Renderer>();
			foreach (var r in rs) {
				r.enabled = Debug.isDebugBuild;
				points.Add(r.transform.position);
			}
		}
		
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(points[0], 1);
		Gizmos.color = Color.yellow;
		for (var i = 0; i < points.Count - 1; i++) {
			Gizmos.DrawLine(points[i], points[i + 1]);
		}
		Gizmos.DrawLine(points[points.Count - 1], points[0]);
	}
}