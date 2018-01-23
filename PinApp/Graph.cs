using System;
using System.Collections.Generic;
using System.Drawing;
using ZedGraph;

namespace PinApp
{
    class Graph
    {
        #region ATTRIBUTI

        public MainForm mainform;

        public List<double> modacclist0 = new List<double>();
        public List<double> modgyrlist0 = new List<double>();
        public List<double> thetalist0 = new List<double>();

        public List<double> modacclist1 = new List<double>();
        public List<double> modgyrlist1 = new List<double>();
        public List<double> thetalist1 = new List<double>();

        public List<double> modacclist2 = new List<double>();
        public List<double> modgyrlist2 = new List<double>();
        public List<double> thetalist2 = new List<double>();

        public List<double> modacclist3 = new List<double>();
        public List<double> modgyrlist3 = new List<double>();
        public List<double> thetalist3 = new List<double>();

        public List<double> modacclist4 = new List<double>();
        public List<double> modgyrlist4 = new List<double>();
        public List<double> thetalist4 = new List<double>();

        public int startIndexDR = 0;

        #endregion Attributi

        #region DELEGATI

        delegate void refreshInvokeDR(ZedGraphControl zgc, double[] spostamentoX, double[] spostamentoY);
        delegate void refreshInvoke();

        #endregion Delegati

        // COSTRUTTORE
        public Graph(MainForm mainform)
        {
            this.mainform = mainform;
        }

        #region Ricezione dei dati da graficare

        // METODO per la ricezione dei dati ottenuti
        public void Receive(double[] modacc, double[] modgyr, double[] theta, List<double[]> spostamento, int sensore)
        {
            switch (sensore)
            {
                case 0:
                    if (thetalist0.Count != 0)
                    {
                        double discontinuity = theta[0] - thetalist0[thetalist0.Count - 1];
                        if (Math.Abs(discontinuity) > 2)
                        {
                            for (int i = 0; i < theta.Length; i++)
                                theta[i] = theta[i] - discontinuity;
                        }

                    }

                    for (int i = 0; i < modacc.Length; i++)
                    {
                        this.modacclist0.Add(modacc[i]);
                        this.modgyrlist0.Add(modgyr[i]);
                        this.thetalist0.Add(theta[i]);
                    }
                    break;

                case 1:
                    if (thetalist1.Count != 0)
                    {
                        double discontinuity = theta[0] - thetalist1[thetalist1.Count - 1];
                        if (Math.Abs(discontinuity) > 2)
                        {
                            for (int i = 0; i < theta.Length; i++)
                                theta[i] = theta[i] - discontinuity;
                        }

                    }

                    for (int i = 0; i < modacc.Length; i++)
                    {
                        this.modacclist1.Add(modacc[i]);
                        this.modgyrlist1.Add(modgyr[i]);
                        this.thetalist1.Add(theta[i]);
                    }
                    break;

                case 2:
                    if (thetalist2.Count != 0)
                    {
                        double discontinuity = theta[0] - thetalist2[thetalist2.Count - 1];
                        if (Math.Abs(discontinuity) > 2)
                        {
                            for (int i = 0; i < theta.Length; i++)
                                theta[i] = theta[i] - discontinuity;
                        }

                    }

                    for (int i = 0; i < modacc.Length; i++)
                    {
                        this.modacclist2.Add(modacc[i]);
                        this.modgyrlist2.Add(modgyr[i]);
                        this.thetalist2.Add(theta[i]);
                    }
                    break;

                case 3:
                    if (thetalist3.Count != 0)
                    {
                        double discontinuity = theta[0] - thetalist3[thetalist3.Count - 1];
                        if (Math.Abs(discontinuity) > 2)
                        {
                            for (int i = 0; i < theta.Length; i++)
                                theta[i] = theta[i] - discontinuity;
                        }

                    }

                    for (int i = 0; i < modacc.Length; i++)
                    {
                        this.modacclist3.Add(modacc[i]);
                        this.modgyrlist3.Add(modgyr[i]);
                        this.thetalist3.Add(theta[i]);
                    }
                    break;

                case 4:
                    if (thetalist4.Count != 0)
                    {
                        double discontinuity = theta[0] - thetalist4[thetalist4.Count - 1];
                        if (Math.Abs(discontinuity) > 2)
                        {
                            for (int i = 0; i < theta.Length; i++)
                                theta[i] = theta[i] - discontinuity;
                        }

                    }

                    for (int i = 0; i < modacc.Length; i++)
                    {
                        this.modacclist4.Add(modacc[i]);
                        this.modgyrlist4.Add(modgyr[i]);
                        this.thetalist4.Add(theta[i]);
                    }
                    break;
            }

            switch (sensore)
            {
                case 0:
                    CreateGraphAccelerometro(mainform.zedGraphControl0_0, modacclist0);
                    CreateGraphGiroscopio(mainform.zedGraphControl1_0, modgyrlist0);
                    CreateGraphTheta(mainform.zedGraphControl2_0, thetalist0);
                    break;
                case 1:
                    CreateGraphAccelerometro(mainform.zedGraphControl0_1, modacclist1);
                    CreateGraphGiroscopio(mainform.zedGraphControl1_1, modgyrlist1);
                    CreateGraphTheta(mainform.zedGraphControl2_1, thetalist1);
                    break;
                case 2:
                    CreateGraphAccelerometro(mainform.zedGraphControl0_2, modacclist2);
                    CreateGraphGiroscopio(mainform.zedGraphControl1_2, modgyrlist2);
                    CreateGraphTheta(mainform.zedGraphControl2_2, thetalist2);
                    break;
                case 3:
                    CreateGraphAccelerometro(mainform.zedGraphControl0_3, modacclist3);
                    CreateGraphGiroscopio(mainform.zedGraphControl1_3, modgyrlist3);
                    CreateGraphTheta(mainform.zedGraphControl2_3, thetalist3);
                    break;
                case 4:
                    CreateGraphAccelerometro(mainform.zedGraphControl0_4, modacclist4);
                    CreateGraphGiroscopio(mainform.zedGraphControl1_4, modgyrlist4);
                    CreateGraphTheta(mainform.zedGraphControl2_4, thetalist4);
                    break;

            }

        }

