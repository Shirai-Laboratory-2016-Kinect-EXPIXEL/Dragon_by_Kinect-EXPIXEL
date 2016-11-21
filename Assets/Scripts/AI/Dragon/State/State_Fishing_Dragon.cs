﻿//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
//========================================================================
// ■ State_Fishing_Dragon
//------------------------------------------------------------------------
//	ドラゴンの釣り状態クラス。
//========================================================================

public class State_Fishing_Dragon : State_Base_Dragon {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	Transform sea;
	bool is_setup;
	bool is_sinking;
	Vector3 default_position;
	Quaternion default_rotation;
	float error_second = 60;
	Game_Manager game_manager;
	List<Coroutine> coroutines;
	bool is_error;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	public override void initialize() {
		base.initialize();

		sea = GameObject.FindWithTag("Water").transform;
		default_position = ai.transform.position;
		default_rotation = ai.transform.rotation;

		game_manager = GameObject.FindWithTag("Scene_Manager")
			.GetComponent<Game_Manager>();
		error_second += Time.time;

		coroutines = new List<Coroutine>();
		coroutines.Add( ai.StartCoroutine( turn() ) );
	}
	IEnumerator turn() {
		ai.animator.SetBool("Is_Move", true);
		ai.animator.SetFloat("Move_Z", -1);
		target_position = choice_near_position( search_enemies() );

		while (true) {
			face_target();
			var delta = target_position - ai.transform.position;
			if ( Vector3.Angle(ai.transform.forward, delta) < 5)
				break;
			yield return null;
		}

		ai.animator.SetFloat("Move_Z", 0);
		ai.animator.SetBool("Is_Move", false);
		yield return new WaitForSeconds(1);
		coroutines.Add( ai.StartCoroutine( jump() ) );
	}
	IEnumerator jump() {
		ai.animator.SetBool("Is_Swim", true);
		var r = ai.transform.rotation * Quaternion.Euler(-45, 0, 0);
		ai.rigidbody.AddForce(
			r * Vector3.forward * 2000,
			ForceMode.Impulse);

		while (true) {
			if (ai.transform.position.y < sea.position.y - 1) {
				ai.animator.SetBool("Is_Under_Water", true);
				break;
			}
			yield return null;
		}
		
		ai.rigidbody.useGravity = false;
		is_setup = true;
		coroutines.Add( ai.StartCoroutine( tracking() ) );
	}
	IEnumerator tracking() {
		ai.animator.SetFloat("Move_Z", 1);
		while (true) {
			target_position = choice_near_position( search_enemies() );
			tracking_target();
			if (is_sinking)	break;
			yield return null;
		}
		coroutines.Add( ai.StartCoroutine( back_home() ) );
	}
	IEnumerator back_home() {
		while (true) {
			target_position = default_position;
			tracking_target();
			var delta =
				Vector3.Distance(ai.transform.position, default_position);
			if (delta < 17)	break;
			yield return null;
		}
		forcing_stop();
		yield return new WaitForSeconds(1);
		coroutines.Add( ai.StartCoroutine( back_jump() ) );
	}
	IEnumerator back_jump() {
		var pos = ai.transform.position;
		pos.y = sea.position.y - 1;
		ai.transform.position = pos;
		ai.animator.SetFloat("Move_Z", 0);
		ai.animator.SetBool("Is_Swim", false);
		var r = ai.transform.rotation * Quaternion.Euler(-45, 0, 0);
		ai.rigidbody.AddForce(
			r * Vector3.forward * 2000,
			ForceMode.Impulse);
		ai.rigidbody.useGravity = true;

		while (true) {
			if (ai.transform.position.y > sea.position.y - 1) {
				ai.animator.SetBool("Is_Under_Water", false);
				break;
			}
			yield return null;
		}
		coroutines.Add( ai.StartCoroutine( landing_home() ) );
	}
	IEnumerator landing_home() {
		while (true) {
			RaycastHit hit_info;
			var is_hit = Physics.Raycast(
				ai.transform.position, -ai.transform.up, out hit_info, 1);
			if (is_hit && hit_info.transform.tag == "Home")
				break;
			yield return null;
		}
		coroutines.Add( ai.StartCoroutine( correct_home() ) );
	}
	IEnumerator correct_home() {
		forcing_stop();
		var temp = ai.max_move_speed;
		ai.max_move_speed = 2;
		ai.animator.SetBool("Is_Move", true);
		ai.animator.SetFloat("Move_Z", 1);
		while (true) {
			target_position = default_position;
			tracking_target();
			var delta =
				Vector3.Distance(ai.transform.position, target_position);
			if (delta < 1)
				break;
			yield return null;
		}
		ai.max_move_speed = temp;
		coroutines.Add( ai.StartCoroutine( turn_home() ) );
	}
	IEnumerator turn_home() {
		forcing_stop();
		ai.animator.SetFloat("Move_Z", -1);
		target_position = Camera.main.transform.position;
		target_position.y = ai.transform.position.y;

		while (true) {
			face_target();
			var delta = target_position - ai.transform.position;
			if ( Vector3.Angle(ai.transform.forward, delta) < 1)
				break;
			yield return null;
		}

		ai.animator.SetFloat("Move_Z", 0);
		ai.animator.SetBool("Is_Move", false);

		ai.fsm.change( new State_Result_Dragon() );
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	public override void update() {
		base.update();

		
		if (error_second < Time.time)
			is_error = true;
		Debug_EX.add( "コルーチン停止 : " + (error_second - Time.time) );

		if (is_error) {
			stop_all_coroutines();
			game_manager.next_scene_name =
				SceneManager.GetActiveScene().name;
		}


		if (!is_setup)	return;

		if ( ai.animator.GetBool("Is_Swim") &&
			ai.animator.GetBool("Is_Under_Water") ) {
			var pos = ai.transform.position;
			pos.y = Mathf.Min(pos.y, sea.position.y - 1.5f);
			ai.transform.position = pos;
		}
	}
	//--------------------------------------------------------------------
	// ● 更新（遅）
	//--------------------------------------------------------------------
	public override void late_update() {
		base.late_update();
	}
	//--------------------------------------------------------------------
	// ● コルーチンの全停止
	//--------------------------------------------------------------------
	void stop_all_coroutines() {
		foreach (var c in coroutines) {
			if (c != null)	ai.StopCoroutine(c);
		}
	}
	//--------------------------------------------------------------------
	// ● 終了
	//--------------------------------------------------------------------
	public override void finalize() {
		base.finalize();
		
		stop_all_coroutines();

		ai.transform.position = default_position;
		ai.transform.rotation = default_rotation;
		ai.rigidbody.useGravity = true;
		ai.animator.SetBool("Is_Swim", false);
		ai.animator.SetBool("Is_Move", false);
		ai.animator.SetFloat("Move_X", 0);
		ai.animator.SetFloat("Move_Z", 0);
	}
	//--------------------------------------------------------------------
	// ● 衝突情報を受信
	//--------------------------------------------------------------------
	public override void receive_collision_data(ref Collision_Data data) {
		base.receive_collision_data(ref data);

		if (data.target.name == "Terrain")
			is_error = true;

		if (is_sinking)	return;
		
		var e = data.target.root.GetComponent<Enemy>();
		if ( e != null && !e.fsm.is_death() ) {
			e.status.death();

			var probability = Random.value + ai.status.mood / 100;
			ai.status.item = Data_Manager.item.get(
				probability < 0.8 ?
				"Demise of The Humanity" : "プロフィール");
			is_sinking = true;
		}
	}
}