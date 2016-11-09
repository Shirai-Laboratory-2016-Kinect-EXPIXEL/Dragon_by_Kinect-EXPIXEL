//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;
//========================================================================
// ■ Main_Camera
//------------------------------------------------------------------------
//	メインカメラのクラス。
//========================================================================

public class Main_Camera : Entity {
	//--------------------------------------------------------------------
	// ● 定数
	//--------------------------------------------------------------------
	// 初期状態
	public enum Start_State {
		TPS,
		SHIP,
	}
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	public float distance = 0.9f;						// カメラ距離
	public float ship_distance = 5;						// 船のカメラ距離
	public float rotation_speed = 100;					// 回転速度
	public float slerp_speed = 10;						// 回転補間速度
	public Vector3 offset = new Vector3(0.47f, 1.33f, 0);	// 中心加算位置
	public Vector3 ship_offset = new Vector3(0, 1, -2); // 船の中心加算位置
	VignetteAndChromaticAberration camera_chromatic;
	ColorCorrectionCurves camera_color;
	[HideInInspector] public Transform player;			// プレイヤー
	[HideInInspector] public Player_Status player_status;
	[HideInInspector] public Transform ship;			// 船
	[HideInInspector] public Transform look_target;		// 向く対象
	[HideInInspector] public Vector3 euler_angles;		// 回転角度ベクトル
	public Start_State start_state = Start_State.TPS;	// 初期状態
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	protected override void Start() {
		base.Start();


		camera_chromatic =
			Camera.main.GetComponent<VignetteAndChromaticAberration>();
		camera_color =
			Camera.main.GetComponent<ColorCorrectionCurves>();

		// 各種ゲームオブジェクトを代入
		player = GameObject.FindWithTag("Player").transform;
		player_status = player.GetComponent<Player_Status>();
		ship = GameObject.FindWithTag("Ship").transform;
		
		// 初期角度を保存
		euler_angles = transform.eulerAngles;


		// 状態を設定
		fsm.set_death( new State_Death_Camera() );
		switch (start_state) {
			// ＴＰＳ視点状態に遷移
			case Start_State.TPS:
				fsm.change( new State_TPS_Camera() );	break;
			// 船視点状態に遷移
			case Start_State.SHIP:
				fsm.change( new State_Ship_Camera() );	break;
		}
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	protected override void Update() {
		base.Update();
		
		camera_chromatic.intensity =
			0.4f * (1 - player_status.strength_rate);
		camera_chromatic.blur = 1 - player_status.strength_rate;
		camera_color.saturation =
			Mathf.Max(player_status.strength_rate, 0.2f);
	}
	//--------------------------------------------------------------------
	// ● 更新（遅）
	//--------------------------------------------------------------------
	protected override void LateUpdate() {
		base.LateUpdate();
	}
	//--------------------------------------------------------------------
	// ● 衝突情報を受信
	//--------------------------------------------------------------------
	public override void send_collision_data(ref Collision_Data data) {
		base.send_collision_data(ref data);
	}
}