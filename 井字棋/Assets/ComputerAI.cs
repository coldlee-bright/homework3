using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace Assets
{
  
    public class ComputerAI : MonoBehaviour
    {
        public void Start()
        {

        }
        public int x = -1, y = -1;
        public void Step1()
        {
            
            Game r = new Game();
            List<int> row = new List<int>();
            List<int> col = new List<int>();
            int count1 = 0;
            int result = r.check();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Game.state[i, j] == 0 && result == 0)
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
        public void Step2()
        {
            Game a = new Game();
            System.Random rd = new System.Random();
            do
            {
                int i = rd.Next(1, 5);
                if (Game.state[0, 1] != 0 && Game.state[1, 0] != 0 && Game.state[1, 2] != 0 && Game.state[2, 1] != 0)
                    break;
                x = (2 * i - 1) / 3; y = (2 * i - 1) % 3;
            } while (Game.state[x, y] != 0);
        }
        public void Step3()
        {
            Game a = new Game();
            if (Game.state[1, 1] == 0)
            { x = 1; y = 1; }
            else
            {
                System.Random rd = new System.Random();
                do
                {
                    int i = rd.Next(1, 10);
                    int j = rd.Next(1, 10);
                    if (Game.state[0, 0] != 0 && Game.state[0, 2] != 0 && Game.state[2, 0] != 0 && Game.state[2, 2] != 0)
                        break;
                    x = i * 2 % 4; y = j * 2 % 4;
                } while (Game.state[x, y] != 0);
            }

        }
        public void Step4()
        {
            int r = 4, s = 4, v = 1;
            for (int m = 0; m < 3; m++)
            {
                for (int n = 0; n < 3; n++)
                {
                    if (Game.state[m, n] == 0 && (Game.state[m, 0] == 1 || Game.state[m, 1] == 1 || Game.state[m, 2] == 1) && (Game.state[0, n] == 1 || Game.state[1, n] == 1 || Game.state[2, n] == 1) && (Game.state[m, 0] != 2 && Game.state[m, 1] != 2 && Game.state[m, 2] != 2 && Game.state[0, n] != 2 && Game.state[1, n] != 2 && Game.state[2, n] != 2))
                    {
                        if (r == 4 && s == 4)
                        {
                            r = m;
                            s = n;
                            v = 2;

                        }
                        else
                        {
                            r = x;
                            s = y;
                        }
                    }
                }
            }
            if (v == 2)
            {
                x = r;
                y = s;
            }
        }
        public void Step5()
        {
            for (int m = 0; m < 3; m++)
            {
                for (int n = 0; n < 3; n++)
                {
                    if (Game.state[m, n] == 0 && (Game.state[m, 0] == 2 || Game.state[m, 1] == 2 || Game.state[m, 2] == 2) && (Game.state[0, n] == 2 || Game.state[1, n] == 2 || Game.state[2, n] == 2) && (Game.state[m, 0] != 1 && Game.state[m, 1] != 1 && Game.state[m, 2] != 1 && Game.state[0, n] != 1 && Game.state[1, n] != 1 && Game.state[2, n] != 1))
                    {
                        x = m;
                        y = n;
                    }
                }
            }
        }

        public void Step6(int k)
        {
            Game a = new Game();
            //closs
            if (Game.state[1, 1] == k)
            {
                if (Game.state[0, 0] == k && Game.state[2, 2] == 0)
                {
                    x = 2; y = 2;
                }
                else if (Game.state[2, 2] == k && Game.state[0, 0] == 0)
                {
                    x = 0; y = 0;
                }
                else if (Game.state[0, 2] == k && Game.state[2, 0] == 0)
                {
                    x = 2; y = 0;
                }
                else if (Game.state[2, 0] == k && Game.state[0, 2] == 0)
                {
                    x = 0; y = 2;
                }
            }
            //col
            for (int i = 0; i < 3; i++)
            {
                for (int j = 1; j < 3; j++)
                {
                    if (Game.state[i, j] == k)
                    {
                        if (Game.state[i, (j + 1) % 3] == k && Game.state[i, (j - 1) % 3] == 0)
                        {
                            x = i; y = (j - 1) % 3;
                        }

                        if (Game.state[i, (j - 1) % 3] == k && Game.state[i, (j + 1) % 3] == 0)
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
                    if (Game.state[j, i] == k)
                    {
                        if (Game.state[(j + 1) % 3, i] == k && Game.state[(j - 1) % 3, i] == 0)
                        {
                            x = (j - 1) % 3; y = i;
                        }
                        if (Game.state[(j - 1) % 3, i] == k && Game.state[(j + 1) % 3, i] == 0)
                        {
                            x = (j + 1) % 3; y = i;
                        }
                    }
                }
            }
        }
    }
}
