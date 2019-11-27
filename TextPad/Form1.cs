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
using static TextPad.FindAndReplace;

namespace TextPad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CreateMyTabPage("New File");
            this.toolStripComboBox1.Text = (GetActiveEditor().SelectionFont.Name);
            this.toolStripComboBox2.Text = (GetActiveEditor().SelectionFont.Size.ToString());
            if (this.toolStripComboBox1.Items.Count == 0)
                foreach (FontFamily family in FontFamily.Families)
                    this.toolStripComboBox1.Items.Add(family.Name);
            if (this.toolStripComboBox2.Items.Count == 0)
                for (int i = 1; i < 31; i++)
                    this.toolStripComboBox2.Items.Add(i.ToString());
            
            //CreateMyChildForm();
        }
        int childCount = 0;
        private void CreateMyChildForm()
        {
            Form child = new Form();
            childCount++;
            String formText = "Child " + childCount;
            child.Text = formText;
            child.MdiParent = this;
            child.Show();
        }
        public RichTextBox CreateMyTabPage(String title)
        {
            TabPage page = new TabPage(title);
            RichTextBox rtb = new RichTextBox();
            rtb.Dock = DockStyle.Fill;
            rtb.ContextMenuStrip = this.contextMenuStrip1;
            rtb.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            rtb.EnableAutoDragDrop = true;
            rtb.HideSelection = false;

            //this.richTextBox1.TabIndex = 0;

            page.Controls.Add(rtb);
            tabControl1.TabPages.Add(page);
            tabControl1.SelectedTab = page;
            return rtb;

        }
        private RichTextBox GetActiveEditor()
        {
            TabPage tp = tabControl1.SelectedTab;
            RichTextBox rtb = null;
            if (tp != null)
            {
                rtb = tp.Controls[0] as RichTextBox;
            }
            return rtb;
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
            Console.WriteLine("opening!!");
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
                RichTextBox rtb = CreateMyTabPage(title);
                if (dotformat == "rtf")  //如果后缀是 rtf 加载文件进来
                {
                    rtb.LoadFile(title, RichTextBoxStreamType.RichText);
                }
                else
                {
                    rtb.Text = reader.ReadToEnd();
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
                    GetActiveEditor().SaveFile(title, RichTextBoxStreamType.RichText);
                else if (dotformat == "uni")
                    GetActiveEditor().SaveFile(title, RichTextBoxStreamType.UnicodePlainText);
                else
                    GetActiveEditor().SaveFile(title, RichTextBoxStreamType.PlainText);
            }
            else
            {
                另存为ToolStripMenuItem_Click(sender,e);
            }
        }
        private void 保存ToolStripMenuItem_Click()
        {
            if (title != "Untitled")
            {
                String dotformat = title.Substring(title.LastIndexOf(".") + 1);
                dotformat = dotformat.ToLower();
                if (dotformat == "rtf")
                    GetActiveEditor().SaveFile(title, RichTextBoxStreamType.RichText);
                else if (dotformat == "uni")
                    GetActiveEditor().SaveFile(title, RichTextBoxStreamType.UnicodePlainText);
                else
                    GetActiveEditor().SaveFile(title, RichTextBoxStreamType.PlainText);
            }
            else
            {
                //另存为ToolStripMenuItem_Click(sender, e);
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
            
            saveFileDialog1.Filter = "文本文件|*.txt;*.html;*.docx;*.doc;*.rtf|所有文件|*.*"; //文件打开的过滤器
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                
                title = saveFileDialog1.FileName;
                this.Text = title;                  //显示打开的文件名
                richTextBox1.Modified = false;
                String dotformat = title.Substring(title.LastIndexOf(".") + 1);//获取文件格式
                dotformat = dotformat.ToLower();
                FileStream file = new FileStream(title, FileMode.Create);
                StreamReader reader = new StreamReader(file, textformat);
                RichTextBox rtb = CreateMyTabPage(title);
                if (dotformat == "rtf")  //如果后缀是 rtf 加载文件进来
                {
                    rtb.LoadFile(title, RichTextBoxStreamType.RichText);
                }
                else
                {
                    rtb.Text = reader.ReadToEnd();
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
            string selectText = GetActiveEditor().SelectedText;
            if (selectText != "")
            {
                Clipboard.SetText(selectText);
            }
        }
        private void 粘贴ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            GetActiveEditor().Select();
            RichTextBox rtb = GetActiveEditor();
            rtb.Paste();
        }
        private void 删除ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            GetActiveEditor().SelectedText = "";
        }
        private void 剪切ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            string selectText = GetActiveEditor().SelectedText;
            if (selectText != "")
            {
                Clipboard.SetText(selectText);
            }
            GetActiveEditor().SelectedText = "";
        }

        private void 全选ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            GetActiveEditor().SelectAll();
        }

        private void 全选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((RichTextBox)contextMenuStrip1.SourceControl).SelectAll();
        }

        private void TabPage1_Click(object sender, EventArgs e)
        {

        }

        private void MenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }


        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            保存ToolStripMenuItem_Click();
            tabControl1.TabPages.Remove(tabControl1.SelectedTab);
            if(tabControl1.TabPages.Count == 0)
            {
                CreateMyTabPage("New File");
            }
        }

        private void ToolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToolStripComboBox combo1 = this.toolStripComboBox1;
            ToolStripComboBox combo2 = this.toolStripComboBox2;
            int size = Convert.ToInt32(this.toolStripComboBox2.SelectedItem.ToString());
            if (combo1.SelectedItem == null) return;
            string ss = combo1.SelectedItem.ToString().Trim();
            GetActiveEditor().SelectionFont = new Font(ss, size, GetActiveEditor().SelectionFont.Style);
        }

        private void ToolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.toolStripComboBox2.Items.Count == 0)
                for(int i = 1;i<31;i++)
                    this.toolStripComboBox2.Items.Add(i.ToString());
            int size;
            if (toolStripComboBox2.SelectedItem == null) return;
            size = Convert.ToInt32(this.toolStripComboBox2.SelectedItem.ToString());
            GetActiveEditor().SelectionFont = new Font(GetActiveEditor().SelectionFont.FontFamily, size, GetActiveEditor().SelectionFont.Style);
        }

        private void 搜索SToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 查找和替换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindAndReplace findAndReplaceForm = new FindAndReplace();
            //打开非模式对话框
            findAndReplaceForm.Show();
            //findAndReplaceForm.ShowDialog();

            //创建委托实例，并使用委托的多播使得每一次查找的下一次不用重头开始
            findAndReplaceForm.SearchEvent += new FindAndReplace.SearchEventHandle(MySearch);    //订阅查询事件
            findAndReplaceForm.ReplaceEvent += new FindAndReplace.ReplaceEventHandle(MyReplace); //订阅替换事件

        }

        private void MySearch(object sender, SearchEventArgClass e)
        {
            string strToSearch = e.SearchString;
            if (strToSearch.Length == 0)
                return;

            //int start = richTextBox1.SelectionStart;
            int start = GetActiveEditor().SelectionStart;

            //start = richTextBox1.Find(strToSearch, start, RichTextBoxFinds.MatchCase);
            start = GetActiveEditor().Find(strToSearch, start, RichTextBoxFinds.MatchCase);
            if (start == -1)
            {
                MessageBox.Show("已查找到文档的结尾", "查找结束对话框");
                start = 0;
                GetActiveEditor().Select();
            }
            else
            {   
                //查找下一处，从该位置开始查询
                start = start + strToSearch.Length;
                //选中查询到的字符串
                //richTextBox1.Select();
                //richTextBox1.Focus();
                GetActiveEditor().Select();
                //GetActiveEditor().Focus();
            }
            
        }


        private void MyReplace(object sender, ReplaceEventArgClass e)
        {

            string strToSearch = e.SearchString;    //要替换的字符串

            string strToReplace = e.ReplaceString;  //新的字符串
            //如果查找字符为空或新的字符串为空，则不反应
            if (strToReplace.Length == 0 || GetActiveEditor().SelectionLength == 0)
                return;

            //将选中的字符串替换成新的字符串
            GetActiveEditor().SelectedText = strToReplace;

            //查找起始位置
            int start = GetActiveEditor().SelectionStart;
            start = GetActiveEditor().Find(strToSearch, start, RichTextBoxFinds.MatchCase);

            //查询到尾部，结束查询
            if (start == -1)
            {

                MessageBox.Show("已查找到文档的结尾", "查找结束对话框");
                GetActiveEditor().Select();
                start = 0;
            }
            else
            {
                //查找下一处
                start = start + strToSearch.Length;
                //选中查询到的字符串
                GetActiveEditor().Select();
                //GetActiveEditor().Focus();
            }
        }

  

        private void DDrop(object sender, DragEventArgs e)
        {
            //e.Effect = DragDropEffects.Copy;
            //string[] str = (string[])e.Data.GetData(DataFormats.FileDrop, true);
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            Console.WriteLine("!!!!!!!!");

        }

        private void DEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
                Console.WriteLine("Draging1!!!");
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
    }
}
