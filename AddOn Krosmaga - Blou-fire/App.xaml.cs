using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AddOn_Krosmaga___Blou_fire.Services;

namespace AddOn_Krosmaga___Blou_fire
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
	    public TrackerCoreSrv TrackerCoreService { get; set; }
	    protected override void OnStartup(StartupEventArgs e)
	    {
		    TrackerCoreService = new TrackerCoreSrv();
			TrackerCoreService.StartService();
			base.OnStartup(e);
		}
    }
}