        // METODO per i dati DR calcolati
        public void ReceiveDR(List<double[]> spostamento)
        {
            double[] spostamentoX = new double[spostamento.Count];
            double[] spostamentoY = new double[spostamento.Count];

            for (int i = 0; i < spostamento.Count; i++)
            {
                    spostamentoX[i] = (spostamento[i][0]);
                    spostamentoY[i] = (spostamento[i][1]);
            }

            CreateGraphDeadReckoning(mainform.zedGraphControlDeadReckoning, spostamentoX, spostamentoY);
        }

        #endregion Ricezione dei dati da graficare

        #region Disegno dei grafici

        // METODO per il disegno grafico modacc
        public void CreateGraphAccelerometro(ZedGraphControl zgc, List<double> modacc)
        {
            GraphPane myPane = zgc.GraphPane;

            myPane.CurveList.Clear();

            PointPairList list = new PointPairList();
            for (int i = 0; i < modacc.Count; i++)
                list.Add(i, modacc[i]);

            LineItem myCurve = myPane.AddCurve("", list, Color.FromArgb(31, 21, 163), SymbolType.None);

            zgc.AxisChange();
            if (zgc.InvokeRequired)
            {
                refreshInvoke d = new refreshInvoke(zgc.Refresh);
                zgc.Invoke(d);
            }
            else zgc.Refresh();
        }

        // METODO per il disegno grafico modgyr
        public void CreateGraphGiroscopio(ZedGraphControl zgc, List<double> modGyr)
        {
            GraphPane myPane = zgc.GraphPane;

            myPane.CurveList.Clear();

            PointPairList list = new PointPairList();
            for (int i = 0; i < modGyr.Count; i++)
                list.Add(i, modGyr[i]);

            LineItem myCurve = myPane.AddCurve("", list, Color.FromArgb(31, 21, 163), SymbolType.None);

            zgc.AxisChange();
            if (zgc.InvokeRequired)
            {
                refreshInvoke d = new refreshInvoke(zgc.Refresh);
                zgc.Invoke(d);
            }
            else zgc.Refresh();
        }

        // METODO per il disegno grafico Theta
        public void CreateGraphTheta(ZedGraphControl zgc, List<double> theta)
        {
            GraphPane myPane = zgc.GraphPane;

            myPane.CurveList.Clear();

            PointPairList list = new PointPairList();
            for (int i = 0; i < theta.Count; i++)
                list.Add(i, theta[i]);

            LineItem myCurve = myPane.AddCurve("", list, Color.FromArgb(31, 21, 163), SymbolType.None);

            zgc.AxisChange();
            if (zgc.InvokeRequired)
            {
                refreshInvoke d = new refreshInvoke(zgc.Refresh);
                zgc.Invoke(d);
            }
            else zgc.Refresh();
        }

