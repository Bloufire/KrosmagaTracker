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
using System.Windows.Shapes;

namespace AddOn_Krosmaga___Blou_fire.Pages
{
    /// <summary>
    /// Logique d'interaction pour StatsWindow.xaml
    /// </summary>
    public partial class StatsWindow : Window
    {
        public StatsWindow(UIPlayerDatas uIDatas)
        {
            InitializeComponent();
            PlayerDatas = uIDatas;
        }


        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        public UIPlayerDatas PlayerDatas
        {
            get;
            set;
        }

      
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            PlayerDatas.WinrateParClasse.Clear();
            PlayerDatas.ToursParClasse.Clear();
            PlayerDatas.WinsForThisGroup = 0;
            PlayerDatas.LosesForThisGroup = 0;
            var checkboxes_checked = checkboxes.Children.OfType<CheckBox>().Where(x => x.IsChecked.HasValue && x.IsChecked.Value);
            foreach (var item in checkboxes_checked)
            {
                var matchesConcerned = PlayerDatas.MatchsList.Where(x => x.PlayerClasse == item.Name);

                int vIop = matchesConcerned.Count(x => x.Deck.OpponentClasse == "Iop" && x.ResultatMatch == 1);
                int vEca = matchesConcerned.Count(x => x.Deck.OpponentClasse == "Ecaflip" && x.ResultatMatch == 1);
                int vCra = matchesConcerned.Count(x => x.Deck.OpponentClasse == "Cra" && x.ResultatMatch == 1);
                int vEni = matchesConcerned.Count(x => x.Deck.OpponentClasse == "Eniripsa" && x.ResultatMatch == 1);
                int vSacri = matchesConcerned.Count(x => x.Deck.OpponentClasse == "Sacrieur" && x.ResultatMatch == 1);
                int vSadi = matchesConcerned.Count(x => x.Deck.OpponentClasse == "Sadida" && x.ResultatMatch == 1);
                int vSram = matchesConcerned.Count(x => x.Deck.OpponentClasse == "Sram" && x.ResultatMatch == 1);
                int vXel = matchesConcerned.Count(x => x.Deck.OpponentClasse == "Xelor" && x.ResultatMatch == 1);
                int vEnu = matchesConcerned.Count(x => x.Deck.OpponentClasse == "Enutrof" && x.ResultatMatch == 1);

                int dIop = matchesConcerned.Count(x => x.Deck.OpponentClasse == "Iop" && x.ResultatMatch == 0);
                int dEca = matchesConcerned.Count(x => x.Deck.OpponentClasse == "Ecaflip" && x.ResultatMatch == 0);
                int dCra = matchesConcerned.Count(x => x.Deck.OpponentClasse == "Cra" && x.ResultatMatch == 0);
                int dEni = matchesConcerned.Count(x => x.Deck.OpponentClasse == "Eniripsa" && x.ResultatMatch == 0);
                int dSacri = matchesConcerned.Count(x => x.Deck.OpponentClasse == "Sacrieur" && x.ResultatMatch == 0);
                int dSadi = matchesConcerned.Count(x => x.Deck.OpponentClasse == "Sadida" && x.ResultatMatch == 0);
                int dSram = matchesConcerned.Count(x => x.Deck.OpponentClasse == "Sram" && x.ResultatMatch == 0);
                int dXel = matchesConcerned.Count(x => x.Deck.OpponentClasse == "Xelor" && x.ResultatMatch == 0);
                int dEnu = matchesConcerned.Count(x => x.Deck.OpponentClasse == "Enutrof" && x.ResultatMatch == 0);

                PlayerDatas.AddItemToWinrateParClasse(
                    item.Name,
                    new List<double>()
                    {
                        vIop * 100 / ((vIop + dIop) == 0 ? 1 : (vIop + dIop)),
                        vEca * 100 / ((vEca + dEca) == 0 ? 1 : (vEca + dEca)),
                        vCra * 100 / ((vCra + dCra) == 0 ? 1 : (vCra + dCra)),
                        vEni * 100 / ((vEni + dEni) == 0 ? 1 : (vEni + dEni)),
                        vSacri * 100 / ((vSacri + dSacri) == 0 ? 1 : (vSacri + dSacri)),
                        vSadi * 100 / ((vSadi + dSadi) == 0 ? 1 : (vSadi + dSadi)),
                        vSram * 100 / ((vSram + dSram) == 0 ? 1 : (vSram + dSram)),
                        vXel * 100 / ((vXel + dXel) == 0 ? 1 : (vXel + dXel)),
                        vEnu * 100 / ((vEnu + dEnu) == 0 ? 1 : (vEnu + dEnu))
                    });
                PlayerDatas.AddItemToToursParClasse(
                    item.Name,
                    new List<double>()
                    {
                        matchesConcerned.Any(x => x.Deck.OpponentClasse == "Iop") ? matchesConcerned.Where(x => x.Deck.OpponentClasse == "Iop").Average(x => x.NbToursMatch) : 0,
                        matchesConcerned.Any(x => x.Deck.OpponentClasse == "Ecaflip") ? matchesConcerned.Where(x => x.Deck.OpponentClasse == "Ecaflip").Average(x => x.NbToursMatch) : 0,
                        matchesConcerned.Any(x => x.Deck.OpponentClasse == "Cra") ? matchesConcerned.Where(x => x.Deck.OpponentClasse == "Cra").Average(x => x.NbToursMatch) : 0,
                        matchesConcerned.Any(x => x.Deck.OpponentClasse == "Eniripsa") ? matchesConcerned.Where(x => x.Deck.OpponentClasse == "Eniripsa").Average(x => x.NbToursMatch) : 0,
                        matchesConcerned.Any(x => x.Deck.OpponentClasse == "Sacrieur") ? matchesConcerned.Where(x => x.Deck.OpponentClasse == "Sacrieur").Average(x => x.NbToursMatch) : 0,
                        matchesConcerned.Any(x => x.Deck.OpponentClasse == "Sadida") ? matchesConcerned.Where(x => x.Deck.OpponentClasse == "Sadida").Average(x => x.NbToursMatch) : 0,
                        matchesConcerned.Any(x => x.Deck.OpponentClasse == "Sram") ? matchesConcerned.Where(x => x.Deck.OpponentClasse == "Sram").Average(x => x.NbToursMatch) : 0,
                        matchesConcerned.Any(x => x.Deck.OpponentClasse == "Xelor") ? matchesConcerned.Where(x => x.Deck.OpponentClasse == "Xelor").Average(x => x.NbToursMatch) : 0,
                        matchesConcerned.Any(x => x.Deck.OpponentClasse == "Enutrof") ? matchesConcerned.Where(x => x.Deck.OpponentClasse == "Enutrof").Average(x => x.NbToursMatch) : 0
                    });

                PlayerDatas.WinsForThisGroup += matchesConcerned.Count(x => x.ResultatMatch == 1);
                PlayerDatas.LosesForThisGroup += matchesConcerned.Count(x => x.ResultatMatch == 0);
            }
        }

    }
}
