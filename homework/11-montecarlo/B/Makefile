all: out.txt
	cat out.txt

out.txt: main.exe
	mono $< > $@

main.exe: main.cs lib.dll
	mcs -reference:lib.dll main.cs

lib.dll : ../lib/vector.cs ../lib/vector_rene.cs ../lib/montecarlo.cs
	mcs -target:library -out:$@ $^

clean:
	rm *.dll *.exe [Oo]ut* *.svg *.txt