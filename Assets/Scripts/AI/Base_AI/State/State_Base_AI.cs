//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
//========================================================================
// ■ State_Base_AI
//------------------------------------------------------------------------
//	人工知能の基盤状態クラス。
//========================================================================

public class State_Base_AI {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	public AI ai;	// ＡＩ本体
	public Vector3 target_position;
	protected float losing_second;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	public virtual void initialize() {
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	public virtual void update() {
	}
	//--------------------------------------------------------------------
	// ● 更新（ＩＫ）
	//--------------------------------------------------------------------
	public virtual void update_ik() {
	}
	//--------------------------------------------------------------------
	// ● 更新（遅）
	//--------------------------------------------------------------------
	public virtual void late_update() {
	}
	//--------------------------------------------------------------------
	// ● 次の状態を判定
	//--------------------------------------------------------------------
	public virtual void check_next_state() {
	}
	//--------------------------------------------------------------------
	// ● 終了
	//--------------------------------------------------------------------
	public virtual void finalize() {
	}
	//--------------------------------------------------------------------
	// ● 衝突情報を受信
	//--------------------------------------------------------------------
	public virtual void receive_collision_data(ref Collision_Data data) {
	}
	//--------------------------------------------------------------------
	// ● 敵を探索
	//--------------------------------------------------------------------
	protected List<Vector3> search_enemies() {
		List<Vector3> result = new List<Vector3>();
		
		var gos = GameObject.FindGameObjectsWithTag(ai.enemy_tag);
		foreach (var go in gos) {
			var e = go.GetComponent<AI>();
			if ( e == null) {
				result.Add(go.transform.position);
			} else if (!e.fsm.is_death() ) {
				result.Add(
					e.center != null ? e.center.position :
					go.transform.position);
			}
		}
		return result;
	}
	//--------------------------------------------------------------------
	// ● 最近位置を抽出
	//--------------------------------------------------------------------
	protected Vector3 choice_near_position(List<Vector3> positions) {
		Vector3 result = Vector3.zero;
		float min_distance = float.PositiveInfinity;

		foreach (var position in positions) {
			var distance =
				Vector3.Distance(position, ai.transform.position);
			if (distance > ai.sight_distance)	continue;

			if (min_distance > distance) {
				min_distance = distance;
				result = position;
			}
		}
		return result;
	}
	//--------------------------------------------------------------------
	// ● 発見しているか？
	//--------------------------------------------------------------------
	protected bool is_sighting(List<Vector3> positions,
								out Vector3 sight_position) {
		var position = choice_near_position(positions);
		if (position != Vector3.zero) {
			position.y += 1;
			var delta = position - ai.eye.position;
			var delta_distance = delta.magnitude;

			if (delta_distance < ai.sight_distance &&
				Vector3.Angle(ai.eye.forward, delta) < ai.sight_angle)
			{
				RaycastHit hit_info;
				var is_hit = Physics.Raycast(
					ai.eye.position,
					delta.normalized,
					out hit_info,
					Mathf.Min(delta_distance, ai.sight_distance),
					LayerMask.GetMask("Ground")
				);
				if (!is_hit) {
					sight_position = position;
					return true;
				}
			}
		}
		sight_position = Vector3.zero;
		return false;
	}
	//--------------------------------------------------------------------
	// ● 対象を向く
	//--------------------------------------------------------------------
	protected void face_target() {
		var delta = target_position - ai.transform.position;
		ai.transform.rotation = Quaternion.RotateTowards(
			Quaternion.LookRotation(ai.transform.forward),
			Quaternion.LookRotation(delta),
			ai.rotate_speed * Time.deltaTime
		);
	}
	//--------------------------------------------------------------------
	// ● 対象を追跡
	//--------------------------------------------------------------------
	protected void tracking_target() {
		face_target();
		ai.force_direction += ai.transform.forward;
	}
	//--------------------------------------------------------------------
	// ● 停止
	//--------------------------------------------------------------------
	public void stop() {
		ai.force_direction = Vector3.zero;
		ai.torque_direction = Vector3.zero;
	}
	//--------------------------------------------------------------------
	// ● 停止（強制）
	//--------------------------------------------------------------------
	public void forcing_stop() {
		stop();
		ai.rigidbody.velocity = Vector3.zero;
		ai.rigidbody.angularVelocity = Vector3.zero;
	}
}