using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SaveManager
{
    #region 單例設計模式
    public static SaveManager instance
    {
        get // 當有人讀取我，當有人需要我、使用我
        {
            // 如果我不存在於記憶體中
            if (_instance == null)
            {
                // 那麼我就自己創造自己
                _instance = new SaveManager();
            }
            // 回傳給對方
            return _instance;
        }
    }
    static SaveManager _instance = null;
    #endregion

    /// <summary>體力</summary>
    public float ap
    {
        get // 當有人讀取體力值
        {
            return _ap;
        }
        set // 當有人寫入體力值
        {
            // 限制範圍在0~最大之間
            _ap = Mathf.Clamp(value, 0f, maxap);
            // 當人修改體力時通知所有訂閱我的用戶
            if (體力變化事件 != null)
            {
                體力變化事件.Invoke();
            }
        }
    }
    float _ap = 10f;
    public float maxap = 10f;
    public Action 體力變化事件 = null;

    /// <summary> 生命值 </summary>
    public float hp
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = Mathf.Clamp(value, 0f, maxhp);
            if (血量變化事件 != null)
            {
                血量變化事件.Invoke();
            }
            // 如果沒有血 而且 遊戲尚未結束
            if (hp <= 0f && isOver == false || GamePlayManager.instance.玩家.transform.position.y <= -100 && isOver == false) // GamePlayManager.instance.玩家.transform.position.y <= -100 死亡
            {
                isOver = true;
            }
        }
    }
    float _hp = 100f;
    public float maxhp = 100f;
    public Action 血量變化事件 = null;

    /// <summary>是否結束</summary>
    public bool isOver
    {
        get
        {
            return _isOver;
        }
        set
        {
            _isOver = value;
            // 如果有人訂閱死亡事件 而且 確實已經遊戲結束了
            if (死亡事件 != null && _isOver == true)
            {
                死亡事件.Invoke();
            }
        }
    }
    bool _isOver = false;
    public Action 死亡事件 = null;

    /// <summary> 分數 </summary>
    public int score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            if (分數變化事件 != null)
            {
                分數變化事件.Invoke();
            }
        }
    }
    int _score = 0; // 每次新的遊戲都歸零, 不需要重置的不用寫這行
    public Action 分數變化事件 = null;

    /// <summary>最高分(存讀檔)</summary>
    public int highScore
    {
        // 當有人讀取最高分的時候 直接去硬碟查詢highScore值
        // 如果查不到就給0
        get { return PlayerPrefs.GetInt("highScore", 0); }
        // 當有人寫入最高分的時候 將這個值存到硬碟的highScore值當中
        set { PlayerPrefs.SetInt("highScore", value); }
    }

    /// <summary>
    /// 寶石
    /// </summary>
    public int gem
    {
        get
        {
            return PlayerPrefs.GetInt("gem", 0);
        }
        set
        {
            PlayerPrefs.SetInt("gem", value);
            if (寶石變化事件 != null)
            {
                寶石變化事件.Invoke();
            }
        }
    }
    public Action 寶石變化事件 = null;

    /// <summary>生命值等級(自動存檔)</summary>
    public int 生命值等級
    {
        get
        {
            return PlayerPrefs.GetInt("HPLEVEL", 0);
        }

        set
        {
            PlayerPrefs.SetInt("HPLEVEL", value);
        }

    }

    /// <summary>體力等級(自動存檔)</summary>
    public int 體力等級
    {
        get
        {
            return PlayerPrefs.GetInt("APLEVEL", 0);
        }

        set
        {
            PlayerPrefs.SetInt("APLEVEL", value);
        }

    }


    public Action 商店刷新事件 = null;
    public void 要求刷新商店()
    {
        if (商店刷新事件 != null)
        {
            商店刷新事件.Invoke(); // 刷新
        }
    }
    /// <summary>
    /// 鑰匙顯示器
    /// </summary>
    public int 任務道具
    {
        get { return _任務道具; }
        set
        {
            _任務道具 = value;
            if (任務道具刷新 != null)
                任務道具刷新.Invoke();
        }
    }
    int _任務道具 = 0;
    public Action 任務道具刷新 = null;


}
