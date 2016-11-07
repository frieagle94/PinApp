using System;
using System.Collections.Generic;
using System.IO;

namespace PinApp
{
    public class Support
    {
        #region ATTRIBUTI

        //ATTRIBUTI DA ESTERNO
        private int numSensori;
        private MainForm mainForm;
        private static string FILEPATH = "C:/Users/Public/PinApp_";
        private static string FILEPATHSAVE;
        private static string FILEPATHLOG;
        private static StreamWriter sw;
        private static StreamWriter sw_Save;
        private string ID;
        public string[] statoPostura;

        //ATTRIBUTI USATI INTERNAMENTE
        private static int movementState = -1;
        private static bool turnControl = false;
        private static double turnCount;
        private static int posturaState = -1;

        private static double prevX = 0;
        private static double prevY = 0;
        private static double previousWindowTheta0 = 0;
        private static double previousWindowYaw0 = 0;
        private static int previousWindowDR = 1;

        private static double previousWindowTheta1 = 0;
        private static double previousWindowYaw1 = 0;

        private static double previousWindowTheta2 = 0;
        private static double previousWindowYaw2 = 0;

        private static double previousWindowTheta3 = 0;
        private static double previousWindowYaw3 = 0;

        private static double previousWindowTheta4 = 0;
        private static double previousWindowYaw4 = 0;

        public List<double[]> DRpoints { get; }

        #endregion ATTRIBUTI

        // COSTRUTTORE
        public Support(int numSensori, MainForm mainForm, string ID, string[] stato_pos)
        {
            statoPostura = stato_pos;
            this.numSensori = numSensori;
            this.mainForm = mainForm;
            this.ID = ID;
            FILEPATHLOG = FILEPATH + "log_" + ID + ".txt";
            FILEPATHSAVE = FILEPATH + "save_" + ID + ".csv";
            if (File.Exists(FILEPATHLOG))
                File.Delete(FILEPATHLOG);
            if (File.Exists(FILEPATHSAVE))
                File.Delete(FILEPATHSAVE);

            DRpoints = new List<double[]>();
            DRpoints.Add(new double[2] { 0, 0 });
            previousWindowDR = 1;
            prevX = 0;
            prevY = 0;
        }

        #region Salvataggio dei dati

        // METODO per il salvataggio su csv
        public void saveData(double[,,] toSaveData, double[,,] toSaveQuaternioni, string ID)
        {
            using (sw_Save = File.AppendText(FILEPATHSAVE))
            {
                int length = (toSaveData.GetLength(1) == 500) ? toSaveData.GetLength(1) / 2 : toSaveData.GetLength(1);

                //Scorro i campionamenti e salvo su csv
                for (int k = 0; k < length; k++)
                {
                    for (int j = 0; j < numSensori; j++)
                    {
                        for (int i = 0; i < toSaveData.GetLength(0); i++)
                        {
                            sw_Save.Write(toSaveData[i, k, j] + ";");
                        }
                        for (int i = 0; i < toSaveQuaternioni.GetLength(0); i++)
                        {
                            sw_Save.Write(toSaveQuaternioni[i, k, j] + ";");
                        }
                        sw_Save.Write(";");
                    }
                    sw_Save.Write("\r");
                }
            }
            mainForm.displayText("[" + ID + "] Il file PinApp_save_xSimulator.csv è stato aggiornato.\r\r");
            Console.Beep();
        }

        // METODO per il salvataggio degli eventi su file di log
        public void appendLog(string toAppend)
        {
            using (sw = File.AppendText(FILEPATHLOG))
            {
                bool swLock = false;
                while (!swLock)
                {
                    try
                    {
                        sw.Write(toAppend);
                        swLock = true;
                    }
                    catch (IOException)
                    {

                    }
                }
            }
        }

