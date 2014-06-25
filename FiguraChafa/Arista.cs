using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;

namespace RunnerRedBayesiana
{
    [SerializableAttribute] 
    class Arista
    {
        public Vertice v1, v2;
        public bool marcada = false;
        int peso;
        int distancia;   
        Color color = Color.Black;
        int grosor = 1;
        public Rectangle ptRect = new Rectangle();
        public List<Rectangle> pixeles = new List<Rectangle>();

        public Arista(Vertice vert1, Vertice vert2)
        {
            peso = 0;
            v1 = vert1;
            v2 = vert2;
            distancia = (int)Math.Sqrt((v2.pos.X - v1.pos.X) * (v2.pos.X - v1.pos.X) - (v2.pos.Y - v1.pos.Y) * (v2.pos.Y - v1.pos.Y));
            asigna();
        }
        public void dibujate(Graphics g,int estilo)
        { 
            Pen pen = new Pen(color, grosor);
            Font drawFont = new Font(FontFamily.GenericMonospace, 10);


            double teta1 = Math.Atan2(v2.pos.Y - v1.pos.Y, v2.pos.X - v1.pos.X);
            float x1 = v1.pos.X + (float)((Math.Cos(teta1)) * (v2.diam / 2));
            float y1 = v1.pos.Y + (float)((Math.Sin(teta1)) * (v2.diam / 2));

            double teta2 = Math.Atan2(v1.pos.Y - v2.pos.Y, v1.pos.X - v2.pos.X);
            float x2 = v2.pos.X + (float)((Math.Cos(teta2)) * (v2.diam / 2));
            float y2 = v2.pos.Y + (float)((Math.Sin(teta2)) * (v2.diam / 2));


            if (estilo == 1)
            {
                AdjustableArrowCap aac = new AdjustableArrowCap(5, 5);
                aac.Filled = true;
                pen.CustomEndCap = aac; 
                //String strPeso = Convert.ToString(peso);
                //g.DrawString(strPeso, drawFont, Brushes.Black, (x1+x2)/2,(y1+y2)/2 );
            }

            if (peso != 0)
            {
                String strPeso = Convert.ToString(peso);
                g.DrawString(strPeso, drawFont, Brushes.Black, (x1 + x2) / 2, (y1 + y2) / 2);
            }

            
            if (marcada == true)
            {
                pen.Color = Color.Yellow;
                g.DrawLine(pen, x1, y1, x2, y2);
            }
            else
            {
                //pen.Color = Color.Black;
                g.DrawLine(pen, x1, y1, x2, y2);
                asignaPixeles(g,ptRect);
            }
            


            PointF p1 = calculaPunto(-50);
            PointF p2 = calculaPunto(-140);
            if (v1 == v2)
            {      
                g.DrawBezier(pen, p1.X,p1.Y, p1.X+20, p1.Y - 50, p1.X-50, p1.Y - 50, p2.X,p2.Y);
            }
        }
        public void dibujaCamino(Graphics g, int it)
        { 
                Pen pen = new Pen(Brushes.Blue);
                //Rectangulos de intersección

                //Tienen que ser enteros los culeros
                double teta1 = Math.Atan2(v2.pos.Y - v1.pos.Y, v2.pos.X - v1.pos.X);
                int x0 = (int)(v1.pos.X + (float)((Math.Cos(teta1)) * (it)));
                int y0 = (int)(v1.pos.Y + (float)((Math.Sin(teta1)) * (it)));

                double teta2 = Math.Atan2(v1.pos.Y - v2.pos.Y, v1.pos.X - v2.pos.X);
                int x1 = (int)(v2.pos.X + (float)((Math.Cos(teta2)) * (v2.diam / 2)));
                int y1 = (int)(v2.pos.Y + (float)((Math.Sin(teta2)) * (v2.diam / 2)));

                Rectangle pix = new Rectangle(x0, y0, grosor, grosor);

                int dx = x1 - x0;
                int dy = y1 - y0;    

                if (Math.Abs(dx) > Math.Abs(dy))
                {          // pendiente < 1
                    float m = (float)dy / (float)dx;
                    float b = y0 - m * x0;
                    if (dx < 0)
                        dx = -1;
                    else
                        dx = 1;
                    while (x0 != x1)
                    {
                        x0 += dx;
                        y0 = (int)Math.Round(m * x0 + b);
                        pix.X = x0;
                        pix.Y = y0;

                        g.DrawRectangle(pen, pix.X, pix.Y, grosor, grosor);
                        pixeles.Add(pix);
                        //Thread.Sleep(1); 
                        if (it == x1)
                            break;
                        
                    }
                }
                else
                    if (dy != 0)
                    {                              // pendiente >= 1
                        float m = (float)dx / (float)dy;      // calcula pendiente
                        float b = x0 - m * y0;
                        if (dy < 0)
                            dy = -1;
                        else
                            dy = 1;
                        while (y0 != y1)
                        {
                            y0 += dy;
                            x0 = (int)Math.Round(m * y0 + b);
                            pix.X = x0;
                            pix.Y = y0;

                            pixeles.Add(pix);
                            g.DrawRectangle(pen, pix.X, pix.Y, grosor, grosor);
                            //Thread.Sleep(1);
                            
                        }
                    }
            
        }
        public void asignaPixeles(Graphics g, Rectangle rtMarca)
        {
            Pen pen = new Pen(Brushes.Red);
            //Rectangulos de intersección

            //Tienen que ser enteros los culeros
            double teta1 = Math.Atan2(v2.pos.Y - v1.pos.Y, v2.pos.X - v1.pos.X);
            int x0 = (int)(v1.pos.X + (float)((Math.Cos(teta1)) ));
            int y0 = (int)(v1.pos.Y + (float)((Math.Sin(teta1)) ));

            double teta2 = Math.Atan2(v1.pos.Y - v2.pos.Y, v1.pos.X - v2.pos.X);
            int x1 = (int)(v2.pos.X + (float)((Math.Cos(teta2)) * (v2.diam / 2)));
            int y1 = (int)(v2.pos.Y + (float)((Math.Sin(teta2)) * (v2.diam / 2)));

            Rectangle pix = new Rectangle(x0, y0, grosor, grosor);

            int dx = x1 - x0;
            int dy = y1 - y0;
            bool band = false;

            if (Math.Abs(dx) > Math.Abs(dy))
            {          // pendiente < 1
                float m = (float)dy / (float)dx;
                float b = y0 - m * x0;
                if (dx < 0)
                    dx = -1;
                else
                    dx = 1;
                while (x0 != x1)
                {
                    x0 += dx;
                    y0 = (int)Math.Round(m * x0 + b);
                    pix.X = x0;
                    pix.Y = y0;


                    if (rtMarca.X == pix.X && rtMarca.Y == pix.Y)
                        band = true;

                    if(band)
                        g.DrawRectangle(pen, pix.X, pix.Y, grosor, grosor);
                    //else
                      //  g.DrawRectangle(new Pen(Brushes.White), pix.X, pix.Y, grosor, grosor);

                    //Thread.Sleep(1); 
                }
            }
            else
                if (dy != 0)
                {                              // pendiente >= 1
                    float m = (float)dx / (float)dy;      // calcula pendiente
                    float b = x0 - m * y0;
                    if (dy < 0)
                        dy = -1;
                    else
                        dy = 1;
                    while (y0 != y1)
                    {
                        y0 += dy;
                        x0 = (int)Math.Round(m * y0 + b);
                        pix.X = x0;
                        pix.Y = y0;


                        if (rtMarca.X == pix.X && rtMarca.Y == pix.Y)
                            band = true;

                        if (band)
                            g.DrawRectangle(pen, pix.X, pix.Y, grosor, grosor);
                        //else
                          //  g.DrawRectangle(new Pen(Brushes.White), pix.X, pix.Y, grosor, grosor);

                        
                     
                        //Thread.Sleep(1);

                    }
                }

        }
        public void asigna()
        {
            Pen pen = new Pen(Brushes.Blue);
            //Rectangulos de intersección

            //Tienen que ser enteros los culeros
            double teta1 = Math.Atan2(v2.pos.Y - v1.pos.Y, v2.pos.X - v1.pos.X);
            int x0 = (int)(v1.pos.X + (float)((Math.Cos(teta1))));
            int y0 = (int)(v1.pos.Y + (float)((Math.Sin(teta1))));

            double teta2 = Math.Atan2(v1.pos.Y - v2.pos.Y, v1.pos.X - v2.pos.X);
            int x1 = (int)(v2.pos.X + (float)((Math.Cos(teta2)) * (v2.diam / 2)));
            int y1 = (int)(v2.pos.Y + (float)((Math.Sin(teta2)) * (v2.diam / 2)));

            Rectangle pix = new Rectangle(x0, y0, grosor, grosor);

            int dx = x1 - x0;
            int dy = y1 - y0;
            bool band = false;

            if (Math.Abs(dx) > Math.Abs(dy))
            {          // pendiente < 1
                float m = (float)dy / (float)dx;
                float b = y0 - m * x0;
                if (dx < 0)
                    dx = -1;
                else
                    dx = 1;
                while (x0 != x1)
                {
                    x0 += dx;
                    y0 = (int)Math.Round(m * x0 + b);
                    pix.X = x0;
                    pix.Y = y0;


            
                    pixeles.Add(pix);
                    //Thread.Sleep(1); 
                }
            }
            else
                if (dy != 0)
                {                              // pendiente >= 1
                    float m = (float)dx / (float)dy;      // calcula pendiente
                    float b = x0 - m * y0;
                    if (dy < 0)
                        dy = -1;
                    else
                        dy = 1;
                    while (y0 != y1)
                    {
                        y0 += dy;
                        x0 = (int)Math.Round(m * y0 + b);
                        pix.X = x0;
                        pix.Y = y0;


                      
                        pixeles.Add(pix);

                        //Thread.Sleep(1);

                    }
                }

        }


