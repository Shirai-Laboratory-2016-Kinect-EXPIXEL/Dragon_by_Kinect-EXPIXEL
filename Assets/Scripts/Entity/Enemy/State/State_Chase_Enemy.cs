//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
//========================================================================
// ■ State_Chase_Enemy
//------------------------------------------------------------------------
//	敵の追跡状態クラス。
//========================================================================

public class State_Chase_Enemy : State_Base_Enemy {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	float next_gun_second;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	public override void initialize() {
		base.initialize();

		ai.ses["Engine"].volume = 1;

		Audio_Manager.se.set_volume(0.8f);
//		Audio_Manager.se.play("notice");
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	public override void update() {
		base.update();

		var positions = search_enemies();
		var pos = choice_near_position(positions);
		if (pos != Vector3.zero)	target_position = pos;
		tracking_target();
		

		update_gun();
		

		// 見失っている場合、見失い状態に遷移
		Vector3 position;
		if ( !is_sighting( search_enemies(), out position) ) {
			var delta = target_position - ai.transform.position;

			if (delta.magnitude < 1)
				ai.fsm.change( new State_Lose_Enemy() );

			losing_second += Time.deltaTime;
			if (losing_second > 10)
				ai.fsm.change( new State_Lose_Enemy() );
		} else
			losing_second = 0;
	}
	//--------------------------------------------------------------------
	// ● 更新（銃）
	//--------------------------------------------------------------------
	void update_gun() {
		var s = (Enemy_Status)ai.status;

		if (next_gun_second < Time.time && s.bullets > 0) {
			var delta = target_position - ai.transform.position;
			if (delta.magnitude < 10 &&
				Vector3.Angle(ai.transform.forward, delta) < 10)
			{
				Object.Instantiate(
					ai.bullet, ai.gun.position, ai.gun.rotation);
				ai.ses["Shoot"].Play();
				s.bullets--;
				next_gun_second = Time.time + 0.5f;
			}
		}
	}
	//--------------------------------------------------------------------
	// ● 更新（遅）
	//--------------------------------------------------------------------
	public override void late_update() {
		base.late_update();
	}
	//--------------------------------------------------------------------
	// ● 終了
	//--------------------------------------------------------------------
	public override void finalize() {
		base.finalize();

		ai.ses["Engine"].volume = 0.5f;
	}
	//--------------------------------------------------------------------
	// ● 衝突情報を受信
	//--------------------------------------------------------------------
	public override void receive_collision_data(ref Collision_Data data) {
		base.receive_collision_data(ref data);
	}
}