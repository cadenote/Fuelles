using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;
using System.Globalization;

namespace Fuelles
{
    public partial class Form1 : Form
    {
        const int INSIDE = 0; // 0000
        const int LEFT = 1;   // 0001
        const int RIGHT = 2;  // 0010
        const int BOTTOM = 4; // 0100
        const int TOP = 8;    // 1000
        double xmin = 0.0;
        double ymin = 0.0;

        // Compute the bit code for a point (x, y) using the clip rectangle
        // bounded diagonally by (xmin, ymin), and (xmax, ymax)

        // ASSUME THAT xmax, xmin, ymax and ymin are global constants.

        int ComputeOutCode(double xx, double yy)
        {
            int code;
            double ymax = dblPaperWidth;

            code = INSIDE;          // initialised as being inside of [[clip window]]

            if (xx < xmin)           // to the left of clip window
                code |= LEFT;
            else if (xx > dblPaperWidth)      // to the right of clip window
                code |= RIGHT;
            if (yy < ymin)           // below the clip window
                code |= BOTTOM;
            else if (yy > dblPaperHeight)      // above the clip window
                code |= TOP;

            return code;
        }
        // Cohen–Sutherland clipping algorithm clips a line from
        // P0 = (x0, y0) to P1 = (x1, y1) against a rectangle with 
        // diagonal from (xmin, ymin) to (xmax, ymax).
        (double, double, double, double) Encaja (double x0, double y0, double x1, double y1)
        {
            // compute outcodes for P0, P1, and whatever point lies outside the clip rectangle
            double x = 0.0, y = 0.0;
            int outcode0 = ComputeOutCode(x0, y0);
            int outcode1 = ComputeOutCode(x1, y1);

            while (true)
            {
                if ((outcode0 | outcode1) == 0)
                {
                    // bitwise OR is 0: both points inside window; trivially accept and exit loop
                    break;
                }
                else if ((outcode0 & outcode1) != 0)
                {
                    // bitwise AND is not 0: both points share an outside zone (LEFT, RIGHT, TOP,
                    // or BOTTOM), so both must be outside window; exit loop (accept is false)
                    break;
                }
                else
                {
                    // failed both tests, so calculate the line segment to clip
                    // from an outside point to an intersection with clip edge


                    // At least one endpoint is outside the clip rectangle; pick it.
                    int outcodeOut = outcode0 != 0 ? outcode0 : outcode1;

                    // Now find the intersection point;
                    // use formulas:
                    //   slope = (y1 - y0) / (x1 - x0)
                    //   x = x0 + (1 / slope) * (ym - y0), where ym is ymin or ymax
                    //   y = y0 + slope * (xm - x0), where xm is xmin or xmax
                    // No need to worry about divide-by-zero because, in each case, the
                    // outcode bit being tested guarantees the denominator is non-zero
                    if ((outcodeOut & TOP) != 0)
                    {           // point is above the clip window
                        x = x0 + (x1 - x0) * (dblPaperHeight - y0) / (y1 - y0);
                        y = dblPaperHeight;
                    }
                    else if ((outcodeOut & BOTTOM) != 0)
                    { // point is below the clip window
                        x = x0 + (x1 - x0) * (ymin - y0) / (y1 - y0);
                        y = ymin;
                    }
                    else if ((outcodeOut & RIGHT) != 0)
                    {  // point is to the right of clip window
                        y = y0 + (y1 - y0) * (dblPaperWidth - x0) / (x1 - x0);
                        x = dblPaperWidth;
                    }
                    else if ((outcodeOut & LEFT) != 0)
                    {   // point is to the left of clip window
                        y = y0 + (y1 - y0) * (xmin - x0) / (x1 - x0);
                        x = xmin;
                    }

                    // Now we move outside point to intersection point to clip
                    // and get ready for next pass.
                    if (outcodeOut == outcode0)
                    {
                        x0 = x;
                        y0 = y;
                        outcode0 = ComputeOutCode(x0, y0);
                    }
                    else
                    {
                        x1 = x;
                        y1 = y;
                        outcode1 = ComputeOutCode(x1, y1);
                    }
                }

            }
            return (x0, y0, x1, y1);
        }
    }
}
