using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class WordDisplay : UserControl
    {
        public WordDisplay()
        {
            InitializeComponent();
        }

        public IList<TileInfo> TileInfos
        {
            get { return (IList<TileInfo>)GetValue(TileInfosProperty); }
            set { SetValue(TileInfosProperty, value); }
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            var s = base.MeasureOverride(availableSize);
            return s;
        }

        protected override Size ArrangeOverride(Size availableSize)
        {
            var s = base.ArrangeOverride(availableSize);
            return s;
        }

        public static readonly DependencyProperty TileInfosProperty = DependencyProperty.Register(
            "TileInfos",
            typeof(IList<TileInfo>),
            typeof(WordDisplay),
            new PropertyMetadata(new PropertyChangedCallback(OnTileInfosChanged)));

        private static void OnTileInfosChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var display = sender as WordDisplay;
            if (display == null)
                throw new ArgumentException("sender");

            display.TilePanel.Children.Clear();
            var newTiles = args.NewValue as IList<TileInfo>;
            if (newTiles != null)
            {
                foreach (var tile in newTiles)
                {
                    var tileBlock = new TileDisplay();
                    tileBlock.Character.Text = tile.Letter;
                    tileBlock.Score.Text = tile.Score;
                    display.TilePanel.Children.Add(tileBlock);
                }
            }
        }
    }
}
