﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EDCHOST24
{
    // STL : Storage the Package
    public class PackageList //存储预备要用的物资信息
    {
        private List<Package> mPackageList;

        private int X_MAX;
        private int X_MIN;
        private int Y_MAX;
        private int Y_MIN;
        private int LIMITED_TIME;
        private int TIME_INTERVAL;

        // point to package has been generated
        public int mPointer;

        public PackageList(int _X_MAX, int _X_MIN, int _Y_MAX, int _Y_MIN, int _INITIAL_AMOUNT, int _LIMITED_TIME, int _TIME_INTERVAL) //生成指定数量的物资
        {
            mPointer = _INITIAL_AMOUNT - 1;

            X_MAX = _X_MAX;
            X_MIN = _X_MIN;
            Y_MAX = _Y_MAX;
            Y_MIN = _Y_MIN;
            LIMITED_TIME = _LIMITED_TIME;
            TIME_INTERVAL = _TIME_INTERVAL;

            mPackageList = new List<Package> ();
            Random NRand = new Random();

            // initialize package at the beginning of game
            for (int i = 0; i < _INITIAL_AMOUNT; i++)
            {
                Dot Departure = Dot(NRand.Next(X_MIN, X_MAX), NRand.Next(Y_MIN, Y_MAX));
                Dot Destination = Dot(NRand.Next(X_MIN, X_MAX), NRand.Next(Y_MIN, Y_MAX));

                if (!(IsPosLegal(Departure) && IsPosLegal(Destination)))
                {
                    i--;
                    continue;
                }

                mPackageList.Add(Departure, Destination, 0);
            }


            // generate the time series for packages
            int LastGenerationTime = 0;
            for (int i = _INITIAL_AMOUNT; LastGenerationTime + TIME_INTERVAL <= LIMITED_TIME; i++)
            {
                Dot Departure = Dot(NRand.Next(X_MIN, X_MAX), NRand.Next(Y_MIN, Y_MAX));
                Dot Destination = Dot(NRand.Next(X_MIN, X_MAX), NRand.Next(Y_MIN, Y_MAX));

                if (!(IsPosLegal(Departure) && IsPosLegal(Destination)))
                {
                    i--;
                    continue;
                }

                int GenerationTime = NRand.Next(LastGenerationTime, LastGenerationTime + TIME_INTERVAL);

                LastGenerationTime += TIME_INTERVAL;
                mPackageList.Add(Departure, Destination, GenerationTime);
            }
        }

        /*
        public static Dot CrossNo2Dot(int CrossNoX, int CrossNoY)
        {
            int x = Game.MAZE_SHORT_BORDER_CM + Game.MAZE_SIDE_BORDER_CM + Game.MAZE_CROSS_DIST_CM * CrossNoX;
            int y = Game.MAZE_SHORT_BORDER_CM + Game.MAZE_SIDE_BORDER_CM + Game.MAZE_CROSS_DIST_CM * CrossNoY;
            Dot temp = new Dot(x, y);
            return temp;
        }
        */

        

        public Package Index (int i)
        {
            return mPackageList[i];
        }

        public void PickPackage(int i)
        {
            mPackageList[i].PickPackage();
        }

        public void ResetPointer()
        {
            mPointer = 0;
        }

        // judge whether the generation point of package is legal
        private bool IsPosLegal(Dot PackagePos)
        {
            
        }
    }
}
