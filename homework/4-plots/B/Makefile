gamma.gnuplot.png gamma.gnuplot.eps lngamma.gnuplot.png lngamma.gnuplot.eps: gamma.data.txt gamma.tab.txt gamma.plot.gpi lngamma.data.txt lngamma.tab.txt lngamma.plot.gpi Makefile
	gnuplot gamma.plot.gpi
	gnuplot lngamma.plot.gpi	

gamma.data.txt lngamma.data.txt: main.exe
	mono main.exe gamma | sed 's/,/./g' > gamma.data.txt
	mono main.exe lngamma | sed 's/,/./g' > lngamma.data.txt

%.exe: %.cs
	mcs $<

PHONY: clean
clean:
	rm *.exe
	rm *.png
	rm *.eps
	rm lngamma.data.txt
	rm gamma.data.txt
