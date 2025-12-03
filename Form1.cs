using System;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;
using System.Drawing;
using System.Threading.Tasks;
using MN = MathNet.Numerics;
using FT = MathNet.Numerics.IntegralTransforms;
using NuclearLeakSim_WinForms.Models;

namespace NuclearLeakSim_WinForms
{
    public partial class Form1 : Form
    {
        private readonly int sampleRTD = 10;
        private readonly int sampleHumidity = 20;
        private readonly int samplePressure = 1000;
        private readonly int sampleAcoustic = 30000;
        private readonly int sampleGeiger = 11000;

        public Form1()
        {
            InitializeComponent();
            InitializeSliderRanges();

            try { SystemDiagramBox.Image = Image.FromFile("sps.jpg"); }
            catch { SystemDiagramBox.BackColor = Color.LightGray; }

            TrackBarK.ValueChanged += (s, e) => RefreshAllPlots();
            TrackBarTau.ValueChanged += (s, e) => RefreshAllPlots();
            TrackBarLambda.ValueChanged += (s, e) => RefreshAllPlots();
            TrackBarTauE.ValueChanged += (s, e) => RefreshAllPlots();
            TrackBarTauD.ValueChanged += (s, e) => RefreshAllPlots();
            TrackBarF0.ValueChanged += (s, e) => RefreshAllPlots();
            TrackBarQ.ValueChanged += (s, e) => RefreshAllPlots();

            RefreshAllPlots();
        }

        private void InitializeSliderRanges()
        {
            TrackBarK.Minimum = 1; TrackBarK.Maximum = 50; TrackBarK.Value = 10;
            TrackBarTau.Minimum = 1; TrackBarTau.Maximum = 5000; TrackBarTau.Value = 500;
            TrackBarLambda.Minimum = 1; TrackBarLambda.Maximum = 100; TrackBarLambda.Value = 5;
            TrackBarTauE.Minimum = 1; TrackBarTauE.Maximum = 1000; TrackBarTauE.Value = 20;
            TrackBarTauD.Minimum = 1; TrackBarTauD.Maximum = 1000; TrackBarTauD.Value = 19;
            TrackBarF0.Minimum = 1000; TrackBarF0.Maximum = 4000; TrackBarF0.Value = 2000;
            TrackBarQ.Minimum = 10; TrackBarQ.Maximum = 100; TrackBarQ.Value = 20;
        }

        private (double k, double tau, double lambda, double tauE, double tauD, double f0, double q) GetSliderValues()
        {
            return (TrackBarK.Value / 10.0, TrackBarTau.Value / 1000.0, TrackBarLambda.Value,
                    TrackBarTauE.Value / 100000.0, TrackBarTauD.Value / 100000.0,
                    TrackBarF0.Value, TrackBarQ.Value / 10.0);
        }

        private void BtnDefaultSimulate_Click(object sender, EventArgs e)
        {
            TrackBarK.Value = 10; TrackBarTau.Value = 500; TrackBarLambda.Value = 5;
            TrackBarTauE.Value = 20; TrackBarTauD.Value = 19; TrackBarF0.Value = 2000; TrackBarQ.Value = 20;
            RefreshAllPlots();
            MessageBox.Show("Simulasi reset ke default.");
        }