        // METODO per riordinare temporalmente il file di log
        public void sortLog()
        {
            //Prendo il file log e ne genero un array di stringhe contenente le varie righe
            string[] logs = File.ReadAllLines(FILEPATHLOG);

            //Mappo le linee associando un intero per poterle identificare
            Dictionary<int, string> log = new Dictionary<int, string>();

            //Riempio la mappa di stringhe
            for (int i = 0; i < logs.Length; i++)
                log.Add(i, logs[i]);

            //Creo un array di supporto necessario per manipolare la singola linea
            string[] support = new string[2];

            //Mappo le coppie (data parsata, stringa) associando un intero per poterle identificare 
            Dictionary<int, KeyValuePair<DateTime, string>> logTimes = new Dictionary<int, KeyValuePair<DateTime, string>>();

            //Per ogni linea nel file
            for (int i = 0; i < log.Count; i++)
            {
                //Parso la data-ora dalla singola linea di log
                support = log[i].Split('-');
                DateTime logTime = DateTime.Parse(support[0]);

                //Rimepio la mappa di date
                logTimes.Add(i, new KeyValuePair<DateTime, string>(logTime, support[1]));
            }

            // UTILIZZO SELECTION SORT PER RIORDINARE I FENOMENI IN ORDINE TEMPORALE
            int pos_min;
            for (int i = 0; i < logTimes.Count - 1; i++)
            {
                pos_min = i;
                for (int j = i + 1; j < logTimes.Count; j++)
                {
                    if (DateTime.Compare(logTimes[j].Key, logTimes[pos_min].Key) < 0)
                        pos_min = j;
                }

                if (pos_min != i)
                {
                    KeyValuePair<DateTime, string> temp = logTimes[i];
                    KeyValuePair<DateTime, string> temp2 = logTimes[pos_min];
                    logTimes.Remove(i);
                    logTimes.Remove(pos_min);
                    logTimes.Add(pos_min, temp);
                    logTimes.Add(i, temp2);
                }
            }

            File.Delete(FILEPATHLOG);

            for (int i = 0; i < logs.Length; i++)
                appendLog(logTimes[i].Key.ToString() + " -" + logTimes[i].Value + "\r");
        }

        #endregion Salvataggio dei dati

        #region Algoritmi di analisi

        // METODO per la conversione RADIANTI -> GRADI
        public static double radianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }

        // METODO per la conversione GRADI -> RADIANTI
        public static double degreeToRadian(double angle)
        {
            return angle * (Math.PI / 180.0);
        }

        // METODO per il calcolo dello smoothing
        public static double[] smoothing(double[] data, int range)
        {
            range = 10;
            int dataSize = data.Length;
            double[] smoothed_data = new double[dataSize];
            double totalSum = 0;

            for (int i = 0; i < dataSize; i++)
            {
                if (i < range)
                {
                    for (int j = 0; j < i + range; j++)
                    {
                        totalSum += data[j];
                    }
                    smoothed_data[i] = totalSum / (i + range + 1);
                }
                else if (i >= range && i <= dataSize - range)
                {
                    for (int j = i - range; j < i + range; j++)
                    {
                        totalSum += data[j];
                    }
                    smoothed_data[i] = totalSum / (2 * range + 1);
                }
                else if (i > dataSize - range)
                {
                    for (int j = i - range; j < dataSize; j++)
                    {
                        totalSum += data[j];
                    }
                    smoothed_data[i] = totalSum / (dataSize - (i - range));
                }
                totalSum = 0;
            }
            return smoothed_data;
        }

