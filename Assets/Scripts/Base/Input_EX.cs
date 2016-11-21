//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
//========================================================================
// ■ Input_EX
//------------------------------------------------------------------------
//	入力の拡張クラス。
//========================================================================

public class Input_EX : Mono_Behaviour_EX {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	public static bool is_human_exists { get; private set; }
	public static bool is_start_fishing { get; private set; }
	public static Vector2 hand_position { get; private set; }
	float fishing_accumulate_second;
	Vector3 debug_position;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	void Start() {
		hand_position = new Vector2(1920 / 2, -1080 + 200);
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	void Update() {
		is_human_exists = false;
		is_start_fishing = false;
		

		update_kinect();
		

		is_human_exists = is_human_exists || Input.GetKey(KeyCode.Space);
		is_start_fishing = is_start_fishing || Input.GetMouseButton(0);


		hand_position += new Vector2(
			Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") )
			* 1000 * Time.deltaTime;
		var temp = hand_position;
		temp.x = Mathf.Clamp(hand_position.x, 0, 1920);
		temp.y = Mathf.Clamp(hand_position.y, -1080, 0);
		hand_position = temp;
	}
	//--------------------------------------------------------------------
	// ● 更新（キネクト）
	//--------------------------------------------------------------------
	void update_kinect() {
		var kinect = KinectManager.Instance;
		if (kinect == null)	return;
		
		var id = kinect.GetPlayer1ID();
		if (id <= 0)	return;

		var main_position = kinect.GetUserPosition(id);
		var hip = 0;
		var right_hand = 11;
			

		// 人が存在するかを判定
		is_human_exists = kinect.IsJointTracked(id, hip);


		if ( kinect.IsJointTracked(id, right_hand) ) {
			var joint_position = kinect.GetJointPosition(id, right_hand);
			joint_position -= main_position;
			var joint_rotation =
				kinect.GetJointOrientation(id, right_hand, true);
//			transform.localPosition = joint_position;
//			transform.rotation = joint_rotation;


			// 右手の位置を設定
			var y = 1 - Mathf.Clamp01(joint_position.y);
			hand_position = new Vector2(
				Mathf.Clamp01(joint_position.x + 0.5f) * 1920,
				y * -1080);
			

			// 釣り開始を判定
			var z = Mathf.Clamp01(joint_position.z * 2 * -1);
			if (y > 0.4 && z <= 0)
				fishing_accumulate_second = Time.time + 1;
			if (fishing_accumulate_second > Time.time && z >= 0.5)
				is_start_fishing = true;


			debug_position = joint_position;
			debug_position.z = z;
			Debug_EX.add(Color.yellow);
			Debug_EX.add("右手の位置 : " + joint_position);
			Debug_EX.add(Color.white);
		}
	}
	//--------------------------------------------------------------------
	// ● ＧＵＩ描画
	//--------------------------------------------------------------------
	void OnGUI() {
		if (!Debug_EX.is_view)	return;

		var font_size = 60;
		var rect = new Rect(10, 500, 600, font_size + 5);
		var gui_style = new GUIStyle();
		gui_style.normal.textColor = Color.yellow;
		gui_style.fontSize = font_size;
		GUI.Label(rect, ("右手の位置 : " + debug_position), gui_style);
	}
}