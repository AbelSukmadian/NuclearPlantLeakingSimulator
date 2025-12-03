using System;
using System.Linq;
using System.Numerics;
using MathNet.Numerics;

namespace NuclearLeakSim_WinForms.Models
{
    public static class SimulationUtils
    {
        // Simulate first-order response of sensor to arbitrary input array
        // y'[t] = (1/τ) * (u[t] - y[t])  -- Euler forward
        public static double[] SimulateFirstOrder(double[] input, double tau, double dt, double initial = 0.0)
        {
            int n = input.Length;
            double[] y = new double[n];
            if (n == 0) return y;
            y[0] = initial;
            if (tau <= 0) tau = 1e-6;
            for (int i = 1; i < n; i++)
            {
                y[i] = y[i - 1] + (dt / tau) * (input[i - 1] - y[i - 1]);
                if (double.IsNaN(y[i]) || double.IsInfinity(y[i])) y[i] = 0.0;
            }
            return y;
        }

        // Simulate Geiger response from per-sample lambda (events/sec). Returns pulse train (0/1 scaled by pulse amplitude)
        public static double[] SimulateGeiger(double[] lambdaSeries, double pulseTau, double deadTime, double dt, double K = 1.0)
        {
            int n = lambdaSeries.Length;
            double[] outp = new double[n];
            Random rand = new Random(12345);
            double nextAvailable = -1e9;
            for (int i = 0; i < n; i++)
            {
                double t = i * dt;
                double lam = lambdaSeries[i];
                double p = lam * dt;
                if (p < 0) p = 0;
                if (t >= nextAvailable && rand.NextDouble() < p)
                {
                    // create exponential pulse starting at i
                    for (int j = i; j < n; j++)
                    {
                        double dtRel = (j - i) * dt;
                        outp[j] += K * Math.Exp(-dtRel / Math.Max(pulseTau, 1e-9));
                    }
                    nextAvailable = t + deadTime;
                }
            }
            // clamp NaN/Inf & normalize a bit
            for (int i = 0; i < n; i++)
            {
                if (double.IsNaN(outp[i]) || double.IsInfinity(outp[i])) outp[i] = 0;
            }
            return outp;
        }

        // Simulate acoustic resonant system driven by external excitation (input array)
        // Uses simple 2nd-order Euler integration (state-space)
        public static double[] SimulateAcousticFromExcitation(double[] excitation, double f0, double Q, double dt, double K = 1.0)
        {
            int n = excitation.Length;
            double[] y = new double[n];
            if (n == 0) return y;
            double w0 = 2.0 * Math.PI * f0;
            double alpha = w0 / Q;
            double x1 = 0.0, x2 = 0.0; // x1 = output, x2 = derivative
            for (int i = 0; i < n; i++)
            {
                double u = excitation[i] * K;
                double dd = -alpha * x2 - w0 * w0 * x1 + u;
                x2 += dd * dt;
                x1 += x2 * dt;
                if (double.IsNaN(x1) || double.IsInfinity(x1)) { x1 = 0; x2 = 0; }
                y[i] = x1;
            }
            // normalization to avoid huge values
            double max = y.Max(Math.Abs);
            if (max > 0)
            {
                for (int i = 0; i < n; i++) y[i] /= max;
            }
            return y;
        }
    }
}
