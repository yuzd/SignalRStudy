using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Host.WindowsService
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main(string[] args)
        {
            if (Environment.UserInteractive)
            {

                SignalRHostWindowsService s = new SignalRHostWindowsService();
                s.start(args);
            }
            else
            {
                
                ServiceBase.Run(new SignalRHostWindowsService());
            }

            //以控制台程序debug  注释掉下面一句
            //Console.ReadKey();
        }
    }
}
