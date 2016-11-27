//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
//========================================================================
// ■ Dragon
//------------------------------------------------------------------------
//	ドラゴンのクラス。
//========================================================================

public class Dragon : AI {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	public GameObject smartphone;
	public Transform point_smartphone;
	public GameObject water_spray;
	public GameObject water_hose;
	[HideInInspector] public float idle_type;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	protected override void Start() {
		base.Start();
		

		enemy_tag = "Enemy";
		
		fsm.set_death( new State_Death_Dragon() );
//		fsm.change( new State_Controll_Dragon() );
		fsm.change( new State_Contact_Dragon() );
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	protected override void Update() {
		base.Update();
	}
	//--------------------------------------------------------------------
	// ● 待機型を変更
	//--------------------------------------------------------------------
	public void change_idle_type() {
		idle_type = (int)Mathf.Repeat(idle_type + 1, 4);
	}
	//--------------------------------------------------------------------
	// ● 更新（遅）
	//--------------------------------------------------------------------
	protected override void LateUpdate() {
		base.LateUpdate();
		

		// 死亡＆復活テスト
		if ( Input.GetKeyDown(KeyCode.P) ) {
			if ( !fsm.is_death() )
				status.death();
			else {
				status.recovery();
				fsm.change( new State_Controll_Dragon() );
			}
		}
	}
	//--------------------------------------------------------------------
	// ● 衝突情報を受信
	//--------------------------------------------------------------------
	public override void send_collision_data(ref Collision_Data data) {
		base.send_collision_data(ref data);
	}
	//--------------------------------------------------------------------
	// ● ＳＥを再生
	//--------------------------------------------------------------------
	public void se_play(string name) {
		if (fsm.state() is State_Result_Dragon)
			name = "SE_Want_Praise";
		ses[name].Play();
	}
	//--------------------------------------------------------------------
	// ● エディタ描画
	//--------------------------------------------------------------------
	void OnDrawGizmosSelected() {
	}
}