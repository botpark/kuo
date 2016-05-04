using System.ServiceProcess;

namespace Kuo
{
    static class Program
    {
        /// <summary>
        ///     El principal punto de entrada para la aplicación.
        /// </summary>
        static void Main()
        {
            #if DEBUG
                        Kuo _service = new Kuo();
                        _service.OnDebug();
                        System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
            #else
                        ServiceBase[] ServicesToRun;
                        ServicesToRun = new ServiceBase[]
                        {
                            new Kuo()
                        };

                        ServiceBase.Run(ServicesToRun);
            #endif
        }
    }
}
