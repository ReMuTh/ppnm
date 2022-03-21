LIBDIR = ../../matlib

all: fig_oscillator.png
fig_oscillator.png: fig.oscillator.gpi oscillator.data.txt
	gnuplot $<

oscillator.data.txt log: main.exe
	mono $< 1 | sed 's/,/./g' > $@ 2>log

main.exe: main.cs matlib.dll
	mcs -target:exe -out:main.exe -reference:matlib.dll main.cs

matlib.dll: \
	$(LIBDIR)/odeint/ode.cs \
	$(LIBDIR)/matrix/vector.cs
	mcs -target:library -out:./$@ $^

clean:
	$(RM) *.dll *.exe *.png *.txt log*