        // METODO per il calcolo della deviazione standard con media mobile
        public static double[] stdDeviation(double[] data, int range)
        {
            int dataSize = data.Length;
            double[] avg_interval = smoothing(data, range);
            double[] stdDev = new double[dataSize];
            double totalSum = 0;
            for (int i = 0; i < dataSize; i++)
            {
                if (i < range)
                {
                    for (int j = 0; j < i + range; j++)
                    {
                        totalSum += Math.Pow(data[j] - avg_interval[j], 2);
                    }
                    stdDev[i] = Math.Sqrt(totalSum / (i + range + 1));
                }
                else if (i >= range && i <= dataSize - range)
                {
                    for (int j = i - range; j < i + range; j++)
                    {
                        totalSum += Math.Pow(data[j] - avg_interval[j], 2);
                    }
                    stdDev[i] = Math.Sqrt(totalSum / (2 * range + 1));
                }
                else if (i > dataSize - range)
                {
                    for (int j = i - range; j < dataSize; j++)
                    {
                        totalSum += Math.Pow(data[j] - avg_interval[j], 2);
                    }
                    stdDev[i] = Math.Sqrt(totalSum / (dataSize - (i - range)));
                }
                totalSum = 0;
            }
            return stdDev;
        }

        // METODO per il calcolo del modulo
        public static double[] computeModule(double[,] toModule, bool sens)
        {
            double[] module = new double[toModule.GetLength(1)];
            if (sens)
            {
                for (int j = 0; j < toModule.GetLength(1); j++)
                    module[j] = Math.Sqrt(toModule[0, j] * toModule[0, j] + toModule[1, j] * toModule[1, j] + toModule[2, j] * toModule[2, j]);
            }
            else
            {
                for (int j = 0; j < toModule.GetLength(1); j++)
                    module[j] = Math.Sqrt(toModule[3, j] * toModule[3, j] + toModule[4, j] * toModule[4, j] + toModule[5, j] * toModule[5, j]);
            }
            return module;
        }

        // METODO per il calcolo dell'angolo di Eulero ROLL (φ)
        private static double roll(double q0, double q1, double q2, double q3)
        {
            double numerator = (2 * q2) * (q3) + (2 * q0) * (q1);
            double denominator = (2 * (q0 * q0)) + (2 * (q3 * q3)) - 1;
            return Math.Atan2(numerator, denominator);
        }

        // METODO per il calcolo dell'angolo di Eulero PITCH (θ)
        private static double pitch(double q0, double q1, double q2, double q3)
        {
            double exp = (2 * q1 * q3 - 2 * q0 * q2);
            return (-1) * Math.Asin(exp);
        }

        // METODO per il calcolo dell'angolo di Eulero YAW (ψ)
        private static double yaw(double q0, double q1, double q2, double q3)
        {
            double numerator = (2 * q1) * (q2) + (2 * q0) * (q3);
            double denominator = (2 * (q0 * q0)) + (2 * (q1 * q1)) - 1;
            return Math.Atan2(numerator, denominator);
        }

        // METODO per il calcolo degli angoli di Eulero
        public static double[,] computeEulero(double[,] quaternioni)
        {
            double[,] angoli = new double[500, 3];

            for (int i = 0; i < 500; i++)
            {
                double q0 = quaternioni[0, i];
                double q1 = quaternioni[1, i];
                double q2 = quaternioni[2, i];
                double q3 = quaternioni[3, i];

                angoli[i, 0] = roll(q0, q1, q2, q3);
                angoli[i, 1] = pitch(q0, q1, q2, q3);
                angoli[i, 2] = yaw(q0, q1, q2, q3);

                angoli[i, 0] = radianToDegree(angoli[i, 0]);
                angoli[i, 1] = radianToDegree(angoli[i, 1]);
                angoli[i, 2] = radianToDegree(angoli[i, 2]);
            }
            return angoli;
        }

