//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
//========================================================================
// ■ World_Time
//------------------------------------------------------------------------
//	世界時間のクラス。
//========================================================================

public class World_Time : Mono_Behaviour_EX {
	//--------------------------------------------------------------------
	// ● 定数
	//--------------------------------------------------------------------
	// 時間の秒数
	public static readonly float HOUR_TO_SECOND = 3600;
	// 最大世界秒
	public static readonly float MAX_SECOND = 24 * HOUR_TO_SECOND;
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	public float max_real_second = 2 * HOUR_TO_SECOND;
	public float second = 8 * HOUR_TO_SECOND;
	[HideInInspector] public float day;
	float move_second;
	float move_second_mult = 1;
	//--------------------------------------------------------------------
	// ● 時刻を設定
	//--------------------------------------------------------------------
	public void set_move_second_mult(float move_second_mult) {
		this.move_second_mult = move_second_mult;
	}
	//--------------------------------------------------------------------
	// ● 時刻を設定
	//--------------------------------------------------------------------
	public void set_hour(float hour) {
	}
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	void Start() {
		move_second = MAX_SECOND / max_real_second;

		day = second / MAX_SECOND;
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	void Update() {
		second += move_second * move_second_mult * Time.deltaTime;
		second = Mathf.Repeat(second, MAX_SECOND);

		day = second / MAX_SECOND;
		
/*
//		if ( Input.GetKeyDown(KeyCode.Return) )
//			set_hour(second / HOUR_TO_SECOND + 6);
		set_move_second_mult(Input.GetKey(KeyCode.Return) ? 50: 1);
*/
	}
}