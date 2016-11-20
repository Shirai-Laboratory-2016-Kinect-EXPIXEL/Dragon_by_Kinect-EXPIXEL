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
		is_human_exists = !Input.GetKey(KeyCode.Space);
//		if ( Input.GetKeyDown(KeyCode.Space) )
//			is_human_exists = !is_human_exists;


		is_start_fishing = Input.GetMouseButton(0);


		hand_position += new Vector2(
			Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") )
			* 1000 * Time.deltaTime;
//		var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//		hand_position = new Vector2(pos.x, pos.y);
		var temp = hand_position;
		temp.x = Mathf.Clamp(hand_position.x, 0, 1920);
		temp.y = Mathf.Clamp(hand_position.y, -1080, 0);
		hand_position = temp;
	}
}