        // METODO per il calcolo degli angoli YAW con correzione delle discontinuità
        public static double[] RPYEulerAngles(double[,] quaternioni, int sensore, int angolo)
        {
            double[] yawArray = new double[quaternioni.GetLength(1)];
            for (int i = 0; i < quaternioni.GetLength(1); i++)
            {
                double q0 = quaternioni[0, i];
                double q1 = quaternioni[1, i];
                double q2 = quaternioni[2, i];
                double q3 = quaternioni[3, i];
                switch (angolo)
                {
                    case 0:
                        yawArray[i] = radianToDegree(yaw(q0, q1, q2, q3));
                        break;
                    case 1:
                        yawArray[i] = radianToDegree(roll(q0, q1, q2, q3));
                        break;
                    case 2:
                        yawArray[i] = radianToDegree(pitch(q0, q1, q2, q3));
                        break;
                }

                switch (sensore)
                {
                    case 0:
                        if (i == 0 && previousWindowYaw0 != 0)
                        { //Riceviamo il primo valore della finestra attuale che va confrontato con l'ultimo valore della finestra precedente
                            yawArray[0] += fixAngleLeap(previousWindowYaw0, yawArray[0], 160);
                        }

                        else if (i != 0)
                        {
                            yawArray[i] += fixAngleLeap(yawArray[i - 1], yawArray[i], 160);
                        }
                        break;
                    case 1:
                        if (i == 0 && previousWindowYaw1 != 0)
                        { //Riceviamo il primo valore della finestra attuale che va confrontato con l'ultimo valore della finestra precedente
                            yawArray[0] += fixAngleLeap(previousWindowYaw1, yawArray[0], 160);
                        }

                        else if (i != 0)
                        {
                            yawArray[i] += fixAngleLeap(yawArray[i - 1], yawArray[i], 160);
                        }
                        break;
                    case 2:
                        if (i == 0 && previousWindowYaw2 != 0)
                        { //Riceviamo il primo valore della finestra attuale che va confrontato con l'ultimo valore della finestra precedente
                            yawArray[0] += fixAngleLeap(previousWindowYaw2, yawArray[0], 160);
                        }

                        else if (i != 0)
                        {
                            yawArray[i] += fixAngleLeap(yawArray[i - 1], yawArray[i], 160);
                        }
                        break;
                    case 3:
                        if (i == 0 && previousWindowYaw3 != 0)
                        { //Riceviamo il primo valore della finestra attuale che va confrontato con l'ultimo valore della finestra precedente
                            yawArray[0] += fixAngleLeap(previousWindowYaw3, yawArray[0], 160);
                        }

                        else if (i != 0)
                        {
                            yawArray[i] += fixAngleLeap(yawArray[i - 1], yawArray[i], 160);
                        }
                        break;
                    case 4:
                        if (i == 0 && previousWindowYaw4 != 0)
                        { //Riceviamo il primo valore della finestra attuale che va confrontato con l'ultimo valore della finestra precedente
                            yawArray[0] += fixAngleLeap(previousWindowYaw4, yawArray[0], 160);
                        }

                        else if (i != 0)
                        {
                            yawArray[i] += fixAngleLeap(yawArray[i - 1], yawArray[i], 160);
                        }
                        break;
                }

            }
            switch (sensore)
            {
                case 0:
                    previousWindowYaw0 = yawArray[(yawArray.Length / 2) - 1];
                    break;
                case 1:
                    previousWindowYaw1 = yawArray[(yawArray.Length / 2) - 1];
                    break;
                case 2:
                    previousWindowYaw2 = yawArray[(yawArray.Length / 2) - 1];
                    break;
                case 3:
                    previousWindowYaw3 = yawArray[(yawArray.Length / 2) - 1];
                    break;
                case 4:
                    previousWindowYaw4 = yawArray[(yawArray.Length / 2) - 1];
                    break;
            }

            return yawArray;
        }

        // METODO per la correzione delle discontinuità
        private static double fixAngleLeap(double prevAngles, double currAngles, int leap)
        {
            if (Math.Abs(prevAngles - currAngles) > leap)
            { // Si verifica un salto
                if (prevAngles > currAngles)
                { // Si verifica quando theta di i-1 è più in alto di theta di i
                    return 360;
                }
                else if (prevAngles < currAngles)
                { // Si verifica quando theta di i-1 è più in basso di theta di i 
                    return -360;
                }
            }
            // Non si verifica un salto
            return 0;
        }

        // METODO per il calcolo del rapporto incrementale
        public static double[] RIfunc(double[] toRI)
        {
            double[] RI = new double[toRI.Length];
            for (int i = 0; i < toRI.Length - 1; i++)
            {
                RI[i] = toRI[i + 1] - toRI[i];
            }
            RI[toRI.Length - 1] = toRI[toRI.Length - 1];
            return RI;
        }

