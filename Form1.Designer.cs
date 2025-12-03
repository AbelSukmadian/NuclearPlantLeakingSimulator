namespace NuclearLeakSim_WinForms
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.MainLayout = new System.Windows.Forms.TableLayoutPanel();

            // --- 1. INPUT PANEL (KIRI) ---
            this.InputPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.BtnDefaultSimulate = new System.Windows.Forms.Button();
            this.BtnRunLeakScenario = new System.Windows.Forms.Button();

            // Groups
            this.GroupGeneralParams = new System.Windows.Forms.GroupBox();
            this.LblFormulaGeneral = new System.Windows.Forms.Label();
            this.PanelTau = new System.Windows.Forms.Panel(); this.labelTau = new System.Windows.Forms.Label(); this.TrackBarTau = new System.Windows.Forms.TrackBar();
            this.PanelK = new System.Windows.Forms.Panel(); this.labelK = new System.Windows.Forms.Label(); this.TrackBarK = new System.Windows.Forms.TrackBar();

            this.GroupGeiger = new System.Windows.Forms.GroupBox();
            this.LblFormulaGeiger = new System.Windows.Forms.Label();
            this.PanelTauD = new System.Windows.Forms.Panel(); this.labelTauD = new System.Windows.Forms.Label(); this.TrackBarTauD = new System.Windows.Forms.TrackBar();
            this.PanelTauE = new System.Windows.Forms.Panel(); this.labelTauE = new System.Windows.Forms.Label(); this.TrackBarTauE = new System.Windows.Forms.TrackBar();
            this.PanelLambda = new System.Windows.Forms.Panel(); this.labelLambda = new System.Windows.Forms.Label(); this.TrackBarLambda = new System.Windows.Forms.TrackBar();

            this.GroupAcoustic = new System.Windows.Forms.GroupBox();
            this.LblFormulaAcoustic = new System.Windows.Forms.Label();
            this.PanelQ = new System.Windows.Forms.Panel(); this.labelQ = new System.Windows.Forms.Label(); this.TrackBarQ = new System.Windows.Forms.TrackBar();
            this.PanelF0 = new System.Windows.Forms.Panel(); this.labelF0 = new System.Windows.Forms.Label(); this.TrackBarF0 = new System.Windows.Forms.TrackBar();

            this.GroupSensorInfo = new System.Windows.Forms.GroupBox();
            this.LblInfoGeiger = new System.Windows.Forms.Label(); this.LblInfoAcoustic = new System.Windows.Forms.Label(); this.LblInfoPressure = new System.Windows.Forms.Label();
            this.LblInfoHumidity = new System.Windows.Forms.Label(); this.LblInfoRTD = new System.Windows.Forms.Label();

            // --- 2. SENSOR PANEL (TENGAH) ---
            this.SensorScrollPanel = new System.Windows.Forms.Panel();
            this.SensorLayout = new System.Windows.Forms.TableLayoutPanel();
            this.GroupRTD = new System.Windows.Forms.GroupBox(); this.LblDetailRTD = new System.Windows.Forms.Label(); this.PlotLayoutRTD = new System.Windows.Forms.TableLayoutPanel(); this.Plot_RTD_FFT = new ScottPlot.FormsPlot(); this.Plot_RTD_Time = new ScottPlot.FormsPlot();
            this.GroupPressure = new System.Windows.Forms.GroupBox(); this.LblDetailPressure = new System.Windows.Forms.Label(); this.PlotLayoutPressure = new System.Windows.Forms.TableLayoutPanel(); this.Plot_Pressure_FFT = new ScottPlot.FormsPlot(); this.Plot_Pressure_Time = new ScottPlot.FormsPlot();
            this.GroupHumidity = new System.Windows.Forms.GroupBox(); this.LblDetailHumidity = new System.Windows.Forms.Label(); this.PlotLayoutHumidity = new System.Windows.Forms.TableLayoutPanel(); this.Plot_Hum_FFT = new ScottPlot.FormsPlot(); this.Plot_Hum_Time = new ScottPlot.FormsPlot();
            this.GroupAcousticPlots = new System.Windows.Forms.GroupBox(); this.LblDetailAcoustic = new System.Windows.Forms.Label(); this.PlotLayoutAcoustic = new System.Windows.Forms.TableLayoutPanel(); this.Plot_Acoustic_FFT = new ScottPlot.FormsPlot(); this.Plot_Acoustic_Time = new ScottPlot.FormsPlot();
            this.GroupGeigerPlots = new System.Windows.Forms.GroupBox(); this.LblDetailGeiger = new System.Windows.Forms.Label(); this.PlotLayoutGeiger = new System.Windows.Forms.TableLayoutPanel(); this.Plot_Geiger_FFT = new ScottPlot.FormsPlot(); this.Plot_Geiger_Time = new ScottPlot.FormsPlot();

            // --- 3. SYSTEM PANEL (KANAN - UPDATE) ---
            this.SystemScrollPanel = new System.Windows.Forms.Panel(); // Panel Scroll Baru
            this.SystemPlotLayout = new System.Windows.Forms.TableLayoutPanel();
            this.SystemDiagramBox = new System.Windows.Forms.PictureBox();
            this.LblTotalSystemFormula = new System.Windows.Forms.Label();
            this.Plot_PoleZero_S = new ScottPlot.FormsPlot();
            this.LblSFormula = new System.Windows.Forms.Label();
            this.Plot_PoleZero_Z = new ScottPlot.FormsPlot();
            this.LblZFormula = new System.Windows.Forms.Label();

            // Initialization Code
            this.MainLayout.SuspendLayout(); this.InputPanel.SuspendLayout();
            this.GroupGeneralParams.SuspendLayout(); this.PanelTau.SuspendLayout(); ((System.ComponentModel.ISupportInitialize)(this.TrackBarTau)).BeginInit(); this.PanelK.SuspendLayout(); ((System.ComponentModel.ISupportInitialize)(this.TrackBarK)).BeginInit();
            this.GroupGeiger.SuspendLayout(); this.PanelTauD.SuspendLayout(); ((System.ComponentModel.ISupportInitialize)(this.TrackBarTauD)).BeginInit(); this.PanelTauE.SuspendLayout(); ((System.ComponentModel.ISupportInitialize)(this.TrackBarTauE)).BeginInit(); this.PanelLambda.SuspendLayout(); ((System.ComponentModel.ISupportInitialize)(this.TrackBarLambda)).BeginInit();
            this.GroupAcoustic.SuspendLayout(); this.PanelQ.SuspendLayout(); ((System.ComponentModel.ISupportInitialize)(this.TrackBarQ)).BeginInit(); this.PanelF0.SuspendLayout(); ((System.ComponentModel.ISupportInitialize)(this.TrackBarF0)).BeginInit();
            this.GroupSensorInfo.SuspendLayout();
            this.SystemScrollPanel.SuspendLayout(); // Init Scroll Panel
            this.SystemPlotLayout.SuspendLayout(); ((System.ComponentModel.ISupportInitialize)(this.SystemDiagramBox)).BeginInit();
            this.SensorScrollPanel.SuspendLayout(); this.SensorLayout.SuspendLayout();
            this.GroupRTD.SuspendLayout(); this.PlotLayoutRTD.SuspendLayout();
            this.GroupPressure.SuspendLayout(); this.PlotLayoutPressure.SuspendLayout();
            this.GroupHumidity.SuspendLayout(); this.PlotLayoutHumidity.SuspendLayout();
            this.GroupAcousticPlots.SuspendLayout(); this.PlotLayoutAcoustic.SuspendLayout();
            this.GroupGeigerPlots.SuspendLayout(); this.PlotLayoutGeiger.SuspendLayout();
            this.SuspendLayout();

            // 
            // MainLayout
            // 
            this.MainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainLayout.ColumnCount = 3;
            this.MainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 340F)); // Kiri
            this.MainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F)); // Tengah
            this.MainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F)); // Kanan
            this.MainLayout.Controls.Add(this.InputPanel, 0, 0);
            this.MainLayout.Controls.Add(this.SensorScrollPanel, 1, 0);
            this.MainLayout.Controls.Add(this.SystemScrollPanel, 2, 0); // Masukkan ScrollPanel, bukan PlotLayout langsung

            // 
            // InputPanel (Kiri)
            // 
            this.InputPanel.AutoScroll = true;
            this.InputPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.InputPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.InputPanel.WrapContents = false;
            this.InputPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InputPanel.Controls.Add(this.BtnDefaultSimulate);
            this.InputPanel.Controls.Add(this.BtnRunLeakScenario);
            this.InputPanel.Controls.Add(this.GroupGeneralParams);
            this.InputPanel.Controls.Add(this.GroupGeiger);
            this.InputPanel.Controls.Add(this.GroupAcoustic);
            this.InputPanel.Controls.Add(this.GroupSensorInfo);

            // Buttons & Groups (Isi Panel Kiri - Sama seperti sebelumnya)
            this.BtnDefaultSimulate.Size = new System.Drawing.Size(300, 40); this.BtnDefaultSimulate.Text = "Reset Simulation (Default)"; this.BtnDefaultSimulate.Click += new System.EventHandler(this.BtnDefaultSimulate_Click); this.BtnDefaultSimulate.Margin = new System.Windows.Forms.Padding(3, 3, 3, 15);
            this.BtnRunLeakScenario.Size = new System.Drawing.Size(300, 40); this.BtnRunLeakScenario.Text = "Run Leak Scenario (Async)"; this.BtnRunLeakScenario.Click += new System.EventHandler(this.BtnRunLeakScenario_Click); this.BtnRunLeakScenario.Margin = new System.Windows.Forms.Padding(3, 3, 3, 20);

            this.GroupGeneralParams.Size = new System.Drawing.Size(300, 180); this.GroupGeneralParams.Text = "Parameter Umum (Orde-1)";
            this.LblFormulaGeneral.Location = new System.Drawing.Point(10, 85); this.LblFormulaGeneral.Size = new System.Drawing.Size(280, 80); this.LblFormulaGeneral.Text = "G(s) = K / (τs + 1)\nK: Gain\nτ: Time Constant";
            this.GroupGeneralParams.Controls.Add(this.LblFormulaGeneral); this.GroupGeneralParams.Controls.Add(this.PanelK); this.GroupGeneralParams.Controls.Add(this.PanelTau);
            this.PanelK.Location = new System.Drawing.Point(10, 20); this.PanelK.Size = new System.Drawing.Size(130, 50); this.PanelK.Controls.Add(this.labelK); this.PanelK.Controls.Add(this.TrackBarK); this.labelK.Text = "K (Gain)"; this.labelK.Location = new System.Drawing.Point(3, 0); this.TrackBarK.Location = new System.Drawing.Point(0, 15); this.TrackBarK.Size = new System.Drawing.Size(125, 45); this.TrackBarK.TickStyle = System.Windows.Forms.TickStyle.None;
            this.PanelTau.Location = new System.Drawing.Point(150, 20); this.PanelTau.Size = new System.Drawing.Size(130, 50); this.PanelTau.Controls.Add(this.labelTau); this.PanelTau.Controls.Add(this.TrackBarTau); this.labelTau.Text = "τ (Time)"; this.labelTau.Location = new System.Drawing.Point(3, 0); this.TrackBarTau.Location = new System.Drawing.Point(0, 15); this.TrackBarTau.Size = new System.Drawing.Size(125, 45); this.TrackBarTau.TickStyle = System.Windows.Forms.TickStyle.None;

            this.GroupGeiger.Size = new System.Drawing.Size(300, 260); this.GroupGeiger.Text = "Parameter Geiger";
            this.LblFormulaGeiger.Location = new System.Drawing.Point(10, 145); this.LblFormulaGeiger.Size = new System.Drawing.Size(280, 100); this.LblFormulaGeiger.Text = "P(k) = (λt)^k * e^(-λt) / k!\nλ: Rate (Kejadian/Detik)\nτe: Pulse Decay\nτd: Dead Time";
            this.GroupGeiger.Controls.Add(this.LblFormulaGeiger); this.GroupGeiger.Controls.Add(this.PanelLambda); this.GroupGeiger.Controls.Add(this.PanelTauE); this.GroupGeiger.Controls.Add(this.PanelTauD);
            this.PanelLambda.Location = new System.Drawing.Point(10, 20); this.PanelLambda.Size = new System.Drawing.Size(130, 50); this.PanelLambda.Controls.Add(this.labelLambda); this.PanelLambda.Controls.Add(this.TrackBarLambda); this.labelLambda.Text = "λ (Rate)"; this.labelLambda.Location = new System.Drawing.Point(3, 0); this.TrackBarLambda.Location = new System.Drawing.Point(0, 15); this.TrackBarLambda.Size = new System.Drawing.Size(125, 45); this.TrackBarLambda.TickStyle = System.Windows.Forms.TickStyle.None;
            this.PanelTauE.Location = new System.Drawing.Point(150, 20); this.PanelTauE.Size = new System.Drawing.Size(130, 50); this.PanelTauE.Controls.Add(this.labelTauE); this.PanelTauE.Controls.Add(this.TrackBarTauE); this.labelTauE.Text = "τe (Decay)"; this.labelTauE.Location = new System.Drawing.Point(3, 0); this.TrackBarTauE.Location = new System.Drawing.Point(0, 15); this.TrackBarTauE.Size = new System.Drawing.Size(125, 45); this.TrackBarTauE.TickStyle = System.Windows.Forms.TickStyle.None;
            this.PanelTauD.Location = new System.Drawing.Point(10, 80); this.PanelTauD.Size = new System.Drawing.Size(130, 50); this.PanelTauD.Controls.Add(this.labelTauD); this.PanelTauD.Controls.Add(this.TrackBarTauD); this.labelTauD.Text = "τd (Dead)"; this.labelTauD.Location = new System.Drawing.Point(3, 0); this.TrackBarTauD.Location = new System.Drawing.Point(0, 15); this.TrackBarTauD.Size = new System.Drawing.Size(125, 45); this.TrackBarTauD.TickStyle = System.Windows.Forms.TickStyle.None;

            this.GroupAcoustic.Size = new System.Drawing.Size(300, 220); this.GroupAcoustic.Text = "Parameter Akustik (Orde-2)";
            this.LblFormulaAcoustic.Location = new System.Drawing.Point(10, 85); this.LblFormulaAcoustic.Size = new System.Drawing.Size(280, 120); this.LblFormulaAcoustic.Text = "G(s) = K·ωn² / (s² + 2ζωn·s + ωn²)\nF0: Freq Resonansi\nQ: Quality Factor\nζ = 1/(2Q)";
            this.GroupAcoustic.Controls.Add(this.LblFormulaAcoustic); this.GroupAcoustic.Controls.Add(this.PanelF0); this.GroupAcoustic.Controls.Add(this.PanelQ);
            this.PanelF0.Location = new System.Drawing.Point(10, 20); this.PanelF0.Size = new System.Drawing.Size(130, 50); this.PanelF0.Controls.Add(this.labelF0); this.PanelF0.Controls.Add(this.TrackBarF0); this.labelF0.Text = "F0 (Freq)"; this.labelF0.Location = new System.Drawing.Point(3, 0); this.TrackBarF0.Location = new System.Drawing.Point(0, 15); this.TrackBarF0.Size = new System.Drawing.Size(125, 45); this.TrackBarF0.TickStyle = System.Windows.Forms.TickStyle.None;
            this.PanelQ.Location = new System.Drawing.Point(150, 20); this.PanelQ.Size = new System.Drawing.Size(130, 50); this.PanelQ.Controls.Add(this.labelQ); this.PanelQ.Controls.Add(this.TrackBarQ); this.labelQ.Text = "Q (Quality)"; this.labelQ.Location = new System.Drawing.Point(3, 0); this.TrackBarQ.Location = new System.Drawing.Point(0, 15); this.TrackBarQ.Size = new System.Drawing.Size(125, 45); this.TrackBarQ.TickStyle = System.Windows.Forms.TickStyle.None;

            this.GroupSensorInfo.Size = new System.Drawing.Size(300, 150); this.GroupSensorInfo.Text = "Info Sampling Sensor";
            this.GroupSensorInfo.Controls.AddRange(new System.Windows.Forms.Control[] { this.LblInfoRTD, this.LblInfoPressure, this.LblInfoHumidity, this.LblInfoAcoustic, this.LblInfoGeiger });
            this.LblInfoRTD.Location = new System.Drawing.Point(10, 25); this.LblInfoRTD.Text = "RTD: 10 Hz";
            this.LblInfoPressure.Location = new System.Drawing.Point(10, 45); this.LblInfoPressure.Text = "Pressure: 1,000 Hz";
            this.LblInfoHumidity.Location = new System.Drawing.Point(10, 65); this.LblInfoHumidity.Text = "Humidity: 20 Hz";
            this.LblInfoAcoustic.Location = new System.Drawing.Point(10, 85); this.LblInfoAcoustic.Text = "Acoustic: 30,000 Hz";
            this.LblInfoGeiger.Location = new System.Drawing.Point(10, 105); this.LblInfoGeiger.Text = "Geiger: 11,000 Hz";


            // --- 3. SYSTEM SCROLL PANEL (KANAN - FIXED) ---
            this.SystemScrollPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SystemScrollPanel.AutoScroll = true; // ENABLE SCROLLING
            this.SystemScrollPanel.Controls.Add(this.SystemPlotLayout);

            // SystemPlotLayout (Inside Scroll Panel)
            this.SystemPlotLayout.Dock = System.Windows.Forms.DockStyle.Top; // Dock Top agar bisa discroll
            this.SystemPlotLayout.AutoSize = true; // AutoSize agar tinggi menyesuaikan konten
            this.SystemPlotLayout.ColumnCount = 1;
            this.SystemPlotLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.SystemPlotLayout.RowCount = 6;

            // ATUR TINGGI FIXED UNTUK AGAR BISA DI-SCROLL
            this.SystemPlotLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F)); // Gambar
            this.SystemPlotLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));  // Rumus Total
            this.SystemPlotLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 400F)); // Plot S (Fixed Height)
            this.SystemPlotLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));  // Rumus S
            this.SystemPlotLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 400F)); // Plot Z (Fixed Height)
            this.SystemPlotLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));  // Rumus Z
            // Total Height = 200 + 40 + 400 + 60 + 400 + 60 = 1160px (Pasti Scroll)

            this.SystemDiagramBox.Dock = System.Windows.Forms.DockStyle.Fill; this.SystemDiagramBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.LblTotalSystemFormula.Text = "G_Total(s) = Σ G_i(s)"; this.LblTotalSystemFormula.TextAlign = System.Drawing.ContentAlignment.MiddleCenter; this.LblTotalSystemFormula.Dock = System.Windows.Forms.DockStyle.Fill; this.LblTotalSystemFormula.Font = new System.Drawing.Font("Consolas", 11F, System.Drawing.FontStyle.Bold);
            this.Plot_PoleZero_S.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblSFormula.Text = "Pole S: s = -1/τ"; this.LblSFormula.TextAlign = System.Drawing.ContentAlignment.MiddleLeft; this.LblSFormula.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Plot_PoleZero_Z.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblZFormula.Text = "Mapping Z: z = e^(sT)"; this.LblZFormula.TextAlign = System.Drawing.ContentAlignment.MiddleLeft; this.LblZFormula.Dock = System.Windows.Forms.DockStyle.Fill;

            this.SystemPlotLayout.Controls.Add(this.SystemDiagramBox, 0, 0);
            this.SystemPlotLayout.Controls.Add(this.LblTotalSystemFormula, 0, 1);
            this.SystemPlotLayout.Controls.Add(this.Plot_PoleZero_S, 0, 2);
            this.SystemPlotLayout.Controls.Add(this.LblSFormula, 0, 3);
            this.SystemPlotLayout.Controls.Add(this.Plot_PoleZero_Z, 0, 4);
            this.SystemPlotLayout.Controls.Add(this.LblZFormula, 0, 5);

            // --- SENSOR PANEL (TENGAH) ---
            this.SensorScrollPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SensorScrollPanel.AutoScroll = true;
            this.SensorScrollPanel.Controls.Add(this.SensorLayout);

            this.SensorLayout.Dock = System.Windows.Forms.DockStyle.Top;
            this.SensorLayout.AutoSize = true;
            this.SensorLayout.RowCount = 5;
            this.SensorLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.SensorLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.SensorLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.SensorLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.SensorLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 350F));

            this.SensorLayout.Controls.Add(this.GroupRTD);
            this.SensorLayout.Controls.Add(this.GroupPressure);
            this.SensorLayout.Controls.Add(this.GroupHumidity);
            this.SensorLayout.Controls.Add(this.GroupAcousticPlots);
            this.SensorLayout.Controls.Add(this.GroupGeigerPlots);

            // Setup Groups (Helper)
            void SetupSensorGroup(System.Windows.Forms.GroupBox grp, System.Windows.Forms.Label lbl, System.Windows.Forms.TableLayoutPanel tbl, ScottPlot.FormsPlot p1, ScottPlot.FormsPlot p2, string title, string detail)
            {
                grp.Text = title; grp.Dock = System.Windows.Forms.DockStyle.Fill; grp.Height = 350;
                lbl.Text = detail; lbl.Dock = System.Windows.Forms.DockStyle.Top; lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter; lbl.Height = 20;
                tbl.Dock = System.Windows.Forms.DockStyle.Fill; tbl.ColumnCount = 2; tbl.RowCount = 1;
                tbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
                tbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
                p1.Dock = System.Windows.Forms.DockStyle.Fill; p2.Dock = System.Windows.Forms.DockStyle.Fill;
                tbl.Controls.Add(p1, 0, 0); tbl.Controls.Add(p2, 1, 0);
                grp.Controls.Add(tbl); grp.Controls.Add(lbl);
            }

            SetupSensorGroup(this.GroupRTD, this.LblDetailRTD, this.PlotLayoutRTD, this.Plot_RTD_Time, this.Plot_RTD_FFT, "Sensor: PT100 RTD", "Freq: 5Hz | fs: 10Hz");
            SetupSensorGroup(this.GroupPressure, this.LblDetailPressure, this.PlotLayoutPressure, this.Plot_Pressure_Time, this.Plot_Pressure_FFT, "Sensor: MPX5700 Pressure", "Freq: 500Hz | fs: 1kHz");
            SetupSensorGroup(this.GroupHumidity, this.LblDetailHumidity, this.PlotLayoutHumidity, this.Plot_Hum_Time, this.Plot_Hum_FFT, "Sensor: SHT31 Humidity", "Freq: 10Hz | fs: 20Hz");
            SetupSensorGroup(this.GroupAcousticPlots, this.LblDetailAcoustic, this.PlotLayoutAcoustic, this.Plot_Acoustic_Time, this.Plot_Acoustic_FFT, "Sensor: ADMP401 Acoustic", "Freq: 15kHz | fs: 30kHz");
            SetupSensorGroup(this.GroupGeigerPlots, this.LblDetailGeiger, this.PlotLayoutGeiger, this.Plot_Geiger_Time, this.Plot_Geiger_FFT, "Sensor: SBM-20 Geiger", "Freq: 5.2kHz | fs: 11kHz");

            // Final Config
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1500, 900);
            this.Controls.Add(this.MainLayout);
            this.Name = "Form1";
            this.Text = "Nuclear Sensor Dashboard";

            this.MainLayout.ResumeLayout(false);
            this.InputPanel.ResumeLayout(false);
            this.SystemScrollPanel.ResumeLayout(false); this.SystemScrollPanel.PerformLayout(); // Updated
            this.SystemPlotLayout.ResumeLayout(false); this.SystemPlotLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SystemDiagramBox)).EndInit();
            this.SensorScrollPanel.ResumeLayout(false); this.SensorScrollPanel.PerformLayout();
            this.SensorLayout.ResumeLayout(false);
            this.GroupGeneralParams.ResumeLayout(false); this.GroupGeneralParams.PerformLayout();
            this.PanelTau.ResumeLayout(false); this.PanelTau.PerformLayout(); ((System.ComponentModel.ISupportInitialize)(this.TrackBarTau)).EndInit();
            this.PanelK.ResumeLayout(false); this.PanelK.PerformLayout(); ((System.ComponentModel.ISupportInitialize)(this.TrackBarK)).EndInit();
            this.GroupGeiger.ResumeLayout(false); this.GroupGeiger.PerformLayout();
            this.PanelTauD.ResumeLayout(false); this.PanelTauD.PerformLayout(); ((System.ComponentModel.ISupportInitialize)(this.TrackBarTauD)).EndInit();
            this.PanelTauE.ResumeLayout(false); this.PanelTauE.PerformLayout(); ((System.ComponentModel.ISupportInitialize)(this.TrackBarTauE)).EndInit();
            this.PanelLambda.ResumeLayout(false); this.PanelLambda.PerformLayout(); ((System.ComponentModel.ISupportInitialize)(this.TrackBarLambda)).EndInit();
            this.GroupAcoustic.ResumeLayout(false); this.GroupAcoustic.PerformLayout();
            this.PanelQ.ResumeLayout(false); this.PanelQ.PerformLayout(); ((System.ComponentModel.ISupportInitialize)(this.TrackBarQ)).EndInit();
            this.PanelF0.ResumeLayout(false); this.PanelF0.PerformLayout(); ((System.ComponentModel.ISupportInitialize)(this.TrackBarF0)).EndInit();
            this.GroupSensorInfo.ResumeLayout(false); this.GroupSensorInfo.PerformLayout();
            this.GroupRTD.ResumeLayout(false); this.PlotLayoutRTD.ResumeLayout(false);
            this.GroupPressure.ResumeLayout(false); this.PlotLayoutPressure.ResumeLayout(false);
            this.GroupHumidity.ResumeLayout(false); this.PlotLayoutHumidity.ResumeLayout(false);
            this.GroupAcousticPlots.ResumeLayout(false); this.PlotLayoutAcoustic.ResumeLayout(false);
            this.GroupGeigerPlots.ResumeLayout(false); this.PlotLayoutGeiger.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        // Variables
        private System.Windows.Forms.TableLayoutPanel MainLayout;
        private System.Windows.Forms.FlowLayoutPanel InputPanel;
        private System.Windows.Forms.Button BtnDefaultSimulate, BtnRunLeakScenario;
        private System.Windows.Forms.GroupBox GroupGeneralParams, GroupGeiger, GroupAcoustic, GroupSensorInfo;
        private System.Windows.Forms.Panel PanelTau, PanelK, PanelLambda, PanelTauE, PanelTauD, PanelQ, PanelF0;
        private System.Windows.Forms.Label labelTau, labelK, labelLambda, labelTauE, labelTauD, labelQ, labelF0;
        private System.Windows.Forms.TrackBar TrackBarTau, TrackBarK, TrackBarLambda, TrackBarTauE, TrackBarTauD, TrackBarQ, TrackBarF0;
        private System.Windows.Forms.Label LblFormulaGeneral, LblFormulaGeiger, LblFormulaAcoustic;
        private System.Windows.Forms.Label LblInfoGeiger, LblInfoAcoustic, LblInfoPressure, LblInfoHumidity, LblInfoRTD;

        private System.Windows.Forms.Panel SystemScrollPanel; // NEW
        private System.Windows.Forms.TableLayoutPanel SystemPlotLayout;
        private System.Windows.Forms.PictureBox SystemDiagramBox;
        private System.Windows.Forms.Label LblTotalSystemFormula, LblSFormula, LblZFormula;
        private ScottPlot.FormsPlot Plot_PoleZero_S, Plot_PoleZero_Z;

        private System.Windows.Forms.Panel SensorScrollPanel;
        private System.Windows.Forms.TableLayoutPanel SensorLayout;
        private System.Windows.Forms.GroupBox GroupRTD, GroupPressure, GroupHumidity, GroupAcousticPlots, GroupGeigerPlots;
        private System.Windows.Forms.Label LblDetailRTD, LblDetailPressure, LblDetailHumidity, LblDetailAcoustic, LblDetailGeiger;
        private System.Windows.Forms.TableLayoutPanel PlotLayoutRTD, PlotLayoutPressure, PlotLayoutHumidity, PlotLayoutAcoustic, PlotLayoutGeiger;
        private ScottPlot.FormsPlot Plot_RTD_Time, Plot_RTD_FFT, Plot_Pressure_Time, Plot_Pressure_FFT, Plot_Hum_Time, Plot_Hum_FFT, Plot_Acoustic_Time, Plot_Acoustic_FFT, Plot_Geiger_Time, Plot_Geiger_FFT;
    }
}