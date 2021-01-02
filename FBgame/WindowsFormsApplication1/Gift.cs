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
    class Gift
    {
        int X_Gift;
        int Y_Gift;
        int gift_Count = 0;
        int randomNext_Gift = 4;
        int distance_2pipe = 150;

        int X_Thunder;
        int Y_Thunder;

        public int count_Flash = 0;

        Random random = new Random();

        public Bitmap gift = new Bitmap(Properties.Resources.gift_gif);
        public Bitmap thunder = new Bitmap(Properties.Resources.thunder_gif);

        public WindowsMediaPlayer soundThunder = new WMPLib.WindowsMediaPlayer();
        public WindowsMediaPlayer soundTheFlash = new WMPLib.WindowsMediaPlayer();

        string path_SoundThunder = string.Format("{0}Resources\\thunder.wav",
                 Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\")));

        string path_SoundTheFlash = string.Format("{0}Resources\\TheFlash.wav",
                 Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\")));

        public bool gift_Flash_Appearance = false;

        public bool Thunder_Appearance = false;

        public Rectangle recGift;
        public Rectangle recThunder;

        public Gift()
        {
            recGift = new Rectangle(X_Gift, Y_Gift, 50, 50);
            recThunder = new Rectangle(X_Thunder, Y_Thunder, 50, 50);
        }

        

        public void SoundThunder()
        {
            soundThunder.URL = path_SoundThunder;

            soundThunder.controls.play();
        }
        public void SoundTheFlash()
        {
            soundTheFlash.URL = path_SoundTheFlash;

            soundTheFlash.controls.play();
        }
        public void GetGift(Form form, Pipe pipe)
        {

            if (pipe.X_pipePairs1 + pipe.recPipe.Width <= 0)
            {
                gift_Count++;
                if (gift_Count == randomNext_Gift)
                {
                    //SoundEmergency();
                    gift_Flash_Appearance = true;

                    DrawGift(form);

                    gift_Count = 0;

                    randomNext_Gift = random.Next(4, 5);
                }
            }
        }

        public void DrawGift(Form form)
        {
            X_Gift = form.Width + 50;

            Y_Gift = random.Next(0, form.Height - 50);

        }

        public void GiftInGame()
        {
            X_Gift -= 5;

        }

        public void Impact_Gift_Bird(Bird bird, Timer timer1, Timer timer_Bird, Timer timer2)
        {
            if ((bird.X_Bird + bird.picBoxBird.Width >= X_Gift)
                      && (bird.X_Bird <= X_Gift + this.recGift.Width))
            {
                
                if (bird.Y_Bird + bird.picBoxBird.Height >= Y_Gift
                         && bird.Y_Bird <= Y_Gift + this.recGift.Height)
                {

                    if (this.gift_Flash_Appearance)
                    {
                        
                        bird.isGetGift = true;

                        
                        Thunder_Appearance = true;

                        SoundThunder();

                        SoundTheFlash();

                        timer2.Start();
                    }

                    this.gift_Flash_Appearance = false;

                }

            }
        }

        public void Appear_Thunder(Bird bird)
        {
            if (bird.isGetGift)
            {
                X_Thunder = bird.X_Bird;
                Y_Thunder = bird.Y_Bird - 50;
            }
            else
            {
                this.Thunder_Appearance = false;
            }
        }

        public void Draw_Gift_Flash(Graphics g)
        {
            g.DrawImage(gift, new RectangleF(X_Gift, Y_Gift, recGift.Width, recGift.Height));
        }

        public void Draw_Thunder(Graphics g)
        {
            g.DrawImage(thunder, new RectangleF(X_Thunder, Y_Thunder, recThunder.Width, recThunder.Height));
        }
    }
}
