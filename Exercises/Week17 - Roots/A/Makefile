all: out.txt
	cat $<

out.txt : A.exe
	mono $< > $@

A.exe : A.cs matlib.dll
	mcs $< -reference:$(word 2,$^) -out:$@

matlib.dll : ../matrix.cs ../vector.cs ../root.cs
	mcs -target:library -out:$@ $^

clean :
	rm -f *.dll *.txt *.exe
