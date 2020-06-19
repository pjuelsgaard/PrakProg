all: out.txt
	cat $<


out.txt : B.exe
	mono $< > $@

B.exe : B.cs matlib.dll
	mcs $< -reference:matlib.dll >$@

matlib.dll : ../integrator.cs ../vector.cs
	mcs -target:library -out:$@ $^

clean:
	rm -f *.txt *.dll
