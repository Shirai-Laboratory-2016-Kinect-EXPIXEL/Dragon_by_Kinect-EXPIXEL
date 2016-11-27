//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//========================================================================
// ■ Smartphone
//------------------------------------------------------------------------
//	スマートフォンのクラス。
//========================================================================

public class Smartphone : MonoBehaviour {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	new Rigidbody rigidbody;
	Dictionary<string, AudioSource> se_put;
	Dragon dragon;
	bool is_relinquish;
	bool is_destroy;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	void Start() {
		rigidbody = GetComponent<Rigidbody>();
		rigidbody.isKinematic = true;

		se_put = new Dictionary<string, AudioSource>();
		foreach ( var a in GetComponentsInChildren<AudioSource>() )
			se_put[a.name] = a;

		dragon = GameObject.FindWithTag("Dragon").GetComponent<Dragon>();
	}
	//--------------------------------------------------------------------
	// ● 更新（遅）
	//--------------------------------------------------------------------
	void LateUpdate() {
		if (!is_relinquish) {
			transform.position = dragon.point_smartphone.position;
			transform.rotation = dragon.point_smartphone.rotation;
		}

		if ( !(dragon.fsm.state() is State_Fishing_Dragon) &&
			!(dragon.fsm.state() is State_Result_Dragon) &&
			!is_destroy)
		{
			se_put["SE_Put"].Play();
			is_relinquish = true;
			rigidbody.isKinematic = false;
			Destroy(gameObject, 3);
			is_destroy = true;
		}
	}
	//--------------------------------------------------------------------
	// ● 離す
	//--------------------------------------------------------------------
	public void relinquish() {
		se_put["SE_Shine"].Play();
	}
	//--------------------------------------------------------------------
	// ● 新たに物理衝突したコールバック関数
	//--------------------------------------------------------------------
	void OnCollisionEnter(Collision collision) {
		se_put["SE_Put"].Play();
	}
}