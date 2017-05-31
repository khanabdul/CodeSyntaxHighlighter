using ColorCode;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

namespace CodeSyntaxHighLiter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.AddRange(Languages.All.Select(a => a.Id).ToArray());
            label4.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string code = CodeTextBox.Text;//File.ReadAllText(Server.MapPath("~/Test.txt"));
                if (!string.IsNullOrEmpty(code))
                {

                    if (comboBox1.SelectedItem != null && !string.IsNullOrEmpty(comboBox1.SelectedItem.ToString()))
                    {


                        string finalCode = new CodeColorizer().Colorize(code, Languages.FindById(comboBox1.SelectedItem.ToString()));
                        ResultTextBox.Text = finalCode;
                        DisplayHtml(finalCode); label4.Text = "Hurray!!...I Am Genious.Don't Say Thanks...My Pleasure.";
                        label4.Visible = true;
                        label6.Text = Languages.FindById(comboBox1.SelectedItem.ToString()).Name;
                        label6.Visible = true;
                    }

                    else
                    {
                        label6.Visible = false;
                        label4.Visible = true;
                        label4.Text = "Dammit!!!...I Am Mad,Please Select Language ";
                    }
                }
                else
                {
                    label6.Visible = false;
                    label4.Visible = true;
                    label4.Text = "Hungry!!!...Please Feed Some Actual Code Text.";
                }
            }
            catch (Exception)
            {
                label4.Visible = true;
                label4.Text = "Help!!!...I Need To Be Hospitalized...Try Again Or Contact Abdul.";
            }

        }


        private void DisplayHtml(string html)
        {
            webBrowser1.Navigate("about:blank");
            if (webBrowser1.Document != null)
            {
                webBrowser1.Document.Write(string.Empty);
            }
            webBrowser1.DocumentText = html;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            
            if (!string.IsNullOrEmpty(ResultTextBox.Text))
            {
                Clipboard.SetText(ResultTextBox.Text);
            }
            else
            {
                label6.Visible = false;
                label4.Visible = true;
                label4.Text = "Crap!!! Nothing To Copy..";
            }
        }
        public class JsonTag
        {
            public JsonTag(JsonReader reader)
            {
                TokenType = reader.TokenType;
                Value = reader.Value;
                ValueType = reader.ValueType;
            }

            public JsonToken TokenType { get; set; }
            public object Value { get; set; }
            public Type ValueType { get; set; }
        }
        private TreeNode Json2Tree(JObject obj)
        {
            //create the parent node
            TreeNode parent = new TreeNode();
            //loop through the obj. all token should be pair<key, value>
            foreach (var token in obj)
            {
                //change the display Content of the parent
                parent.Text = token.Key.ToString();
                //create the child node
                TreeNode child = new TreeNode();
                child.Text = token.Key.ToString();
                //check if the value is of type obj recall the method
                if (token.Value.Type.ToString() == "Object")
                {
                    // child.Text = token.Key.ToString();
                    //create a new JObject using the the Token.value
                    JObject o = (JObject)token.Value;
                    //recall the method
                    child = Json2Tree(o);
                    //add the child to the parentNode
                    parent.Nodes.Add(child);
                }
                //if type is of array
                else if (token.Value.Type.ToString() == "Array")
                {
                    int ix = -1;
                    //  child.Text = token.Key.ToString();
                    //loop though the array
                    foreach (var itm in token.Value)
                    {
                        //check if value is an Array of objects
                        if (itm.Type.ToString() == "Object")
                        {
                            TreeNode objTN = new TreeNode();
                            //child.Text = token.Key.ToString();
                            //call back the method
                            ix++;

                            JObject o = (JObject)itm;
                            objTN = Json2Tree(o);
                            child.Text = token.Key.ToString();
                            objTN.Text = token.Key.ToString() + "[" + ix + "]";
                            child.Nodes.Add(objTN);
                            
                            //parent.Nodes.Add(child);
                        }
                        //regular array string, int, etc
                        else if (itm.Type.ToString() == "Array")
                        {
                            ix++;
                            TreeNode dataArray = new TreeNode();
                            foreach (var data in itm)
                            {
                                dataArray.Text = token.Key.ToString() + "[" + ix + "]";
                                dataArray.Nodes.Add(data.ToString());
                            }
                            child.Nodes.Add(dataArray);
                        }

                        else
                        {
                            child.Nodes.Add(itm.ToString());
                        }
                    }
                    parent.Nodes.Add(child);
                }
                else
                {
                    //if token.Value is not nested
                    // child.Text = token.Key.ToString();
                    //change the value into N/A if value == null or an empty string 
                    if (token.Value.ToString() == "")
                        child.Nodes.Add("N/A");
                    else
                        child.Nodes.Add(token.Value.ToString());
                    parent.Nodes.Add(child);
                }
            }
            return parent;

        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string json = CodeTextBox.Text;
                JObject obj = JObject.Parse(json);
                tvw_display.Nodes.Clear();
                TreeNode parent = Json2Tree(obj);
                parent.Text = "Root Object";
                tvw_display.Nodes.Add(parent);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
            }
        }

    }
}
