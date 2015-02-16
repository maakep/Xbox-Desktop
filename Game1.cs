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
using System.IO;


namespace XboxDesktop
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GamePadState newState;
        GamePadState oldState;
        notifyTest nt = new notifyTest();
        
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;
        public const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        public const int MOUSEEVENTF_RIGHTUP = 0x10;
        public const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        public const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        public const int MOUSEEVENTF_WHEEL = 0x800;
        public const int MOUSEEVENTF_HWHEEL = 0x1000;

        int sens = 20;
        int player = 1;
        PlayerIndex controller = PlayerIndex.One;

        bool everyOther = false;
        double mouseX;
        double mouseY;
        double scrollY;
        double scrollX;

        public void Player(int index)
        {
            this.player = index;
            switch(index)
            {
                case 1:
                    controller = PlayerIndex.One;
                    break;
                case 2:
                    controller = PlayerIndex.Two;
                    break;
                case 3:
                    controller = PlayerIndex.Three;
                    break;
                case 4:
                    controller = PlayerIndex.Four;
                    break;
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(controller).IsConnected)
            {
                newState = GamePad.GetState(controller);
            }
            else
            {

            }

                                                                                        // back + start = shutdown
            if (newState.Buttons.Back == ButtonState.Pressed)
            {
                if (oldState.Buttons.Start == ButtonState.Pressed)
                {
                    System.Diagnostics.Process.Start("shutdown", "/s /t 10");
                    System.Threading.Thread.Sleep(1000);
                }
            }

                                                                                        //right + left shoulder = abort shutdown
            if (newState.Buttons.LeftShoulder == ButtonState.Pressed)
            {
                if (oldState.Buttons.RightShoulder == ButtonState.Pressed)
                {
                    System.Diagnostics.Process.Start("shutdown", "/a");
                    System.Threading.Thread.Sleep(1000);
                }
            }

            if (newState.Buttons.RightStick == ButtonState.Pressed && oldState.Buttons.RightStick != ButtonState.Pressed )
            {
                nt.HideAndShow(this);
            }

            if (newState.Buttons.LeftShoulder == ButtonState.Pressed)
            {
                sens = 5;
            }
            else if (sens == 5 || sens == 60)
            {
                sens = 20;
            }

            if (newState.Buttons.RightShoulder == ButtonState.Pressed)
            {
                sens = 60;
            }
            
                                                                                        // right trigger + y,x,a,b = ctrl w, ctrl c, ctrl v, ctrl tab
            if (newState.Triggers.Right == 1)
            {
                if (newState.Buttons.Y == ButtonState.Pressed && oldState.Buttons.Y != ButtonState.Pressed)
                {
                    System.Windows.Forms.SendKeys.Send("^{w}");
                }
                else if (newState.Buttons.X == ButtonState.Pressed && oldState.Buttons.X != ButtonState.Pressed)
                {
                    System.Windows.Forms.SendKeys.Send("^{c}");
                }
                else if (newState.Buttons.A == ButtonState.Pressed && oldState.Buttons.A != ButtonState.Pressed)
                {
                    System.Windows.Forms.SendKeys.Send("^{v}");
                }
                else if (newState.Buttons.B == ButtonState.Pressed && oldState.Buttons.B != ButtonState.Pressed)
                {
                    System.Windows.Forms.SendKeys.Send("^{TAB}");
                }
            }
            if (newState.Triggers.Right == 0 && oldState.Triggers.Right == 1)
            {
                //when released
            }
                                                                                        //left trigger + Y, X, A, B = alt f4,  Backspace, volume, alt tab
            if (newState.Triggers.Left == 1)
            {
                if (newState.Buttons.Y == ButtonState.Pressed && oldState.Buttons.Y != ButtonState.Pressed)
                {
                    System.Windows.Forms.SendKeys.Send("%{F4}");
                }
                if (newState.Buttons.X == ButtonState.Pressed)
                {
                    System.Windows.Forms.SendKeys.Send("{BS}");
                    System.Threading.Thread.Sleep(30);
                    if (oldState.Buttons.X != ButtonState.Pressed)
                    {
                        System.Threading.Thread.Sleep(400);
                    }
                }
                if (newState.Buttons.A == ButtonState.Pressed && oldState.Buttons.A != ButtonState.Pressed)
                {
                    System.Diagnostics.Process.Start("SndVol.exe");
                }
                if (newState.Buttons.B == ButtonState.Pressed && oldState.Buttons.B != ButtonState.Pressed)
                {
                    System.Windows.Forms.SendKeys.Send("%{TAB}");
                }
            }
                                                                                    // start = enter
            if (newState.Buttons.Start == ButtonState.Pressed && oldState.Buttons.Back != ButtonState.Pressed && oldState.Buttons.Start != ButtonState.Pressed)
            {
                System.Windows.Forms.SendKeys.Send("{ENTER}");
            }
                                                                                    // back = ctrl t
            if (newState.Buttons.Back == ButtonState.Pressed && oldState.Buttons.Back != ButtonState.Pressed && oldState.Buttons.Start != ButtonState.Pressed)
            {
                System.Windows.Forms.SendKeys.Send("^{t}");
            }

                                                                                    //A = mouse leftclick x1
            if (newState.Buttons.A == ButtonState.Pressed && oldState.Buttons.A != ButtonState.Pressed && newState.Triggers.Right != 1 && newState.Triggers.Left != 1)
            {
                mouse_event(MOUSEEVENTF_LEFTDOWN, Mouse.GetState().X, Mouse.GetState().Y, 0, 0);
            }
                                                                                   // Held down when held down
            if (oldState.Buttons.A == ButtonState.Pressed && newState.Buttons.A == ButtonState.Released && newState.Triggers.Right != 1 && newState.Triggers.Left != 1)
            {
                mouse_event(MOUSEEVENTF_LEFTUP, Mouse.GetState().X, Mouse.GetState().Y, 0, 0);
            }

                                                                                    // X = right click x1
            if (newState.Buttons.X == ButtonState.Pressed && oldState.Buttons.X != ButtonState.Pressed && newState.Triggers.Right != 1 && newState.Triggers.Left != 1)
            {
                mouse_event(MOUSEEVENTF_RIGHTDOWN, Mouse.GetState().X, Mouse.GetState().Y, 0, 0);
            }
                                                                                   // Held down when held down
            if (oldState.Buttons.X == ButtonState.Pressed && newState.Buttons.X == ButtonState.Released && newState.Triggers.Right != 1 && newState.Triggers.Left != 1)
            {
                mouse_event(MOUSEEVENTF_RIGHTUP, Mouse.GetState().X, Mouse.GetState().Y, 0, 0);
            }
            
                                                                                     // Y = osk.exe
            if (oldState.Buttons.Y == ButtonState.Pressed && newState.Buttons.Y == ButtonState.Released && newState.Triggers.Right != 1 && newState.Triggers.Left != 1)
            {
                everyOther = !everyOther;
                if (everyOther)
                {
                    try
                    {
                        System.Diagnostics.Process.Start("keyboard.exe");
                    }
                    catch(Exception e)
                    {
                    }
                }
                else                                                                // Toggle
                {
                    try{
                        System.Diagnostics.Process [] proc = System.Diagnostics.Process.GetProcessesByName("keyboard");
                        proc[0].Kill();
                    }
                    catch(Exception e)
                    {
                        System.Windows.Forms.MessageBox.Show("Cannot close On-screen Keyboard: \n" + e.Message);
                    }
                }
            }
                                                                                    // B = Escape
            if (oldState.Buttons.B == ButtonState.Pressed && newState.Buttons.B == ButtonState.Released && newState.Triggers.Right != 1 && newState.Triggers.Left != 1)
            {
                System.Windows.Forms.SendKeys.SendWait("{ESC}");
            }


                                                                                    // DPAD left = left arrow
            if (newState.DPad.Left == ButtonState.Pressed)
            {
                System.Windows.Forms.SendKeys.SendWait("{LEFT}");
                System.Threading.Thread.Sleep(40);
                if (oldState.DPad.Left != ButtonState.Pressed)
                {
                    System.Threading.Thread.Sleep(300);
                }
            }

                                                                                    // DPad Right = right arrow
            if (newState.DPad.Right == ButtonState.Pressed)
            {
                System.Windows.Forms.SendKeys.Send("{RIGHT}");
                System.Threading.Thread.Sleep(40);
                if (oldState.DPad.Right != ButtonState.Pressed)
                {
                    System.Threading.Thread.Sleep(300);
                }
            }

                                                                                    // Dpad Down = Down arrow
            if (newState.DPad.Down == ButtonState.Pressed)
            {
                System.Windows.Forms.SendKeys.Send("{DOWN}");
                System.Threading.Thread.Sleep(40);
                if (oldState.DPad.Down != ButtonState.Pressed)
                {
                    System.Threading.Thread.Sleep(300);
                }
            }

                                                                                    // DPad up = upp  arrow
            if (newState.DPad.Up == ButtonState.Pressed)
            {
                System.Windows.Forms.SendKeys.Send("{UP}");
                System.Threading.Thread.Sleep(40);
                if (oldState.DPad.Up != ButtonState.Pressed)
                {
                    System.Threading.Thread.Sleep(300);
                }
            }


                                                                                    //scroll with right stick

            scrollY = (newState.ThumbSticks.Right.Y * 40);
            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, (int)scrollY, 0);
            scrollX = (newState.ThumbSticks.Right.X * 40);
            mouse_event(MOUSEEVENTF_HWHEEL, 0, 0, (int)scrollX, 0);

                                                                                    //left joystick controls mouse
                Vector2 mousePos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                
                mouseX = mousePos.X;
                mouseX += newState.ThumbSticks.Left.X*sens;
                mouseY = mousePos.Y;
                mouseY -= newState.ThumbSticks.Left.Y*sens;

                Mouse.SetPosition((int)mouseX, (int)mouseY);
                
                
                                                                                    //stickClick = middleclick x1
                if (newState.Buttons.LeftStick == ButtonState.Pressed && oldState.Buttons.LeftStick != ButtonState.Pressed)
                {
                    mouse_event(MOUSEEVENTF_MIDDLEDOWN, Mouse.GetState().X, Mouse.GetState().Y, 0, 0);
                }
                                                                                    //held down when held down
                if (oldState.Buttons.LeftStick == ButtonState.Pressed && newState.Buttons.LeftStick == ButtonState.Released)
                {
                    mouse_event(MOUSEEVENTF_MIDDLEUP, Mouse.GetState().X, Mouse.GetState().Y, 0, 0);
                }

                try
                {
                    this.oldState = this.newState;
                }
                catch
                {

                }
       
            base.Update(gameTime);
        }
    }
}