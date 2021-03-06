﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 解数独 {
    public partial class Form1 : Form {
        private string r = "";
        private bool x = false;
        private int[, ] pu = new int[9, 9];
        public Form1 () {
            InitializeComponent ();
        }
        private void button1_Click (object sender, EventArgs e) {
            fu (pu); //初次赋值
            label3.Text = ""; //还原
            for (int i = 0; i < 9; i++) //检测是否有明显冲突
            {
                for (int j = 0; j < 9; j++) {
                    if (!IsValid (i, j)) {
                        label3.Text = "无解";
                    }
                }
            }
            if (label3.Text != "无解") //如果没有冲突  则开始求解
            {
                GetAnswer (0);
            }
            fu (pu); //赋值
            for (int i = 0; i < 9; i++) //判断是否得出结果  如没有结果 则无解
            {
                for (int j = 0; j < 9; j++) {
                    if (pu[i, j] == 0) {
                        label3.Text = "无解";
                    }
                }
            }
        }
        void fu (int[, ] bu) //赋值
        {
            for (int i = 0; i < 9; i++) {
                for (int j = 0; j < 9; j++) {
                    bu[i, j] = tableLayoutPanel1.Controls["textBox" + (i * 9 + j + 1).ToString ()].Text == "" ? 0 :
                        Convert.ToInt32 (tableLayoutPanel1.Controls["textBox" + (i * 9 + j + 1).ToString ()].Text);
                }
            }
        }
        void uf (int[, ] bu) //显示
        {
            for (int i = 0; i < 9; i++) {
                for (int j = 0; j < 9; j++) {
                    tableLayoutPanel1.Controls["textBox" + (i * 9 + j + 1).ToString ()].Text = bu[i, j].ToString ();
                }
            }
        }

        bool IsValid (int i, int j) //判断是否冲突
        {
            int n = pu[i, j];
            int[] query = new int[9] { 0, 0, 0, 3, 3, 3, 6, 6, 6 };
            int t, u;
            for (t = 0; t < 9; t++) {
                if ((t != i && pu[t, j] == n && n != 0) || (t != j && pu[i, t] == n && n != 0)) //判断行列
                    return false;
            }
            for (t = query[i]; t < query[i] + 3; t++) //判断9格
            {
                for (u = query[j]; u < query[j] + 3; u++) {
                    if ((t != i || u != j) && pu[t, u] == n && n != 0)
                        return false;
                }
            }
            return true;
        }
        void GetAnswer (int n) {
            if (n == 81) { //是否已经是最后一个格子
                uf (pu);
                x = true;
            }
            if (x)
                return;
            int i = n / 9, j = n % 9;

            if (pu[i, j] != 0) { //如果当前格子不需要填数字，就跳到下一个格子
                GetAnswer (n + 1);
                return;
            }

            for (int k = 0; k < 9; k++) {
                pu[i, j]++; //当前格子进行尝试所有解
                if (IsValid (i, j))
                    GetAnswer (n + 1); //验证通过，就继续下一个
            }
            pu[i, j] = 0; //如果上面的单元无解，就还原、回溯
            return;
        }

        private void button2_Click (object sender, EventArgs e) {
            for (int i = 0; i < 9; i++) {
                for (int j = 0; j < 9; j++) {
                    tableLayoutPanel1.Controls["textBox" + (i * 9 + j + 1).ToString ()].Text = "";
                }
            }
            textBox1.Focus ();
            x = false;
            label3.Text = "";
        }
        private void Form1_KeyDown (object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.Enter:
                    button1_Click (null, null);
                    break;
                case Keys.Escape:
                    bool z = false;
                    fu (pu);
                    for (int i = 0; i < 9; i++) {
                        for (int j = 0; j < 9; j++) {
                            if (pu[i, j] != 0) {
                                z = true;
                                break;
                            }
                        }
                    }
                    if (z) {
                        button2_Click (null, null);
                    } else {
                        Application.Exit ();
                    }
                    break;
                case Keys.D:
                case Keys.Right:
                    if ((ActiveControl.TabIndex) % 9 == 0) {
                        tableLayoutPanel1.Controls["textBox" + (ActiveControl.TabIndex - 8).ToString ()].Focus ();
                        break;
                    } else {
                        tableLayoutPanel1.Controls["textBox" + (ActiveControl.TabIndex + 1).ToString ()].Focus ();
                        break;
                    }
                case Keys.A:
                case Keys.Left:
                    if ((ActiveControl.TabIndex) % 9 == 1) {
                        tableLayoutPanel1.Controls["textBox" + (ActiveControl.TabIndex + 8).ToString ()].Focus ();
                        break;
                    } else {
                        tableLayoutPanel1.Controls["textBox" + (ActiveControl.TabIndex - 1).ToString ()].Focus ();
                        break;
                    }
                case Keys.S:
                case Keys.Down:
                    if ((ActiveControl.TabIndex - 1) / 9 == 8) {
                        tableLayoutPanel1.Controls["textBox" + (ActiveControl.TabIndex - 72).ToString ()].Focus ();
                        break;
                    } else {
                        tableLayoutPanel1.Controls["textBox" + (ActiveControl.TabIndex + 9).ToString ()].Focus ();
                        break;
                    }
                case Keys.W:
                case Keys.Up:
                    if ((ActiveControl.TabIndex - 1) / 9 == 0) {
                        tableLayoutPanel1.Controls["textBox" + (ActiveControl.TabIndex + 72).ToString ()].Focus ();
                        break;
                    } else {
                        tableLayoutPanel1.Controls["textBox" + (ActiveControl.TabIndex - 9).ToString ()].Focus ();
                        break;

                    }
            }

        }
        private void Form1_Load (object sender, EventArgs e) {
            label3.Text = "";
            label1.Focus ();
            for (int i = 0; i < 9; i++) {
                for (int j = 1; j <= 9; j++) {
                    tableLayoutPanel1.Controls["textBox" + (i * 9 + j).ToString ()].KeyPress += new System.Windows.Forms.KeyPressEventHandler (SKeyPress);
                }
            }

        }

        private void Form1_KeyUp (object sender, KeyEventArgs e) { }
        private void SKeyPress (object sender, KeyPressEventArgs e) {
            if (!char.IsNumber (e.KeyChar) && !char.IsControl (e.KeyChar)) {
                e.Handled = true;
            }
        }
    }
}