        // METODO per il disegno grafico DR
        public void CreateGraphDeadReckoning(ZedGraphControl zgc, double[] spostamentoX, double[] spostamentoY)
        {
            if (zgc.InvokeRequired)
            {
                refreshInvokeDR d = new refreshInvokeDR(CreateGraphDeadReckoning);
                zgc.Invoke(d, zgc, spostamentoX, spostamentoY);
            }
            else
            {
                zgc.AxisChange();
                zgc.Invalidate();
                zgc.Refresh();

                GraphPane myPane = zgc.GraphPane;
                myPane.Title.Text = "Estimated 2D path";

                for (int i = 0; i < spostamentoX.Length; i++)
                {
                    myPane.CurveList[0].AddPoint(new PointPair(spostamentoX[i], spostamentoY[i]));
                    zgc.AxisChange();
                    zgc.Invalidate();
                    zgc.Refresh();
                }

                zgc.AxisChange();
                zgc.Invalidate();
                zgc.Refresh();
                startIndexDR = spostamentoX.Length;
            }
        }

        #endregion Disegno dei grafici

        // METODO per la pulizia post-connessione di grafici e strutture
        public void ClearGraph()
        {
            #region Reset strutture d'appoggio

            modacclist0 = new List<double>();
            modgyrlist0 = new List<double>();
            thetalist0 = new List<double>();

            modacclist1 = new List<double>();
            modgyrlist1 = new List<double>();
            thetalist1 = new List<double>();

            modacclist2 = new List<double>();
            modgyrlist2 = new List<double>();
            thetalist2 = new List<double>();

            modacclist3 = new List<double>();
            modgyrlist3 = new List<double>();
            thetalist3 = new List<double>();

            modacclist4 = new List<double>();
            modgyrlist4 = new List<double>();
            thetalist4 = new List<double>();

            #endregion Reset strutture d'appoggio

            #region Pulizia grafici DR

            mainform.zedGraphControlDeadReckoning.GraphPane.CurveList.Clear();
            if (mainform.zedGraphControlDeadReckoning.InvokeRequired)
            {
                refreshInvoke d = new refreshInvoke(mainform.zedGraphControlDeadReckoning.Refresh);
                mainform.zedGraphControlDeadReckoning.Invoke(d);
            }
            else mainform.zedGraphControlDeadReckoning.Refresh();

            #endregion Pulizia grafici DR

            #region Pulizia grafici sensore 0

            mainform.zedGraphControl0_0.GraphPane.CurveList.Clear();
            mainform.zedGraphControl1_0.GraphPane.CurveList.Clear();
            mainform.zedGraphControl2_0.GraphPane.CurveList.Clear();

            if (mainform.zedGraphControl0_0.InvokeRequired)
            {
                refreshInvoke d = new refreshInvoke(mainform.zedGraphControl0_0.Refresh);
                mainform.zedGraphControl0_0.Invoke(d);
            }
            else mainform.zedGraphControl1_0.Refresh();
            if (mainform.zedGraphControl1_0.InvokeRequired)
            {
                refreshInvoke d = new refreshInvoke(mainform.zedGraphControl1_0.Refresh);
                mainform.zedGraphControl1_0.Invoke(d);
            }
            else mainform.zedGraphControl0_0.Refresh();
            if (mainform.zedGraphControl2_0.InvokeRequired)
            {
                refreshInvoke d = new refreshInvoke(mainform.zedGraphControl2_0.Refresh);
                mainform.zedGraphControl2_0.Invoke(d);
            }
            else mainform.zedGraphControl2_0.Refresh();

            #endregion Pulizia grafici sensore 0

            #region Pulizia grafici sensore 1

            mainform.zedGraphControl0_1.GraphPane.CurveList.Clear();
            mainform.zedGraphControl1_1.GraphPane.CurveList.Clear();
            mainform.zedGraphControl2_1.GraphPane.CurveList.Clear();

            if (mainform.zedGraphControl0_1.InvokeRequired)
            {
                refreshInvoke d = new refreshInvoke(mainform.zedGraphControl0_1.Refresh);
                mainform.zedGraphControl0_1.Invoke(d);
            }
            else mainform.zedGraphControl1_1.Refresh();
            if (mainform.zedGraphControl1_1.InvokeRequired)
            {
                refreshInvoke d = new refreshInvoke(mainform.zedGraphControl1_1.Refresh);
                mainform.zedGraphControl1_1.Invoke(d);
            }
            else mainform.zedGraphControl0_1.Refresh();
            if (mainform.zedGraphControl2_1.InvokeRequired)
            {
                refreshInvoke d = new refreshInvoke(mainform.zedGraphControl2_1.Refresh);
                mainform.zedGraphControl2_1.Invoke(d);
            }
            else mainform.zedGraphControl2_1.Refresh();

            #endregion Pulizia grafici sensore 1

            #region Pulizia grafici sensore 2

            mainform.zedGraphControl0_2.GraphPane.CurveList.Clear();
            mainform.zedGraphControl1_2.GraphPane.CurveList.Clear();
            mainform.zedGraphControl2_2.GraphPane.CurveList.Clear();

            if (mainform.zedGraphControl0_2.InvokeRequired)
            {
                refreshInvoke d = new refreshInvoke(mainform.zedGraphControl0_2.Refresh);
                mainform.zedGraphControl0_2.Invoke(d);
            }
            else mainform.zedGraphControl1_2.Refresh();
            if (mainform.zedGraphControl1_2.InvokeRequired)
            {
                refreshInvoke d = new refreshInvoke(mainform.zedGraphControl1_2.Refresh);
                mainform.zedGraphControl1_2.Invoke(d);
            }
            else mainform.zedGraphControl0_2.Refresh();
            if (mainform.zedGraphControl2_2.InvokeRequired)
            {
                refreshInvoke d = new refreshInvoke(mainform.zedGraphControl2_2.Refresh);
                mainform.zedGraphControl2_2.Invoke(d);
            }
            else mainform.zedGraphControl2_2.Refresh();

            #endregion Pulizia grafici sensore 2

            #region Pulizia grafici sensore 3

            mainform.zedGraphControl0_3.GraphPane.CurveList.Clear();
            mainform.zedGraphControl1_3.GraphPane.CurveList.Clear();
            mainform.zedGraphControl2_3.GraphPane.CurveList.Clear();

            if (mainform.zedGraphControl0_3.InvokeRequired)
            {
                refreshInvoke d = new refreshInvoke(mainform.zedGraphControl0_3.Refresh);
                mainform.zedGraphControl0_3.Invoke(d);
            }
            else mainform.zedGraphControl1_3.Refresh();
            if (mainform.zedGraphControl1_3.InvokeRequired)
            {
                refreshInvoke d = new refreshInvoke(mainform.zedGraphControl1_3.Refresh);
                mainform.zedGraphControl1_3.Invoke(d);
            }
            else mainform.zedGraphControl0_3.Refresh();
            if (mainform.zedGraphControl2_3.InvokeRequired)
            {
                refreshInvoke d = new refreshInvoke(mainform.zedGraphControl2_3.Refresh);
                mainform.zedGraphControl2_3.Invoke(d);
            }
            else mainform.zedGraphControl2_3.Refresh();

            #endregion Pulizia grafici sensore 3

            #region Pulizia grafici sensore 4

            mainform.zedGraphControl0_4.GraphPane.CurveList.Clear();
            mainform.zedGraphControl1_4.GraphPane.CurveList.Clear();
            mainform.zedGraphControl2_4.GraphPane.CurveList.Clear();

            if (mainform.zedGraphControl0_4.InvokeRequired)
            {
                refreshInvoke d = new refreshInvoke(mainform.zedGraphControl0_4.Refresh);
                mainform.zedGraphControl0_4.Invoke(d);
            }
            else mainform.zedGraphControl1_4.Refresh();
            if (mainform.zedGraphControl1_4.InvokeRequired)
            {
                refreshInvoke d = new refreshInvoke(mainform.zedGraphControl1_4.Refresh);
                mainform.zedGraphControl1_4.Invoke(d);
            }
            else mainform.zedGraphControl0_4.Refresh();
            if (mainform.zedGraphControl2_4.InvokeRequired)
            {
                refreshInvoke d = new refreshInvoke(mainform.zedGraphControl2_4.Refresh);
                mainform.zedGraphControl2_4.Invoke(d);
            }
            else mainform.zedGraphControl2_4.Refresh();

            #endregion Pulizia grafici sensore 4
        }
    }
}