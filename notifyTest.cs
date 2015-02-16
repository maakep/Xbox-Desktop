using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Windows.Forms;

namespace XboxDesktop
{
    class notifyTest
    {
        private System.Windows.Forms.NotifyIcon notifyIcon1 = new System.Windows.Forms.NotifyIcon();
        private System.Windows.Forms.ContextMenu contextMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuPlayer1;
        private System.Windows.Forms.MenuItem menuPlayer2;
        private System.Windows.Forms.MenuItem menuPlayer3;
        private System.Windows.Forms.MenuItem menuPlayer4;
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Form form;
        Game1 game;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Game1));
        

        public void init(Game1 game)
        {
            this.game = game;
            game.IsMouseVisible = true;
            form = (System.Windows.Forms.Form)System.Windows.Forms.Control.FromHandle(game.Window.Handle);
            form.FormClosing += new System.Windows.Forms.FormClosingEventHandler(formClose);

            form.BackColor = System.Drawing.SystemColors.ActiveBorder;
            form.TransparencyKey = System.Drawing.SystemColors.ActiveBorder;
            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            form.ClientSize = new System.Drawing.Size(400, 285);

            form.Text = "Xbox Desktop";
            form.BackgroundImage = Resource1.background;
            int height = Screen.FromControl(form).WorkingArea.Height;
            int width = Screen.FromControl(form).WorkingArea.Width;
            form.SetBounds(width-760, height-540, 760, 538);
            form.TopMost = true;
            form.Opacity = 0.7;
            this.components = new System.ComponentModel.Container();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuPlayer1 = new System.Windows.Forms.MenuItem();
            this.menuPlayer2 = new System.Windows.Forms.MenuItem();
            this.menuPlayer3 = new System.Windows.Forms.MenuItem();
            this.menuPlayer4 = new System.Windows.Forms.MenuItem();


            notifyIcon1.Icon = Resource1.Game;

            this.menuItem1.Index = 2;
            this.menuItem1.Text = "E&xit";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);

            this.menuItem2.Index = 1;
            this.menuItem2.Text = "Open";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);

            this.menuItem4.Index = 3;
            this.menuItem4.Text = "Player";

            this.menuPlayer1.Text = "1";
            this.menuPlayer1.Click += new System.EventHandler(this.menuPlayer_Click);
            this.menuPlayer2.Text = "2";
            this.menuPlayer2.Click += new System.EventHandler(this.menuPlayer_Click);
            this.menuPlayer3.Text = "3";
            this.menuPlayer3.Click += new System.EventHandler(this.menuPlayer_Click);
            this.menuPlayer4.Text = "4";
            this.menuPlayer4.Click += new System.EventHandler(this.menuPlayer_Click);
            
            this.menuItem3.Text = "Xbox Desktop";
            this.menuItem3.Enabled = false;

            this.contextMenu1.MenuItems.AddRange(
                new System.Windows.Forms.MenuItem[] 
                { 
                   this.menuItem3,
                   this.menuItem4,
                   this.menuItem2,
                   this.menuItem1
                });

            this.menuItem4.MenuItems.AddRange(
                new System.Windows.Forms.MenuItem[]
                {
                    this.menuPlayer1,
                    this.menuPlayer2,
                    this.menuPlayer3,
                    this.menuPlayer4
                });

            notifyIcon1.ContextMenu = this.contextMenu1;
            
            notifyIcon1.Text = "Xbox Desktop";
            notifyIcon1.DoubleClick += notifyIcon1_DoubleClick;
            notifyIcon1.BalloonTipText = "Xbox Desktop is running in your taskbar, double click me for help!";
            notifyIcon1.Visible = true;
            notifyIcon1.ShowBalloonTip(1);
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            form.Visible = true;
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            form.Dispose();
        }

        private void menuPlayer_Click(object sender, EventArgs e)
        {
            MenuItem temp = (MenuItem)sender;
            int index = int.Parse(temp.Text);
            game.Player(index);
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            
            if (form.Visible == true)
            {
                form.Visible = false;
            }
            else
            {
                form.Visible = true;
            }
            if (form.WindowState == FormWindowState.Minimized)
            {
                form.WindowState = FormWindowState.Normal;
            }
        }
       

        private void formClose(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (e.CloseReason.ToString() == "UserClosing")
            {
                e.Cancel = true;
                notifyIcon1.BalloonTipText = "Still running...";
                notifyIcon1.ShowBalloonTip(400);
            }
            form.Visible = false;
        }

        public void HideAndShow(Game1 game)
        {
            form = (System.Windows.Forms.Form)System.Windows.Forms.Control.FromHandle(game.Window.Handle);
            if (form.Visible)
            {
                form.Visible = false;
            }
            else
            {
                form.Visible = true;
            }
            
        }
    }
}
