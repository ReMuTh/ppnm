all: out.txt Thx_decay.svg; cat $<

Thx_decay.svg: thx_data.txt thx_plot.gpi	
	gnuplot thx_plot.gpi

out.txt thx_data.txt: main.exe
	mono $< > $@

main.exe: main.cs matrix.dll QRGS.dll
	mcs -reference:matrix.dll -reference:QRGS.dll main.cs

QRGS.dll : ../matlib/QRGS.cs matrix.dll
	mcs -target:library -reference:matrix.dll -out:$@  ../matlib/QRGS.cs

matrix.dll : ../matlib/vector.cs ../matlib/matrix.cs ../matlib/matrix_rene.cs
	mcs -target:library -out:$@ $^

clean:
	rm *.dll *.exe [Oo]ut*