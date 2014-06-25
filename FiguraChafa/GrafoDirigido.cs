using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace RunnerRedBayesiana
{
    [SerializableAttribute]
    class GrafoDirigido : Grafo
    {
        private int tiempo;
        public GrafoDirigido()
        {

        }
        public override void dibujaGrafo(Graphics osdc,bool activo)
        {
            foreach (Vertice vertice in listV)
            {
                //dibuja los vertices y el estilo indica que las aristas 
                //que salen del vertice tiene flecha (1 = flecha, 0 = sin flecha)
              
                //vertice.setColor(Color.Cyan);
                vertice.dibujate(osdc, 1);

                if (!activo)
                {
                    vertice.dibujate(osdc, 1);
                }
            }
        }
        public override bool esDirigido()
        {
            return true;
        }

        bool tienePeso()
        {
            List<Arista> lA = new List<Arista>();
            foreach (Vertice v in listV)
            {
                lA = v.dameRelaciones();
                foreach (Arista a in lA)
                {
                    if (a.getPeso() == 0)
                    {
                        System.Windows.Forms.MessageBox.Show("No todas las aristas tienen peso");
                        return false;
                    }
                }

            }
            
            return true;
        }
        public override bool esPlano()
        {
            throw new Exception("The method or operation is not implemented.");
        }
        public override int numeroAristas()
        {
            int num = 0;
            foreach (Vertice v in listV)
            {
                num += v.numAristas();
            }
            return num;
        }
        public override void agregaArista(Arista arista)
        {
            Vertice v1 = arista.v1;
            v1.agregaArista(arista);
        }
        public override void eliminaArista(Point pos)
        {
            foreach (Vertice v in listV)
            {
                //si es verdadero, ya elimino una arista
                //hacemos esta verificacion para que no borre mas de una arista
                if (v.eliminaArista(pos) == true)
                    break;
            } 
        }

        private void ImprimeMatriz(int[,] m)
        {
            int n = (int)Math.Sqrt(m.Length);
            int valor;
            //Form4 forma4 = new Form4(n);
            String[] row = new string[n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {

                    valor = m[i, j];
                    if (valor < 99999)
                    {
                        row[j] = Convert.ToString(valor);
                    }
                    else
                        row[j] = "infinito";


                }
                //forma4.imprime(row);
            }
            //forma4.Show();
        }
        public override int[,] calculaFloyd()
        {
            
            int n = listV.Count + 1;
            int[,] matriz = null;
            int[,] mFloyd = null;
            Arista a = null;

            int comp = componentes();
            if (comp == 1)
            {
                if (tienePeso())
                {
                    matriz = new int[n, n];
                    mFloyd = new int[n, n];

                    for (int i = 1; i < n; i++)
                    {
                        matriz[i, 0] = listV[i - 1].id;
                        for (int j = 1; j < n; j++)
                        {
                            matriz[0, j] = listV[j - 1].id;
                            if (i == j)
                                matriz[i, j] = 0;
                            else
                            {
                                a = listV[i - 1].dameAristaCon(listV[j - 1]);
                                if (a != null)
                                {
                                    matriz[i, j] = a.getPeso();
                                }
                                else
                                    matriz[i, j] = 999999;
                            }

                        }
                    }

                    for (int k = 1; k < n; k++)
                    {
                        for (int i = 1; i < n; i++)
                        {
                            for (int j = 1; j < n; j++)
                            {
                                if (matriz[i, k] + matriz[k, j] < matriz[i, j])
                                    matriz[i, j] = matriz[i, k] + matriz[k, j];

                            }
                        }
                    }
                    //imprimeMatriz(mFloyd);
                }
                
            }
            else
                System.Windows.Forms.MessageBox.Show("El grafo no esta conectado");

            return matriz;
        }
        public override Vertice centroGrafo()
        {
            Vertice v = null;
            int val1 = 0, val2 = 0, centro = 999999, columna = 0, col = 0;
            int[,] mFloyd = null;

            mFloyd = calculaFloyd();

            if (mFloyd != null)
            {
                int n = (int)Math.Sqrt(mFloyd.Length);
                List<int> lint = new List<int>();
                List<int> lintM = new List<int>();

                for (int j = 1; j < n; j++)
                {
                    for (int i = 1; i < n; i++)
                    {
                        val1 = mFloyd[i, j];
                        if (val1 > val2)
                        {
                            val2 = val1;
                            columna = j;
                        }
                    }
                    lint.Add(val2);
                    val2 = 0;
                    lintM.Add(mFloyd[0, columna]);
                }

                columna = 0;
                String cad = "La Suma de Excentricidades es: ";
                foreach (int valor in lint)
                {
                    columna++;
                    if (valor < centro)
                    {
                        col = columna;
                        centro = valor;
                    }
                    if(valor == 999999)
                        cad = cad + " " + Convert.ToString("infinito");
                    else
                        cad = cad + " " + Convert.ToString(valor);
                }
                
                v = dameVertice(mFloyd[0, col]);

                if (v != null)
                {
                    v.setColor(Color.Red);
                    cad = cad + " Por lo tanto: " + Convert.ToString(v.id) + " es el centro con excentricidad de: " + Convert.ToString(centro);
                    System.Windows.Forms.MessageBox.Show(cad);
                }
                else
                    System.Windows.Forms.MessageBox.Show("No hay Centro");
      
            }
            return v;
        }

        public void bpf(Vertice v)
        {
            v.marcado = true;
            v.id2 = tiempo;
            tiempo++;

            List<Arista> listA = ordena(v.dameRelacionesSM(), 0);
            //Hay que ordenar la listA de menor a Mayor
            foreach (Arista a in listA)
            {
                if (a.v2.marcado == false)
                {
                    //Aristas de arbol
                    a.setColor(Color.DodgerBlue);
                    bpf(a.v2);
                }
            }
        }
        public void bpf2(Vertice v)
        {
            v.marcado = true;
            v.id3 = tiempo;
            tiempo++;

            List<Arista> listA = ordena(v.dameRelacionesSM(), 0);
            //Hay que ordenar la listA de menor a Mayor
            foreach (Arista a in listA)
            {
                if (a.v2.marcado == false)
                {
                    //Aristas de arbol
                    a.setColor(Color.DodgerBlue);
                    bpf2(a.v2);
                }
            }
        }
        public override void baf()
        {
            //Form5 form5 = new Form5();
            respalda();
            List<Arista> lA;
            //Inicializacion////////////
            tiempo = 0;
            foreach (Vertice v in listV)
            {
                v.id2 = 0;
                v.marcado = false;
                lA = v.dameRelaciones();
                foreach (Arista a in lA)
                {
                    a.setColor(Color.Black);
                    a.marcada = false;
                }
            }
            ////////////////////////////
            foreach (Vertice v in listV)
            {
                if (v.marcado == false)
                {
                    bpf(v);
                }
            }
            //Coloreado de aristas que se eliminarian
            foreach (Vertice v in listV)
            {
                lA = v.dameRelaciones();
                foreach (Arista a in lA)
                {
                    if (a.v1.id2 >= a.v2.id2)
                        a.setColor(Color.DarkGray);

                    //Arisas de Avance
                    if (a.v1.id2 <= a.v2.id2 && a.getColor() == Color.Black)
                        a.setColor(Color.Green);
                }
            }

            //Aristas de retroceso
            foreach (Vertice v in listV)
            {
                lA = v.dameRelaciones();
                foreach (Arista a1 in lA)
                {
                    if (a1.v1.id2 >= a1.v2.id2 && a1.getColor() == Color.DarkGray)
                    {
                        if (a1.v2.hayCaminoCon(a1.v1))
                            a1.setColor(Color.Yellow);
                    }
                }
            }
            //form5.ShowDialog();
            restaura();

        }
        List<Arista> ordena(List<Arista> listA, int criterio)
        {
            Arista ar;
            List<Arista> lA = new List<Arista>();    
            while (listA.Count != 0)
            {
                ar = listA[0];
                foreach (Arista a in listA)
                {
                    if (criterio == 0)
                    {
                        if (ar.v2.id > a.v2.id)
                        {
                            ar = a;
                        }
                    }
                    if(criterio == 1)
                    {
                        if (ar.v2.id2 < a.v2.id2)
                        {
                            ar = a;
                        }
                    }
                }
                lA.Add(ar);
                listA.Remove(ar);
            }
            return lA;
        }
        public void bpfCF(Vertice v)
        {

            v.marcado = true;
            List<Arista> listA = ordena(v.dameRelacionesSM(),0);
            //Hay que ordenar la listA de menor a Mayor
            foreach (Arista a in listA)
            {
                if (a.v2.marcado == false)
                {
                    //Aristas de arbol
                    a.setColor(Color.DodgerBlue);
                    bpfCF(a.v2);
                }
            }
            v.id2 = tiempo;
            tiempo++;
        }
        public void bpfCF2(Vertice v)
        {

            v.marcado = true;
            List<Arista> listA = ordena(v.dameRelacionesSM(), 1);
            //Hay que ordenar la listA de menor a Mayor
            foreach (Arista a in listA)
            {
                if (a.v2.marcado == false)
                {
                    //Aristas de arbol
                    a.setColor(Color.DodgerBlue);
                    bpfCF2(a.v2);
                }
            } 
        }
        public Vertice dameVertconMayorId2()
        {
            Vertice vertice = null;
            if(listV.Count != 0)
            {
                foreach (Vertice v in listV)
                {
                    if (v.marcado == false)
                    {
                        vertice = v;
                        break;
                    }
                }
                foreach (Vertice v in listV)
                {
                    if (v.id2 > vertice.id2 && v.marcado == false)
                        vertice = v;
                }
            }
            return vertice;
        }
        public bool hayVerticeSinMArca()
        {
            int marcas = 0;
            foreach (Vertice v in listV)
            {
                if (v.marcado == true)
                    marcas++;
            }
            if (marcas == listV.Count)
                return true;
            else
                return false;
        }
        public override void compFuertes()
        {
           
            List<Arista> lA;
            respalda();
            //Inicializacion////////////
            tiempo = 1;
            foreach (Vertice v in listV)
            {
                v.id2 = 0;
                v.marcado = false;
                lA = v.dameRelaciones();
                foreach (Arista a in lA)
                {
                    a.setColor(Color.Black);
                    a.marcada = false;
                }
            }

            foreach (Vertice v in listV)
            {
                if(v.marcado == false)
                    bpfCF(v);
            }
            

            ///Invertimos Aristas///////////////
            List<Arista> lA2 = new List<Arista>();
            foreach (Vertice v in listV)
            {
                lA = v.dameRelaciones();  
                foreach (Arista a in lA)
                {
                    lA2.Add(a);  
                }
                lA.Clear();
            }
            foreach (Arista a in lA2)
            {
                this.agregaArista(new Arista(a.v2, a.v1));
            }
            
            ///Volvemos a inicializar/////////
            tiempo = 1;
            foreach (Vertice v in listV)
            {
                v.id3 = 0;
                v.marcado = false;
                lA = v.dameRelaciones();
                foreach (Arista a in lA)
                {
                    a.setColor(Color.Black);
                    a.marcada = false;
                }
            }
            //Volvemos a hacer una busqueda en profundidad
            int componentes = 0;
            Vertice vert = listV[0];
            while (!hayVerticeSinMArca())
            {
                componentes++;
                bpf2(dameVertconMayorId2());

            }
            ////Para colorear Aristas fuera del componente
            List<Arista> lAux = new List<Arista>();
            foreach (Vertice v in listV)
            {
                lAux = v.dameRelaciones();
                foreach(Arista a in lAux)
                {
                    if (Color.DodgerBlue != a.getColor())
                        a.setColor(Color.DarkGray);
                }
            }
            System.Windows.Forms.MessageBox.Show("Se encontraron " + Convert.ToString(componentes) + " componenetes Fuertes");
            restaura();
        }
        public int componentes()
        {
            List<Arista> lA;
           
            //Inicializacion////////////
            tiempo = 1;
            foreach (Vertice v in listV)
            {
                v.id2 = 0;
                v.marcado = false;
                lA = v.dameRelaciones();
                foreach (Arista a in lA)
                {
                    a.setColor(Color.Black);
                    a.marcada = false;
                }
            }
            bpfCF(listV[0]);

            /**********Invertimos Aristas*******/
            List<Arista> lA2 = new List<Arista>();
            //Metemos todas las aristas del grafo en lA2
            foreach (Vertice v in listV)
            {
                lA = v.dameRelaciones();
                foreach (Arista a in lA)
                {
                    lA2.Add(a);
                }
                lA.Clear();
            }
            //Respaldamos Aristas antes de invertirlas
            List<Arista> lAR = new List<Arista>(lA2);
            //Invertimos Aristas
            foreach (Arista a in lA2)
            {
                this.agregaArista(new Arista(a.v2, a.v1));
                this.agregaArista(new Arista(a.v1, a.v2));
            }
            //************************************///
            //Volvemos a inicializar/////////
            tiempo = 1;
            foreach (Vertice v in listV)
            {
                v.marcado = false;
                lA = v.dameRelaciones();
                foreach (Arista a in lA)
                {
                    a.setColor(Color.Black);
                    a.marcada = false;
                }
            }
            int componentes = 0;
            foreach (Vertice v in listV)
            {
                if (v.marcado == false)
                {
                    componentes++;
                    bpfCF2(v);
                }
            }
            //Para colorear Aristas fuera del componente
            List<Arista> lAux = new List<Arista>();
            foreach (Vertice v in listV)
            {
                lAux = v.dameRelaciones();
                foreach (Arista a in lAux)
                {
                    if (Color.DodgerBlue != a.getColor())
                        a.setColor(Color.DarkGray);
                }
            }

            //Restauramos Aristas/////////////
            foreach (Vertice v in listV)
            {
                lA = v.dameRelaciones();
                foreach (Arista a in lA)
                {
                    a.setColor(Color.Black);
                }
                lA.Clear();
            }
            foreach (Arista a in lAR)
            {
                Arista arista = new Arista(a.v1, a.v2);
                arista.setPeso(a.getPeso());
                this.agregaArista(arista);
            }
            return componentes;
            /////////////////////////////////////
        }
    }
}
