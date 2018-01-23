using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace PinApp
{
    public class Server
    {
        #region ATTRIBUTI

        private MainForm mainForm;
        private Graph graph;
        private DateTime startTime;
        int timeToAdd = 500;
        public string[] statoPostura = new string[3];

        #endregion ATTRIBUTI

        #region DELEGATI

        // Delegato per avvio analisi
        delegate void analysis(double[,,] sampwin, double[,,] quaternioni, int numSensori, string ID, Graph graph, Support help, DateTime startTime);

        // Delegati per analisi dati e fenomeni
        delegate void saveData(double[,,] sampwin, double[,,] quaternioni, string ID);
        delegate double[] module(double[,] toModule, bool sens);
        delegate double[] analizeTurn(double[,,] sampwin, int numSens);
        delegate void analizePostura(double[] modacc, DateTime startTime);
        delegate void analizePosition(double[,,] quaternioni, double[] modacc, int numSens);
        delegate void movementToFile(double[] modacc, DateTime startTime);
        delegate void turnToFile(double[] theta, int lengthTurn, DateTime startTime);

        // Delegati per invio dei dati ad oggetto Graph
        delegate void refreshGraph(double[] modacc, double[] modgyr, double[] theta, List<Double[]> points, int s);
        delegate void refreshGraphDR(List<Double[]> points);

        #endregion DELEGATI

        //COSTRUTTORE
        public Server(MainForm mainForm)
        {
            statoPostura[0] = "";
            this.mainForm = mainForm;
            graph = new Graph(mainForm);
        }

        // GESTIONE BASE SERVER
        public void Launch()
        {
            //Aspetto un secondo per permettere la sincronizzazione dei thread
            Thread.Sleep(1000);

            mainForm.displayText("Avviato!\r");

            //Sempre attivo!
            while (true)
            {
                //Avvio il server.
                TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 45555);
                listener.Start();

                //Rimango in attesa di connessione.
                Console.Beep();
                mainForm.displayText("In attesa di connessione...\r");

                //Accetto la connessione.
                Socket socket = listener.AcceptSocket();

                //Pulisco e reinizializzo i grafici per rappresentare la nuova connessione
                graph.ClearGraph();

                mainForm.setGraph(mainForm.zedGraphControl0_0, mainForm.zedGraphControl1_0, mainForm.zedGraphControl2_0);
                mainForm.setGraph(mainForm.zedGraphControl0_1, mainForm.zedGraphControl1_1, mainForm.zedGraphControl2_1);
                mainForm.setGraph(mainForm.zedGraphControl0_2, mainForm.zedGraphControl1_2, mainForm.zedGraphControl2_2);
                mainForm.setGraph(mainForm.zedGraphControl0_3, mainForm.zedGraphControl1_3, mainForm.zedGraphControl2_3);
                mainForm.setGraph(mainForm.zedGraphControl0_4, mainForm.zedGraphControl1_4, mainForm.zedGraphControl2_4);
                mainForm.setDRGraph(mainForm.zedGraphControlDeadReckoning);
                
                //Gestisco la connessione
                Execute(socket);

                //Chiudo la connessione e resetto oggetti e strutture di supporto
                listener.Stop();
                graph = new Graph(mainForm);
                timeToAdd = 500;
                statoPostura[0] = "";
                mainForm.displayText("Connessione terminata.\r");
                mainForm.displayText("__________________________________________________\r\r\r");
            }
        }

        // GESTIONE SINGOLA CONNESSIONE
        public void Execute(Socket socket)
        {
            try
            {
                //Imposto variabili necessarie allo scambio di informazioni
                Stream stream = new NetworkStream(socket);
                BinaryReader reader = new BinaryReader(stream);

                // RICEZIONE DEI 14 BYTE ALL'APERTURA DELLA CONNESSIONE (vedi seconda nota a pagina 3 del tema d'esame)

                // Leggo e stampo ID del dispositivo connesso (10 byte)
                char[] ID_arr = reader.ReadChars(10);
                string ID = new string(ID_arr);
                Console.Beep();
                mainForm.displayText("\rConnesso a:\r");
                mainForm.displayText(ID + "\r");

                // Leggo e stampo la frequenza emulata (4 byte)
                int frequenza = reader.ReadInt32();
                mainForm.displayText("[" + ID + "] Il dispositivo emula la frequenza di: " + frequenza + " Hz.\r");
                mainForm.displayText("[" + ID + "] In attesa di ricezione dati...");

                #region Lettura primo pacchetto
                // RICEZIONE DEL PRIMO PACCHETTO: determino il numero dei sensori e quindi la struttura dei paccetti successivi

                int pacchettiLetti = 0;         // numero pacchetti letti
                bool ricezione = false;         // controllo per messaggio utente
                byte[] bidMid = new byte[3];    // sequenza BID MID
                byte varLength = 0;             // variabile che conterrà il valore Length
                byte[] length = new byte[2];    // appoggio per calcolare i byte da leggere

                while (!(bidMid[0] == 0xFF && bidMid[1] == 0x32)) // cerca la sequenza FF-32 per contraddistinguere un pacchetto
                {
                    bidMid[0] = bidMid[1];
                    bidMid[1] = varLength;
                    varLength = reader.ReadByte();

                    // controllo utente per la prima ricezione dati
                    if (!ricezione)
                    {
                        Console.Beep();
                        mainForm.displayText("\r[" + ID + "] Ricevo i dati...\r\n");
                        startTime = DateTime.Now;
                        ricezione = true;
                    }
                }

                int byteToRead = 0; // conterrà i byte da leggere

                if (varLength != 0xFF) // modalità normale
                    byteToRead = varLength;

                else  // modalità extended-length
                {
                    length = reader.ReadBytes(2); // leggo i byte di dati
                    byteToRead = (length[0] * 256) + length[1];
                }

                byte[] data = reader.ReadBytes(byteToRead + 1); // struttura per i dati (compreso checksum) del 1° pacchetto
                byte[] pacchetto;                               // struttura per i pacchetti, la inizializzo qui sotto

                if (varLength != 0xFF) // modalità normale
                    pacchetto = new byte[byteToRead + 4]; // dove 4 sono BID, MID, LENGTH (NORMALE) e CHECKSUM

                else  // modalità extended-length
                    pacchetto = new byte[byteToRead + 6]; // dove 6 sono BID, MID, EXT-LENGTH, LENGTH[0], LENGTH[1] e CHECKSUM

                int maxSensori = 10; // numero massimo di sensori consentiti da Xbus Master
                int numSensori = (byteToRead - 2) / 52; // calcolo del numero di sensori della connessione (-2 sono i contatori!)

                // ricostruisco il primo pacchetto utilizzando la struttura precedente
                pacchetto[0] = 0xFF;
                pacchetto[1] = 0x32;
                pacchetto[2] = varLength; // contiene OxFF se i sensori sono 5, oppure la lunghezza dei dati

                if (varLength != 0xFF) // modalità normale
                    data.CopyTo(pacchetto, 3); // copia dei dati preventivamente salvati nella struttura del pacchetto

                else // modalità extended-length
                {
                    pacchetto[3] = length[0];
                    pacchetto[4] = length[1];
                    data.CopyTo(pacchetto, 5); // copia dei dati preventivamente salvati nella struttura del pacchetto
                }

                #endregion Lettura primo pacchetto

                // Inizializzazione strutture per il salvataggio dei dati

                double[,,] sampwin = new double[9, 500, numSensori]; // vettore tridimensionale per il salvataggio dei dati
                double[,,] quaternioni = new double[4, 500, numSensori]; // vettore tridimensionale per il salvataggio dei quaternioni
                Support help = new Support(numSensori, mainForm, ID, statoPostura);

                int indiceFinestra = 0; // indice del campionamento in cui scrivo
                int[] indiceLettura = new int[maxSensori]; // contiene per ogni sensore il relativo indice di lettura

                #region Lettura pacchetti successivi

                for (int i = 0; i < numSensori; i++)
                    indiceLettura[i] = 5 + (52 * i); // setto gli indici per ogni sensore (5 per bidMid varLength e 2 contatori)

                // finchè il pacchetto è "nullo"
                while (pacchetto.Length != 0)
                {
                    for (int i = 0; i < numSensori; i++)
                    {
                        byte[] valore = new byte[4];    // 4 byte per un valore
                        float valoreConvertito = 0;     // conterrà il risultato dalla conversione

                        for (int campi = 0; campi < 13; campi++) // 13 campi, 3(x,y,z) * 3(accel, girosc, magnet) + 4 (quater)
                        {
                            if (numSensori < 5) // modalità normale
                            {
                                valore[0] = pacchetto[indiceLettura[i] + 3]; // lettura inversa per complemento a 2
                                valore[1] = pacchetto[indiceLettura[i] + 2];
                                valore[2] = pacchetto[indiceLettura[i] + 1];
                                valore[3] = pacchetto[indiceLettura[i]];
                            }

                            else // modalità extended-length
                            {
                                valore[0] = pacchetto[indiceLettura[i] + 5]; // lettura inversa per complemento a 2 (porto avanti
                                valore[1] = pacchetto[indiceLettura[i] + 4]; // di due l'indice per scartare i due byte di length)
                                valore[2] = pacchetto[indiceLettura[i] + 3];
                                valore[3] = pacchetto[indiceLettura[i] + 2];
                            }

                            valoreConvertito = BitConverter.ToSingle(valore, 0); // conversione dei 4 byte in numero reale

                            if (campi < 9) //mantengo solo i primi 4 valori relativi a accelerometro, giroscopio e magnetometro
                                sampwin[campi, indiceFinestra, i] = valoreConvertito;
                            else
                                quaternioni[campi - 9, indiceFinestra, i] = valoreConvertito;

                            indiceLettura[i] += 4; // sposto avanti l'indice per leggere il successivo numero reale
                        }
                    }

                    for (int i = 0; i < numSensori; i++)
                        indiceLettura[i] = 5 + (52 * i); // ripristino gli indici

                    pacchettiLetti++;
                    indiceFinestra++;   // scrivo nel campionamento successivo

                    #endregion Lettura pacchetti successivi

                    #region Operazioni post raccoglimento finestra

                    if (indiceFinestra == 500) // ho raccolto la finestra dei dati
                    {
                        mainForm.displayText("[" + ID + "] Finestra di dati raccolta, analisi avviata correttamente.\r");
                        analysis analisi = new analysis(Analize);
                        analisi.Invoke(sampwin, quaternioni, numSensori, ID, graph, help, startTime);

                        // i primi 250 campionamenti della prossima finestra sono gli ultimi 250 di quella corrente (vedi tema esame)
                        for (int i = 250; i < 500; i++)
                        {
                            for (int j = 0; j < 5; j++)
                            {
                                for (int k = 0; k < 9; k++)
                                    sampwin[k, i - 250, j] = sampwin[k, i, j];
                            }
                        }

                        startTime = startTime.AddSeconds(0.02 * timeToAdd);
                        indiceFinestra = 250; // setto l'indice del prossimo campionamento
                        timeToAdd = 250;
                    }

                    // LETTURA DEL PACCHETTO SEGUENTE
                    if (numSensori < 5)  // modalità normale
                        pacchetto = reader.ReadBytes(byteToRead + 4);
                    else // modalità extended-length
                        pacchetto = reader.ReadBytes(byteToRead + 6);
                }

                if (indiceFinestra != 500)
                {
                    analysis analisi = new analysis(Analize);
                    double[,,] temp = new double[sampwin.GetLength(0), indiceFinestra, numSensori];

                    for (int i = 0; i < sampwin.GetLength(0); i++)
                    {
                        for (int j = 0; j < indiceFinestra; j++)
                        {
                            for (int k = 0; k < numSensori; k++)
                                temp[i, j, k] = sampwin[i, j, k];
                        }
                    }
                    timeToAdd = sampwin.GetLength(0) - 250;
                    startTime = startTime.AddSeconds(0.02 * sampwin.GetLength(0));
                    mainForm.displayText("[" + ID + "] Finestra di dati raccolta, analisi avviata correttamente.\r");
                    analisi.Invoke(temp, quaternioni, numSensori, ID, graph, help, startTime); //Invoco l'analisi della finestra appena raccolta
                }

                #endregion Operazioni post raccoglimento finestra

                #region Operazioni al termine della connessione

                refreshGraphDR refreshGraphDR = new refreshGraphDR(graph.ReceiveDR);
                refreshGraphDR.Invoke(help.DRpoints);

                if (statoPostura[0] != "")
                    help.appendLog(statoPostura[1] + " - " + startTime.AddSeconds(0.02 * Int32.Parse(statoPostura[2])).ToString() + " " + statoPostura[0] + "\n");

                help.sortLog();
                mainForm.displayText("[" + ID + "] Ricezione terminata: ho ricevuto " + pacchettiLetti + " pacchetti di dati.\r");
                Console.Beep();
            }
            catch (EndOfStreamException)
            {
                mainForm.displayText("\r\rLa connessione è stata interrotta in modo anomalo. Riprova.\r\r");
            }

            #endregion Operazioni al termine della connessione
        }

        // GESTIONE ANALISI DELLA SINGOLA FINESTRA
        private void Analize(double[,,] sampwin, double[,,] quaternioni, int numSensori, string ID, Graph graph, Support help, DateTime startTime)
        {
            // Salvataggio campionamento su CSV
            saveData save = new saveData(help.saveData);
            save.Invoke(sampwin, quaternioni, ID);

            #region Analisi da rappresentare

            for (int s = 0; s < numSensori; s++)
            {
                // Calcolo di Theta: Start
                analizeTurn analisiGirata = new analizeTurn(help.FunzioneOrientamento);
                IAsyncResult risultatoGirata = analisiGirata.BeginInvoke(sampwin, s, null, null);

                // Calcolo modulo di accelerometro e giroscopio: Start & End 
                double[,] toModule = new double[6, sampwin.GetLength(1)];
                for (int k = 0; k < toModule.GetLength(1); k++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        toModule[j, k] = sampwin[j, k, s];
                    }
                }
                module moduleGyrDelegate = new module(Support.computeModule);
                module moduleAccDelegate = new module(Support.computeModule);
                IAsyncResult risultatoGyr = moduleGyrDelegate.BeginInvoke(toModule, false, null, null);
                IAsyncResult risultatoAcc = moduleAccDelegate.BeginInvoke(toModule, true, null, null);

                double[] modgyr = Support.smoothing(moduleGyrDelegate.EndInvoke(risultatoGyr), 10);
                double[] modacc = moduleAccDelegate.EndInvoke(risultatoAcc);

                // Dead Reckoning: Start (non ritorna nulla)
                analizePosition analisiPosizione = new analizePosition(help.analizePosition);
                IAsyncResult risultatoPosizione = analisiPosizione.BeginInvoke(quaternioni, modacc, s, null, null);

                modacc = Support.smoothing(modacc, 10);

                // Calcolo di theta: End
                double[] theta = analisiGirata.EndInvoke(risultatoGirata);

                if (s == 0)
                {
                    // Analisi moto-stazionamento su file: Start (non ritorna nulla)
                    movementToFile movementToFile = new movementToFile(help.analisiMovimento);
                    movementToFile.Invoke(modacc, startTime);

                    // Analisi Lay/Stand/Sit su file: Start (non ritorna nulla)
                    analizePostura analisiPostura = new analizePostura(help.analisiPostura);
                    analisiPostura.Invoke(modacc, startTime);

                    // Analisi girate su file: Start (non ritorna nulla)
                    int lengthTurn = (sampwin.GetLength(1) == 500) ? sampwin.GetLength(1) / 2 : sampwin.GetLength(1);
                    turnToFile turnToFile = new turnToFile(help.analisiTurn);
                    turnToFile.Invoke(theta, lengthTurn, startTime);
                }

                analisiPosizione.EndInvoke(risultatoPosizione);

                // Refresh grafici dopo l'analisi
                switch (s)
                {
                    case 0:
                        refreshGraph refreshGraph0 = new refreshGraph(graph.Receive);
                        refreshGraph0.Invoke(modacc, modgyr, theta, help.DRpoints, s);
                        break;
                    case 1:
                        refreshGraph refreshGraph1 = new refreshGraph(graph.Receive);
                        refreshGraph1.Invoke(modacc, modgyr, theta, help.DRpoints, s);
                        break;
                    case 2:
                        refreshGraph refreshGraph2 = new refreshGraph(graph.Receive);
                        refreshGraph2.Invoke(modacc, modgyr, theta, help.DRpoints, s);
                        break;
                    case 3:
                        refreshGraph refreshGraph3 = new refreshGraph(graph.Receive);
                        refreshGraph3.Invoke(modacc, modgyr, theta, help.DRpoints, s);
                        break;
                    case 4:
                        refreshGraph refreshGraph4 = new refreshGraph(graph.Receive);
                        refreshGraph4.Invoke(modacc, modgyr, theta, help.DRpoints, s);
                        break;
                }
            }

            #endregion Analisi da rappresentare
        }
    }
}