        #endregion Algoritmi di analisi

        #region Riconoscimento dei fenomeni

        // METODO per l'analisi moto-stazionamento
        public void analisiMovimento(double[] modacc, DateTime startTime)
        {
            double[] devStand = stdDeviation(modacc, 10);

            int startIndex = 0;

            if (movementState == -1)
                movementState = (devStand[0] >= 1) ? 1 : 0;

            int length = (modacc.Length == 500) ? modacc.Length / 2 : modacc.Length;
            for (int i = 1; i < length; i++)
            {
                if (devStand[i] >= 0.78 && movementState == 0)
                {
                    appendLog((startTime.AddSeconds(0.02 * startIndex)).ToString() + " - " + (startTime.AddSeconds(0.02 * i)).ToString() + " FERMO.\n");
                    startIndex = i;
                    movementState = 1;
                }

                if (devStand[i] < 0.78 && movementState == 1)
                {
                    appendLog((startTime.AddSeconds(0.02 * startIndex)).ToString() + " - " + (startTime.AddSeconds(0.02 * i)).ToString() + " IN MOVIMENTO.\n");
                    startIndex = i;
                    movementState = 0;
                }
            }
        }

        // METODO per il calcolo di Theta
        public double[] FunzioneOrientamento(double[,,] sampwin, int sensore)
        {
            double[,] toTheta = new double[3, sampwin.GetLength(1)];
            double[] theta = new double[toTheta.GetLength(1)];

            for (int i = 0; i < toTheta.GetLength(1); i++)
            {
                for (int k = 0; k < toTheta.GetLength(0); k++)
                {
                    toTheta[k, i] = sampwin[k + 6, i, sensore];
                }
                theta[i] = Math.Atan2(toTheta[1, i], toTheta[2, i]);
                theta[i] = radianToDegree(theta[i]);
                switch (sensore)
                {
                    case 0:
                        if (i == 0 && previousWindowTheta0 != 0)
                        { // Riceviamo il primo valore della finestra attuale che va confrontato con l'ultimo valore della finestra precedente

                            theta[0] += fixAngleLeap(previousWindowTheta0, theta[0], 160);
                        }
                        else if (i != 0)
                        {
                            theta[i] += fixAngleLeap(theta[i - 1], theta[i], 160);
                        }
                        break;
                    case 1:
                        if (i == 0 && previousWindowTheta0 != 0)
                        { // Riceviamo il primo valore della finestra attuale che va confrontato con l'ultimo valore della finestra precedente

                            theta[0] += fixAngleLeap(previousWindowTheta1, theta[0], 160);
                        }
                        else if (i != 0)
                        {
                            theta[i] += fixAngleLeap(theta[i - 1], theta[i], 160);
                        }
                        break;
                    case 2:
                        if (i == 0 && previousWindowTheta0 != 0)
                        { // Riceviamo il primo valore della finestra attuale che va confrontato con l'ultimo valore della finestra precedente

                            theta[0] += fixAngleLeap(previousWindowTheta2, theta[0], 160);
                        }
                        else if (i != 0)
                        {
                            theta[i] += fixAngleLeap(theta[i - 1], theta[i], 160);
                        }
                        break;
                    case 3:
                        if (i == 0 && previousWindowTheta0 != 0)
                        { // Riceviamo il primo valore della finestra attuale che va confrontato con l'ultimo valore della finestra precedente

                            theta[0] += fixAngleLeap(previousWindowTheta3, theta[0], 160);
                        }
                        else if (i != 0)
                        {
                            theta[i] += fixAngleLeap(theta[i - 1], theta[i], 160);
                        }
                        break;
                    case 4:
                        if (i == 0 && previousWindowTheta0 != 0)
                        { // Riceviamo il primo valore della finestra attuale che va confrontato con l'ultimo valore della finestra precedente

                            theta[0] += fixAngleLeap(previousWindowTheta4, theta[0], 160);
                        }
                        else if (i != 0)
                        {
                            theta[i] += fixAngleLeap(theta[i - 1], theta[i], 160);
                        }
                        break;
                }

            }
            return theta;
        }

