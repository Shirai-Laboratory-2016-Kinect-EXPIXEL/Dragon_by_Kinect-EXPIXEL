//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//========================================================================
// ■ Enemy_Manager
//------------------------------------------------------------------------
//	敵の管理クラス。
//========================================================================

public class Enemy_Manager : Mono_Behaviour_EX {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	public List<Loitering_Points> loitering_points;
	public List<Transform> respawn_points;
	public GameObject enemy;
	public int max_enemy_count = 3;
	List<Enemy> enemies;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	void Start() {
		enemies = new List<Enemy>();
		var gos = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (var go in gos)
			enemies.Add( go.GetComponent<Enemy>() );
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	void Update() {
		enemies.RemoveAll(e => e == null);

		if (enemies.Count < max_enemy_count) {
			var e = Instantiate(enemy).GetComponent<Enemy>();
			e.transform.position =
				respawn_points[ Random.Range(0, respawn_points.Count) ].position;
			e.points =
				loitering_points[ Random.Range(0, loitering_points.Count) ];
			enemies.Add(e);
		}
	}
}