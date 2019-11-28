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
            CreateMyTabPage("New 1");
            if (this.toolStripComboBox2.Items.Count == 0)
                for (int i = 1; i < 31; i++)
                    this.toolStripComboBox2.Items.Add(i.ToString());
            this.toolStripComboBox1.Text = (GetActiveEditor().SelectionFont.Name);
            this.toolStripComboBox2.Text = (GetActiveEditor().SelectionFont.Size.ToString());
            if (this.toolStripComboBox1.Items.Count == 0)
                foreach (FontFamily family in FontFamily.Families)
                    this.toolStripComboBox1.Items.Add(family.Name);
            
            //CreateMyChildForm();
        }
        int childCount = 0;
        int newfilenum = 2;
        String[] newfilename = new String[1000];
        private void CreateMyChildForm()
        {
            Form child = new Form();
            childCount++;
            String formText = "Child " + childCount;
            child.Text = formText;
            child.MdiParent = this;
            child.Show();
        }

        private void ischange(object sender, EventArgs e) ////
        {
            if (!tabControl1.SelectedTab.Text.Contains("*")){
                tabControl1.SelectedTab.Text += "*";
            }
        }
        public RichTextBox CreateMyTabPage(String title) //创建新的文档窗口TabPage
        {
            TabPage page = new TabPage(title);
            RichTextBox rtb = new RichTextBox(); //创建富文本框控件
            rtb.TextChanged += ischange; //文本框内容被修改后触发ischange事件

            //富文本框属性初始化
            rtb.Dock = DockStyle.Fill;
            rtb.ContextMenuStrip = this.contextMenuStrip1;
            rtb.Font = new System.Drawing.Font("微软雅黑", 15F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            rtb.EnableAutoDragDrop = true;
            rtb.HideSelection = false;

            page.Controls.Add(rtb); //将富文本框控件添加到TabPage控件中
            tabControl1.TabPages.Add(page); //将TabPage控件添加到TabControl控件中
            tabControl1.SelectedTab = page; //选中当前TabPage窗口
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
        
        private String title = "New 1";  //保存打开的文件的标题
        Encoding textformat = Encoding.Default;          //设置文本的格式为 UTF-8
        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)//打开文件
        {
            openFileDialog1.Filter = "文本文件|*.txt;*.html;*.docx;*.doc;*.rtf|所有文件|*.*"; //文件打开的过滤器
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                title = openFileDialog1.FileName; //赋值窗口里选中的文件名给title
                this.Text = title; //显示打开的文件名
                richTextBox1.Modified = false;
                String dotformat = title.Substring(title.LastIndexOf(".") + 1);
                //获取文件格式
                dotformat = dotformat.ToLower();
                FileStream file = new FileStream(title, FileMode.Open,FileAccess.Read);
                StreamReader reader = new StreamReader(file, textformat);
                RichTextBox rtb = CreateMyTabPage(title); //为打开的文件新建TabPage窗口
                if (dotformat == "rtf")  //加载rtf格式文件
                {
                    rtb.LoadFile(title, RichTextBoxStreamType.RichText);
                    //富文本框加载打开文件内容
                }
                else //加载其它格式文件
                {
                    rtb.Text = reader.ReadToEnd();
                }
                file.Close();
                reader.Close();
            }
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e) //保存文件
        {
            if (tabControl1.SelectedTab.Text.Contains("."))//判断当前文件是否为新建文件
            {
                String dotformat = title.Substring(title.LastIndexOf(".") + 1);
                //获取当前文件的尾缀
                dotformat = dotformat.ToLower();
                if (dotformat == "rtf")
                    GetActiveEditor().SaveFile(title,RichTextBoxStreamType.RichText); //保存选中TabPage的内容
                else if (dotformat == "uni")
                    GetActiveEditor().SaveFile(title,RichTextBoxStreamType.UnicodePlainText); //保存选中TabPage的内容
                else
                    GetActiveEditor().SaveFile(title, RichTextBoxStreamType.PlainText); //保存选中TabPage的内容
            }
            else //如果当前文件是新建文件则触发另存为事件
                另存为ToolStripMenuItem_Click(sender, e);

            if (tabControl1.SelectedTab.Text.Contains("*"))
                tabControl1.SelectedTab.Text = tabControl1.SelectedTab.Text.Replace("*", "");
        }

        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e) //文件另存为
        {
            saveFileDialog1.Filter = "文本文件|*.txt;*.html;*.docx;*.doc;*.rtf|所有文件|*.*";//文件保存的过滤器
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                title = saveFileDialog1.FileName;//赋值窗口里选中的文件名给title
                this.Text = title;

                //根据过滤的文件格式保存文件
                if (saveFileDialog1.FilterIndex == 1)
                    richTextBox1.SaveFile(title, RichTextBoxStreamType.RichText);
                else if (saveFileDialog1.FilterIndex == 2)
                    richTextBox1.SaveFile(title, RichTextBoxStreamType.PlainText);
                else if (saveFileDialog1.FilterIndex == 3)
                    richTextBox1.SaveFile(title, RichTextBoxStreamType.UnicodePlainText);
                else 
                    richTextBox1.SaveFile(title, RichTextBoxStreamType.RichText);
            }
            if (tabControl1.SelectedTab.Text.Contains("*"))
                tabControl1.SelectedTab.Text = tabControl1.SelectedTab.Text.Replace("*", "");
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

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e) //新建文件
        {
            title = "New " + newfilenum.ToString(); //为新建文件命名
            RichTextBox rtb = CreateMyTabPage(title); //为新建文件命名创造TabPage窗口
            newfilenum += 1;//新建文件数加1
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

        private void 复制ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            string selectText = GetActiveEditor().SelectedText;//获取当前TabPage窗口里选中的文本
            if (selectText != "")
            {
                Clipboard.SetText(selectText); //复制选中文本到粘贴板中
            }
        }
        private void 粘贴ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            GetActiveEditor().Select();//获取当前TabPage窗口里选中的文本
            RichTextBox rtb = GetActiveEditor();
            rtb.Paste();//将粘贴板上的内容粘贴到当前TabPage窗口里选中的地方
        }
        private void 剪切ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            string selectText = GetActiveEditor().SelectedText;//获取当前TabPage窗口里选中的文本
            if (selectText != "")
            {
                Clipboard.SetText(selectText);//复制选中文本到粘贴板中
            }
            GetActiveEditor().SelectedText = "";//删除选中文本
        }
        private void 全选ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            GetActiveEditor().SelectAll();//选中当前TabPage窗口里所有内容
        }

        private void 全选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((RichTextBox)contextMenuStrip1.SourceControl).SelectAll();
        }

        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e) ////
        {
            保存ToolStripMenuItem_Click(sender,e);
            if (tabControl1.SelectedTab.Text.Contains("New "))
            {
                newfilenum -= 1;
            }
            tabControl1.TabPages.Remove(tabControl1.SelectedTab);
            if(tabControl1.TabPages.Count == 0)
            {
                CreateMyTabPage("New 1");
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
            GetActiveEditor().SelectionFont = new Font(GetActiveEditor().Font.FontFamily, size, GetActiveEditor().Font.Style);
        }

        private void 查找和替换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindAndReplace findAndReplaceForm = new FindAndReplace();
            findAndReplaceForm.ShowDialog();
        }

        private void 退出XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            关闭ToolStripMenuItem_Click(sender, e);
        }
    }
}
