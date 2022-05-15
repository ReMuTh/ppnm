all: out.txt 
	cat out.txt

erf.data.txt out.txt: main.exe
	mono $< > $@

main.exe: main.cs integrate.dll
	mcs -reference:integrate.dll main.cs


integrate.dll : ../lib/integrate.cs
	mcs -target:library -out:$@ $^

clean:
	rm *.dll *.exe [Oo]ut*