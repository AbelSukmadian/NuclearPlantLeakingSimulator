using System;
using System.Linq;

namespace NuclearLeakSim_WinForms.Models // Pastikan namespace ini benar
{
    public class AcousticSensor : SensorBase
    {
        // PERUBAHAN: Tambahkan seed tetap (misalnya 42)
        private Random rand = new Random(42);
        public double F0 { get; set; } = 2000; // resonant freq (Hz)
        public double Q { get; set; } = 2.0;   // quality factor (damping)
        public double NoiseAmp { get; set; } = 0.2;

        public AcousticSensor()
        {
            Name = "ADMP401 Acoustic";
            SampleRate = 8000;
            K = 1.0;
            // Tau tidak relevan di sini
        }

        public override double[] Generate(double[] time)
        {
            int n = time.Length;
            double[] signal = new double[n];
            double dt = (n > 1) ? (time[1] - time[0]) : 1.0 / SampleRate;

            // Pastikan F0 dan Q tidak nol
            double f0_safe = Math.Max(this.F0, 1.0);
            double q_safe = Math.Max(this.Q, 0.01);

            double w0 = 2 * Math.PI * f0_safe;
            double alpha = w0 / q_safe;

            double x1 = 0, x2 = 0;

            // Penting: Reset Random di awal Generate agar urutannya selalu sama
            //          untuk durasi simulasi yang sama
            rand = new Random(42); // Reset seed setiap kali Generate dipanggil

            for (int i = 0; i < n; i++)
            {
                // Noise sekarang akan selalu sama untuk pemanggilan Generate yang identik
                double noise = NoiseAmp * (rand.NextDouble() - 0.5);
                double input = noise;

                // Gunakan K, F0 (via w0), dan Q (via alpha) dari properti
                double dx2 = -alpha * x2 - w0 * w0 * x1 + this.K * input;

                x2 += dx2 * dt;
                x1 += x2 * dt;
                signal[i] = x1;
            }

            double max = Math.Abs(signal.Max()) > Math.Abs(signal.Min()) ? Math.Abs(signal.Max()) : Math.Abs(signal.Min());
            if (max > 1e-9) // Hindari pembagian nol jika sinyal nol
            {
                for (int i = 0; i < n; i++)
                {
                    signal[i] /= max; // Normalisasi
                }
            }

            return signal;
        }
    }
}