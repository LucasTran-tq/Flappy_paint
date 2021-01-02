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
    class Coins
    {
        int X_Coins;
        int Y_Coins;
        int coins_Count = 0;
        int distance_2pipe = 150;
        int randomNext_Coins = 2;

     

        public Rectangle recCoins;

        Random random = new Random();

        public Bitmap coins = new Bitmap(Properties.Resources.coins_gif);
        

        WindowsMediaPlayer soundCash = new WMPLib.WindowsMediaPlayer();

        string path_SoundCash = string.Format("{0}Resources\\Cash Register.wav",
                 Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\")));

        public bool coins_Appearance = false;

        
        public Coins()
        {
            recCoins = new Rectangle(X_Coins, Y_Coins, 40, 40);
        }

        public void SoundCash()
        {
            soundCash.URL = path_SoundCash;
            soundCash.controls.play();
        }

        public void GetCoins(Form form, Bird bird, Pipe pipe)
        {
            
            // bird pass through pipe
            if(pipe.X_pipePairs1 + pipe.recPipe.Width <= 0)
            {
                coins_Count++;
                if(coins_Count == randomNext_Coins)
                {
                    //picBoxCoins.Visible = true;

                    // coins visible
                    //bird.gotCoins = false;
                    coins_Appearance = true;

                    DrawCoins(form, pipe);
                    coins_Count = 0;
                    randomNext_Coins = random.Next(4,5);
                }
            }
        }

        

        public void DrawCoins(Form form, Pipe pipe)
        {
            X_Coins = form.Width + 150 + pipe.recPipe.Width + random.Next(0, distance_2pipe);

            Y_Coins = random.Next(0, form.Height - 40);
           
        }

        public void CoinsInGame(Bird bird)
        {
            if (bird.isGetGift)
            {
                X_Coins -= 10;
            }
            else
            {
                X_Coins -= 4;
            }
            

        }


        public void Impact_Coins_Bird(Bird bird)
        {
            // 50 is the skin of picboxCoins
            if(bird.X_Bird + bird.recBird.Width >= X_Coins && bird.X_Bird <= X_Coins + this.recCoins.Width)
            {
                // increase score
                if(bird.Y_Bird + bird.recBird.Height >= Y_Coins && bird.Y_Bird <= Y_Coins + this.recCoins.Height)
                {
                    SoundCash();

                    if (coins_Appearance)
                    {
                        bird.scoreOfGame += 3;
                    }
                    coins_Appearance = false;
                }

            }
        }
        

        public void Draw(Graphics gfx)
        {
           
            gfx.DrawImage(coins, new RectangleF(X_Coins, Y_Coins, recCoins.Width, recCoins.Height));

        }

    }
}
