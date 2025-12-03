using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using MathNet.Numerics.LinearAlgebra.Complex;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics;

namespace NuclearLeakSim_WinForms.Models
{
    public enum SensorType { FirstOrder, SecondOrder, Geiger }

    public class SensorModelDescriptor
    {
        public string Name;
        public double K;
        public double Tau;       // for first order or pulseTau for Geiger
        public SensorType Type;
        public double wn;        // for second order
        public double zeta;      // for second order
        public double pulseTau;  // geiger pulse shaping
        public double deadTime;  // geiger dead time

        public SensorModelDescriptor(string name, double K, double Tau, SensorType type,
            double wn = 0, double zeta = 0, double pulseTau = 0, double deadTime = 0)
        {
            Name = name; this.K = K; this.Tau = Tau; Type = type;
            this.wn = wn; this.zeta = zeta; this.pulseTau = pulseTau; this.deadTime = deadTime;
        }
    }

    public static class SystemAnalyzer
    {
        // Build polynomial numerator & denominator for a sensor model (s-domain)
        // Polynomials are represented as double[] coefficients in descending powers:
        // e.g. a2*s^2 + a1*s + a0 -> [a2, a1, a0]
        public static (double[] num, double[] den) ModelToPolynomial(SensorModelDescriptor m)
        {
            if (m.Type == SensorType.FirstOrder)
            {
                // G(s) = K / (tau s + 1) => numerator: [K], denominator: [tau, 1]
                return (new double[] { m.K }, new double[] { m.Tau, 1.0 });
            }
            else if (m.Type == SensorType.SecondOrder)
            {
                // G(s) = K * (s + z) / (s^2 + 2*zeta*wn*s + wn^2)
                double z = 0.0; // if none given, numerator: K*s  (we can set zero at 0)
                // numerator: K*[1, z] means K*s + K*z  -> coefficients [K, K*z]
                double[] num = new double[] { m.K, m.K * z };
                double[] den = new double[] { 1.0, 2.0 * m.zeta * m.wn, m.wn * m.wn };
                return (num, den);
            }
            else // Geiger approximated as first-order shaping (ignore delay in poles)
            {
                // K / (tau s + 1)
                return (new double[] { m.K }, new double[] { m.Tau, 1.0 });
            }
        }

        // polynomial multiply (coeff arrays descending)
        public static double[] PolyMultiply(double[] a, double[] b)
        {
            int na = a.Length, nb = b.Length;
            double[] r = new double[na + nb - 1];
            for (int i = 0; i < na; i++)
                for (int j = 0; j < nb; j++)
                    r[i + j] += a[i] * b[j];
            return r;
        }

        // polynomial add (align degrees)
        public static double[] PolyAdd(double[] a, double[] b)
        {
            int na = a.Length, nb = b.Length;
            int n = Math.Max(na, nb);
            double[] ra = new double[n]; double[] rb = new double[n];
            // align to right (lowest order)
            for (int i = 0; i < na; i++) ra[n - na + i] = a[i];
            for (int i = 0; i < nb; i++) rb[n - nb + i] = b[i];
            double[] r = new double[n];
            for (int i = 0; i < n; i++) r[i] = ra[i] + rb[i];
            return r;
        }

        // combine multiple transfer functions (sum in parallel): return numerator & denominator for G_total(s)
        public static (double[] numTot, double[] denTot) CombineTransferFunctions(IEnumerable<(double[] num, double[] den)> mods)
        {
            // sum G_i = N_i/D_i => common denominator = product of all D_i
            // compute commonDen = Π D_i
            List<double[]> dens = mods.Select(x => x.den).ToList();
            double[] commonDen = new double[] { 1.0 };
            foreach (var d in dens) commonDen = PolyMultiply(commonDen, d);

            // each term N_i * (commonDen / D_i)
            double[] totalNum = new double[commonDen.Length];
            foreach (var m in mods)
            {
                // factor = commonDen / m.den -> compute by polynomial division? simpler: multiply all other denominators
                double[] other = new double[] { 1.0 };
                foreach (var d in dens)
                {
                    if (!ReferenceEquals(d, m.den))
                        other = PolyMultiply(other, d);
                    else
                    {
                        // but if there are repeated equal arrays different instances, the reference check fails
                        // to avoid complexity: compute factor as commonDen divided by m.den (polynomial long division)
                    }
                }
                // safer approach: compute factor = commonDen / m.den using polynomial long division
                double[] factor = PolyDivide(commonDen, m.den);
                double[] term = PolyMultiply(m.num, factor);
                totalNum = PolyAdd(totalNum, term);
            }
            // trim leading zeros
            totalNum = TrimLeadingZeros(totalNum);
            commonDen = TrimLeadingZeros(commonDen);
            return (totalNum, commonDen);
        }