        private async void BtnRunLeakScenario_Click(object sender, EventArgs e)
        {
            var (k, tau, lambda, tauE, tauD, f0, q) = GetSliderValues();

            Cursor = Cursors.WaitCursor;
            BtnRunLeakScenario.Enabled = false;

            try
            {
                var simResult = await Task.Run(() =>
                {
                    var scenario = new LeakScenario();
                    int fs = 30000;
                    double dt = 1.0 / fs;
                    int N = (int)(scenario.Duration * fs);
                    if (N <= 1) N = 4000;

                    double[] time = MN.Generate.LinearSpaced(N, 0, scenario.Duration);
                    var (Tenv, Penv, Henv, AcousticExc, RadLambda) = scenario.Generate(time);
                    double safeTau = Math.Max(tau, 1e-6);

                    double[] rtd_out = new double[N];
                    double[] rtd_temp = SimulationUtils.SimulateFirstOrder(Tenv, safeTau, dt, Tenv.FirstOrDefault());
                    var rtdModel = new RTDSensor();
                    for (int i = 0; i < N; i++) rtd_out[i] = k * ((rtdModel.R0 * (1.0 + rtdModel.Alpha * (rtd_temp[i] - 0.0)) - rtdModel.R0) / rtdModel.R0);

                    double[] pres_out = SimulationUtils.SimulateFirstOrder(Penv, safeTau, dt, Penv.FirstOrDefault()).Select(v => k * v).ToArray();
                    double[] hum_out = SimulationUtils.SimulateFirstOrder(Henv, safeTau, dt, Henv.FirstOrDefault()).Select(v => k * v).ToArray();
                    double[] ac_out = SimulationUtils.SimulateAcousticFromExcitation(AcousticExc, f0, Math.Max(q, 0.01), dt, k);
                    double[] ge_out = SimulationUtils.SimulateGeiger(RadLambda, Math.Max(tauE, 1e-9), tauD, dt, k);

                    return (rtd_out, pres_out, hum_out, ac_out, ge_out, fs);
                });

                DrawSensorPlotsFromData(simResult.Item1, simResult.fs, new RTDSensor(), Plot_RTD_Time, Plot_RTD_FFT);
                DrawSensorPlotsFromData(simResult.Item2, simResult.fs, new PressureSensor(), Plot_Pressure_Time, Plot_Pressure_FFT);
                DrawSensorPlotsFromData(simResult.Item3, simResult.fs, new HumiditySensor(), Plot_Hum_Time, Plot_Hum_FFT);
                DrawSensorPlotsFromData(simResult.Item4, simResult.fs, new AcousticSensor(), Plot_Acoustic_Time, Plot_Acoustic_FFT);
                DrawSensorPlotsFromData(simResult.Item5, simResult.fs, new GeigerSensor(), Plot_Geiger_Time, Plot_Geiger_FFT);

                MessageBox.Show("Simulasi Skenario selesai.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
                BtnRunLeakScenario.Enabled = true;
            }
        }

        private void RefreshAllPlots()
        {
            var (k, tau, lambda, tauE, tauD, f0, q) = GetSliderValues();

            // 1. UPDATE RUMUS DI PANEL KANAN (DETAIL)
            LblTotalSystemFormula.Text = "G_Total(s) = Σ G_sensor(s)";

            // Tampilkan rumus detail S-Domain dengan nilai aktual
            double pole1stOrder = -1.0 / Math.Max(tau, 0.001);
            LblSFormula.Text = $"Karakteristik S-Domain:\n" +
                               $"Pole (Orde-1): s = -1/τ = {pole1stOrder:F2} (Posisi pada sumbu Real)\n" +
                               $"Sistem stabil jika semua Pole berada di sebelah KIRI sumbu imajiner .";

            // Tampilkan rumus detail Z-Domain
            LblZFormula.Text = $"Karakteristik Z-Domain:\n" +
                               $"Mapping: z = e^(sT)\n" +
                               $"Sistem stabil jika semua Pole berada di DALAM lingkaran satuan (Garis Biru).";

            // 2. Plot Sensor
            DrawSensorPlots(new RTDSensor { K = k, Tau = tau, SampleRate = sampleRTD }, Plot_RTD_Time, Plot_RTD_FFT);
            DrawSensorPlots(new PressureSensor { K = k, Tau = tau, SampleRate = samplePressure }, Plot_Pressure_Time, Plot_Pressure_FFT);
            DrawSensorPlots(new HumiditySensor { K = k, Tau = tau, SampleRate = sampleHumidity }, Plot_Hum_Time, Plot_Hum_FFT);
            DrawSensorPlots(new AcousticSensor { K = k, F0 = f0, Q = q, SampleRate = sampleAcoustic }, Plot_Acoustic_Time, Plot_Acoustic_FFT);
            var ge = new GeigerSensor { K = k, SampleRate = sampleGeiger, Lambda = lambda, PulseTau = tauE, DeadTime = tauD };
            DrawSensorPlots(ge, Plot_Geiger_Time, Plot_Geiger_FFT);

            // 3. Analisis Sistem
            try
            {
                double wn = 2 * Math.PI * f0;
                double zeta = 1.0 / (2.0 * Math.Max(q, 0.01));

                var Gmods = SystemAnalyzer.BuildSensorModels(
                    new SensorModelDescriptor("RTD", k, tau, SensorType.FirstOrder),
                    new SensorModelDescriptor("Pressure", k, tau, SensorType.FirstOrder),
                    new SensorModelDescriptor("Humidity", k, tau, SensorType.FirstOrder),
                    new SensorModelDescriptor("Acoustic", k, 0, SensorType.SecondOrder, wn: wn, zeta: zeta),
                    new SensorModelDescriptor("Geiger", k, tauE, SensorType.Geiger, pulseTau: tauE, deadTime: tauD)
                );

                var (numTot, denTot) = SystemAnalyzer.CombineTransferFunctions(Gmods);
                var poles = SystemAnalyzer.FindPolynomialRoots(denTot);

                // --- Plot S-Domain ---
                Plot_PoleZero_S.Plot.Clear();
                Plot_PoleZero_S.Plot.Style(ScottPlot.Style.Default);
                Plot_PoleZero_S.Plot.Title("S-Domain Poles");
                Plot_PoleZero_S.Plot.XLabel("Real (σ)");
                Plot_PoleZero_S.Plot.YLabel("Imag (jω)");
                Plot_PoleZero_S.Plot.Grid(true);
                Plot_PoleZero_S.Plot.AddHorizontalSpan(0, 100000, color: Color.FromArgb(40, 255, 0, 0)); // Arsiran Kanan

                if (poles.Length > 0)
                {
                    Plot_PoleZero_S.Plot.AddScatter(
                        poles.Select(pt => pt.Real).ToArray(),
                        poles.Select(pt => pt.Imaginary).ToArray(),
                        color: Color.Red, lineWidth: 0, markerSize: 10,
                        markerShape: ScottPlot.MarkerShape.cross
                    );
                }
                Plot_PoleZero_S.Plot.AddVerticalLine(0, Color.Black, 2, ScottPlot.LineStyle.Dash);
                Plot_PoleZero_S.Plot.AxisAuto();
                Plot_PoleZero_S.Plot.SetAxisLimits(xMin: -5, xMax: 1, yMin: -5, yMax: 5); // Zoom Area Penting
                Plot_PoleZero_S.Render();

                // --- Plot Z-Domain ---
                double Ts = 1.0 / Math.Max(Math.Max(sampleAcoustic, samplePressure), sampleGeiger);
                var (numZ, denZ) = SystemAnalyzer.DiscretizeToZ(numTot, denTot, Ts);
                var polesZ = SystemAnalyzer.FindPolynomialRoots(denZ);

                Plot_PoleZero_Z.Plot.Clear();
                Plot_PoleZero_Z.Plot.Style(ScottPlot.Style.Default);
                Plot_PoleZero_Z.Plot.Title("Z-Domain Poles");
                Plot_PoleZero_Z.Plot.XLabel("Real");
                Plot_PoleZero_Z.Plot.YLabel("Imag");
                Plot_PoleZero_Z.Plot.Grid(true);

                int nPoints = 100;
                double[] ucX = new double[nPoints]; double[] ucY = new double[nPoints];
                for (int i = 0; i < nPoints; i++) { double a = 2 * Math.PI * i / (nPoints - 1); ucX[i] = Math.Cos(a); ucY[i] = Math.Sin(a); }
                Plot_PoleZero_Z.Plot.AddScatter(ucX, ucY, color: Color.Blue, lineStyle: ScottPlot.LineStyle.Dash, markerSize: 0);

                if (polesZ.Length > 0)
                {
                    Plot_PoleZero_Z.Plot.AddScatter(
                        polesZ.Select(pt => pt.Real).ToArray(),
                        polesZ.Select(pt => pt.Imaginary).ToArray(),
                        color: Color.Red, lineWidth: 0, markerSize: 10,
                        markerShape: ScottPlot.MarkerShape.cross
                    );
                }

                Plot_PoleZero_Z.Plot.AxisAuto();
                Plot_PoleZero_Z.Plot.SetAxisLimits(-1.5, 1.5, -1.5, 1.5);
                Plot_PoleZero_Z.Plot.AxisScaleLock(true, ScottPlot.EqualScaleMode.PreserveLargest);
                Plot_PoleZero_Z.Render();
            }
            catch (Exception ex)
            {
                Console.WriteLine("SysAnalyzer Error: " + ex.Message);
            }
        }

        private void DrawSensorPlotsFromData(double[] signal, double fs, SensorBase dummySensor, ScottPlot.FormsPlot plotTime, ScottPlot.FormsPlot plotFFT)
        {
            if (signal == null) signal = new double[] { 0 };
            plotTime.Plot.Clear();
            plotTime.Plot.Style(ScottPlot.Style.Default);
            plotTime.Plot.AddSignal(signal, sampleRate: fs);
            plotTime.Plot.Title(dummySensor.Name + " - Time (SCENARIO)");
            plotTime.Plot.AxisAuto();
            plotTime.Render();

            try
            {
                if (signal.Length < 2) return;
                int fftLength = signal.Length % 2 == 0 ? signal.Length : signal.Length - 1;
                var complex = signal.Take(fftLength).Select(x => new Complex(x, 0)).ToArray();
                FT.Fourier.Forward(complex, FT.FourierOptions.Matlab);
                double[] freq = Enumerable.Range(0, fftLength / 2).Select(i => (double)i * fs / fftLength).ToArray();
                double[] mag = complex.Take(fftLength / 2).Select(c => c.Magnitude).ToArray();

                plotFFT.Plot.Clear();
                plotFFT.Plot.Style(ScottPlot.Style.Default);
                plotFFT.Plot.AddScatter(freq, mag);
                plotFFT.Plot.Title(dummySensor.Name + " - FFT (SCENARIO)");
                plotFFT.Plot.SetAxisLimits(yMin: 0);
                plotFFT.Render();
            }
            catch { }
        }

        private void DrawSensorPlots(SensorBase sensor, ScottPlot.FormsPlot plotTime, ScottPlot.FormsPlot plotFFT)
        {
            double duration = (sensor is AcousticSensor) ? 1.0 : 5.0;
            if (sensor is PressureSensor) duration = 2.0;
            int N = (int)(duration * sensor.SampleRate); if (N <= 0) N = 100;

            double[] time = MN.Generate.LinearSpaced(N, 0, duration);
            double[] signal;
            try { signal = sensor.Generate(time); } catch { signal = new double[N]; }
            if (signal == null) signal = new double[N];

            for (int i = 0; i < signal.Length; i++) if (double.IsNaN(signal[i]) || double.IsInfinity(signal[i])) signal[i] = 0;

            if (plotTime != null)
            {
                plotTime.Plot.Clear();
                plotTime.Plot.Style(ScottPlot.Style.Default);
                plotTime.Plot.AddSignal(signal, sampleRate: sensor.SampleRate);
                plotTime.Plot.Title(sensor.Name + " - Time");
                plotTime.Plot.AxisAuto();
                plotTime.Render();
            }

            try
            {
                if (signal.Length < 2) return;
                int fftLength = signal.Length % 2 == 0 ? signal.Length : signal.Length - 1;
                var complex = signal.Take(fftLength).Select(x => new Complex(x, 0)).ToArray();
                FT.Fourier.Forward(complex, FT.FourierOptions.Matlab);
                double[] freq = Enumerable.Range(0, fftLength / 2).Select(i => (double)i * sensor.SampleRate / fftLength).ToArray();
                double[] mag = complex.Take(fftLength / 2).Select(c => c.Magnitude).ToArray();

                if (plotFFT != null)
                {
                    plotFFT.Plot.Clear();
                    plotFFT.Plot.Style(ScottPlot.Style.Default);
                    plotFFT.Plot.AddScatter(freq, mag);
                    plotFFT.Plot.Title(sensor.Name + " - FFT");
                    plotFFT.Plot.SetAxisLimits(yMin: 0);
                    plotFFT.Render();
                }
            }
            catch { }
        }
    }
}