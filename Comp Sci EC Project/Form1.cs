﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;


namespace Comp_Sci_EC_Project
{
    //Using a bunch of APIs/Libraries to make things simpler
    public partial class Form1 : Form
    {

        IFirebaseConfig authconfig = new FirebaseConfig
        {
            
            BasePath = "https://ec-project-21eec.firebaseio.com/", //Database reference link
            AuthSecret = "RnnFzjMhIEWvrzK8kraCxa0Uj29z2ULrEN88hAlf" //Database key
        };

        IFirebaseClient httpclient;

        public Form1()
        {

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            httpclient = new FireSharp.FirebaseClient(authconfig); //Initializes connection

        }


        private async void button1_Click(object sender, EventArgs e)
        {
            var data = new Data //Creates a variable with two fields
            {

                Name = textBox2.Text,
                Value = textBox3.Text
            };

            SetResponse response = await httpclient.SetAsync(textBox1.Text, data);
            Data result = response.ResultAs<Data>();

        }

        private async void button3_Click(object sender, EventArgs e)
        {
            FirebaseResponse response = await httpclient.GetAsync(textBox1. Text);
            Data obj = response.ResultAs<Data>();

            try
            {
                textBox2.Text = obj.Name;
                textBox3.Text = obj.Value;
            }
            catch (Exception except)
            {
                MessageBox.Show("Something went Wrong!\n" + except);
            }

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            FirebaseResponse response = await httpclient.DeleteAsync(textBox1.Text);
            button3_Click(sender, e);
            
        }
    }
}
