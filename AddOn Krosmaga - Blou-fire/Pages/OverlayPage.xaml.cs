using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using NLog;

namespace AddOn_Krosmaga___Blou_fire.Pages
{
	/// <summary>
	/// Logique d'interaction pour OverlayPage.xaml
	/// </summary>
	public partial class OverlayPage
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public OverlayPage()
		{
            try
            {
                InitializeComponent();
            }
            catch (Exception e)
            {
                logger.Error("Error : " + e);
            }
			this.AllowsTransparency = true;
			//this.WindowStyle = WindowStyle.None;
			this.ResizeMode = ResizeMode.NoResize;
			this.Background = Brushes.Transparent;
        }

        private void OverlayPage_OnMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
				this.DragMove();
		}

	}
}
