using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;

namespace PinApp
{
    public partial class MainForm : Form
    {
        #region DELEGATI

        delegate void SetTextCallback(string text);
        delegate void updateGraphCallback(double time, double[][] data);
        delegate void updateDRGraphCallback(List<double[]> points);

        #endregion delegati

        // COSTRUTTORE
        public MainForm()
        {
            InitializeComponent();

            // Inizializzazione dei controlli ZedGraph
            setGraph(zedGraphControl0_0, zedGraphControl1_0, zedGraphControl2_0);
            setGraph(zedGraphControl0_1, zedGraphControl1_1, zedGraphControl2_1);
            setGraph(zedGraphControl0_2, zedGraphControl1_2, zedGraphControl2_2);
            setGraph(zedGraphControl0_3, zedGraphControl1_3, zedGraphControl2_3);
            setGraph(zedGraphControl0_4, zedGraphControl1_4, zedGraphControl2_4);
            setDRGraph(zedGraphControlDeadReckoning);
        }

        // METODO per la scrittura PROTETTA sulla console messaggi utente
        public void displayText(string text)
        {
            if (messageTextBox.InvokeRequired)
            {
                SetTextCallback act = new SetTextCallback(displayText);
                messageTextBox.Invoke(act, text);
            }
            else
            {
                messageTextBox.AppendText(text);
            }
        }

        // METODO per l'inizializzazione dei grafici modAcc, modGyr e Theta
        public void setGraph(ZedGraphControl zgc0, ZedGraphControl zgc1, ZedGraphControl zgc2)
        {
            // Creazione GraphPanes per modulo accelerazione, modulo giroscopio e angolo theta
            GraphPane modAcc = zgc0.GraphPane;
            GraphPane modGyr = zgc1.GraphPane;
            GraphPane theta = zgc2.GraphPane;

            setPane(modAcc, "Modulo Accelerazione", "Tempo", "Mod Accelerazione", 1, 0.1);
            setPane(modGyr, "Modulo Giroscopio", "Tempo", "Mod Giroscopio", 1, 0.1);
            setPane(theta, "Theta", "Tempo", "ArcTan(magny/magnz)", 1, 0.1);

            modAcc.XAxis.Title.IsVisible = true;
            modGyr.XAxis.Title.IsVisible = true;
            theta.XAxis.Title.IsVisible = true;
        }

        // METODO per l'inizializzazione del grafico DR
        public void setDRGraph(ZedGraphControl zgc2)
        {
            GraphPane DRPane = zgc2.GraphPane;

            DRPane.Chart.Fill.Color = System.Drawing.Color.LightSteelBlue; 

            DRPane.Title.Text = "Dead Reckoning";
            DRPane.Title.FontSpec.FontColor = Color.Black;
            DRPane.XAxis.Scale.FontSpec.FontColor = Color.Black;
            DRPane.YAxis.Scale.FontSpec.FontColor = Color.Black;
            DRPane.XAxis.MajorTic.Color = Color.Black;
            DRPane.XAxis.MinorTic.Color = Color.Black;
            DRPane.YAxis.MajorTic.Color = Color.Black;
            DRPane.YAxis.MinorTic.Color = Color.Black;
            DRPane.Chart.Border.Color = Color.Black;

            DRPane.XAxis.Title.IsVisible = false;
            DRPane.YAxis.Title.IsVisible = false;
            DRPane.Border.IsVisible = false;
            DRPane.YAxis.MajorGrid.IsZeroLine = false; // Rimuove la riga nera che segna lo zero

            LineItem Curve = DRPane.AddCurve("Percorso", null, Color.FromArgb(140, 0, 0), SymbolType.None);
            Curve.Line.IsSmooth = true;

            zgc2.AxisChange();
        }

        // METODO per la personalizzazione dei singoli grafici
        private void setPane(GraphPane pane, string titletext, string xtext, string ytext, double majorstep, double minorstep)
        {
            // Impostazione Colore
            pane.Chart.Fill.Color = System.Drawing.Color.LightSteelBlue;

            pane.XAxis.Scale.FontSpec.FontColor = Color.Black;
            pane.YAxis.Scale.FontSpec.FontColor = Color.Black;
            pane.YAxis.Title.FontSpec.FontColor = Color.Black;
            pane.XAxis.Title.FontSpec.FontColor = Color.Black;
            pane.Title.FontSpec.FontColor = Color.Black;
            pane.XAxis.MajorTic.Color = Color.Black;
            pane.XAxis.MinorTic.Color = Color.Black;
            pane.YAxis.MajorTic.Color = Color.Black;
            pane.YAxis.MinorTic.Color = Color.Black;
            pane.Chart.Border.Color = Color.Black;

            pane.Title.Text = titletext;
            pane.XAxis.Title.Text = xtext;
            pane.YAxis.Title.Text = ytext;
         

            LineItem curve = pane.AddCurve("", null, Color.FromArgb(0, 81, 255), SymbolType.None);
            curve.Line.IsSmooth = true;

            pane.XAxis.Title.IsVisible = false;
            pane.Legend.IsVisible = false;
            pane.Border.IsVisible = false;
            pane.YAxis.MajorGrid.IsZeroLine = false; // Rimuove la riga nera che segna lo zero
        }

        // METODO per la gestione della pressione del bottone "Chiudi"
        private void closeButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}