//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
//========================================================================
// ■ Enemy
//------------------------------------------------------------------------
//	敵のクラス。
//========================================================================

public class Enemy : AI {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	public GameObject explosion;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	protected override void Start() {
		base.Start();

		enemy_tag = "Dragon";

		fsm.set_death( new State_Death_Enemy() );
		fsm.change( new State_Loitering_Enemy() );

//		ses["Engine"].volume = 0.5f;
//		ses["Engine"].Play();
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	protected override void Update() {
		base.Update();
	}
	//--------------------------------------------------------------------
	// ● 衝突情報を送る
	//--------------------------------------------------------------------
	public override void send_collision_data(ref Collision_Data data) {
		base.send_collision_data(ref data);
	}
	//--------------------------------------------------------------------
	// ● エディタ描画
	//--------------------------------------------------------------------
	void OnDrawGizmosSelected() {
		// ４終点を設定
		var positions = new Vector3[] {
			Quaternion.Euler(-45, -45, 0) * Vector3.forward,
			Quaternion.Euler(45, -45, 0) * Vector3.forward,
			Quaternion.Euler(45, 45, 0) * Vector3.forward,
			Quaternion.Euler(-45, 45, 0) * Vector3.forward,
		};
		for (var i = 0; i < positions.Length; i++) {
			var angle = Quaternion.Euler(transform.eulerAngles +
				positions[i] * sight_angle);
			positions[i] = transform.position +
				angle * Vector3.forward * sight_distance;
		}

		// ４終点を使用し、カメラ状に描画
		Gizmos.color = Color.red;
		for (var i = 0; i < positions.Length; i++) {
			Gizmos.DrawLine(transform.position, positions[i]);
			var next_i = (int)Mathf.Repeat(i + 1, positions.Length);
			Gizmos.DrawLine(positions[i], positions[next_i]);
		}

//		Gizmos.DrawFrustum(
//			transform.position,
//			sight_angle * 2, sight_distance, 0, 1);
	}
}