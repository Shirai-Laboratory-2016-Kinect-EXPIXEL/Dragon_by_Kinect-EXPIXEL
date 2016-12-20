//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
//========================================================================
// ■ News_Sentence_Manager
//------------------------------------------------------------------------
//	ニュース文章の総合データクラス。
//========================================================================

public class News_Sentence_Manager : Sentence_Manager {
	//--------------------------------------------------------------------
	// ● ゲッター
	//--------------------------------------------------------------------
	// サブフォルダ階層を取得
	override protected string get_sub_path() {
		return "News/";
	}
	//--------------------------------------------------------------------
	// ● コンストラクタ
	//--------------------------------------------------------------------
	public News_Sentence_Manager() : base() {
//		initialize_test();
	}
	//--------------------------------------------------------------------
	// ● 初期化（テスト）
	//--------------------------------------------------------------------
	protected void initialize_test() {
		// 文章を表示
		foreach (KeyValuePair< string, Dictionary<string, ArrayList> > pair
			in sentences) {
			foreach (KeyValuePair<string, ArrayList> p in pair.Value) {
				foreach (News_Sentence sentence in p.Value)
					sentence.print();
			}
		}
	}
	//--------------------------------------------------------------------
	// ● 文章クラスを追加
	//--------------------------------------------------------------------
	override protected void add_sentence(string file_name, string scene,
										ArrayList array_list) {
		sentences[file_name][scene].Add( new News_Sentence(array_list) );
	}
}