all: out.txt
	cat $<


out.txt : A.exe
	mono $< > $@

A.exe : A.cs matlib.dll
	mcs $< -reference:matlib.dll >$@

matlib.dll : ../integrator.cs ../vector.cs
	mcs -target:library -out:$@ $^

clean:
	rm -f *.txt
