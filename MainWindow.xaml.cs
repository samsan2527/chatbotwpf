using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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

namespace ChatBox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<ChatMessage> Messages { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            string fpath = AppDomain.CurrentDomain.BaseDirectory + "bg1.jpg";
            var imageBrush = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(fpath))
            };
            imageBrush.Opacity = 0.2;
            ChatList.Background = imageBrush;

            Messages = new ObservableCollection<ChatMessage>
            {
                new ChatMessage
                {
                    Text = "How may I help you?",
                    IsSender = false
                    
                },
                new ChatMessage
                {
                    Text = "Click any",
                    IsSender = true,
                    Buttons = new List<ButtonInfo>
                    {
                        new ButtonInfo { Content = "Create Account", Tag = "Create Account" },
                        new ButtonInfo { Content = "Existing Customer", Tag = "Existing Customer" }
                    }
                }
                
            };
            send_btn.Visibility = Visibility.Hidden;
            ChatInput.Visibility = Visibility.Hidden;
            ChatList.ItemsSource = Messages;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                string tag = button.Tag as string;
                // Handle button click based on the Tag or Content
                MessageBox.Show($"Button clicked: {tag}");
                if(tag=="Existing Customer")
                {
                    Messages.Add(new ChatMessage
                    {
                        Text = "Please Enter Your Mobile Number\n for example '32132132'",
                        IsSender = false
                    });
                    send_btn.Visibility = Visibility.Visible;
                    ChatInput.Visibility = Visibility.Visible;
                    
                }
                else if(tag== "Cards" || tag == "Statements" || tag == "Payments")
                {
                    Messages.Add(new ChatMessage
                    {
                        Text = "Please wait we will redirect you to the page",
                        IsSender = false
                    });
                }
                ScrollToBottom();
            }
        }
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string userInput = ChatInput.Text;
            if (!string.IsNullOrEmpty(userInput))
            {
                Messages.Add(new ChatMessage { Text ="You Entered : "+ userInput, IsSender = true });
                Messages.Add(new ChatMessage { Text ="Welcome Back SAMSAN...\n We missed you a lot", IsSender = false });
                Messages.Add(new ChatMessage { Text = "Our Services", IsSender = true,
                    Buttons = new List<ButtonInfo>
                    {
                        new ButtonInfo { Content = "Cards", Tag = "Cards" },
                        new ButtonInfo { Content = "Statements", Tag = "Statements" },
                        new ButtonInfo { Content = "Payments", Tag = "Payments" }
                    }
                });

            }
            send_btn.Visibility = Visibility.Hidden;
            ChatInput.Visibility = Visibility.Hidden;
            ScrollToBottom();


        }

        private void ScrollToBottom()
        {
            var scrollViewer = GetScrollViewer(ChatList);
            if (scrollViewer != null)
            {
                scrollViewer.ScrollToEnd();
            }
        }

        private ScrollViewer GetScrollViewer(DependencyObject depObj)
        {
            if (depObj is ScrollViewer)
                return depObj as ScrollViewer;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);
                var scrollViewer = GetScrollViewer(child);
                if (scrollViewer != null)
                    return scrollViewer;
            }
            return null;
        }
    }
    public class ChatMessage
    {
        public string Text { get; set; }
        public bool IsSender { get; set; } 
        public List<ButtonInfo> Buttons { get; set; } = new List<ButtonInfo>();
    }
    public class ButtonInfo
    {
        public string Content { get; set; }
        public string Tag { get; set; }
    }
}
