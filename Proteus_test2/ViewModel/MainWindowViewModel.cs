using Proteus_test2.Common.MVVM;
using Proteus_test2.Interfaces;
using Proteus_test2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Proteus_test2.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        public static int NumTab = 0;

        private ObservableCollection<Tab> tabViewModels;
        public ObservableCollection<Tab> TabViewModels
        {
            get => tabViewModels;
            set
            {
                tabViewModels = value;
                OnPropertyChanged(nameof(TabViewModels));
            }
        }

        public ICommand NewTabCommand { get; }
        public RoutedEventHandler NewTabAction { get; set; }
        public MainWindowViewModel()
        {
            TabViewModels = new ObservableCollection<Tab>();
            

            NewTabAction += NewTabExecute2;
            NewTabCommand = new RelayCommand(NewTabExecute);
            tabViewModels.CollectionChanged += Tabs_CollectionChanged;
            tabViewModels.Add(new DataTabModel());
        }

        private void NewTabExecute2(object sender, RoutedEventArgs e)
        {
            NewTabExecute(null);
        }

        private void Tabs_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Tab tab;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    tab = (Tab)e.NewItems[0];
                    tab.CloseRequested += OnTanCloseRequested;
                    break;
                case NotifyCollectionChangedAction.Remove:
                    tab = (Tab)e.OldItems[0];
                    tab.CloseRequested -= OnTanCloseRequested;
                    break;
            }
            //NumTab = e.NewStartingIndex;
        }

        private void OnTanCloseRequested(object sender, EventArgs e)
        {
            if (((DataTabModel)sender).DeleteEnable && NumTab > 1)
            {
                tabViewModels.Remove((Tab)sender);
                NumTab--;
            }
            
        }

        private void NewTabExecute(object obj)
        {
            if(NumTab < 10)
            {
                tabViewModels.Add(new DataTabModel());
            }
        }
    }
}
