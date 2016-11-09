//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections.Generic;
//========================================================================
// ■ Main_Camera
//------------------------------------------------------------------------
//	メインカメラのクラス。
//========================================================================

public class Backup_Main_Camera : Mono_Behaviour_EX {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	Transform player;
	Quaternion default_rotation;
	public float distance = 3;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	void Start() {
		player = GameObject.FindWithTag("Player")
			.GetComponent<Entity>().eye;
		
		default_rotation = transform.rotation;
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	void Update() {
	}
	//--------------------------------------------------------------------
	// ● 更新（遅）
	//--------------------------------------------------------------------
	void LateUpdate() {
		transform.rotation = Quaternion.Lerp(
			transform.rotation,
			player.rotation * default_rotation,
			10 * Time.deltaTime);
		transform.position = Vector3.Lerp(
			transform.position,
			player.position - transform.forward * distance,
			100 * Time.deltaTime);


		// カメラ間の障害物を検出
		RaycastHit hit_info;
		bool hit = Physics.Linecast(
			player.position,
			transform.position,
			out hit_info,
			LayerMask.GetMask("Ground")
		);
		// 障害物が存在する場合、障害物の手前位置に修正
		if (hit)
			transform.position =
				hit_info.point - hit_info.transform.forward;
	}
}