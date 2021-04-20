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
using System.Windows.Controls;
using System.Windows.Input;

namespace Proteus_test2.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<ITab> tabViewModels;
        public ObservableCollection<ITab> TabViewModels
        {
            get => tabViewModels;
            set
            {
                tabViewModels = value;
                OnPropertyChanged(nameof(TabViewModels));
            }
        }

        ICommand NewTabCommand { get; }
        public MainWindowViewModel()
        {
            TabViewModels = new ObservableCollection<ITab>();
            TabViewModels.Add(new DataTabModel());

            NewTabCommand = new RelayCommand(NewTabExecute);
            tabViewModels.CollectionChanged += Tabs_CollectionChanged;
        }

        private void Tabs_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ITab tab;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    tab = (ITab)e.NewItems[0];
                    tab.CloseRequested += OnTanCloseRequested;
                    break;
                case NotifyCollectionChangedAction.Remove:
                    tab = (ITab)e.OldItems[0];
                    tab.CloseRequested -= OnTanCloseRequested;
                    break;
            }
        }

        private void OnTanCloseRequested(object sender, EventArgs e)
        {
            tabViewModels.Remove((ITab)sender);
        }

        private void NewTabExecute(object obj)
        {
            tabViewModels.Add(new DataTabModel());
        }
    }
}
