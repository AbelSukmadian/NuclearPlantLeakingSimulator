using System;

namespace NuclearLeakSim_WinForms.Models
{
    public class HumiditySensor : SensorBase
    {
        // HAPUS properti TauHumidity dan Kgain
        // public double TauHumidity { get; set; } = 8.0; 
        // public double Kgain { get; set; } = 1.0;       

        public HumiditySensor()
        {
            Name = "SHT31 Humidity";
            SampleRate = 2;
            // Gunakan nilai default dari base class
            // Tau = TauHumidity; // HAPUS
            // K = Kgain;       // HAPUS
        }

        public override double[] Generate(double[] time)
        {
            int n = time.Length;
            double[] Henv = new double[n];
            for (int i = 0; i < n; i++)
            {
                int periodIndex = (int)(time[i] / 30.0);
                Henv[i] = (periodIndex % 2 == 0) ? 40.0 : 80.0;
            }

            double dt = (n > 1) ? (time[1] - time[0]) : 1.0;
            double[] Hs = new double[n];
            Hs[0] = Henv[0];

            // PERBAIKAN: Gunakan 'this.Tau' (dari slider) bukan 'TauHumidity'
            double tau = Math.Max(this.Tau, 1e-6); // Hindari pembagian nol
            for (int i = 1; i < n; i++)
            {
                Hs[i] = Hs[i - 1] + (dt / tau) * (Henv[i - 1] - Hs[i - 1]);
            }

            double[] output = new double[n];
            for (int i = 0; i < n; i++)
                // PERBAIKAN: Gunakan 'this.K' (dari slider) bukan 'Kgain'
                output[i] = this.K * Hs[i];

            return output;
        }
    }
}