using System;

namespace NuclearLeakSim_WinForms.Models
{
    public class PressureSensor : SensorBase
    {
        // HAPUS properti TauPressure dan Kgain
        // public double TauPressure { get; set; } = 0.001; 
        // public double Kgain { get; set; } = 1.0;         

        public PressureSensor()
        {
            Name = "MPX5700 Pressure";
            SampleRate = 4000;
            // Gunakan nilai default dari base class
            // Tau = TauPressure; // HAPUS
            // K = Kgain;       // HAPUS
        }

        public override double[] Generate(double[] time)
        {
            int n = time.Length;
            double[] penv = new double[n];

            for (int i = 0; i < n; i++)
            {
                double t = time[i];
                double step = (t >= 1.0) ? 20.0 : 0.0;
                double smallOsc = 0.5 * Math.Sin(2.0 * Math.PI * 50.0 * t);
                penv[i] = step + smallOsc;
            }

            double dt = (n > 1) ? (time[1] - time[0]) : 1.0;
            double[] ps = new double[n];
            ps[0] = penv[0];

            // PERBAIKAN: Gunakan 'this.Tau' (dari slider) bukan 'TauPressure'
            double tau = Math.Max(this.Tau, 1e-6); // Hindari pembagian nol
            for (int i = 1; i < n; i++)
            {
                ps[i] = ps[i - 1] + (dt / tau) * (penv[i - 1] - ps[i - 1]);
            }

            double[] output = new double[n];
            for (int i = 0; i < n; i++)
                // PERBAIKAN: Gunakan 'this.K' (dari slider) bukan 'Kgain'
                output[i] = this.K * ps[i];

            return output;
        }
    }
}