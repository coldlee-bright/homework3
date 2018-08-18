﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public class NewBehaviourScript : MonoBehaviour
{
    private int[,] state = new int[3, 3];
    private int count = 0;
    private int x = -1, y = -1;
    private int A = 0;
    private int B = 1;
    private int C = 2;
    private int D = 0;
    private int k;

    void Start()
    {
        reset();
    }
    void step1()
    {
        List<int> row = new List<int>();
        List<int> col = new List<int>();
        int count1 = 0;
        int result = check();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (state[i, j] == 0&&result==0)
                {
                    row.Add(i);
                    col.Add(j);
                    count1++;
                }
            }
        }
        if (count1 == 0)
        {
            x = -1; y = -1;
            return;
        }
        System.Random ran = new System.Random();
        if (result == 0)
        {
            int index = ran.Next(0, count1);
            x = row[index];
            y = col[index];
            return;
        }      
    }
    void step2()
    {
        System.Random rd = new System.Random();
        do
        {
            int i = rd.Next(1, 5);
            if (state[0, 1] != 0 && state[1, 0] != 0 && state[1, 2] != 0 && state[2, 1] != 0)
                break;
            x = (2 * i - 1) / 3; y = (2 * i - 1) % 3;
        } while (state[x, y] != 0);
    }
    void step3()
    {
        if (state[1, 1] == 0)
        { x = 1; y = 1; }
        else
        {
            System.Random rd = new System.Random();
            do
            {
                int i = rd.Next(1, 10);
                int j = rd.Next(1, 10);
                if (state[0, 0] != 0 && state[0, 2] != 0 && state[2, 0] != 0 && state[2, 2] != 0)
                    break;
                x = i * 2 % 4; y = j * 2 % 4;
            } while (state[x, y] != 0);
        }

    }
    void step4(int k)
    {
        //closs
        if (state[1, 1] == k)
        {
            if (state[0, 0] == k && state[2, 2] == 0)
            {
                x = 2; y = 2;
            }
            else if (state[2, 2] == k && state[0, 0] == 0)
            {
                x = 0; y = 0;
            }
            else if (state[0, 2] == k && state[2, 0] == 0)
            {
                x = 2; y = 0;
            }
            else if (state[2, 0] == k && state[0, 2] == 0)
            {
                x = 0; y = 2;
            }
        }
        //col
        for (int i = 0; i < 3; i++)
        {
            for (int j = 1; j < 3; j++)
            {
                if (state[i, j] == k)
                {
                    if (state[i, (j + 1) % 3] == k && state[i, (j - 1) % 3] == 0)
                    {
                        x = i; y = (j - 1) % 3;
                    }

                    if (state[i, (j - 1) % 3] == k && state[i, (j + 1) % 3] == 0)
                    {
                        x = i; y = (j + 1) % 3;
                    }
                }
            }
        }
        //cow
        for (int i = 0; i < 3; i++)
        {
            for (int j = 1; j < 3; j++)
            {
                if (state[j, i] == k)
                {
                    if (state[(j + 1) % 3, i] == k && state[(j - 1) % 3, i] == 0)
                    {
                        x = (j - 1) % 3; y = i;
                    }
                    if (state[(j - 1) % 3, i] == k && state[(j + 1) % 3, i] == 0)
                    {
                        x = (j + 1) % 3; y = i;
                    }
                }
            }
        }
    }
    void output()
    {
        string result1 = @"D:\unity\Documents\井字棋\对战结果.txt";
        FileStream fs = new FileStream(result1, FileMode.Append,FileAccess.Write,FileShare.Write);
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
    void OnGUI()
    {
        GUIStyle fontStyle = new GUIStyle();
        fontStyle.normal.background = null;
        fontStyle.normal.textColor = new Color(1, 0, 0);
        fontStyle.fontSize = 24;
        GUI.backgroundColor = Color.red;
        if (GUI.Button(new Rect(385, 200, 100, 50), "电脑先手")&&state[1,1]==0)
        {
            state[1, 1] = 2;
        }
        if (GUI.Button(new Rect(275, 200, 100, 50), "Reset"))
            reset();
        int result = check();
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
                            state[i, j] = 1;
                            result = check();                      
                             step1();
                        if ((state[0, 2] == 1 && state[2, 0] == 1) || (state[0, 0] == 1 && state[2, 2] == 1))
                            step2();
                        else
                            step3();
                        step4(1);
                        step4(2);            
                        if (x != -1 && y != -1 && state[x, y] == 0 && result == 0)
                            state[x, y] = 2;
                        }
                    }                                                       
            }
        }
        int count = 0;
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
    int check()
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
    void reset()
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