using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ChatBox
{
    public class ChatMessageTemplateSelector: DataTemplateSelector
    {
        public DataTemplate ReceiverTemplate { get; set; }
        public DataTemplate SenderTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var message = item as ChatMessage;
            if (message != null)
            {
                return message.IsSender ? SenderTemplate : ReceiverTemplate;
            }
            return base.SelectTemplate(item, container);
        }
    }
}
