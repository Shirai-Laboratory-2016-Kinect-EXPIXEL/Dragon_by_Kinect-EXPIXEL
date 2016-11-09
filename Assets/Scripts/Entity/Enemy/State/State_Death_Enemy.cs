//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
//========================================================================
// ■ State_Death_Enemy
//------------------------------------------------------------------------
//	敵の死亡状態クラス。
//========================================================================

public class State_Death_Enemy : State_Base_Enemy {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	Transform smoke;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	public override void initialize() {
		base.initialize();
		
		stop();
		ai.rigidbody.useGravity = true;

		var go = Object.Instantiate(ai.explosion);
		go.transform.position = ai.transform.position + Vector3.up * 0.5f;
		go.transform.rotation = ai.transform.rotation;

		smoke = Object.Instantiate(ai.smoke).transform;
		

		var rs = ai.GetComponentsInChildren<Renderer>();
		foreach (var r in rs) {
			r.material.SetColor("_Color", Color.white * 0.1f);
		}

		ai.ses["Engine"].Stop();
//		ai.animator.SetBool("Death", true);
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	public override void update() {
		base.update();
	}
	//--------------------------------------------------------------------
	// ● 更新（遅）
	//--------------------------------------------------------------------
	public override void late_update() {
		base.late_update();

		if (smoke != null) {
			smoke.position = ai.transform.position + Vector3.up * 0.5f;
			smoke.rotation = ai.transform.rotation;

			if (ai.transform.position.y < ai.sea.position.y) {
				Object.Destroy(smoke.gameObject);
			}
		}
	}
	//--------------------------------------------------------------------
	// ● 終了
	//--------------------------------------------------------------------
	public override void finalize() {
		base.finalize();

		ai.ses["Engine"].Play();
//		ai.animator.SetBool("Death", false);
	}
	//--------------------------------------------------------------------
	// ● 衝突情報を受信
	//--------------------------------------------------------------------
	public override void receive_collision_data(ref Collision_Data data) {
		base.receive_collision_data(ref data);
	}
}