        // METODO per l'analisi delle girate
        public void analisiTurn(double[] theta, int length, DateTime startTime)
        {
            theta = smoothing(theta, 10);

            turnCount = (turnControl) ? turnCount : theta[0];
            turnControl = true;

            int startIndex = 0;
            int i = 1;
            int j = 0;

            while (i < length)
            {
                j = i;
                while (i < length && theta[i] >= theta[i - 1])
                { // La funzione cresce
                    i++;
                }
                if (Math.Abs(theta[i - 1] - turnCount) > 30)
                {  // Se la differenza tra l'ultimo elemento che sta crescendo e il primo elemento della parte crescente è maggiore di 30 abbiamo una girata

                    appendLog(startTime.AddSeconds(0.02 * startIndex).ToString() + " - " + startTime.AddSeconds(0.02 * (i - 1)).ToString() + " GIRATA SX " + (theta[i - 1] - turnCount).ToString() + " gradi.\n");
                    startIndex = i - 1;
                    turnCount = theta[i - 1];
                }
                j = i;
                while (i < length && theta[i] < theta[i - 1])
                { // La funzione decresce
                    i++;
                }
                if (Math.Abs(theta[i - 1] - turnCount) > 30)
                {// Se la differenza tra l'ultimo elemento che sta decrescendo e il primo elemento della parte decrescente è maggiore di 30 abbiamo una girata
                    appendLog(startTime.AddSeconds(0.02 * startIndex).ToString() + " - " + startTime.AddSeconds(0.02 * (i - 1)).ToString() + " GIRATA DX " + ((theta[i - 1] - turnCount) * -1).ToString() + " gradi.\n");
                    startIndex = i - 1;
                    turnCount = theta[i - 1];
                }
            }
        }

