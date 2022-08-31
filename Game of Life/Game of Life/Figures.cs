﻿using System;
using System.Collections.Generic;
using System.Linq;
namespace Game_of_Life
{
    internal static class Figures
    {
        private static Dictionary<string, int[,]> ListFigures = new Dictionary<string, int[,]>()
        {
            ["Glider"] = new int[,]
            {
               {0, 1, 0},
               {0, 0, 1},
               {1, 1, 1},
            },
            ["Pulsar"] = new int[,]
            {
               {0, 1, 0, 0, 0, 0, 0, 1, 0},
               {1, 1, 1, 0, 0, 0, 1, 1, 1},
            },
            ["Cube"] = new int[,]
            {
               {1, 1, 1, 1, 1},
               {1, 1, 1, 1, 1},
               {1, 1, 1, 1, 1},
               {1, 1, 1, 1, 1},
               {1, 1, 1, 1, 1},
            },
            ["Triangle"] = new int[,]
            {
               {0, 0, 0, 1, 0, 0, 0},
               {0, 0, 1, 1, 1, 0, 0},
               {0, 1, 1, 1, 1, 1, 0},
               {1, 1, 1, 1, 1, 1, 1},
            },
            ["Figure 1"] = new int[,]
            {
               {1, 0, 0},
               {0, 1, 1},
               {1, 1, 0},
            },
            ["LWSS"] = new int[,]
            {
               {0, 1, 0, 0, 1},
               {1, 0, 0, 0, 0},
               {1, 0, 0, 0, 1},
               {1, 1, 1, 1, 0},
            },
            ["Улей"] = new int[,]
            {
               {0, 1, 0},
               {1, 0, 1},
               {1, 0, 1},
               {0, 1, 0},
            },
            ["Каравай"] = new int[,]
            {
               {0, 1, 1, 0},
               {1, 0, 0, 1},
               {0, 1, 0, 1},
               {0, 0, 1, 0},
            },
            ["Блок"] = new int[,]
            {
               {1, 1},
               {1, 1},
            },
            ["Ящик"] = new int[,]
            {
               {0, 1, 0},
               {1, 0, 1},
               {0, 1, 0},
            },
            ["Пруд"] = new int[,]
            {
               {0, 1, 1, 0},
               {1, 0, 0, 1},
               {1, 0, 0, 1},
               {0, 1, 1, 0},
            },
            ["Галактика Кока"] = new int[,]
            {
               {1, 1, 1, 1, 1, 1, 0, 1, 1},
               {1, 1, 1, 1, 1, 1, 0, 1, 1},
               {0, 0, 0, 0, 0, 0, 0, 1, 1},
               {1, 1, 0, 0, 0, 0, 0, 1, 1},
               {1, 1, 0, 0, 0, 0, 0, 1, 1},
               {1, 1, 0, 0, 0, 0, 0, 1, 1},
               {1, 1, 0, 0, 0, 0, 0, 0, 0},
               {1, 1, 0, 1, 1, 1, 1, 1, 1},
               {1, 1, 0, 1, 1, 1, 1, 1, 1},
            },
            ["Мигалка"] = new int[,]
            {
               {1, 1, 1},
            },
            ["Крест"] = new int[,]
            {
               {0, 0, 1, 1, 1, 1, 0, 0},
               {0, 0, 1, 0, 0, 1, 0, 0},
               {1, 1, 1, 0, 0, 1, 1, 1},
               {1, 0, 0, 0, 0, 0, 0, 1},
               {1, 0, 0, 0, 0, 0, 0, 1},
               {1, 1, 1, 0, 0, 1, 1, 1},
               {0, 0, 1, 0, 0, 1, 0, 0},
               {0, 0, 1, 1, 1, 1, 0, 0},
            },
            ["Пентадекатлон"] = new int[,]
            {
               {1, 1, 1, 1, 1, 1, 1, 1},
               {1, 0, 1, 1, 1, 1, 0, 1},
               {1, 1, 1, 1, 1, 1, 1, 1},
            },
        };
        public static bool[,] Get(string key)
        {
            int s = ListFigures["Pulsar"].GetLength(0);
            if (!ListFigures.ContainsKey(key))
                return null;
            bool[,] arrayBool = new bool[ListFigures[key].GetLength(0), ListFigures[key].GetLength(1)];
            for (int x = 0; x < arrayBool.GetLength(0); x++)
            {
                for (int y = 0; y < arrayBool.GetLength(1); y++)
                {
                    if (ListFigures[key][x, y] == 1)
                        arrayBool[x, y] = true;
                }
            }
            return arrayBool;
        }

    }
}
