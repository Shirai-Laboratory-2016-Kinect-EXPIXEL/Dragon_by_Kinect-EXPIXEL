//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
//========================================================================
// ■ Directional_Lights
//------------------------------------------------------------------------
//	平行光源達のクラス。
//========================================================================

public class Directional_Lights : Mono_Behaviour_EX {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	public float start_day_angle = -75;
	public GameObject sun;
	public GameObject moon;
	World_Time world_time;
	Quaternion default_rotation;
	Color day_ambient_color;
	Color night_ambient_color;
	Color day_fog_color;
	Color night_fog_color;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	void Start() {
		world_time = GameObject.FindWithTag("Scene_Manager")
			.GetComponent<World_Time>();

		var temp = transform.eulerAngles;
		temp.x = 0;
		default_rotation = Quaternion.Euler(temp);

		day_ambient_color = RenderSettings.ambientSkyColor;
		night_ambient_color = day_ambient_color * 0.1f;
		day_fog_color = RenderSettings.fogColor;
		night_fog_color = day_fog_color * 0.1f;
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	void Update() {
		transform.rotation = Quaternion.AngleAxis(
			start_day_angle + world_time.day * 360, Vector3.right)
			* default_rotation;

		var hour = world_time.day * 24;
		sun.SetActive(5 < hour && hour < 19);
		moon.SetActive(17 < hour || hour < 7);
		
		
		// 地形上方向と太陽光方向の角度差を計算
		var delta_angle = Vector3.Angle(Vector3.up, transform.forward);
		// 太陽光の角度比率を計算
		var rate = (delta_angle - 60) / 120;
		rate = Mathf.Clamp01(rate);

		// 角度比率から、環境光を設定
		RenderSettings.ambientSkyColor =
			Color.Lerp(night_ambient_color, day_ambient_color, rate);
		// 角度比率から、フォグ色を設定
		RenderSettings.fogColor =
			Color.Lerp(night_fog_color, day_fog_color, rate);
	}
}