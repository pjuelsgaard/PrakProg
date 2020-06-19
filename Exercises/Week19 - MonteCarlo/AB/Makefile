B.svg: B.gpi out.txt
	gnuplot $<

out.txt : AB.exe
	mono $< 2> $@

AB.exe : AB.cs matlib.dll
	mcs $< -reference:$(word 2,$^) -out:$@

matlib.dll : ../matrix.cs ../vector.cs ../mc.cs ../fit.cs
	mcs -target:library -out:$@ $^

clean :
	rm -f *.dll *.txt *.exe

