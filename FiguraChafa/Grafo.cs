using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace RunnerRedBayesiana
{
    [SerializableAttribute]
    abstract class Grafo
    {
        protected List<Vertice> listV = new List<Vertice>();//Lista de Vertices
        private List<Vertice> vR = null;
        private List<Arista> lR = null;

        //Metodos Para Manipulacion del Grafo
        public Grafo()
        {
            
        }
        public void setColor(Color color)
        {
            foreach (Vertice v in listV)
                v.setColor(color);
        }
        public void setColorAristas(Color color)
        {
            List<Arista> listA;
            foreach (Vertice v in listV)
            {
                listA = v.dameRelaciones();
                foreach (Arista arista in listA)
                {
                    arista.setColor(color);
                    arista.marcada = false;
                }
            }
        }
        public virtual void dibujaGrafo(Graphics osdc,bool activo)
        {

        }
        public virtual bool esDirigido()
        {
            return false;
        }
        public void muevete(int x1, int y1, int x2, int y2)
        {
            int dx = x2 - x1;
            int dy = y2 - y1;

            foreach (Vertice v in listV)
            {
                v.pos.X += dx;
                v.pos.Y += dy;
            }
        }
        public bool intersect(Point p)
        {
            return (hayVertice(p));
        }
        public bool hayVertice(Point position)
        {
            foreach (Vertice pV in listV)
            {
                if (pV.Intersect(position))
                {
                    return true;
                }
            }
            return false;
        }
        public bool hayArista(Point position)
        {
            
            List<Arista> lA = new List<Arista>();
            foreach (Vertice pV in listV)
            {
                lA = pV.dameRelaciones();
                foreach (Arista a in lA)
                {
                    if (a.intersectOn(position))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public Arista dameArista(Point position)
        {
            List<Arista> lA = new List<Arista>();
            Arista arista = null;
            foreach (Vertice pV in listV)
            {
                lA = pV.dameRelaciones();
                foreach (Arista a in lA)
                {
                    if (a.intersectOn(position))
                    {
                        arista = a;
                    }
                }
            }
            return arista;
        }
        
        public virtual void colorGrafo(Color color)
        {

        }
        public void respalda()
        {    
            //Resplado vertices
            vR = new List<Vertice>(listV);
            /****Respaldo Arista***************/
            List<Arista> lA;
            lR = new List<Arista>();
            //Metemos todas las aristas del grafo en lA2
            foreach (Vertice v in listV)
            {
                lA = v.dameRelaciones();
                foreach (Arista a in lA)
                {
                    lR.Add(a);
                }
            }    
        }
        public void restaura()
        {
            if (listV.Count != 0 && vR != null)
            {
                listV.Clear();

                List<Arista> lA3;
                //Restauramos Vertices
                foreach (Vertice v in vR)
                {
                    v.setColor(Color.GreenYellow);
                    lA3 = v.dameRelaciones();
                    lA3.Clear();
                    this.agregaVertice(v);
                }
                //Restauramos Aristas
                foreach (Arista a in lR)
                {
                    //Arista arista = new Arista(a.v1, a.v2);
                    //arista.setPeso(a.getPeso());
                    Arista arista = new Arista(a.v1, a.v2);
                    arista.setPeso(a.getPeso());

                    a.v1.agregaArista(arista);
                    //this.agregaArista(a);
                }
            }
        }

        public bool[,] obtenMatrizAdyacencia()
        {
            var matrizAdyacencia = new bool[listV.Count,listV.Count];
            for (int i = 0; i < listV.Count; i++)
            {
                var vertice1 = listV[i];
                for (int j = 0; j < listV.Count; j++)
                {
                    var vertice2 = listV[j];
                    if (vertice1.tieneAristaCon(vertice2))
                        matrizAdyacencia[i, j] = true;
                }
            }
            return matrizAdyacencia;
        }


        //Metodos Virtuales para Grafos No Dirigidos
        public virtual bool tieneCamino()
        {
            return false;
        }
        public virtual bool tieneCircuito()
        {
            return false;
        }
        public virtual bool esBipartita()
        {
            return false;
        }
        public virtual void complemento()
        {
            
        }
        public virtual String calculaEuler(Graphics g)
        {
            return "";
        }
        public virtual void dibujaCamino(Graphics g, String camino, int iter)
        {
        }
        public virtual bool dibujaArista(Graphics g, Arista arista, int iter)
        {
            return false;
        }
        abstract public bool esPlano();
        public virtual bool kuratowsky_k33()
        {
            return false;
        }
        public virtual bool kuratowsky_k5()
        {
            return false;
        }
        public virtual void colorea()
        {
        }
        public virtual void prim()
        {

        }


        //Metodos para Grafos Dirigidos
        public virtual int[,] calculaFloyd()
        {
            int[,] nada = new int[4, 4];
            return nada;
        }
        public virtual Vertice centroGrafo()
        {
            Vertice v = null;
            return v;
        }
        public virtual void baf()
        {
        }
        public virtual void compFuertes()
        {

        }

        //Metodos Para la Manipulacion de Vertices
        public int numeroVertices()
        {
            return listV.Count;
        }
        public void agregaVertice(Vertice vertice)
        {
            listV.Add(vertice);
        }
        public void eliminaVertice(Vertice vertice)
        {
            foreach (Vertice v in listV)
            {
                v.eliminaRelacionesCon(vertice);
            }
            listV.Remove(vertice);
        }
        public void CopiaVertices(List<Vertice> vertices)
        {
            foreach (Vertice v in vertices)
            {
                agregaVertice(v);
            }
        }
        public Vertice dameVertice(Point pos)
        {
            foreach (Vertice v in listV)
            {
                if (v.Intersect(pos))
                    return v;
            }
            Vertice vertice = null;
            return vertice;
        }
        public Vertice dameVertice(int id)
        {
            foreach (Vertice v in listV)
            {
                if (v.id == id)
                    return v;
            }
            Vertice vertice = null;
            return vertice;
        }

        //Metodos para Manipulacion de Aristas
        public virtual int numeroAristas()
        {
            return 0;
        }      
        public virtual void agregaArista(Arista arista)
        {          
        }
        public virtual void eliminaArista(Point pos)
        {
        }     
    }
}