        // polynomial long division: returns quotient (assumes divisor degree <= dividend)
        public static double[] PolyDivide(double[] dividend, double[] divisor)
        {
            // both arrays descending
            dividend = (double[])dividend.Clone();
            int n = dividend.Length, m = divisor.Length;
            if (m > n) return new double[] { 0.0 };
            double[] quotient = new double[n - m + 1];
            double[] rem = (double[])dividend.Clone();
            for (int k = 0; k <= n - m; k++)
            {
                double coeff = rem[k] / divisor[0];
                quotient[k] = coeff;
                for (int j = 0; j < m; j++)
                {
                    rem[k + j] -= coeff * divisor[j];
                }
            }
            return TrimLeadingZeros(quotient);
        }

        private static double[] TrimLeadingZeros(double[] p)
        {
            int idx = 0;
            while (idx < p.Length && Math.Abs(p[idx]) < 1e-14) idx++;
            if (idx == 0) return p;
            if (idx == p.Length) return new double[] { 0.0 };
            double[] r = new double[p.Length - idx];
            Array.Copy(p, idx, r, 0, r.Length);
            return r;
        }

        // build models helper from descriptors
        public static List<(double[] num, double[] den)> BuildSensorModels(params SensorModelDescriptor[] descs)
        {
            var list = new List<(double[] num, double[] den)>();
            foreach (var d in descs) list.Add(ModelToPolynomial(d));
            return list;
        }

        // Find polynomial roots via companion matrix eigenvalues (returns System.Numerics.Complex[])
        public static Complex[] FindPolynomialRoots(double[] poly)
        {
            poly = TrimLeadingZeros(poly);
            int n = poly.Length - 1; // degree
            if (n <= 0) return new Complex[0];
            // companion matrix (n x n), monic polynomial x^n + a_{n-1} x^{n-1} + ... + a0
            double lead = poly[0];
            // build monic coefficients c[i] = poly[i]/lead
            var c = new double[poly.Length];
            for (int i = 0; i < poly.Length; i++) c[i] = poly[i] / lead;
            // companion: first row = -c[1..n], subdiagonal ones
            var M = DenseMatrix.Create(n, n, Complex.Zero);
            for (int j = 0; j < n; j++)
                M[0, j] = new Complex(-c[1 + j], 0.0);
            for (int i = 1; i < n; i++)
                M[i, i - 1] = Complex.One;
            // eigenvalues
            var evd = M.Evd();
            var eig = evd.EigenValues.ToArray(); // Complex[]
            return eig;
        }

        // Discretize to z-domain: simple bilinear transform (Tustin) or ZOH approximation
        // Here we implement bilinear transform on polynomials using substitution s = 2/T * (z-1)/(z+1)
        // For simplicity we use a numeric approach: evaluate frequency response and fit? (heavy)
        // Instead: do zero-order hold approximation by converting continuous TF to discrete via matched pole mapping approx
        // To keep it simple and robust, we will approximate by mapping poles p -> z = exp(p*Ts) and zeros similarly (if finite)
        public static (double[] numZ, double[] denZ) DiscretizeToZ(double[] numS, double[] denS, double Ts)
        {
            // find poles and zeros in s
            var zerosS = FindPolynomialRoots(numS);
            var polesS = FindPolynomialRoots(denS);

            // map to z: z = exp(p*Ts)
            var zerosZ = zerosS.Select(z => Complex.Exp(z * Ts)).ToArray();
            var polesZ = polesS.Select(p => Complex.Exp(p * Ts)).ToArray();

            // build poly from roots: poly(z) = prod (z - root)
            Complex[] numZc = PolyFromRoots(zerosZ);
            Complex[] denZc = PolyFromRoots(polesZ);

            // convert Complex coefficients to real doubles (if imaginary parts small)
            double[] numZ = numZc.Select(c => Math.Abs(c.Imaginary) < 1e-8 ? c.Real : c.Real).ToArray();
            double[] denZ = denZc.Select(c => Math.Abs(c.Imaginary) < 1e-8 ? c.Real : c.Real).ToArray();

            // normalize so leading den = 1
            if (denZ.Length > 0 && Math.Abs(denZ[0]) > 1e-12)
            {
                double lead = denZ[0];
                for (int i = 0; i < denZ.Length; i++) denZ[i] /= lead;
                for (int i = 0; i < numZ.Length; i++) numZ[i] /= lead;
            }
            return (numZ, denZ);
        }

        // Given complex roots, build polynomial coefficients (descending) as Complex[]
        public static Complex[] PolyFromRoots(Complex[] roots)
        {
            // start with poly = [1]
            Complex[] poly = new Complex[] { Complex.One };
            foreach (var r in roots)
            {
                // multiply poly by (z - r)
                Complex[] next = new Complex[poly.Length + 1];
                for (int i = 0; i < next.Length; i++) next[i] = Complex.Zero;
                for (int i = 0; i < poly.Length; i++)
                {
                    next[i] += poly[i];
                    next[i + 1] -= poly[i] * r;
                }
                poly = next;
            }
            return poly;
        }
    }
}
