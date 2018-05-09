﻿using P9_UndaVerde;
using System.Collections.Generic;
using System.Windows;
using System.Diagnostics;
using System.Threading;
using System;
using System.Threading.Tasks;

namespace TrafficSimTM
{
    class SemaphoreSystem
    {
        private Stopwatch clk;
        private MainWindow mainWin = Application.Current.Windows[0] as MainWindow;
        private List<Point> _coordinates;
        public List<SemaphoreUI> _semaphores;
        public SemaphoreSystem(List<Point> coordinates)
        {
            char i = 'a';
            int delay = 1;
            _semaphores = new List<SemaphoreUI>();
            _coordinates = coordinates;
            foreach (var item in _coordinates)
            {
                _semaphores.Add(new SemaphoreUI("sem" + i++.ToString(), (int)item.X, (int)item.Y,delay++));
            }
        }

        public void StartSystem()
        {
            List<Task> semTsk = new List<Task>();
            foreach (var item in _semaphores)
            {
                semTsk.Add(new Task(async () =>
                {
                    while (true)
                    {
                        
                        item.lightUp();
                        await Task.Delay(3000);
                        item._color = true;
                        await Task.Delay(item._delay * 1000);
                        item.lightUp();
                        await Task.Delay(3000);
                        item._color = false;
                    }              
            }));
            }

                
            foreach (var item in semTsk)
            {
                item.Start(TaskScheduler.FromCurrentSynchronizationContext());
            }

            
                        
        }
    }
}