        // METODO per l'analisi Lay/Stand/Sit
        public void analisiPostura(double[] modacc, DateTime startTime)
        {
            modacc = smoothing(modacc, 10);
            int startIndex = 0;

            if (posturaState == -1)
            {
                posturaState = (modacc[0] <= 2.7) ? 0 : (modacc[0] > 2.7 && modacc[0] <= 3.7) ? 1 : (modacc[0] > 3.7 && modacc[0] < 7.2) ? 2 : 3;
                if (modacc[0] <= 2.7)
                    statoPostura[0] = "LAY";
                if (modacc[0] > 2.7 && modacc[0] <= 3.7)
                    statoPostura[0] = "LAY-SIT";
                if (modacc[0] > 3.7 && modacc[0] < 7.2)
                    statoPostura[0] = "SIT";
                if (modacc[0] >= 7.2)
                    statoPostura[0] = "STAND";
                statoPostura[1] = startTime.ToString();
                statoPostura[2] = modacc.Length.ToString();
            }
            else
                statoPostura[2] = (Int32.Parse(statoPostura[2]) + modacc.Length).ToString();
            int index = (modacc.Length == 500) ? modacc.Length / 2 : modacc.Length;

            for (int i = 1; i < modacc.Length; i++)
            {
                try
                {
                    if (posturaState == 0 && modacc[i] > 2.7)
                    {
                        appendLog(startTime.AddSeconds(0.02 * startIndex).ToString() + " - " + startTime.AddSeconds(0.02 * (i - 1)).ToString() + " LAY.\r");
                        posturaState = (modacc[i] <= 2.7) ? 0 : (modacc[i] > 2.7 && modacc[i] <= 3.7) ? 1 : (modacc[i] > 3.7 && modacc[i] < 7.2) ? 2 : 3;
                    }

                    if (posturaState == 1 && (modacc[i] <= 2.7 || modacc[i] > 3.7))
                    {
                        appendLog(startTime.AddSeconds(0.02 * startIndex).ToString() + " - " + startTime.AddSeconds(0.02 * (i - 1)).ToString() + " LAY-SIT.\r");
                        posturaState = (modacc[i] <= 2.7) ? 0 : (modacc[i] > 2.7 && modacc[i] <= 3.7) ? 1 : (modacc[i] > 3.7 && modacc[i] < 7.2) ? 2 : 3;
                    }

                    if (posturaState == 2 && (modacc[i] <= 3.7 || modacc[0] >= 7.2))
                    {
                        appendLog(startTime.AddSeconds(0.02 * startIndex).ToString() + " - " + startTime.AddSeconds(0.02 * (i - 1)).ToString() + " SIT.\r");
                        posturaState = (modacc[i] <= 2.7) ? 0 : (modacc[i] > 2.7 && modacc[i] <= 3.7) ? 1 : (modacc[i] > 3.7 && modacc[i] < 7.2) ? 2 : 3;
                    }

                    if (posturaState == 3 && (modacc[i] < 7.2))
                    {
                        appendLog(startTime.AddSeconds(0.02 * startIndex).ToString() + " - " + startTime.AddSeconds(0.02 * (i - 1)).ToString() + " STAND.\r");
                        posturaState = (modacc[i] <= 2.7) ? 0 : (modacc[i] > 2.7 && modacc[i] <= 3.7) ? 1 : (modacc[i] > 3.7 && modacc[i] < 7.2) ? 2 : 3;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    if (index != 250)
                    {
                        switch (posturaState)
                        {
                            case 0:
                                appendLog(startTime.ToString() + " - " + startTime.AddSeconds(0.02 * index).ToString() + " LAY.\r");
                                break;
                            case 1:
                                appendLog(startTime.ToString() + " - " + startTime.AddSeconds(0.02 * index).ToString() + " LAY-SIT.\r");
                                break;
                            case 2:
                                appendLog(startTime.ToString() + " - " + startTime.AddSeconds(0.02 * index).ToString() + " SIT.\r");
                                break;
                            case 3:
                                appendLog(startTime.ToString() + " - " + startTime.AddSeconds(0.02 * index).ToString() + " STAND.\r");
                                break;
                        }
                    }
                }
            }
        }

        // METODO per l'analisi Dead Reckoning (pag.12)
        public void analizePosition(double[,,] quaternioni, double[] modacc, int sensore)
        {
            double[,] toRPY = new double[quaternioni.GetLength(0), quaternioni.GetLength(1)];

            for (int i = 0; i < quaternioni.GetLength(0); i++)
                for (int j = 0; j < quaternioni.GetLength(1); j++)
                    toRPY[i, j] = quaternioni[i, j, sensore];

            double[] stdDev = smoothing(stdDeviation(modacc, 10), 10);
            double x = 0;
            double y = 0;

            // Analizzo DR solo per il primo sensore
            if (sensore == 0)
            {
                double[] yaw0 = smoothing(RPYEulerAngles(toRPY, sensore, 0), 10);
                for (int i = previousWindowDR; i < stdDev.Length; i++)
                {
                    if (stdDev[i] > 1)
                    {
                        // C'è movimento
                        x = stdDev[i] * Math.Cos(degreeToRadian(yaw0[i]));
                        y = stdDev[i] * Math.Sin(degreeToRadian(yaw0[i]));
                        prevX += y;   // Aggiorniamo le precedenti x aggiungendo la nuova "distanza" sull'asse x 
                        prevY += x;   //  Aggiorniamo le precedenti y aggiungendo la nuova "distanza" sull'asse y 

                        DRpoints.Add(new double[2] { prevX, prevY });
                    }
                }
            }

            // Analizzo solo la parte della finestra che non ho ancora computato
            if (stdDev.Length == 500)
                if (sensore == 0)
                    previousWindowDR = 250;
        }

        #endregion Riconoscimento dei fenomeni
    }
}