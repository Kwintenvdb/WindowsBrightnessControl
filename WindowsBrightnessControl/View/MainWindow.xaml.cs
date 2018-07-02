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
		}

		protected override void OnWindowVisibleChanged(bool visible)
		{
			if (_storyboard != null)
			{
				this.BeginAnimation(VisibilityProperty, null);
				_storyboard.Remove(this);
				_storyboard.Stop(this);
			}

			if (visible)
			{
				this.Visibility = Visibility.Visible;
			}

			_storyboard = new Storyboard();
			double from = this.Opacity;
			double to = visible ? 1.0 : 0.0;
			double duration = visible ? 0.1 : 0.2;
			var fadeDurationTimeSpan = TimeSpan.FromSeconds(duration);
			var fadeAnimation = new DoubleAnimation(from, to, fadeDurationTimeSpan);

			_storyboard.Children.Add(fadeAnimation);
			Storyboard.SetTargetName(fadeAnimation, this.Name);
			Storyboard.SetTargetProperty(fadeAnimation, new PropertyPath(OpacityProperty));

			if (!visible)
			{
				var visibilityAnimation = new ObjectAnimationUsingKeyFrames();
				var keyTime = KeyTime.FromTimeSpan(fadeDurationTimeSpan);
				visibilityAnimation.KeyFrames.Add(new DiscreteObjectKeyFrame(Visibility.Collapsed, keyTime));

				_storyboard.Children.Add(visibilityAnimation);
				Storyboard.SetTargetName(visibilityAnimation, this.Name);
				Storyboard.SetTargetProperty(visibilityAnimation, new PropertyPath(VisibilityProperty));
			}
			

			BeginStoryboard(_storyboard);
			//this.BeginAnimation(OpacityProperty, fadeAnimation);
		}
	}
}
