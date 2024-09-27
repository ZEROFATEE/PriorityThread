using BasicThreading;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PriorityThread
{
    public partial class Form1 : Form
    {
        Thread threadA, threadB, threadC, threadD;
        public Form1()
        {
            InitializeComponent();
            label1.Text = "-Thread Starts-";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartThreads();
        }

        private void StartThreads()
        {
            Console.WriteLine("-Thread Starts-");
            threadA = new Thread(MyThreadClass.Thread1) { Name = "Thread A", Priority = ThreadPriority.Highest };
            threadB = new Thread(MyThreadClass.Thread1AndThread2) { Name = "Thread B", Priority = ThreadPriority.Normal };
            threadC = new Thread(MyThreadClass.Thread1) { Name = "Thread C", Priority = ThreadPriority.AboveNormal };
            threadD = new Thread(MyThreadClass.Thread1AndThread2) { Name = "Thread D", Priority = ThreadPriority.BelowNormal };


            threadA.Start();
            threadB.Start();
            threadC.Start();
            threadD.Start();


            {
                threadA.Join();
                threadB.Join();
                threadC.Join();
                threadD.Join();

            }
        }
    }
}

namespace BasicThreading
{
    public static class MyThreadClass
    {
        private static ManualResetEvent signalEvent = new ManualResetEvent(false);
        //T-T ayoko na nilagay to para magfocus sa prio pero ayaw pa rin AHAHAHAHA buang
        public static void Thread1()
        {
            if (Thread.CurrentThread.Name == "Thread A" || Thread.CurrentThread.Name == "Thread C")
            {
                signalEvent.Set();
            }
            else
            {
                signalEvent.WaitOne();
            }

            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(500);
                Console.WriteLine("Name of Thread: " + Thread.CurrentThread.Name + " (Thread1) = " + i);

                if (Thread.CurrentThread.Name == "Thread B" || Thread.CurrentThread.Name == "Thread D")

                {
                    break;
                }
            }
        
            }
        public static void Thread2()
        {
            for (int i = 1; i < 6; i++)
            {
                Thread.Sleep(1500);
                Console.WriteLine("Name of Thread: " + Thread.CurrentThread.Name + " (Thread2) = " + i);
            }
        }


        //wala to sa instructions pero nilagay namin to run thread B and D sa thread1
        public static void Thread1AndThread2()
        {
            Thread1();
            Thread2();
        }
    }
}

 
