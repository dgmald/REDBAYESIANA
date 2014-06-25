namespace RunnerRedBayesiana
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.nuevoGrafoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.todosLosGrafosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grafoActivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolBox = new System.Windows.Forms.ToolStrip();
            this.Vertex = new System.Windows.Forms.ToolStripButton();
            this.Arista = new System.Windows.Forms.ToolStripButton();
            this.MoveElement = new System.Windows.Forms.ToolStripButton();
            this.DeleteElement = new System.Windows.Forms.ToolStripButton();
            this.MoveGraph = new System.Windows.Forms.ToolStripButton();
            this.Clean = new System.Windows.Forms.ToolStripButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.colorToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.grosorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pesoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.colorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tamañoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.fondoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorGrafoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorAristasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.toolStrip1.SuspendLayout();
            this.ToolBox.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.contextMenuStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripDropDownButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(739, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "Grafo";
            this.toolStrip1.MouseEnter += new System.EventHandler(this.toolStrip1_MouseEnter);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoGrafoToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.guardarToolStripMenuItem1,
            this.guardarToolStripMenuItem});
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(64, 22);
            this.toolStripDropDownButton1.Text = "Archivo";
            // 
            // nuevoGrafoToolStripMenuItem
            // 
            this.nuevoGrafoToolStripMenuItem.Name = "nuevoGrafoToolStripMenuItem";
            this.nuevoGrafoToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.nuevoGrafoToolStripMenuItem.Text = "Nuevo";
            this.nuevoGrafoToolStripMenuItem.Click += new System.EventHandler(this.nuevoGrafoToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.loadToolStripMenuItem.Text = "Abrir";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // guardarToolStripMenuItem1
            // 
            this.guardarToolStripMenuItem1.Name = "guardarToolStripMenuItem1";
            this.guardarToolStripMenuItem1.Size = new System.Drawing.Size(161, 22);
            this.guardarToolStripMenuItem1.Text = "Guardar";
            this.guardarToolStripMenuItem1.Click += new System.EventHandler(this.guardarToolStripMenuItem1_Click);
            // 
            // guardarToolStripMenuItem
            // 
            this.guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            this.guardarToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.guardarToolStripMenuItem.Text = "Guardar como";
            this.guardarToolStripMenuItem.Click += new System.EventHandler(this.guardarToolStripMenuItem_Click);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.todosLosGrafosToolStripMenuItem,
            this.grafoActivoToolStripMenuItem});
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(41, 22);
            this.toolStripDropDownButton2.Text = "Ver";
            // 
            // todosLosGrafosToolStripMenuItem
            // 
            this.todosLosGrafosToolStripMenuItem.Name = "todosLosGrafosToolStripMenuItem";
            this.todosLosGrafosToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.todosLosGrafosToolStripMenuItem.Text = "Todos los Grafos";
            this.todosLosGrafosToolStripMenuItem.Click += new System.EventHandler(this.todosLosGrafosToolStripMenuItem_Click);
            // 
            // grafoActivoToolStripMenuItem
            // 
            this.grafoActivoToolStripMenuItem.Name = "grafoActivoToolStripMenuItem";
            this.grafoActivoToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.grafoActivoToolStripMenuItem.Text = "Grafo Activo";
            this.grafoActivoToolStripMenuItem.Click += new System.EventHandler(this.grafoActivoToolStripMenuItem_Click);
            // 
            // ToolBox
            // 
            this.ToolBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.ToolBox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Vertex,
            this.Arista,
            this.MoveElement,
            this.DeleteElement,
            this.MoveGraph,
            this.Clean});
            this.ToolBox.Location = new System.Drawing.Point(0, 25);
            this.ToolBox.Name = "ToolBox";
            this.ToolBox.Padding = new System.Windows.Forms.Padding(0);
            this.ToolBox.Size = new System.Drawing.Size(34, 434);
            this.ToolBox.TabIndex = 1;
            this.ToolBox.Text = "toolStrip2";
            this.ToolBox.MouseEnter += new System.EventHandler(this.toolStrip2_MouseEnter);
            // 
            // Vertex
            // 
            this.Vertex.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Vertex.Image = ((System.Drawing.Image)(resources.GetObject("Vertex.Image")));
            this.Vertex.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Vertex.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Vertex.Margin = new System.Windows.Forms.Padding(0);
            this.Vertex.Name = "Vertex";
            this.Vertex.Size = new System.Drawing.Size(33, 34);
            this.Vertex.Text = "Inserta Vertices";
            this.Vertex.Click += new System.EventHandler(this.toolBoxVertice);
            // 
            // Arista
            // 
            this.Arista.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Arista.Image = ((System.Drawing.Image)(resources.GetObject("Arista.Image")));
            this.Arista.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Arista.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Arista.Name = "Arista";
            this.Arista.Size = new System.Drawing.Size(33, 34);
            this.Arista.Text = "Arista Dirigida";
            this.Arista.Click += new System.EventHandler(this.toolBoxArista);
            // 
            // MoveElement
            // 
            this.MoveElement.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MoveElement.Image = ((System.Drawing.Image)(resources.GetObject("MoveElement.Image")));
            this.MoveElement.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MoveElement.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MoveElement.Name = "MoveElement";
            this.MoveElement.Size = new System.Drawing.Size(33, 34);
            this.MoveElement.Text = "Mover Vertice";
            this.MoveElement.Click += new System.EventHandler(this.toolBoxMoveElement);
            // 
            // DeleteElement
            // 
            this.DeleteElement.Image = ((System.Drawing.Image)(resources.GetObject("DeleteElement.Image")));
            this.DeleteElement.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.DeleteElement.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleteElement.Name = "DeleteElement";
            this.DeleteElement.Size = new System.Drawing.Size(33, 34);
            this.DeleteElement.Click += new System.EventHandler(this.toolBoxDeleteElement);
            // 
            // MoveGraph
            // 
            this.MoveGraph.Image = ((System.Drawing.Image)(resources.GetObject("MoveGraph.Image")));
            this.MoveGraph.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MoveGraph.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MoveGraph.Name = "MoveGraph";
            this.MoveGraph.Size = new System.Drawing.Size(33, 34);
            this.MoveGraph.Click += new System.EventHandler(this.toolBoxMoveGraph);
            // 
            // Clean
            // 
            this.Clean.Image = ((System.Drawing.Image)(resources.GetObject("Clean.Image")));
            this.Clean.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Clean.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Clean.Name = "Clean";
            this.Clean.Size = new System.Drawing.Size(33, 34);
            this.Clean.Click += new System.EventHandler(this.toolBoxClean);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colorToolStripMenuItem1,
            this.grosorToolStripMenuItem,
            this.pesoToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(118, 70);
            // 
            // colorToolStripMenuItem1
            // 
            this.colorToolStripMenuItem1.Name = "colorToolStripMenuItem1";
            this.colorToolStripMenuItem1.Size = new System.Drawing.Size(117, 22);
            this.colorToolStripMenuItem1.Text = "Color";
            this.colorToolStripMenuItem1.Click += new System.EventHandler(this.colorToolStripMenuItem1_Click);
            // 
            // grosorToolStripMenuItem
            // 
            this.grosorToolStripMenuItem.Name = "grosorToolStripMenuItem";
            this.grosorToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.grosorToolStripMenuItem.Text = "Grosor";
            this.grosorToolStripMenuItem.Click += new System.EventHandler(this.grosorToolStripMenuItem_Click);
            // 
            // pesoToolStripMenuItem
            // 
            this.pesoToolStripMenuItem.Name = "pesoToolStripMenuItem";
            this.pesoToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.pesoToolStripMenuItem.Text = "Peso";
            this.pesoToolStripMenuItem.Click += new System.EventHandler(this.pesoToolStripMenuItem_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colorToolStripMenuItem,
            this.tamañoToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(124, 48);
            // 
            // colorToolStripMenuItem
            // 
            this.colorToolStripMenuItem.Name = "colorToolStripMenuItem";
            this.colorToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.colorToolStripMenuItem.Text = "Color";
            this.colorToolStripMenuItem.Click += new System.EventHandler(this.colorToolStripMenuItem_Click);
            // 
            // tamañoToolStripMenuItem
            // 
            this.tamañoToolStripMenuItem.Name = "tamañoToolStripMenuItem";
            this.tamañoToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.tamañoToolStripMenuItem.Text = "Tamaño";
            this.tamañoToolStripMenuItem.Click += new System.EventHandler(this.tamañoToolStripMenuItem_Click);
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fondoToolStripMenuItem,
            this.colorGrafoToolStripMenuItem,
            this.colorAristasToolStripMenuItem});
            this.contextMenuStrip3.Name = "contextMenuStrip3";
            this.contextMenuStrip3.Size = new System.Drawing.Size(159, 70);
            // 
            // fondoToolStripMenuItem
            // 
            this.fondoToolStripMenuItem.Name = "fondoToolStripMenuItem";
            this.fondoToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.fondoToolStripMenuItem.Text = "Color Fondo";
            this.fondoToolStripMenuItem.Click += new System.EventHandler(this.fondoToolStripMenuItem_Click);
            // 
            // colorGrafoToolStripMenuItem
            // 
            this.colorGrafoToolStripMenuItem.Name = "colorGrafoToolStripMenuItem";
            this.colorGrafoToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.colorGrafoToolStripMenuItem.Text = "Color Vertices";
            this.colorGrafoToolStripMenuItem.Click += new System.EventHandler(this.colorGrafoToolStripMenuItem_Click);
            // 
            // colorAristasToolStripMenuItem
            // 
            this.colorAristasToolStripMenuItem.Name = "colorAristasToolStripMenuItem";
            this.colorAristasToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.colorAristasToolStripMenuItem.Text = "Color Aristas";
            this.colorAristasToolStripMenuItem.Click += new System.EventHandler(this.colorAristasToolStripMenuItem_Click);
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(150, 150);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 459);
            this.Controls.Add(this.ToolBox);
            this.Controls.Add(this.toolStrip1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "Form1";
            this.Text = "Runner - Graph Generator";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ToolBox.ResumeLayout(false);
            this.ToolBox.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.contextMenuStrip3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nuevoGrafoToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton Arista;
        private System.Windows.Forms.ToolStripButton MoveElement;
        private System.Windows.Forms.ToolStripButton DeleteElement;
        private System.Windows.Forms.ToolStripButton MoveGraph;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem todosLosGrafosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem grafoActivoToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem1;
        private System.Windows.Forms.ToolStripButton Clean;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem colorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tamañoToolStripMenuItem;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.ToolStripMenuItem fondoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem grosorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pesoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorGrafoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorAristasToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton Vertex;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        public System.Windows.Forms.ToolStrip ToolBox;



    }
}

