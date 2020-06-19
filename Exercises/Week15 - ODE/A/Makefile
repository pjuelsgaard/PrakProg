A.svg : A.gpi out.txt
	gnuplot $<

out.txt : A.exe
	mono $< > $@

A.exe : A.cs matlib.dll
	mcs $< -reference:$(word 2, $^) > $@

matlib.dll : ../rk.cs ../vector.cs
	mcs -target:library -out:$@ $^

clean:
	rm -f *.svg *.txt *.dll
