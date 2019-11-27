using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextPad
{
    public partial class FindAndReplace : Form
    {
        public FindAndReplace()
        {
            InitializeComponent();
        }

        //定义handler委托类型
        public delegate void SearchEventHandle(object sender, SearchEventArgClass e);
        public delegate void ReplaceEventHandle(object sender, ReplaceEventArgClass e);

        public event SearchEventHandle SearchEvent; //定义查找事件

        public event ReplaceEventHandle ReplaceEvent; //定义替换事件




        private void Find_Btn_Click(object sender, EventArgs e)
        {
            //如果查询内容为空，无反应
            if (this.textBox1.Text.Length == 0)
                return;
            //触发查找事件
            else if(SearchEvent != null)
            {
                SearchEventArgClass ee = new SearchEventArgClass(this.textBox1.Text);
                SearchEvent(sender, ee);
            }
        }

        private void Next_Btn_Click(object sender, EventArgs e)
        {

            //if (this.textBox2.Text.Length == 0) return;

            if (ReplaceEvent != null)
            {

                ReplaceEventArgClass ee = new ReplaceEventArgClass(this.textBox1.Text, this.textBox1.Text);

                ReplaceEvent(sender, ee);
            }
        }

        private void Replace_Btn_Click(object sender, EventArgs e)
        {
            if (this.textBox2.Text.Length == 0) return;

            if (ReplaceEvent != null)
            {

                ReplaceEventArgClass ee = new ReplaceEventArgClass(this.textBox1.Text, this.textBox2.Text);

                ReplaceEvent(sender, ee);
            }
        }

        public class SearchEventArgClass : EventArgs
        {
            private string searchString;
            public SearchEventArgClass(string str)
            {
                this.searchString = str;
            }
            public string SearchString
            {
                get { return this.searchString; }
                set { this.searchString = value; }
            }
        }

        public class ReplaceEventArgClass : EventArgs

        {

            private string searchString;

            private string replaceString;



            public ReplaceEventArgClass(string str1, string str2)

            {

                this.searchString = str1;

                this.replaceString = str2;

            }

            public string SearchString

            {

                get { return this.searchString; }

                set { this.searchString = value; }

            }

            public string ReplaceString

            {

                get { return this.replaceString; }

                set { this.replaceString = value; }

            }

        }

     
    }

    

    

}
