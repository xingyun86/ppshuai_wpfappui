using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Startup_Program(String processName)
        {
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(processName)
                {
                    CreateNoWindow = false,
                    WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized,
                }).WaitForExit();// System.Threading.Timeout.Infinite);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "信息提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
        }
        private void JiSuanQi_Click(object sender, RoutedEventArgs e)
        {
            //Startup_Program("ms-settings:display");
            Startup_Program("calc.exe");
        }
        private void SystemSettings_Click(object sender, RoutedEventArgs e)
        {
            Startup_Program("msinfo32.exe");
        }
        private void add_button_to_list_grid(String pngName, RoutedEventHandler btnHandler)
        {
            Button button = new Button()
            {
                Background = System.Windows.Media.Brushes.Transparent,
                BorderBrush = System.Windows.Media.Brushes.Transparent,
                BorderThickness = new Thickness(0),
            };
            Uri uri = new Uri("D:/res/" + pngName);
            BitmapImage bitmap = new BitmapImage(uri);
            Image cellImage = new Image();
            cellImage.Source = bitmap;
            button.Content = cellImage;
            button.Height = 160;
            button.Width = 160;
            button.Click += btnHandler;
            btnListWrapPanel.Children.Add(button);
        }
        // 窗口加载事件
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnPlay.Visibility = Visibility.Hidden;
            btnPause.Visibility = Visibility.Hidden;
            btnStop.Visibility = Visibility.Hidden;
            sliderVolume.Visibility = Visibility.Hidden;

            // 绑定视频文件
            //mediaElement.Source = new Uri("D:/res/big_buck_bunny.mp4");
            mediaElement.Source = new Uri("https://vfx.mtime.cn/Video/2019/12/24/mp4/191224094703992721_1080.mp4");
            // 交互式控制
            mediaElement.LoadedBehavior = MediaState.Manual;
            // 添加元素加载完成事件 -- 自动开始播放
            mediaElement.Loaded += new RoutedEventHandler(media_Loaded);
            // 添加媒体播放结束事件 -- 重新播放
            mediaElement.MediaEnded += new RoutedEventHandler(media_MediaEnded);
            // 添加元素卸载完成事件 -- 停止播放
            mediaElement.Unloaded += new RoutedEventHandler(media_Unloaded);
        }
        /*
          元素事件 
        */
        private void media_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as MediaElement).Play();
        }
        private void media_MediaEnded(object sender, RoutedEventArgs e)
        {
            // MediaElement需要先停止播放才能再开始播放，
            // 否则会停在最后一帧不动
            (sender as MediaElement).Stop();
            //(sender as MediaElement).Play();

            this.secondGrid.Visibility = Visibility.Hidden;
            add_button_to_list_grid("jisuanqi.png", JiSuanQi_Click);
            add_button_to_list_grid("peizhi.png", SystemSettings_Click);
            add_button_to_list_grid("dianhua.png", JiSuanQi_Click);
            add_button_to_list_grid("diantai.png", JiSuanQi_Click);
            add_button_to_list_grid("naozhong.png", JiSuanQi_Click);
            this.thirdGrid.Visibility = Visibility.Visible;
        }
        private void media_Unloaded(object sender, RoutedEventArgs e)
        {
            (sender as MediaElement).Stop();
        }
        /*
          播放控制按钮的点击事件 
        */
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Play();
        }
        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Pause();
        }
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
        }
    }
}
