using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public GameObject panelGameOver;				//ゲームオーバーテキスト
	public GameObject buttons;					//操作ボタン
    public int meteorHP = 2;

    //定数定義
    private const int MAX_ORB = 10;

    //オブジェクト参照
    public GameObject orbPrefab;
    public GameObject canvasGame;
    public GameObject textScore;
	public GameObject TextHighScore;
    public GameObject player;
    public GameObject gameoverScore;
    public GameObject gameoverHighScore;

    //設定値
    public Vector3 createPosition = new Vector3(0f, 800.0f, 0f);





	//高さ変えたい検討
	/*
	float height = GameObject.


	
	var sr = object.GetComponent<SpriteRenderer>();
	var width = sr.bounds.size.x;


	float height = gameObject.GetComponent<Renderer>().bounds.size.y;
	print ("height: " + height);


	float height = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
	print ("height: " + height);


	Vector3 tmp = GameObject.Find("hogehoge").transform.position;
	GameObject.Find("hogehoge").transform.position = new Vector3(tmp.x + 100, tmp.y, tmp.z);

	float x = tmp.x;
	float y = tmp.y;
	float z = tmp.z;
	*/



    //メンバ変数
    private int score = 0;
    private int nextScore = 100;
    private int highScore = 0;

    void Awake()
    {
        Application.targetFrameRate = 60;
    }

	// Use this for initialization
	void Start () {
        //初期オーブ生成
        CreateOrb();
        RefreshScoreText();
        //ハイスコア読み込み
        highScore = PlayerPrefs.GetInt("HighScore", 0);
		RefreshHighScoreText();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // オーブ生成
    public void CreateOrb () {
        GameObject orb = (GameObject)Instantiate(orbPrefab);        
        // HPの設定
        orb.GetComponent<OrbManager>().SetHP(meteorHP);
        //playerの位置を基準に上に移動する
        Vector3 pos = new Vector3(createPosition.x,
                                  createPosition.y + player.transform.localPosition.y,
                                  createPosition.z);
        //ポジション設定
        orb.transform.localPosition = pos;
        meteorHP += 1;
    }

    //オーブ入手
    public void GetOrb (){
        score += 1;
        RefreshScoreText();
    }

	public void LoadTitle() {
		SceneManager.LoadScene ("TitleScene");
	}

	public void InLoadTitle() {
		Invoke ("LoadTitle", 0.1f);
	}

	public void GameOver(){
        //ハイスコア更新
        if( score > highScore ){
            highScore = score;
            PlayerPrefs.SetInt("HighScore",highScore);
            PlayerPrefs.Save();
            RefreshHighScoreText();
        }

        panelGameOver.SetActive (true);
		buttons.SetActive (false);
        textScore.SetActive(false);
        TextHighScore.SetActive(false);
        //ゲームオーバー画面のスコア
        gameoverScore.GetComponent<Text>().text = score.ToString();
        gameoverHighScore.GetComponent<Text>().text = "Best\n" + highScore;
	}

    //スコアてきすと更新
    void RefreshScoreText (){
        textScore.GetComponent<Text>().text = score.ToString();
    }

	void RefreshHighScoreText(){
		TextHighScore.GetComponent<Text>().text =
            "BEST " + highScore;
	}

    public void Restart() {
        // 現在のScene名を取得する
        Scene loadScene = SceneManager.GetActiveScene();
        // Sceneの読み直し
        SceneManager.LoadScene(loadScene.name);
    }

	public void InRestart(){
		Invoke ("Restart", 0.1f);
	}

}
