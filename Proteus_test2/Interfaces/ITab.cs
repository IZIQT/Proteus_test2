using Proteus_test2.Common.MVVM;
using System;
using System.Windows.Input;

namespace Proteus_test2.Interfaces
{
    public interface ITab
    {
        string Name { get; set; }
        ICommand CloseCommand { get; set; }
        event EventHandler CloseRequested;
    }

    public abstract class Tab : ITab
    {
        public Tab()
        {
            CloseCommand = new RelayCommand(p => CloseRequested?.Invoke(this, EventArgs.Empty));
        }
        public string Name { get; set; }
        public ICommand CloseCommand { get; set; }
        public event EventHandler CloseRequested;
    }
}
