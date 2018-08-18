using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;
using Assets;

public  class Game : MonoBehaviour
{

    public static int[,] state = new int[3, 3];
    public int count = 0;
    public int A = 0;
    public int B = 1;
    public int C = 2;
    public int D = 0;
    public int k;
    public int a, b, c, d;
    public static Game  onlineiplist = new Game();
    public void Start()
    {
        reset();
    }  
    public void OnGUI()
    {
        ComputerAI r = new ComputerAI();
        GUIStyle fontStyle = new GUIStyle();
        fontStyle.normal.background = null;
        fontStyle.normal.textColor = new Color(1, 0, 0);
        fontStyle.fontSize = 24;
        GUI.backgroundColor = Color.red;
        int result = check();
        if (GUI.Button(new Rect(385, 200, 100, 50), "电脑先手")&&state[1,1]==0)
        {
            state[1, 1] = 2;
        }
        if (GUI.Button(new Rect(275, 200, 100, 50), "Reset"))
            reset();
        
        if (GUI.Button(new Rect(330, 300, 100, 50), "悔棋") && count != 0 && result == 0)
        {
            state[a, b] = 0;
            state[c, d] = 0;
        }

        if (result == 1)
        {
            GUI.Label(new Rect(325, 170, 100, 50), " 你赢了!", fontStyle);
            A = 1;
            output();
        }
        else if (result == 2)
        {
            GUI.Label(new Rect(325, 170, 100, 50), "菜鸟，你输了", fontStyle);
            A = 2;
           output();
        }     
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (state[i, j] == 1) 
                    GUI.Button(new Rect(300 + i * 50, j * 50, 50, 50), "O");
                if (state[i, j] == 2)
                    GUI.Button(new Rect(300 + i * 50, j * 50, 50, 50), "X");
                if (GUI.Button(new Rect(300 + i * 50, j * 50, 50, 50), ""))
                {
                    if (result == 0)
                    {
                        a = i; b = j;
                        state[i, j] = 1;
                        result = check();
                        r.Step1();

                        if ((state[0, 2] == 1 && state[2, 0] == 1) || (state[0, 0] == 1 && state[2, 2] == 1))
                            r.Step2();
                        else
                            r.Step3();
                        r.Step4();
                        r.Step5();
                        r.Step6(1);
                        r.Step6(2);
                        if (r.x != -1 && r.y != -1 && state[r.x, r.y] == 0 && result == 0)
                        {
                            state[r.x, r.y] = 2;
                            c = r.x; d = r.y;
                        }
                    }
                }
                                                        
            }
        }
        count=0;
        for(int i = 0; i < 3; i++)
        {
            for(int j=0; j < 3; j++)
            {
                if (state[i, j] == 0)
                    count++;
            }
        }
        if (result == 0 && count == 0)
        {
            GUI.Label(new Rect(315, 170, 100, 50), "平局", fontStyle);
            output();
        }           
    }

    public void output()
    { 
        string result1 = @"D:\对战结果.txt";
        FileStream fs = new FileStream(result1, FileMode.Append, FileAccess.Write, FileShare.Write);
        StreamWriter wr = null;
        wr = new StreamWriter(fs);
        if (A == 1 && B == 1)
        {
            wr.WriteLine("你赢了!");
            wr.Close();
            B++;
        }
        else if (A == 2 && C == 2)
        {
            wr.WriteLine("菜鸟，你输了");
            wr.Close();
            C++;
        }
        if (A == 0 && count == 0 && D == 0)
        {
            wr.WriteLine("平局");
            wr.Close();
            D++;
        }
    }
    public int check()
    {
        for (int i = 0; i < 3; i++)
        {
            if (state[i, 0] != 0 && state[i, 0] == state[i, 1] && state[i, 1] == state[i, 2])
            {
                return state[i, 0];
            }
            if (state[0, i] != 0 && state[0, i] == state[1, i] && state[1, i] == state[2, i])
            {
                return state[0, i];
            }
        }
        if (state[1, 1] != 0 && (state[0, 0] == state[1, 1] && state[1, 1] == state[2, 2] || state[0, 2] == state[1, 1] && state[1, 1] == state[2, 0]))
        {
            return state[1, 1];
        }
        return 0;
    }
    public void reset()
    {
        count = 0;
        A = 0;
        B = 1;
        C = 2;
        D = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                state[i, j] = 0;
            }
        }
    }
  
    void Update()
    {

    }
}