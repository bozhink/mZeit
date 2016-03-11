using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;

namespace mZeit
{
    public partial class Form1 : Form
    {
        private const string fileName = "zeit.xml";
        protected const string Charset = "utf-8";
        protected Encoding encoding = Encoding.GetEncoding(Charset);

        private List<ElementHost> elementHostList = new List<ElementHost>();
        private List<TimerControl> timerControlList = new List<TimerControl>();

        private int numberOfWatches;

        public Form1()
        {
            elementHostList.Clear();
            timerControlList.Clear();

            XmlDocument watchesXml = ReadXml();
            XmlNodeList watchesNodeList = watchesXml.SelectNodes("//watch");

            int numberOfWatches = 0;
            foreach (XmlNode node in watchesNodeList)
            {
                numberOfWatches++;

                XmlDocument xml = new XmlDocument();
                xml.InnerXml = node.OuterXml;

                TimerControl timerControl = new TimerControl();
                timerControl.Clear();
                timerControl.Xml = xml;

                timerControlList.Add(timerControl);

                ElementHost elementHost = new ElementHost();
                elementHost.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left)));
                elementHost.Location = new System.Drawing.Point(4 + (numberOfWatches - 1) * 200, 4);
                elementHost.Name = "elementHost" + numberOfWatches;
                elementHost.Size = new System.Drawing.Size(200, 490);
                elementHost.TabIndex = 0;
                elementHost.Child = timerControl;
                elementHostList.Add(elementHost);
            }

            InitializeComponent();

            foreach (ElementHost eh in elementHostList)
            {
                this.watchesPanel.Controls.Add(eh);
            }
        }

        ~Form1()
        {
            OnCloseForm();
        }

        private void OnCloseForm()
        {
            StringBuilder result = new StringBuilder();
            result.Append("<time>");
            foreach (TimerControl tc in timerControlList)
            {
                result.Append(tc.Xml.OuterXml);
            }
            result.Append("</time>");
            WriteXmlFile(result.ToString());
        }

        private XmlDocument ReadXml()
        {
            if (!File.Exists(fileName))
            {
                CreateEmptyXml();
            }

            StreamReader reader = null;
            string line = string.Empty;
            StringBuilder result = new StringBuilder();
            try
            {
                reader = new StreamReader(fileName, encoding);
                while ((line = reader.ReadLine()) != null)
                {
                    result.Append(line);
                    result.Append('\n');
                }
            }
            catch (IOException ioex)
            {
                MessageBox.Show(ioex.Message, "Can not read file " + fileName + ".");
            }
            catch (ArgumentOutOfRangeException aorex)
            {
                MessageBox.Show(aorex.Message, "Argument out of range in the StringBuilder.");
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            XmlDocument watchesXml = new XmlDocument();
            watchesXml.LoadXml(result.ToString());

            return watchesXml;
        }

        private string BuildWatchXml(int watchNumber, string watchName, ListBox.ObjectCollection listItems)
        {
            StringBuilder result = new StringBuilder();
            result.Append("<watch" + watchNumber + ">");
            result.Append("<name>");
            result.Append(watchName);
            result.Append("</name>");
            result.Append("<items>");
            for (int i = 0; i < listItems.Count; i++)
            {
                result.Append("<item>");
                result.Append(listItems[i].ToString());
                result.Append("</item>");
            }
            result.Append("</items>");
            result.Append("</watch" + watchNumber + ">");
            return result.ToString();
        }

        private void CreateEmptyXml()
        {
            StringBuilder result = new StringBuilder();
            result.Append("<time></time>");
            WriteXmlFile(result.ToString());
        }

        private void WriteXmlFile(string xml)
        {
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(fileName, false, encoding);
                writer.Write(xml);
            }
            catch (IOException ioex)
            {
                Console.WriteLine("Can not write to file " + fileName + ".");
                Console.WriteLine(ioex.Message);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnCloseForm();
            Close();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnCloseForm();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            OnCloseForm();
        }

        private void newWatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            numberOfWatches++;
            {
                TimerControl timerControl = new TimerControl();
                timerControl.Clear();
                timerControlList.Add(timerControl);

                ElementHost elementHost = new ElementHost();
                elementHost.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left)));
                elementHost.Location = new System.Drawing.Point(4 + (numberOfWatches - 1) * 200, 4);
                elementHost.Name = "elementHost" + numberOfWatches;
                elementHost.Size = new System.Drawing.Size(200, 490);
                elementHost.TabIndex = 0;
                elementHost.Child = timerControl;
                elementHostList.Add(elementHost);
            }
            this.watchesGroupBox.SuspendLayout();
            this.SuspendLayout();

            this.watchesPanel.Controls.Clear();
            foreach (ElementHost eh in elementHostList)
            {
                this.watchesPanel.Controls.Add(eh);
            }

            this.watchesGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