        private PointF calculaPunto(double angulo)
        {
            PointF pF = new PointF(); 
            float x1 = v1.pos.X + (float)((Math.Cos(angulo*Math.PI/180)) * (v2.diam / 2));
            float y1 = v1.pos.Y + (float)((Math.Sin(angulo*Math.PI/180)) * (v2.diam / 2));
            pF.X = x1;
            pF.Y = y1;
            return pF;
        }
        public bool intersectOn(Point pos)
        {
            //Rectangulos de intersección
            Rectangle mouse = new Rectangle(pos.X, pos.Y, 3, 3);
            Rectangle pix = new Rectangle(v1.pos.X, v1.pos.Y, grosor, grosor);

            PointF p1 = calculaPunto(-50);
            PointF p2 = calculaPunto(-140);


            int x0 = v1.pos.X;
            int y0 = v1.pos.Y;
            int x1 = v2.pos.X;
            int y1 = v2.pos.Y;

            int dx = v2.pos.X - v1.pos.X;
            int dy = v2.pos.Y - v1.pos.Y;

            if (Math.Abs(dx) > Math.Abs(dy))
            {          // pendiente < 1
                float m = (float)dy / (float)dx;
                float b = y0 - m * x0;
                if (dx < 0)
                    dx = -1;
                else
                    dx = 1;
                while (x0 != x1)
                {
                    x0 += dx;
                    y0 = (int)Math.Round(m * x0 + b);
                    pix.X = x0;
                    pix.Y = y0;

                    if (mouse.IntersectsWith(pix))
                        return true; 
                }
            }
            else
                if (dy != 0)
                {                              // pendiente >= 1
                    float m = (float)dx / (float)dy;      // calcula pendiente
                    float b = x0 - m * y0;
                    if (dy < 0)
                        dy = -1;
                    else
                        dy = 1;
                    while (y0 != y1)
                    {
                        y0 += dy;
                        x0 = (int)Math.Round(m * y0 + b);
                        pix.X = x0;
                        pix.Y = y0;

                        if (mouse.IntersectsWith(pix))
                            return true; 
                    }
                }

            if (v1 == v2)
            {
                List<double> ptList = new List<double>();
                Bezier bc = new Bezier();

                ptList.Add(p1.X);
                ptList.Add(p1.Y);
                ptList.Add(p1.X + 20);
                ptList.Add(p1.Y - 50);
                ptList.Add(p1.X - 50);
                ptList.Add(p1.Y - 50);
                ptList.Add(p2.X);
                ptList.Add(p2.Y);


                const int POINTS_ON_CURVE = 200;
                double[] ptind = new double[ptList.Count];
                double[] p = new double[POINTS_ON_CURVE];
                ptList.CopyTo(ptind, 0);
                bc.Bezier2D(ptind, (POINTS_ON_CURVE) / 2, p);

                for (int i = 1; i != POINTS_ON_CURVE - 1; i += 2)
                {
                    if(mouse.IntersectsWith(new Rectangle((int)p[i + 1], (int)p[i], grosor, grosor)))
                        return true;
                }
            }

            return false;
        }
        public int getPeso()
        {
            return peso;
        }
        public void setPeso(int p)
        {
            peso = p;
        }
        public void setColor(Color c)
        {
            color = c;
        }
        public Color getColor()
        {
            return color;
        }
        public void setGrosor(int value)
        {
            grosor = value;
        }
    }
}

 
  