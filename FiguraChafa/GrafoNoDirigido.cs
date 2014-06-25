using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace RunnerRedBayesiana
{
    [SerializableAttribute]
    class GrafoNoDirigido : Grafo
    {
        private int tiempo;
        public GrafoNoDirigido()
        {

        }
        public override bool esDirigido()
        {
            return false;
        }
        public override void dibujaGrafo(Graphics osdc, bool activo)
        {
            foreach (Vertice vertice in listV)
            {
                //dibuja los vertices y el estilo indica que las aristas 
                //que salen del vertice no tienen flecha (1 = flecha, 0 = sin flecha)
                //vertice.setColor(Color.GreenYellow);
                vertice.dibujate(osdc, 0);
                if (!activo)
                {
                    vertice.dibujate(osdc, 0);
                }
            }
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
                    if (criterio == 1)
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
        void bpfCF(Vertice v)
        {

            v.marcado = true;
            List<Arista> listA = ordena(v.dameRelacionesSM(), 0);
            //Hay que ordenar la listA de menor a Mayor
            foreach (Arista a in listA)
            {
                if (a.v2.marcado == false)
                {
                    //Aristas de arbol
                    //a.setColor(Color.DodgerBlue);
                    bpfCF(a.v2);
                }
            }
            v.id2 = tiempo;
            tiempo++;
        }
        void bpfCF2(Vertice v)
        {

            v.marcado = true;
            List<Arista> listA = ordena(v.dameRelacionesSM(), 1);
            //Hay que ordenar la listA de menor a Mayor
            foreach (Arista a in listA)
            {
                if (a.v2.marcado == false)
                {
                    //Aristas de arbol
                    //a.setColor(Color.DodgerBlue);
                    bpfCF2(a.v2);
                }
            }
        }
        int componentes()
        {
            int res = 0;
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

            //Invertimos Aristas///////////////
            /**List<Arista> lA2 = new List<Arista>();
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
            }*/
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

            foreach (Vertice v in listV)
            {
                if (v.marcado == false)
                {
                    res++;
                    bpfCF2(v);
                }
            }


            foreach (Vertice v in listV)
            {
                v.marcado = false;
            }
            return res;
        }

        public override bool esBipartita()
        {
            foreach (Vertice v in listV)
            {
                v.setColor(Color.GreenYellow);
            }
            //Inicializamos
            foreach (Vertice v in listV)
            {
                v.id2 = 0;
            }
            //Checamos si hay mas de un conjunto
            int var = componentes();
            if (var > 1 || listV.Count == 1)
            {
                System.Windows.Forms.MessageBox.Show("El grafo no esta conectado");
                return false;
            }
            //Inicializamos
          
            foreach (Vertice v in listV)
            {
                if (v.id % 2 == 0)
                    v.id2 = 1;
                else
                    v.id2 = 0;
            }

            List<Arista> lA;
            foreach(Vertice ve in listV)
            {
                lA = ve.dameRelaciones();
                foreach (Arista a in lA)
                {
                   
                    if (a.v1.id2 == 2)
                    {
                        a.v2.id2 = 1;
                    }
                    if (a.v1.id2 == 1)
                    {
                        a.v2.id2 = 2;
                    }
                }
            }

            //Chequeo de que ninguna arista tenga vertices con mismo id2
            foreach (Vertice vert in listV)
            {
                lA = vert.dameRelaciones();
                foreach (Arista a in lA)
                {
                    if (a.v1.id2 == a.v2.id2)
                    {
                        System.Windows.Forms.MessageBox.Show("No es Bipartita");
                        return false;
                    }
                }
            }

            foreach (Vertice v in listV)
            {
                if (v.id2 == 2)
                    v.setColor(Color.Cyan);
            
            }

            return true;
            
        }
        public override void complemento()
        {
            List<Arista> lArRelE = new List<Arista>(); //Existentes
            List<Arista> lArRelC = new List<Arista>(); //complemento

            foreach (Vertice v in listV)
            {
                lArRelE = v.dameRelaciones();
                if (lArRelE.Count == 0)
                {
                    foreach (Vertice v2 in listV)
                    {
                        if (v != v2) //Para que no haya orejas
                            lArRelE.Add(new Arista(v, v2));
                    }
                }
                else
                {
                    foreach (Vertice vert in listV)
                    {
                        if (!v.tieneAristaCon(vert) && v != vert) //No orejas
                            lArRelC.Add(new Arista(v, vert));
                    }
                    v.estableRelaciones(lArRelC);
                    lArRelE.Clear();
                    lArRelC.Clear();
                }
            }
        }
        public override bool tieneCamino()
        {
            int tolerancia = 0; //Si permanece en Cero hay Circuito
            foreach (Vertice v in listV)
            {
                if (v.numAristas() % 2 != 0)
                {
                    tolerancia++;
                    if (tolerancia > 2)
                    {
                        System.Windows.Forms.MessageBox.Show("No es Euler");
                        return false;
                    }
                }
                if (v.numAristas() == 0)
                {
                    return false;
                }
                int var = componentes();
                if (var > 1)
                {
                    System.Windows.Forms.MessageBox.Show("Grafo No conectado");
                    return false;
                }
            
            }
            return true;  //Si hay Caminos
        }
        public override bool tieneCircuito()
        {

            foreach (Vertice v in listV)
            {
                if (v.numAristas() % 2 != 0)
                {
                    System.Windows.Forms.MessageBox.Show("No es Euler");
                    return false;
                }

                if (v.numAristas() == 0)
                {
                    System.Windows.Forms.MessageBox.Show("No es Euler");
                    return false;
                }

            }
            int var = componentes();
            if (var > 1)
            {
                System.Windows.Forms.MessageBox.Show("Grafo No conectado");
                return false;
            }
            return true;  //Si hay Caminos   
        }
        public override bool dibujaArista(Graphics g, Arista arista, int iter)
        {
            int indice;

            if (arista != null && arista.pixeles.Count - 1 > iter)
            {
                indice = arista.pixeles.Count - iter - 1;
                arista.ptRect = arista.pixeles[indice];
                //g.FillRectangle(Brushes.White, arista.pixeles[iter]);

            }
            else
                return true;

            return false;
           
        }
        public override void dibujaCamino(Graphics g, String camino, int iter)
        {
            List<int> lnum = new List<int>();
            string[] str2 = camino.Split(',');
            foreach (string str1 in str2)
            {
                lnum.Add(Convert.ToInt32(str1));
            }

            if (lnum.Count - 1 > iter)
            {
                listV[lnum[iter]].dameAristaCon(listV[lnum[iter + 1]]).marcada = true;
                listV[lnum[iter + 1]].dameAristaCon(listV[lnum[iter]]).marcada = true;


                //listV[lnum[iter]].dameAristaCon(listV[lnum[iter + 1]]).asignaPixeles(g);
                //g.FillRectangle(Brushes.White, listV[lnum[iter]].dameAristaCon(listV[lnum[iter + 1]]).pixeles[iter]);

                //listV[lnum[iter]].dameAristaCon(listV[lnum[iter + 1]]).dibujaCamino(g, iter);
                //listV[lnum[iter+1]].dameAristaCon(listV[lnum[iter]]).dibujaCamino(g, iter);
            }
            else
            {
                foreach (Vertice vert in listV)
                {
                    List<Arista> aristas;
                    aristas = vert.dameRelaciones();
                    foreach (Arista arista in aristas)
                    {
                        arista.marcada = false;
                    }
                }
            }
        }
        public override String calculaEuler(Graphics g)
        {
            /**List<Arista> listAr;
            foreach (Vertice vert in listV)
            {
                listAr = vert.dameRelaciones();
                foreach(Arista ar in listAr)
                {
                    ar.setColor(Color.Black);
                    ar.pixeles.Clear();
                    ar.ptRect = new Rectangle();
                }
            }**/

            Vertice v = listV[0];
            foreach (Vertice vtce in listV)
            {
                if (vtce.numAristas() % 2 != 0)
                    v = vtce;
            }

            Arista a = v.dameAristaSinMarca();
            //inicializamos cadena que indica el camino
            //con el primer elemento de la lista
            String SubCircuito = "";
            String strCamino = "";
            
            while (a != null) 
            {
                //Buscamos que ya no haya caminos en ningun vertice
                SubCircuito = SubCircuito + (Convert.ToString(v.id));

                //Marcamos la arista de v2 a v1
                a.v2.dameAristaCon(v).marcada = true;
                v = a.v2;

                
                //a.dibujaCamino(g);
                
                //Y marcamos la Arista de v1 a v2
                a.marcada = true;
                a = v.dameAristaSinMarca();



                //Este caso es por si ya ya no tiene salida el recorrido
                // y busca vertices aún con caminos disponibles
                if (a == null)
                {   //Checar
                    SubCircuito = SubCircuito + ", ";
                    SubCircuito = SubCircuito + (Convert.ToString(v.id));
                    foreach (Vertice vt in listV)
                    {
                        a = vt.dameAristaSinMarca();
                        if (a != null)
                        {
                            v = vt;
                            break;
                        }
                    }
                    //Aqui la concatenacion de cadenas
                    //Caso para un solo circuito
                    if (strCamino == "")
                    {
                        strCamino = SubCircuito;
                        SubCircuito = "";
                    }//Caso para mas de un subcircuito
                    else
                    {
                        for (int i = 0; i < strCamino.Length; i++)
                        {   //si strCamino se encuentra un caracter similar
                            //con respecto a camino de cero
                            if (strCamino[i] == SubCircuito[1])
                            {
                                //SubCircuito = SubCircuito.Remove(SubCircuito.Length-3);
                                SubCircuito = SubCircuito.Remove(1,3);
                                //hacemos una inserccion ordenada de toda la cadena camino
                                strCamino = strCamino.Insert(i+2, SubCircuito);
                                SubCircuito = "";
                                break;
                            }
                        }
                        
                    }   
                }
                SubCircuito = SubCircuito + ", ";
            }

            //Desmarcamos las Aristas
            foreach (Vertice vert in listV)
            {
                 List<Arista> aristas;
                 aristas = vert.dameRelaciones();
                 foreach (Arista arista in aristas)
                 {
                     arista.marcada = false;
                 }
            }
            
            //Regresamos la cadena del camino
            return strCamino;
        }
        public override bool esPlano()
        {
            //Checar este metodo ya que no funciona
            bool valor = false;
            if (numeroAristas() <= 3 * numeroVertices() - 6)
                valor = true;
            else
                valor = false;

            //Checar esta condicion
            if (!tieneCamino() && numeroAristas() > 3)
            {
                if (numeroAristas() <= 2 * numeroVertices() - 4)
                    valor = true;
                else
                    valor = false;
            }
            return valor;
        }
        public override bool kuratowsky_k33()
        {
            DesmarcaAristas();
            colorGrafo(Color.GreenYellow);
            int n = listV.Count;
            bool exit = false;
            Vertice c1v1, c1v2, c1v3;
            Vertice c2v1, c2v2, c2v3;

            for (int it1 = 0; it1 < n ; it1++)
            {
                c1v1 = listV[it1];
                for (int it2 = 0; it2 < n ; it2++)
                {
                    c1v2 = listV[it2];
                    for (int it3 = 0; it3 < n ; it3++)
                    {
                        c1v3 = listV[it3];
                        for (int cit1 = 0; cit1 < n ; cit1++)
                        {
                            c2v1 = listV[cit1];
                            for (int cit2 = 0; cit2 < n ; cit2++)
                            {
                                c2v2 = listV[cit2];
                                for (int cit3 = 0; cit3 < n ; cit3++)
                                {
                                    c2v3 = listV[cit3];
                                    if(
                                        c1v1.numAristas() >= 3 &&
                                        c1v2.numAristas() >= 3 &&
                                        c1v3.numAristas() >= 3 &&
                                        c2v1.numAristas() >= 3 &&
                                        c2v2.numAristas() >= 3 &&
                                        c2v3.numAristas() >= 3
                                        )
                                    if (
                                        MarcaCamino(c1v1, c2v1) &&
                                        MarcaCamino(c1v1, c2v2) &&
                                        MarcaCamino(c1v1, c2v3) &&
                                        MarcaCamino(c1v2, c2v1) &&
                                        MarcaCamino(c1v2, c2v2) &&
                                        MarcaCamino(c1v2, c2v3) &&
                                        MarcaCamino(c1v3, c2v1) &&
                                        MarcaCamino(c1v3, c2v2) &&
                                        MarcaCamino(c1v3, c2v3) &&
                                        c1v1.numMarcas() == 3 &&
                                        c1v2.numMarcas() == 3 &&
                                        c1v3.numMarcas() == 3 &&
                                        c2v1.numMarcas() == 3 &&
                                        c2v2.numMarcas() == 3 &&
                                        c2v3.numMarcas() == 3
                                    )
                                    {
                                        exit = true;
                                        colorGrafo(Color.GreenYellow);
                                        foreach (Vertice v in listV)
                                        {
                                            
                                            if (v != c1v1 && v != c1v2 && v != c1v3 && v != c2v1 && v != c2v2 && v != c2v3)
                                            {
                                                if (v.numMarcas() > 2)
                                                    exit = false;
                                            }
                                            if (v.numMarcas() == 0)
                                            {
                                                v.setColor(Color.Gray);
                                                //exit = true; //Solo para eliminacion de vertices
                                            }
                                        }
                                        if (exit)
                                        if (System.Windows.Forms.MessageBox.Show("K3,3 No Plano Desea buscar otro resultado", "Peticion",
                                                System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                                        {
                                            //exit = true;//Para eliminacion de vertices y aristas
                                            if (System.Windows.Forms.MessageBox.Show("Desea acomodar el K3,3", "Peticion",
                                                    System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                                            {
                                                c1v1.pos.X = 300;
                                                c1v1.pos.Y = 200;
                                                c1v2.pos.X = 500;
                                                c1v2.pos.Y = 200;
                                                c1v3.pos.X = 700;
                                                c1v3.pos.Y = 200;

                                                c2v1.pos.X = 300;
                                                c2v1.pos.Y = 600;
                                                c2v2.pos.X = 500;
                                                c2v2.pos.Y = 600;
                                                c2v3.pos.X = 700;
                                                c2v3.pos.Y = 600;
                                            }
                                            return true;
                                        }
                                        
                                    }else
                                        DesmarcaAristas();
      
                                }
                                
                            }
                            
                        }
                    }
                }
            }
            return false;            
        }
        public override bool kuratowsky_k5()
        {
            DesmarcaAristas();
            colorGrafo(Color.GreenYellow);
            int n = listV.Count;
            bool exit = false;
            Vertice v1, v2, v3, v4, v5;

            for (int it1 = 0; it1 < n; it1++)
            {
                v1 = listV[it1];
                for (int it2 = 0; it2 < n; it2++)
                {
                    v2 = listV[it2];
                    for (int it3 = 0; it3 < n; it3++)
                    {
                        v3 = listV[it3];
                        for (int it4 = 0; it4 < n; it4++)
                        {
                            v4 = listV[it4];
                            for (int it5 = 0; it5 < n; it5++)
                            {
                                v5 = listV[it5];
                                if (v1.numAristas() > 3 && v2.numAristas() > 3 && v3.numAristas() > 3 &&
                                    v4.numAristas() > 3 && v5.numAristas() > 3)
                                {
                                        if (
                                            MarcaCamino(v1, v2) &&
                                            MarcaCamino(v1, v3) &&
                                            MarcaCamino(v1, v4) &&
                                            MarcaCamino(v1, v5) &&
                                            MarcaCamino(v2, v3) &&
                                            MarcaCamino(v2, v4) &&
                                            MarcaCamino(v2, v5) &&        
                                            MarcaCamino(v3, v4) &&
                                            MarcaCamino(v3, v5) &&                      
                                            MarcaCamino(v4, v5) &&

                                            v1.numMarcas() == 4 &&
                                            v2.numMarcas() == 4 &&
                                            v3.numMarcas() == 4 &&
                                            v4.numMarcas() == 4 &&
                                            v5.numMarcas() == 4 
                                            
                                        )
                                        {
                                            exit = true;
                                            colorGrafo(Color.GreenYellow);
                                            foreach (Vertice v in listV)
                                            {
                                                if (v != v1 && v != v2 && v != v3 && v != v4 && v != v5)
                                                {
                                                    if (v.numMarcas() > 2)
                                                        exit = false;
                                                }
                                                if (v.numMarcas() == 0)
                                                {
                                                    v.setColor(Color.Gray);
                                                    //exit = true; //Solo para eliminacion de vertices
                                                }
                                            }
                                           
                                            if(exit)
                                            if (System.Windows.Forms.MessageBox.Show("K5 No Plano, Desea Buscar otro resultado", "Peticion",
                                                    System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                                            { 
                                                if (System.Windows.Forms.MessageBox.Show("Desea acomodar el K5", "Peticion",
                                                        System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                                                {
                                                        v1.pos.X = 712;
                                                        v1.pos.Y = 350;
                                                        v2.pos.X = 573;
                                                        v2.pos.Y = 540;
                                                        v3.pos.X = 350;
                                                        v3.pos.Y = 467;
                                                        v4.pos.X = 350;
                                                        v4.pos.Y = 232;
                                                        v5.pos.X = 573;
                                                        v5.pos.Y = 159;
                                                        
                                                }
                                                
                                                return true;
                                                //exit = true;
                                             }    
                                        }
                                        else
                                            DesmarcaAristas();
                                }
                                }

                            }
                        
                        }
                }
            }
            return false;
            
        }
        public void ColoreaCamino(Vertice v1, Vertice v2)
        {
            List<Arista> pA = new List<Arista>();//El resultado esta en pA contiene las aristas que dibujan el camino
            List<Arista> aA; //Auxiliar de aristas para checar por cada vertice
            List<Vertice> aV1 = new List<Vertice>(); //auxiliar de vertices aV1 para comprobar en cada ciclo
            List<Vertice> aV2 = new List<Vertice>(); //Para almacenar los  vertices que pasaran a ser aV1 
            int watchdog = 0;

            if (listV.Contains(v1) && listV.Contains(v2))
            {
                aV1.Add(v1);//Inicializamos con el primer vertice
                while (true)
                {
                    //Recorremos la lista de Vertices  que tuvo relacion con el vertice anterior
                    for (int i = 0; i < aV1.Count; i++)
                    {
                        //Pedimos relaciones con el vertice y las guardamos en aA
                        aA = aV1[i].dameRelaciones();
                        //Recorremos las relaciones por cada vertice 
                        foreach (Arista a in aA)
                        {
                            //Si un vertice a.v2 apunta a un destino
                            //Quiere decir que si se encontro el destino
                            if (a.v2 == v2 && a.v2 != v1)
                            {
                                //Coloreamos dos aristas porque es no dirigido
                                a.v2.dameAristaCon(aV1[i]).setColor(Color.Cyan);
                                a.setColor(Color.Cyan);

                                //Guardamos el camino de Aristas en pA
                                pA.Add(a.v2.dameAristaCon(aV1[i]));
                                pA.Add(a);

                                //hacemos v2 a v1 para volver a calcular es un estilo recursivo
                                v2 = a.v1;
                                ////Limpiamos los datos para evitar que se copie la basura de v2 a v1
                                ////Checar con este pedazo y volver a llamar la funcion
                                aV1.Clear();
                                aV1.Add(v1);
                                aV2.Clear();
                                //Copiamos aV1 en aV2
                                foreach (Vertice v in aV1)
                                {
                                    aV2.Add(v);
                                }
                                ///////////////////////////////////////////////

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
                    if (v2 == v1 || watchdog > 100000)
                        break;
                }
            }
        }
        public bool MarcaCamino(Vertice v1, Vertice v2)
        {
            List<Arista> pA = new List<Arista>();//El resultado esta en pA contiene las aristas que dibujan el camino
            List<Arista> aA; //Auxiliar de aristas para checar por cada vertice
            List<Vertice> aV1 = new List<Vertice>(); //auxiliar de vertices aV1 para comprobar en cada ciclo
            List<Vertice> aV2 = new List<Vertice>(); //Para almacenar los  vertices que pasaran a ser aV1 
            int watchdog = 0;

            if (listV.Contains(v1) && listV.Contains(v2) && v1 != v2)
            {
                aV1.Add(v1);//Inicializamos con el primer vertice
                while (true)
                {
                    //Recorremos la lista de Vertices  que tuvo relacion con el vertice anterior
                    for (int i = 0; i < aV1.Count; i++)
                    {
                        //Pedimos relaciones con el vertice y las guardamos en aA
                        aA = aV1[i].dameRelacionesSM();
                        //Recorremos las relaciones por cada vertice 
                        foreach (Arista a in aA)
                        {
                            //Si un vertice a.v2 apunta a un destino
                            //Quiere decir que si se encontro el destino
                            if (a.v2 == v2 && a.v2 != v1)
                            {
                                //Coloreamos dos aristas porque es no dirigido
                                //a.v2.dameAristaCon(aV1[i]).setColor(Color.DarkGray);
                                //a.setColor(Color.DarkGray);

                                //Guardamos el camino de Aristas en pA
                                pA.Add(a.v2.dameAristaCon(aV1[i]));
                                pA.Add(a);

                                //hacemos v2 a v1 para volver a calcular es un estilo recursivo
                                v2 = a.v1;
                                //Limpiamos los datos para evitar que se copie la basura de v2 a v1
                                aV1.Clear();
                                aV1.Add(v1);
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
                    if(watchdog > 9)
                        break;
                    

                    if (v2 == v1)
                    {
                        foreach (Arista a in pA)
                            a.marcada = true;
                        return true;
                    }
                        
                }
            }
            return false;
        }
        public void DesmarcaAristas()
        {
            foreach (Vertice v in listV)
            {
                v.desmarcaAristas();
            }
        }
      

        public override void prim()
        {
            List<Arista> listA = new List<Arista>();
            List<Arista> lAux;
            
            int valido = 0;
            //Almacenamos las aristas validas en una lista
            foreach (Vertice v in listV)
            {

                lAux = v.dameRelaciones();
                foreach (Arista a in lAux)
                {
                    valido++;
                    if (!listA.Contains(a) && a.getPeso() != 0)
                    {
                        listA.Add(a);
                    }
                }
                //lAux.Clear();
            }
 

            if (valido / 2 == listA.Count && componentes() <= 1 )
            {
                respalda();
                foreach (Vertice v in listV)
                {
                    lAux = v.dameRelaciones();
                    lAux.Clear();
                }
                
                foreach (Vertice v in listV)
                {
                    v.marcado = false;
                }
                //respalda();
                //Obtenemos la arista menor de listA y lo metemos en la lista resultado
                List<Arista> listResult = new List<Arista>();
                while (componentes() > 1 && listA.Count != 0)
                {
                    Arista aMenor = listA[0];
                    foreach (Arista a in listA)
                    {
                        if (aMenor.getPeso() >= a.getPeso())
                        {
                            aMenor = a;                           
                        }
                    }

                    aMenor.v1.marcado = true;
                    
                    

                    if (!MarcaCamino(aMenor.v2,aMenor.v1))
                    {
                        listResult.Add(aMenor);
                        Arista arista = new Arista(aMenor.v1, aMenor.v2);
                        arista.setPeso(aMenor.getPeso());
                        agregaArista(arista);
                        arista = new Arista(aMenor.v2, aMenor.v1);
                        agregaArista(arista);
                    }
      
                   listA.Remove(aMenor);
                    
                    
                }

                System.Windows.Forms.MessageBox.Show("El algoritmo termino su ejecucion");
                setColorAristas(Color.Black);
                
                
                restaura();
                
            }
            else
                System.Windows.Forms.MessageBox.Show("No Cumple todas sus condiciones para ejecutarse");


            
            
        }
        bool adyacentesColorDiferente(Vertice v)
        {
            List<Arista> lAdy = v.dameRelaciones();
            foreach (Arista a in lAdy)
            {
                if (v.vCrom == a.v2.vCrom)
                    return false;
            }
            return true;
        }
    
        public override void colorea()
        {
            int var = componentes();
            if (var <= 1)
            {
                colorGrafo(Color.GreenYellow);
                int vcrom = 0, color; //numero de colores  y valor cromatico para cada vertice
                List<Arista> lAdy;
                foreach (Vertice v in listV)
                {
                    lAdy = v.dameRelaciones();
                    foreach (Arista a in lAdy)
                    {
                       
                        foreach (Vertice v2 in listV)
                        {
                            if (!v.tieneAristaCon(v2) && v2.vCrom != 0)
                            {
                                vcrom = v2.vCrom;
                                v.vCrom = vcrom;
                                if(adyacentesColorDiferente(v))
                                   break;
                            }
                        }
                        
                        v.vCrom = vcrom;
                        
                        
                        //Si ese obtenido no adyacente es igual
                        if (!adyacentesColorDiferente(v))
                        {
                            foreach (Arista a2 in lAdy)
                            {
                                if (a2.v2.vCrom > vcrom)
                                    vcrom = a2.v2.vCrom;
                            }
                            v.vCrom = vcrom + 1;
                        }
                    }
                }


               
                //Buscamos el vertce con el vcrom mas alto
                foreach (Vertice v in listV)
                {
                    if (vcrom < v.vCrom)
                        vcrom = v.vCrom;
                }

                /// Coloreamos los vertices ///////////
                foreach (Vertice v in listV)
                {
                    color = 765 / (vcrom + 1);
                    v.setColor(color * v.vCrom);
                }
                System.Windows.Forms.MessageBox.Show("El valor Cromatico es: " + Convert.ToString(vcrom));
                foreach (Vertice v in listV)
                {
                    v.setColor(Color.GreenYellow);
                    v.vCrom = 0;
                }
            }
            else
                System.Windows.Forms.MessageBox.Show("Hay " + Convert.ToString(var) + " conjuntos que no se relacion");
        }
        public override void colorGrafo(Color color)
        {
            foreach (Vertice v in listV)
            {
                v.vCrom = 0;
                v.setColor(color);
            }
        }
        public override int numeroAristas()
        {
            int num = 0;
            foreach (Vertice v in listV)
            {
                num += v.numAristas();
            }
            return num/2;
        }
        public override void agregaArista(Arista arista)
        {
            Vertice v1 = arista.v1;  //Obtenemos el Vertice 1
            Vertice v2 = arista.v2;  //Y el vertice 2 de donde cuelga la arista

            v2.agregaArista(new Arista(v2, v1)); //Agragamos una arista en v2 (de v2 a v1)
            v1.agregaArista(arista); //Y otra arista en v1 (de v1 a v2)
            //numAristas++; //Incrementamos 2 veces
        }
        public override void eliminaArista(Point pos)
        {
            foreach (Vertice v in listV)
            {
                v.eliminaArista(pos);      
            }
        }

    }
}
