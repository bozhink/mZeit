using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;

namespace mZeit
{
    public partial class Form1 : Form
    {
        private const string fileName = "zeit.xml";
        protected const string Charset = "utf-8";

        public Form1()
        {
            InitializeComponent();
            Encoding encoding = Encoding.GetEncoding(Charset);

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
                Console.WriteLine("Can not read file " + fileName + ".");
                Console.WriteLine(ioex.Message);
            }
            catch (ArgumentOutOfRangeException aorex)
            {
                Console.WriteLine("Argument out of range in the StringBuilder.");
                Console.WriteLine(aorex.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            string x = result.ToString();

            // Watch 1
            Match w = Regex.Match(x, "<watch1>[\\s\\S]*?</watch1>");
            Match n = Regex.Match(w.Value, "(?<=<name>)[\\s\\S]*?(?=</name>)");
            textBox1.Text = n.Value;
            for (Match m = Regex.Match(w.Value, "(?<=<item>)[\\s\\S]*?(?=</item>)"); m.Success; m = m.NextMatch())
            {
                listBox1.Items.Add(m.Value);
            }

            // Watch 2
            w = Regex.Match(x, "<watch2>[\\s\\S]*?</watch2>");
            n = Regex.Match(w.Value, "(?<=<name>)[\\s\\S]*?(?=</name>)");
            textBox2.Text = n.Value;
            for (Match m = Regex.Match(w.Value, "(?<=<item>)[\\s\\S]*?(?=</item>)"); m.Success; m = m.NextMatch())
            {
                listBox2.Items.Add(m.Value);
            }

            // Watch 3
            w = Regex.Match(x, "<watch3>[\\s\\S]*?</watch3>");
            n = Regex.Match(w.Value, "(?<=<name>)[\\s\\S]*?(?=</name>)");
            textBox3.Text = n.Value;
            for (Match m = Regex.Match(w.Value, "(?<=<item>)[\\s\\S]*?(?=</item>)"); m.Success; m = m.NextMatch())
            {
                listBox3.Items.Add(m.Value);
            }

            // Watch 4
            w = Regex.Match(x, "<watch4>[\\s\\S]*?</watch4>");
            n = Regex.Match(w.Value, "(?<=<name>)[\\s\\S]*?(?=</name>)");
            textBox4.Text = n.Value;
            for (Match m = Regex.Match(w.Value, "(?<=<item>)[\\s\\S]*?(?=</item>)"); m.Success; m = m.NextMatch())
            {
                listBox4.Items.Add(m.Value);
            }

            // Watch 5
            w = Regex.Match(x, "<watch5>[\\s\\S]*?</watch5>");
            n = Regex.Match(w.Value, "(?<=<name>)[\\s\\S]*?(?=</name>)");
            textBox5.Text = n.Value;
            for (Match m = Regex.Match(w.Value, "(?<=<item>)[\\s\\S]*?(?=</item>)"); m.Success; m = m.NextMatch())
            {
                listBox5.Items.Add(m.Value);
            }

            // Watch 6
            w = Regex.Match(x, "<watch6>[\\s\\S]*?</watch6>");
            n = Regex.Match(w.Value, "(?<=<name>)[\\s\\S]*?(?=</name>)");
            textBox6.Text = n.Value;
            for (Match m = Regex.Match(w.Value, "(?<=<item>)[\\s\\S]*?(?=</item>)"); m.Success; m = m.NextMatch())
            {
                listBox6.Items.Add(m.Value);
            }

            // Watch 7
            w = Regex.Match(x, "<watch7>[\\s\\S]*?</watch7>");
            n = Regex.Match(w.Value, "(?<=<name>)[\\s\\S]*?(?=</name>)");
            textBox7.Text = n.Value;
            for (Match m = Regex.Match(w.Value, "(?<=<item>)[\\s\\S]*?(?=</item>)"); m.Success; m = m.NextMatch())
            {
                listBox7.Items.Add(m.Value);
            }

            // Watch 8
            w = Regex.Match(x, "<watch8>[\\s\\S]*?</watch8>");
            n = Regex.Match(w.Value, "(?<=<name>)[\\s\\S]*?(?=</name>)");
            textBox8.Text = n.Value;
            for (Match m = Regex.Match(w.Value, "(?<=<item>)[\\s\\S]*?(?=</item>)"); m.Success; m = m.NextMatch())
            {
                listBox8.Items.Add(m.Value);
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
            result.Append(BuildWatchXml(1, textBox1.Text, listBox1.Items));
            result.Append(BuildWatchXml(2, textBox2.Text, listBox2.Items));
            result.Append(BuildWatchXml(3, textBox3.Text, listBox3.Items));
            result.Append(BuildWatchXml(4, textBox4.Text, listBox4.Items));
            result.Append(BuildWatchXml(5, textBox5.Text, listBox5.Items));
            result.Append(BuildWatchXml(6, textBox6.Text, listBox6.Items));
            result.Append(BuildWatchXml(7, textBox7.Text, listBox7.Items));
            result.Append(BuildWatchXml(8, textBox8.Text, listBox8.Items));
            result.Append("</time>");
            WriteXmlFile(result.ToString());
        }

        private string BuildWatchXml (int watchNumber, string watchName, ListBox.ObjectCollection listItems)
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
            result.Append("<time>");
            for (int i = 1; i <= 8; i++)
            {
                result.Append("<watch" + i + ">");
                result.Append("<name>Watch " + i + "</name>");
                result.Append("<items><item>No value</item></items>");
                result.Append("</watch" + i + ">");
            }
            result.Append("</time>");
            WriteXmlFile(result.ToString());
        }

        private void WriteXmlFile(string xml)
        {
            StreamWriter writer = null;
            Encoding encoding = Encoding.GetEncoding(Charset);
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

        private void lButton1_Click(object sender, EventArgs e)
        {
            DateTime t = DateTime.Now;
            listBox1.Items.Add(t.Year + "-" + t.Month + "-" + t.Day + " " + t.Hour + ":" + t.Minute + ":" + t.Second + "." + t.Millisecond);
        }

        private void rButton1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void lButton2_Click(object sender, EventArgs e)
        {
            DateTime t = DateTime.Now;
            listBox2.Items.Add(t.Year + "-" + t.Month + "-" + t.Day + " " + t.Hour + ":" + t.Minute + ":" + t.Second + "." + t.Millisecond);
        }

        private void rButton2_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
        }

        private void lButton3_Click(object sender, EventArgs e)
        {
            DateTime t = DateTime.Now;
            listBox3.Items.Add(t.Year + "-" + t.Month + "-" + t.Day + " " + t.Hour + ":" + t.Minute + ":" + t.Second + "." + t.Millisecond);
        }

        private void rButton3_Click(object sender, EventArgs e)
        {
            listBox3.Items.Clear();
        }

        private void lButton4_Click(object sender, EventArgs e)
        {
            DateTime t = DateTime.Now;
            listBox4.Items.Add(t.Year + "-" + t.Month + "-" + t.Day + " " + t.Hour + ":" + t.Minute + ":" + t.Second + "." + t.Millisecond);
        }

        private void rButton4_Click(object sender, EventArgs e)
        {
            listBox4.Items.Clear();
        }

        private void lButton5_Click(object sender, EventArgs e)
        {
            DateTime t = DateTime.Now;
            listBox5.Items.Add(t.Year + "-" + t.Month + "-" + t.Day + " " + t.Hour + ":" + t.Minute + ":" + t.Second + "." + t.Millisecond);
        }

        private void rButton5_Click(object sender, EventArgs e)
        {
            listBox5.Items.Clear();
        }

        private void lButton6_Click(object sender, EventArgs e)
        {
            DateTime t = DateTime.Now;
            listBox6.Items.Add(t.Year + "-" + t.Month + "-" + t.Day + " " + t.Hour + ":" + t.Minute + ":" + t.Second + "." + t.Millisecond);
        }

        private void rButton6_Click(object sender, EventArgs e)
        {
            listBox6.Items.Clear();
        }

        private void lButton7_Click(object sender, EventArgs e)
        {
            DateTime t = DateTime.Now;
            listBox7.Items.Add(t.Year + "-" + t.Month + "-" + t.Day + " " + t.Hour + ":" + t.Minute + ":" + t.Second + "." + t.Millisecond);
        }

        private void rButton7_Click(object sender, EventArgs e)
        {
            listBox7.Items.Clear();
        }

        private void lButton8_Click(object sender, EventArgs e)
        {
            DateTime t = DateTime.Now;
            listBox8.Items.Add(t.Year + "-" + t.Month + "-" + t.Day + " " + t.Hour + ":" + t.Minute + ":" + t.Second + "." + t.Millisecond);
        }

        private void rButton8_Click(object sender, EventArgs e)
        {
            listBox8.Items.Clear();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnCloseForm();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            OnCloseForm();
        }
    }
}
