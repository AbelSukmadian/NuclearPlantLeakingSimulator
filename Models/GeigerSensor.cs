using System;

namespace NuclearLeakSim_WinForms.Models
{
    public class GeigerSensor : SensorBase
    {
        private readonly Random _rand = new Random(123);
        // stochastic and shape parameters
        public double Lambda { get; set; } = 50.0;       // true event rate (events/sec)
        public double DeadTime { get; set; } = 0.00019; // tau_d (s)
        public double PulseTau { get; set; } = 0.00010; // tau_e (s) decay of pulse

        // Properti K (Gain) sudah diwarisi (inherited) dari SensorBase

        public GeigerSensor()
        {
            Name = "SBM-20 Geiger";
            SampleRate = 10000; // 10 kHz good for pulse shape
            Tau = PulseTau;
            K = 1.0; // Atur nilai K dari base class
        }

        // Generate time-array-based waveform (pulse train with decay + dead-time)
        public override double[] Generate(double[] time)
        {
            int n = time.Length;
            if (n == 0) return Array.Empty<double>();

            // [PERBAIKAN BUG] Tambahkan pengecekan 'n > 1' untuk dt
            double dt = (n > 1) ? (time[1] - time[0]) : (1.0 / SampleRate);

            double[] output = new double[n];
            double nextAvailable = 0.0;

            for (int i = 0; i < n; i++)
            {
                if (time[i] < nextAvailable) continue;

                // Poisson arrival with rate Lambda
                if (_rand.NextDouble() < Lambda * dt)
                {
                    // shape pulse for some samples
                    int pulseSamples = Math.Min((int)Math.Ceiling(PulseTau / dt * 8.0), n - i); // length ~ several taus
                    for (int j = 0; j < pulseSamples && (i + j) < n; j++)
                    {
                        double tRel = j * dt;
                        output[i + j] += K * Math.Exp(-tRel / PulseTau);
                    }
                    // enforce dead-time (non-paralyzable simple model)
                    nextAvailable = time[i] + DeadTime;
                }
            }
            return output;
        }
    }
}