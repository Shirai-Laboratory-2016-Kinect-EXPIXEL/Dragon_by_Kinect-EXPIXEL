//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//========================================================================
// ■ State_Machine
//------------------------------------------------------------------------
//	有限状態機械クラス。
//========================================================================

public class State_Machine {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	Entity ai;	// ＡＩ本体
	Dictionary<string, State_Base_Entity> states;	// 各種状態
	State_Base_Entity death_state;	// 死亡状態（確保のみ）
	//--------------------------------------------------------------------
	// ● コンストラクタ
	//--------------------------------------------------------------------
	public State_Machine(Entity ai, State_Base_Entity death_state) {
		this.ai = ai;
		this.death_state = death_state;

		states = new Dictionary<string, State_Base_Entity>();
		change( "Body", new State_Base_Entity() );
	}
	//--------------------------------------------------------------------
	// ● セッター
	//--------------------------------------------------------------------
	// 死亡状態を設定
	public void set_death(State_Base_Entity death_state) {
		this.death_state = death_state;
	}
	//--------------------------------------------------------------------
	// ● 状態を取得
	//--------------------------------------------------------------------
	public State_Base_Entity state(string region_name = "Body") {
		return states[region_name];
	}
	//--------------------------------------------------------------------
	// ● 状態を変更
	//--------------------------------------------------------------------
	// 部位を指定
	public void change(string region_name, State_Base_Entity state) {
		// 前回状態を終了
		if ( states.ContainsKey(region_name) )
			this.state(region_name).finalize();
		states[region_name] = state;			// 現在状態に適用
		this.state(region_name).ai = ai;		// 自身を登録
		this.state(region_name).initialize();	// 初期化
	}
	// 本体のみ
	public void change(State_Base_Entity state) {
		change("Body", state);
	}
	//--------------------------------------------------------------------
	// ● 死亡状態に変更
	//--------------------------------------------------------------------
	public void change_death() {
		if ( !is_death() )	change("Body", death_state);
	}
	//--------------------------------------------------------------------
	// ● 死亡したか？
	//--------------------------------------------------------------------
	public bool is_death() {
		return state("Body").GetType() == death_state.GetType();
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	public void update() {
		var list = new List<string>(states.Keys);
		foreach (var key in list)
			states[key].update();
	}
	//--------------------------------------------------------------------
	// ● 更新（ＩＫ）
	//--------------------------------------------------------------------
	public void update_ik() {
		var list = new List<string>(states.Keys);
		foreach (var key in list)
			states[key].update_ik();
	}
	//--------------------------------------------------------------------
	// ● 更新（遅）
	//--------------------------------------------------------------------
	public void late_update() {
		var list = new List<string>(states.Keys);
		foreach (var key in list)
			states[key].late_update();

		update_test();
	}
	//--------------------------------------------------------------------
	// ● 衝突情報を送る
	//--------------------------------------------------------------------
	public void send_collision_data(ref Collision_Data data) {
		var list = new List<string>(states.Keys);
		foreach (var key in list)
			states[key].receive_collision_data(ref data);
	}
	//--------------------------------------------------------------------
	// ● 更新（テスト）
	//--------------------------------------------------------------------
	void update_test() {
//		Debug_EX.new_line();
		Debug_EX.add(Color.yellow);
		var s = ai.status;
		Debug_EX.add(ai.name +
			(s != null ? (" HP : " + ai.status.strength) : "") );
		Debug_EX.add(Color.white);
		
		var list = new List<string>(states.Keys);
		foreach (var key in list)
			Debug_EX.add( key + "\t: " + states[key].ToString() );

//		Debug_EX.add("");
	}
}