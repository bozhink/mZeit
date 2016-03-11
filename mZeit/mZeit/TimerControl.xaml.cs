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
using System.Xml;

namespace mZeit
{
    /// <summary>
    /// Interaction logic for TimerControl.xaml
    /// </summary>
    public partial class TimerControl : UserControl
    {
        public TimerControl()
        {
            InitializeComponent();
        }

        private void watchLapButton_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem li = new ListBoxItem();
            DateTime t = DateTime.Now;
            //li.Content = System.DateTime.Now.ToString();
            li.Content = t.Year.ToString("d4") + "-" + t.Month.ToString("d2") + "-" + t.Day.ToString("d2") + " " + t.Hour.ToString("d2") + ":" + t.Minute.ToString("d2") + ":" + t.Second.ToString("d2") + "." + t.Millisecond.ToString("d3");
            watchListBox.Items.Add(li);
        }

        private void watchResetButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Reset watch", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
            if (result == MessageBoxResult.Yes)
            {
                watchListBox.Items.Clear();
            }
        }

        public void Clear()
        {
            watchListBox.Items.Clear();
            watchTextBox.Text = string.Empty;
        }

        public XmlDocument Xml
        {
            get
            {
                XmlDocument xml = new XmlDocument();
                XmlNode watch = xml.CreateElement("watch");

                XmlAttribute isPaused = xml.CreateAttribute("paused");
                isPaused.InnerText = "no";

                XmlAttribute lastTimeStamp = xml.CreateAttribute("last-time-stamp");
                lastTimeStamp.InnerText = System.DateTime.Now.ToString();

                XmlElement watchName = xml.CreateElement("title");
                watchName.InnerText = watchTextBox.Text;

                XmlElement watchItems = xml.CreateElement("items");
                foreach (ListBoxItem li in watchListBox.Items)
                {
                    XmlElement item = xml.CreateElement("item");
                    item.InnerText = li.Content.ToString();
                    watchItems.AppendChild(item);
                }

                watch.Attributes.Append(isPaused);
                watch.Attributes.Append(lastTimeStamp);
                watch.AppendChild(watchName);
                watch.AppendChild(watchItems);

                xml.AppendChild(watch);

                return xml;
            }

            set
            {
                XmlNode watchName = value.SelectSingleNode("//title");
                if (watchName != null)
                {
                    watchTextBox.Text = watchName.InnerText;
                }

                foreach(XmlNode item in value.SelectNodes("//item"))
                {
                    ListBoxItem li = new ListBoxItem();
                    li.Content = item.InnerText;
                    watchListBox.Items.Add(li);
                }
            }
        }
    }
}
