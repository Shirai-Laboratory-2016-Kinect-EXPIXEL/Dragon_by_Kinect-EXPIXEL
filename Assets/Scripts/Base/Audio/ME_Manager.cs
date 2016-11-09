﻿//========================================================================
// ■ ME_Manager
//------------------------------------------------------------------------
//	ME 管理クラス。
//========================================================================

public class ME_Manager : Base_Audio_Manager {
	//--------------------------------------------------------------------
	// ● 再生中か？
	//--------------------------------------------------------------------
	public bool is_playing() {
		return source.isPlaying;
	}
	//--------------------------------------------------------------------
	// ● コンストラクタ
	//--------------------------------------------------------------------
	public ME_Manager() {
		path += "ME/";			// フォルダ階層を設定
	}
	//--------------------------------------------------------------------
	// ● 再生
	//--------------------------------------------------------------------
	override public void play(string name, string sub_path = "") {
		base.play(name, sub_path);			// 基盤クラスを再生
		source.clip = audios[name].clip;	// 発生源に登録
		source.Play();						// 再生
	}
}