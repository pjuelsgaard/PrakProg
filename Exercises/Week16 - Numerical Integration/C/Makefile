all: out.txt
	cat $<

out.txt : C.exe
	mono $< > $@

C.exe : C.cs integrate.dll
	mcs $< -reference:$(word 2, $^) -out:$@

integrate.dll : ../integrator.cs ../vector.cs
	mcs -target:library -out:$@ $^

clean:
	rm -f *.txt *.dll
