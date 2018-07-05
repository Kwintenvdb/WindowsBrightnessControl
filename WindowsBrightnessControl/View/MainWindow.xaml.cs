using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace WindowsBrightnessControl.View
{
    public partial class MainWindow : BrightnessWindow
    {
		private Storyboard _storyboard;

		public MainWindow()
        {
            InitializeComponent();

			int marginTop = 80;
			this.Top = SystemParameters.WorkArea.Height - this.ActualHeight - marginTop;
			int marginLeft = 30;
			this.Left = SystemParameters.WorkArea.Width - this.Width - marginLeft;

			this.Visibility = Visibility.Collapsed;
		}

		protected override void OnWindowVisibleChanged(bool visible)
		{
			if (_storyboard != null)
			{
				this.BeginAnimation(VisibilityProperty, null);
				this.BeginAnimation(OpacityProperty, null);
				_storyboard.Remove(this);
				_storyboard.Stop(this);
				_storyboard = null;
			}

			if (visible)
			{
				this.Opacity = 1;
				this.Visibility = Visibility.Visible;
			}
			else
			{
				_storyboard = new Storyboard();
				double from = this.Opacity;
				double to = 0.0;
				double duration = 0.2;
				var fadeDurationTimeSpan = TimeSpan.FromSeconds(duration);
				var fadeAnimation = new DoubleAnimation(from, to, fadeDurationTimeSpan);

				_storyboard.Children.Add(fadeAnimation);
				Storyboard.SetTargetName(fadeAnimation, this.Name);
				Storyboard.SetTargetProperty(fadeAnimation, new PropertyPath(OpacityProperty));

				var visibilityAnimation = new ObjectAnimationUsingKeyFrames();
				var keyTime = KeyTime.FromTimeSpan(fadeDurationTimeSpan);
				visibilityAnimation.KeyFrames.Add(new DiscreteObjectKeyFrame(Visibility.Collapsed, keyTime));

				_storyboard.Children.Add(visibilityAnimation);
				Storyboard.SetTargetName(visibilityAnimation, this.Name);
				Storyboard.SetTargetProperty(visibilityAnimation, new PropertyPath(VisibilityProperty));

				BeginStoryboard(_storyboard);
			}
		}
	}
}
