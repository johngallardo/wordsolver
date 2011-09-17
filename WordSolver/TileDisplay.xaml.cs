using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace WordSolver
{
    //[DefaultBindingProperty("DisplayCharacter")]
    public partial class TileDisplay : UserControl, INotifyPropertyChanged
    {
        public TileDisplay()
        {
            this.DataContext = this;
            InitializeComponent();            
        }

        public string DisplayCharacter
        {
            get { return GetValue(DisplayCharacterProperty) as string; }
            set
            {
                if (value != DisplayCharacter)
                {
                    SetValue(DisplayCharacterProperty, value);
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            var h = PropertyChanged;
            if (h != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                h(this, e);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static readonly DependencyProperty DisplayCharacterProperty = DependencyProperty.Register(
            "DisplayCharacter",
            typeof(string),
            typeof(TileDisplay),
            new PropertyMetadata("*"));
    }
}
