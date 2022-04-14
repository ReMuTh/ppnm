all: out.txt #erf.gnuplot.svg
	cat out.txt

#erf.gnuplot.svg: erf.data.txt erf.tab.txt erf.plot.gpi	
#	gnuplot erf.plot.gpi	

erf.data.txt out.txt: main.exe
	mono $< > $@

main.exe: main.cs integrate.dll
	mcs -reference:integrate.dll main.cs


integrate.dll : ../lib/integrate.cs
	mcs -target:library -out:$@ $^

clean:
	rm *.dll *.exe *.eps [Oo]ut* *.svg