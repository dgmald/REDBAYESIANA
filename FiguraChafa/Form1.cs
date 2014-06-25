using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace RunnerRedBayesiana
{
    public partial class Form1 : Form
    {
        List<Grafo> grafos = new List<Grafo>(); //Lista de Grafos
        List<Vertice> vertices = new List<Vertice>(); //Lista de Vertices temporal

        Grafo grafoActivo;  //referencia al grafo activo 
        Vertice rV1,rV2;    //referencias a vertices 
        int id;             //id del vertice
        Point p1, p2;       //Puntos del mouse
        private Bitmap osb; //Para poner una imagen de fondo y paginar
        private Graphics osdc; //Para dibujar cualquier grafico en pantalla
        PaintEventArgs ee;  
        bool band;          //bandera de validacion para pintar aristas
        bool bandView = false;
        int select = -1;    //selccion de opcion del menu
        Stream myStream;    //Para manipular archivo y serializarlo
        String strArch;     //Nombre y ruta del archivo
        bool existe = false;        //Para saber si existe un 
        Graphics g;
        String camino;
        int it = 0;
        const int tamV = 25; //tamaño del vertice
        Arista aristaActiva; //Referencia a Arista seleccionada
        Vertice verticeActivo; //Referencia a un vertice seleccionado
        Color colorFondo = Color.Gray; //Color Inicial de Fondo
        /********Euler**********/
        int inc = 0;
        Arista ar;
        Vertice vert1, vert2;
        List<int> lnum;
        /**********************/

        public Form1()
        {
            InitializeComponent();
            band = false;
            p1 = new Point();
            p2 = new Point();
            this.Width = 1024;
            this.Height = 700;
            osb = new Bitmap(1600, 1024);
            osdc = Graphics.FromImage(osb);
            g = CreateGraphics();

        }
      
        //Eventos del Mouse
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
           
            //Modo Vista todos los grafos
            //Si todos los grafos estan visibles
            //Obtenemos la referencia del grafo al que dimos MouseDown
            if (bandView)
            {
                foreach (Grafo gfo in grafos)
                {
                    if (gfo.intersect(new Point(e.X, e.Y)))
                    {
                        grafoActivo = gfo;
                        if (grafoActivo.esDirigido())
                        {
                            Arista.Enabled = true;
                            //toolStripArist2.Enabled = false;
                        }
                        else
                        {
                            //toolStripArist2.Enabled = true;
                            Arista.Enabled = false;
                        }
                    }
                }
            }
            //Si se oprime click izquierdo
            if (e.Button == MouseButtons.Left)
            {
                switch (select)
                {
                    case 0: //Caso Aristas No dirigidas
                        if (grafoActivo == null)
                        {
                            grafoActivo = new GrafoNoDirigido();
                            grafos.Add(grafoActivo);


                            grafoActivo.CopiaVertices(vertices);
                            //Pintamos el grafo
                            //grafoActivo.setColor(Color.DarkBlue);
                            //Eliminamos la lista de vertices despues de copiada
                            vertices.Clear();
                            Arista.Enabled = false;
                        }
                        if (grafoActivo != null)
                        {
                            rV1 = grafoActivo.dameVertice(new Point(e.X, e.Y));
                            if (rV1 != null)
                            {
                                p1.X = e.X;
                                p1.Y = e.Y;
                                band = true;
                            }
                        }
                        break;
                    case 3: //Caso Aristas Dirigidas
                        if (grafoActivo == null)
                        {
                            grafoActivo = new GrafoDirigido();
                            grafos.Add(grafoActivo);

                            grafoActivo.CopiaVertices(vertices);

                            //grafoActivo.setColor(Color.Cyan);
                            vertices.Clear();
                            //toolStripArist2.Enabled = false;
                        }
                        if (grafoActivo != null)
                        {
                            rV1 = grafoActivo.dameVertice(new Point(e.X, e.Y));
                            if (rV1 != null)
                            {
                                p1.X = e.X;
                                p1.Y = e.Y;
                                band = true;
                            }
                        }
                        break;
                    case 2:
                        Cursor = Cursors.SizeAll;
                        if (grafoActivo != null)
                            rV1 = grafoActivo.dameVertice(new Point(e.X, e.Y));

                        break;
                    case 4:
                        Cursor = Cursors.NoMove2D;
                        if (grafoActivo != null)
                            rV1 = grafoActivo.dameVertice(new Point(e.X, e.Y));
                        break;
                    case 5:
                        Cursor = Cursors.Help;
                        if (grafoActivo != null)
                            grafoActivo.eliminaArista(new Point(e.X, e.Y));
                        break;
                    case 6:   //Selector

                        break;
                }
            }
            
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (select)
                {
                    case 0: //Caso Aristas No Dirigidas
                        if (grafoActivo != null)
                        {
                            rV2 = grafoActivo.dameVertice(new Point(e.X, e.Y));
                            if (rV1 != null && rV2 != null)
                            {
                                grafoActivo.agregaArista(new Arista(rV1, rV2));
                                //Eliminamos referencias
                                //para que en siguientes operaciones no interfiera
                                rV1 = null;
                                rV2 = null;
                            }
                        }
                        break;
                    case 3: //Caso Aristas Dirigidas
                        if (grafoActivo != null)
                        {
                            rV2 = grafoActivo.dameVertice(new Point(e.X, e.Y));
                            if (rV1 != null && rV2 != null)
                            {
                                grafoActivo.agregaArista(new Arista(rV1, rV2));
                                rV1 = null;
                                rV2 = null;
                            }
                        }
                        break;
                    case 1: //Caso Cursor
                        if (grafoActivo != null)
                        {
                            grafoActivo.agregaVertice(new Vertice(new Point(e.X, e.Y), tamV, grafoActivo.numeroVertices()));
                        }
                        else
                        {
                            vertices.Add(new Vertice(new Point(e.X, e.Y), tamV, id));
                            id++; //checar
                        }
                        break;
                    case 2:                                     //Caso Circulo
                        //Si se suelta el mouse deja de moverse el nodo o vertice
                        rV1 = null;
                        break;
                    case 4:
                        rV1 = null;
                        break;
                    case 5:
                        if (grafoActivo != null)
                        {
                            rV1 = grafoActivo.dameVertice(new Point(e.X, e.Y));
                            if (rV1 != null)//Para checar q no se tome referencia donde no hay vertices
                            {
                                grafoActivo.eliminaVertice(rV1);
                            }
                            rV1 = null;
                            rV2 = null;
                        }
                        break;
                }
            }
            //Sacamos un contextMenu en caso de darle click a una arista
            if (e.Button == MouseButtons.Right)
            {
                if (grafoActivo != null)
                {
                    if (grafoActivo.hayArista(p2))
                    {
                        contextMenuStrip1.Show(e.X, e.Y);
                        aristaActiva = grafoActivo.dameArista(p2);
                    }
                    else
                        if (grafoActivo.hayVertice(p2))
                        {
                            contextMenuStrip2.Show(e.X, e.Y);
                            verticeActivo = grafoActivo.dameVertice(p2);
                        }           
                        else
                           contextMenuStrip3.Show(p2);
                }
               
            }
            //Restauramos el Cursor a su flecha originaria
            Cursor = Cursors.Arrow;
            band = false;
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            habilitaOpciones();
            switch (select)
            {
                case 0:                                    //Caso Arista 
                    break;
                case 1:                                    //Caso Cursor

                    break;
                case 2:                                     //Caso Circulo
                    //Mientras el vertice sea nullo
                    //lo movemos a libertad
                    if (rV1 != null)   
                    {                       
                        rV1.pos.X = e.X;
                        rV1.pos.Y = e.Y;
                    }
                    break;
                case 4:
                    if (rV1 != null)
                    {
                        //Se mueve mediante una posicion de referncia q va en rv1
                        //y en e van los desplazamientos
                        if(grafoActivo != null)
                            grafoActivo.muevete(rV1.pos.X,rV1.pos.Y,e.X,e.Y);
                    }
                    break;
            }
            p2.X = e.X;
            p2.Y = e.Y;
            this.Form1_Paint(sender, ee);
        }
        //Paint
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
           
            osdc.SmoothingMode = SmoothingMode.AntiAlias;
            osdc.Clear(colorFondo);
            ee = e;
            Pen pen = new Pen(Color.FromArgb(0, 0, 0), 1);
            switch (select)
            {
                case 0://Arista No dirigida
                    if (band)
                    {
                        osdc.DrawLine(pen, p1, p2);
                        Cursor = Cursors.Hand;
                    }
                    break;
                case 3: //Arista Dirigida
                    if (band)
                    {
                        AdjustableArrowCap aac = new AdjustableArrowCap(5, 5);
                        aac.Filled = true;
                        pen.CustomEndCap = aac;
                        osdc.DrawLine(pen, p1, p2);
                        Cursor = Cursors.Hand;
                        
                    }
                    break;
                case 1:
                    osdc.DrawEllipse(pen, p2.X - 20, p2.Y - 20, 40, 40);
                    break;
                case 2: //Mover Vertice
                    break;

                case 5://Mover Grafo
             
                    break;
                case 6: //Ver todo
                    
                    break;
                case 7:
                    if (grafoActivo != null)
                    {

                        /**if (lnum.Count - 1 > inc)
                        {
                            vert1 = grafoActivo.dameVertice(lnum[inc]);
                            vert2 = grafoActivo.dameVertice(lnum[inc + 1]);

                            ar = vert2.dameAristaCon(vert1);


                            if (grafoActivo.dibujaArista(g, ar, it) && vert1 != null && vert2 !=null)
                            {
                                inc++;
                                it = 0;
                                vert2.dameAristaCon(vert1).setColor(Color.Red);
                                ar.setColor(Color.Red);
                            }

                        }
                        else
                        {
                            grafoActivo.complemento();
                            grafoActivo.complemento();
                            
                        }**/
                        if (grafoActivo != null)
                            grafoActivo.dibujaCamino(g, camino, it);
                    }
                    break;
            }

            if (bandView == true)
            {
                foreach (Grafo grafo in grafos)
                {
                    grafo.dibujaGrafo(osdc, false);
                }
            }

            //Vertices que no corresponden a ningun grafo
            foreach (Vertice v in vertices)
            {
                //v.setColor(Color.Gray);
                v.dibujate(osdc, 0);
            }

            if (grafoActivo != null && grafoActivo.numeroVertices() != 0)
            {

                grafoActivo.dibujaGrafo(osdc,true);
                //Si hay grafos activos habilitamos
                DeleteElement.Enabled = true; //Mov vertice
                MoveGraph.Enabled = true; //Goma
                MoveElement.Enabled = true;    //Mov grafo
            }
            else
            {
                //Si no hay grafos activos deshabilitamos
                //toolStripArist2.Enabled = true;
                Arista.Enabled = true;

                DeleteElement.Enabled = false;
                MoveGraph.Enabled = false;
                MoveElement.Enabled = false;
                
                //y Eliminamos grafo
                grafos.Remove(grafoActivo);
                
                grafoActivo = null;
            }
            

            if (grafos.Count == 0)
                Clean.Enabled = false;
            else
                Clean.Enabled = true;

            g.DrawImage(osb, 0, 0);
        }
        //Serializacion
        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            BinaryFormatter formatter = new BinaryFormatter();

            saveFileDialog1.Filter = "All files (*.*)|*.*|Archivos de Red Bayesiana (*.rb)|*.rb";
            
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    formatter.Serialize(myStream, grafos);
                    strArch = saveFileDialog1.FileName;
                    myStream.Close();
                }
            }
        }
        private void guardarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Si myStrem es null significa q no se ha guardado
            //Y si existe es igual a false quiere decir que ya fue cargado
            if (myStream == null && existe == false)
            {
                guardarToolStripMenuItem_Click(sender, e);
            }
            if(myStream != null || existe == true)
            {
                myStream = new FileStream(strArch, FileMode.Create);

                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    formatter.Serialize(myStream, grafos);
                }
                catch (SerializationException se)
                {
                    MessageBox.Show("La Serialización Falló. Motivo: " + se.Message);
                    throw;
                }
                finally
                {
                    myStream.Close();

                }
            }
            
        }
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "All files (*.*)|*.*|Archivos de Red Bayesiana (*.rb)|*.rb";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            
            vertices.Clear();
            if (grafos.Count != 0)
            {
                if (MessageBox.Show("¿Desea guardar el grafo antes de salir?", "Confirmación",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    guardarToolStripMenuItem1_Click(sender, e);
                }
                grafos.Clear();
                grafoActivo = null;
            }

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            BinaryFormatter formatter = new BinaryFormatter();
                            // Deserializamos la List<PosArista> almacenada en el archivo y 
                            // la asignamos a la lista actual posAr
                            grafos = (List<Grafo>)formatter.Deserialize(myStream);
                            grafoActivo = grafos[0];

                            
                            existe = true;
                            bandView = true;
                            strArch = openFileDialog1.FileName;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void toolBoxVertice(object sender, EventArgs e)
        {
            Vertex.Checked = true;
            MoveGraph.Checked = false;
            DeleteElement.Checked = false;
            MoveElement.Checked = false;
            MoveGraph.Checked = false;
            Arista.Checked = false;
            select = 1;
               
        }
        private void toolBoxArista(object sender, EventArgs e)
        {
            
            Arista.Checked = true;
            Vertex.Checked = false;
            MoveGraph.Checked = false;
            DeleteElement.Checked = false;
            MoveElement.Checked = false;
            MoveGraph.Checked = false;            
            select = 3;
        }
        private void grafoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            grafoActivo = null;
            id = 0;
            Vertex.Checked = false;
            MoveGraph.Checked = false;
            DeleteElement.Checked = false;
            MoveElement.Checked = false;
            Arista.Checked = false;
            
        }
        private void toolBoxMoveElement(object sender, EventArgs e)
        {
            select = 2;//Mover Vertice
            MoveElement.Checked = true;
            Vertex.Checked = false;
            MoveGraph.Checked = false;
            DeleteElement.Checked = false;
            MoveGraph.Checked = false;
            Arista.Checked = false;
            //toolStripArist2.Checked = false;
        }
        private void toolBoxDeleteElement(object sender, EventArgs e)
        {
            select = 5; //Goma  
            DeleteElement.Checked = true;
            Vertex.Checked = false;
            MoveGraph.Checked = false;
            MoveElement.Checked = false;
            
            MoveGraph.Checked = false;
            Arista.Checked = false;
            //toolStripArist2.Checked = false;
        }
        private void toolBoxMoveGraph(object sender, EventArgs e)
        {
            select = 4; //Mover Grafo
            MoveGraph.Checked = true;
            Vertex.Checked = false;
            DeleteElement.Checked = false;
            MoveElement.Checked = false;
            Arista.Checked = false;
            //toolStripArist2.Checked = false;
        }
        private void todosLosGrafosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bandView = true;
            grafoActivoToolStripMenuItem.Enabled = true;
            todosLosGrafosToolStripMenuItem.Enabled = false;
        }
        private void grafoActivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bandView = false;
            grafoActivoToolStripMenuItem.Enabled = false;
            todosLosGrafosToolStripMenuItem.Enabled = true;
        }
        private void nuevoGrafoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bandView = false;
            grafoActivo = null;
            id = 0;         
            Arista.Enabled = true;
        }
        private void toolBoxClean(object sender, EventArgs e)
        {
            grafos.Remove(grafoActivo);
                
            grafoActivo = null;
            id = 0;

            Form1_Paint(sender, ee);
            
        }
        private void toolStrip1_MouseEnter(object sender, EventArgs e)
        {
            habilitaOpciones();
        }
        //Habilitaciones
        void habilitaOpciones()
        {
            if (grafos.Count != 0)
            {
                toolStripDropDownButton2.Enabled = true;                
                guardarToolStripMenuItem.Enabled = true;
                guardarToolStripMenuItem1.Enabled = true;
                nuevoGrafoToolStripMenuItem.Enabled = true;
                //toolStrip3.Visible = true;
                
                if (grafoActivo != null)
                {

                    if (grafoActivo.esDirigido())
                    {

                                              

                    }
                    else
                    {

                    }
                    if (grafos.Count == 1)
                    {
                        toolStripDropDownButton2.Enabled = false;
                    }
                }
                

                if (!bandView)
                {
                    grafoActivoToolStripMenuItem.Enabled = false;
                }
            }
            else//Si no hay grafo Activo
            {
                guardarToolStripMenuItem.Enabled = false;
                guardarToolStripMenuItem1.Enabled = false;
                nuevoGrafoToolStripMenuItem.Enabled = false;
                Clean.Enabled = false;
            }
        } 
    

        //Vertice
        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            verticeActivo.setColor(colorDialog1.Color);
        }
        private void tamañoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunnerRedBayesiana.Form2 forma2 = new RunnerRedBayesiana.Form2("Tamaño");
            forma2.ShowDialog();
            if (forma2.nVertices > 5)
            {
                verticeActivo.diam = forma2.nVertices;
            }

        }
        //Fondo de la Form y Color de Vertices del Grafo
        private void fondoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            colorFondo = colorDialog1.Color;
        }
        private void colorGrafoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (grafoActivo != null)
            {
                colorDialog1.ShowDialog();
                grafoActivo.setColor(colorDialog1.Color);
            }
        }
        private void colorAristasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (grafoActivo != null)
            {
                colorDialog1.ShowDialog();
                grafoActivo.setColorAristas(colorDialog1.Color);
            }
        }

        private void colorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            aristaActiva.setColor(colorDialog1.Color);

        }
        private void grosorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 forma2 = new Form2("Grosor");
            forma2.ShowDialog();
            if (forma2.nVertices > 2)
            {
                aristaActiva.setGrosor(forma2.nVertices);
            }

        }
        private void pesoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 forma2 = new Form2("Grosor");
            forma2.ShowDialog();
            if (forma2.nVertices > 0)
            {
                aristaActiva.setPeso(forma2.nVertices);
            }
        }
        private void toolStrip2_MouseEnter(object sender, EventArgs e)
        {
            habilitaOpciones();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}