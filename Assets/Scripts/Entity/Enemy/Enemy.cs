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

public class Enemy : Entity {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	[HideInInspector] public Transform player;
	World_Time world_time;
	public new GameObject light;
	public Transform gun;
	public GameObject bullet;
	public GameObject explosion;
	public GameObject smoke;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	protected override void Start() {
		base.Start();

		enemy_tag = "Player";

		player = GameObject.FindWithTag("Player").transform;
		world_time = GameObject.FindWithTag("Scene_Manager")
			.GetComponent<World_Time>();

		fsm.set_death( new State_Death_Enemy() );
		fsm.change( new State_Loitering_Enemy() );

		ses["Engine"].volume = 0.5f;
		ses["Engine"].Play();
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	protected override void Update() {
		base.Update();

		if ( !fsm.is_death() ) {
			// 傾きを補正
			var temp = transform.position;
			temp.y = Mathf.Max(temp.y, sea.position.y + 1);
			transform.position = temp;

			var angle = transform.eulerAngles;
			if (angle.x > 180)	angle.x -= 360;
			angle.x = Mathf.Clamp(angle.x, -40, 40);
			transform.eulerAngles = angle;

			// 発光を設定
			var hour = world_time.day * 24;
			light.SetActive(18 < hour || hour < 6);
		} else
			light.SetActive(false);
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