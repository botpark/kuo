namespace Kuo
{
    partial class Installer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Kuo = new System.ServiceProcess.ServiceProcessInstaller();
            this.KuoService = new System.ServiceProcess.ServiceInstaller();
            // 
            // Kuo
            // 
            this.Kuo.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.Kuo.Password = null;
            this.Kuo.Username = null;
            // 
            // KuoService
            // 
            this.KuoService.Description = "Servicio que gestiona el dispositivo biometrico";
            this.KuoService.DisplayName = "Kuo";
            this.KuoService.ServiceName = "BPark";
            this.KuoService.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            this.KuoService.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.serviceInstaller1_AfterInstall);
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.Kuo,
            this.KuoService});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller Kuo;
        private System.ServiceProcess.ServiceInstaller KuoService;
    }
}