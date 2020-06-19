all: out.txt
	cat $<

out.txt : B.exe
	mono $< > $@

B.exe : B.cs matlib.dll CERN.dat
	mcs $< -reference:$(word 2,$^) -out:$@

matlib.dll : ../matrix.cs ../vector.cs ../minimize.cs
	mcs -target:library -out:$@ $^

clean :
	rm -f *.dll *.txt *.exe

