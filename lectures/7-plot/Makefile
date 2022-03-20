
erf.gnuplot.png: erf.data.txt erf.tab.txt Makefile
	echo "\
	set terminal png \n\
	set output '$@' \n\
	set xlabel 'x' \n\
	set ylabel 'y' \n\
	set key left top \n\
	set title 'Error function' \n\
	plot [-1.5:1.5] [-2:2] '$<' index 0 using 1:2 with lines title 'Error function',\
	'erf.tab.txt' with points title 'tab' \n\
	"| tee log.gpi | gnuplot
	
erf.tab.txt: Makefile
	echo 0 0 1 >$@
	echo 0.4	0.428392355	0.571607645 >>$@
	echo 0.8	0.742100965	0.257899035 >>$@
	echo 0.9	0.796908212	0.203091788 >>$@

erf.data.txt: main.exe
	mono $< | sed 's/,/./g' > $@

%.exe: %.cs
	mcs $<

PHONY: clean
clean:
	rm *.exe
