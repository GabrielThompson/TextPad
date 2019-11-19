using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextPad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private String title = "Untitled";  //保存打开的文件的标题
        Encoding textformat = Encoding.UTF8;          //设置文本的格式为 UTF-8
        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /**
             * openFileDialog1 是在设计界面拖出来的控件 OpenFileDialog
             * 
             * 主要是打开 rtf 格式的文件
             */
            openFileDialog1.Filter = "文本文件|*.txt;*.html;*.docx;*.doc;*.rtf|所有文件|*.*"; //文件打开的过滤器
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                title = openFileDialog1.FileName;
                this.Text = title;                  //显示打开的文件名
                richTextBox1.Modified = false;
                String dotformat = title.Substring(title.LastIndexOf(".") + 1);//获取文件格式
                dotformat = dotformat.ToLower();
                FileStream file = new FileStream(title, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(file, textformat);
                if (dotformat == "rtf")  //如果后缀是 rtf 加载文件进来
                {
                    richTextBox1.LoadFile(title, RichTextBoxStreamType.RichText);
                }
                else
                {
                    richTextBox1.Text = reader.ReadToEnd();
                }
                file.Close();
                reader.Close();
            }
        }
        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (title != "Untitled")
            {
                String dotformat = title.Substring(title.LastIndexOf(".") + 1);
                dotformat = dotformat.ToLower();
                if (dotformat == "rtf")
                    richTextBox1.SaveFile(title, RichTextBoxStreamType.RichText);
                else if (dotformat == "uni")
                    richTextBox1.SaveFile(title, RichTextBoxStreamType.UnicodePlainText);
                else
                    richTextBox1.SaveFile(title, RichTextBoxStreamType.PlainText);
            }
            else
            {
                另存为ToolStripMenuItem_Click(sender,e);
            }
        }
        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "文本文件|*.txt;*.html;*.docx;*.doc;*.rtf|所有文件|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                title = saveFileDialog1.FileName;
                this.Text = title;
                if (saveFileDialog1.FilterIndex == 1)
                    richTextBox1.SaveFile(title, RichTextBoxStreamType.RichText);
                else if (saveFileDialog1.FilterIndex == 2)
                    richTextBox1.SaveFile(title, RichTextBoxStreamType.PlainText);
                else if (saveFileDialog1.FilterIndex == 3)
                    richTextBox1.SaveFile(title, RichTextBoxStreamType.UnicodePlainText);
                else 
                    richTextBox1.SaveFile(title, RichTextBoxStreamType.RichText);
            }
        }
        private void OpenFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            //textBox1.Text += "\n1";
        }

        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {
            int index = this.richTextBox1.GetFirstCharIndexOfCurrentLine();
            int line = richTextBox1.GetLineFromCharIndex(index);
            richTextBox2.Font = richTextBox1.Font;
            richTextBox2.Clear();
            for (int i = 0; i < line+1; i++)
            {
                richTextBox2.Text += (i+1)+"\n";
            }

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog2.Filter = "文本文件|*.txt;*.html;*.docx;*.doc;*.rtf|所有文件|*.*"; //文件打开的过滤器
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                title = openFileDialog1.FileName;
                this.Text = title;                  //显示打开的文件名
                richTextBox1.Modified = false;
                String dotformat = title.Substring(title.LastIndexOf(".") + 1);//获取文件格式
                dotformat = dotformat.ToLower();
                FileStream file = new FileStream(title, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(file, textformat);
                if (dotformat == "rtf")  //如果后缀是 rtf 加载文件进来
                {
                    richTextBox1.LoadFile(title, RichTextBoxStreamType.RichText);
                }
                else
                {
                    richTextBox1.Text = reader.ReadToEnd();
                }
                file.Close();
                reader.Close();
            }
        }

        

        private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.Right)
            {
                contextMenuStrip1.PerformLayout();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selectText = ((RichTextBox)contextMenuStrip1.SourceControl).SelectedText;
            if (selectText != "")
            {
                Clipboard.SetText(selectText);
            }
        }
        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.contextMenuStrip1.SourceControl.Select();
            RichTextBox rtb = (RichTextBox)this.contextMenuStrip1.SourceControl;
            rtb.Paste();
        }

        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selectText = ((RichTextBox)contextMenuStrip1.SourceControl).SelectedText;
            if (selectText != "")
            {
                Clipboard.SetText(selectText);
            }
            ((RichTextBox)contextMenuStrip1.SourceControl).SelectedText = "";
        }

        private void 删除ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ((RichTextBox)contextMenuStrip1.SourceControl).SelectedText = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void 复制ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            string selectText = richTextBox1.SelectedText;
            if (selectText != "")
            {
                Clipboard.SetText(selectText);
            }
        }
        private void 粘贴ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            richTextBox1.Select();
            RichTextBox rtb = richTextBox1;
            rtb.Paste();
        }
        private void 删除ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = "";
        }

        private void 全选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void 全选ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ((RichTextBox)contextMenuStrip1.SourceControl).SelectAll();
        }
    }
}
