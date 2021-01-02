using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Media;
using WMPLib;
using System.IO;

namespace WindowsFormsApplication1
{
    class Rocket
    {
        public int X_Rocket;
        public int Y_Rocket;
        int rocket_Count = 0;
        int randomNext_Rocket = 4;
        int distance_2pipe = 150;

        int X_Fire;
        int Y_Fire;

        Random random = new Random();

        public Bitmap rocket = new Bitmap(Properties.Resources.rocket);
        public Bitmap emergency = new Bitmap(Properties.Resources.emergency_gif)
            ;
        public Bitmap fire = new Bitmap(Properties.Resources.fire);

        WindowsMediaPlayer soundEmergency = new WMPLib.WindowsMediaPlayer();
        public WindowsMediaPlayer soundBoomExplosion = new WMPLib.WindowsMediaPlayer();

        string path_SoundEmergency = string.Format("{0}Resources\\emergency.wav",
                 Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\")));

        string path_SoundBoomExplosion = string.Format("{0}Resources\\boomExplosion.wav",
                 Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\")));

        public Rectangle recRocket;
        public Rectangle recEmergency;
        public Rectangle recFire;

        public Rocket()
        {
            recRocket = new Rectangle(X_Rocket, Y_Rocket, 70, 50);
            recEmergency = new Rectangle(-10, 0, 41, 41);
            recFire = new Rectangle(-10, 0, 50, 100);
        }

        public bool emergency_Appearance = false;
        public bool fire_Appearance = false;
        public bool rocket_Apearance = false;

        // shield impact rocket. and rocket get fire
        public bool rocket_Fire = false;

        public void SoundEmergency()
        {
            soundEmergency.URL = path_SoundEmergency;
            soundEmergency.controls.play();
        }
        public void SoundBoomExplosion()
        {
            soundBoomExplosion.URL = path_SoundBoomExplosion;
            soundBoomExplosion.controls.play();
        }

        public void GetRocket(Form form, Bird bird, Pipe pipe)
        {

            // pass through pipe
            if (pipe.X_pipePairs1 + pipe.recPipe.Width <= 0)
            {
                rocket_Count++;
                if (rocket_Count == randomNext_Rocket)
                {
                    SoundEmergency();
                    
                    emergency_Appearance = true;

                    fire_Appearance = false;

                    rocket_Apearance = true;

                    rocket_Fire = false;

                    DrawRocket(form, pipe);
                    rocket_Count = 0;
                    randomNext_Rocket = random.Next(5, 6);
                }
            }
        }

        public void DrawRocket(Form form, Pipe pipe)
        {
            X_Rocket = form.Width + 1500;

            Y_Rocket = random.Next(0, form.Height - 50);
           
            
        }

        public void RocketInGame(Bird bird)
        {
            if (rocket_Fire)
            {
                X_Rocket -= 7;
                Y_Rocket += 7;
            }

            else if (bird.isGetGift)
            {
                  X_Rocket -= 13;
            }
            else
            {
                X_Rocket -= 7;
            }
        }

        

        public void InvisibleEmergency(Form form)
        {
            if(X_Rocket >= form.Width + 50 && X_Rocket <= form.Width + 70)
            {
                
                emergency_Appearance = false;
            }
        }

        public void Move_Fire(Bird bird)
        {
           
            if (bird.isGetRocket && !bird.isAlive)
            {
                X_Fire = bird.X_Bird - 15;
                Y_Fire = bird.Y_Bird - 55;

            }

            else if (rocket_Fire)
            {
                X_Fire = X_Rocket;
                Y_Fire = Y_Rocket - 50; 
            }
        }

        public void Impact_Rocket_Bird(Bird bird, Gift gift, LifeSpan lifeSpan, Timer timer_Bird, Timer timer)
        {

            if ((bird.X_Bird + bird.picBoxBird.Width >= X_Rocket)
                      && (bird.X_Bird <= X_Rocket + recRocket.Width))
            {
                // increase score
                if (bird.Y_Bird + bird.picBoxBird.Height >= Y_Rocket
                         && bird.Y_Bird <= Y_Rocket + recRocket.Height)
                {

                    if (rocket_Apearance)
                    {
                        bird.isGetRocket = true;

                        bird.isAlive = false;

                        fire_Appearance = true;

                        SoundBoomExplosion();
        
                        gift.Thunder_Appearance = false;

                        gift.soundTheFlash.controls.stop();

                        bird.SoundGameOver();

                        timer.Stop();

                        rocket_Apearance = false;
                    }

                    

                }

            }
        }

        public void Draw_Rocket(Graphics g)
        {
            
            g.DrawImage(rocket, new RectangleF(X_Rocket, Y_Rocket, recRocket.Width, recRocket.Height));
        }
        public void Draw_Emergency(Graphics g)
        {

            g.DrawImage(emergency, new RectangleF(350, Y_Rocket, recEmergency.Width, recEmergency.Height));
        }

        public void Draw_Fire(Graphics g)
        {
            g.DrawImage(fire, new RectangleF(X_Fire, Y_Fire, recFire.Width, recFire.Height));
        }
    }
}
