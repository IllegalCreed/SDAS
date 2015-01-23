using SDAS.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SDAS.Views
{
    public class ProgressNode : ViewModelBase
    {
        #region Properties
        private String mName;
        public String Name
        {
            get
            {
                return mName;
            }
            set
            {
                if (mName != value)
                {
                    mName = value;
                    RaisePropertyChanged(() => Name);
                }
            }
        }
        #endregion
    }

    public class ProgressNodeCollection : ObservableCollection<ProgressNode>
    {
    }

    public partial class ProgressControl : Control
    {
        #region Const
        public const double LINE_HEIGHT_RATE = 0.3;
        #endregion

        #region Dependency Properties
        public ProgressNodeCollection Nodes
        {
            get
            {
                return (ProgressNodeCollection)GetValue(NodesProperty);
            }
            set
            {
                SetValue(NodesProperty, value);
            }
        }
        public static readonly DependencyProperty NodesProperty =
            DependencyProperty.Register("Nodes", typeof(ProgressNodeCollection), typeof(ProgressControl), new PropertyMetadata(null));

        public int CurrentProgress
        {
            get
            {
                return (int)GetValue(CurrentProgressProperty);
            }
            set
            {
                SetValue(CurrentProgressProperty, value);
            }
        }
        public static readonly DependencyProperty CurrentProgressProperty =
            DependencyProperty.Register("CurrentProgress",
            typeof(int),
            typeof(ProgressControl),
            new PropertyMetadata(-1, CurrentProgressChangedCallback));

        public Size NodeSize
        {
            get
            {
                return (Size)GetValue(NodeSizeProperty);
            }
            set
            {
                SetValue(NodeSizeProperty, value);
            }
        }
        public static readonly DependencyProperty NodeSizeProperty =
            DependencyProperty.Register("NodeSize", typeof(Size), typeof(ProgressControl), new PropertyMetadata(new Size()));

        public Brush FontBrush
        {
            get
            {
                return (Brush)GetValue(FontBrushProperty);
            }
            set
            {
                SetValue(FontBrushProperty, value);
            }
        }
        public static readonly DependencyProperty FontBrushProperty =
            DependencyProperty.Register("FontBrush", typeof(Brush), typeof(ProgressControl), new PropertyMetadata(Brushes.White));
        #endregion

        #region Ctor
        public ProgressControl()
        {
        }
        #endregion

        #region Render
        protected override void OnRender(DrawingContext dc)
        {
            var height = ActualHeight == 0 ? Height : ActualHeight;
            var width = ActualWidth == 0 ? Width : ActualWidth;

            var intervals = Nodes.Count - 1;
            var lineWidth = (width - (Nodes.Count * NodeSize.Width)) / intervals;
            var lineHeight = NodeSize.Height * LINE_HEIGHT_RATE;

            var progressNodeSize = new Size(NodeSize.Width - 4, NodeSize.Height - 4);
            var progressLineHeight = lineHeight - 4;
            var progressLineWidth = lineWidth + 8;

            var nodeCenterY = NodeSize.Height / 2;

            //画圆
            for (int i = 0; i < Nodes.Count; i++)
            {
                var nodeCenterX = i * NodeSize.Width + i * lineWidth + NodeSize.Width / 2;
                dc.DrawEllipse(Background, null, new Point(nodeCenterX, nodeCenterY), NodeSize.Width / 2, NodeSize.Height / 2);

                if (i <= CurrentProgress)
                {
                    dc.DrawEllipse(Foreground, null, new Point(nodeCenterX, nodeCenterY), progressNodeSize.Width / 2, progressNodeSize.Height / 2);
                }
            }

            //画线
            for (int i = 1; i < Nodes.Count; i++)
            {
                var left = i * NodeSize.Width + (i - 1) * lineWidth - 2;
                var top = nodeCenterY - lineHeight / 2;
                var ltPoint = new Point(left, top);
                var rbPoint = new Point(left + lineWidth + 4, top + lineHeight);
                dc.DrawRectangle(Background, null, new Rect(ltPoint, rbPoint));

                if (i <= CurrentProgress)
                {
                    left -= 2;
                    top += 2;
                    var ltProgressPoint = new Point(left, top);
                    var rbProgressPoint = new Point(left + progressLineWidth, top + progressLineHeight);
                    dc.DrawRectangle(Foreground, null, new Rect(ltProgressPoint, rbProgressPoint));
                }
            }

            //画字
            for (int i = 0; i < Nodes.Count; i++)
            {
                var nodeCenterX = i * NodeSize.Width + i * lineWidth + NodeSize.Width / 2;

                var numFont = CreateFont((i + 1).ToString());
                dc.DrawText(numFont, new Point((nodeCenterX - numFont.Width / 2), nodeCenterY - numFont.Height / 2));

                var textFont = CreateFont(Nodes[i].Name);
                var textPoint = new Point((nodeCenterX - textFont.Width / 2), NodeSize.Height + 7);
                dc.DrawText(textFont, textPoint);
            }
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            double width = arrangeBounds.Width;
            double height = arrangeBounds.Height;

            if (this.VerticalAlignment == System.Windows.VerticalAlignment.Center)
            {
                height=NodeSize.Height + 20;
            }
            
            if (this.HorizontalAlignment == System.Windows.HorizontalAlignment.Center)
            {
                width = Nodes.Count * NodeSize.Width + (Nodes.Count-1) * 50;
            }

            return new Size(width, height);
        }

        private FormattedText CreateFont(String text)
        {
            return new FormattedText(text,
                                  CultureInfo.GetCultureInfo("zh"),
                                  FlowDirection.LeftToRight,
                                  new Typeface(FontFamily.Source),
                                  FontSize,
                                  FontBrush);
        }

        private static void CurrentProgressChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ProgressControl)
            {
                var progressControl = d as ProgressControl;
                progressControl.InvalidateVisual();
            }
        }
        #endregion
    }
}
