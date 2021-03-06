using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;


namespace RunnerRedBayesiana
{
    [SerializableAttribute]
    class Vertice
    {
        public Point pos; //Posicion del Circulo
        public int diam;   //diametro 
        public int id;    //identificador
        public int id2;   //Para usos esclusivos Bipartita y Bosque Abarcador
        public int id3 = 0;
        public Rectangle rect;//rectangulo de intersección
        List<Arista> listA = new List<Arista>(); //Lista de Aristas
        Color color = Color.GreenYellow;
        public int vCrom;
        public bool marcado = false;

        public Vertice(Point position, int radio, int identificador)
        {
            pos = position;
            diam = 30;
            id = identificador;
            id2 = 0;
        }
        public bool Intersect(Point point)
        {
            rect = new Rectangle(pos.X - diam / 2, pos.Y - diam / 2, diam, diam);
            return rect.IntersectsWith(new Rectangle(point, new Size(1, 1)));            
        }
        public void agregaArista(Arista arista)
        {
            listA.Add(arista);
        }
        public void agregaAristaHasta(Vertice v2)
        {
            listA.Add(new Arista(this, v2));
        }
        public void eliminaRelacionesCon(Vertice vertice)
        {
            //Eliminamos las aristas que tengan relacion con vertice
            //v1 es este, v2 es el otro nodo por lo tanto eliminaremos v2 y decrementaremos
            //el numero de aristas de los v1
            List<Arista> relaciones = new List<Arista>();      // lista de aristas realcionadas
            foreach (Arista a in listA)
            {
                if (a.v2 == vertice)
                {
                    relaciones.Add(a);
                }
            }

            //Ahora eliminamos la lista 
            foreach (Arista a in relaciones)
                listA.Remove(a);
            
            relaciones.Clear();
        }
        public List<Arista> dameRelaciones()
        {
            return listA;
        }
        public List<Arista> dameRelacionesSM()
        {
            List<Arista> lA = new List<Arista>();
            foreach (Arista a in listA)
            {
                if (!a.marcada)
                    lA.Add(a);
            }
            return lA;
        }
        public void desmarcaAristas()
        {
            foreach (Arista a in listA)
            {
                a.marcada = false;
            }
        }
        public bool tieneAristaCon(Vertice vertice)
        {
            foreach (Arista a  in listA)
            {
                if (a.v2 == vertice)
                    return true;
            }
            return false;
        }
        
        public void estableRelaciones(List<Arista> lA)
        {
            listA.Clear();
            listA = new List<Arista>(lA);   
        }
        public bool eliminaArista(Point pos)
        {
            //Elimina arista y regresa true 
            // si en realidad se elimino
            int total = listA.Count;
            Arista arista;
            for (int i = 0; i < total; i++)
            {
                arista = listA[i];
                if (arista.intersectOn(pos))
                {
                    listA.Remove(arista);
                    return true;
                }
            }
            return false;
        }
        public void setColor(int valor)
        {
            /** El rango de colores va de 0 a 765 **/
            int r = valor, g = 0, b = 0;
            if (r >= 200 && r >= 0)
            {
                r = 150;
                g = valor - 200;
                valor = g;
            }
            if(g >= 200 && g >= 0)
            {
                g = 180;
                b = valor - 200;
                valor = b;
            }
            if (b >= 200 && b >= 0)
            {
                b = 200;
            }    
            color = Color.FromArgb(r+50,g+50,b+50);
        }
        public void setColor(Color c)
        {
            color = c;
        }
        public int getColor()
        {
            return color.A + color.B + color.G;
        }
        public void dibujate(Graphics g, int estilo)
        {
            Font drawFont = new Font("Arial", 10);
            
            //String cad = Convert.ToString(listA.Count);
            //g.DrawString(cad, drawFont, Brushes.Black, pos.X - 30, pos.Y + 20);
            //Impresion de posiscion X y Y
            //String x = Convert.ToString(pos.X);
            //g.DrawString(x, drawFont, Brushes.Black, pos.X - 30, pos.Y + 20);
            //String y = Convert.ToString(pos.Y);
            //g.DrawString(y, drawFont, Brushes.Black, pos.X + 30, pos.Y + 20);


            SolidBrush brocha = new SolidBrush(color);
            foreach (Arista arista in listA)
            {
                arista.dibujate(g, estilo);
            }
            //Contorno Nodo y Relleno
            g.DrawEllipse(new Pen(Color.FromArgb(0, 0, 0)), pos.X - diam / 2, pos.Y - diam / 2, diam, diam);
            g.FillEllipse(brocha, pos.X - diam / 2, pos.Y - diam / 2, diam, diam);

            ///Num id y posicion
            String num = Convert.ToString(id);
            g.DrawString(num, drawFont, Brushes.Black, pos.X - 5, pos.Y - 5);


            if (estilo == 0)
            {
                //Num cormatico
                String num2 = Convert.ToString(vCrom);
                g.DrawString(num2, drawFont, brocha, pos.X - 30, pos.Y - 30);
            }
            
                
            /**if (id2 == 1)
            {
                g.DrawEllipse(new Pen(Color.FromArgb(0, 0, 0), 2), pos.X - rad / 2, pos.Y - rad / 2, rad, rad);
                g.FillEllipse(Brushes.LightSkyBlue, pos.X - rad / 2, pos.Y - rad / 2, rad, rad);
            }
            if (id2 == 2)
            {
                g.DrawEllipse(new Pen(Color.FromArgb(0, 0, 0), 2), pos.X - rad / 2, pos.Y - rad / 2, rad, rad);
                g.FillEllipse(Brushes.BlueViolet, pos.X - rad / 2, pos.Y - rad / 2, rad, rad);
            }**/
            /** id2 bipartita y Comp Fuertes**/
            String num3 = Convert.ToString(id2);
            g.DrawString(num3, drawFont, Brushes.Black, pos.X + 5, pos.Y + 5);

            //if(marcado)
              //  g.DrawString("Marcado", drawFont, Brushes.Black, pos.X + 15, pos.Y + 15);

        }
        public Arista dameArista(int index)
        {
            return listA[index];
        }
        public Arista dameAristaCon(Vertice v)
        {
            foreach (Arista a in listA)
            {
                if (a.v2 == v)
                    return a;
            }
            return null;
        }
        public Arista dameAristaCon(int id)
        {
            foreach (Arista a in listA)
            {
                if (a.v2.id == id)
                    return a;
            }
            return null;
        }
        public Arista dameAristaSinMarca()
        {
            List<Arista> lA = new List<Arista>();
            Arista arista = null;
            foreach (Arista a in listA)
            {
                if (!a.marcada)
                {
                    lA.Add(a);
                    arista = lA[0];
                }
            }
 
            if (arista != null)
            {
                foreach(Arista a2 in lA)
                {
                    if (arista.v2.numAristas() < a2.v2.numAristas())
                        arista = a2;
                }
                foreach (Arista a2 in lA)
                {
                    if (arista.v2.numMarcas() > a2.v2.numMarcas())
                        arista = a2;
                }
            }
            return arista;
        }
        public Vertice dameVecinoMenor()//Menor Arsita con v2 no marcado
        {
            Vertice vertice = null;
            foreach (Arista a in listA)
            {
                if (!a.v2.marcado)
                    vertice = a.v2;
            }

            if (vertice != null)
            {
                foreach (Arista a in listA)
                {
                    if (vertice.id > a.v2.id)
                        vertice = a.v2;
                }
            }                      
            return vertice;
        }
        public int numAristas()
        {
            return listA.Count;
        }
        public int numMarcas()
        {
            int marcas = 0;
            foreach (Arista a in listA)
            {
                if (a.marcada)
                    marcas++;
            }
            return marcas;
        }

