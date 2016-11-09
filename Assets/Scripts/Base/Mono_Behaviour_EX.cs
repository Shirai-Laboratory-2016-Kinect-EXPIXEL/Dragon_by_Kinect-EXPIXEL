//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//========================================================================
// ■ Mono_Behaviour_EX
//------------------------------------------------------------------------
//	Unity基盤クラスの拡張クラス。
//========================================================================

public class Mono_Behaviour_EX : MonoBehaviour {
	//--------------------------------------------------------------------
	// ● 全子オブジェクトを取得
	//--------------------------------------------------------------------
	public List<GameObject> get_all() {
		var all_children = new List<GameObject>();
		get_children(gameObject, ref all_children);
		return all_children;
	}
	//--------------------------------------------------------------------
	// ● 子オブジェクトを取得
	//--------------------------------------------------------------------
	void get_children(GameObject go,
								ref List<GameObject> all_children) {
		var children = go.GetComponentInChildren<Transform>();
		// 子が居なければ終了
		if (children.childCount == 0)	return;

		foreach (Transform t in children) {
			all_children.Add(t.gameObject);
			get_children(t.gameObject, ref all_children);
		}
	}
	//--------------------------------------------------------------------
	// ● 全子コンポーネントを取得
	//--------------------------------------------------------------------
	public List<Type> get_all_components<Type>() where Type : Component {
		var all_components = new List<Type>();

		var game_objects = get_all();
		foreach (var go in game_objects) {
			var c = go.GetComponent<Type>();
			if (c != null)	all_components.Add(c);
		}

		return all_components;
	}
	//--------------------------------------------------------------------
	// ● レイヤー内の全子オブジェクトを取得
	//--------------------------------------------------------------------
	public List<GameObject> get_all_in_layer(string layer) {
		var id = LayerMask.NameToLayer(layer);
		var all_children = new List<GameObject>();
		
		var game_objects = get_all();
		foreach (var go in game_objects) {
			if (go.layer == id)	all_children.Add(go);
		}

		return all_children;
	}
	//--------------------------------------------------------------------
	// ● ボタン押下コールバック関数
	//--------------------------------------------------------------------
	public virtual void on_click(GameObject button) {
	}
}