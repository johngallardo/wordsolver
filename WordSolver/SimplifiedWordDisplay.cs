using System;
using System.Diagnostics;
using System.Net;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Text;

namespace WordSolver
{
    public class SimplifiedWordDisplay : Grid
    {
        static SimplifiedWordDisplay()
        {
            RectFillBrush = new LinearGradientBrush
            {
                EndPoint = new Point(0.5, 1),
                StartPoint = new Point(0.5, 0)
            };
            RectFillBrush.GradientStops.Add(
                new GradientStop { Color = Color.FromArgb(0xFF, 0xE1, 0xE2, 0x9F) });
            RectFillBrush.GradientStops.Add(
                new GradientStop { Color = Color.FromArgb(0xFF, 0x9D, 0x9D, 0x61), Offset = 1 });
        }
        
        public SimplifiedWordDisplay()
        {
            LetterBlocks = new List<TextBlock>();
            ScoreBlocks = new List<TextBlock>();
            RectBlocks = new List<Rectangle>();            
        }

        public IEnumerable<TileInfo> TileInfos
        {
            get { return (IEnumerable<TileInfo>)GetValue(TileInfosProperty); }
            set { SetValue(TileInfosProperty, value); }
        }
       
        public static readonly DependencyProperty TileInfosProperty = DependencyProperty.Register(
            "TileInfos",
            typeof(IEnumerable<TileInfo>),
            typeof(SimplifiedWordDisplay),
            new PropertyMetadata(new PropertyChangedCallback(OnTileInfosChanged)));

        private static void OnTileInfosChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            Debug.WriteLine("OnTileInfosChanged, input type {0}, is IEnumerable<T> {1}",
                args.NewValue != null ? args.NewValue.GetType().ToString() : "(null)",
                args.NewValue is IEnumerable<TileInfo>);

            var display = sender as SimplifiedWordDisplay;
            if (display == null)
                throw new ArgumentException("sender");
            
			var newTiles = args.NewValue as IEnumerable<TileInfo>;      
			if(newTiles == null)
			{
				display.Children.Add(new TextBlock { Text = "Invalid!" });
			}
                
            if (newTiles != null)
            {                
                int count = newTiles.Count();
				while(display.ColumnDefinitions.Count < count)
                {
                    display.ColumnDefinitions.Add(
                        new ColumnDefinition { Width = new GridLength(75) });
                    display.LetterBlocks.Add(null);
                    display.ScoreBlocks.Add(null);
                    display.RectBlocks.Add(null);
                }
                
				int i = 0;
				foreach( var tile in newTiles )
				{
                    display.AddTileDisplay(tile.Letter, tile.Score, i++);                    
                }

                // hide the ones no longer used
                while (i < display.ColumnDefinitions.Count)
                {
                    if (display.LetterBlocks[i] != null)
                    {
                        display.LetterBlocks[i].Visibility = Visibility.Collapsed;
                        display.RectBlocks[i].Visibility = Visibility.Collapsed;
                        display.ScoreBlocks[i].Visibility = Visibility.Collapsed;
                    }
                    i++;
                }
            }                
        }

        private void AddTileDisplay(string tile, string score, int column)
        {
            var rect = RectBlocks[column];
            if (rect == null)
            {
                rect = new Rectangle();
                rect.RadiusX = 10;
                rect.RadiusY = 10;
                rect.Height = 75;
                rect.Width = 75;
                rect.Fill = RectFillBrush;
                rect.Stroke = RectStrokeBrush;                
                Children.Add(rect);
                rect.SetValue(Grid.ColumnProperty, column);
                RectBlocks[column] = rect;
            }
            rect.Visibility = System.Windows.Visibility.Visible;

            var tx = LetterBlocks[column];
            if (tx == null)
            {
                tx = new TextBlock
                {
                    Height = 75,
                    Width = 75,
                    TextAlignment = TextAlignment.Center,
                    FontSize = 56,
                    Margin = new Thickness(-3, 10, 0, 0),
                    Foreground = TextBrush,
                    FontWeight = FontWeights.Bold,
                    TextWrapping = System.Windows.TextWrapping.NoWrap,
                };
                TextOptions.SetTextHintingMode(tx, TextHintingMode.Animated);
                tx.SetValue(Grid.ColumnProperty, column);
                Children.Add(tx);
                LetterBlocks[column] = tx;
            }
            tx.Text = tile;
            tx.Visibility = System.Windows.Visibility.Visible;

            var scoreBlock = ScoreBlocks[column];
            if (scoreBlock == null)
            {
                scoreBlock = new TextBlock
                {
                    Text = score,
                    TextWrapping = System.Windows.TextWrapping.NoWrap,
                    Foreground = TextBrush,
                    TextAlignment = TextAlignment.Right,
                    Margin = new Thickness(0, 15, 5, 0),                    
                    
                };
                TextOptions.SetTextHintingMode(scoreBlock, TextHintingMode.Animated);
                scoreBlock.SetValue(Grid.ColumnProperty, column);
                Children.Add(scoreBlock);
                ScoreBlocks[column] = scoreBlock;
            }
            scoreBlock.Text = score;
            scoreBlock.Visibility = System.Windows.Visibility.Visible;
        }

        private List<TextBlock> LetterBlocks { get; set; }
        private List<TextBlock> ScoreBlocks { get; set; }
        private List<Rectangle> RectBlocks { get; set; }

        private static readonly Brush TextBrush = new SolidColorBrush(Color.FromArgb(0x0FF, 0x2D, 0x08, 0x08));
        private static readonly Brush RectStrokeBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x64, 0x6A, 0x41));
        private static readonly LinearGradientBrush RectFillBrush;
    }
}
