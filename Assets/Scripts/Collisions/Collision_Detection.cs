//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
//========================================================================
// ■ Collision_Detection
//------------------------------------------------------------------------
//	衝突判定のクラス。
//========================================================================

public class Collision_Detection : Mono_Behaviour_EX {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	public Collision_Manager manager;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	void Start() {
		if (manager == null)
			manager = transform.root.GetComponent<Collision_Manager>();
	}
	//--------------------------------------------------------------------
	// ● 新たに物理衝突したコールバック関数
	//--------------------------------------------------------------------
	void OnCollisionEnter(Collision collision) {
		send_data( new Collision_Data(
			transform, collision.transform, collision.contacts[0].point,
			Collision_Data.Type.ENTER, false) );
//		add_all(collision.transform);
//		manager.collisions[collision] = Time.deltaTime;
	}
	//--------------------------------------------------------------------
	// ● 物理衝突中のコールバック関数
	//--------------------------------------------------------------------
	void OnCollisionStay(Collision collision) {
		send_data( new Collision_Data(
			transform, collision.transform, collision.contacts[0].point,
			Collision_Data.Type.STAY, false) );
//		add_all(collision.transform);
//		manager.collisions[collision] = Time.deltaTime;
	}
	//--------------------------------------------------------------------
	// ● 物理衝突から離れたコールバック関数
	//--------------------------------------------------------------------
	void OnCollisionExit(Collision collision) {
		Vector3 position = collision.contacts.Length > 0 ?
			collision.contacts[0].point : collision.transform.position;

		send_data( new Collision_Data(
			transform, collision.transform, position,
			Collision_Data.Type.EXIT, false) );
//		delete_all(collision.transform);
//		manager.collisions.Remove(collision);
	}
	//--------------------------------------------------------------------
	// ● 新たに範囲衝突したコールバック関数
	//--------------------------------------------------------------------
	void OnTriggerEnter(Collider collider) {
		send_data( new Collision_Data(
			transform, collider.transform, collider.transform.position,
			Collision_Data.Type.ENTER, true) );
//		add_all(collider.transform);
//		manager.triggers[collider] = Time.deltaTime;
	}
	//--------------------------------------------------------------------
	// ● 範囲衝突中のコールバック関数
	//--------------------------------------------------------------------
	void OnTriggerStay(Collider collider) {
		send_data( new Collision_Data(
			transform, collider.transform, collider.transform.position,
			Collision_Data.Type.STAY, true) );
//		add_all(collider.transform);
//		manager.triggers[collider] = Time.deltaTime;
	}
	//--------------------------------------------------------------------
	// ● 範囲衝突から離れたコールバック関数
	//--------------------------------------------------------------------
	void OnTriggerExit(Collider collider) {
		send_data( new Collision_Data(
			transform, collider.transform, collider.transform.position,
			Collision_Data.Type.EXIT, true) );
//		delete_all(collider.transform);
//		manager.triggers.Remove(collider);
	}
	//--------------------------------------------------------------------
	// ● 全衝突一覧に追加
	//--------------------------------------------------------------------
	void add_all(Transform target) {
		if (target.root != transform.root) {
			ArrayList array_list = manager.all.ContainsKey(name) ?
				manager.all[name] : new ArrayList();
			if ( !array_list.Contains(target) ) {
				array_list.Add(target);
				manager.all[name] = array_list;
			}
		}
	}
	//--------------------------------------------------------------------
	// ● 全衝突一覧から削除
	//--------------------------------------------------------------------
	void delete_all(Transform target) {
		if ( manager.all.ContainsKey(name) ) {
			ArrayList array_list = manager.all[name];
			array_list.Remove(target);
			if (array_list.Count == 0)
				manager.all.Remove(name);
		}
	}
	//--------------------------------------------------------------------
	// ● 衝突情報を送信
	//--------------------------------------------------------------------
	void send_data(Collision_Data data) {
		if (data.is_not_self() && manager != null)
			manager.send_data(ref data);
	}
}