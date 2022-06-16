import numpy as np
from scipy.interpolate import Akima1DInterpolator as akima
from matplotlib import pyplot as plt

input_data = np.loadtxt('data/input_data_endpoints.txt')
akima_interpol = np.loadtxt('data/interpol_akima_endpoints.txt')
naive_interpol = np.loadtxt('data/interpol_naive_endpoints.txt')
bica_interpol = np.loadtxt('data/interpol_bica_endpoints.txt')

x = input_data[:,0]
y = input_data[:,1]
n = len(x);

rand_akima = akima(x,y)

xs = np.linspace(x[0], x[n-1],200)
plt.figure(figsize=(10,6))
plt.title("Comparing C# and Python Akima sub-splines",fontweight="bold")
plt.scatter(x,y,label="Input points")
plt.plot(xs,rand_akima(xs),label="SciPy Akima interpolation",linewidth=2)
plt.plot(akima_interpol[:,0],akima_interpol[:,1],label='C# Akima end-points', linestyle='dashed')
plt.plot(naive_interpol[:,0],naive_interpol[:,1],label='C# Naive end-points', linestyle='dashed')
plt.plot(bica_interpol[:,0],bica_interpol[:,1],label='C# Bica end-points', linestyle='dashed')
plt.legend()
plt.tight_layout()
plt.savefig("fig/scipy_comparison.pdf")
plt.show()