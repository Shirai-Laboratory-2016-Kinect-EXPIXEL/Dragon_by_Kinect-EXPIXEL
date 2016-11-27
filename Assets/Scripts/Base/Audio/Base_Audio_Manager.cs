//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections.Generic;
//========================================================================
// ■ Base_Audio_Manager
//------------------------------------------------------------------------
//	音管理の基盤クラス。
//========================================================================

public class Base_Audio_Manager {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	protected Dictionary<string, Audio> audios;	// キャッシュ
	protected AudioSource source;				// 音の発生源
	protected string path = "Audio/";			// フォルダ階層
	//--------------------------------------------------------------------
	// ● 再生中の音を取得
	//--------------------------------------------------------------------
	public Audio get_playing_audio() {
		return audios["present"];
	}
	//--------------------------------------------------------------------
	// ● 読み込まれているか？
	//--------------------------------------------------------------------
	public bool is_loading(ref string name) {
		// 内部データが設定されているか
		return audios.ContainsKey(name) && audios[name].clip;
	}
	//--------------------------------------------------------------------
	// ● セッター
	//--------------------------------------------------------------------
	// 音量を設定
	public void set_volume(float volume) {
		source.volume = volume;
	}
	//--------------------------------------------------------------------
	// ● コンストラクタ
	//--------------------------------------------------------------------
	protected Base_Audio_Manager() {
		audios = new Dictionary<string, Audio>();	// キャッシュを作成
		audios["present"] = null;

		// 音の発生源を作成
		source = Audio_Manager.speaker.AddComponent<AudioSource>();
	}
	//--------------------------------------------------------------------
	// ● 読み込み
	//--------------------------------------------------------------------
	public void load(string name, string sub_path = "",
						string file_name = null) {
		// ファイル名が指定されていない場合、識別名と兼用する
		if (file_name == null)	file_name = name;
		file_name = path + sub_path + file_name;	// 階層を合成

		// 読み込まれていない場合、読み込む
		if ( !audios.ContainsKey(name) )
			audios[name] = new Audio(ref name, ref file_name);
	}
	//--------------------------------------------------------------------
	// ● 再生
	//--------------------------------------------------------------------
	virtual public void play(string name, string sub_path = "") {
		// 読み込まれていない場合、読み込む
		if ( !audios.ContainsKey(name) )	load(name, sub_path);

		audios["present"] = audios[name];	// 現在再生音に登録
	}
	//--------------------------------------------------------------------
	// ● 消去
	//--------------------------------------------------------------------
	public void clear() {
		// 音データを全て解放
		foreach (KeyValuePair<string, Audio> pair in audios)
			pair.Value.release();

		audios.Clear();		// キャッシュを消去
		source.clip = null;	// 発生源を消去
	}
}