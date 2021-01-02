using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace WindowsFormsApplication1
{
    class GameLoop
    {
        Bird bird;
        Pipe pipe;

        public object[] objectOfGame;
        
        public GameLoop()
        {
            bird = new Bird();
            pipe = new Pipe();
        }

        public void Load()
        {
            objectOfGame[0] = bird;
        }
        public void Draw(Graphics gfx)
        {
            bird.Draw(gfx);
            pipe.Draw(gfx);
            
        }
    }
}
