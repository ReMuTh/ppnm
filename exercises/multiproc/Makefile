CSC = mcs
RUN = mono

all: Out.txt
	Cat Out.txt

Out.txt: t2.exe t1.exe Makefile
	cat /dev/null > $@
	time $(RUN) t1.exe   5e7 >> $@
	time $(RUN) t2.exe   5e7 >> $@

t2.exe: t2.cs
	$(CSC) -target:exe -out:$@ $<

t1.exe: t1.cs
	$(CSC) -target:exe -out:$@ $<

clean:
	$(RM) *.dll *.exe [Oo]ut* [Ll]og*