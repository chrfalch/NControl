using NControl.Abstractions;
using NGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NControlDemo.FormsApp.Controls
{
    public class BlueFrameControl: NControlView
    {
        public BlueFrameControl()
        {
            InputTransparent = true;
        }

        public override void Draw(NGraphics.ICanvas canvas, NGraphics.Rect rect)
        {
            base.Draw(canvas, rect);

            rect.Inflate(-10, -10);
            canvas.DrawRectangle(rect, Pens.Blue, null);            
        }
    }
}
