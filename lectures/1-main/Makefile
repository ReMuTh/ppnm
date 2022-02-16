Out.txt: main.exe
	mono main.exe > Out.txt

main.exe: main.cs hello.dll
	mcs main.cs -target:exe -reference:hello.dll

hello.dll: hello.cs world.cs
	mcs hello.cs world.cs -target:library -out:hello.dll

clean:
	$(RM) *.exe *.dll [Oo]ut.txt

test:
	echo $(RM)
