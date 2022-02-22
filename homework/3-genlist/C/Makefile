CSC = mcs
RUN = mono
OUT = Out.data
IN = input.data

all:$(OUT)
	cat $<

$(OUT): main.exe $(IN)
	> $@
	echo 'Exercise "genlist" part C: chained-node generic list' >> $@
	echo '\ninput: irregular table:' >>$@
	cat $(IN) >> $@
	echo '\noutput: regular table:' >>$@
	$(RUN) main.exe < $(IN) >> $@

$(IN): Makefile
	> $@
	echo -1.23 '   '  23.4 '\t   \t' 456.7 >> $@
	echo '  \t    ' 0.07 '  \t\t\t     '  -7.4e8 '\t' 6.72 >> $@
	echo 0.53 23.4 4.5 >> $@

main.exe:main.cs genlist.dll
	$(CSC) -t:exe -out:$@ $(addprefix -r:,$(filter %.dll,$^)) $(filter %.cs,$^)

genlist.dll:genlist.cs
	$(CSC) -target:library -out:$@ $<

clean:
	$(RM) *.dll *.exe *.data