        //Exclusiva para Aristas de retroceso
        public bool hayCaminoCon(Vertice v2)
        {
              List<Arista> pA = new List<Arista>();//El resultado esta en pA contiene las aristas que dibujan el camino
            List<Arista> aA; //Auxiliar de aristas para checar por cada vertice
            List<Vertice> aV1 = new List<Vertice>(); //auxiliar de vertices aV1 para comprobar en cada ciclo
            List<Vertice> aV2 = new List<Vertice>(); //Para almacenar los  vertices que pasaran a ser aV1 
            int watchdog = 0;


            List<Arista> auxA = new List<Arista>();
            
                aV1.Add(this);//Inicializamos con el primer vertice
                while (true)
                {
                    //Recorremos la lista de Vertices  que tuvo relacion con el vertice anterior
                    for (int i = 0; i < aV1.Count; i++)
                    {
                        //Importante liberar esta lista cada ciclo
                        auxA.Clear();
                        //Pedimos relaciones con el vertice y las guardamos en aA
                        aA = aV1[i].dameRelacionesSM();
                        //Seleccionamos solo las del color q nos interesa                
                        foreach (Arista a in aA)
                        {
                            if (a.getColor() == Color.DodgerBlue)
                                auxA.Add(a);
                        }
                        //Y se las asignamos aA y continua todo normalmente
                        aA = auxA;

                        //Recorremos las relaciones por cada vertice 
                        foreach (Arista a in aA)
                        {
                            //Si un vertice a.v2 apunta a un destino
                            //Quiere decir que si se encontro el destino
                            if (a.v2 == v2 && a.v2 != this)
                            {
                                
                                
                                //Coloreamos dos aristas porque es no dirigido
                                //a.v2.dameAristaCon(aV1[i]).setColor(Color.DarkGray);
                                //a.setColor(Color.DarkGray);

                                //Guardamos el camino de Aristas en pA
                                //pA.Add(a.v2.dameAristaCon(aV1[i]));
                                //pA.Add(a);

                                //hacemos v2 a v1 para volver a calcular es un estilo recursivo
                                v2 = a.v1;
                                //Limpiamos los datos para evitar que se copie la basura de v2 a v1
                                aV1.Clear();
                                aV1.Add(this);
                                aV2.Clear();
                                //Copiamos aV1 en aV2
                                foreach (Vertice v in aV1)
                                {
                                    aV2.Add(v);
                                }
                                ///////////////////
                                watchdog = 0;
                                break;
                                
                            }
                            //Agregamos todos los vertices a aV2 para proximo ciclo
                            aV2.Add(a.v2);
                        }
                    }

                    //Limpiamos el auxiliar de lista de vertices1 (aV1)  
                    aV1.Clear();
                    //Copiamos aV2 en aV1
                    foreach (Vertice v in aV2)
                    {
                        aV1.Add(v);
                    }
                    //Y limpiamos aV2
                    aV2.Clear();

                    watchdog++;
                    if(watchdog > 10)
                        break;
                    

                    if (v2 == this)
                    {
                        return true;
                    }
                        
                
            }
            return false;
          }
            
    }
}
