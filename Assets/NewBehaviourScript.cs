using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    // Use this for initialization
    private int turn = 1; //To store which turn
    private int[,] state = new int[3, 3];//To store the game situation, 1 for O, 2 for X, 0 for empty
    private int count = 0;
    private int A = 0;
    private int B = 1;
    private int C = 2;
    private int D = 0;
    void Start () {
        reset();
    }

    void OnGUI()
    {

        GUIStyle fontStyle = new GUIStyle();
        fontStyle.normal.background = null;
        fontStyle.normal.textColor = new Color(1, 0, 0);
        fontStyle.fontSize = 24;
        GUI.backgroundColor = Color.red;

        if (GUI.Button(new Rect(130, 200, 100, 50), "电脑先手"))
            turn = 0;
        if (GUI.Button(new Rect(20, 200, 100, 50), "Reset"))
            reset();
        int result = check();
        A = check();
        if (result == 1)
        {
            GUI.Label(new Rect(25, 170, 100, 50), "O wins!", fontStyle);
            output();
        }
        else if (result == 2)
        {
            GUI.Label(new Rect(25, 170, 100, 50), "X wins!", fontStyle);;
            output();
        }
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (state[i, j] == 1)
                    GUI.Button(new Rect(i * 50, j * 50, 50, 50), "O");
                if (state[i, j] == 2)
                    GUI.Button(new Rect(i * 50, j * 50, 50, 50), "X");
                if (GUI.Button(new Rect(i * 50, j * 50, 50, 50), ""))
                {
                    if (result == 0)
                    {
                        if (turn == 1)
                            state[i, j] = 1;
                        else
                            state[i, j] = 2;
                        turn = 1 - turn;
                        count++;
                    }
                }
            }
        }
        if (result == 0 && count == 9)
        {
            GUI.Label(new Rect(25, 170, 100, 50), "This a draw!", fontStyle);
            output();
        }
    }

    void reset()
    {
        count = 0;
        turn = 1;
        A = 0;
        B = 1;
        C = 2;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                state[i, j] = 0;
            }
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
    void output()
    {
        string result1 = @"C:\Users\ZS\Desktop\New Unity Project\对战结果.txt";
        FileStream fs = new FileStream(result1, FileMode.Append);
        StreamWriter wr = null;
        wr = new StreamWriter(fs);
        if(A==1&&B==1)
        {
            wr.WriteLine("O wins!");
            wr.Close();
            B++;

        }
        else if(A==2&&C==2)
        {
            wr.WriteLine("X wins!");
            wr.Close();
            C++;
        }
        if(A == 0 && count == 9&&D==0)
        {
            wr.WriteLine("This is a draw!");
            wr.Close();
            D++;
        }
    }
    

    // Update is called once per frame
    void Update () {
		
	}
}
