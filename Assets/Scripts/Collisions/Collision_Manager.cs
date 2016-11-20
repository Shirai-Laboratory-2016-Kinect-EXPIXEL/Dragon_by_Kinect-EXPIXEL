//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
//========================================================================
// ■ Collision_Manager
//------------------------------------------------------------------------
//	衝突判定の管理クラス。
//========================================================================

public class Collision_Manager : Mono_Behaviour_EX {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	public Dictionary<string, ArrayList> all;
	public Dictionary< string, Dictionary<Transform, float> > hit_second;
	public Dictionary<Collision, float> collisions;
	public Dictionary<Collider, float> triggers;
	public AI ai;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	protected virtual void Start() {
		all = new Dictionary<string, ArrayList>();
		hit_second =
			new Dictionary< string, Dictionary<Transform, float> >();
		collisions = new Dictionary<Collision, float>();
		triggers = new Dictionary<Collider, float>();

		if (ai == null)	ai = GetComponent<AI>();
	}
	//--------------------------------------------------------------------
	// ● 更新（早）
	//--------------------------------------------------------------------
	void FixedUpdate() {
//		clear();
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	void Update() {
//		clear();
	}
	//--------------------------------------------------------------------
	// ● 更新（遅）
	//--------------------------------------------------------------------
	protected virtual void LateUpdate() {
/*
		foreach ( Collision key in new List<Collision>(collisions.Keys) )
			collisions[key] += Time.deltaTime;
		
		foreach ( Collider key in new List<Collider>(triggers.Keys) )
			triggers[key] += Time.deltaTime;


		foreach (ArrayList array_list in all.Values) {
			for (int i = array_list.Count - 1; i >= 0; i--) {
				if (array_list[i] == null)
					array_list.RemoveAt(i);
			}
		}

		clear();
*/
	}
	//--------------------------------------------------------------------
	// ● 削除
	//--------------------------------------------------------------------
	public void clear() {
		foreach (ArrayList array_list in all.Values)
			array_list.Clear();
		all.Clear();
	}
	//--------------------------------------------------------------------
	// ● 衝突を確認
	//--------------------------------------------------------------------
	public bool check(Transform target, string region_name) {
//		return all.ContainsKey(region_name) &&
//			all[region_name].Contains(target);

		if ( all.ContainsKey(region_name) ) {
			foreach (Transform t in all[region_name])
				if ( t != null && (t == target || t.root == target) )
					return true;
		}
		return false;
	}
	public bool check_region(string region_name) {
		return all.ContainsKey(region_name);
	}
	//--------------------------------------------------------------------
	// ● 衝突物を取得
	//--------------------------------------------------------------------
	public ArrayList get(string region_name) {
		if ( all.ContainsKey(region_name) )
			return all[region_name];

		return new ArrayList();
	}
	//--------------------------------------------------------------------
	// ● 衝突情報を受信
	//--------------------------------------------------------------------
	public virtual void send_data(ref Collision_Data data) {
		if (ai != null)
			ai.send_collision_data(ref data);
	}
}