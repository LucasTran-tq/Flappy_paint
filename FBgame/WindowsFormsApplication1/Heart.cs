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
    class Heart
    {
        int X_Hearts;
        int Y_Hearts;
        int pipes_Count = 0;
        int distance_2pipe = 150;
        int randomNext_Hearts = 3;
        public bool sign_getHearts = false;
        public bool sign_decreaseHearts = false;


        Random random = new Random();

        public Bitmap hearts = new Bitmap(Properties.Resources.heart_item);

        public Rectangle recHeart;

        public bool heart_Appearance = false;

    

        public Heart()
        {
            recHeart = new Rectangle(X_Hearts, Y_Hearts, 40, 40);
        }

        public void DrawHearts(Form form, Pipe pipe)
        {
            X_Hearts = form.Height + 150 + pipe.recPipe.Width + random.Next(50, distance_2pipe);

            Y_Hearts = random.Next(0, form.Height - 30);

        }
        public void GetHearts(Form form, Bird bird, Pipe pipe)
        {

            // bird pass through pipe
            if (pipe.X_pipePairs1 + pipe.recPipe.Width <= 0)
            {
                pipes_Count++;
                if (pipes_Count == randomNext_Hearts)
                {
                   
                    heart_Appearance = true;

                    DrawHearts(form, pipe);
                    pipes_Count = 0;
                    randomNext_Hearts = random.Next(4, 5);
                }
            }
        }

        public void HeartsInGame(Bird bird)
        {
            if (bird.isGetGift)
            {
                X_Hearts -= 10;
            }
            else
            {
                X_Hearts -= 4;
            }
        }

        public void Impact_Hearts_Bird(Bird bird)
        {
        
            if (bird.X_Bird + bird.recBird.Width >= X_Hearts && bird.X_Bird <= X_Hearts + recHeart.Height)
            {
                // increase score
                if (bird.Y_Bird + bird.recBird.Height >= Y_Hearts && bird.Y_Bird <= Y_Hearts + recHeart.Height)
                {
                    
                    if (heart_Appearance)
                    {
                        sign_getHearts = true;
                    }
                    heart_Appearance = false;
                }

            }

        }

        public void Draw(Graphics g)
        {
            g.DrawImage(hearts, new RectangleF(X_Hearts, Y_Hearts, recHeart.Width, recHeart.Height));
        }

    }
}
